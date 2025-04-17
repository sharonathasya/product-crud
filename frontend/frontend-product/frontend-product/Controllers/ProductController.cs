using frontend_product.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Net.Http.Headers;
using System.Text;

namespace frontend_product.Controllers
{
    public class ProductController : BaseController
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiUrl = "https://localhost:44339/Product/";
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProductController(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<IActionResult> Index()
        {
            var IsLoggin = IsUserLoggedIn();
            if (IsLoggin)
            {



                return View();
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }

        }

        public async Task<IActionResult> CreateProduct()
        {
            var IsLoggin = IsUserLoggedIn();
            if (IsLoggin)
            {


                return View("/Views/Product/Index.cshtml");
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }

        }

        public async Task<IActionResult> Edit(ProductReq model, Product request, string action)
        {
            if (model.Id == null)
            {
                return View("/Views/Product/ListProduct.cshtml");
            }
            else
            {
                var IsLoggin = IsUserLoggedIn();
                if (IsLoggin)
                {

                    var token = _httpContextAccessor.HttpContext.Session.GetString("JWToken");
                    if (token != null)
                    {
                        if(action != "SubmitEdit")
                        {
                            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                            var json = JsonConvert.SerializeObject(model);
                            var content = new StringContent(json, Encoding.UTF8, "application/json");

                            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                            var response = await _httpClient.PostAsync(_apiUrl + "GetProductById", content);


                            if (response.IsSuccessStatusCode)
                            {
                                var result = await response.Content.ReadAsStringAsync();
                                var data = JsonConvert.DeserializeObject<ServiceResponseSingle<Product>>(result);
                                if (data.STATUS == "SUCCESS")
                                {

                                    TempData["SuccessMessage"] = data.MESSAGE;
                                    return View("/Views/Product/EditProduct.cshtml", data.RESULT);
                                }
                                else
                                {
                                    TempData["ErrorMessage"] = data.MESSAGE;
                                    return RedirectToAction("ListData", "Product");
                                }
                            }
                            else
                            {
                                TempData["ErrorMessage"] = " data.MESSAGE";
                                return RedirectToAction("ListData", "Product");
                            }
                        }
                        else
                        {
                            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                            var jsonEdit = JsonConvert.SerializeObject(request);
                            var contentEdit = new StringContent(jsonEdit, Encoding.UTF8, "application/json");

                            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                            var responseEdit = await _httpClient.PostAsync(_apiUrl + "EditProduct", contentEdit);


                            if (responseEdit.IsSuccessStatusCode)
                            {
                                var resultEdit = await responseEdit.Content.ReadAsStringAsync();
                                var dataEdit = JsonConvert.DeserializeObject<ServiceResponseSingle<List<ResDataProduct>>>(resultEdit);
                                if (dataEdit.STATUS == "SUCCESS")
                                {

                                    var safeData = dataEdit.RESULT ?? new List<ResDataProduct>();
                                    return RedirectToAction("ListData", "Product");
                                }
                                else
                                {
                                    var safeData = dataEdit.RESULT ?? new List<ResDataProduct>();
                                    return RedirectToAction("ListData", "Product");
                                }
                            }
                        }
                        
                    }
                    else
                    {
                        return RedirectToAction("Login", "Login");
                    }

                }
                else
                {
                    return RedirectToAction("Login", "Login");
                }


                return View("/Views/Attachment/ListAttachment.cshtml");
            }

            

        }

        public async Task<IActionResult> ListData()
        {
            var IsLoggin = IsUserLoggedIn();
            if (IsLoggin)
            {

                var token = _httpContextAccessor.HttpContext.Session.GetString("JWToken");
                if (token != null)
                {
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    var model = new
                    {
                        Id = "",
                        Name = "",
                        Price = ""
                    };

                    var json = JsonConvert.SerializeObject(model);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var request = new HttpRequestMessage(HttpMethod.Post, _apiUrl + "GetProductList")
                    {
                        Content = content
                    };
                    var response = await _httpClient.SendAsync(request);
                    var ss = response;

                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        var data = JsonConvert.DeserializeObject<ServiceResponseSingle<List<ResDataProduct>>>(result);
                        if (data.STATUS == "SUCCESS")
                        {

                            var safeData = data.RESULT ?? new List<ResDataProduct>();
                            return View("/Views/Product/ListProduct.cshtml", safeData);
                        }
                        else
                        {
                            var safeData = data.RESULT ?? new List<ResDataProduct>();
                            return View("/Views/Product/ListProduct.cshtml", safeData);
                        }
                    }
                    else
                    {
                        TempData["ErrorMessage"] = " data.MESSAGE";
                        return RedirectToAction("ListProduct", "Product");
                    }
                }
                else
                {
                    return RedirectToAction("Login", "Login");
                }
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }


            return View("/Views/Product/ListProduct.cshtml");
        }


        [HttpPost]
        public async Task<IActionResult> SubmitData(Product model)
        {

            var token = _httpContextAccessor.HttpContext.Session.GetString("JWToken");
            if (token != null)
            {
                var json = JsonConvert.SerializeObject(model);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await _httpClient.PostAsync(_apiUrl + "AddProduct", content);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<ServiceResponseSingle<string>>(result);
                    

                    if (data.STATUS == "SUCCESS")
                    {

                        TempData["SuccessMessage"] = data.MESSAGE;
                        return RedirectToAction("ListData", "Product");
                    }
                    else
                    {
                        TempData["ErrorMessage"] = data.MESSAGE;
                        return RedirectToAction("ListData", "Product");
                    }

                }
                else
                {
                    TempData["ErrorMessage"] = " data.MESSAGE";
                    return RedirectToAction("ListData", "Product");
                }
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }



            return View("/Views/Login/index.cshtml");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(ProductReq model)
        {

            if (model.Id == null)
            {
                return View("/Views/Product/ListProduct.cshtml");
            }
            else
            {
                var IsLoggin = IsUserLoggedIn();
                if (IsLoggin)
                {
                        var token = _httpContextAccessor.HttpContext.Session.GetString("JWToken");
                        if (token != null)
                        {
                            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                            var json = JsonConvert.SerializeObject(model);
                            var content = new StringContent(json, Encoding.UTF8, "application/json");

                            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                            var response = await _httpClient.PostAsync(_apiUrl + "DeleteProduct", content);


                            if (response.IsSuccessStatusCode)
                            {
                                var result = await response.Content.ReadAsStringAsync();
                                var data = JsonConvert.DeserializeObject<ServiceResponseSingle<ResDataProduct>>(result);
                                if (data.STATUS == "SUCCESS")
                                {

                                    TempData["SuccessMessage"] = data.MESSAGE;
                                    return RedirectToAction("ListData", "Product");
                                }
                                else
                                {
                                    TempData["ErrorMessage"] = data.MESSAGE;
                                    return RedirectToAction("ListData", "Product");
                                }
                            }
                            else
                            {
                                TempData["ErrorMessage"] = " data.MESSAGE";
                                return RedirectToAction("ListData", "Product");
                            }
                        }
                        else
                        {
                            return RedirectToAction("Login", "Login");
                        }
                    
                }
                else
                {
                    return RedirectToAction("Login", "Login");
                }


                return View("/Views/Attachment/ListAttachment.cshtml");
            }

        }
    }
}
