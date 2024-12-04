using GestionContrato.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionContrato.BLL.Services.Interfaces
{
    public interface IPerfilService
    {
        Task<PerfilDto> update(PerfilDto modelo);
        Task<PerfilDto> show(Guid id);
    }
}
