using backend_product.ViewModels;

namespace backend_product.Interfaces
{
    public interface IUserService
    {
        Task<UserRes> Register(ReqAddUser request);
        Task<UserRes> GetUserByEmail(ReqIdUser request);
    }
}
