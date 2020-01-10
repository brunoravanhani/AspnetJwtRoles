
using AspnetJwtRoles.Models;
using AspnetJwtRoles.Repositories;
using AspnetJwtRoles.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AspnetJwtRoles.Controllers
{
    [Route("v1/account")]
    public class AccountController : ControllerBase
    {
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<dynamic>> Authenticate([FromBody] User model)
        {
            var user = UserRepository.Get(model.Username, model.Password);

            if (user == null)
            {
                return NotFound(new { message = "Usuário ou senha inválidos" });
            }

            var token = TokenService.GenerateToken(user);

            user.Password = "";

            return new
            {
                user,
                token
            };
        }
    }
}
