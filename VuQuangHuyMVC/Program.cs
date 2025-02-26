using BusinessLogicLayer.Service;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Services;
using VuQuangHuyMVC.BusinessObjects;

var builder = WebApplication.CreateBuilder(args);

// Cấu hình DbContext
builder.Services.AddDbContext<FunewsManagementContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DATA")));

// Cấu hình Session
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Hết hạn sau 30 phút
    options.Cookie.HttpOnly = true; // Bảo mật cookie
    options.Cookie.IsEssential = true;
});

// Đăng ký các Service và Repository
builder.Services.AddScoped<FunewsManagementContext>();

// Category
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

// SystemAccount
builder.Services.AddScoped<ISystemAccountService, SystemAccountService>();
builder.Services.AddScoped<ISystemAccountRepository, SystemAccountRepository>();

// Tag
builder.Services.AddScoped<ITagService, TagService>();
builder.Services.AddScoped<ITagRepository, TagRepository>();

// NewsArticle
builder.Services.AddScoped<INewsArticleService, NewsArticleService>();
builder.Services.AddScoped<INewsArticleRepository, NewsArticleRepository>();


builder.Services.AddScoped<EmailService>();



// Cấu hình Controllers & Views
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Cấu hình Middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Bật Session Middleware
app.UseSession();

app.UseAuthorization();

// Cấu hình Route mặc định
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=News}/{action=Index}/{id?}");

app.Run();
