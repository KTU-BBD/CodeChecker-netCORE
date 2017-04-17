using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using CodeChecker.Data;
using CodeChecker.Models;
using CodeChecker.Models.AccountViewModels;
using CodeChecker.Models.AssetViewModels;
using CodeChecker.Models.Models;
using CodeChecker.Services;
using CodeChecker.Models.Models.DatabaseSeeders;
<<<<<<< HEAD
using CodeChecker.Models.Repositories;
using CodeChecker.Models.UserViewModels;
=======
using CodeChecker.Services.FileUpload;
>>>>>>> 60e89ec293aada283f5eca63bc3e7c25cbedea92

namespace CodeChecker
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see https://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets<Startup>();
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
            services.AddTransient<BaseSeeder>();
<<<<<<< HEAD
            services.AddScoped<ApplicationUserRepository>();
            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ApplicationUser, AdminPanelUserViewModel>();
                cfg.CreateMap<ApplicationUser, TopUserViewModel>().ReverseMap();
=======

            services.AddTransient<FileUploadService>();

            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ApplicationUser, ApplicationUserViewModel>();
                cfg.CreateMap<Asset, AssetProfileViewModel>();
>>>>>>> 60e89ec293aada283f5eca63bc3e7c25cbedea92
            });

            var mapper = config.CreateMapper();

            services.AddSingleton(mapper);
            services.AddMvc();

            services.AddAuthorization(options =>
            {
                options.AddPolicy(
                    "CanUseAdminPanel",
                    policy => policy.RequireRole("Administrator", "Moderator", "Contributor")
                );
            });

            // Add application services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory,
            BaseSeeder seeder)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseIdentity();

            // Add external authentication middleware below. To configure them please see https://go.microsoft.com/fwlink/?LinkID=532715

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            seeder.EnsureSeedData().Wait();
        }
    }
}