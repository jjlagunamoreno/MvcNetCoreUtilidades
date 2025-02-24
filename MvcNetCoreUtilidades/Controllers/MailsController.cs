using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace MvcNetCoreUtilidades.Controllers
{
    public class MailsController : Controller
    {
        private IConfiguration configuration;

        public MailsController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public IActionResult SendMail()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendMail(string to, string asunto, string mensaje)
        {
            MailMessage mail = new MailMessage();

            // Obtener datos de configuración ajustados a tu appsettings.json
            string user = this.configuration.GetValue<string>("MailSetting:Credenciales:User");
            string password = this.configuration.GetValue<string>("MailSetting:Credenciales:Password");
            string host = this.configuration.GetValue<string>("MailSetting:Server:Host");
            int port = this.configuration.GetValue<int>("MailSetting:Server:Port");
            bool ssl = this.configuration.GetValue<bool>("MailSetting:Server:Ssl");
            bool defaultCredentials = this.configuration.GetValue<bool>("MailSetting:Server:DefaultCredentials");

            // Configurar el correo
            mail.From = new MailAddress(user);
            mail.To.Add(to);
            mail.Subject = asunto;
            mail.Body = mensaje;
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.Normal;

            // Configurar el cliente SMTP
            SmtpClient smtpClient = new SmtpClient
            {
                Host = host,
                Port = port,
                EnableSsl = ssl,
                UseDefaultCredentials = defaultCredentials,
                Credentials = new NetworkCredential(user, password) // Asignamos las credenciales
            };

            await smtpClient.SendMailAsync(mail);
            ViewData["MENSAJE"] = "Mail enviado correctamente.";
            return View();
        }
    }
}
