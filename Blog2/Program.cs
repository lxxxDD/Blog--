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
// ע�� IConfiguration
builder.Services.AddSingleton(builder.Configuration);

// ������ݿ�������
builder.Services.AddDbContext<DreamBlogContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DreamBlog"));
});
builder.Services.AddMvcCore().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss"; //����Ĭ�ϸ�ʽ      
                                                                         //����ѭ������
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

});


//���Ի����򿪷�������
//�κο�������ͨ��

// ���ÿ����������
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
// ʹ�ÿ����������
app.UseCors("AllowAnyOriginPolicy");
app.UseAuthorization();
app.UseStaticFiles();
app.MapControllers();

app.Run();
