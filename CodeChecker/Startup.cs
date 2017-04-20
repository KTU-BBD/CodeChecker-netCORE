﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using CodeChecker.Data;
using CodeChecker.Models.AccountViewModels;
using CodeChecker.Models.AssetViewModels;
using CodeChecker.Models.ContestViewModels;
using CodeChecker.Models.Models;
using CodeChecker.Services;
using CodeChecker.Models.Models.DatabaseSeeders;
using CodeChecker.Services.FileUpload;
using CodeChecker.Models.Repositories;
using CodeChecker.Models.UserViewModels;
using Newtonsoft.Json;

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

            Repositories(services);
            Services(services);
            Mapper(services);
            Policies(services);

            services.AddMvc().AddJsonOptions(x => x.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

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

        private void Repositories(IServiceCollection services)
        {
            services.AddScoped<ApplicationUserRepository>();
            services.AddScoped<AssetRepository>();
            services.AddScoped<ContestRepository>();
            services.AddScoped<ContestCreatorRepository>();
            services.AddScoped<ContestParticipantRepository>();
        }

        private void Services(IServiceCollection services)
        {
            services.AddTransient<FileUploadService>();

            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();
        }

        private void Mapper(IServiceCollection services)
        {
            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ApplicationUser, AdminPanelUserViewModel>();
                cfg.CreateMap<ApplicationUser, TopUserViewModel>().ReverseMap();
                cfg.CreateMap<ApplicationUser, UserIdViewModel>().ReverseMap();
                cfg.CreateMap<ApplicationUser, UserViewModel>().ReverseMap();
                cfg.CreateMap<Asset, AssetProfileViewModel>();
                cfg.CreateMap<Contest, CreateContestViewModel>().ReverseMap();
                cfg.CreateMap<Contest, ViewContestViewModel>();
                cfg.CreateMap<Contest, ContestViewModel>();
                cfg.CreateMap<ContestCreator, ContestCreatorViewModel>();
            });

            var mapper = config.CreateMapper();
            services.AddSingleton(mapper);
        }

        private void Policies(IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy(
                    "CanUseAdminPanel",
                    policy => policy.RequireRole("Administrator", "Moderator", "Contributor")
                );
            });
        }
    }
}