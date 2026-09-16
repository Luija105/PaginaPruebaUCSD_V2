using PaginaPruebaUCSD_V2.Models;

namespace PaginaPruebaUCSD_V2.Services;

public interface IAuthService
{
    Student? CurrentStudent { get; }
    Task RestoreSessionAsync();
    Task SetSessionAsync(Student student, TimeSpan duration);
    Task LogoutAsync();
    event Action? OnAuthStateChanged;
}
