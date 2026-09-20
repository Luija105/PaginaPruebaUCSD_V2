using System.Net.Http;
using System.Net.Http.Json;
using PaginaPruebaUCSD_V2.Models;

namespace PaginaPruebaUCSD_V2.Services;

// ── SQL / API implementation ──────────────────────────────────────────────────
// Client-side proxy that talks to a server API which accesses the database.
// In Blazor WebAssembly you must NOT connect directly to SQL from the browser.
// Create ASP.NET Core endpoints and call them from here via HttpClient.
//
// To activate: in Program.cs replace
//   builder.Services.AddScoped<IStudentService, MockStudentService>();
// with
//   builder.Services.AddScoped<IStudentService, SqlStudentService>();

public class SqlStudentService : IStudentService
{
    private readonly HttpClient _http;

    public SqlStudentService(HttpClient http) => _http = http;

    // ── Autenticación ─────────────────────────────────────────────────────────
    public async Task<Student?> LoginAsync(string matricula, string password)
    {
        var response = await _http.PostAsJsonAsync("api/auth/login",
            new { Matricula = matricula, Password = password });
        if (!response.IsSuccessStatusCode) return null;
        return await response.Content.ReadFromJsonAsync<Student?>();
    }

    public async Task<Student?> GetStudentByMatriculaAsync(string matricula)
    {
        return await _http.GetFromJsonAsync<Student?>(
            $"api/students/by-matricula/{matricula}");
    }

    // ── Dashboard ─────────────────────────────────────────────────────────────
    public async Task<AcademicRecord?> GetAcademicRecordAsync(int studentId)
    {
        return await _http.GetFromJsonAsync<AcademicRecord?>(
            $"api/students/{studentId}/academicrecord");
    }

    public async Task<List<CreditPeriod>> GetCreditHistoryAsync(int studentId)
    {
        var result = await _http.GetFromJsonAsync<List<CreditPeriod>?>(
            $"api/students/{studentId}/credithistory");
        return result ?? [];
    }

    // ── Consultas Académicas ──────────────────────────────────────────────────

    /// <summary>GET api/students/{id}/seleccion-actual</summary>
    public async Task<List<Asignatura>> GetSeleccionActualAsync(int studentId)
    {
        var result = await _http.GetFromJsonAsync<List<Asignatura>?>(
            $"api/students/{studentId}/seleccion-actual");
        return result ?? [];
    }

    /// <summary>GET api/students/{id}/calificaciones-actuales</summary>
    public async Task<List<Calificacion>> GetCalificacionesActualesAsync(int studentId)
    {
        var result = await _http.GetFromJsonAsync<List<Calificacion>?>(
            $"api/students/{studentId}/calificaciones-actuales");
        return result ?? [];
    }

    /// <summary>GET api/students/{id}/historial-calificaciones</summary>
    public async Task<List<CalificacionHistorial>> GetHistorialCalificacionesAsync(int studentId)
    {
        var result = await _http.GetFromJsonAsync<List<CalificacionHistorial>?>(
            $"api/students/{studentId}/historial-calificaciones");
        return result ?? [];
    }

    /// <summary>GET api/students/{id}/asignaturas-faltantes</summary>
    public async Task<List<AsignaturaFaltante>> GetAsignaturasFaltantesAsync(int studentId)
    {
        var result = await _http.GetFromJsonAsync<List<AsignaturaFaltante>?>(
            $"api/students/{studentId}/asignaturas-faltantes");
        return result ?? [];
    }

    /// <summary>GET api/students/{id}/record-por-periodo</summary>
    public async Task<List<CalificacionPeriodo>> GetRecordPorPeriodoAsync(int studentId)
    {
        var result = await _http.GetFromJsonAsync<List<CalificacionPeriodo>?>(
            $"api/students/{studentId}/record-por-periodo");
        return result ?? [];
    }
}