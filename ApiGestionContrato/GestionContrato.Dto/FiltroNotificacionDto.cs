using GestionContrato.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionContrato.Dto
{
    public class FiltroNotificacionDto
    {
        public Guid? IdUsuarioRegistro { get; set; } 
        public string? Vista { get; set; }

        public FiltroNotificacionDto()
        {
            Vista = string.Empty;
            IdUsuarioRegistro = null;

        }
    }
}
