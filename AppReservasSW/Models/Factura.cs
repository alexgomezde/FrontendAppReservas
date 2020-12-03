using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppReservasSW.Models
{
    public class Factura
    {
        public int FAC_CODIGO { get; set; }
        public int PAG_CODIGO { get; set; }
        public string FAC_COMPROBANTE { get; set; }
        public string FAC_ESTADO { get; set; }
    }
}