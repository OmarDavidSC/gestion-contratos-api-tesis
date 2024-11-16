using System;
using System.Collections.Generic;

namespace GestionContrato.Models
{
    public partial class DocumentosAdicionales
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid IdContrato { get; set; }
        public string NombreArchivo { get; set; } = null!;
        public byte[] ByteArchivo { get; set; } = null!;
        public Guid? IdTipoDocumento { get; set; }
        public bool Eliminado { get; set; }
        public Guid IdUsuarioRegistro { get; set; }
        public DateTime FechaRegistro { get; set; }
        public Guid? IdUsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }

        public virtual Contrato IdContratoNavigation { get; set; } = null!;
        public virtual TipoDocumento TipoDocumento { get; set; } = null!;
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
