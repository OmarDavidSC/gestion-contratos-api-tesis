using System;
using System.Collections.Generic;

namespace GestionContrato.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            AdendaIdUsuarioModificacionNavigations = new HashSet<Adenda>();
            AdendaIdUsuarioRegistroNavigations = new HashSet<Adenda>();
            AdministradoresContratoIdUsuarioModificacionNavigations = new HashSet<AdministradoresContrato>();
            AdministradoresContratoIdUsuarioNavigations = new HashSet<AdministradoresContrato>();
            AdministradoresContratoIdUsuarioRegistroNavigations = new HashSet<AdministradoresContrato>();
            ArchivoContratoIdUsuarioModificacionNavigations = new HashSet<ArchivoContrato>();
            ArchivoContratoIdUsuarioRegistroNavigations = new HashSet<ArchivoContrato>();
            AreaIdUsuarioModificacionNavigations = new HashSet<Area>();
            AreaIdUsuarioRegistroNavigations = new HashSet<Area>();
            AreaIdUsuarioResponsableNavigations = new HashSet<Area>();
            BancoIdUsuarioModificacionNavigations = new HashSet<Banco>();
            BancoIdUsuarioRegistroNavigations = new HashSet<Banco>();
            CompaniaAseguradoraIdUsuarioModificacionNavigations = new HashSet<CompaniaAseguradora> ();
            CompaniaAseguradoraIdUsuarioRegistroNavigations = new HashSet<CompaniaAseguradora>();
            ContratoIdUsuarioAprobadorCierreNavigations = new HashSet<Contrato>();
            ContratoIdUsuarioAprobadorContratoNavigations = new HashSet<Contrato>();
            ContratoIdUsuarioModificacionNavigations = new HashSet<Contrato>();
            ContratoIdUsuarioRegistroNavigations = new HashSet<Contrato>();
            DocumentosAdicionaleIdUsuarioModificacionNavigations = new HashSet<DocumentosAdicionales>();
            DocumentosAdicionaleIdUsuarioRegistroNavigations = new HashSet<DocumentosAdicionales>();
            GarantiaIdUsuarioModificacionNavigations = new HashSet<Garantia>();
            GarantiaIdUsuarioRegistroNavigations = new HashSet<Garantia>();
            HistorialEventos = new HashSet<HistorialEvento>();
            InverseIdUsuarioModificacionNavigation = new HashSet<Usuario>();
            InverseIdUsuarioRegistroNavigation = new HashSet<Usuario>();
            MetodoEntregaIdUsuarioModificacionNavigations = new HashSet<MetodoEntrega>();
            MetodoEntregaIdUsuarioRegistroNavigations = new HashSet<MetodoEntrega>();
            MonedumIdUsuarioModificacionNavigations = new HashSet<Moneda>();
            MonedumIdUsuarioRegistroNavigations = new HashSet<Moneda>();
            PolizaIdUsuarioModificacionNavigations = new HashSet<Poliza>();
            PolizaIdUsuarioRegistroNavigations = new HashSet<Poliza>();
            ProveedorIdUsuarioModificacionNavigations = new HashSet<Proveedor>();
            ProveedorIdUsuarioRegistroNavigations = new HashSet<Proveedor>();
            SistemaContratacionIdUsuarioModificacionNavigations = new HashSet<SistemaContratacion>();
            SistemaContratacionIdUsuarioRegistroNavigations = new HashSet<SistemaContratacion>();
            TipoAdendumIdUsuarioModificacionNavigations = new HashSet<TipoAdenda>();
            TipoAdendumIdUsuarioRegistroNavigations = new HashSet<TipoAdenda>();
            TipoContratoIdUsuarioModificacionNavigations = new HashSet<TipoContrato>();
            TipoContratoIdUsuarioRegistroNavigations = new HashSet<TipoContrato>();
            TipoDocumentoIdUsuarioModificacionNavigations = new HashSet<TipoDocumento>();
            TipoDocumentoIdUsuarioRegistroNavigations = new HashSet<TipoDocumento>();
            TipoGarantiumIdUsuarioModificacionNavigations = new HashSet<TipoGarantia>();
            TipoGarantiumIdUsuarioRegistroNavigations = new HashSet<TipoGarantia>();
            TipoPolizaIdUsuarioModificacionNavigations = new HashSet<TipoPoliza>();
            TipoPolizaIdUsuarioRegistroNavigations = new HashSet<TipoPoliza>();
        }

        public Guid Id { get; set; } = Guid.NewGuid();
        public string Nombre { get; set; } = null!;
        public string ApellidoPaterno { get; set; } = null!;
        public string ApellidoMaterno { get; set; } = null!;
        public string Correo { get; set; } = null!;
        public string Clave { get; set; } = null!;
        public Guid IdArea { get; set; }
        public string? Rol { get; set; } = null!;
        public bool Habilitado { get; set; }
        public string NombreCompleto { get; set; } = null!;
        public Guid IdUsuarioRegistro { get; set; }
        public DateTime FechaRegistro { get; set; }
        public Guid? IdUsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }

        public virtual Area Area { get; set; } = null!;
        public virtual Usuario? UsuarioModificacion { get; set; }
        public virtual Usuario UsuarioRegistro { get; set; } = null!;
        public virtual ICollection<Adenda> AdendaIdUsuarioModificacionNavigations { get; set; }
        public virtual ICollection<Adenda> AdendaIdUsuarioRegistroNavigations { get; set; }
        public virtual ICollection<AdministradoresContrato> AdministradoresContratoIdUsuarioModificacionNavigations { get; set; }
        public virtual ICollection<AdministradoresContrato> AdministradoresContratoIdUsuarioNavigations { get; set; }
        public virtual ICollection<AdministradoresContrato> AdministradoresContratoIdUsuarioRegistroNavigations { get; set; }
        public virtual ICollection<ArchivoContrato> ArchivoContratoIdUsuarioModificacionNavigations { get; set; }
        public virtual ICollection<ArchivoContrato> ArchivoContratoIdUsuarioRegistroNavigations { get; set; }
        public virtual ICollection<Area> AreaIdUsuarioModificacionNavigations { get; set; }
        public virtual ICollection<Area> AreaIdUsuarioRegistroNavigations { get; set; }
        public virtual ICollection<Area> AreaIdUsuarioResponsableNavigations { get; set; }
        public virtual ICollection<Banco> BancoIdUsuarioModificacionNavigations { get; set; }
        public virtual ICollection<Banco> BancoIdUsuarioRegistroNavigations { get; set; }
        public virtual ICollection<CompaniaAseguradora> CompaniaAseguradoraIdUsuarioModificacionNavigations { get; set; }
        public virtual ICollection<CompaniaAseguradora> CompaniaAseguradoraIdUsuarioRegistroNavigations { get; set; }
        public virtual ICollection<Contrato> ContratoIdUsuarioAprobadorCierreNavigations { get; set; }
        public virtual ICollection<Contrato> ContratoIdUsuarioAprobadorContratoNavigations { get; set; }
        public virtual ICollection<Contrato> ContratoIdUsuarioModificacionNavigations { get; set; }
        public virtual ICollection<Contrato> ContratoIdUsuarioRegistroNavigations { get; set; }
        public virtual ICollection<DocumentosAdicionales> DocumentosAdicionaleIdUsuarioModificacionNavigations { get; set; }
        public virtual ICollection<DocumentosAdicionales> DocumentosAdicionaleIdUsuarioRegistroNavigations { get; set; }
        public virtual ICollection<Garantia> GarantiaIdUsuarioModificacionNavigations { get; set; }
        public virtual ICollection<Garantia> GarantiaIdUsuarioRegistroNavigations { get; set; }
        public virtual ICollection<HistorialEvento> HistorialEventos { get; set; }
        public virtual ICollection<Usuario> InverseIdUsuarioModificacionNavigation { get; set; }
        public virtual ICollection<Usuario> InverseIdUsuarioRegistroNavigation { get; set; }
        public virtual ICollection<MetodoEntrega> MetodoEntregaIdUsuarioModificacionNavigations { get; set; }
        public virtual ICollection<MetodoEntrega> MetodoEntregaIdUsuarioRegistroNavigations { get; set; }
        public virtual ICollection<Moneda> MonedumIdUsuarioModificacionNavigations { get; set; }
        public virtual ICollection<Moneda> MonedumIdUsuarioRegistroNavigations { get; set; }
        public virtual ICollection<Poliza> PolizaIdUsuarioModificacionNavigations { get; set; }
        public virtual ICollection<Poliza> PolizaIdUsuarioRegistroNavigations { get; set; }
        public virtual ICollection<Proveedor> ProveedorIdUsuarioModificacionNavigations { get; set; }
        public virtual ICollection<Proveedor> ProveedorIdUsuarioRegistroNavigations { get; set; }
        public virtual ICollection<SistemaContratacion> SistemaContratacionIdUsuarioModificacionNavigations { get; set; }
        public virtual ICollection<SistemaContratacion> SistemaContratacionIdUsuarioRegistroNavigations { get; set; }
        public virtual ICollection<TipoAdenda> TipoAdendumIdUsuarioModificacionNavigations { get; set; }
        public virtual ICollection<TipoAdenda> TipoAdendumIdUsuarioRegistroNavigations { get; set; }
        public virtual ICollection<TipoContrato> TipoContratoIdUsuarioModificacionNavigations { get; set; }
        public virtual ICollection<TipoContrato> TipoContratoIdUsuarioRegistroNavigations { get; set; }
        public virtual ICollection<TipoDocumento> TipoDocumentoIdUsuarioModificacionNavigations { get; set; }
        public virtual ICollection<TipoDocumento> TipoDocumentoIdUsuarioRegistroNavigations { get; set; }
        public virtual ICollection<TipoGarantia> TipoGarantiumIdUsuarioModificacionNavigations { get; set; }
        public virtual ICollection<TipoGarantia> TipoGarantiumIdUsuarioRegistroNavigations { get; set; }
        public virtual ICollection<TipoPoliza> TipoPolizaIdUsuarioModificacionNavigations { get; set; }
        public virtual ICollection<TipoPoliza> TipoPolizaIdUsuarioRegistroNavigations { get; set; }
    }
}
