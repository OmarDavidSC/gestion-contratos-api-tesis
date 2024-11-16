using GestionContrato.BLL.Services;
using GestionContrato.BLL.Services.Interfaces;
using GestionContrato.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestionContrato.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SistemaContratacionController : ControllerBase
    {
        private readonly ISistemaContratacionService sistemaContratacionService;

        public SistemaContratacionController(ISistemaContratacionService _sistemaContratacionService)
        {
            sistemaContratacionService = _sistemaContratacionService;
        }

        [HttpGet]
        [Route("obtener-sistema-contrataciones")]
        public async Task<IActionResult> obtenerSistemaContrataciones(string? valorBusqueda)
        {
            try
            {
                var listaSistemaContratacion = await sistemaContratacionService.listar(valorBusqueda);
                return Ok(listaSistemaContratacion);

            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Error: {ex.Message} " });
            }
        }

        [HttpPost]
        [Route("registrar-sistema-contratacion")]
        public async Task<IActionResult> registrar([FromBody] SistemaContratacionDto sistemaContratacion)
        {
            try
            {
                await sistemaContratacionService.crear(sistemaContratacion);
                return Ok(new { message = "Sistema Contratacion Registrada correctamente" });

            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Ha ocurrido un problema {ex.Message}" });
            }
        }

        [HttpPost]
        [Route("editar-sistema-contratacion")]
        public async Task<IActionResult> editar([FromBody] SistemaContratacionDto sistemaContratacion)
        {
            try
            {
                await sistemaContratacionService.editar(sistemaContratacion);
                return Ok(new { message = "Sistema Contratacion editada correctamente" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Ha ocurrido un problema {ex.Message}" });
            }
        }

        [HttpPost]
        [Route("eliminar-sistema-contratacion/{id:guid}")]
        public async Task<IActionResult> eliminar(Guid id)
        {
            try
            {
                await sistemaContratacionService.eliminar(id);

                return Ok(new { message = "Sistema Contratacion Desahabilitada correctamente" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Ha ocurrido un problema {ex.Message}" });
            }
        }

        [HttpGet]
        [Route("obtener-sistema-contratacion/{id:guid}")]
        public async Task<IActionResult> obtenerSistemaContratacionId(Guid id)
        {
            try
            {
                var sistemaContratacion = await sistemaContratacionService.obtenerSistemaContratacionId(id);
                return Ok(sistemaContratacion);

            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Error: {ex.Message} " });
            }
        }
    }
}
