using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using HistorialDonacionesWebService.Models;
using Servicios;

namespace HistorialDonacionesWebService.Controllers
{
    public class DonacionesController : ApiController
    {
        private readonly ServicioDonaciones _servicioDonaciones;

        public DonacionesController(ServicioDonaciones servicioDonaciones)
        {
            _servicioDonaciones = servicioDonaciones;
        }

        [Route("api/Donaciones/GetDonacionesUsuario/{idUsuario}")]
        public HttpResponseMessage GetDonacionesUsuario(int idUsuario)
        {
            var donacionesInsumos = _servicioDonaciones.GetDonacionesInsumosUsuario(idUsuario);
            var donacionesMonetarias = _servicioDonaciones.GetDonacionesMonetariasUsuario(idUsuario);

            var responseList = new List<HistorialDonacionesResponse>();

            foreach (var donacionInsumo in donacionesInsumos)
            {
                responseList.Add(new HistorialDonacionesResponse
                {
                    NombreNecesidad = donacionInsumo.NecesidadesDonacionesInsumos.Nombre,
                    Estado = GetEstadoNecesidad(donacionInsumo.NecesidadesDonacionesInsumos.Necesidades.Estado, donacionInsumo.NecesidadesDonacionesInsumos.Necesidades.FechaFin),
                    FechaDonacion = DateTime.Now,
                    IdNecesidad = donacionInsumo.NecesidadesDonacionesInsumos.IdNecesidad,
                    MiDonacion = donacionInsumo.Cantidad,
                    TipoDonacion = "Insumo",
                    TotalRecaudado = _servicioDonaciones.GetTotalDonacionesInsumo(donacionInsumo.IdNecesidadDonacionInsumo)
                });
            }

            foreach (var donacionMonetaria in donacionesMonetarias)
            {
                responseList.Add(new HistorialDonacionesResponse
                {
                    NombreNecesidad = donacionMonetaria.NecesidadesDonacionesMonetarias.CBU,
                    Estado = GetEstadoNecesidad(donacionMonetaria.NecesidadesDonacionesMonetarias.Necesidades.Estado, donacionMonetaria.NecesidadesDonacionesMonetarias.Necesidades.FechaFin),
                    FechaDonacion = donacionMonetaria.FechaCreacion,
                    IdNecesidad = donacionMonetaria.NecesidadesDonacionesMonetarias.IdNecesidad,
                    MiDonacion = donacionMonetaria.Dinero,
                    TipoDonacion = "Monetaria",
                    TotalRecaudado = _servicioDonaciones.GetTotalDonacionesMonetaria(donacionMonetaria.IdNecesidadDonacionMonetaria)
                });
            }

            if (!responseList.Any())
            {
                return Request.CreateResponse(System.Net.HttpStatusCode.NotFound, "No se han encontrado donaciones para el usuario");
            }

            return Request.CreateResponse(System.Net.HttpStatusCode.OK, responseList);
        }

        private string GetEstadoNecesidad(int estadoNecesidad, DateTime fechaFin)
        {
            string estado;

            switch (estadoNecesidad)
            {
                case 0:
                case 1:
                    estado = (fechaFin - DateTime.Now).Days > 0 ? "Activa" : "Finalizada";
                    break;
                case 2:
                    estado = "Finalizada";
                    break;
                case 3:
                    estado = "Denunciada";
                    break;
                case 4:
                    estado = "En revisión";
                    break;
                default:
                    estado = "Estado no disponible";
                    break;
            }

            return estado;
        }
    }
}