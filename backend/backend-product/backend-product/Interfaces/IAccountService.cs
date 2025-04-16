using backend_product.ViewModels;

namespace backend_product.Interfaces
{
    public interface IAccountService
    {
        Task<ResLogin> Login(ReqLogin request);
    }
}
