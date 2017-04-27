using System;
using Microsoft.AspNetCore.Builder;
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
using System.Collections.Generic;
using AutoMapper;
using CodeChecker.Models.AssignmentViewModels;
using CodeChecker.Models.ServiceViewModels;
using CodeChecker.Services.CodeSubmit;
using CodeChecker.Tasks;

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

            services.Configure<AppSettings>(appSettings =>
            {
                appSettings.Microservice = new Microservice
                {
                    // Untyped Syntax - Configuration[""]
                    Uri = Configuration.GetSection("Microservice")["Uri"]
                };
            });

            Repositories(services);
            Services(services);
            Policies(services);
            Tasks(services);

            services.AddMvc().AddJsonOptions(x => x.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

            //var config = new AutoMapper.MapperConfiguration(cfg =>
            //{
            //    cfg.CreateMap<ApplicationUser, AdminPanelUserViewModel>();
            //    cfg.CreateMap<ApplicationUser, TopUserViewModel>().ReverseMap();
            //    cfg.CreateMap<List<ApplicationUser>, List<TopUserViewModel>>();
            //    cfg.CreateMap<ApplicationUser, UserIdViewModel>().ReverseMap();
            //    cfg.CreateMap<ApplicationUser, UserViewModel>().ReverseMap();
            //    cfg.CreateMap<Asset, AssetProfileViewModel>();
            //    cfg.CreateMap<Contest, CreateContestViewModel>().ReverseMap();
            //    cfg.CreateMap<Contest, ViewContestViewModel>();
            //    cfg.CreateMap<Contest, ContestViewModel>();
            //    cfg.CreateMap<ContestCreator, ContestCreatorViewModel>();
            //});

            //var mapper = config.CreateMapper();
            //services.AddSingleton(mapper);
            
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

            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<ApplicationUser, AdminPanelUserViewModel>();
                cfg.CreateMap<ApplicationUser, TopUserViewModel>().ReverseMap();
                cfg.CreateMap<List<ApplicationUser>, List<TopUserViewModel>>().ReverseMap();
                cfg.CreateMap<ApplicationUser, UserIdViewModel>().ReverseMap();
                cfg.CreateMap<ApplicationUser, UserViewModel>().ReverseMap();
                cfg.CreateMap<Asset, AssetProfileViewModel>();
                cfg.CreateMap<Contest, CreateContestViewModel>().ReverseMap();
                cfg.CreateMap<Contest, ViewContestViewModel>();
                cfg.CreateMap<Contest, ContestViewModel>();
                cfg.CreateMap<Contest, ContestWithAssignmentViewModel>();
                cfg.CreateMap<ContestCreator, ContestCreatorViewModel>();
                cfg.CreateMap<ContestCreator, ContestContributorViewModel>();
                cfg.CreateMap<Assignment, ShortAssignmentViewModel>();
                cfg.CreateMap<Assignment, AssignmentViewModel>();

            });

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
            services.AddScoped<ContestParticipantRepository>();
            services.AddScoped<AssignmentRepository>();
            services.AddScoped<SubmissionRepository>();
        }

        private void Services(IServiceCollection services)
        {
            services.AddTransient<FileUploadService>();
            services.AddTransient<CodeSubmitService>();

            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();
        }

        private void Tasks(IServiceCollection services)
        {
            services.AddTransient<CodeTestTask>();
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