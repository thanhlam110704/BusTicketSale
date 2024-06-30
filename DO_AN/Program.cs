using DO_AN.Models;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession();
//đặt lại đường dẫ viewcomponent
//builder.Services.AddMvc()
//    .AddRazorOptions(options =>
//    {
//        options.ViewLocationFormats.Add("/Components/TrainSearch/{0}.cshtml");
//        //options.ViewLocationFormats.Add("/Areas/{2}/ViewComponents/Shared/{0}.cshtml");
//        //options.ViewLocationFormats.Add("/ViewComponents/{1}/{0}.cshtml");
//        //options.ViewLocationFormats.Add("/ViewComponents/Shared/{0}.cshtml");
//    });



builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddRazorPages();
builder.Services.AddDbContext<DOANContext>(opts =>
{
    opts.UseSqlServer(
    builder.Configuration["ConnectionStrings:DoAnConnection"]);
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Train}/{action=SearchTrain}/{id?}");
    endpoints.MapRazorPages();
});


app.Run();
