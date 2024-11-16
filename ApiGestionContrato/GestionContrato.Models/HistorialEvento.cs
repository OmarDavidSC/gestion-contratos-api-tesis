using System;
using System.Collections.Generic;

namespace GestionContrato.Models
{
    public partial class HistorialEvento
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? Descripcion { get; set; }
        public Guid IdContrato { get; set; }
        public Guid IdUsuarioRegistro { get; set; }
        public DateTime FechaRegistro { get; set; }

        public virtual Contrato IdContratoNavigation { get; set; } = null!;
        public virtual Usuario UsuarioRegistro { get; set; } = null!;

        public void Crear(Guid idContrato, Guid idUsuarioRegistro)
        {
            Id = Guid.NewGuid();
            IdContrato = idContrato;
            IdUsuarioRegistro = idUsuarioRegistro;
            FechaRegistro = DateTime.Now;
        }
    }
}
