using System;
using System.Collections.Generic;

namespace GestionContrato.Models
{
    public partial class Area
    {
        public Area()
        {
            Contratos = new HashSet<Contrato>();
            Usuarios = new HashSet<Usuario>();
        }

        public Guid Id { get; set; } = Guid.NewGuid();
        public string Nombre { get; set; } = null!;
        public Guid IdUsuarioResponsable { get; set; }
        public bool Habilitado { get; set; }
        public Guid IdUsuarioRegistro { get; set; }
        public DateTime FechaRegistro { get; set; }
        public Guid? IdUsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }

        public virtual Usuario? UsuarioModificacion { get; set; }
        public virtual Usuario UsuarioRegistro { get; set; } = null!;
        public virtual Usuario UsuarioResponsable { get; set; } = null!;
        public virtual ICollection<Contrato> Contratos { get; set; }
        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}
