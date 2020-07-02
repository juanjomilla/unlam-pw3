using System.Web.Mvc;
using Repositorio;

namespace AyudandoEnLaPandemia.ViewModels.Necesidad
{
    [Bind(Include = "Nombre, Cantidad")]
    public class InsumoForm : NecesidadesDonacionesInsumos
    {
    }
}