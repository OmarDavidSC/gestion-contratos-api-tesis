using GestionContrato.BLL.Services.Interfaces;
using GestionContrato.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestionContrato.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoPolizaController : ControllerBase
    {
        private readonly ITipoPolizaService tipoPolizaService;

        public TipoPolizaController(ITipoPolizaService _tipoPolizaService)
        {
            tipoPolizaService = _tipoPolizaService;
        }

        [HttpGet]
        [Route("obtener-tipo-poliza")]
        public async Task<IActionResult> obtenerTipoPolizas(string? valorBusqueda)
        {
            try
            {
                var listaTipoGantias = await tipoPolizaService.listar(valorBusqueda);
                return Ok(listaTipoGantias);

            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Error: {ex.Message} " });
            }
        }

        [HttpPost]
        [Route("registrar-tipo-poliza")]
        public async Task<IActionResult> registrar([FromBody] TipoPolizaDto tipoPoliza)
        {
            try
            {
                await tipoPolizaService.crear(tipoPoliza);
                return Ok(new { message = "Tipo Poliza registrada correctamente" });

            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Ha ocurrido un problema {ex.Message}" });
            }
        }

        [HttpPost]
        [Route("editar-tipo-poliza")]
        public async Task<IActionResult> editar([FromBody] TipoPolizaDto tipoPoliza)
        {
            try
            {
                await tipoPolizaService.editar(tipoPoliza);
                return Ok(new { message = "Tipo Poliza editada correctamente" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Ha ocurrido un problema {ex.Message}" });
            }
        }

        [HttpPost]
        [Route("eliminar-tipo-poliza/{id:guid}")]
        public async Task<IActionResult> eliminar(Guid id)
        {
            try
            {
                await tipoPolizaService.eliminar(id);

                return Ok(new { message = "Tipo Poliza eliminada correctamente" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Ha ocurrido un problema {ex.Message}" });
            }
        }

        [HttpGet]
        [Route("obtener-tipo-poliza/{id:guid}")]
        public async Task<IActionResult> obtenerTipoPolizaId(Guid id)
        {
            try
            {
                var tipoPoliza = await tipoPolizaService.obtenerTipoPolizaId(id);
                return Ok(tipoPoliza);

            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Error: {ex.Message} " });
            }
        }
    }
}
