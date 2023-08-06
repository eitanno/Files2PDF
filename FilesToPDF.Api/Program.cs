using FilesToPDF.Api.Services;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseWebRoot("wwwroot");

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

// add services
builder.Services.AddScoped<WordConversionService>();
builder.Services.AddScoped<ExcelConversionService>();
builder.Services.AddScoped<PDFConversionService>();
builder.Services.AddScoped<PicConversionService>();
builder.Services.AddScoped<PowerPointConversionService>();
builder.Services.AddScoped<TIFFConversionService>();

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();

// Add logging configuration
builder.Logging.AddFile(configuration["Logging:File:Path"]);
builder.Logging.AddConsole();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseDefaultFiles();
app.UseStaticFiles();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();



