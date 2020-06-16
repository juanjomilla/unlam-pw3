using System;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web.Hosting;

namespace Repositorio.Repositorios
{
    public class UsuarioRepositorio : Repository<Usuarios>, IUsuarioRepositorio
    {
        // El contexto ya está registrado en autofac, por lo tanto se inyecta automáticamente
        public UsuarioRepositorio(Contexto contexto) : base(contexto) { }

        public Usuarios BuscarUsuario(Usuarios usuario)
        {
            var usuarioEcontrado = Get(x => x.Email == usuario.Email && x.Password == usuario.Password).FirstOrDefault();
            //var usuarioEcontrado = (from u in _dbContext.Usuarios 
            //                        where u.Email == usuario.Email && 
            //                        u.Password == usuario.Password 
            //                        select u).FirstOrDefault<Usuarios>();

            return usuarioEcontrado;

        }

        public void CrearUsuario(Usuarios usuarioNuevo)
        {
            using (var unitOfWork = new UnitOfWork(_dbContext))
            {
                _dbContext.Usuarios.Add(usuarioNuevo);

                try
                {
                   _dbContext.SaveChanges();

                }
                catch (DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                    throw;
                }
                /////////////////////////////////////////////Enviar Email//////////////////////////////////////////

                BuildEmailTemplate(usuarioNuevo.IdUsuario);
            }
        }

        public void ValidarUsuario(int IdUsuario)
        {
            var usuarioValidado = Get(x => x.IdUsuario == IdUsuario).FirstOrDefault();
            usuarioValidado.Activo = true;
            _dbContext.SaveChanges();
        }

        private void BuildEmailTemplate(int IdUsuario)
        {
                string body = System.IO.File.ReadAllText(HostingEnvironment.MapPath("~/Views/EmailTemplate/") + "Text" + ".cshtml");
                var regInfo = _dbContext.Usuarios.Where(x => x.IdUsuario == IdUsuario).FirstOrDefault();
                var url = "https://localhost:44384/" + "Login/Confirm?IdUsuario=" + IdUsuario;
                body = body.Replace("@ViewBag.ConfirmationLink", url);
                body = body.ToString();
                BuildEmailTemplate("Su cuenta fue exitosamente creada", body, regInfo.Email);
            }

            private void BuildEmailTemplate(string subjectText, string bodyText, string sendTo)
            {
                string from, to, bcc, cc, subject, body;
                from = "yzornetta@gmail.com";
                to = sendTo.Trim();
                bcc = "";
                cc = "";
                subject = subjectText;
                StringBuilder sb = new StringBuilder();
                sb.Append(bodyText);
                body = sb.ToString();
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(from);
                mail.To.Add(new MailAddress(to));
                if (!string.IsNullOrEmpty(bcc))
                {
                    mail.Bcc.Add(new MailAddress(bcc));
                }
                if (!string.IsNullOrEmpty(cc))
                {
                    mail.CC.Add(new MailAddress(cc));
                }
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;
                SendEmail(mail);
            }

            private void SendEmail(MailMessage mail)
            {

                SmtpClient client = new SmtpClient();
                client.Host = "smtp.gmail.com";
                client.Port = 587;
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Credentials = new System.Net.NetworkCredential("miEmail@gmail.com", "MiClave");
                try
                {
                    client.Send(mail);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            
            }
    }
}
