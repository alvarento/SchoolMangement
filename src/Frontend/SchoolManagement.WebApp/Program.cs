using Radzen;
using SchoolManagement.Communication.Utils.Logs;
using SchoolManagement.WebApp.Components;
using SchoolManagement.WebApp.Services.AlunoService;

Console.OutputEncoding = System.Text.Encoding.UTF8;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
	.AddInteractiveServerComponents();

builder.Services.AddRadzenComponents();

builder.Services.AddScoped(sp =>
    new HttpClient { BaseAddress = new Uri("https://localhost:5001") });


builder.Services.AddScoped<IAlunoService, AlunoService>();


PrintLauchConsole(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error", createScopeForErrors: true);
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
	.AddInteractiveServerRenderMode();

app.Run();



void PrintLauchConsole(IConfiguration configuration)
{
	builder.Logging.ClearProviders();
	builder.Logging.AddConsole();
	builder.Logging.AddFilter("Microsoft.Hosting.Lifetime", LogLevel.None);

	var port = configuration.GetSection("ConfigLocalHost:Port").Value ?? "5002";


	builder.WebHost.UseUrls($"https://localhost:{port}");

	LogLaunch.PrintLauchConsole(port, "🎉 FrontEnd");
}
