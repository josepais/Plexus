using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace ApiBonitaUIPath.Controllers
{
    [ApiController]
    [Route("api/employee")]
    public class AltaEmpleado : ControllerBase {

        [HttpPost]
        public ActionResult CreateEmployee (Root darAltaEmpleado) {
            var token = "";
            // trust any certificate
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            ServicePointManager.ServerCertificateValidationCallback +=
                (sender, cert, chain, sslPolicyErrors) => { return true; };

            var paramsToken = new Temp() { password = "Hipolito1@@", usernameOrEmailAddress = "Prueba", tenancyName = "Default" };
            using (WebClient wc1 = new WebClient()) {
                var urlQueTeDaToken = "https://rpa.iberostar.com/api/Account/Authenticate";
                wc1.Headers["Content-Type"] = "application/json";
                token = wc1.UploadString(urlQueTeDaToken, JsonConvert.SerializeObject(paramsToken));
            }
            var token1 = JsonConvert.DeserializeObject<Result>(token).result;

            using (WebClient wc = new WebClient()) {
                var URI = "https://rpa.iberostar.com/odata/Queues/UiPathODataSvc.AddQueueItem";
                var json = JsonConvert.SerializeObject(darAltaEmpleado);

                wc.UseDefaultCredentials = true;
                wc.Headers[HttpRequestHeader.Authorization] = $"Bearer {token1}";
                wc.Headers["Content-Type"] = "application/json;charset=UTF-8";
                wc.Headers["X-UIPATH-OrganizationUnitID"] = "1";
                wc.Headers["X-UIPATH-TenantName"] = "Default";
                wc.UploadString(URI, "POST", json);
            }

            return Ok();
        }
        [HttpGet] 
        public string DevolverId ([FromQuery] string idCasoEmpleado) {
            //return idCasoEmpleado + ":" + idEmpleadoSAP;
            //Buscar donde he guardado los datos, el dato a devolver
            //Leemos el contenido del fichero:
            var file = Environment.CurrentDirectory + "\\File\\ids.json";
            var ficheroContentido = System.IO.File.ReadAllText(file);
            Fichero contenidoFicheroDto = JsonConvert.DeserializeObject<Fichero>(ficheroContentido);

            //Obtienes el elemento de la coleccion que tiene el id que nos piden:
            var elemento = contenidoFicheroDto.ClienteBonitaSAP.FirstOrDefault(e => e.IdCasoBonita == idCasoEmpleado);

            return elemento?.IdCandidatoSAP;
        }
        [HttpPost("/Ids")]
        public void GuardarIds(string idCasoBonita, string idCandidatoSAP) {
            //Leemos el contenido del fichero:
            var file = Environment.CurrentDirectory + "\\File\\ids.json";
            var ficheroContentido = System.IO.File.ReadAllText(file);
            
            Fichero contenidoFicheroDto = JsonConvert.DeserializeObject<Fichero>(ficheroContentido);
            //Agregas el contenido al fichero
            if (contenidoFicheroDto != null)
            {
                contenidoFicheroDto.ClienteBonitaSAP.Add(new ClienteBonitaSAP
                {
                    IdCandidatoSAP = idCandidatoSAP,
                    IdCasoBonita = idCasoBonita
                });
            }
            else
            {
                contenidoFicheroDto = new Fichero();
                List<ClienteBonitaSAP> cliente = new List<ClienteBonitaSAP> {
                    new ClienteBonitaSAP() {
                        IdCandidatoSAP = idCandidatoSAP, IdCasoBonita = idCasoBonita
                    }
                };
                contenidoFicheroDto.ClienteBonitaSAP = cliente;
            }
            //Guardas el contenidoFicheroDto en el archivo que tienes
            string json = JsonConvert.SerializeObject(contenidoFicheroDto);
            System.IO.File.WriteAllText(file, json);

        }
        [HttpDelete("{IdCasoBonita}")]
        public void DeleteIdJson(string IdCasoBonita) {
            var file = Environment.CurrentDirectory + "\\File\\ids.json";
            var ficheroContentido = System.IO.File.ReadAllText(file);

            Fichero contenidoFicheroDto = JsonConvert.DeserializeObject<Fichero>(ficheroContentido);
            var elemento = contenidoFicheroDto.ClienteBonitaSAP.FirstOrDefault(e => e.IdCasoBonita == IdCasoBonita);
            if(elemento != null) {
                contenidoFicheroDto.ClienteBonitaSAP.Remove(elemento);

                string json = JsonConvert.SerializeObject(contenidoFicheroDto);
                System.IO.File.WriteAllText(file, json);
            }            
        }
        public class Result {
            public string result { get; set; }
        }
        public class Temp {
            public string password { get; set; }
            public string usernameOrEmailAddress { get; set; }
            public string tenancyName { get; set; }
        }
    }
}
