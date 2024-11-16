using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionContrato.Dto
{
    public class AsigarUsuarioAprobadorDto
    {
        public Guid Id { get; set; }
        public Guid IdEstado { get; set; }
        public Guid IdUsuarioAprobadorContrato { get; set; }
        public Guid IdUsuarioModificacion { get; set; }
        public string Comentarios { get; set; }
        public string Evento { get; set; }
    }
}
