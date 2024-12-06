using GestionContrato.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionContrato.Dto
{
    public class ContratoDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? CodigoContrato { get; set; }
        public string? TituloContrato { get; set; }
        public EstadoDto? Estado { get; set; }
        public AreaDto? Area { get; set; }
        public ProveedorDto? Proveedor { get; set; }
        public TipoContratoDto? TipoContrato { get; set; }
        public string? DetalleContrato { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public DateTime? FechaFinReal { get; set; }
        public string? MontoContrato { get; set; }
        public string? MontoTotal { get; set; }
        public int? DiasFaltanParaVencimiento { get; set; }
        public MonedaDto? Moneda { get; set; }
        public MetodoEntregaDto? MetodoEntrega { get; set; }
        public SistemaContratacionDto? SistemaContratacion { get; set; }
        public DateTime? FechaCierreContrato { get; set; }
        public UsuarioDto? UsuarioAprobadorContrato { get; set; }
        public UsuarioDto? UsuarioAprobadorCierre { get; set; }
        public DateTime? FechaAnulacion { get; set; }
        public string? MotivoAnulacion { get; set; }
        public string? ComentarioCierreContrato { get; set; }
        public bool? Eliminado { get; set; }
        public UsuarioDto? UsuarioRegistro { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public UsuarioDto? UsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }

        public List<AdendaDto>? Adenda { get; set; }
        public List<AdministradoresContratoDto>? AdministradoresContratos { get; set; }
        public List<ArchivoContratoDto>? ArchivoContratos { get; set; }
        public List<DocumentosAdicionalesDto>? DocumentosAdicionales { get; set; }
        public List<GarantiaDto>? Garantia { get; set; }
        public List<HistorialEventoDto>? HistorialEventos { get; set; }
        public List<PolizaDto>? Polizas { get; set; }
    }
}
