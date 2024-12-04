using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionContrato.Dto
{
    public class ResetPasswordDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? NuevaContrasena { get; set; }
        public string? ConfirmarContrasena { get; set; }
    }
}
