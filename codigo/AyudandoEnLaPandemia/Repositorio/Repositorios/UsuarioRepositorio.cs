using System;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web.Hosting;

namespace Repositorio.Repositorios
{
    public class UsuarioRepositorio : Repositorio<Usuarios>, IUsuarioRepositorio
    {
        // El contexto ya está registrado en autofac, por lo tanto se inyecta automáticamente
        public UsuarioRepositorio(Contexto contexto) : base(contexto) { }

        public void ActualizarPerfil(string nombre, string apellido, DateTime fechaNacimiento, string foto, int idUsuario, string userName)
        {
            using (var unitOfWork = new UnitOfWork(_dbContext))
            {
                Usuarios perfilViejo = unitOfWork.Usuarios.Get(idUsuario);

                perfilViejo.Nombre = nombre;
                perfilViejo.Apellido = apellido;
                perfilViejo.FechaNacimiento = fechaNacimiento;
                perfilViejo.Foto = foto;
                perfilViejo.UserName = userName;

                unitOfWork.SaveChanges();

            }
        }

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
                unitOfWork.Usuarios.Add(usuarioNuevo);

                try
                {
                    unitOfWork.SaveChanges();

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

                BuildEmailTemplate(usuarioNuevo.IdUsuario, usuarioNuevo.Token);
            }
        }

        public bool ValidarEmail(string email)
        {
            var emailEncontrado = Get(x => x.Email == email).FirstOrDefault();
            if (emailEncontrado==null)
            {
                return false;
            }
            else
            {
                return true;
            }
           
        }

        public void ValidarUsuario(int IdUsuario, string token)
        {
            var usuarioValidado = Get(x => x.IdUsuario == IdUsuario & x.Token==token).FirstOrDefault();
            usuarioValidado.Activo = true;
            _dbContext.SaveChanges();
        }

        public string VerificarUserName(string posibleUserName, string nombre, string apellido)
        {
                var existingUsers = Get(u => u.UserName.StartsWith(posibleUserName)).ToList();

                //Find the first possible open username.
                if (existingUsers.Count == 0)
                {
                     return posibleUserName;
                }
                else
                {
                    //Iterate through all the possible usernames and create it when a spot is open.
                    for (var i = 1; i <= existingUsers.Count; i++)
                    {
                    string userName = String.Format("{0}.{1}{2}", nombre, apellido, i);

                        if (existingUsers.FirstOrDefault(u => u.UserName == userName) == null)
                        {

                        posibleUserName = userName;

                        }
                    }
                }

            return posibleUserName;

        }

        private void BuildEmailTemplate(int IdUsuario, string token)
        {
                string body = System.IO.File.ReadAllText(HostingEnvironment.MapPath("~/Views/EmailTemplate/") + "Text" + ".cshtml");
                var regInfo = _dbContext.Usuarios.Where(x => x.IdUsuario == IdUsuario).FirstOrDefault();
                var url = "https://localhost:44384/" + "Login/Confirm?IdUsuario="+IdUsuario+"&Token="+token;
                body = body.Replace("@ViewBag.ConfirmationLink", url);
                body = body.ToString();
                BuildEmailTemplate("Su cuenta fue exitosamente creada", body, regInfo.Email);
            }

            private void BuildEmailTemplate(string subjectText, string bodyText, string sendTo)
            {
                string from, to, bcc, cc, subject, body;
                from = "ayudandoenlapandemia@gmail.com";
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
                client.Credentials = new System.Net.NetworkCredential("ayudandoenlapandemia@gmail.com", "ayudandoenlapandemia1!");
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
