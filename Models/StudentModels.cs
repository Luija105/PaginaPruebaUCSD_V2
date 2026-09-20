namespace PaginaPruebaUCSD_V2.Models;

// ── Domain models ─────────────────────────────────────────────────────────────
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

// ── Modelos de Consultas Académicas (Agregados para resolver los errores CS0246) ──

public class Asignatura
{
    public string Clave { get; set; } = "";
    public string Nombre { get; set; } = "";
    public int Creditos { get; set; }
    public string Aula { get; set; } = "";
    public string Horario { get; set; } = "";
    public string Profesor { get; set; } = "";
}

public class Calificacion
{
    public string Clave { get; set; } = "";
    public string Asignatura { get; set; } = "";
    public int Creditos { get; set; }
    public decimal NotaAcumulada { get; set; }
    public string Literal { get; set; } = "";
}

public class CalificacionHistorial
{
    public string Periodo { get; set; } = "";
    public string Clave { get; set; } = "";
    public string Asignatura { get; set; } = "";
    public int Creditos { get; set; }
    public decimal Nota { get; set; }
    public string Literal { get; set; } = "";
    public string Estado { get; set; } = "";
}

public class AsignaturaFaltante
{
    public string Clave { get; set; } = "";
    public string Nombre { get; set; } = "";
    public int Creditos { get; set; }
    public string Prerrequisitos { get; set; } = "";
    public int CuatrimestreSugerido { get; set; }
}

public class CalificacionPeriodo
{
    public string Periodo { get; set; } = "";
    public decimal IndicePeriodo { get; set; }
    public int CreditosCursados { get; set; }
    public List<CalificacionHistorial> Asignaturas { get; set; } = new();
}