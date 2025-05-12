using ITI.Shipping.APIs.Filters;
using ITI.Shipping.Core.Application.Abstraction;
using ITI.Shipping.Core.Application.Abstraction.Auth;
using ITI.Shipping.Core.Application.Abstraction.chat;
using ITI.Shipping.Core.Application.Abstraction.User;
using ITI.Shipping.Core.Application.Mapping;
using ITI.Shipping.Core.Application.Services;
using ITI.Shipping.Core.Application.Services.AuthServices;
using ITI.Shipping.Core.Application.Services.ChatbotService;
using ITI.Shipping.Core.Application.Services.UserServices;
using ITI.Shipping.Core.Domin.Entities;
using ITI.Shipping.Core.Domin.UnitOfWork.Contract;
using ITI.Shipping.Infrastructure.Presistence.Data;
using ITI.Shipping.Infrastructure.Presistence.UnitOfWork;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
namespace ITI.Shipping.APIs;
public static class ServiceContainer
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,IConfiguration Configuration)
    {

        string txt = "";
        services.AddControllers();
        services.AddOpenApi();
        services.AddDbContext<ApplicationContext>(options =>
        {
            options.UseLazyLoadingProxies().UseSqlServer(Configuration.GetConnectionString("con"));
        });

        services.AddCors(options =>
        {
            options.AddPolicy(txt,
            builder =>
            {
                builder.AllowAnyOrigin();
                builder.AllowAnyMethod();
                builder.AllowAnyHeader();
            });
        });

        services.AuthenticationConfigurations();
        services.AddSwaggerServices();

        services.AddScoped(typeof(IUnitOfWork),typeof(UnitOfWork));
        services.AddAutoMapper(typeof(MappingProfile));
        //services.AddScoped(typeof(IServiceManager),typeof(ServiceManager));

        //services.AddScoped<IRoleService,RoleService>();
        //services.AddScoped<IUserService,UsersService>();
        //services.AddScoped<IChatbotService, ChatbotService>();
        //services.AddScoped<IShipmentService, ShipmentService>();
        //services.AddScoped<IAIService, AIService>();
        //services.AddHttpClient<IOpenAIService, OpenAIService>();

        // Application Services
    services.AddScoped<IServiceManager, ServiceManager>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<IUserService, UsersService>();
        services.AddScoped<IShipmentService, ShipmentService>();

        // AI & Chatbot Services (Important Order!)
        services.AddHttpClient<IOpenAIService, OpenAIService>();
        services.AddScoped<IAIService, AIService>();
        services.AddScoped<IChatbotService, ChatbotService>();

        return services;
    }
    private static IServiceCollection AuthenticationConfigurations(this IServiceCollection services)
    {
        services.AddSingleton<IJWTProvider,JWTProvider>();
        services.AddIdentity<ApplicationUser,ApplicationRole>()
            .AddEntityFrameworkStores<ApplicationContext>()
            .AddDefaultTokenProviders();
        services.AddTransient<IAuthorizationHandler,PermissionAuthorizationHandler>();
        services.AddTransient<IAuthorizationPolicyProvider,PermissionAuthorizationPolicyProvider>();
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.SaveToken = true;
            options.TokenValidationParameters = new()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = "ShippingProject",
                ValidAudience = "ShippingProject users",
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("lEQCxTrFYTOsyFtbtoWwPdDJ3066bWiP"))
            };
        });
        services.Configure<IdentityOptions>(options =>
        {
            options.Password.RequiredLength = 8;
            //options.SignIn.RequireConfirmedEmail = true;
            options.User.RequireUniqueEmail = true;
        });
        return services;
    }
    // for testing
    private static IServiceCollection AddSwaggerServices(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme,new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Description = "Enter the Bearer authorization : 'Bearer Generate-JWT-Token'",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = JwtBearerDefaults.AuthenticationScheme
                        }
                    },
                    new string[] { }
                }
            });
        });
        return services;
    }
}