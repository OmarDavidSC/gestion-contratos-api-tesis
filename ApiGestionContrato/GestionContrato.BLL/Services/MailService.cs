using GestionContrato.BLL.Services.Interfaces;
using MimeKit;
using MailKit.Net.Smtp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionContrato.BLL.Services
{
    public class MailService : IMailService
    {
        public async Task enviarCorreo(string destinatario, string asunto, string mensaje)
        {
            // Crea el mensaje de correo
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress("Empresa ADNI", "serquencoronadoomardavid@gmail.com"));
            email.To.Add(new MailboxAddress("Destinatario", destinatario));
            email.Subject = asunto;

            // Cuerpo del correo
            email.Body = new TextPart("html")
            {
                Text = mensaje
            };

            // Configura el cliente SMTP
            using var smtp = new SmtpClient();
            try
            {
                // Conéctate al servidor SMTP de Gmail
                await smtp.ConnectAsync("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);

                // Autenticación
                await smtp.AuthenticateAsync("serquencoronadoomardavid@gmail.com", "$Serquen200$");

                // Envía el correo
                await smtp.SendAsync(email);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error enviando correo: {ex.Message}");
                throw; // Para depuración      
            }
            finally
            {
                await smtp.DisconnectAsync(true);
            }
        }
    }

}
