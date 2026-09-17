namespace PaginaPruebaUCSD_V2.Models;

// ── Domain models ─────────────────────────────────────────────────────────────
// These classes mirror the SQL schema. Map them 1-to-1 with EF Core entities
// or Dapper query results when switching from the mock service.

public enum EstatusEstudiante { Activo, Inactivo, Egresado, Suspendido }

public class Student
{
    public int Id { get; set; }
    public string Matricula { get; set; } = "";
    public string Nombre { get; set; } = "";
    public string Apellido { get; set; } = "";
    public string NombreCompleto => $"{Nombre} {Apellido}";
    public string Carrera { get; set; } = "";
    public string Facultad { get; set; } = "";
    public int AnioIngreso { get; set; }
    public EstatusEstudiante Estatus { get; set; }
    public string Iniciales => $"{Nombre[0]}{Apellido[0]}";
}

public class AcademicRecord
{
    public int StudentId { get; set; }
    public decimal IndiceAcademico { get; set; }
    public decimal IndiceAcumulado { get; set; }
    public int CreditosAprobados { get; set; }
    public int CreditosTotales { get; set; }
    public int CuatrimestreActual { get; set; }
    public int MateriasCursadas { get; set; }
    public int MateriasAprobadas { get; set; }
    public int CreditosRestantes => CreditosTotales - CreditosAprobados;
    public int PorcentajeCreditosInt => (int)Math.Round((decimal)CreditosAprobados / CreditosTotales * 100);
    public decimal TasaAprobacion => MateriasCursadas > 0
        ? Math.Round((decimal)MateriasAprobadas / MateriasCursadas * 100, 1)
        : 0;
}

public class CreditPeriod
{
    public string Periodo { get; set; } = "";
    public int Creditos { get; set; }
}

public class AuthCredentials
{
    public string Matricula { get; set; } = "";
    public string Password { get; set; } = "";
}
