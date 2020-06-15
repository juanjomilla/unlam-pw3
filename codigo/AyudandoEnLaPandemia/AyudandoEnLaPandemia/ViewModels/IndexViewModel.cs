using System.Collections.Generic;
using Repositorio;

namespace AyudandoEnLaPandemia.ViewModels
{
    public class IndexViewModel : BaseViewModel
    {
        public IEnumerable<Necesidades> TopNecesidades { get; set; }
    }
}