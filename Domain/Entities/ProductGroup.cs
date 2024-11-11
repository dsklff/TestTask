namespace Domain.Entities
{
    public class ProductGroup
    {
        public Guid Id { get; set; }
        public int Number { get; set; }
        public virtual IList<ProductGroupItem> ProductGroupItems { get; set; } = new List<ProductGroupItem>();
    }
}
