using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;


namespace Application.Services
{
    public class BackgroundGroupService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<BackgroundGroupService> _logger;
        private const double targetPrice = 200;

        public BackgroundGroupService(IServiceProvider serviceProvider, ILogger<BackgroundGroupService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Group background service starting..");

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Running group background service at {Time}", DateTime.Now);

                try
                {
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        IApplicationDbContext _context = scope.ServiceProvider.GetRequiredService<IApplicationDbContext>();

                        List<Product> products = await _context.Products.Where(x => x.Price < targetPrice && x.NonProcessedQuantity > 0).OrderByDescending(x => x.Price).ToListAsync();

                        int lastNumber = 0;

                        ProductGroup? lastGroup = await _context.ProductGroups
                            .OrderByDescending(x => x.Number)
                            .FirstOrDefaultAsync();

                        if(lastGroup != null)
                        {
                            lastNumber = lastGroup.Number;
                        }

                        List<ProductGroup> productGroups = new List<ProductGroup>();

                        while (products.Any(p => p.NonProcessedQuantity > 0))
                        {
                            ProductGroup newGroup = new ProductGroup
                            {
                                Id = Guid.NewGuid(),
                                Number = ++lastNumber
                            };

                            double currentGroupTotal = 0;

                            foreach (var product in products)
                            {
                                int maxQuantityForGroup = (int)((targetPrice - currentGroupTotal) / product.Price);
                                int quantityToAdd = Math.Min(product.NonProcessedQuantity, maxQuantityForGroup);

                                if (quantityToAdd > 0)
                                {
                                    newGroup.ProductGroupItems.Add(new ProductGroupItem
                                    {
                                        Id = Guid.NewGuid(),
                                        Product = product,
                                        ProductGroup = newGroup,
                                        ProcessedQuantity = quantityToAdd
                                    });

                                    currentGroupTotal += quantityToAdd * product.Price;
                                    product.NonProcessedQuantity -= quantityToAdd;
                                }

                                if (currentGroupTotal == targetPrice)
                                    break;
                            }
                             
                            productGroups.Add(newGroup);
                        }

                        await _context.ProductGroups.AddRangeAsync(productGroups);

                        await _context.SaveChangesAsync();
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while running the product grouping.");
                }

                await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
            }

            _logger.LogInformation("Group background service stopping..");
        }
    }
}
