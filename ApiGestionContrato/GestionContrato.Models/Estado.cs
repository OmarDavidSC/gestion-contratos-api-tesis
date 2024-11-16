using System;
using System.Collections.Generic;

namespace GestionContrato.Models
{
    public partial class Estado
    {
        public Estado()
        {
            Contratos = new HashSet<Contrato>();
        }

        public Guid Id { get; set; } = Guid.NewGuid();
        public string Nombre { get; set; } = null!;

        public virtual ICollection<Contrato> Contratos { get; set; }
    }
}
