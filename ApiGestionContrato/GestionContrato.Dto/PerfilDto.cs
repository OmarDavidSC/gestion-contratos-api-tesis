using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionContrato.Dto
{
    public class PerfilDto
    {
        public Guid? Id { get; set; } = Guid.NewGuid();
        public string? Nombre { get; set; } = null!;
        public string? ApellidoPaterno { get; set; } = null!;
        public string? ApellidoMaterno { get; set; } = null!;
        public string? Correo { get; set; } = null!;
        public UsuarioDto? UsuarioRegistro { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public UsuarioDto? UsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
    }
}
