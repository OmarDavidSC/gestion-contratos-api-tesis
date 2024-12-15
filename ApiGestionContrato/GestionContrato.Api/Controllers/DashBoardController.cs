using GestionContrato.BLL.Services;
using GestionContrato.BLL.Services.Interfaces;
using GestionContrato.DAL.Repositorios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestionContrato.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashBoardController : ControllerBase
    {
        private readonly IDashboardService service;

        public DashBoardController(IDashboardService _service)
        {
            service = _service;
        }

        [HttpGet]
        [Route("por-estado")]
        public async Task<FG<object>> contratosPorEstado()
        {
            var response = new FG<object>(false, new { }, "");
            try
            {
                var data = await service.contratosPorEstado();
                response = new FG<object>(true, data, "Contratos por estado obtentidos con éxito");
                return response;

            }
            catch (Exception ex)
            {
                response = new FG<object>($"{ex.Message}");
                return response;
            }
        }

        [HttpGet]
        [Route("por-proveedor")]
        public async Task<FG<object>> contratosPorProveedor()
        {
            var response = new FG<object>(false, new { }, "");
            try
            {
                var data = await service.contratosPorProveedor();
                response = new FG<object>(true, data, "Contratos por Proveedor obtentidos con éxito");
                return response;

            }
            catch (Exception ex)
            {
                response = new FG<object>($"{ex.Message}");
                return response;
            }
        }

        [HttpGet]
        [Route("por-tipo")]
        public async Task<FG<object>> contratosPorTipo()
        {
            var response = new FG<object>(false, new { }, "");
            try
            {
                var data = await service.contratosPorTipo();
                response = new FG<object>(true, data, "Contratos por Tipo obtentidos con éxito");
                return response;

            }
            catch (Exception ex)
            {
                response = new FG<object>($"{ex.Message}");
                return response;
            }
        }

        [HttpGet]
        [Route("por-mes")]
        public async Task<FG<object>> contratosPorMes()
        {
            var response = new FG<object>(false, new { }, "");
            try
            {
                var data = await service.contratosPorMes();
                response = new FG<object>(true, data, "Contratos por Mes obtentidos con éxito");
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
