using GestionContrato.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionContrato.BLL.Services.Interfaces
{
    public interface IUsuarioService
    {
        Task<List<UsuarioDto>> lista(string? valorBusqueda);
        Task<UsuarioDto> crear(UsuarioDto modelo);
        Task<bool> editar(UsuarioDto modelo);
        Task<bool> eliminar(Guid id);

        Task<UsuarioDto> obtenerUsuarioId(Guid id);

        Task<List<UsuarioDto>> buscarUsuarios(string palabra);
    }
}
