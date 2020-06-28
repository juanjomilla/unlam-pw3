using System.Collections.Generic;
using Repositorio;

namespace AyudandoEnLaPandemia.ViewModels.Necesidad
{
    public class BuscarNecesidadViewModel : BaseViewModel
    {
        public IEnumerable<Necesidades> Necesidades { get; set; }
    }
}