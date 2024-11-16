using GestionContrato.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionContrato.BLL.Services.Interfaces
{
    public interface IMetodoEntregaService
    {
        Task<List<MetodoEntregaDto>> listar(string? valorBusqueda);
        Task<MetodoEntregaDto> crear(MetodoEntregaDto modelo);
        Task<bool> editar(MetodoEntregaDto modelo);
        Task<bool> eliminar(Guid id);
        Task<MetodoEntregaDto> obtenerMetodoEntregaId(Guid id);
    }
}
