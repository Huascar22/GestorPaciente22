using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;
using System.ComponentModel.Design;
using System.Security.Cryptography.X509Certificates;
using System.Runtime.InteropServices.JavaScript;

namespace Controlador.Servicios
{
    public class EnviarCorreo
    {
        private string EmailFrom = "podcastdehoy@gmail.com";
        private static string SmtpHost = "smtp.gmail.com";
        private static int SmtpPort = 587;
        private static string SmtpUser = "podcastdehoy@gmail.com";
        private static string SmtpPass = "sleofmlhjldtsyho";
        private static string DisplayName = "Gestor Paciente";
       

        public static void Enviar(string destino, string subject, string mensajeEnviar, string urlRetorno)
        {
            MimeMessage mensaje = new();
            mensaje.From.Add(new MailboxAddress(DisplayName, SmtpUser));
            mensaje.To.Add(new MailboxAddress("Destino", destino));
            mensaje.Subject = subject;

            BodyBuilder cuerpoMensaje = new();
            //cuerpoMensaje.TextBody = "Hola Mundo";
            cuerpoMensaje.HtmlBody = $"{mensajeEnviar} <a href=\""+ urlRetorno + "\">Enlace</a>";
            mensaje.Body = cuerpoMensaje.ToMessageBody();
            SmtpClient clienteSmtp = new SmtpClient();
            clienteSmtp.CheckCertificateRevocation = false;
            clienteSmtp.Connect(SmtpHost, SmtpPort, MailKit.Security.SecureSocketOptions.StartTls);
            clienteSmtp.Authenticate(SmtpUser, SmtpPass);
            clienteSmtp.Send(mensaje);
            clienteSmtp.Disconnect(true);
        }

        public static void GenerarCodigo()
        {

        }
        
    }
}
