namespace Repositorio.Repositorios
{
    public interface IDonacionesInsumosRepositorio : IRepositorio<DonacionesInsumos>
    {
        decimal GetTotalDonaciones(int idNecesidadDonacionInsumo);
    }
}
