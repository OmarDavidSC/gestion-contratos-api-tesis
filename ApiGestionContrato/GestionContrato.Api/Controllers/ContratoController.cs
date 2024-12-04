using GestionContrato.BLL.Services;
using GestionContrato.BLL.Services.Interfaces;
using GestionContrato.DAL.Repositorios;
using GestionContrato.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestionContrato.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContratoController : ControllerBase
    {
        private readonly IContratoService contratoService;

        public ContratoController(IContratoService _contratoService)
        {
            contratoService = _contratoService;
        }

        [HttpPost]
        [Route("obtener-contrato-bandeja")]
        public async Task<IActionResult> listar(FiltroBandejaDto? filtroBandeja)
        {
            try
            {
                var contratos = await contratoService.listaBandeja(filtroBandeja);
                return Ok(contratos);

            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Ha ocurrido un problema {ex.Message}" });
            }
        }

        [HttpPost]
        [Route("registrar-contrato")]
        public async Task<IActionResult> crear([FromBody] ContratoDto contrato)
        {
            try
            {
                await contratoService.registrarContrato(contrato);
                return Ok(new { message = "Contrato registrado" });

            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Ha ocurrido un problema {ex.Message}" });
            }
        }

        [HttpGet]
        [Route("obtener-contrato/{id:guid}")]
        public async Task<IActionResult> getIdContrado(Guid id)
        {
            try
            {
                var contrato = await contratoService.getIdContrato(id);
                return Ok(contrato);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Error: {ex.Message} " });
            }
        }

        [HttpPost]
        [Route("editar-contrato")]
        public async Task<IActionResult> editarContrato([FromBody] ContratoDto contrato)
        {
            try
            {
                await contratoService.editarContraro(contrato);
                return Ok(new { message = "Contrato editado correctamente" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Ha ocurrido un problema {ex.Message}" });
            }
        }

        [HttpPost]
        [Route("guardar-contrato")]
        public async Task<IActionResult> guardarContrato([FromBody] ContratoDto contrato)
        {
            try
            {
                await contratoService.guardarContrato(contrato);
                return Ok(new { message = "Contrato guardado correctamente" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Ha ocurrido un problema {ex.Message}" });
            }
        }

        [HttpPost]
        [Route("derivar-contrato")]
        public async Task<IActionResult> derivarContrato([FromBody] AsigarUsuarioAprobadorDto modelo)
        {
            try
            {
                await contratoService.derivarContrato(modelo);
                return Ok(new { message = "Contrato derivado correctamente" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Ha ocurrido un problema {ex.Message}" });
            }
        }

        [HttpPost]
        [Route("accion-contrato")]
        public async Task<IActionResult> accionContrato([FromBody] AccionContratoDto modelo)
        {
            try
            {
                await contratoService.accionContrato(modelo);
                return Ok(new { message = "Contrato derivado correctamente" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Ha ocurrido un problema {ex.Message}" });
            }
        }

        [HttpGet]
        [Route("notifications")]
        public async Task<FG<object>> obtenerNotificaciones()
        {
            var response = new FG<object>(false, new { }, "");
            try
            {
                var data = await contratoService.ObtenerNotificacionesContratos();
                response = new FG<object>(true, data, "Notificaciones");
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
