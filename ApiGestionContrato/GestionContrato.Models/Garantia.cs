using System;
using System.Collections.Generic;

namespace GestionContrato.Models
{
    public partial class Garantia
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string NumeroGarantia { get; set; } = null!;
        public Guid IdBanco { get; set; }
        public Guid IdTipoGarantia { get; set; }
        public string Monto { get; set; } = null!;
        public Guid IdMoneda { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public Guid IdContrato { get; set; }
        public string NombreArchivo { get; set; } = null!;
        public byte[] ByteArchivo { get; set; } = null!;
        public bool Eliminado { get; set; }
        public Guid IdUsuarioRegistro { get; set; }
        public DateTime FechaRegistro { get; set; }
        public Guid? IdUsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }

        public virtual Banco Banco { get; set; } = null!;
        public virtual Contrato IdContratoNavigation { get; set; } = null!;
        public virtual Moneda Moneda { get; set; } = null!;
        public virtual TipoGarantia TipoGarantia { get; set; } = null!;
        public virtual Usuario? UsuarioModificacion { get; set; }
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
