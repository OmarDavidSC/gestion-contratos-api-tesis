using GestionContrato.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionContrato.BLL.Services.Interfaces
{
    public interface IMonedaService
    {
        Task<List<MonedaDto>> listar(string? valorBusqueda);
        Task<MonedaDto> crear(MonedaDto modelo);
        Task<bool> editar(MonedaDto modelo);
        Task<bool> eliminar(Guid id);
        Task<MonedaDto> obtenerMonedaId(Guid id);
    }
}
