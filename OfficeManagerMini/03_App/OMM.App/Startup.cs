using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OMM.Data;
using OMM.Domain;
using OMM.App.Extensions;
using OMM.Data.Seeding;
using OMM.Services.AutoMapper;
using OMM.App.Models;
using System.Globalization;
using Microsoft.AspNetCore.Authorization;
using OMM.App.Infrastructure.CustomAuthorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using OMM.Services.Data;
using OMM.Services.Data.DTOs.Employees;
using CloudinaryDotNet;
using OMM.Services.SendGrid;
using OMM.Services.YWeather;
using OMM.App.Infrastructure.ViewComponents.Models.Departments;

namespace OMM.App
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            AutoMapperConfig.RegisterMappings(
                typeof(ErrorViewModel).Assembly,
                typeof(EmployeeLoginDto).Assembly,
                typeof(DepartmentViewComponentViewModel).Assembly);

            services.AddDbContext<OmmDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection"))
                    .UseLazyLoadingProxies());

            services.AddIdentity<Employee, IdentityRole>(options =>
                {
                    options.SignIn.RequireConfirmedEmail = false;
                    options.Password.RequireDigit = false;
                    options.Password.RequiredUniqueChars = 0;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequiredLength = 5;

                    options.User.RequireUniqueEmail = true;
                })
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<OmmDbContext>();

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Employees/Login";
                options.AccessDeniedPath = "/Employees/AccessDenied";
            });

            services.AddMvc(options =>
            {
                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                options.Filters.Add(new AuthorizeFilter(policy));

                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            Account cloudinaryCredentials = new Account(
                this.Configuration["Cloudinary:CloudName"],
                this.Configuration["Cloudinary:ApiKey"],
                this.Configuration["Cloudinary:ApiSecret"]);

            Cloudinary cloudinaryUtility = new Cloudinary(cloudinaryCredentials);

            services.AddSingleton(cloudinaryUtility);

            services.AddSingleton<IAuthorizationPolicyProvider, MinimumAccessLevelPolicyProvider>();
            services.AddSingleton<IAuthorizationHandler, MinimumAccessLevelAuthorizationHandler>();

            //Database seeding services
            services.AddScoped<AssetTypeSeeder>();
            services.AddScoped<DepartmentSeeder>();
            services.AddScoped<LeavingReasonSeeder>();
            services.AddScoped<ProjectPositionSeeder>();
            services.AddScoped<StatusSeeder>();
            services.AddScoped<RolesSeeder>();

            //OMM Data services
            services.AddTransient<IEmployeesService, EmployeesService>();
            services.AddTransient<IDepartmentsService, DepartmentsService>();
            services.AddTransient<IAssetsService, AssetsService>();
            services.AddTransient<IAssetTypesService, AssetTypesService>();
            services.AddTransient<ILeavingReasonsService, LeavingReasonsService>();
            services.AddTransient<IStatusesService, StatusesService>();

            //Third parties services
            services.AddTransient<ICloudinaryService, CloudinaryService>();
            services.AddTransient<ISendGrid, SendGird>();
            services.AddTransient<IYWeatherService, YWeatherService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
            CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;

            app.UseDataBaseSeeding();
            app.UseAdminSeeding();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "areas",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
