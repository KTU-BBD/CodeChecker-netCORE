using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using CodeChecker.Data;
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
using System.Linq;
using System.Net.Http;
using AutoMapper;
using CodeChecker.Middleware;
using CodeChecker.Models.ArticleViewModel;
using CodeChecker.Models.AssignmentViewModels;
using CodeChecker.Models.AssignmentViewModels.InputOutputViewModels;
using CodeChecker.Models.ContactViewModel;
using CodeChecker.Models.Models.Enums;
using CodeChecker.Models.ServiceViewModels;
using CodeChecker.Models.SubmissionViewModels;
using CodeChecker.Services.CodeSubmit;
using CodeChecker.Services.EmailSending;
using CodeChecker.Tasks;
using Hangfire;
using Microsoft.AspNetCore.Http;
using CodeChecker.Models.FAQViewModel;

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

            services.AddHangfire(config =>
                config.UseSqlServerStorage(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
            services.AddTransient<BaseSeeder>();

            services.Configure<IdentityOptions>(options =>
            {
                options.SecurityStampValidationInterval = TimeSpan.FromSeconds(0);
            });

            services.Configure<AppSettings>(appSettings =>
            {
                appSettings.Microservice = new Microservice
                {
                    Uri = Configuration.GetSection("Microservice")["Uri"]
                };

                appSettings.MailSettings = new MailSettings
                {
                    Address = Configuration.GetSection("Email")["Address"],
                    UserName = Configuration.GetSection("Email")["UserName"],
                    Password = Configuration.GetSection("Email")["Password"],
                    SenderName = Configuration.GetSection("Email")["SenderName"],
                    SenderMail = Configuration.GetSection("Email")["SenderMail"],
                    Port = Convert.ToInt32(Configuration.GetSection("Email")["Port"])
                };
            });

            Repositories(services);
            Tasks(services);
            Services(services);
            Policies(services);

            services.AddMvc()
                .AddJsonOptions(x => x.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

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

            var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>();
            contextOptions.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            var dbContext = new ApplicationDbContext(contextOptions.Options);
            dbContext.Database.Migrate();

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
            if (env.IsDevelopment() || env.IsEnvironment("Testing"))
            {
                app.UseMiddleware<AuthenticationMiddleware>();
            }

            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<List<Article>, List<ArticleListViewModel>>().ReverseMap();
                cfg.CreateMap<Article, ArticleListViewModel>().ReverseMap();
                cfg.CreateMap<Article, EditArticlePostViewModel>().ReverseMap();
                cfg.CreateMap<Article, ArticleViewModel>().ReverseMap();
                cfg.CreateMap<ApplicationUser, AdminPanelUserViewModel>()
                    .ForMember(c => c.LockoutEnabled, o => o.MapFrom(src => src.LockoutEnd != null))
                    .ReverseMap();
                cfg.CreateMap<ApplicationUser, TopUserViewModel>().ReverseMap();
                cfg.CreateMap<List<ApplicationUser>, List<TopUserViewModel>>().ReverseMap();
                cfg.CreateMap<ApplicationUser, UserIdViewModel>().ReverseMap();
                cfg.CreateMap<ApplicationUser, UserViewModel>().ReverseMap();
                cfg.CreateMap<ApplicationUser, ProfileViewViewModel>()
                    .ForMember(c => c.TotalSubmissions, o => o.MapFrom(src => src.SubmissionGroups.Count))
                    .ForMember(c => c.SuccesfullSubmissions, o => o.MapFrom(src => src.SubmissionGroups.Count(c => c.Verdict == SubmissionVerdict.Success)))
                    .ForMember(c => c.UnsuccesfullSubmissions, o => o.MapFrom(src => src.SubmissionGroups.Count(c => c.Verdict != SubmissionVerdict.Success)))
                    .ReverseMap();
                cfg.CreateMap<UserStatistic, UserStatisticViewModel>().ReverseMap();
                cfg.CreateMap<ApplicationUser, ProfileUpdateViewModel>().ReverseMap();
                cfg.CreateMap<ApplicationUser, UserProfileViewModel>().ReverseMap();
                cfg.CreateMap<ApplicationUser, PersonalProfileViewModel>().ReverseMap();
                cfg.CreateMap<ApplicationUser, PersonalProfileUpdateViewModel>().ReverseMap();
                cfg.CreateMap<Asset, AssetProfileViewModel>().ReverseMap();
                cfg.CreateMap<Contest, CreateContestViewModel>().ReverseMap();
                cfg.CreateMap<Contest, ViewContestViewModel>().ReverseMap();
                cfg.CreateMap<Contest, ContestViewModel>()
                    .ForMember(c => c.IsPublic, o => o.MapFrom(src => string.IsNullOrEmpty(src.Password)))
                    .ForMember(c => c.IsStarted, o => o.MapFrom(src => src.StartAt < DateTime.Now))
                    .ForMember(c => c.Length, o => o.MapFrom(src => src.EndAt.Subtract(src.StartAt).TotalHours))
                    .ReverseMap();
                cfg.CreateMap<Contest, ContestWithAssignmentViewModel>();
                cfg.CreateMap<Assignment, ShortAssignmentViewModel>().ReverseMap();
                cfg.CreateMap<Contest, EditContestGetViewModel>().ReverseMap();
                cfg.CreateMap<Contest, EditContestPostViewModel>().ReverseMap();
                cfg.CreateMap<Assignment, EditAssignmentGetViewModel>().ReverseMap();
                cfg.CreateMap<Assignment, EditAssignmentGetViewModel>().ReverseMap();
                cfg.CreateMap<Assignment, EditAssignmentPostViewModel>().ReverseMap();
                cfg.CreateMap<Assignment, ShortAssignmentViewModel>().ReverseMap();
                cfg.CreateMap<Assignment, AssignmentViewModel>().ReverseMap();
                cfg.CreateMap<Contact, ContactUpdateViewModel>().ReverseMap();
                cfg.CreateMap<Input, InputViewModel>().ReverseMap();
                cfg.CreateMap<Output, OutputViewModel>().ReverseMap();
                cfg.CreateMap<SubmissionGroup, LastSubmissionViewModel>().ReverseMap();
                cfg.CreateMap<List<SubmissionGrouppingList>, List<SubmissionGrouppingListViewModel>>().ReverseMap();
                cfg.CreateMap<SubmissionGroup, SubmissionViewModel>().ReverseMap();
                cfg.CreateMap<List<SubmissionGroup>, List<SubmissionViewModel>>().ReverseMap();
                cfg.CreateMap<Faq, EditFaqViewModel>().ReverseMap();
            });

            app.UseStaticFiles();
            app.UseIdentity();

            // Add external authentication middleware below. To configure them please see https://go.microsoft.com/fwlink/?LinkID=532715

            app.UseHangfireServer();


            RecurringJob.AddOrUpdate(() => seeder.SendStatistic(),
                Cron.Daily
            );

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
            services.AddScoped<InputRepository>();
            services.AddScoped<OutputRepository>();
            services.AddScoped<ArticleRepository>();
            services.AddScoped<FAQRepository>();
            services.AddScoped<SubmissionGroupRepository>();
            services.AddScoped<ContactRepository>();
        }

        private void Services(IServiceCollection services)
        {
            services.AddTransient<FileUploadService>();
            services.AddTransient<CodeSubmitService>();
            services.AddTransient<EmailSenderService>();
            services.AddTransient<ViewRenderService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<HttpClient>();

            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();
        }

        private void Tasks(IServiceCollection services)
        {
            services.AddSingleton<CodeTestTask>();
            services.AddSingleton<SendEmailTask>();
        }

        private void Policies(IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy(
                    "CanUseAdminPanel",
                    policy => policy.RequireRole("Administrator", "Moderator", "Contributor")
                );
                options.AddPolicy(
                    "CanEditContests",
                    policy => policy.RequireRole("Administrator", "Moderator")
                );
                options.AddPolicy(
                    "CanEditArticles",
                    policy => policy.RequireRole("Administrator", "Moderator")
                );
            });
        }
    }
}