using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SchoolProject.Core;
using SchoolProject.Core.Middleware;
using SchoolProject.Infrustructure;
using SchoolProject.Infrustructure.Data;
using SchoolProject.Service;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(option =>
    option.UseSqlServer(builder.Configuration.GetConnectionString("dbcontext"))
    );
builder.Services.AddInfrustructureDependencies()
                 .AddServiceDependencies()
                 .AddCoreDependencies();

builder.Services.AddControllersWithViews();

builder.Services.AddLocalization(opt =>
    opt.ResourcesPath = ""
    );
builder.Services.Configure<RequestLocalizationOptions>(option =>
{
    List<CultureInfo> Cultures = new List<CultureInfo>
    {
        new CultureInfo("en-US"),
        new CultureInfo("de-DE"),
        new CultureInfo("fr-FR"),
        new CultureInfo("ar-EG")

    };
    option.DefaultRequestCulture = new RequestCulture("ar-EG");
    option.SupportedCultures = Cultures;
    option.SupportedUICultures = Cultures;
});
    

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

    app.UseSwagger();

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");

        options.RoutePrefix = string.Empty;
    });
    app.MapOpenApi();
}
app.UseMiddleware<ErrorHandlerMiddleware>();

var options = app.Services.GetService<IOptions<RequestLocalizationOptions>>();

app.UseRequestLocalization(options.Value);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
