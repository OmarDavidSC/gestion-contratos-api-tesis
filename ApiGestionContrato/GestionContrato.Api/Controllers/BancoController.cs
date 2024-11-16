using GestionContrato.BLL.Services.Interfaces;
using GestionContrato.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestionContrato.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BancoController : ControllerBase
    {
        private readonly IBancoService bancoService;

        public BancoController(IBancoService _bancoService)
        {
            bancoService = _bancoService;
        }

        [HttpGet]
        [Route("obtener-bancos")]
        public async Task<IActionResult> obtenerBancos(string? valorBusqueda)
        {
            try
            {
                var listaBancos = await bancoService.listar(valorBusqueda);
                return Ok(listaBancos);

            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Error: {ex.Message} " });
            }
        }

        [HttpPost]
        [Route("registrar-banco")]
        public async Task<IActionResult> registrar([FromBody] BancoDto banco)
        {
            try
            {
                await bancoService.crear(banco);
                return Ok(new { message = "Banco Registrada correctamente" });

            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Ha ocurrido un problema {ex.Message}" });
            }
        }

        [HttpPost]
        [Route("editar-banco")]
        public async Task<IActionResult> editar([FromBody] BancoDto banco)
        {
            try
            {
                await bancoService.editar(banco);
                return Ok(new { message = "Banco editada correctamente" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Ha ocurrido un problema {ex.Message}" });
            }
        }

        [HttpPost]
        [Route("eliminar-banco/{id:guid}")]
        public async Task<IActionResult> eliminar(Guid id)
        {
            try
            {
                await bancoService.eliminar(id);

                return Ok(new { message = "Banco Desahabilitada correctamente" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Ha ocurrido un problema {ex.Message}" });
            }

        }

        [HttpGet]
        [Route("obtener-banco/{id:guid}")]
        public async Task<IActionResult> obtenerBancoId(Guid id)
        {
            try
            {
                var banco = await bancoService.obtenerBancoId(id);
                return Ok(banco);

            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Error: {ex.Message} " });
            }
        }
    }
}
