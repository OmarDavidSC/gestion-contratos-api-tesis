using System;
using System.Collections.Generic;

namespace GestionContrato.Models
{
    public partial class Poliza
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string NumeroPoliza { get; set; } = null!;
        public Guid IdCompaniaAseguradora { get; set; }
        public Guid IdTipoPoliza { get; set; }
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

        public virtual CompaniaAseguradora CompaniaAseguradora { get; set; } = null!;
        public virtual Contrato IdContratoNavigation { get; set; } = null!;
        public virtual Moneda Moneda { get; set; } = null!;
        public virtual TipoPoliza TipoPoliza { get; set; } = null!;
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
