using Bussiness_Layer.InterfaceBL;
using Bussiness_Layer.ServiceBL;
using Microsoft.EntityFrameworkCore;
using Repository_Layer.Context;
using Repository_Layer.Hashing;
using Repository_Layer.InterfaceRL;
using Repository_Layer.ServiceRL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<BookStoreContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection")));

builder.Services.AddScoped<IUserInterfaceRL, UserServiceRL>();
builder.Services.AddScoped<IUserInterfaceBL, UserServiceBL>();
builder.Services.AddScoped<HashingPassword>();

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
