using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using WebServer.Controllers;
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

            services.AddMvcCore().AddAuthorization();

            services.AddMvc();//.AddMvcOptions(opts => opts.EnableEndpointRouting = false);

            services.AddRouting();
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

            services.AddTransient<IMoneyKeysRepository, MoneyKeysRepository>();
            services.AddTransient<IMoneyKeysService, MoneyKeysService>();

            services.AddTransient<IGameScreenshotsRepository, GameScreenshotsRepository>();
            services.AddTransient<IGameScreenshotsService, GameScreenshotsService>();

            services.AddTransient<IMessagesRepository, MessagesRepository>();
            services.AddTransient<IMessagesService, MessagesService>();

            services.AddTransient<IChatParticipantsRepository, ChatParticipantsRepository>();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("MyPolicy", policy => 
                {
                    policy.Requirements.Add(new AccountRequirement());
                    policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
                    //policy.RequireRole("User");
                });
            });
            services.AddScoped<IAuthorizationHandler, AuthFilter>();
            //services.AddHttpContextAccessor();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddSignalR();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = AuthOptions.ISSUER,
                    ValidateAudience = true,
                    ValidAudience = AuthOptions.AUDIENCE,
                    ValidateLifetime = false,
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
                 .WithOrigins(
                 "http://localhost:3000",
                 "http://localhost:3001",
                 "http://localhost:3002")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .WithExposedHeaders("AccessToken", "RefreshToken")
                .AllowCredentials()
                );

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseStatusCodePages();

            app.UseDeveloperExceptionPage();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<ChatController>("/chat");
            });
        }
    }
}