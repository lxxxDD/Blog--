using Blog2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// 注册 IConfiguration
builder.Services.AddSingleton(builder.Configuration);

// 添加数据库上下文
builder.Services.AddDbContext<DreamBlogContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DreamBlog"));
});
builder.Services.AddMvcCore().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss"; //日期默认格式      
                                                                         //忽略循环引用
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

});


//测试环境或开发环境下
//任何跨域请求都通过

// 配置跨域请求策略
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAnyOriginPolicy",
        builder =>
        {
            builder.AllowAnyOrigin();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// 使用跨域请求策略
app.UseCors("AllowAnyOriginPolicy");
app.UseAuthorization();
app.UseStaticFiles();
app.MapControllers();

app.Run();
