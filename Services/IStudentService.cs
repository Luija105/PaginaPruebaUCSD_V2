using PaginaPruebaUCSD_V2.Models;

namespace PaginaPruebaUCSD_V2.Services;

// ── Service contract ──────────────────────────────────────────────────────────
// Implement this interface against any data source (SQL Server via EF Core,
// PostgreSQL via Npgsql, a REST API via HttpClient, etc.) and register the
// new class in Program.cs — no component code changes required.


public interface IStudentService
{
    // ── Autenticación ─────────────────────────────────────────────────────────
    Task<Student?> LoginAsync(string matricula, string password);
    Task<Student?> GetStudentByMatriculaAsync(string matricula);

    // ── Dashboard ─────────────────────────────────────────────────────────────
    Task<AcademicRecord?> GetAcademicRecordAsync(int studentId);
    Task<List<CreditPeriod>> GetCreditHistoryAsync(int studentId);

    // ── Consultas Académicas ──────────────────────────────────────────────────
    Task<List<Asignatura>> GetSeleccionActualAsync(int studentId);
    Task<List<Calificacion>> GetCalificacionesActualesAsync(int studentId);
    Task<List<CalificacionHistorial>> GetHistorialCalificacionesAsync(int studentId);
    Task<List<AsignaturaFaltante>> GetAsignaturasFaltantesAsync(int studentId);
    Task<List<CalificacionPeriodo>> GetRecordPorPeriodoAsync(int studentId);
}