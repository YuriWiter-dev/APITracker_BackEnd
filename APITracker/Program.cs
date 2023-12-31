using APITracker.Data;
using APITracker.Profiles;
using APITracker.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

// Add services to the container.
//builder.Services.AddCors(option =>
//{
//    option.AddPolicy("AllOrigins",
//        builder =>
//        {
//            builder.AllowAnyHeader()
//            .AllowAnyOrigin()
//            .AllowAnyMethod();
//        });
//});

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.AllowAnyOrigin();
                      });
});

builder.Services.AddCors(option =>
{
    option.AddPolicy("AllOrigins",
        builder =>
        {
            builder.AllowAnyHeader()
            .AllowAnyOrigin()
            .AllowAnyMethod();
        });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(x => x.AddProfile(new RequisicaoProfile()));

builder.Services.AddScoped<IBaseRepositorio, BaseRepositorio>();

builder.Services.AddScoped<IEnderecoApiRepository, EnderecoApiRepository>();



builder.Services.AddEntityFrameworkSqlServer().AddDbContext<BaseContext>((serviceProvider, dbContextBuilder) =>
{
    dbContextBuilder.UseSqlServer(builder.Configuration.GetConnectionString("BaseDatabase"));
}, ServiceLifetime.Scoped);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseCors("AllOrigins");

app.Run();
