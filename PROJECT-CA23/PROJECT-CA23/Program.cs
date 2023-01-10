
using Microsoft.EntityFrameworkCore;
using PROJECT_CA23.Database;
using PROJECT_CA23.Services.IServices;
using PROJECT_CA23.Services;
using PROJECT_CA23.Repositories;
using PROJECT_CA23.Repositories.IRepositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text.Json.Serialization;
using PROJECT_CA23.Services.Adapters.IAdapters;
using PROJECT_CA23.Services.Adapters;

namespace PROJECT_CA23
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<CA23Context>(options =>
            {
                options.UseSqlite(builder.Configuration.GetConnectionString("ProjectCA23ConnectionString"));
            });

            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IJwtService, JwtService>();
                        
            builder.Services.AddScoped<IAddressAdapter, AddressAdapter>();

            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IAddressRepository, AddressRepository>();

            builder.Services.AddHttpContextAccessor();

            var mySecretKey = builder.Configuration["MyApiSetting:MySecretKey"];

            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(mySecretKey)),
                    ValidateIssuer = false,
                    ValidateAudience = false,

                };
            });

            builder.Services.AddCors(p => p.AddPolicy("corsforprojectca23", builder =>
            {
                builder.WithOrigins("*")
                .AllowAnyMethod()
                .AllowAnyHeader();
            }));

            builder.Services.AddControllers()
                .AddJsonOptions(option => option.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles); // IGNORES CYCLES

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(option =>
            {
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                option.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
                var securityScheme = new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header is using Bearer scheme. \r\n\r\n" +
                        "Enter token. \r\n\r\n" +
                        "Example: \"d5f41g85d1f52a\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                };
                option.AddSecurityDefinition("Bearer", securityScheme);
                option.AddSecurityRequirement(new OpenApiSecurityRequirement { { securityScheme, new[] { "Bearer" } } });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Cors naudojama pries CSRF/XSRF
            app.UseCors("corsforprojectca23");

            app.UseHttpsRedirection();

            app.UseAuthentication();    // ORDER MATTERS
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}