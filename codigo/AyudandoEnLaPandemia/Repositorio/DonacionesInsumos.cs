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
    
    public partial class DonacionesInsumos
    {
        public int IdDonacionInsumo { get; set; }
        public int IdNecesidadDonacionInsumo { get; set; }
        public int IdUsuario { get; set; }
        public int Cantidad { get; set; }
    
        public virtual NecesidadesDonacionesInsumos NecesidadesDonacionesInsumos { get; set; }
        public virtual Usuarios Usuarios { get; set; }
    }
}
