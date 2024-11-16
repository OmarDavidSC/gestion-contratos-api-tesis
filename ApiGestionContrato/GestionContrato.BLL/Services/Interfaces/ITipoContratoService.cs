using GestionContrato.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionContrato.BLL.Services.Interfaces
{
    public interface ITipoContratoService
    {
        Task<List<TipoContratoDto>> listar(string? valorBusqueda);
        Task<TipoContratoDto> crear(TipoContratoDto modelo);
        Task<bool> editar(TipoContratoDto modelo);
        Task<bool> eliminar(Guid id);
        Task<TipoContratoDto> obtenerTipoContratoId(Guid id);
    }
}
