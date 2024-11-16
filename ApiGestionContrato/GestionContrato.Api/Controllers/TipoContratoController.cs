using GestionContrato.BLL.Services.Interfaces;
using GestionContrato.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestionContrato.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoContratoController : ControllerBase
    {
        private readonly ITipoContratoService tipoContratoService;

        public TipoContratoController(ITipoContratoService _tipoContratoService)
        {
            tipoContratoService = _tipoContratoService;
        }

        [HttpGet]
        [Route("obtener-tipo-contrato")]
        public async Task<IActionResult> obtenerTipoContratos(string? valorBusqueda)
        {
            try
            {
                var listaTipoGantias = await tipoContratoService.listar(valorBusqueda);
                return Ok(listaTipoGantias);

            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Error: {ex.Message} " });
            }
        }

        [HttpPost]
        [Route("registrar-tipo-contrato")]
        public async Task<IActionResult> registrar([FromBody] TipoContratoDto tipoContrato)
        {
            try
            {
                await tipoContratoService.crear(tipoContrato);
                return Ok(new { message = "Tipo Contrato Registrado correctamente" });

            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Ha ocurrido un problema {ex.Message}" });
            }
        }

        [HttpPost]
        [Route("editar-tipo-contrato")]
        public async Task<IActionResult> editar([FromBody] TipoContratoDto tipoContrato)
        {
            try
            {
                await tipoContratoService.editar(tipoContrato);
                return Ok(new { message = "Tipo Contrato editada correctamente" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Ha ocurrido un problema {ex.Message}" });
            }
        }

        [HttpPost]
        [Route("eliminar-tipo-contrato/{id:guid}")]
        public async Task<IActionResult> eliminar(Guid id)
        {
            try
            {
                await tipoContratoService.eliminar(id);

                return Ok(new { message = "Tipo Contrato eliminado correctamente" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Ha ocurrido un problema {ex.Message}" });
            }
        }

        [HttpGet]
        [Route("obtener-tipo-contrato/{id:guid}")]
        public async Task<IActionResult> obtenerTipoContratoId(Guid id)
        {
            try
            {
                var tipoContrato = await tipoContratoService.obtenerTipoContratoId(id);
                return Ok(tipoContrato);

            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Error: {ex.Message} " });
            }
        }
    }
}
