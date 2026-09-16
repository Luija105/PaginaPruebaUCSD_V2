using PaginaPruebaUCSD_V2.Models;

namespace PaginaPruebaUCSD_V2.Services;

// ── Service contract ──────────────────────────────────────────────────────────
// Implement this interface against any data source (SQL Server via EF Core,
// PostgreSQL via Npgsql, a REST API via HttpClient, etc.) and register the
// new class in Program.cs — no component code changes required.

public interface IStudentService
{
    Task<Student?> LoginAsync(string matricula, string password);
    Task<Student?> GetStudentByMatriculaAsync(string matricula);
    Task<AcademicRecord?> GetAcademicRecordAsync(int studentId);
    Task<List<CreditPeriod>> GetCreditHistoryAsync(int studentId);
}
