using System.Collections.Generic;
using Servicios.Models;

namespace AyudandoEnLaPandemia.ViewModels.Donaciones
{
    public class HistorialDonacionesViewModel : BaseViewModel
    {
        public IEnumerable<HistorialDonaciones> ListaDonaciones { get; set; }
    }
}