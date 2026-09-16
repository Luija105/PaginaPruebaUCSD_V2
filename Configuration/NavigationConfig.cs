namespace BlazorStudentPortal.Configuration;

// ── Navigation menu configuration ─────────────────────────────────────────────
// Add, remove, or reorder NavSection entries to modify the top navigation.
// No component code needs to change — only this file.

public record NavSubItem(string Label, string Href);
public record NavSection(string Id, string Label, List<NavSubItem> Items);

public static class NavigationConfig
{
    public static readonly List<NavSection> Sections =
    [
        new("aula Virtual", "Aula Virtual",
        [
            new("Aula virtual Grado",        "https://virtual.ucsd.edu.do/login/index.php"),
            new("Aula virtual Postgrado",          "https://ed.ucsd.edu.do/postgrado/login/index.php?loginredirect=1"),
 
        ]),
        new("consultas", "Consultas",
        [
            new("Academicas",      "/consultasacademicas"),
            new("Economicas",    "/consultaseconomicas"),
            new("Estado de Tesis", "/EstadoTesis"),
            new("Reglamento de Calificaciones","/ReglasCalificaciones"),
            new("Retiros", "#retiros"),
        ]),
        new("manuales y documentos", "Manuales y Documentos",
        [
            new("Instructivo de Posgrado",  "/instructivoposgrado"),
            new("Manual de Inscripcion Tema Anteproyecto",    "/manualinscripcionanteproyecto"),
            new("Manual de Reajuste",    "/manualdereajuste"),
            new("Manual Operativo de Virtualidad",       "/manualoperativovirtualidad"),
        ]),
        new("pagos", "Pagos",
        [
            new("Fecha de Pagos",          "/fechapago"),
            new("Otros Pagos",             "/otrospagos"),
            new("Pago de Inscripcion", "/pagoinscripcion"),
        ]),
         new("procesos", "Procesos",
        [
            new("Actualizar Datos de Acceso",          "/consejeria"),
            new("Adicion de Asignaturas",             "/salud"),
            new("Evaluación Universitaria", "/actividades"),
            new("Reajuste de selección",       "/practicas"),
            new("Retiro de Asignaturas",       "/retiroasignaturas"),
            new("Seleccion de Asignaturas",       "/seleccionasignaturas"),
        ]),
          new("solicitudes", "Solicitudes",
        [
            new("Solicitud de Documentos",          "/solicituddocumentos"),
        ]),
            new("trabajo de grado", "Trabajo de Grado",
        [
            new("Consulta de Estatus de Solicitud",   "/consultaestatussolicitud"),
            new("Inscripción de Tema de Anteproyecto",    "/inscripciontemaanteproyecto"),
            new("Validar Encuentros", "/validarencuentros"),
        ]),
    ];
}
