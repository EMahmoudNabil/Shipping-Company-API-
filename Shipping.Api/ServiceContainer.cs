using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Shipping.Api.Core.Abstraction;
using Shipping.Api.Core.Domain.Models;
using Shipping.Api.Infrastructure.Data;
using Shipping.Api.Infrastructure.Repositories;
using Shipping.Api.Services;
using Shipping.Api.Services.Filters;
using System.Text;
namespace Shipping.Api;
public static class ServiceContainer
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,IConfiguration Configuration)
    {
        string txt = "";
        services.AddControllers();
        services.AddOpenApi();
        services.AddDbContext<ShippingContext>(options =>
        {
            options.UseLazyLoadingProxies().UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
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
        services.AddAutoMapper(typeof(MapperConfig));
        services.AddScoped(typeof(IGenericRepository<,>),typeof(GenericRepository<,>));
        services.AddScoped<IUnitOfWork,UnitOfWork>();
        services.AddScoped<ICourierReportsService,CourierReportsService>();
        services.AddScoped<IRegionService,RegionService>();
        services.AddScoped<IUserService,UsersService>();
        services.AddScoped<ISpecialCityCostService,SpecialCityCostService>();
        services.AddScoped<ISpecialCourierRegionService,SpecialCourierRegionService>();
        services.AddScoped<ICityService,CityService>();
        services.AddScoped<ICityRepository,CityRepository>();
        services.AddScoped<IOrderRepository,OrderRepository>();
        services.AddScoped<IOrderService,OrderServices>();
        services.AddScoped<IRoleService,RoleService>();
        services.AddScoped<IProductService, ProductService>();
        return services;
    }

    private static IServiceCollection AuthenticationConfigurations(this IServiceCollection services)
    {
        services.AddSingleton<IJWTProvider,JWTProvider>();
        services.AddIdentity<ApplicationUser,ApplicationRole>()
            .AddEntityFrameworkStores<ShippingContext>()
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
