using GestionContrato.BLL.Services.Interfaces;
using GestionContrato.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestionContrato.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniaAseguradoraController : ControllerBase
    {
        private readonly ICompaniaAseguradoraService companiaAseguradoraService;

        public CompaniaAseguradoraController(ICompaniaAseguradoraService _companiaAseguradoraService)
        {
            companiaAseguradoraService = _companiaAseguradoraService;
        }

        [HttpGet]
        [Route("obtener-compania-aseguradoras")]
        public async Task<IActionResult> obtenerCompaniaAseguradoras(string? valorBusqueda)
        {
            try
            {
                var listaCompaniaAseguradoras = await companiaAseguradoraService.listar(valorBusqueda);
                return Ok(listaCompaniaAseguradoras);

            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Error: {ex.Message} " });
            }
        }

        [HttpPost]
        [Route("registrar-compania-aseguradora")]
        public async Task<IActionResult> registrar([FromBody] CompaniaAseguradoraDto companiaAseguradora)
        {
            try
            {
                await companiaAseguradoraService.crear(companiaAseguradora);
                return Ok(new { message = "Compañia Aseguradora registrada correctamente" });

            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Ha ocurrido un problema {ex.Message}" });
            }
        }

        [HttpPost]
        [Route("editar-compania-aseguradora")]
        public async Task<IActionResult> editar([FromBody] CompaniaAseguradoraDto companiaAseguradora)
        {
            try
            {
                await companiaAseguradoraService.editar(companiaAseguradora);
                return Ok(new { message = "CompaniaAseguradora editada correctamente" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Ha ocurrido un problema {ex.Message}" });
            }
        }

        [HttpPost]
        [Route("eliminar-compania-aseguradora/{id:guid}")]
        public async Task<IActionResult> eliminar(Guid id)
        {
            try
            {
                await companiaAseguradoraService.eliminar(id);

                return Ok(new { message = "CompaniaAseguradora Desahabilitada correctamente" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Ha ocurrido un problema {ex.Message}" });
            }

        }

        [HttpGet]
        [Route("obtener-compania-aseguradora/{id:guid}")]
        public async Task<IActionResult> obtenerCompaniaAseguradoraId(Guid id)
        {
            try
            {
                var companiaAseguradora = await companiaAseguradoraService.obtenerCompaniaAseguradoraId(id);
                return Ok(companiaAseguradora);

            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Error: {ex.Message} " });
            }
        }
    }
}

