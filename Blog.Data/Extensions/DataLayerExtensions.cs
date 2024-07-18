using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Blog.Data.Context;
using Blog.Data.Repositories.Abstractions;
using Blog.Data.Repositories.Concretes;
using Blog.Data.UnitOfWorks;

namespace Blog.Data.Extensions
{
    public static class DataLayerExtensions
    {
        public static IServiceCollection LoadDataLayerExtension(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            // Configure the DbContext with the correct migrations assembly
            services.AddDbContext<AppDbContext>(opt =>
                opt.UseSqlServer(
                    config.GetConnectionString("DefaultConnection"),
                    x => x.MigrationsAssembly("Blog.Data")  // Specify the assembly that contains the migrations
                )
            );

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}
