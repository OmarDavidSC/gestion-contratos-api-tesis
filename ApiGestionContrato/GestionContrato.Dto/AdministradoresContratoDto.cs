using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionContrato.Dto
{
    public class AdministradoresContratoDto
    {
        public Guid? Id { get; set; } = Guid.NewGuid();
        public UsuarioDto? Usuario { get; set; }
        public Guid? IdContrato { get; set; }
        public bool? Eliminado { get; set; }
        public UsuarioDto? UsuarioRegistro { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public UsuarioDto? UsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
    }
}
