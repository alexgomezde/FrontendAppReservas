using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppReservasSW.Models
{
    public class Pago
    {
        public int PAG_CODIGO { get; set; }
        public int RES_CODIGO { get; set; }
        public System.DateTime PAG_FECHA { get; set; }
        public string PAG_TIPO { get; set; }
        public int TPA_CODIGO { get; set; }
        public string PAG_ESTADO { get; set; }
    }
}