using System;
using System.Collections.Generic;

namespace GestionContrato.Models
{
    public partial class Proveedor
    {
        public Proveedor()
        {
            Contratos = new HashSet<Contrato>();
        }

        public Guid Id { get; set; } = Guid.NewGuid();
        public string Nombre { get; set; } = null!;
        public string Ruc { get; set; } = null!;
        public bool Habilitado { get; set; }
        public Guid IdUsuarioRegistro { get; set; }
        public DateTime FechaRegistro { get; set; }
        public Guid? IdUsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }

        public virtual Usuario? UsuarioModificacion { get; set; }
        public virtual Usuario UsuarioRegistro { get; set; } = null!;
        public virtual ICollection<Contrato> Contratos { get; set; }
    }
}
