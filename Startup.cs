using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.AzureAD.UI;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PowerService.Data;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.HttpOverrides;
using PowerService.Util;

namespace PowerService
{
    public class Startup
    {
        private const char NewChar = '_';

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
       
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //if (Configuration["ASPNETCORE_ENVIRONMENT"].ToString() != "Development")
            //{
            //services.AddAuthentication(AzureADDefaults)
            //    .AddAzureAD(options => Configuration.Bind("AzureAd", options));
            //    services.AddAuthentication(AzureADDefaults.AuthenticationScheme)
            //       .AddAzureAD(options =>
            //       Configuration.Bind("AzureAd", options)
            //       ).AddCookie();

            //    services.Configure<OpenIdConnectOptions>(AzureADDefaults.OpenIdScheme, options =>
            //    {

            //        options.Authority = options.Authority + "/v2.0/";        // Microsoft identity platform
            //    options.TokenValidationParameters.ValidateIssuer = false; // accept several tenants (here simplified)
            //});

            //services.AddControllersWithViews(options =>
            //{
            //    var policy = new AuthorizationPolicyBuilder()
            //        .RequireAuthenticatedUser()
            //        .Build();
            //    options.Filters.Add(new AuthorizeFilter(policy));
            //});
            //}
            //else
            //{
            services.AddControllersWithViews(options =>
            {
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
                options.Filters.Add(new AllowAnonymousFilter());
            });

            //}
            services.AddRazorPages();

            services.AddDbContext<PowerServiceContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("PowerServiceContext")));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "PowerBase API",
                    Version = "v1"
                  
                });

                c.CustomOperationIds(e => $"{e.ActionDescriptor.RouteValues["controller"]}_{e.HttpMethod}_{e.RelativePath.Replace(oldChar: '/', NewChar).Replace(oldChar:'{', '_').Replace(oldChar:'}','_').TrimEnd(NewChar)}" );
                c.SchemaFilter<SwaggerIgnoreFilter>();
            });
          
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedProto
            });
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();


            app.UseRouting();
            //if (Configuration["ASPNETCORE_ENVIRONMENT"].ToString()!= "Development")
            //{
            //    app.UseAuthentication();
            //}
        
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });

            app.UseSwagger(c =>
            {
                c.SerializeAsV2 = true;
                c.PreSerializeFilters.Add((swagger, httpReq) =>
                {
                    swagger.Servers = new List<OpenApiServer> { new OpenApiServer { Url = $"{httpReq.Scheme}://{httpReq.Host.Value}" } };
                });

            });
          

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "PowerBase API");
              

            });


        }
    }
}
