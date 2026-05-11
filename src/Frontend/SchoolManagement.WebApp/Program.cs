using Radzen;
using SchoolManagement.Communication.Utils.Logs;
using SchoolManagement.WebApp.Components;
using SchoolManagement.WebApp.Handler.AuthHandler;
using SchoolManagement.WebApp.Services.AlunoService;
using SchoolManagement.WebApp.Services.LocalStorageService;

Console.OutputEncoding = System.Text.Encoding.UTF8;

var builder = WebApplication.CreateBuilder(args);

Uri baseUrl = new Uri(builder.Configuration["ApiConfig:BaseUrl"] ?? "https://localhost:5001");

// Add services to the container.
builder.Services.AddRazorComponents()
	.AddInteractiveServerComponents();

builder.Services.AddRadzenComponents();

builder.Services.AddHttpClient<IAlunoService, AlunoService>(client => client.BaseAddress = baseUrl)
    .AddHttpMessageHandler<AuthHandler>();

builder.Services.AddTransient<AuthHandler>();
builder.Services.AddScoped<IAlunoService, AlunoService>();
builder.Services.AddScoped<ILocalStorageService, LocalStorageService>();


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
