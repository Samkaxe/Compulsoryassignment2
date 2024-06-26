using Microsoft.EntityFrameworkCore;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using PatientDatabase;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddOpenTelemetry()
    .ConfigureResource(builder => builder.AddService("PatientService"))
    .WithTracing(builder =>
        {
            builder
                .AddZipkinExporter(options =>
                    options.Endpoint = new Uri("http://zipkin:9411/api/v2/spans"))
                .AddSource("PatientService.API")
                .SetSampler(new AlwaysOnSampler())
                .AddAspNetCoreInstrumentation();
        }
    );

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Seq("http://localhost:5341") 
    // setup
    .CreateLogger();


builder.Services.AddScoped<IPatientRepository, PatientRepository>();
builder.Services.AddDbContext<PatientDbContext>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins", builder =>
    {
        builder.WithOrigins("http://localhost:4200", "http://localhost:3000") // Patient ui
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

var app = builder.Build();

app.UseCors("AllowSpecificOrigins");

using (var scope = app.Services.CreateScope()) 
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<PatientDbContext>(); 
    context.Database.Migrate();
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
