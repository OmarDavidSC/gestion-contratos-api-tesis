using GestionContrato.BLL.Services;
using GestionContrato.BLL.Services.Interfaces;
using GestionContrato.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestionContrato.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoGarantiaController : ControllerBase
    {
        private readonly ITipoGarantiaService tipoGarantiaService;

        public TipoGarantiaController(ITipoGarantiaService _tipoGarantiaService)
        {
            tipoGarantiaService = _tipoGarantiaService;
        }

        [HttpGet]
        [Route("obtener-tipo-garantia")]
        public async Task<IActionResult> obtenerTipoGarantias(string? valorBusqueda)
        {
            try
            {
                var listaTipoGantias = await tipoGarantiaService.listar(valorBusqueda);
                return Ok(listaTipoGantias);

            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Error: {ex.Message} " });
            }
        }

        [HttpPost]
        [Route("registrar-tipo-garantia")]
        public async Task<IActionResult> registrar([FromBody] TipoGarantiaDto tipoGarantia)
        {
            try
            {
                await tipoGarantiaService.crear(tipoGarantia);
                return Ok(new { message = "Tipo Garantia registrada correctamente" });

            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Ha ocurrido un problema {ex.Message}" });
            }
        }

        [HttpPost]
        [Route("editar-tipo-garantia")]
        public async Task<IActionResult> editar([FromBody] TipoGarantiaDto tipoGarantia)
        {
            try
            {
                await tipoGarantiaService.editar(tipoGarantia);
                return Ok(new { message = "Tipo Garantia editada correctamente" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Ha ocurrido un problema {ex.Message}" });
            }
        }

        [HttpPost]
        [Route("eliminar-tipo-garantia/{id:guid}")]
        public async Task<IActionResult> eliminar(Guid id)
        {
            try
            {
                await tipoGarantiaService.eliminar(id);

                return Ok(new { message = "Tipo Garantia eliminada correctamente" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Ha ocurrido un problema {ex.Message}" });
            }
        }

        [HttpGet]
        [Route("obtener-tipo-garantia/{id:guid}")]
        public async Task<IActionResult> obtenerTipoGarantiaId(Guid id)
        {
            try
            {
                var tipoGarantia = await tipoGarantiaService.obtenerTipoGarantiaId(id);
                return Ok(tipoGarantia);

            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Error: {ex.Message} " });
            }
        }
    }
}
