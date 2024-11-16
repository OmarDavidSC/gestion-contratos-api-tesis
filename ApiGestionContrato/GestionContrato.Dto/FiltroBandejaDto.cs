using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionContrato.Dto
{
    public class FiltroBandejaDto
    {
        public string Vista { get; set; }
        public string Codigo { get; set; }
        public string Asunto { get; set; }
        public List<Guid> TipoContrato { get; set; }
        public List<Guid> ListaEstado { get; set; }
        public List<Guid> ListaTipoContrato { get; set; }
        public List<Guid> ListaArea { get; set; }
        public DateTime? FechaRegistroInicio { get; set; }
        public DateTime? FechaRegistroFin { get; set; }
        public Guid? IdUsuarioRegistro { get; set; }
        public string TextoBusquedaRapida { get; set; }

        public FiltroBandejaDto()
        {
            Vista = string.Empty;
            Codigo = string.Empty;
            Asunto = string.Empty;
            TipoContrato = new List<Guid>();
            ListaEstado = new List<Guid>();
            ListaTipoContrato = new List<Guid>();
            ListaArea = new List<Guid>();
            FechaRegistroInicio = null;
            FechaRegistroFin = null;
            IdUsuarioRegistro = null;
            TextoBusquedaRapida = string.Empty;
        }
    }
}
