using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using WebServer.DAL.Context;
using WebServer.DAL.Repository;
using WebServer.DAL.Repository.Classes;
using WebServer.DAL.Repository.Interfaces;
using WebServer.Services;
using WebServer.Services.Interfaces;
using WebServer.Services.Services;

namespace WebServer
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
            services.AddCors();
            services.AddControllers();
            services.AddMvc().AddMvcOptions(opts => opts.EnableEndpointRouting = false);
            /*services.AddCors(opts =>
            {
                opts.AddPolicy("CorsPolicy", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });*/

            services.AddDbContext<CommonContext>(options => options.UseSqlServer(Configuration["ConnectionString:Connect"]));

            services.AddTransient<IGameFinalScoreRepository, GameFinalScoreRepository>();

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUserService, UserService>();

            services.AddTransient<IGameRepository, GameRepository>();
            services.AddTransient<IGameService, GameService>();

            services.AddTransient<IOrderRepository, OrdersRepository>();
            services.AddTransient<IOrderService, OrderService>();

            services.AddTransient<IGameMarkRepository, GameMarkRepository>();
            services.AddTransient<IGameMarkService, GameMarkService>();

            services.AddTransient<IFeedbackRepository, FeedbackRepository>();
            services.AddTransient<IFeedbackService, FeedbackService>();

            services.AddTransient<IRefreshTokensRepository, RefreshTokensRepository>();
            services.AddTransient<IRefreshTokensService, RefreshTokenService>();

            services.AddTransient<IBannedUsersRepository, BannedUsersRepository>();
            services.AddTransient<IBannedUsersService, BannedUsersService>();

            services.AddTransient<IOffersRepository, OffersRepository>();
            services.AddTransient<IOffersService, OffersService>();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("MyPolicy", policy => policy.Requirements.Add(new AccountRequirement()));
            });
            services.AddTransient<IAuthorizationHandler, AuthFilter>();
            services.AddHttpContextAccessor();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = AuthOptions.ISSUER,
                    ValidateAudience = true,
                    ValidAudience = AuthOptions.AUDIENCE,
                    ValidateLifetime = true,
                    IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                    ValidateIssuerSigningKey = true
                };
                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            context.Response.Headers.Add("Token-Expired", "true");
                        }
                        return Task.CompletedTask;
                    }
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            //app.UseCors("CorsPolicy");
            app.UseCors(opts => opts
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod()
                //.AllowCredentials()
                );

            app.UseRouting();

            app.UseAuthorization();

            app.UseAuthentication();

            app.UseStatusCodePages();

            app.UseDeveloperExceptionPage();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}