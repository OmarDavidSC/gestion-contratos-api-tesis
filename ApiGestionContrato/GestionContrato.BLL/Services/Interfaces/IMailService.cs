using GestionContrato.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionContrato.BLL.Services.Interfaces
{
    public interface IMailService
    {
        Task enviarCorreo(string destinatario, string asunto, string mensaje);
    }
}
