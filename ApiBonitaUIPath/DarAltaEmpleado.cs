﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ApiBonitaUIPath {
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class SpecificContent {
        public string idCaso { get; set; }
        public string Hotel { get; set; }
        public string Departamento { get; set; }
        public string Puesto { get; set; }
        public string FechaAlta { get; set; }
        public string FechaFin { get; set; }
        public string Jornada { get; set; }
        public string TipoContrato { get; set; }
        public string Nombre { get; set; }
        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }
        public string Telefono { get; set; }
        public string CorreoElectronico { get; set; }
        public string Minusvalia { get; set; }
        public string GradoMinusvalia { get; set; }
        public string DocumentoIdentidad { get; set; }
        public string Numero { get; set; }
        public string Caducidad { get; set; }
        public string NumeroSSIMSS { get; set; }
        public string CuentaBancaria { get; set; }
        public string Nacionalidad { get; set; }
        public string Sexo { get; set; }
        public string SiglaDomicilio { get; set; }
        public string Domicilio { get; set; }
        public string CodigoPostal { get; set; }
        public string Region { get; set; }
        public string Municipio { get; set; }
        public string Provincia { get; set; }
        public string TelefonoMovil { get; set; }
        public string FechaNacimiento { get; set; }
        public string LugarNacimiento { get; set; }
        public string NivelEstudios { get; set; }
        public string EstadoCivil { get; set; }
        public string UnidadMedicaFamiliar { get; set; }
        public string NumeroRegistroFederal { get; set; }
        public string PensionCompensatoria { get; set; }
        public string HijosACargo { get; set; }
        public string Conyuge { get; set; }
    }

    public class ItemData {
        public string Name { get; set; }
        public SpecificContent SpecificContent { get; set; }
        public string Reference { get; set; }
        public string Priority { get; set; }
    }

    public class Root {
        public ItemData itemData { get; set; }
    }
}
