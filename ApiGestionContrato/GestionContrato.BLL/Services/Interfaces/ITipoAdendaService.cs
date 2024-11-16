using GestionContrato.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionContrato.BLL.Services.Interfaces
{
    public interface ITipoAdendaService
    {
        Task<List<TipoAdendaDto>> listar(string? valorBusqueda);
        Task<TipoAdendaDto> crear(TipoAdendaDto modelo);
        Task<bool> editar(TipoAdendaDto modelo);
        Task<bool> eliminar(Guid id);
        Task<TipoAdendaDto> obtenerTipoAdendaId(Guid id);
    }
}
