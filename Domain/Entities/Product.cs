namespace Domain.Entities
{
    public class Product
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string UnitOfMeasure { get; set; }
        public double Price { get; set; }
        public int NonProcessedQuantity { get; set; } 
    }
}




