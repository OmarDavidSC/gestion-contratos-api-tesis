using GestionContrato.BLL.Services;
using GestionContrato.BLL.Services.Interfaces;
using GestionContrato.DAL.Repositorios;
using GestionContrato.Dto;
using Microsoft.AspNetCore.Mvc;

namespace GestionContrato.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PerfilController
    {
        private readonly IPerfilService perfilService;
        private readonly IResetPassword restaurarService;

        public PerfilController(IPerfilService _perfilService, IResetPassword _restaurarService)
        {
            perfilService = _perfilService;
            restaurarService = _restaurarService;
        }

        [HttpGet]
        [Route("show/{id:guid}")]
        public async Task<FG<object>> show(Guid id)
        {
            var response = new FG<object>(false,new { }, "");
            try
            {
                var data = await perfilService.show(id);
                response = new FG<object>(true, data, "Perfil Obtenido con éxito");
                return response;

            }
            catch (Exception ex)
            {
                response = new FG<object>($"{ex.Message}");
                return response;
            }
        }

        [HttpPost]
        [Route("update")]
        public async Task<FG<object>> editarPerfik([FromBody] PerfilDto perfil)
        {
            var response = new FG<object>(false, new { }, "");
            try
            {
               var perfilActualizado =  await perfilService.update(perfil);
               response = new FG<object>(true, perfilActualizado, "Datos del Perfil Actualizado correctamente");
               return response;
            }
            catch (Exception ex)
            {
                response = new FG<object>($"{ex.Message}");
                return response;
            }
        }

        [HttpPost]
        [Route("update-password")]
        public async Task<FG<object>> updatePassword([FromBody] ResetPasswordDto modelo)
        {
            var response = new FG<object>(false, new { }, "");
            try
            {
                var nuevasCredenciales = await restaurarService.update(modelo);
                response = new FG<object>(true, nuevasCredenciales, "Contraseña actualizada correctamente");
                return response;
            }
            catch (Exception ex)
            {
                response = new FG<object>($"{ex.Message}");
                return response;
            }
        }
    }
}
