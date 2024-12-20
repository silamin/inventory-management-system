namespace BlazorServerApp.Application.Interfaces
{
    public interface IAuthRepository
    {
        Task<string> LoginAsync(LoginRequest loginRequest);
    }
}
