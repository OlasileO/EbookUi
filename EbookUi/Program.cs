using Ebook_Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Ebook_Model.Seeder;
using Ebook_Service.Abstract;
using Ebook_Service.Implementation;
using Microsoft.AspNetCore.Authentication.Cookies;
using EbookUi.Models.Shoppingcart;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<EbookDbContext>(options => options.UseSqlServer(
builder.Configuration.GetConnectionString("conn")));

builder.Services.AddIdentity<IdentityUser,IdentityRole>()
    .AddEntityFrameworkStores<EbookDbContext>()
    .AddDefaultUI()
    .AddDefaultTokenProviders();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped(sc => ShoppingCart.GetShoppingCart(sc));

builder.Services.AddScoped<IGenreService,GenreService>();
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IPublisherService, PublisherService>();
builder.Services.AddScoped<IBookSerivce, BookSerivce>();
builder.Services.AddScoped<IOrderService, OrderService>();

builder.Services.AddMemoryCache();
builder.Services.AddSession();

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
});
var app = builder.Build();

//using( var scope = app.Services.CreateScope())
//{
//    await DbSeeder.SeederDefault(scope.ServiceProvider);
//}

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
app.UseSession();
app.UseAuthorization();


app.UseEndpoints(endpoints =>
{
    //
    endpoints.MapRazorPages();
});
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Book}/{action=Index}/{id?}");

app.Run();
