using ProductManagement.DAL.Helpers.Extensions;
using ProductManagement.BL.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using ProductManagement.Common.Exceptions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRepositories(builder.Configuration);
builder.Services.AddServices();

builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
    options.SerializerSettings.Converters.Add(new StringEnumConverter());
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseMiddleware<ExceptionHandler>();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();
