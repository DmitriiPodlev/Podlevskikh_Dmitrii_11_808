using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Vk.Services;

namespace Vk
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public delegate IStorage ServiceResolver(string key);

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IStorage, BlogEntriesStorage>();
            services.AddTransient<PostEntriesStorage>();
            services.AddTransient<CommentEntriesStorage>();
            services.AddTransient<ServiceResolver>(serviceProvider => key =>
            {
                switch (key)
                {
                    case "post":
                        return serviceProvider.GetService<PostEntriesStorage>();
                    case "comment":
                        return serviceProvider.GetService<CommentEntriesStorage>();
                    default:
                        throw new KeyNotFoundException();
                }
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ServiceResolver serviceResolver)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", new HomeController(serviceResolver).GetForm);
                endpoints.MapPost("/Home/AddEntry", new HomeController(serviceResolver).AddEntry);
                endpoints.MapGet("/Home/GetAllPosts", new HomeController(serviceResolver).GetPosts);
                endpoints.MapPost("/Home/RemovePost/{id}", new HomeController(serviceResolver).RemovePost);
                endpoints.MapGet("/Home/EditPost/{id}", new HomeController(serviceResolver).EditPost);
                endpoints.MapPost("/Home/EditPost/{id}", new HomeController(serviceResolver).EditPost);
                endpoints.MapGet("/Home/AddComment/{id}", new HomeController(serviceResolver).AddComment);
                endpoints.MapPost("/Home/AddComment/{id}", new HomeController(serviceResolver).AddComment);
                endpoints.MapGet("/Home/GetComments/{id}", new HomeController(serviceResolver).GetComments);
                endpoints.MapPost("/Home/GetComments/{id}", new HomeController(serviceResolver).GetComments);
            });

        }
    }
}
