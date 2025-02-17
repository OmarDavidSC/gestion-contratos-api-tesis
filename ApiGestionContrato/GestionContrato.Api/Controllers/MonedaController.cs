using GestionContrato.BLL.Services;
using GestionContrato.BLL.Services.Interfaces;
using GestionContrato.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestionContrato.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MonedaController : ControllerBase
    {
        private readonly IMonedaService monedaService;

        public MonedaController(IMonedaService _monedaService)
        {
            monedaService = _monedaService;
        }

        [HttpGet]
        [Route("obtener-monedas")]
        public async Task<IActionResult> obtenerMonedas(string? valorBusqueda)
        {
            try
            {
                var listaMonedas = await monedaService.listar(valorBusqueda);
                return Ok(listaMonedas);

            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Error: {ex.Message} " });
            }
        }

        [HttpPost]
        [Route("registrar-moneda")]
        public async Task<IActionResult> registrar([FromBody] MonedaDto moneda)
        {
            try
            {
                await monedaService.crear(moneda);
                return Ok(new { message = "Moneda registrada correctamente" });

            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Ha ocurrido un problema {ex.Message}" });
            }
        }

        [HttpPost]
        [Route("editar-moneda")]
        public async Task<IActionResult> editar([FromBody] MonedaDto moneda)
        {
            try
            {
                await monedaService.editar(moneda);
                return Ok(new { message = "Moneda editada correctamente" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Ha ocurrido un problema {ex.Message}" });
            }
        }

        [HttpPost]
        [Route("eliminar-moneda/{id:guid}")]
        public async Task<IActionResult> eliminar(Guid id)
        {
            try
            {
                await monedaService.eliminar(id);

                return Ok(new { message = "Moneda Desahabilitada correctamente" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Ha ocurrido un problema {ex.Message}" });
            }
        }

        [HttpGet]
        [Route("obtener-moneda/{id:guid}")]
        public async Task<IActionResult> obtenerMonedaId(Guid id)
        {
            try
            {
                var moneda = await monedaService.obtenerMonedaId(id);
                return Ok(moneda);

            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Error: {ex.Message} " });
            }
        }
    }
}
