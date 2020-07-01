using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AyudandoEnLaPandemia.ViewModels.Donaciones
{
    public class DonacionesInsumosListViewModel : BaseViewModel
    {
        public List<DonacionesInsumosViewModel> InsumosList { get; set; }
    }
}