using GestionContrato.BLL.Services;
using GestionContrato.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestionContrato.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailController : ControllerBase
    {
        private readonly MailService emailService;

        public MailController(MailService _emailService)
        { 
            emailService = _emailService;
        }
           

        [HttpPost("enviar")]
        public async Task<IActionResult> EnviarCorreo([FromBody] CorreoDto request)
        {
            try
            {
                await emailService.enviarCorreo(request.Destinatario, request.Asunto, request.Mensaje);
                return Ok("Correo enviado exitosamente.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error enviando el correo: {ex.Message}");
            }
        }
    }
}
