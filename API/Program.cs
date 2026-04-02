
using BLL.Services;
using DAL.Data;
using DAL.Data;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using OnlineShop.DAL.Repositories;

var builder = WebApplication.CreateBuilder(args);

// 1. KẾT NỐI DATABASE
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

// 2. ĐĂNG KÝ DEPENDENCY INJECTION
//    .NET sẽ tự tạo instance khi nào cần
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductServices, ProductServices>();

// 3. CONTROLLER + JSON + XML
builder.Services.AddControllers(options =>
{
    // Cho phép trả XML khi client gửi Accept: application/xml
    options.RespectBrowserAcceptHeader = false;
})
.AddJsonOptions(options =>
{
    // Giữ nguyên tên PascalCase (ProductName không bị đổi thành productName)
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
    options.JsonSerializerOptions.WriteIndented = true;
})
.AddXmlSerializerFormatters(); // Bật hỗ trợ XML

// 4. CORS — cho phép Angular (port 4200) gọi API
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", policy =>
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod());

});

// 5. SWAGGER — giao diện test API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(); // Truy cập tại /swagger
}
app.UseHttpsRedirection();
app.UseCors("AllowAngular");  // Phải đặt TRƯỚC MapControllers
app.UseAuthorization();
app.MapControllers();
app.Run();
