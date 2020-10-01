using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShoppingCard.Core;
using ShoppingCard.Core.Interfaces;
using ShoppingCard.Core.Operations;

namespace ShoppingCard
{
    public class Startup
    {
        private Settings _settings;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            #region Settings

            var appConfiguration = Configuration.GetSection("Settings");

            services.Configure<Settings>(appConfiguration);

            _settings = appConfiguration.Get<Settings>();

            #endregion

            #region Dependency

            services.AddSingleton<IBasketOperations, BasketOperations>();
            services.AddSingleton<ICampaignOperations, CampaignOperations>();
            services.AddSingleton<ICategoryOperations, CategoryOperations>();
            services.AddSingleton<ICouponOperations, CouponOperations>();
            services.AddSingleton<IProductOperations, ProductOperations>();

            #endregion
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
        }
    }
}