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
    class LoginModelo
    {
        public partial class LoginRequest
        {
            [JsonProperty(PropertyName = "Usuario")]
            public string Usuario { get; set; }
            [JsonProperty(PropertyName = "Password")]
            public string Password { get; set; }
        }

        public partial class LoginResponse
        {
            public string CodigoRespuesta { get; set; }
            public string Mensaje { get; set; }
        }

        public static LoginResponse LoginCheckLock2(string username, string password)
        {
            LoginRequest oRequest = new LoginRequest();
            LoginResponse oResponse = new LoginResponse();
            string jsonRequestMessage = string.Empty;
            oRequest.Usuario = username;
            oRequest.Password = password;
            jsonRequestMessage = JsonConvert.SerializeObject(oRequest);
            oResponse.CodigoRespuesta = "06";
            oResponse.Mensaje = jsonRequestMessage;
            return oResponse;
        }

            public static LoginResponse LoginCheckLock(string username, string password)
        {
            LoginRequest oRequest = new LoginRequest();
            LoginResponse oResponse = new LoginResponse();
            //string urlToken = ConfigurationManager.AppSettings["urlToken"].ToString();
            string urlAPI = "http://13.59.215.98:8081/api/Login";

            string TokenApi = string.Empty;
            string RespCode = string.Empty;
            string jsonRequestMessage = string.Empty;
            string jsonResponseMessage = string.Empty;
            try
            {
                //Sacar Token para la llamada a API CellPay Payware.

                //GrabaLog("Token API: " + TokenApi, "API");

                oRequest.Usuario = username;
                oRequest.Password = password;

                jsonRequestMessage = JsonConvert.SerializeObject(oRequest);

                if (!String.IsNullOrEmpty(jsonRequestMessage))
                {
                    //Realizamos la llamada al API CellPay Payware.
                    using (HttpClient client = new HttpClient(new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip }) { Timeout = TimeSpan.FromSeconds(120) })
                    {
                        HttpRequestMessage request = new HttpRequestMessage()
                        {
                            RequestUri = new Uri(urlAPI),
                            Method = HttpMethod.Post,
                            Content = new StringContent(jsonRequestMessage, Encoding.UTF8, "application/json")
                        };
                        request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                        HttpResponseMessage response = client.SendAsync(request).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            Task<Stream> streamTask = response.Content.ReadAsStreamAsync();
                            Stream stream = streamTask.Result;
                            StreamReader strReader = new StreamReader(stream);
                            jsonResponseMessage = strReader.ReadToEnd();

                            LoginResponse oJsonResponse = JsonConvert.DeserializeObject<LoginResponse>(jsonResponseMessage);
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
