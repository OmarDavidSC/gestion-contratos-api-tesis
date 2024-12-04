using GestionContrato.BLL.Services.Interfaces;
using GestionContrato.DAL.Repositorios;
using GestionContrato.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GestionContrato.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUserLoginService loginService;
        private readonly string secretKey;
        public LoginController(IUserLoginService _loginService, IConfiguration configuration)
        {
            loginService = _loginService;
            secretKey = configuration.GetSection("settings").GetSection("secretKey").ToString();
        }

        [HttpPost]
        [Route("iniciar-sesion")]
        public async Task<FG<object>> IniciarSesion(UserLoginDto usuario)
        {
            try
            {
                var autenticado = await loginService.iniciarSesion(usuario.Correo, usuario.Clave);
                if (autenticado != null)
                {
                    var keyBytes = Encoding.ASCII.GetBytes(secretKey);
                    var claims = new ClaimsIdentity();
                    claims.AddClaim(new Claim(ClaimTypes.Name, usuario.Correo));

                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = claims,
                        Expires = DateTime.UtcNow.AddDays(1),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)
                    };

                    var tokenHandler = new JwtSecurityTokenHandler();
                    var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);
                    string tokenCreado = tokenHandler.WriteToken(tokenConfig);
                    var response = new FG<object>(true, new { token = tokenCreado, Id = autenticado.Id }, "Has Iniciado sesión correctamente");
                    return response;
                }
                else
                {
                    var response = new FG<object>("Usuario o contraseña incorrectos.");
                    return response;
                }
            }
            catch (Exception ex)
            {
                var response = new FG<object>($"{ex.Message}");
                return response;
            }
        }


    }
}
