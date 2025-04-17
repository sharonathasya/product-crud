namespace frontend_product.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Decimal Price { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class ProductReq
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
    }

}
