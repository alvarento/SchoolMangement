using System.Text.Json.Serialization;
using Microsoft.OpenApi;
using Scalar.AspNetCore;
using SchoolManagement.API.Filters;
using SchoolManagement.API.Middleware;
using SchoolManagement.Application;
using SchoolManagement.Communication.Utils.Logs;
using SchoolManagement.Infrastructure;
using SchoolManagement.Infrastructure.Migrations.ExecuteMigrations;


Console.OutputEncoding = System.Text.Encoding.UTF8;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers()
	.AddJsonOptions(options =>
	{
		options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
	});
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddSwaggerGen(options => {
	options.UseInlineDefinitionsForEnums();
	options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
	{
		Description = @"JWT Authorization header using the Bearer scheme.
						Enter 'Bearer' [space] and then your token in the text input below.
						Example: 'Bearer 1234abcdef",
		Name = "Authorization",
		In = ParameterLocation.Header,
		Type = SecuritySchemeType.ApiKey,
		Scheme = "Bearer",
		BearerFormat = "JWT"
	});



	options.AddSecurityRequirement(document =>
		new OpenApiSecurityRequirement
		{
			[
				new OpenApiSecuritySchemeReference("Bearer", document)
			] = []
		});



});

builder.Services.AddMvc(options => options.Filters.Add(typeof(ExceptionFilter)));

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddHttpContextAccessor();

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddFilter("Microsoft.Hosting.Lifetime", LogLevel.None);




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

await ExecuteMigrations();


app.Run();


async Task ExecuteMigrations()
{
	Console.WriteLine("Rodando Migrations..");
	await using var scope = app.Services.CreateAsyncScope();
	await DatabaseMigration.ExecuteMigrations(scope.ServiceProvider);
	Console.WriteLine("Migrations Concluídas!");
}


void PrintLauchConsole(IConfiguration configuration)
{
	//builder.Logging.ClearProviders();
	//builder.Logging.AddConsole();
	//builder.Logging.AddFilter("Microsoft.Hosting.Lifetime", LogLevel.None);

	var port = configuration.GetSection("ConfigLocalHost:Port").Value ?? "5001";


	builder.WebHost.UseUrls($"https://localhost:{port}");
	LogLaunch.PrintLauchConsole(port, "🚀 API", true);
}

