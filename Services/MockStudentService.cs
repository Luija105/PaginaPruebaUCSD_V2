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
        Id = 1,
        Matricula = "1234",
        Nombre = "Alejandra",
        Apellido = "Morales Peña",
        Carrera = "Ingeniería en Sistemas Computacionales",
        Facultad = "Facultad de Ingeniería",
        AnioIngreso = 2021,
        Estatus = EstatusEstudiante.Activo,
    };

    // ── Autenticación ─────────────────────────────────────────────────────────
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

    // ── Dashboard ─────────────────────────────────────────────────────────────
    public async Task<AcademicRecord?> GetAcademicRecordAsync(int studentId)
    {
        await Task.Delay(300);
        if (studentId != 1) return null;
        return new AcademicRecord
        {
            StudentId = 1,
            IndiceAcademico = 3.72m,
            IndiceAcumulado = 3.65m,
            CreditosAprobados = 98,
            CreditosTotales = 160,
            CuatrimestreActual = 7,
            MateriasCursadas = 42,
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

    // ── Consultas Académicas ──────────────────────────────────────────────────
    public async Task<List<Asignatura>> GetSeleccionActualAsync(int studentId)
    {
        await Task.Delay(300);
        if (studentId != 1) return [];
        return
        [
            new() { Codigo = "BIO500", Descripcion = "SER HUMANO Y NATURALEZA",         Creditos = 3, Seccion = "207", Jueves  = "19:25/21:40 (VRTUAL)", Estado = "ABIERTA" },
            new() { Codigo = "FIL320", Descripcion = "METODOLOGIA DE LA INVESTIGACION", Creditos = 3, Seccion = "203", Lunes   = "19:25/21:50 (VRTUAL)", Estado = "ABIERTA" },
            new() { Codigo = "ISC132", Descripcion = "LENGUAJE DE PROGRAMACION",        Creditos = 3, Seccion = "200", Viernes = "17:45/20:10 (EPCLA1)", Estado = "ABIERTA" },
            new() { Codigo = "ISC136", Descripcion = "BASE DE DATOS I",                 Creditos = 4, Seccion = "200", Martes  = "17:45/19:20 (VRTUAL)", Jueves = "16:05/17:45 (VRTUAL)", Estado = "ABIERTA" },
        ];
    }

    public async Task<List<Calificacion>> GetCalificacionesActualesAsync(int studentId)
    {
        await Task.Delay(300);
        if (studentId != 1) return [];
        return
        [
            new() { Codigo = "BIO500", Descripcion = "SER HUMANO Y NATURALEZA",         Creditos = 3, Parcial1 = 85, Estado = "EN CURSO" },
            new() { Codigo = "FIL320", Descripcion = "METODOLOGIA DE LA INVESTIGACION", Creditos = 3, Parcial1 = 78, Estado = "EN CURSO" },
            new() { Codigo = "ISC132", Descripcion = "LENGUAJE DE PROGRAMACION",        Creditos = 3, Parcial1 = 90, Estado = "EN CURSO" },
            new() { Codigo = "ISC136", Descripcion = "BASE DE DATOS I",                 Creditos = 4, Parcial1 = 88, Estado = "EN CURSO" },
        ];
    }

    public async Task<List<CalificacionHistorial>> GetHistorialCalificacionesAsync(int studentId)
    {
        await Task.Delay(300);
        if (studentId != 1) return [];
        return
        [
            new() { Periodo = "20211", Codigo = "MAT101", Descripcion = "CALCULO I",              Creditos = 4, Nota = 85, Estado = "APROBADA" },
            new() { Periodo = "20211", Codigo = "FIS101", Descripcion = "FISICA I",               Creditos = 4, Nota = 78, Estado = "APROBADA" },
            new() { Periodo = "20211", Codigo = "INF101", Descripcion = "INTRODUCCION A LA INF.", Creditos = 3, Nota = 92, Estado = "APROBADA" },
            new() { Periodo = "20212", Codigo = "MAT102", Descripcion = "CALCULO II",             Creditos = 4, Nota = 82, Estado = "APROBADA" },
            new() { Periodo = "20212", Codigo = "INF102", Descripcion = "PROGRAMACION I",         Creditos = 3, Nota = 95, Estado = "APROBADA" },
            new() { Periodo = "20221", Codigo = "ISC101", Descripcion = "PROGRAMACION II",        Creditos = 3, Nota = 91, Estado = "APROBADA" },
            new() { Periodo = "20221", Codigo = "ISC201", Descripcion = "ESTRUCTURA DE DATOS",    Creditos = 3, Nota = 88, Estado = "APROBADA" },
            new() { Periodo = "20222", Codigo = "ISC202", Descripcion = "ALGORITMOS",             Creditos = 3, Nota = 84, Estado = "APROBADA" },
            new() { Periodo = "20222", Codigo = "ISC203", Descripcion = "SISTEMAS OPERATIVOS",    Creditos = 3, Nota = 79, Estado = "APROBADA" },
            new() { Periodo = "20231", Codigo = "ISC301", Descripcion = "REDES DE COMPUTADORAS",  Creditos = 3, Nota = 90, Estado = "APROBADA" },
            new() { Periodo = "20231", Codigo = "ISC302", Descripcion = "ING. DE SOFTWARE I",     Creditos = 3, Nota = 83, Estado = "APROBADA" },
            new() { Periodo = "20232", Codigo = "ISC401", Descripcion = "ING. DE SOFTWARE II",    Creditos = 3, Nota = 87, Estado = "APROBADA" },
            new() { Periodo = "20232", Codigo = "ISC402", Descripcion = "ARQUITECTURA DE COMP.",  Creditos = 3, Nota = 81, Estado = "APROBADA" },
        ];
    }

    public async Task<List<AsignaturaFaltante>> GetAsignaturasFaltantesAsync(int studentId)
    {
        await Task.Delay(300);
        if (studentId != 1) return [];
        return
        [
            new() { Codigo = "ISC500", Descripcion = "PROYECTO I",              Creditos = 3, Prerequisito = "ISC402" },
            new() { Codigo = "ISC501", Descripcion = "PROYECTO II",             Creditos = 3, Prerequisito = "ISC500" },
            new() { Codigo = "ISC450", Descripcion = "SEGURIDAD INFORMATICA",   Creditos = 3, Prerequisito = "ISC301" },
            new() { Codigo = "ISC460", Descripcion = "INTELIGENCIA ARTIFICIAL", Creditos = 3, Prerequisito = "MAT401" },
            new() { Codigo = "ISC470", Descripcion = "DESARROLLO WEB AVANZADO", Creditos = 3, Prerequisito = "ISC132" },
            new() { Codigo = "ISC480", Descripcion = "GESTION DE PROYECTOS TI", Creditos = 3, Prerequisito = ""       },
            new() { Codigo = "HUM101", Descripcion = "ETICA PROFESIONAL",       Creditos = 2, Prerequisito = ""       },
        ];
    }

    public async Task<List<CalificacionPeriodo>> GetRecordPorPeriodoAsync(int studentId)
    {
        await Task.Delay(300);
        if (studentId != 1) return [];

        var historial = await GetHistorialCalificacionesAsync(studentId);
        return historial
            .GroupBy(h => h.Periodo)
            .OrderBy(g => g.Key)
            .Select(g => new CalificacionPeriodo
            {
                Periodo = g.Key,
                CreditosCursados = g.Sum(a => a.Creditos),
                CreditosAprobados = g.Where(a => a.Estado == "APROBADA").Sum(a => a.Creditos),
                IndiceDelPeriodo = g.Any() ? Math.Round(g.Average(a => a.Nota) / 25m, 2) : 0,
                Asignaturas = g.ToList()
            })
            .ToList();
    }
}