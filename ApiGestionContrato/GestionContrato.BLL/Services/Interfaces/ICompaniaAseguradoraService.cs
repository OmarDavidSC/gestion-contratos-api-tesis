using GestionContrato.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionContrato.BLL.Services.Interfaces
{
    public interface ICompaniaAseguradoraService
    {
        Task<List<CompaniaAseguradoraDto>> listar(string? valorBusqueda);
        Task<CompaniaAseguradoraDto> crear(CompaniaAseguradoraDto modelo);
        Task<bool> editar(CompaniaAseguradoraDto modelo);
        Task<bool> eliminar(Guid id);
        Task<CompaniaAseguradoraDto> obtenerCompaniaAseguradoraId(Guid id);
    }
}
