using BlazorServerApp.Application.Interfaces;

namespace BlazorServerApp.Application.UseCases
{
    public class AuthUseCases
    {
        private readonly IAuthRepository _authRepository;

        public AuthUseCases(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        public Task<string> Login(LoginRequest loginRequest)
        {
            return _authRepository.LoginAsync(loginRequest);
        }
    }
}
