namespace backend_product.ViewModels
{
    public class ProductRes
    {
        public string? STATUS { get; set; }
        public string? MESSAGE { get; set; }
        public DataProduct? RESULT { get; set; }
    }

    public class ProductResList
    {
        public string? STATUS { get; set; }
        public string? MESSAGE { get; set; }
        public List<DataProduct>? RESULT { get; set; }
    }

    public class DataProduct
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public Decimal? Price { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
