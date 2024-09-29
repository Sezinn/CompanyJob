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
using Hangfire;
using EmployerJob.Application.Hangfire.Services;

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

// Configure Elasticsearch
builder.Services.AddSingleton<ElasticClientProvider>();
builder.Services.AddSingleton<IElasticClient>(sp =>
{
    var provider = sp.GetRequiredService<ElasticClientProvider>();
    return provider.GetElasticClient();
});

// Register FluentValidation
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastructure(builder.Configuration.GetConnectionString("PostgreSQL"),builder.Configuration.GetConnectionString("Cache"));

builder.Services.AddApplication();
builder.Services.AddSingleton<JobService>();

var app = builder.Build();

// Apply Migrations at Startup
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();
}

// Hangfire Dashboard ve Server'ý baþlat
app.UseHangfireDashboard("/hangfire", new DashboardOptions()
{
    //PrefixPath = "/job",
    DashboardTitle = "Hangfire Dashboard"
});
app.UseHangfireServer();

// Zamanlanmýþ iþlerin baþlatýlmasý için DI ile JobService'yi kullan
var jobService = app.Services.GetRequiredService<JobService>();
jobService.RunOnceJob();

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
