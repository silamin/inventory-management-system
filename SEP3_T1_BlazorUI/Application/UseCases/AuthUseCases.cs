using SEP3_T1_BlazorUI.Models;
using SEP3_T1_BlazorUI.Application.Interfaces;
using SEP3T1BlazorUI.Infrastructure;

namespace SEP3_T1_BlazorUI.Application.UseCases
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
