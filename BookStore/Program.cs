using Bussiness_Layer.InterfaceBL;
using Bussiness_Layer.ServiceBL;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Repository_Layer.Context;
using Repository_Layer.Entity;
using Repository_Layer.Hashing;
using Repository_Layer.InterfaceRL;
using Repository_Layer.ServiceRL;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Book Store",
        Version = "v1"
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. Enter 'Bearer' [space] and then your token in the text input below.Example: \"Bearer 1safsfsdfdfd\"",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

builder.Services.AddDbContext<BookStoreContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection")));

builder.Services.AddScoped<IUserInterfaceRL, UserServiceRL>();
builder.Services.AddScoped<IUserInterfaceBL, UserServiceBL>();
builder.Services.AddScoped<IBookInterfaceRL, BookServiceRL>();
builder.Services.AddScoped<IBookInterfaceBL, BookServiceBL>();
builder.Services.AddScoped<HashingPassword>();


// Add authentication services
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Add a middleware to populate user roles based on the JWT token
app.Use(async (context, next) =>
{
    var user = context.User;
    if (user.Identity.IsAuthenticated)
    {
        if (user.Identity is ClaimsIdentity identity)
        {
            // Find the 'roles' claim
            var roleClaims = identity.FindAll(ClaimTypes.Role);
            if (roleClaims != null && roleClaims.Any())
            {
                // Add each role as a new claim of type 'http://schemas.microsoft.com/ws/2008/06/identity/claims/role'
                foreach (var roleClaim in roleClaims)
                {
                    identity.AddClaim(new Claim(ClaimTypes.Role, roleClaim.Value));
                }
            }
        }
    }
    await next.Invoke();
});

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
