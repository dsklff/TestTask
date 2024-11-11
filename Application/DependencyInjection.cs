using Application.Interfaces;
using Application.Models.Profiles;
using Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IGroupService, GroupService>();
            services.AddHostedService<BackgroundGroupService>();

            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            }, typeof(AutoMapperProfile).Assembly);

            return services;
        }
    }
}
