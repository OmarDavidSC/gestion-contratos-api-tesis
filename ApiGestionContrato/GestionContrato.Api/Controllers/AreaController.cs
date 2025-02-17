using GestionContrato.BLL.Services;
using GestionContrato.BLL.Services.Interfaces;
using GestionContrato.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestionContrato.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AreaController : ControllerBase
    {
        private readonly IAreaService areaService;

        public AreaController(IAreaService _areaService)
        {
            areaService = _areaService;
        }

        [HttpGet]
        [Route("obtener-areas")]
        public async Task<IActionResult> obtenerAreas(string? valorBusqueda)
        {
            try
            {
                var listaAreas = await areaService.listar(valorBusqueda);
                return Ok(listaAreas);

            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Error: {ex.Message} " });
            }
        }

        [HttpPost]
        [Route("registrar-area")]
        public async Task<IActionResult> registrar([FromBody] AreaDto area)
        {
            try
            {
                await areaService.crear(area);
                return Ok(new { message = "Area registrada correctamente" });

            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Ha ocurrido un problema {ex.Message}" });
            }
        }

        [HttpPost]
        [Route("editar-area")]
        public async Task<IActionResult> editar([FromBody] AreaDto area)
        {
            try
            {
                await areaService.editar(area);
                return Ok(new { message = "Area editada correctamente" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Ha ocurrido un problema {ex.Message}" });
            }
        }

        [HttpPost]
        [Route("eliminar-area/{id:guid}")]
        public async Task<IActionResult> eliminar(Guid id)
        {
            try
            {
                await areaService.eliminar(id);

                return Ok(new { message = "Area Desahabilitada correctamente" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Ha ocurrido un problema {ex.Message}" });
            }

        }

        [HttpGet]
        [Route("obtener-area/{id:guid}")]
        public async Task<IActionResult> obtenerAreaId(Guid id)
        {
            try
            {
                var area = await areaService.obtenerAreaId(id);
                return Ok(area);

            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Error: {ex.Message} " });
            }
        }
    }
}
