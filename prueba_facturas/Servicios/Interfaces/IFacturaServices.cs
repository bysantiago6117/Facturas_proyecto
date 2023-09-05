using prueba_facturas.Dto;
using prueba_facturas.Modelos;

namespace prueba_facturas.Servicios.Interfaces
{
    public interface IFacturaServices
    {
        Task<FacturaDto> ObtenerFacturaDtoPorcodigoAsync(string codigo);

        Task ProcesarFacturaAsync(string facturaId);

        Task<List<FacturaDto>> GetAsync();


    }
}
