using Microsoft.Extensions.Options;
using prueba_facturas.Configuracion;
using prueba_facturas.Servicios.Interfaces;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace prueba_facturas.Servicios.Implementacion
{

    public class EmailService : IEmailServices
    {
        private readonly SmtpSettings _smtpSettings;

        public EmailService(IOptions<SmtpSettings> smtpSettings)
        {
            _smtpSettings = smtpSettings.Value;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string nombreCliente, string facturaCodigo, string estado)
        {
            var smtpClient = new SmtpClient(_smtpSettings.SmtpServer)
            {
                Port = _smtpSettings.SmtpPort,
                Credentials = new NetworkCredential(_smtpSettings.SmtpUsername, _smtpSettings.SmtpPassword),
                EnableSsl = _smtpSettings.EnableSsl,
            };

            var templatePath = "email_template.html";
            var emailTemplate = File.ReadAllText(templatePath);

            emailTemplate = emailTemplate.Replace("{{nombreCliente}}", nombreCliente)
                                         .Replace("{{FacturaCodigo}}", facturaCodigo)
                                         .Replace("{{estado}}", estado)
                                         .Replace("{{link}}", "http://localhost:4200/verFactura/correo_" + facturaCodigo); // Reemplaza con tu enlace de Angular

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_smtpSettings.SmtpUsername),
                Subject = subject,
                Body = emailTemplate,
                IsBodyHtml = true,
            };

            mailMessage.To.Add(toEmail);

            await smtpClient.SendMailAsync(mailMessage);
        }
    }
}
