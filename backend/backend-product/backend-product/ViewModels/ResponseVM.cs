namespace backend_product.ViewModels
{
    public class ResponseVM
    {
    }

    public class ResLogin
    {
        public string? STATUS { get; set; }
        public string? MESSAGE { get; set; }
        public string? jwt_token { get; set; }
    }
    public class ResProduct
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Decimal Price  { get; set; }
        public DateTime CreatedAt { get; set; }

    }

}
