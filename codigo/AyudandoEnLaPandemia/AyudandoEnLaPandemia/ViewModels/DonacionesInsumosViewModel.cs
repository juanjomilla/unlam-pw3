using System.Web.Mvc;

namespace AyudandoEnLaPandemia.ViewModels
{
    [Bind(Include = "IdNecesidadDonacionInsumo, IdNecesidad, Nombre, Cantidad, CantidadTotal, CantidadRestante, CantidadAdonar, statusCompleto")]
    public class DonacionesInsumosViewModel
    {
        public int IdNecesidadDonacionInsumo { get; set; }
        public int IdNecesidad { get; set; }
        public string Nombre { get; set; }
        public int Cantidad { get; set; }
        public int CantidadTotal { get; set; }
        public int CantidadRestante { get; set; }
        public int CantidadAdonar { get; set; }
        public bool statusCompleto { get; set; }

    }
}