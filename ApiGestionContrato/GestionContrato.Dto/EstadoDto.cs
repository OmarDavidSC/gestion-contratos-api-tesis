using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionContrato.Dto
{
    public class EstadoDto
    {
        public Guid? Id { get; set; } = Guid.NewGuid();
        public string? Nombre { get; set; } = null!;
    }
}
