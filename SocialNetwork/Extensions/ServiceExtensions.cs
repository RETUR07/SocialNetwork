﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SocialNetwork.Application.Contracts;
using SocialNetwork.Application.Mapping;
using SocialNetwork.Application.Services;
using SocialNetwork.Security.Authorization;
using SocialNetwork.Security.Settings;
using SocialNetworks.Repository.Contracts;
using SocialNetworks.Repository.Repository;
using System;

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
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryManager, RepositoryManager>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<IBlobService, BlobService>();
            services.AddScoped<IRateService, RateService>();
            services.AddScoped<IChatService, ChatService>();
            services.AddScoped<IJwtUtils, JwtUtils>();
        }
        public static void ConfigureDatabase(this IServiceCollection services)
        {
            services.AddDbContext<RepositoryContext>(opts =>
                    opts.UseSqlServer(Environment.GetEnvironmentVariable("sqlConnection"), x => x.MigrationsAssembly("SocialNetwork")));
        }

        public static void ConfigureJWT(this IServiceCollection services, IConfiguration Configuration)
            => services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
    }
}
