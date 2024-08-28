using BusinessObjects.Configurations;
using BusinessObjects.Models;
using Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

using System.Text;
using System.Text.Json.Serialization;
using Microsoft.OData.ModelBuilder;
using Microsoft.AspNetCore.OData;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        //Configuring JWT Authentication in Swagger
        var securityScheme = new OpenApiSecurityScheme()
        {
            Description = "JWT Authorization header using the Bearer scheme.",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.Http,
            Scheme = "Bearer",
            BearerFormat = "JWT" // Optional
        };

        var securityRequirement = new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string[] {}
            }
        };
        builder.Services.AddCors();
        ODataConventionModelBuilder modelBuilder = new ODataConventionModelBuilder();
        modelBuilder.EntitySet<Room>("Rooms");
        modelBuilder.EntitySet<Booking>("Booking");
        builder.Services.AddControllers().AddOData(opt =>
        {
            opt.Filter().Select().OrderBy().Count().Expand().SetMaxTop(100);
            opt.AddRouteComponents("odata", modelBuilder.GetEdmModel());
        });
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowSpecificOrigin",
                builder =>
                {
                    builder.WithOrigins("https://localhost:7204", "https://localhost:7109")
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });
        });

        builder.Services.AddSwaggerGen(option =>
        {
            option.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, securityScheme);
            option.AddSecurityRequirement(securityRequirement);
        });
        // Add services to the container.

        builder.Services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            options.JsonSerializerOptions.WriteIndented = true;
        });
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();

        // Configure Connection Strings
        var connectionStrings = new ConnectionString();
        builder.Configuration.GetSection("ConnectionStrings").Bind(connectionStrings);
        builder.Services.AddSingleton(connectionStrings);

        // Configure JWT settings
        var jwtSettings = new JwtSettings();
        builder.Configuration.GetSection("JwtSettings").Bind(jwtSettings);
        builder.Services.AddSingleton(jwtSettings);

        // Register DbContext
        builder.Services.AddDbContext<HotelContext>(options =>
            options.UseSqlServer(connectionStrings.DefaultConnection));

        //Authentication
        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(s: jwtSettings.Secret)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

        //Authorization
        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("AdminPolicy", policy => policy.RequireRole("0"));
            options.AddPolicy("EmployeePolicy", policy => policy.RequireRole("1"));
        });

        //Register services
        builder.Services.RegisterRepositories();
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseCors(
            builder => builder.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
        );
        app.UseRouting();
        app.UseHttpsRedirection();
        app.Use(async (context, next) =>
        {
            if (context.Request.Method == "OPTIONS")
            {
                context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                context.Response.Headers.Add("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS");
                context.Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type, Authorization");
                context.Response.StatusCode = 204;
                return;
            }
            await next();
        });
        app.UseAuthentication();

        app.MapControllers();

        app.Run();
    }
}