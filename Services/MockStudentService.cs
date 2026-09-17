using PaginaPruebaUCSD_V2.Models;

namespace PaginaPruebaUCSD_V2.Services;

// ── Mock implementation ───────────────────────────────────────────────────────
// Replace this class with a real SQL implementation when ready.
// Example with EF Core:
//
//   public class SqlStudentService(AppDbContext db) : IStudentService
//   {
//       public async Task<Student?> LoginAsync(string matricula, string password)
//       {
//           var user = await db.Students
//               .FirstOrDefaultAsync(s => s.Matricula == matricula);
//           if (user is null || !BCrypt.Verify(password, user.PasswordHash))
//               return null;
//           return user;
//       }
//       // ... rest of methods
//   }

public class MockStudentService : IStudentService
{
    private static readonly Student _demo = new()
    {
        Id          = 1,
        Matricula   = "1234",
        Nombre      = "Alejandra",
        Apellido    = "Morales Peña",
        Carrera     = "Ingeniería en Sistemas Computacionales",
        Facultad    = "Facultad de Ingeniería",
        AnioIngreso = 2021,
        Estatus     = EstatusEstudiante.Activo,
    };

    public async Task<Student?> LoginAsync(string matricula, string password)
    {
        await Task.Delay(600);
        return matricula == "1234" && password == "1234" ? _demo : null;
    }

    public async Task<Student?> GetStudentByMatriculaAsync(string matricula)
    {
        await Task.Delay(300);
        return matricula == _demo.Matricula ? _demo : null;
    }

    public async Task<AcademicRecord?> GetAcademicRecordAsync(int studentId)
    {
        await Task.Delay(300);
        if (studentId != 1) return null;
        return new AcademicRecord
        {
            StudentId         = 1,
            IndiceAcademico   = 3.72m,
            IndiceAcumulado   = 3.65m,
            CreditosAprobados = 98,
            CreditosTotales   = 160,
            CuatrimestreActual    = 7,
            MateriasCursadas  = 42,
            MateriasAprobadas = 39,
        };
    }

    public async Task<List<CreditPeriod>> GetCreditHistoryAsync(int studentId)
    {
        await Task.Delay(200);
        if (studentId != 1) return [];
        return
        [
            new() { Periodo = "2021-I",  Creditos = 18 },
            new() { Periodo = "2021-II", Creditos = 17 },
            new() { Periodo = "2022-I",  Creditos = 19 },
            new() { Periodo = "2022-II", Creditos = 16 },
            new() { Periodo = "2023-I",  Creditos = 14 },
            new() { Periodo = "2023-II", Creditos = 14 },
        ];
    }
}
