using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SocialNetwork.Application.Contracts;
using SocialNetwork.Application.Mapping;
using SocialNetwork.Application.Services;
using SocialNetwork.Security.Authorization;
using SocialNetwork.Security.Settings;
using SocialNetworks.Repository.Contracts;
using SocialNetworks.Repository.Repository;
using SocialNetworks.Repository.Repository.LogRepository;
using System;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(
                typeof(UserMappingProfile),
                typeof(PostMappingProfile), 
                typeof(ChatMappingProfile));
        }
        public static void ConfigureServices(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddScoped<IRepositoryManager, RepositoryManager>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<IBlobService, BlobService>();
            services.AddScoped<IRateService, RateService>();
            services.AddScoped<IChatService, ChatService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IChatWorkerService, ChatWorkerService>();

            services.AddSingleton<IJwtUtils, JwtUtils>();


            services.AddSingleton<IWorkerService, WorkerService>(x => new WorkerService("WorkerQueue", x));
            services.AddScoped<ILogRepositoryManager, LogRepositoryManager>();
            services.AddScoped<IMessageLogRepository, MessageLogRepository>();

            services.AddScoped(_ => {
                return new BlobServiceClient(Configuration.GetConnectionString("AzureBlobStorage"));
            });
        }
        public static void ConfigureDatabase(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddDbContext<RepositoryContext>(opts =>
                    opts.UseSqlServer(Configuration.GetConnectionString("sqlConnection"), x => x.MigrationsAssembly("SocialNetwork")));
        }

        public static void ConfigureJWTAppSettings(this IServiceCollection services, IConfiguration Configuration)
            => services.Configure<AppSettings>(Configuration.GetSection("JWT"));

        public static void ConfigureAuthorization(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetSection("JWT").GetSection("Secret").Value)),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            var accessToken = context.Request.Query["access_token"];

                            var path = context.HttpContext.Request.Path;
                            if (!string.IsNullOrEmpty(accessToken) &&
                                (path.StartsWithSegments("/hubs/changeRate") || path.StartsWithSegments("/api/Blob/")))
                            {
                                context.Token = accessToken;
                            }
                            return Task.CompletedTask;
                        }
                    };
                }

            );
        }
    }
}
