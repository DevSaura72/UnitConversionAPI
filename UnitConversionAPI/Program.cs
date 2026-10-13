using UnitConversionAPI.Configuration;
using UnitConversionAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Bind the UnitConversion section of appsettings.json
// to the strongly typed UnitConversionOptions class.
builder.Services
    .AddOptions<UnitConversionOptions>()
    .Bind(builder.Configuration.GetSection("UnitConversion"))
    // Fail application startup if no categories are configured.
    .Validate(
        options => options.Categories.Count > 0,
        "At least one unit category must be configured.")
    .ValidateOnStart();

// Register the service implementation.
// Whenever a controller requests IUnitConversionService,
// dependency injection supplies UnitConversionService.
builder.Services.AddScoped<
    IUnitConversionService,
    UnitConversionService>();

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
