using System;

namespace PaginaPruebaUCSD_V2.Models
{
    public class HorarioModel
    {
        // Identificador
        public int Id { get; set; }

        // Propiedades principales
        public string SubjectCode { get; set; } = string.Empty;
        public string SubjectName { get; set; } = string.Empty;

        // Alias corregidos con get/set explícitos
        public string Code
        {
            get => SubjectCode;
            set => SubjectCode = value;
        }

        public string Description
        {
            get => SubjectName;
            set => SubjectName = value;
        }

        // Detalles académicos
        public int Credits { get; set; }
        public string Section { get; set; } = string.Empty;
        public string Status { get; set; } = "ABIERTA";
        public string Classroom { get; set; } = string.Empty;
        public string Professor { get; set; } = string.Empty;

        // Fecha y Hora
        public DayOfWeek Day { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string ColorCategory { get; set; } = "blue";

        // Columnas semanales para la vista de tabla
        public string Lunes { get; set; } = string.Empty;
        public string Martes { get; set; } = string.Empty;
        public string Miercoles { get; set; } = string.Empty;
        public string Jueves { get; set; } = string.Empty;
        public string Viernes { get; set; } = string.Empty;
        public string Sabado { get; set; } = string.Empty;
    }
}