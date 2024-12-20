using SEP3_T1_BlazorUI.Models;
using SEP3T1BlazorUI.Infrastructure;

namespace SEP3_T1_BlazorUI.Application.Interfaces
{
    public interface IAuthRepository
    {
        Task<string> LoginAsync(LoginRequest loginRequest);
    }
}
