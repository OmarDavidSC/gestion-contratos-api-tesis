using GestionContrato.BLL.Services.Interfaces;
using GestionContrato.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestionContrato.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoAdendaController : ControllerBase
    {
        private readonly ITipoAdendaService tipoAdendaService;

        public TipoAdendaController(ITipoAdendaService _tipoAdendaService)
        {
            tipoAdendaService = _tipoAdendaService;
        }

        [HttpGet]
        [Route("obtener-tipo-adenda")]
        public async Task<IActionResult> obtenerTipoAdendas(string? valorBusqueda)
        {
            try
            {
                var listaTipoGantias = await tipoAdendaService.listar(valorBusqueda);
                return Ok(listaTipoGantias);

            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Error: {ex.Message} " });
            }
        }

        [HttpPost]
        [Route("registrar-tipo-adenda")]
        public async Task<IActionResult> registrar([FromBody] TipoAdendaDto tipoAdenda)
        {
            try
            {
                await tipoAdendaService.crear(tipoAdenda);
                return Ok(new { message = "Tipo Adenda Registrada correctamente" });

            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Ha ocurrido un problema {ex.Message}" });
            }
        }

        [HttpPost]
        [Route("editar-tipo-adenda")]
        public async Task<IActionResult> editar([FromBody] TipoAdendaDto tipoAdenda)
        {
            try
            {
                await tipoAdendaService.editar(tipoAdenda);
                return Ok(new { message = "Tipo Adenda editada correctamente" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Ha ocurrido un problema {ex.Message}" });
            }
        }

        [HttpPost]
        [Route("eliminar-tipo-adenda/{id:guid}")]
        public async Task<IActionResult> eliminar(Guid id)
        {
            try
            {
                await tipoAdendaService.eliminar(id);

                return Ok(new { message = "Tipo Adenda eliminada correctamente" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Ha ocurrido un problema {ex.Message}" });
            }
        }

        [HttpGet]
        [Route("obtener-tipo-adenda/{id:guid}")]
        public async Task<IActionResult> obtenerTipoAdendaId(Guid id)
        {
            try
            {
                var tipoAdenda = await tipoAdendaService.obtenerTipoAdendaId(id);
                return Ok(tipoAdenda);

            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Error: {ex.Message} " });
            }
        }
    }
}
