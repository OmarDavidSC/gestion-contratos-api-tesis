using GestionContrato.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionContrato.BLL.Services.Interfaces
{
    public interface ITipoPolizaService
    {
        Task<List<TipoPolizaDto>> listar(string? valorBusqueda);
        Task<TipoPolizaDto> crear(TipoPolizaDto modelo);
        Task<bool> editar(TipoPolizaDto modelo);
        Task<bool> eliminar(Guid id);
        Task<TipoPolizaDto> obtenerTipoPolizaId(Guid id);
    }
}
