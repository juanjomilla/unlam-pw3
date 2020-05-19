using System.Collections.Generic;
using Entidades;

namespace AyudandoEnLaPandemia.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        public IEnumerable<Necesidad> TopNecesidades { get; set; }
    }
}