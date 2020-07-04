using System.Web.Mvc;
using AyudandoEnLaPandemia.Const;

namespace AyudandoEnLaPandemia.Controllers
{
    public class BaseController : Controller
    {
        protected int GetIdUsuario()
        {
            return Session[Constantes.IdUsuarioSessionKey] != null ? (int)Session[Constantes.IdUsuarioSessionKey] : 0;
        }

        protected void SetMensajeError(string mensaje)
        {
            TempData[Constantes.MensajeErrorTempData] = mensaje;
        }
    }
}