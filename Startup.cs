using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OurShop.Models;
using OurShop.Models.Databinding;
using OurShop.Models.Interface;
using OurShop.Models.Respitory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OurShop
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
            services.AddDbContext<DBCONTEX>(options => options.UseSqlServer(Configuration["SportStoreProducts:ConnectionString"]));
            services.AddIdentity<UserPlusModel, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireDigit = false;
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);


                options.SignIn.RequireConfirmedEmail = true;
            })
                .AddEntityFrameworkStores<DBCONTEX>()
                .AddDefaultTokenProviders()
                ;
            services.ConfigureApplicationCookie(op => op.AccessDeniedPath = new PathString("/Account/AccessDenied")
              ) ;
            //services.AddDefaultIdentity<>();
            services.Configure<DataProtectionTokenProviderOptions>(p => p.TokenLifespan = TimeSpan.FromDays(60));
            services.AddMvc(options =>
            {
                var policy = new AuthorizationPolicyBuilder()
.RequireAuthenticatedUser().Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            })
                .AddXmlDataContractSerializerFormatters();

            services.AddAuthentication().AddGoogle(p =>
            {
                p.ClientId = "517899196110-n7menp3ujbitonm67b029d1ioadvd64p.apps.googleusercontent.com";
                p.ClientSecret = "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";

            });

            //services.AddMailKit(config => config.UseMailKit(Configuration.GetSection("Email").Get<MailKitOptions>()));
            services.AddTransient<IRefrance, RefranceResp>();
            services.AddTransient<ILoginTrack,LoginTrackResp>();
            services.AddTransient<IWhastapp, WhastappResp>();
            services.AddTransient<IUserAccount, UserAccountResp>();
            services.AddTransient<IPay, PayResp>();
            services.AddTransient<ISubscribeStatuse, SubscribeStatuseResp>();
            services.AddTransient<IAdmin, AdminResp>();
            services.AddTransient<ISubscribe, SubscribeResp>();
            services.AddTransient<ISubscribtionType, SubscribtionTypeResp>();
            services.AddTransient<IBrand, BrandResp>();
            services.AddTransient<ICategory, CategoryResp>();

            services.AddTransient<ICashOnDe, CashOnDeResp>();
            services.AddTransient<IBankType, BankTypeResp>();
            services.AddTransient<IDirectbank, DirectbankResp>();
            services.AddTransient<IPayment, PaymentResp>();
            services.AddTransient<IOnlinePayMent, OnlinePayMentResp>();
            services.AddTransient<ICity, CityResp>();
            services.AddTransient<IColor, ColorResp>();
            services.AddTransient<IDelivery, DeliveryResp>();
            services.AddTransient<IGander, GanderResp>();
            services.AddTransient<IIteam, IteamResp>();
            services.AddTransient<ILike, LikeResp>();
            services.AddTransient<IQoutation, QoutationResp>();
            services.AddTransient<ISize, SizeResp>();
            services.AddTransient<IType, TypeResp>();
            services.AddTransient<ISupplier, SupplierResp>();
            services.AddTransient<ICustomer, CustomerResp>();
            services.AddTransient<IImage, ImageResp>();
            services.AddTransient<ISubub, SububResp>();

            services.AddTransient<IDeliveryStatuse, DeliveryStatuseResp>();
            services.AddTransient<IQoutationDetailz, QoutationDetailzResp>();
            services.AddTransient<IBrandDeatail, BrandDeatailResp>();
            services.AddTransient<IContactDetail, ContactDetailResp>();
            services.AddTransient<IIteamStatuse, IteamStatusResp>();
            services.AddTransient<ILocation, LocationResp>();
            services.AddTransient<IOderType, OderTypeResp>();
            services.AddTransient<IPaymentType, PaymentTypeResp>();
            services.AddTransient<IQoutationBySuplier, QoutationBySuplierResp>();
            services.AddTransient<IQoutationStatuse, QoutationStatuseResp>();
            services.AddTransient<IRating, RatingResp>();
            services.AddTransient<IReserve, ReserveResp>();
            services.AddTransient<IResident, ResidentResp>();
            services.AddTransient<IServiceCategory, ServiceCategoryResp>();
            services.AddTransient<IServiceReserveStatuse, ServiceReserveStatuseResp>();
            services.AddTransient<IService, ServiceResp>();
            services.AddTransient<IServiceType, ServiceTypeResp>();
            services.AddTransient<ISeviceStause, SeviceStauseResp>();
            //services.AddTransient<ISize, SizeResp>();
            services.AddTransient<IStatus, StatusResp>();
            services.AddTransient<IIteamDetail, IteamDetailResp>();
            services.AddTransient<ISupplierStatuse, SupplierStatuseResp>();
            services.AddTransient<ISupplierType, SupplierTypeResp>();

            services.AddControllersWithViews();
            services.AddScoped<Cart>(sp => SessionCart.GetCart(sp));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddMvc();
            services.AddMemoryCache();
            services.AddSession();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                name: "pagination",
                  pattern: "Product/Page{productPage}",
                    defaults: new { Controller = "Product", action = "Product" });


                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Product}/{action=Home}/{id?}");
            });
        }
    }
}
