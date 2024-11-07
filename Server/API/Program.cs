using BankTransactionService.API.Services;
using BankTransactionService.Application.Interfaces;
using BankTransactionService.Domain.Repositories;
using BankTransactionService.Infrastructure.Configuration;
using BankTransactionService.Infrastructure.Messaging;
using BankTransactionService.Infrastructure.Repositories;
using Microsoft.Extensions.Options;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS Settings
/*builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp",
        policy =>
        {
            policy.WithOrigins("https://localhost:4200", "http://localhost", "http://localhost:8080")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});*/

// Add services to the container.
builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDbSettings"));
builder.Services.Configure<RabbitMqSettings>(builder.Configuration.GetSection("RabbitMqSettings"));

// Register settings as services
builder.Services.AddSingleton(resolver => resolver.GetRequiredService<IOptions<MongoDbSettings>>().Value);
builder.Services.AddSingleton(resolver => resolver.GetRequiredService<IOptions<RabbitMqSettings>>().Value);

// Register services
builder.Services.AddScoped<IBankTransactionService, BankTransactionService.Application.Services.BankTransactionService>();
builder.Services.AddScoped<IBankTransactionRepository, BankTransactionRepository>();
builder.Services.AddSingleton<RabbitMqPublisher>();

// Register MongoDbContext
builder.Services.AddScoped<MongoDbContext>();

// Register RabbitMqConsumerService as a hosted service
builder.Services.AddSingleton<IHostedService, RabbitMqConsumerService>();

// Controller Settings
builder.Services.AddControllers();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseCors("AllowAngularApp");
app.MapControllers();
app.Run();
