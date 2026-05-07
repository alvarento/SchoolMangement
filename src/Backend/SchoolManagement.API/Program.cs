using System.Text.Json.Serialization;
using Scalar.AspNetCore;
using SchoolManagement.API.Converters;
using SchoolManagement.API.Filters;
using SchoolManagement.API.Middleware;
using SchoolManagement.Application;
using SchoolManagement.Communication.Utils.Logs;
using SchoolManagement.Infrastructure;


Console.OutputEncoding = System.Text.Encoding.UTF8;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers()
	.AddJsonOptions(options =>
	{
		options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
		options.JsonSerializerOptions.Converters.Add(new DateTimeBrazilConverter());
	});
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddSwaggerGen(c => {
	c.UseInlineDefinitionsForEnums();
});

builder.Services.AddMvc(options => options.Filters.Add(typeof(ExceptionFilter)));

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddRouting(options => options.LowercaseUrls = true);




PrintLauchConsole(builder.Configuration);


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.MapOpenApi();
	app.UseSwagger();
	app.UseSwaggerUI();
	app.MapScalarApiReference("/scalar");
}


app.UseMiddleware<CultureMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


app.Run();


void PrintLauchConsole(IConfiguration configuration)
{
	builder.Logging.ClearProviders();
	builder.Logging.AddConsole();
	builder.Logging.AddFilter("Microsoft.Hosting.Lifetime", LogLevel.None);

	var port = configuration.GetSection("ConfigLocalHost:Port").Value ?? "5001";


	builder.WebHost.UseUrls($"https://localhost:{port}");
	LogLaunch.PrintLauchConsole(port, "🚀 API", true);
}

