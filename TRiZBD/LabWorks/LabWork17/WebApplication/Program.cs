using System.Globalization;
using WebApplication.Contexts;

var builder = Microsoft.AspNetCore.Builder.WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<AppDbContext>();

builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();

CultureInfo.DefaultThreadCurrentCulture = new("ru-RU") 
{ 
    NumberFormat = { NumberDecimalSeparator = "." } 
};

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseRouting();

app.UseSession();
app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
