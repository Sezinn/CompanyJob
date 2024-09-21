using EmployerJob.Infrastructure.DependencyInjection;
using EmployerJob.Application.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using FluentValidation.AspNetCore;
using System.Reflection;
using EmployerJob.Infrastructure.Persistence.Context;
using Nest;
using EmployerJob.Infrastructure.Elasticsearch;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.MaxDepth = 64;
    })
    .AddFluentValidation(config =>
    {
        config.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
    });

// Configure PostgreSQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSQL")));

// Configure Elasticsearch
builder.Services.AddSingleton<ElasticClientProvider>();
builder.Services.AddSingleton<IElasticClient>(sp =>
{
    var provider = sp.GetRequiredService<ElasticClientProvider>();
    return provider.GetElasticClient();
});

// Register MediatR
//builder.Services.AddMediatR(Assembly.GetExecutingAssembly(), typeof(CreateCompanyCommand).Assembly);
//builder.Services.AddMediatR(Assembly.GetExecutingAssembly(), typeof(CreateCompanyCommandHandler).Assembly);

// Register FluentValidation
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

// CQRS ve MediatR ayarlarý
//builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateCompanyCommand).Assembly));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastructure(builder.Configuration.GetConnectionString("PostgreSQL"));
builder.Services.AddApplication();


var app = builder.Build();

// Apply Migrations at Startup
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
