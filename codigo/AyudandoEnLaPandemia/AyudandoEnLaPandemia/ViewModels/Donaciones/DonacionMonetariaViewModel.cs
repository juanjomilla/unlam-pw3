using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AyudandoEnLaPandemia.ViewModels.Donaciones
{
    public class DonacionMonetariaViewModel : BaseViewModel
    {
        public int IdUsuario { get; set; }
        public Double DineroDonado { get; set; }
        public Double totalRestante { get; set; }
        public Double Dinero { get; set; }
        public int DineroAdonar { get; set; }
        public int IdNecesidadDonacionMonetaria { get; set; }
        public string CBU { get; set; }
    }
}