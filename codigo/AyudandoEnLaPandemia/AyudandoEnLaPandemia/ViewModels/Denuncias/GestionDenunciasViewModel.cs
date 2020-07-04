using System.Collections.Generic;

namespace AyudandoEnLaPandemia.ViewModels.Denuncias
{
    public class GestionDenunciasViewModel : BaseViewModel
    {
        public IEnumerable<IEnumerable<string>> ContenidoTabla { get; set; }

        public IEnumerable<string> TitulosTabla { get; set; }
    }
}