//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Repositorio
{
    using System;
    using System.Collections.Generic;
    
    public partial class DonacionesMonetarias
    {
        public int IdDonacionMonetaria { get; set; }
        public int IdNecesidadDonacionMonetaria { get; set; }
        public int IdUsuario { get; set; }
        public decimal Dinero { get; set; }
        public string ArchivoTransferencia { get; set; }
        public System.DateTime FechaCreacion { get; set; }
    
        public virtual NecesidadesDonacionesMonetarias NecesidadesDonacionesMonetarias { get; set; }
        public virtual Usuarios Usuarios { get; set; }
    }
}
