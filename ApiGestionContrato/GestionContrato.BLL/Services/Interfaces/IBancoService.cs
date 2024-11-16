using GestionContrato.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionContrato.BLL.Services.Interfaces
{
    public interface IBancoService
    {
        Task<List<BancoDto>> listar(string? valorBusqueda);
        Task<BancoDto> crear(BancoDto modelo);
        Task<bool> editar(BancoDto modelo);
        Task<bool> eliminar(Guid id);
        Task<BancoDto> obtenerBancoId(Guid id);
    }
}
