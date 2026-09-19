using PaginaPruebaUCSD_V2.Models;

namespace PaginaPruebaUCSD_V2.Services
{
    public interface IScheduleService
    {
        Task<List<HorarioModel>> GetStudentScheduleAsync();
    }

    public class MockScheduleService : IScheduleService
    {
        public async Task<List<HorarioModel>> GetStudentScheduleAsync()
        {
            // Simula un pequeño retraso de red como si fuera una BD real
            await Task.Delay(300);

            return new List<HorarioModel>
            {
                new HorarioModel
                {
                    Id = 1,
                    SubjectCode = "ISO-500",
                    SubjectName = "Proyecto I",
                    Classroom = "Lab 3 - Edificio A",
                    Professor = "Ing. Rosario",
                    Day = DayOfWeek.Monday,
                    StartTime = new TimeSpan(18, 0, 0),
                    EndTime = new TimeSpan(20, 15, 0),
                    ColorCategory = "navy"
                },
                new HorarioModel
                {
                    Id = 2,
                    SubjectCode = "BAD-201",
                    SubjectName = "Bases de Datos I",
                    Classroom = "Aula 204",
                    Professor = "Lic. Martínez",
                    Day = DayOfWeek.Wednesday,
                    StartTime = new TimeSpan(18, 0, 0),
                    EndTime = new TimeSpan(20, 15, 0),
                    ColorCategory = "amber"
                },
                new HorarioModel
                {
                    Id = 3,
                    SubjectCode = "INF-310",
                    SubjectName = "Sistemas Operativos",
                    Classroom = "Lab 1",
                    Professor = "Ing. Gómez",
                    Day = DayOfWeek.Friday,
                    StartTime = new TimeSpan(20, 15, 0),
                    EndTime = new TimeSpan(22, 0, 0),
                    ColorCategory = "dark"
                }
            };
        }
    }
}