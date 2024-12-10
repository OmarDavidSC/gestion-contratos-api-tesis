using GestionContrato.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionContrato.BLL.Services.Interfaces
{
    public interface IDashboardService
    {
        Task<List<DashEstadoDto>> contratosPorEstado();
        Task<List<DashTipoContratoDto>> contratosPorTipo();
        Task<List<DashPorProveedorDto>> contratosPorProveedor();
    }
}