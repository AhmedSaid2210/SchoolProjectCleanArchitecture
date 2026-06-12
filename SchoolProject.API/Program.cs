using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NSwag;
using NSwag.Generation.Processors.Security;
using SchoolProject.Core;
using SchoolProject.Core.Middleware;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Helper;
using SchoolProject.Infrustructure;
using SchoolProject.Infrustructure.Data;
using SchoolProject.Service;
using System.Globalization;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

builder.Services.AddDbContext<AppDbContext>(option =>
    option.UseSqlServer(builder.Configuration.GetConnectionString("dbcontext"))
    );


builder.Services.AddInfrustructureDependencies()
                 .AddServiceDependencies()
                 .AddCoreDependencies();

builder.Services.AddIdentity<User, Role>(option =>
{
    option.Password.RequireDigit = true;
    option.Password.RequireUppercase = true;
    option.Password.RequireNonAlphanumeric = true;
    option.Password.RequireLowercase = true;
    option.Password.RequiredLength = 6;
    option.Password.RequiredUniqueChars = 1;

    option.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    option.Lockout.AllowedForNewUsers = true;
    option.Lockout.MaxFailedAccessAttempts = 5;

    //option.User.AllowedUserNameCharacters =
    option.User.RequireUniqueEmail = true;


}).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

var JwtSettings = new JwtSettings();
builder.Configuration.GetSection("jwtSettings").Bind(JwtSettings);
builder.Services.AddSingleton(JwtSettings);

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
                   ValidateIssuer = JwtSettings.ValidateIssuer,
                   ValidIssuers = new[] { JwtSettings.Issuer },
                   ValidateIssuerSigningKey = JwtSettings.ValidateIssuerSigningKey,
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(JwtSettings.Secret)),
                   ValidAudience = JwtSettings.Audience,
                   ValidateAudience = JwtSettings.ValidateAudience,
                   ValidateLifetime = JwtSettings.ValidateLifeTime,
               };
           });



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
    
builder.Services.AddHttpContextAccessor();

builder.Services.AddOpenApiDocument(option =>
{
    option.AddSecurity("Bearer", new OpenApiSecurityScheme
    {
        Type = OpenApiSecuritySchemeType.Http,
        Name = "Authorization",
        In = OpenApiSecurityApiKeyLocation.Header,
        Description = "Bearer Token Authorization Header",
        Scheme = "Bearer"
    });

    option.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("Bearer"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUI();
    
}
app.UseMiddleware<ErrorHandlerMiddleware>();

var options = app.Services.GetService<IOptions<RequestLocalizationOptions>>();

app.UseRequestLocalization(options.Value);

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
