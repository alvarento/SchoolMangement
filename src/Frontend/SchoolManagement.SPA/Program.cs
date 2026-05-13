using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Radzen;
using SchoolManagement.SPA;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// 2. Configura o endereço base da sua API
var apiBaseAddress = new Uri("https://localhost:5001/");

// 3. Registra o LocalStorageService
builder.Services.AddScoped<ILocalStorageService, LocalStorageService>();

// 4. Registra o AuthHandler (para injetar o Token automaticamente)
builder.Services.AddTransient<AuthHandler>();

// 5. Registra os Serviços de Dados (AlunoService) usando o HttpClient com o Handler
builder.Services.AddHttpClient<IAlunoService, AlunoService>(client =>
{
	client.BaseAddress = apiBaseAddress;
})
.AddHttpMessageHandler<AuthHandler>();

// 6. Registra Serviços de Terceiros (Radzen)
builder.Services.AddRadzenComponents();

// 7. Configura a Autenticação (Caso use o sistema nativo do Blazor)
builder.Services.AddAuthorizationCore();

await builder.Build().RunAsync();
