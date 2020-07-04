using System.Collections.Generic;

namespace AyudandoEnLaPandemia.ViewModels
{
    public class TablaPartialViewModel
    {
        public IEnumerable<IEnumerable<string>> ContenidoTabla { get; set; }
        public IEnumerable<string> TitulosTabla { get; set; }
    }
}