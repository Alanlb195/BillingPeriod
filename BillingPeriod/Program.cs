using AutoMapper;
using BillingPeriod.Models;
using BillingPeriod.Services.Activities;
using BillingPeriod.Services.Billing;
using BillingPeriod.Services.Coockie;
using BillingPeriod.Services.Helpers;
using BillingPeriod.Services.PresentationCardService;
using BillingPeriod.Tools;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddDbContext<DefaultDBContext>();

// Billing Services
builder.Services.AddScoped<IBillingService, BillingService>();
builder.Services.AddSingleton<FinalDateCalculator>();
builder.Services.AddSingleton<PrintDayCalculator>();
// Activity Calendar Services
builder.Services.AddScoped<IActivityService, ActivityService>();
//Coockie Services
builder.Services.AddScoped<ICookieService, CookieService>();
// Presentation Card Services
builder.Services.AddScoped<IPresentationCardService, PresentationCardService>();
// Pascal Triangle Service

//--------- Mapper configuration ------------------//
var mapperConfiguration = new MapperConfiguration(m =>
{
    m.AddProfile(new MappingProfile());
});

IMapper mapper = mapperConfiguration.CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddMvc(); //--------- End Mapper Config

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
