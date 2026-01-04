using Catalog.Application.Commands.CreateProduct;
using Catalog.Application.Interfaces;
using Catalog.Infrastructure.Persistence;
using Catalog.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;
using Scalar.AspNetCore;
using Catalog.Infrastructure;
using Catalog.Application;
using Catalog.API.Middleware;

var builder = WebApplication.CreateBuilder(args);

// --- 1. DEPENDENCY INJECTION ---

// A. Application Layer (MediatR)
builder.Services.AddMediatR(cfg => 
    cfg.RegisterServicesFromAssembly(typeof(CreateProductCommand).Assembly));
    builder.Services.AddApplicationServices();

// B. Infrastructure Layer (DB & Repositories)
builder.Services.AddInfrastructureServices(builder.Configuration.GetConnectionString("DefaultConnection"));
/*
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddDbContext<CatalogDbContext>(opts => 
    opts.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
    */

// C. Observability (OpenTelemetry)
builder.Services.AddOpenTelemetry()
    .WithMetrics(m => m.AddAspNetCoreInstrumentation().AddOtlpExporter())
    .WithTracing(t => t.AddAspNetCoreInstrumentation().AddOtlpExporter());

// D. Health Checks
builder.Services.AddHealthChecks()
    .AddDbContextCheck<CatalogDbContext>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

builder.Services.AddOpenApi();

var app = builder.Build();

// --- 2. MIDDLEWARE ---

// Convert FluentValidation exceptions into JSON 400 responses
app.UseMiddleware<ValidationExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(); 
}


app.MapHealthChecks("/health/ready");
app.MapHealthChecks("/health/live", new() { Predicate = _ => false });

app.MapControllers();

// Auto-Migration (Demo only)

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<CatalogDbContext>();
    
    // OLD: Just creates DB, fails if exists. No history.
    // await db.Database.EnsureCreatedAsync(); 
    
    // NEW: Applies pending migrations one by one. Keeps history.
    await db.Database.MigrateAsync();
}

await app.RunAsync();

/*

    
dotnet ef migrations add AddDescriptionField \
    --project Catalog.Infrastructure \
    --startup-project Catalog.API \
    --output-dir Persistence/Migrations


    
dotnet ef database update \
    --project Catalog.Infrastructure \
    --startup-project Catalog.API

  


*/