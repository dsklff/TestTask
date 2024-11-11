using Application.Interfaces;
using Domain.Entities;
using ExcelDataReader;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IApplicationDbContext _context;
        
        public ProductService(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ImportProductsFromExcel(Stream fileStream)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            using var reader = ExcelReaderFactory.CreateReader(fileStream);
            var productsToAdd = new List<Product>();

            while (reader.Read())
            {
                if (reader.Depth == 0) continue; 

                var productName = reader.GetString(0).Trim();
                var uomName = reader.GetString(1).Trim();
                var price = reader.GetDouble(2);
                var quantity = (int)reader.GetDouble(3);

                var existingProduct = await _context.Products
                    .FirstOrDefaultAsync(p => p.Name == productName);

                if (existingProduct != null)
                {
                    existingProduct.NonProcessedQuantity += quantity;
                }
                else
                {
                    var newProduct = new Product
                    {
                        Name = productName,
                        UnitOfMeasure = uomName,
                        Price = price,
                        NonProcessedQuantity = quantity
                    };
                    productsToAdd.Add(newProduct);
                }
            }

            if (productsToAdd.Any())
            {
                await _context.Products.AddRangeAsync(productsToAdd);
            }

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
