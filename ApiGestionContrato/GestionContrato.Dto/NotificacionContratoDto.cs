using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionContrato.Dto
{
    public class NotificacionContratoDto
    {
        public Guid IdContrato { get; set; }
        public string TituloContrato { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public DateTime FechaNotificacion { get; set; }
        public int DiasRestantes { get; set; }
        public string Mensaje { get; set; }
        public string Tipo { get; set; }
        public string FechaVencimientoLabel { get; set; }
        public string FechaNotificacionLabel { get; set; }

    }
}
