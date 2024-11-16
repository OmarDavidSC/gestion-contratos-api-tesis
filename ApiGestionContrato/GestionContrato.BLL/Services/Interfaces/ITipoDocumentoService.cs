using GestionContrato.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionContrato.BLL.Services.Interfaces
{
    public interface ITipoDocumentoService
    {
        Task<List<TipoDocumentoDto>> listar(string? valorBusqueda);
        Task<TipoDocumentoDto> crear(TipoDocumentoDto modelo);
        Task<bool> editar(TipoDocumentoDto modelo);
        Task<bool> eliminar(Guid id);
        Task<TipoDocumentoDto> obtenerTipoDocumentoId(Guid id);
    }
}
