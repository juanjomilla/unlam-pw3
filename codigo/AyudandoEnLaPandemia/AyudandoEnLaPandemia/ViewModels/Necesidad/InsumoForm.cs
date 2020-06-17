using System.Web.Mvc;

namespace AyudandoEnLaPandemia.ViewModels.Necesidad
{
    [Bind(Include = "Nombre, Cantidad")]
    public class InsumoForm
    {
        public string Nombre { get; set; }

        public int Cantidad { get; set; }
    }
}