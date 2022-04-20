using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiBonitaUIPath {
    public class ClienteBonitaSAP {
        public string IdCasoBonita { get; set; }
        public string IdCandidatoSAP { get; set; }
    }
    public class Fichero {
        public ICollection<ClienteBonitaSAP> ClienteBonitaSAP { get; set; }
    }
}
