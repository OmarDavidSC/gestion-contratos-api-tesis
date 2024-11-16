using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionContrato.Dto
{
    public class GarantiaDto
    {
        public Guid? Id { get; set; } = Guid.NewGuid();
        public string? NumeroGarantia { get; set; } = null!;
        public BancoDto? Banco { get; set; }
        public TipoGarantiaDto? TipoGarantia { get; set; }
        public string? Monto { get; set; } = null!;
        public MonedaDto? Moneda { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public Guid? IdContrato { get; set; }
        public string? NombreArchivo { get; set; } = null!;
        public byte[]? ByteArchivo { get; set; } = null!;
        public bool Eliminado { get; set; }
        public UsuarioDto? UsuarioRegistro { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public UsuarioDto? UsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
    }
}
