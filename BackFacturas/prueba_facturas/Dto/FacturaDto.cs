using prueba_facturas.Modelos;

namespace prueba_facturas.Dto
{
    public class FacturaDto
    {

        public string CodigoFactura { get; set; }

        public string NombreCliente { get; set; }

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
