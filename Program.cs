using System;
using System.Net.Http;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using PaginaPruebaUCSD_V2;
using PaginaPruebaUCSD_V2.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// ── Data services ────────────────────────────────────────────────────────────
builder.Services.AddScoped<IStudentService, MockStudentService>();
builder.Services.AddScoped<IAuthService, AuthService>();

// Registrar el servicio de horarios (asegúrate de usar la clase concreta que implementaste, ej. ScheduleService o MockScheduleService)
builder.Services.AddScoped<IScheduleService, MockScheduleService>();

// HttpClient
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();