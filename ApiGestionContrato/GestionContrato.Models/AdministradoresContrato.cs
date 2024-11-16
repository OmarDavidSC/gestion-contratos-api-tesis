using System;
using System.Collections.Generic;

namespace GestionContrato.Models
{
    public partial class AdministradoresContrato
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid IdUsuario { get; set; }
        public Guid IdContrato { get; set; }
        public bool Eliminado { get; set; }
        public Guid IdUsuarioRegistro { get; set; }
        public DateTime FechaRegistro { get; set; }
        public Guid? IdUsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }

        public virtual Contrato IdContratoNavigation { get; set; } = null!;
        public virtual Usuario? UsuarioModificacion { get; set; }
        public virtual Usuario Usuario { get; set; } = null!;
        public virtual Usuario UsuarioRegistro { get; set; } = null!;

        public void Crear(Guid idContrato, Guid idUsuarioRegistro)
        {
            Id = Guid.NewGuid();
            IdContrato = idContrato;
            IdUsuarioRegistro = idUsuarioRegistro;
            FechaRegistro = DateTime.Now;
            Eliminado = false;
        }
    }
}
