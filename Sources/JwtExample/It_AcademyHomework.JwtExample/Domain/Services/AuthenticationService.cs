using System.Linq;
using System.Threading.Tasks;
using It_AcademyHomework.Authentication;
using It_AcademyHomework.Authentication.Helpers;
using It_AcademyHomework.Authentication.Requests;
using It_AcademyHomework.Authentication.Responses;
using It_AcademyHomework.JwtExample.Domain.Helpers;
using It_AcademyHomework.JwtExample.Repository;

namespace It_AcademyHomework.JwtExample.Domain.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly CustomersDbContext customersDbContext;
        public AuthenticationService(CustomersDbContext customersDbContext)
        {
            this.customersDbContext = customersDbContext;
        }
        public async Task<LoginResponse> Login(LoginRequest loginRequest)
        {
            LoginResponse result = null;

            var customer = customersDbContext.Customers.SingleOrDefault(customer => customer.Active && customer.Username == loginRequest.Username);
            if (customer != null)
            {
                var passwordHash = HashingHelper.HashUsingPbkdf2(loginRequest.Password, customer.PasswordSalt);
                if (customer.Password == passwordHash)
                {
                    var token = await Task.Run(() => TokenHelper.GenerateToken(customer));
                    result = new LoginResponse { Username = customer.Username, FirstName = customer.FirstName, LastName = customer.LastName, Token = token };
                }
            }

            return result;
        }
    }
}
