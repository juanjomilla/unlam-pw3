﻿namespace Repositorio.Repositorios
{
    public interface IDonacionesMonetariasRepositorio : IRepositorio<DonacionesMonetarias>
    {
        decimal GetTotalDonaciones(int idNecesidadDonacionMonetaria);
        void CrearDonacionMonetaria(DonacionesMonetarias donacion);
     }
}
