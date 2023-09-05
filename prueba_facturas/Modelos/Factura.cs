using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver.Encryption;

namespace prueba_facturas.Modelos

{
    public class Factura
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)] 
        public string id { get; set; }


        public string CodigoFactura { get; set; }
        
        public Cliente Cliente { get; set; }

        public string Ciudad { get; set; }

        public string Nit { get; set; }

        public decimal TotalFactura { get; set; }

        public decimal SubTotal { get; set; }

        public decimal Iva { get; set; }

        public decimal Retencion { get; set; }

        public DateTime FechaCreacion { get; set; }

        public Estado Estado { get; set; }

        public bool Pagada { get; set; }

        public DateTime? FechaPago { get; set; }

    }
}
