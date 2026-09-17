using System;
using System.Net.Http;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using PaginaPruebaUCSD_V2;
using PaginaPruebaUCSD_V2.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// ── Data service ──────────────────────────────────────────────────────────────
// Swap MockStudentService for a real implementation (HttpClient + SQL API)
// without touching any component code.
// Default: mock implementation used during development
builder.Services.AddScoped<IStudentService, MockStudentService>();
builder.Services.AddScoped<IAuthService, AuthService>();

// HttpClient is commonly required by services and components in WebAssembly
// register a default one pointing at the app base address. This also
// allows SqlStudentService (if enabled) to be constructed with HttpClient.
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// To use a real SQL-backed backend you must expose an API (ASP.NET Core) that
// accesses the database. Then register SqlStudentService (a proxy that calls
// the API) and an HttpClient configured to the API base address, for example:
//
// builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:5001/") });
// builder.Services.AddScoped<IStudentService, SqlStudentService>();

await builder.Build().RunAsync();
