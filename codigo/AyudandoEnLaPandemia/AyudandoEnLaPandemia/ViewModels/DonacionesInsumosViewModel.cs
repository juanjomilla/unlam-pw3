using Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AyudandoEnLaPandemia.ViewModels
{
    public class DonacionesInsumosViewModel : BaseViewModel
    {
        public IEnumerable<NecesidadesDonacionesInsumos> Insumos { get; set; }

    }
}