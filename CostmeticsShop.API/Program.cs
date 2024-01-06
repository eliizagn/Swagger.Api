using Microsoft.OpenApi.Models;
using System.Reflection;
using System;
using System.Diagnostics.Metrics;
using System.Threading;
using Microsoft.Extensions.Options;
using App.Metrics.AspNetCore.Endpoints;
using App.Metrics.AspNetCore;
using App.Metrics.Formatters.Prometheus;
using Serilog;
using OpenTelemetry.Resources;
using System.Diagnostics;
using OpenTelemetry;
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;
using System.Runtime.InteropServices.Marshalling;
using OpenTelemetry.Exporter;

var builder = WebApplication.CreateBuilder(args);

const string serviceName = "CosmeticsShop";
builder.Services.AddOpenTelemetry()
    .WithTracing(traceProviderBuilder =>
        traceProviderBuilder
            .AddSource(serviceName)
            .ConfigureResource(resource => resource.AddService(serviceName))
            .AddAspNetCoreInstrumentation(options => options.RecordException = true)
            .AddConsoleExporter()
            .AddOtlpExporter());

builder.Host.UseSerilog((context, configuration) =>
{
    configuration.ReadFrom.Configuration(context.Configuration);
});

builder.Services.AddMetrics();
builder.Services.AddControllers();
builder.Services.AddMetricsTrackingMiddleware();

builder.Host.UseMetricsWebTracking()
    .UseMetrics(options => {
        options.EndpointOptions = endpointsOptions =>
    {
        endpointsOptions.MetricsTextEndpointOutputFormatter = new MetricsPrometheusTextOutputFormatter();
        endpointsOptions.MetricsEndpointOutputFormatter = new MetricsPrometheusProtobufOutputFormatter();
        endpointsOptions.EnvironmentInfoEndpointEnabled = false;
    };
    });



builder.Services.AddSwaggerGen(options =>
{

    // Дополнительная информация для генерации документации.
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Cosmetics.Shop",
        Description = "Магазин косметики",
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});



var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseRouting();
app.MapControllers();

app.Run();
