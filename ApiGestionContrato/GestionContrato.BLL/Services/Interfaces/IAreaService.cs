using GestionContrato.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionContrato.BLL.Services.Interfaces
{
    public interface IAreaService
    {
        Task<List<AreaDto>> listar(string? valorBusqueda);
        Task<AreaDto> crear(AreaDto modelo);
        Task<bool> editar(AreaDto modelo);
        Task<bool> eliminar(Guid id);
        Task<AreaDto> obtenerAreaId(Guid id);
    }
}
