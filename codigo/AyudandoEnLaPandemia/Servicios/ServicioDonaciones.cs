using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Web;
using Newtonsoft.Json.Linq;
using Repositorio;
using Servicios.Models;

namespace Servicios
{
    public class ServicioDonaciones : IDisposable
    {
        private readonly UnitOfWork _unitOfWork;

        public ServicioDonaciones(Contexto contexto)
        {
            _unitOfWork = new UnitOfWork(contexto);
        }

        public IEnumerable<DonacionesInsumos> GetDonacionesInsumosUsuario(int idUsuario)
        {
            return _unitOfWork.DonacionesInsumos.Get(x => x.IdUsuario == idUsuario);
        }

        public NecesidadesDonacionesMonetarias GetNecesidadesDonacionesMonetarias(int idNecesidad)
        {
            return _unitOfWork.NecesidadesDonacionesMonetarias.BuscarNecesidad(idNecesidad);
        }

        public IEnumerable<NecesidadesDonacionesInsumos> GetNecesidadesDonacionesInsumos(int idNecesidad)
        {
            return _unitOfWork.NecesidadesDonacionesInsumos.BuscarNecesidad(idNecesidad);
        }

        public IEnumerable<DonacionesMonetarias> GetDonacionesMonetariasUsuario(int idUsuario)
        {
            return _unitOfWork.DonacionesMonetarias.Get(x => x.IdUsuario == idUsuario);
        }

        public int GetTotalDonacionesInsumo(int idNecesidadDonacionInsumo)
        {
            return _unitOfWork.DonacionesInsumos.GetTotalDonaciones(idNecesidadDonacionInsumo);
        }

        public bool ValidarDonacionCompleta(int totalDonaciones, int cantidad)
        {
            if (totalDonaciones>= cantidad)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public decimal GetTotalDonacionesMonetaria(int idNecesidadDonacionMonetaria)
        {
            return _unitOfWork.DonacionesMonetarias.GetTotalDonaciones(idNecesidadDonacionMonetaria);
        }

        public void Dispose()
        {
            if (_unitOfWork != null)
            {
                _unitOfWork.Dispose();
            }
        }

        public void CrearDonacionMonetaria(DonacionesMonetarias donacion)
        {
            _unitOfWork.DonacionesMonetarias.CrearDonacionMonetaria(donacion);
        }

        public string GuardarAdjunto(int idUsuario, HttpPostedFileBase archivo)
        {   
            var extension = Path.GetExtension(archivo.FileName);
            var nombreArchivo = $"{Guid.NewGuid().ToString().Substring(0, 10)}{extension}";
            var path = CrearCarpetaSiNoExiste(idUsuario);

            archivo.SaveAs($"{path}\\{nombreArchivo}");

            return nombreArchivo;
        }

        public IEnumerable<HistorialDonaciones> GetHistorialDonaciones(int idUsuario)
        {
            IEnumerable<HistorialDonaciones> result;

            try
            {
                var request = WebRequest.Create($"https://localhost:44366/api/Donaciones/GetDonacionesUsuario/{idUsuario}");
                var response = request.GetResponse();

                using (var dataStream = response.GetResponseStream())
                {
                    var reader = new StreamReader(dataStream);
                    var responseFromServer = reader.ReadToEnd();

                    var jsonResponse = JArray.Parse(responseFromServer);

                    result = jsonResponse.ToObject<IEnumerable<HistorialDonaciones>>();
                }

                response.Close();
            }
            catch (WebException ex)
            {
                if (((HttpWebResponse)ex.Response).StatusCode == HttpStatusCode.NotFound)
                {
                    result = new List<HistorialDonaciones>();
                }
                else
                {
                    throw;
                }
            }
            catch(Exception)
            {
                throw;
            }

            return result;
        }

        private object CrearCarpetaSiNoExiste(int idUsuario)
        {
            var path = HttpContext.Current.Server.MapPath($"~/Content/donaciones/comprobantes/{idUsuario}");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            return path;
        }

        public void CrearDonacionInsumo(List<DonacionesInsumos> nuevaDonacionInsumolist)
        {
            _unitOfWork.DonacionesInsumos.CrearDonacionInsumo(nuevaDonacionInsumolist);
        }
    }
}
