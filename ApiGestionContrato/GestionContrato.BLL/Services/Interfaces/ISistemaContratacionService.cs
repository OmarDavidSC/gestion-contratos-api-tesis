using GestionContrato.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionContrato.BLL.Services.Interfaces
{
    public interface ISistemaContratacionService
    {
        Task<List<SistemaContratacionDto>> listar(string? valorBusqueda);
        Task<SistemaContratacionDto> crear(SistemaContratacionDto modelo);
        Task<bool> editar(SistemaContratacionDto modelo);
        Task<bool> eliminar(Guid id);
        Task<SistemaContratacionDto> obtenerSistemaContratacionId(Guid id);
    }
}
