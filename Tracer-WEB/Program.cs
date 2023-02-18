using Microsoft.EntityFrameworkCore;
using Tracer_WEB.Data;

var builder = WebApplication.CreateBuilder(args);

// Connect to DB
builder.Services.AddDbContext<TracerContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString(nameof(TracerContext))));


// Add services to the container.
builder.Services.AddControllersWithViews();

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
app.UseDeveloperExceptionPage();
app.UseRouting();
app.UseStatusCodePages();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
