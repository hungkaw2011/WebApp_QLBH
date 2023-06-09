using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using WebApp.DataAccess;
using WebApp.DataAccess.Repository.IRepository;
using WebApp.DataAccess.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using WebApp.Utility;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")
    ));

//Cấu hình Identity phân loại Role để quản lý login và xác thực người dùng=>Lưu trữ dữ liệu người dùng(AddEntityFrameworkStores)
builder.Services.AddIdentity<IdentityUser,IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();

//Tính năng Razor Runtime Compilation cho phép bạn chỉnh sửa trang Razor Pages mà không cần phải khởi động lại ứng dụng => (lag).
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
builder.Services.AddRazorPages();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IEmailSender, EmailSender>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts(); // bật HTTP Strict Transport Security (HSTS), là một cơ chế để bảo vệ các kết nối truy cập ứng dụng
}

builder.Services.AddDistributedMemoryCache();

app.UseHttpsRedirection(); //chuyển hướng các yêu cầu HTTP sang HTTPS
app.UseStaticFiles(); // cho phép truy cập các tài nguyên tĩnh (như CSS, JavaScript, hình ảnh...) của ứng dụng

app.UseRouting(); // xác định cách các yêu cầu HTTP sẽ được định tuyến đến các điểm cuối (endpoints) trong ứng dụng
app.UseAuthentication();// xác thực người dùng và quyền truy cập vào các tài nguyên được bảo vệ trong ứng dụng hay không
app.UseAuthorization(); // thực hiện xác thực và phân quyền trên các yêu cầu HTTP.

app.MapRazorPages();//Định tuyến các yêu cầu đến các trang Razor cho (MidleWare)
app.MapControllerRoute(
    name: "default",
    pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");

app.Run();
