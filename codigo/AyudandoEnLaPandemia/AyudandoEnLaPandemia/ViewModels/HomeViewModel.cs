using System.Collections.Generic;
using Repositorio;

namespace AyudandoEnLaPandemia.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        public IEnumerable<Necesidades> MisNecesidades { get; set; }

        public IEnumerable<Necesidades> Necesidades { get; set; }
    }
}