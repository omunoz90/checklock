using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace App_Control_Parental_Demo.Models
{
    class UsuariosModelo
    {
        public partial class AltaUsuarioRequest
        {
            public string CorreoElectronico { get; set; }
            public string Password { get; set; }
            public string Nombre { get; set; }
            public string SegundoNombre { get; set; }
            public string ApellidoPaterno { get; set; }
            public string ApellidoMaterno { get; set; }
            public int Edad { get; set; }
            public string Telefono { get; set; }
        }

        public partial class AltaUsuarioResponse
        {
            public string CodigoRespuesta { get; set; }
            public string Mensaje { get; set; }
        }

        public static AltaUsuarioResponse AltaUsuarioCheckLock(string correoElectronico, string password, string nombre
                                                                , string segundoNombre, string apellidoPaterno, string apellidoMaterno, int edad
                                                                , string telefono)
        {
            AltaUsuarioRequest oRequest = new AltaUsuarioRequest();
            AltaUsuarioResponse oResponse = new AltaUsuarioResponse();
            //string urlToken = ConfigurationManager.AppSettings["urlToken"].ToString();
            string urlAPI = "http://13.59.215.98:8081/:8081/api/Usuarios";

            string TokenApi = string.Empty;
            string RespCode = string.Empty;
            string jsonRequestMessage = string.Empty;
            string jsonResponseMessage = string.Empty;
            try
            {
                //Sacar Token para la llamada a API CellPay Payware.

                //GrabaLog("Token API: " + TokenApi, "API");

                oRequest.CorreoElectronico = correoElectronico;
                oRequest.Password = password;
                oRequest.Nombre = nombre;
                oRequest.SegundoNombre = segundoNombre;
                oRequest.ApellidoPaterno = apellidoPaterno;
                oRequest.ApellidoMaterno = apellidoMaterno;
                oRequest.Edad = edad;
                oRequest.Telefono = telefono;

                jsonRequestMessage = JsonConvert.SerializeObject(oRequest);


                if (!String.IsNullOrEmpty(jsonRequestMessage))
                {
                    //Realizamos la llamada al API CellPay Payware.
                    using (HttpClient client = new HttpClient(new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip }) { Timeout = TimeSpan.FromSeconds(120) })
                    {
                        HttpRequestMessage request = new HttpRequestMessage()
                        {
                            RequestUri = new Uri(urlAPI),
                            Method = HttpMethod.Post
                        };
                        request.Content = new StringContent(jsonRequestMessage, Encoding.UTF8, "text/json");
                        request.Headers.Clear();
                        request.Content.Headers.ContentType = new MediaTypeHeaderValue("text/json");
                        HttpResponseMessage response = client.SendAsync(request).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            Task<Stream> streamTask = response.Content.ReadAsStreamAsync();
                            Stream stream = streamTask.Result;
                            StreamReader strReader = new StreamReader(stream);
                            jsonResponseMessage = strReader.ReadToEnd();

                            AltaUsuarioResponse oJsonResponse = JsonConvert.DeserializeObject<AltaUsuarioResponse>(jsonResponseMessage);
                            if (oJsonResponse != null)
                            {
                                //Respuesta del API
                                oResponse.CodigoRespuesta = oJsonResponse.CodigoRespuesta;
                                oResponse.Mensaje = oJsonResponse.Mensaje;

                            }
                            else
                            {
                                oResponse.CodigoRespuesta = "96";
                                oResponse.Mensaje = "No hay parametros en la respuesta.";

                            }
                        }
                        else
                        {

                            oResponse.CodigoRespuesta = "06";
                            oResponse.Mensaje = response.ReasonPhrase;

                        }
                    }
                }
                else
                {

                    oResponse.CodigoRespuesta = "06";
                    oResponse.Mensaje = "Error en el mensaje de la petición.";

                }

            }
            catch (AggregateException ex)
            {
                if (ex.InnerException is TaskCanceledException)
                {
                    //ERROR de Excepcion.                    
                    oResponse.CodigoRespuesta = "06";
                    oResponse.Mensaje = ex.InnerException.Message;

                }
                else
                {
                    oResponse.CodigoRespuesta = "06";
                    oResponse.Mensaje = ex.Message;
                }
            }

            return oResponse;
        }
    }
}
