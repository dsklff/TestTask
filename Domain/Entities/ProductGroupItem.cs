namespace Domain.Entities
{
    public class ProductGroupItem
    {
        public Guid Id { get; set; }
        public required virtual Product Product { get; set; }
        public required virtual ProductGroup ProductGroup { get; set; }
        public int ProcessedQuantity { get; set; }
    }
}


