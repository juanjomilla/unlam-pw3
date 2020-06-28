using System;

namespace Servicios.Models
{
    public class HistorialDonaciones
    {
        public string NombreNecesidad { get; set; }

        public DateTime FechaDonacion { get; set; }

        public string TipoDonacion { get; set; }

        public int Estado { get; set; }

        public decimal TotalRecaudado { get; set; }

        public decimal MiDonacion { get; set; }

        public int IdNecesidad { get; set; }
    }
}