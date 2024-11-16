using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionContrato.Dto
{
    public class HistorialEventoDto
    {
        public Guid? Id { get; set; } = Guid.NewGuid();
        public string? Descripcion { get; set; }
        public Guid? IdContrato { get; set; }
        public UsuarioDto? UsuarioRegistro { get; set; }
        public DateTime? FechaRegistro { get; set; }
    }
}
