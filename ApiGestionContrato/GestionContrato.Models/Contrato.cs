using System;
using System.Collections.Generic;

namespace GestionContrato.Models
{
    public partial class Contrato
    {
        public Contrato()
        {
            Adenda = new HashSet<Adenda>();
            AdministradoresContratos = new HashSet<AdministradoresContrato>();
            ArchivoContratos = new HashSet<ArchivoContrato>();
            DocumentosAdicionales = new HashSet<DocumentosAdicionales>();
            Garantia = new HashSet<Garantia>();
            HistorialEventos = new HashSet<HistorialEvento>();
            Polizas = new HashSet<Poliza>();
        }

        public Guid Id { get; set; } = Guid.NewGuid();
        public string? CodigoContrato { get; set; }
        public string TituloContrato { get; set; } = null!;
        public Guid? IdEstado { get; set; }
        public Guid? IdArea { get; set; }
        public Guid? IdProveedor { get; set; }
        public Guid? IdTipoContrato { get; set; }
        public string? DetalleContrato { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public DateTime? FechaFinReal { get; set; }
        public string MontoContrato { get; set; } = null!;
        public string? MontoTotal { get; set; }
        public Guid? IdMoneda { get; set; }
        public Guid? IdMetodoEntrega { get; set; }
        public Guid? IdSistemaContratacion { get; set; }
        public DateTime? FechaCierreContrato { get; set; }
        public Guid? IdUsuarioAprobadorContrato { get; set; }
        public Guid? IdUsuarioAprobadorCierre { get; set; }
        public DateTime? FechaAnulacion { get; set; }
        public string? MotivoAnulacion { get; set; }
        public string? ComentarioCierreContrato { get; set; }
        public bool Eliminado { get; set; }
        public Guid IdUsuarioRegistro { get; set; }
        public DateTime FechaRegistro { get; set; }
        public Guid? IdUsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }

        public virtual Area Area { get; set; } = null!;
        public virtual Estado Estado { get; set; } = null!;
        public virtual MetodoEntrega MetodoEntrega { get; set; } = null!;
        public virtual Moneda Moneda { get; set; } = null!;
        public virtual Proveedor Proveedor { get; set; } = null!;
        public virtual SistemaContratacion SistemaContratacion { get; set; } = null!;
        public virtual TipoContrato? TipoContrato { get; set; } = null!;
        public virtual Usuario? UsuarioAprobadorCierre { get; set; }
        public virtual Usuario? UsuarioAprobadorContrato { get; set; }
        public virtual Usuario? UsuarioModificacion { get; set; }
        public virtual Usuario UsuarioRegistro { get; set; } = null!;
        public virtual ICollection<Adenda> Adenda { get; set; }
        public virtual ICollection<AdministradoresContrato> AdministradoresContratos { get; set; }
        public virtual ICollection<ArchivoContrato> ArchivoContratos { get; set; }
        public virtual ICollection<DocumentosAdicionales> DocumentosAdicionales { get; set; }
        public virtual ICollection<Garantia> Garantia { get; set; }
        public virtual ICollection<HistorialEvento> HistorialEventos { get; set; }
        public virtual ICollection<Poliza> Polizas { get; set; }
    }
}
