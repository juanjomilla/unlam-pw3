using Repositorio;

namespace AyudandoEnLaPandemia.ViewModels
{
    public class DetalleNecesidadViewModel : BaseViewModel
    {
        public Necesidades Necesidad { get; set; }
        public bool EsPropietario { get; set; }
        public bool PuedeValorar { get; set; }
        public string Mensaje { get; set; }
    }
}