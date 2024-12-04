using GestionContrato.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionContrato.BLL.Services.Interfaces
{
    public interface IContratoService
    {
        Task<List<ContratoDto>> listaBandeja(FiltroBandejaDto? filtroBandeja);
        Task<ContratoDto> getIdContrato(Guid id);
        Task<ContratoDto> registrarContrato(ContratoDto modelo); 
        Task<ContratoDto> editarContraro(ContratoDto modelo);
        Task<ContratoDto> guardarContrato(ContratoDto modelo);
        Task<bool> derivarContrato(AsigarUsuarioAprobadorDto modelo);
        Task<bool> accionContrato(AccionContratoDto modelo);
        Task<List<NotificacionContratoDto>> ObtenerNotificacionesContratos();
    }
}
