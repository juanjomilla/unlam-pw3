namespace Repositorio.Repositorios
{
    public interface IDonacionesInsumosRepositorio : IRepositorio<DonacionesInsumos>
    {
        int GetTotalDonaciones(int idNecesidadDonacionInsumo);
        void CrearDonacionInsumo(DonacionesInsumos nuevaDonacionInsumo);
    }
}
