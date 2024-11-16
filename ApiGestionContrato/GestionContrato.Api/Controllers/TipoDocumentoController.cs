using GestionContrato.BLL.Services.Interfaces;
using GestionContrato.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestionContrato.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoDocumentoController : ControllerBase
    {
        private readonly ITipoDocumentoService tipoDocumentoService;

        public TipoDocumentoController(ITipoDocumentoService _tipoDocumentoService)
        {
            tipoDocumentoService = _tipoDocumentoService;
        }

        [HttpGet]
        [Route("obtener-tipo-documento")]
        public async Task<IActionResult> obtenerTipoDocumentos(string? valorBusqueda)
        {
            try
            {
                var listaTipoGantias = await tipoDocumentoService.listar(valorBusqueda);
                return Ok(listaTipoGantias);

            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Error: {ex.Message} " });
            }
        }

        [HttpPost]
        [Route("registrar-tipo-documento")]
        public async Task<IActionResult> registrar([FromBody] TipoDocumentoDto tipoDocumento)
        {
            try
            {
                await tipoDocumentoService.crear(tipoDocumento);
                return Ok(new { message = "Tipo Documento Registrada correctamente" });

            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Ha ocurrido un problema {ex.Message}" });
            }
        }

        [HttpPost]
        [Route("editar-tipo-documento")]
        public async Task<IActionResult> editar([FromBody] TipoDocumentoDto tipoDocumento)
        {
            try
            {
                await tipoDocumentoService.editar(tipoDocumento);
                return Ok(new { message = "Tipo Documento editada correctamente" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Ha ocurrido un problema {ex.Message}" });
            }
        }

        [HttpPost]
        [Route("eliminar-tipo-documento/{id:guid}")]
        public async Task<IActionResult> eliminar(Guid id)
        {
            try
            {
                await tipoDocumentoService.eliminar(id);

                return Ok(new { message = "Tipo Documento eliminada correctamente" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Ha ocurrido un problema {ex.Message}" });
            }
        }

        [HttpGet]
        [Route("obtener-tipo-documento/{id:guid}")]
        public async Task<IActionResult> obtenerTipoDocumentoId(Guid id)
        {
            try
            {
                var tipoDocumento = await tipoDocumentoService.obtenerTipoDocumentoId(id);
                return Ok(tipoDocumento);

            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Error: {ex.Message} " });
            }
        }
    }
}
