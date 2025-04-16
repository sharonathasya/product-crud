using backend_product.Helpers;
using backend_product.Interfaces;
using backend_product.Models;
using backend_product.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace backend_product.Impl
{
    public class AccountService(dbContext context, ITokenManager tokenManager) : IAccountService
    {
        private readonly ITokenManager _tokenManager = tokenManager;
        private readonly dbContext _dbContext = context;


        public async Task<ResLogin> Login(ReqLogin request)
        {
            DateTime aDate = DateTime.Now;
            var keyHash = GetConfig.AppSetting["AppSettings:KeyHash"];
            var tokenlifetime = GetConfig.AppSetting["AppSettings:TokenLifetime"];
            var issuer = GetConfig.AppSetting["AppSettings:Issuer"];
            ResLogin resLogin = new ResLogin();
            try
            {
                var getUser = context.Account.Where(x => x.Username == request.Username && x.Password == request.Password).FirstOrDefault();
                if (getUser != null)
                {
                    if (getUser.IsActive == true)
                    {
                        var KeyJWT = Encoding.ASCII.GetBytes(GetConfig.AppSetting["AppSettings:Secret"]);
                        var tokenHandler = new JwtSecurityTokenHandler();
                        var tokenDescriptor = new SecurityTokenDescriptor
                        {
                            Subject = new ClaimsIdentity(new Claim[]
                              {
                            new Claim("USERID", getUser.UserId.ToString() ),
                            new Claim("USERNAME",getUser.Username),
                            new Claim("ACCOUNTID", getUser.Id.ToString() ),
                              }),
                            Expires = DateTime.UtcNow.AddMinutes(double.Parse(GetConfig.AppSetting["AppSettings:TokenLifetime"])),
                            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(KeyJWT), SecurityAlgorithms.HmacSha256Signature),
                            Issuer = GetConfig.AppSetting["AppSettings:Issuer"]

                        };
                        var token = tokenHandler.CreateToken(tokenDescriptor);
                        resLogin.jwt_token = tokenHandler.WriteToken(token);
                        resLogin.STATUS = Constants.ResponseConstant.Success;
                        resLogin.MESSAGE = Constants.ResponseConstant.SubmitSuccess;
                    }
                    else
                    {
                        resLogin.STATUS = Constants.ResponseConstant.Invalid;
                        resLogin.MESSAGE = Constants.ResponseConstant.InvalidLogin;
                    }

                }
            }
            catch (Exception ex)
            {
                resLogin.STATUS = Constants.ResponseConstant.Invalid;
                resLogin.MESSAGE = ex.Message;
            }

            return resLogin;
        }
    }
}
