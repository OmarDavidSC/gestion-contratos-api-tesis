using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionContrato.Dto
{
    public class DashEstadoDto
    {
        public int CantidadContratos { get; set; }
        public string? Estado { get; set; }
        public int Cantidad { get; set; }
        public List<ListContrato>? Contratos { get; set; }
    }

    public class ListContrato
    {
        public Guid Id { get; set; }
        public string?  CodigoContrato {  get; set; }
        public string? Nombre { get; set; }
        public DateTime? FechaFinReal { get; set; }
    }
}
