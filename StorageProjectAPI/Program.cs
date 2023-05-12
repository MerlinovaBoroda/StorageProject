using StorageProject.Api.Configurations;
using StorageProject.Api.Services;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("MongoDatabase"));

builder.Services.AddSingleton<ItemsService>();
builder.Services.AddSingleton<ItemTypesService>();
builder.Services.AddSingleton<ProvidersService>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(p => p.AddPolicy("CorsPolicy", build =>
{
    //build.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    
    build.WithOrigins("http://localhost:3000").AllowAnyMethod().AllowAnyHeader();
    build.WithOrigins("http://localhost:3001").AllowAnyMethod().AllowAnyHeader();
    build.WithOrigins("http://192.168.7.23:3000").AllowAnyMethod().AllowAnyHeader();
    build.WithOrigins("https://storage-app-sxlb.onrender.com").AllowAnyMethod().AllowAnyHeader();
    
}));


var app = builder.Build();

app.UseCors("CorsPolicy");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
