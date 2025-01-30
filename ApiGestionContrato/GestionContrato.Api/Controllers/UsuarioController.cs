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
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService usuarioService;

        public UsuarioController(IUsuarioService _usuarioService)
        {
            usuarioService = _usuarioService;
        }

        [HttpGet]
        [Route("obtener-usuarios")]
        public async Task<IActionResult> obtenerUsuarios(string? valorBusqueda)
        {
            try
            {
                var listaUsuarios = await usuarioService.lista(valorBusqueda);
                return Ok(listaUsuarios);

            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Error: {ex.Message} " });
            }
        }

        [HttpPost]
        [Route("registrar-usuario")]
        public async Task<IActionResult> registrar([FromBody] UsuarioDto usuario)
        {
            try
            {
                await usuarioService.crear(usuario);
                return Ok(new { message = "Usuario Registrado correctamente" });

            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Ha ocurrido un problema {ex.Message}" });
            }
        }

        [HttpPost]
        [Route("editar-usuario")]
        public async Task<IActionResult> editar([FromBody] UsuarioDto usuario)
        {
            try
            {
                await usuarioService.editar(usuario);
                return Ok(new { message = "Usuario editado correctamente" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Ha ocurrido un problema {ex.Message}" });
            }
        }

        [HttpPost]
        [Route("eliminar-usuario/{id:guid}")]
        public async Task<IActionResult> eliminar(Guid id)
        {
            try
            {
                await usuarioService.eliminar(id);

                return Ok(new { message = "Usuario Desahabilitado correctamente" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Ha ocurrido un problema {ex.Message}" });
            }

        }

        //Metodo Obtener usuario porId
        [HttpGet]
        [Route("obtener-usuario/{id:guid}")]
        public async Task<IActionResult> obtenerUsuario(Guid id)
        {
            try
            {
                var usuario = await usuarioService.obtenerUsuarioId(id);
                return Ok(usuario);

            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Error: {ex.Message} " });
            }
        }

        // Metodo buscar usuario, por culumna nombre completo y correo (coincidencia)
        [HttpGet]
        [Route("buscar-usuarios")]
        public async Task<IActionResult> BuscarUsuarios([FromQuery] string criterio)
        {
            try
            {
                var usuarios = await usuarioService.buscarUsuarios(criterio);
                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Error: {ex.Message}" });
            }
        }

        [HttpPost]
        [Route("vemail")]
        public async Task<FG<object>> validarEmail([FromBody] RestablecerContrasenaDto modelo)
        {
            try
            {
                var usuario = await usuarioService.validarUsuarioEmail(modelo.Email);
                if(usuario != null)
                {
                    var response = new FG<object>(true, usuario , "Correo Electronico validado con éxito");
                    return response;
                } else
                {
                    var response = new FG<object>(false, new {  }, "El Usuario no esta registrado en la compañia");
                    return response;
                }
            }
            catch (Exception ex)
            {
                var response = new FG<object>($"{ex.Message}");
                return response;
            }
        }

        [HttpPost]
        [Route("restablecer-contrasena")]
        public async Task<FG<object>> restablecerContrasena([FromBody] RestablecerContrasenaDto modelo)
        {
            try
            {
                var usuario = await usuarioService.restablcerContrseana(modelo.Email, modelo.NuevaContrasena);
                if (usuario)
                {
                    var response = new FG<object>(true, new {  }, "Credenciales actualizadas correctamente. Inicie sesión con su nueva Contraseña");
                    return response;
                }
                else
                {
                    var response = new FG<object>(false, new { }, "Error al actualizar las crendenciales");
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

