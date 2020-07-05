using System.Collections.Generic;
using Repositorio;

namespace AyudandoEnLaPandemia.ViewModels
{
    public class DetalleNecesidadViewModel : BaseViewModel
    {
        public Necesidades Necesidad { get; set; }
        public bool EsPropietario { get; set; }

        public string Mensaje { get; set; }

        public IEnumerable<KeyValuePair<string, decimal>> DetalleTotalDonacion { get; set; }
    }
}