namespace backend_product.ViewModels
{
    public class RequestVM
    {
    }
    public class ReqLogin
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class ReqAddUser
    {
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public bool? IsActive { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public bool? Gender { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Address { get; set; }

    }
    public class ReqIdUser
    {
        public string id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
    }

    public class ReqProduct
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Decimal Price { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class ReqIdProduct
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Price { get; set; }
    }
}
