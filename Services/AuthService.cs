using System.Text.Json;
using Microsoft.JSInterop;
using PaginaPruebaUCSD_V2.Models;

namespace PaginaPruebaUCSD_V2.Services;

public class AuthService : IAuthService, IAsyncDisposable
{
    private const string StorageKey = "ucsd_student_session";
    private readonly IJSRuntime _js;
    private System.Threading.Timer? _timer;

    public Student? CurrentStudent { get; private set; }

    public event Action? OnAuthStateChanged;

    public AuthService(IJSRuntime js) => _js = js;

    public async Task RestoreSessionAsync()
    {
        try
        {
            var json = await _js.InvokeAsync<string?>("sessionStorage.getItem", StorageKey);
            if (string.IsNullOrWhiteSpace(json)) return;

            var wrapper = JsonSerializer.Deserialize<SessionWrapper>(json);
            if (wrapper is null) return;

            if (wrapper.ExpiresUtc <= DateTime.UtcNow)
            {
                await ClearSessionStorageAsync();
                return;
            }

            CurrentStudent = wrapper.Student;
            StartTimer(wrapper.ExpiresUtc - DateTime.UtcNow);
            OnAuthStateChanged?.Invoke();
        }
        catch { /* ignore */ }
    }

    public async Task SetSessionAsync(Student student, TimeSpan duration)
    {
        CurrentStudent = student;
        var wrapper = new SessionWrapper
        {
            Student = student,
            ExpiresUtc = DateTime.UtcNow.Add(duration)
        };

        var json = JsonSerializer.Serialize(wrapper);
        await _js.InvokeVoidAsync("sessionStorage.setItem", StorageKey, json);
        StartTimer(duration);
        OnAuthStateChanged?.Invoke();
    }

    public async Task LogoutAsync()
    {
        CurrentStudent = null;
        _timer?.Dispose();
        _timer = null;
        await ClearSessionStorageAsync();
        OnAuthStateChanged?.Invoke();
    }

    private void StartTimer(TimeSpan duration)
    {
        _timer?.Dispose();
        _timer = new System.Threading.Timer(async _ => await ExpireAsync(), null, duration, Timeout.InfiniteTimeSpan);
    }

    private async Task ExpireAsync()
    {
        CurrentStudent = null;
        await ClearSessionStorageAsync();
        OnAuthStateChanged?.Invoke();
    }

    private ValueTask ClearSessionStorageAsync() => _js.InvokeVoidAsync("sessionStorage.removeItem", StorageKey);

    public async ValueTask DisposeAsync()
    {
        _timer?.Dispose();
        await Task.CompletedTask;
    }

    private class SessionWrapper
    {
        public Student? Student { get; set; }
        public DateTime ExpiresUtc { get; set; }
    }
}
