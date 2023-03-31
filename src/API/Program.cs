using API.Filter;
using API.Common;
using Application;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Common.Repository;
using Persistence.Common.UnitOfWork;
using Persistence.Interfaces.DbContext;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;

// Add services to the container.

// Configure API Controller
builder.Services.AddControllers(opt => opt.Filters.Add(typeof(ModelBindingValidationFilter)));
builder.Services.Configure<ApiBehaviorOptions>(opt => opt.SuppressModelStateInvalidFilter = true);

// Configure Fluent validation
ValidatorOptions.Global.PropertyNameResolver = (type, member, expression) =>
{
    if (member != null)
    {
        return member.Name;
    }

    return null;
};

builder.Services.AddFluentValidationAutoValidation();

// Configure Auto-Mapper
builder.Services.AddAutoMapper(Assembly.Load("Application"));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// Configure Logging Providers
builder.Logging.ClearProviders();

// Capture all log-level entries from Program


// Configure Application Insights


// Configure CORS
builder.Services.AddCors(options =>
    options.AddPolicy("*", p =>
    p.AllowAnyMethod()
     .AllowAnyHeader()
    )
);

// Configure Database connection string
builder.Services.AddDbContext<BracketsDbContext>(options =>
    options.UseSqlite(configuration.GetConnectionString("DefaultConnection"),
           b => b.MigrationsAssembly(typeof(BracketsDbContext).Assembly.FullName)));

RegisterUnitOfWorkAndRepositories(builder);

builder.Services.AddApplication(configuration);
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IBracketsDbContext>(scope => scope.GetService<IBracketsDbContext>());
builder.Services.AddSingleton(typeof(ILogger), typeof(Logger<Program>));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCustomExceptionHandler();

app.UseHttpsRedirection();

app.UseCors("*");

app.UseAuthorization();

app.MapControllers();

app.Run();

void RegisterUnitOfWorkAndRepositories(WebApplicationBuilder builder)
{
    builder.Services.AddScoped<IUnitOfWork, UnitOfWork>(provider =>
    {
        var dbContext = provider.GetRequiredService<BracketsDbContext>();
        return new UnitOfWork(dbContext);
    });

    builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
}