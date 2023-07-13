using AutoMapper;
using BillingPeriod.Models;
using BillingPeriod.Services.Activities;
using BillingPeriod.Services.Billing;
using BillingPeriod.Services.Helpers;
using BillingPeriod.Tools;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<DefaultDBContext>();



builder.Services.AddScoped<IBillingService, BillingService>();
builder.Services.AddSingleton<FinalDateCalculator>();
builder.Services.AddSingleton<PrintDayCalculator>();

builder.Services.AddScoped<IActivityService, ActivityService>();


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
