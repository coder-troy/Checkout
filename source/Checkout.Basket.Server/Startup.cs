namespace Checkout.Basket.Server
{
    using System;
    using Application;
    using Autofac;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.DependencyInjection;
    using Autofac.Extensions.DependencyInjection;
    using Domain;
    using Infrastructure;

    public class Startup
    {
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            // Create the container builder.
            var cb = new ContainerBuilder();   
            cb.RegisterType<ProductRepository>().As<IProductRepository>();
            cb.RegisterType<BasketRepository>().As<IBasketRepository>();
            cb.RegisterType<GetBasketHandler>().As<IGetBasketHandler>();            
            cb.RegisterType<UpsertBasketItemHandler>().As<IUpsertBasketItemHandler>();
            cb.RegisterType<RemoveBasketItemHandler>().As<IRemoveBasketItemHandler>();
            cb.RegisterType<RemoveAllBasketItemsHandler>().As<IRemoveAllBasketItemsHandler>();cb.Populate(services);
            var container = cb.Build();

            // Create the IServiceProvider based on the container.
            return new AutofacServiceProvider(container);
        }
        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMvc();
        }
    }
}