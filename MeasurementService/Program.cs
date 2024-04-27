using MeasurementDatabase;
using Microsoft.EntityFrameworkCore;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Serilog;
using ILogger = Serilog.ILogger;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IMeasurementRepository, MeasurementRepository>();
builder.Services.AddDbContext<MeasurementDbContext>();

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Seq("http://localhost:5341") 
    .WriteTo.Console()
    .CreateLogger();


builder.Services.AddOpenTelemetry()
    .ConfigureResource(builder => builder.AddService("MeasurementService"))
    .WithTracing(builder =>
        {
            builder
                .AddZipkinExporter(options =>
                    options.Endpoint = new Uri("http://zipkin:9411/api/v2/spans"))
                .AddSource("MeasurementService.API")
                .SetSampler(new AlwaysOnSampler())
                .AddAspNetCoreInstrumentation();
        }
    );

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins", builder =>
    {
        builder.WithOrigins("http://localhost:8080", "http://localhost:3000")
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

builder.Services.AddHttpClient("PatientService", client =>
{
    client.BaseAddress = new Uri("http://patient-service:80/"); // Base URL of the Patient microservice
});


var app = builder.Build();

app.UseCors("AllowSpecificOrigins");

using (var scope = app.Services.CreateScope()) 
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<MeasurementDbContext>(); 
    context.Database.Migrate(); // This line applies the migrations 
}



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
