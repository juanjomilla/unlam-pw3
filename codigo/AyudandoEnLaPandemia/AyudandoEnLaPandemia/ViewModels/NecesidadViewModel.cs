using System.Collections.Generic;
using Repositorio;

namespace AyudandoEnLaPandemia.ViewModels
{
    public class NecesidadViewModel : BaseViewModel
    {
        public IEnumerable<Necesidades> Necesidades { get; set; }

        public bool Editable { get; set; }
    }
}