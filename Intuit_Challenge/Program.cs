using Intuit_Challenge.Data;
using Intuit_Challenge.Services;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
const string corsPolicy = "defaultpolicy";

var appSettings = new ConfigurationService(builder.Configuration);
var configuration = builder.Configuration;

builder.Services.AddDbContext<IntuitChallengeContext>(
                c => c.UseSqlServer(appSettings.GetConnectionString()));

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "CMS", Version = "v1" });
});

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddTransient<IConfigurationService, ConfigurationService>();
builder.Services.AddTransient<IClientService, ClientService>();
builder.Services.AddTransient<IWeatherService, WeatherService>();
builder.Services.AddScoped(typeof(IAsyncRepository<>), typeof(EfRepository<>));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


builder.Services.AddCors(o => o.AddPolicy(corsPolicy, builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader()
           .SetPreflightMaxAge(TimeSpan.FromSeconds(600));
}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseCors(corsPolicy);

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
