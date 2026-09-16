using System.Net.Http;
using System.Net.Http.Json;
using PaginaPruebaUCSD_V2.Models;

namespace PaginaPruebaUCSD_V2.Services;

// Client-side proxy that talks to a server API which in turn accesses a SQL database.
// In Blazor WebAssembly you must not connect directly to SQL from the browser:
// create server API endpoints (e.g. ASP.NET Core) that perform DB operations and
// call those endpoints from this service via HttpClient.
public class SqlStudentService : IStudentService
{
    private readonly HttpClient _http;

    public SqlStudentService(HttpClient http) => _http = http;

    public async Task<Student?> LoginAsync(string matricula, string password)
    {
        // POST api/auth/login { matricula, password } -> returns Student or 401
        var response = await _http.PostAsJsonAsync("api/auth/login", new { Matricula = matricula, Password = password });
        if (!response.IsSuccessStatusCode) return null;
        return await response.Content.ReadFromJsonAsync<Student?>();
    }

    public async Task<Student?> GetStudentByMatriculaAsync(string matricula)
    {
        // GET api/students/by-matricula/{matricula}
        return await _http.GetFromJsonAsync<Student?>($"api/students/by-matricula/{matricula}");
    }

    public async Task<AcademicRecord?> GetAcademicRecordAsync(int studentId)
    {
        // GET api/students/{id}/academicrecord
        return await _http.GetFromJsonAsync<AcademicRecord?>($"api/students/{studentId}/academicrecord");
    }

    public async Task<List<CreditPeriod>> GetCreditHistoryAsync(int studentId)
    {
        // GET api/students/{id}/credithistory
        var result = await _http.GetFromJsonAsync<List<CreditPeriod>?>($"api/students/{studentId}/credithistory");
        return result ?? new List<CreditPeriod>();
    }
}
