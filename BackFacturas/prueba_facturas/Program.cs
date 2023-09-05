using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.OpenApi.Models;
using prueba_facturas.Configuracion;
using prueba_facturas.Dto;
using prueba_facturas.Modelos;
using prueba_facturas.Servicios.Implementacion;
using prueba_facturas.Servicios.Interfaces;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.Configure<FacturaStoreDatabaseSettings>(
    builder.Configuration.GetSection("FacturaStoreDatabase"));

builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("SmtpSettings"));


builder.Services.AddScoped<IFacturaServices, FacturaServicios>();
builder.Services.AddSingleton<IEmailServices, EmailService>();



var mapperConfig = new MapperConfiguration(m => {


    m.AddProfile(new MapperProfile());

});

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{

    var apiInfo = new OpenApiInfo
    {
        Title = "Facturas Api",
        Description = "Api encargada de mandar correos electronicos a los clientes y cambiar el estado de sus facturas.",
        Version = "final",
        Contact = new OpenApiContact
        {
            Name = "Santiago Manrique",
            Email = "santiagomanriq.lopez@gmail.com",

        }

    };

    c.SwaggerDoc("v1", apiInfo);
  
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAnyOrigin", builder =>
    {
        builder
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAnyOrigin");


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
