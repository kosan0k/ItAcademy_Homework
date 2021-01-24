using System.Threading.Tasks;
using It_AcademyHomework.Authentication.Requests;
using It_AcademyHomework.Authentication.Responses;

namespace It_AcademyHomework.Authentication
{
    public interface IAuthenticationService
    {
        Task<LoginResponse> Login(LoginRequest loginRequest);
    }
}
