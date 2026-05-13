using Microsoft.AspNetCore.Components.Authorization;
using Radzen;
using SchoolManagement.Communication.Utils.Logs;
using SchoolManagement.WebApp.Components;
using SchoolManagement.WebApp.Handler.AuthHandler;
using SchoolManagement.WebApp.Services.AlunoService;
using SchoolManagement.WebApp.Services.AuthStateService;
using SchoolManagement.WebApp.Services.LoginService;

Console.OutputEncoding = System.Text.Encoding.UTF8;

var builder = WebApplication.CreateBuilder(args);

Uri baseUrl = new Uri(builder.Configuration["ApiConfig:BaseUrl"] ?? "https://localhost:5001");

// Add services to the container.
builder.Services.AddRazorComponents()
	.AddInteractiveServerComponents(options => options.DetailedErrors = true);

builder.Services.AddRadzenComponents();

//builder.Services.AddAuthentication();
//builder.Services.AddAuthentication(options =>
//{
//	options.DefaultScheme = "Manual";
//	options.DefaultChallengeScheme = "Manual";
//});
//.AddCookie("Manual");
builder.Services.AddAuthentication("Manual")
	.AddCookie("Manual", options =>
	{
		// Isso impede que o ASP.NET tente redirecionar via Servidor
		options.LoginPath = "/login";
		options.Events.OnRedirectToLogin = context =>
		{
			context.Response.StatusCode = StatusCodes.Status401Unauthorized;
			return Task.CompletedTask;
		};
	});

//builder.Services.AddAuthentication("Cookies")
//	.AddCookie("Cookies", options =>
//	{
//		options.LoginPath = "/login";
//	});


builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();

builder.Services.AddHttpContextAccessor();

builder.Services.AddHttpClient<ILoginService, LoginService>(
	client => client.BaseAddress = baseUrl);


builder.Services.AddTransient<AuthHandler>();
builder.Services.AddHttpClient<IAlunoService, AlunoService>(client => client.BaseAddress = baseUrl)
    .AddHttpMessageHandler<AuthHandler>();

builder.Services.AddScoped<ITokenProvider, TokenProvider>();
builder.Services.AddScoped<IAuthStateService, AuthStateService>();
builder.Services.AddScoped<AuthenticationStateProvider>(
	sp => (AuthStateService)sp.GetRequiredService<IAuthStateService>());


builder.Services.AddHttpClient<ILoginService, LoginService>(client => client.BaseAddress = baseUrl);



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

app.UseAuthentication();
app.UseAuthorization();

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
