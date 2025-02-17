using GestionContrato.BLL.Services;
using GestionContrato.BLL.Services.Interfaces;
using GestionContrato.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestionContrato.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProveedorController : ControllerBase
    {
        private readonly IProveedorService proveedorService;

        public ProveedorController(IProveedorService _proveedorService)
        {
            proveedorService = _proveedorService;
        }

        [HttpGet]
        [Route("obtener-provedores")]
        public async Task<IActionResult> obtenerProveedores(string? valorBusqueda)
        {
            try
            {
                var listaProveedor = await proveedorService.listar(valorBusqueda);
                return Ok(listaProveedor);

            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Error: {ex.Message} " });
            }
        }

        [HttpPost]
        [Route("registrar-proveedor")]
        public async Task<IActionResult> registrar([FromBody] ProveedorDto proveedor)
        {
            try
            {
                await proveedorService.crear(proveedor);
                return Ok(new { message = "Proveedor registrado correctamente" });

            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Ha ocurrido un problema {ex.Message}" });
            }
        }

        [HttpPost]
        [Route("editar-proveedor")]
        public async Task<IActionResult> editar([FromBody] ProveedorDto proveedor)
        {
            try
            {
                await proveedorService.editar(proveedor);
                return Ok(new { message = "Proveedor editado correctamente" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Ha ocurrido un problema {ex.Message}" });
            }
        }

        [HttpPost]
        [Route("eliminar-proveedor/{id:guid}")]
        public async Task<IActionResult> eliminar(Guid id)
        {
            try
            {
                await proveedorService.eliminar(id);

                return Ok(new { message = "Proveedor Desahabilitado correctamente" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Ha ocurrido un problema {ex.Message}" });
            }
        }

        [HttpGet]
        [Route("obtener-proveedor/{id:guid}")]
        public async Task<IActionResult> obtenerProveedorId(Guid id)
        {
            try
            {
                var proveedor = await proveedorService.obtenerProveedorId(id);
                return Ok(proveedor);

            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Error: {ex.Message} " });
            }
        }
    }
}
