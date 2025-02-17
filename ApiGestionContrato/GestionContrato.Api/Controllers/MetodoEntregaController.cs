using GestionContrato.BLL.Services;
using GestionContrato.BLL.Services.Interfaces;
using GestionContrato.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestionContrato.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MetodoEntregaController : ControllerBase
    {
        private readonly IMetodoEntregaService metodoEntregaService;

        public MetodoEntregaController(IMetodoEntregaService _metodoEntregaService)
        {
            metodoEntregaService = _metodoEntregaService;
        }

        [HttpGet]
        [Route("obtener-metodo-entregas")]
        public async Task<IActionResult> obtenerMetodoEntregas(string? valorBusqueda)
        {
            try
            {
                var listaMetodoEntrega = await metodoEntregaService.listar(valorBusqueda);
                // Eeste es un pequeño cambio
                return Ok(listaMetodoEntrega);

            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Error: {ex.Message} " });
            }

        }

        [HttpPost]
        [Route("registrar-metodo-entrega")]
        public async Task<IActionResult> registrar([FromBody] MetodoEntregaDto metodoEntrega)
        {
            try
            {
                await metodoEntregaService.crear(metodoEntrega);
                return Ok(new { message = "Metodo de Entrega registrada correctamente" });

            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Ha ocurrido un problema {ex.Message}" });
            }
        }

        [HttpPost]
        [Route("editar-metodo-entrega")]
        public async Task<IActionResult> editar([FromBody] MetodoEntregaDto metodoEntrega)
        {
            try
            {
                await metodoEntregaService.editar(metodoEntrega);
                return Ok(new { message = "Metodo de Entrega editada correctamente" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Ha ocurrido un problema {ex.Message}" });
            }
        }

        [HttpPost]
        [Route("eliminar-metodo-entrega/{id:guid}")]
        public async Task<IActionResult> eliminar(Guid id)
        {
            try
            {
                await metodoEntregaService.eliminar(id);

                return Ok(new { message = "Metodo de Entrega Desahabilitada correctamente" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Ha ocurrido un problema {ex.Message}" });
            }

        }

        [HttpGet]
        [Route("obtener-metodo-entrega/{id:guid}")]
        public async Task<IActionResult> obtenerMetodoEntregaId(Guid id)
        {
            try
            {
                var metodoEntrega = await metodoEntregaService.obtenerMetodoEntregaId(id);//cambio jackson
                return Ok(metodoEntrega);

            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Error: {ex.Message} " });
            }
        }
    }
}
