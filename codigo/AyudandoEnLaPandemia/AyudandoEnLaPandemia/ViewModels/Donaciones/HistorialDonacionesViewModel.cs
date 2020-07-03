using System.Collections.Generic;

namespace AyudandoEnLaPandemia.ViewModels.Donaciones
{
    public class HistorialDonacionesViewModel : BaseViewModel
    {
        public IEnumerable<IEnumerable<string>> ContenidoTabla { get; set; }

        public IEnumerable<string> TitulosTabla { get; set; }
    }
}