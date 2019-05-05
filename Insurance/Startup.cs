using AutoMapper;
using ThirdParty;
using DAL;
using Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Insurance.Services;
using Microsoft.AspNetCore.Rewrite;

namespace insurance_new
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddDbContext<context>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));



            services.AddDbContext<identityContext>(options => options.UseSqlServer(Configuration.GetConnectionString("IdentityConnection")));
            services.AddIdentity<user, role>(
                config =>
                {
                    config.Password.RequireDigit = false;
                    config.Password.RequiredLength = 3;
                    config.Password.RequireNonAlphanumeric = false;
                    config.Password.RequireUppercase = false;
                    config.Password.RequiredUniqueChars = 0;
                    config.Password.RequireLowercase = false;
                    config.User.RequireUniqueEmail = false;
                }
                ).
                AddEntityFrameworkStores<identityContext>().
                AddDefaultTokenProviders();

            services.AddSingleton(d =>
            {
                return new MapperConfiguration(c =>
                {
                    c.CreateMap<insurance, insurance_client>();
                    c.CreateMap<step, step_client>();
                    c.CreateMap<fieldSet, fieldSet_client>();
                    c.CreateMap<field, field_client>();
                    c.CreateMap<dataValue, dataValue_client>().ForMember(dest => dest.text, opt => opt.MapFrom(src => src.title));
                    c.CreateMap<dataValue_category, dataValue_category_client>();
                    c.CreateMap<category, category_client>();
                    c.CreateMap<attribute, attribute_client>();
                    c.CreateMap<user, user_express>().ForMember(dest => dest.actualUserName, opt => opt.MapFrom(src => src.actualUserName));
                    c.CreateMap<paymentType, paymentType_client>();
                }
            );
            });


            services.AddScoped<repository>();
            services.AddScoped<MellatService>();
            services.AddScoped<SmsSender>();
            services.AddScoped<EmailSender>();
            services.AddScoped<HookManager>();

            services.ConfigureApplicationCookie(c =>
            {
                c.LoginPath = "/Admin/Account/Login";
            });

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {
                    HotModuleReplacement = true
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            
            app.UseRewriter(new RewriteOptions()
               .AddRedirectToWww()
            //.AddRedirectToHttps()
            );
            app.UseAuthentication();
            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                  name: "default",
                   template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                routes.MapRoute(
              name: "post",
               template: "{action=Post}/{id}/{title}/{controller=Home}");
                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Home", action = "Index" });
            });
        }


    }
}
