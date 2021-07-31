namespace BillsManagement.Core
{
    using AutoMapper;
    using BillsManagement.DAL.Models;
    using BillsManagement.Repository;
    using BillsManagement.Repository.Repositories;
    using BillsManagement.Repository.RepositoryContracts;
    using BillsManagement.Services;
    using BillsManagement.Services.ServiceContracts;
    using BillsManagement.Services.Services.ChargesService;
    using BillsManagement.Services.Services.UserService;
    using BillsManagement.Utility.Options;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.IdentityModel.Tokens;
    using System;
    using System.Text;

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
            // Auto Mapper Configurations
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapping());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            // Inject AppSetting
            services.Configure<ApplicationSettings>(Configuration.GetSection("ApplicationSettings"));
            //services.Configure<SecuritySettings>(Configuration.GetSection("SecuritySettings"));

            services
                .AddMvc()
                .AddMvcOptions(mvc => mvc.EnableEndpointRouting = false)
                .SetCompatibilityVersion(CompatibilityVersion.Latest);

            // DbContext configuration
            services.AddDbContext<BillsManagementContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("IdentityConnection")));

            // Repository configurations
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IChargesRepository, ChargesRepository>();
            services.AddScoped<IAuthorizationRepository, AuthorizationRepository>();

            // Service configurations
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IChargesService, ChargesService>();

            // Cors
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });

            // JWT Authentication

            services.ConfigureWritable<SecuritySettings>(Configuration.GetSection("SecuritySettings"));

            var key = Encoding.UTF8.GetBytes(Configuration["SecuritySettings:JWT_Secret"].ToString());

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = false;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.Use(async (ctx, next) =>
            {
                await next();
                if (ctx.Response.StatusCode == 204)
                {
                    ctx.Response.ContentLength = 0;
                }
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseCors("CorsPolicy");

            app.UseAuthentication();

            app.UseMvc();
        }
    }
}
