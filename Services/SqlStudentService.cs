using System.Net.Http;
using System.Net.Http.Json;
using PaginaPruebaUCSD_V2.Models;

namespace PaginaPruebaUCSD_V2.Services;

public class SqlStudentService : IStudentService
{
    private readonly HttpClient _http;

    public SqlStudentService(HttpClient http) => _http = http;

    // ── Autenticación ─────────────────────────────────────────────────────────
    public async Task<Student?> LoginAsync(string matricula, string password)
    {
        try
        {
            var response = await _http.PostAsJsonAsync("api/auth/login",
                new { Matricula = matricula, Password = password });

            if (!response.IsSuccessStatusCode) return null;
            return await response.Content.ReadFromJsonAsync<Student?>();
        }
        catch
        {
            return null;
        }
    }

    public async Task<Student?> GetStudentByMatriculaAsync(string matricula)
    {
        try
        {
            return await _http.GetFromJsonAsync<Student?>(
                $"api/students/by-matricula/{matricula}");
        }
        catch
        {
            return null;
        }
    }

    // ── Dashboard ─────────────────────────────────────────────────────────────
    public async Task<AcademicRecord?> GetAcademicRecordAsync(int studentId)
    {
        try
        {
            return await _http.GetFromJsonAsync<AcademicRecord?>(
                $"api/students/{studentId}/academicrecord");
        }
        catch
        {
            return null;
        }
    }

    public async Task<List<CreditPeriod>> GetCreditHistoryAsync(int studentId)
    {
        try
        {
            var result = await _http.GetFromJsonAsync<List<CreditPeriod>?>(
                $"api/students/{studentId}/credithistory");
            return result ?? new List<CreditPeriod>();
        }
        catch
        {
            return new List<CreditPeriod>();
        }
    }

    // ── Consultas Académicas ──────────────────────────────────────────────────

    public async Task<List<Asignatura>> GetSeleccionActualAsync(int studentId)
    {
        try
        {
            var result = await _http.GetFromJsonAsync<List<Asignatura>?>(
                $"api/students/{studentId}/seleccion-actual");
            return result ?? new List<Asignatura>();
        }
        catch
        {
            return new List<Asignatura>();
        }
    }

    public async Task<List<Calificacion>> GetCalificacionesActualesAsync(int studentId)
    {
        try
        {
            var result = await _http.GetFromJsonAsync<List<Calificacion>?>(
                $"api/students/{studentId}/calificaciones-actuales");
            return result ?? new List<Calificacion>();
        }
        catch
        {
            return new List<Calificacion>();
        }
    }

    public async Task<List<CalificacionHistorial>> GetHistorialCalificacionesAsync(int studentId)
    {
        try
        {
            var result = await _http.GetFromJsonAsync<List<CalificacionHistorial>?>(
                $"api/students/{studentId}/historial-calificaciones");
            return result ?? new List<CalificacionHistorial>();
        }
        catch
        {
            return new List<CalificacionHistorial>();
        }
    }

    public async Task<List<AsignaturaFaltante>> GetAsignaturasFaltantesAsync(int studentId)
    {
        try
        {
            var result = await _http.GetFromJsonAsync<List<AsignaturaFaltante>?>(
                $"api/students/{studentId}/asignaturas-faltantes");
            return result ?? new List<AsignaturaFaltante>();
        }
        catch
        {
            return new List<AsignaturaFaltante>();
        }
    }

    public async Task<List<CalificacionPeriodo>> GetRecordPorPeriodoAsync(int studentId)
    {
        try
        {
            var result = await _http.GetFromJsonAsync<List<CalificacionPeriodo>?>(
                $"api/students/{studentId}/record-por-periodo");
            return result ?? new List<CalificacionPeriodo>();
        }
        catch
        {
            return new List<CalificacionPeriodo>();
        }
    }
}