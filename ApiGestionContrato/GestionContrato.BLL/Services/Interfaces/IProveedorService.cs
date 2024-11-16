using GestionContrato.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionContrato.BLL.Services.Interfaces
{
    public interface IProveedorService
    {
        Task<List<ProveedorDto>> listar(string? valorBusqueda);
        Task<ProveedorDto> crear(ProveedorDto modelo);
        Task<bool> editar(ProveedorDto modelo);
        Task<bool> eliminar(Guid id);
        Task<ProveedorDto> obtenerProveedorId(Guid id);
    }
}
