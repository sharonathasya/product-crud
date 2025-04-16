using backend_product.Helpers;
using backend_product.Interfaces;
using backend_product.Models;
using backend_product.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace backend_product.Impl
{
    public class UserService(dbContext context, ITokenManager tokenManager) : IUserService
    {
        private readonly ITokenManager _tokenManager = tokenManager;
        private readonly dbContext _dbContext = context;

        #region Register User
        public async Task<UserRes> Register(ReqAddUser request)
        {
            DateTime aDate = DateTime.Now;
            UserRes userRes = new();

            try
            {
                var currentUser = tokenManager.GetPrincipal();
                var checkdata = context.Account.Where(x => x.Username == request.Username).FirstOrDefault();
                if (checkdata == null)
                {
                    User insertUser = new User
                    {
                        Email = request.Email,
                        Phone = request.Phone,
                        Address = request.Address,
                        FirstName = request.FirstName,
                        LastName = request.LastName,
                        Gender = request.Gender,
                        BirthDate = request.BirthDate,
                        CreatedTime = aDate
                    };
                    context.User.Add(insertUser);
                    context.SaveChanges();


                    var getUserId = context.User.Where(x => x.Email == request.Email).FirstOrDefault();
                    Account insertAcc = new Account
                    {
                        UserId = getUserId.UserId,
                        Username = request.Username,
                        Password = request.Password,
                        CreatedTime = aDate,
                        IsActive = true
                    };
                    context.Account.Add(insertAcc);
                    context.SaveChanges();


                    userRes.STATUS = Constants.ResponseConstant.Success;
                    userRes.MESSAGE = Constants.ResponseConstant.SubmitSuccess;
                }
                else
                {
                    userRes.STATUS = Constants.ResponseConstant.Failed;
                    userRes.MESSAGE = Constants.ResponseConstant.UsernameExist;
                }

            }
            catch (Exception ex)
            {
                string errmsg = ex.Message;
                if (errmsg.IndexOf(Constants.ResponseConstant.LastQuery) > 0)
                    errmsg = errmsg.Substring(0, errmsg.IndexOf(Constants.ResponseConstant.LastQuery));

                userRes.STATUS = Constants.ResponseConstant.Failed;
                userRes.MESSAGE = errmsg;
            }

            return userRes;
        }
        #endregion

        #region Get User By Email
        public async Task<UserRes> GetUserByEmail(ReqIdUser request)
        {
            UserRes userRes = new();
            DataUser dataUser = new();
            try
            {
                var getData = context.Account.Where(x => x.Email == request.Email).FirstOrDefault();
                if (getData != null)
                {
                    dataUser = new()
                    {
                        Userid = getData.UserId,
                        Username = getData.Username,
                        Email = getData.Email,
                        CreatedTime = getData.CreatedTime
                    };

                    userRes.STATUS= Constants.ResponseConstant.Failed;
                    userRes.MESSAGE= Constants.ResponseConstant.EmailExist;
                    userRes.RESULT = dataUser;
                }
                else
                {
                    userRes.STATUS = Constants.ResponseConstant.NotFound;
                    userRes.MESSAGE = Constants.ResponseConstant.DataNotFound;
                }
            }
            catch (Exception ex) 
            {
                string errmsg = ex.Message;
                if (errmsg.IndexOf(Constants.ResponseConstant.LastQuery) > 0)
                    errmsg = errmsg.Substring(0, errmsg.IndexOf(Constants.ResponseConstant.LastQuery));

                userRes.STATUS = Constants.ResponseConstant.Failed;
                userRes.MESSAGE = errmsg;
            }
            return userRes;
        }
        #endregion
    }
}
