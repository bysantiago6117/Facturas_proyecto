using MongoDB.Driver;
using Microsoft.Extensions.Options;
using prueba_facturas.Modelos;
using AutoMapper;
using prueba_facturas.Dto;
using prueba_facturas.Servicios.Interfaces;

namespace prueba_facturas.Servicios.Implementacion

{
    public class FacturaServicios: IFacturaServices
    {

        private readonly IMongoCollection<Factura> _facturascollection;

        private readonly IEmailServices _emailService;

        private readonly IMapper _mapper;

        public FacturaServicios(IOptions<FacturaStoreDatabaseSettings> FacturaStoreDatabaseSettings, IEmailServices emailService, IMapper mapper)
        {
            var mongoClient = new MongoClient(FacturaStoreDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(FacturaStoreDatabaseSettings.Value.DatabaseName);

            _facturascollection = mongoDatabase.GetCollection<Factura>(FacturaStoreDatabaseSettings.Value.FacturaCollectionName);
            _emailService = emailService;
            _mapper = mapper;
        }


        public async Task<List<FacturaDto>> GetAsync()
        {
            var facturas = await _facturascollection.Find(_ => true).ToListAsync();
            var facturasDto = _mapper.Map<List<FacturaDto>>(facturas);
            return facturasDto;
        }

        public async Task<FacturaDto> ObtenerFacturaDtoPorcodigoAsync(string codigo)
        {
            var factura = await _facturascollection.Find(x => x.CodigoFactura == codigo).FirstOrDefaultAsync();
            var facturaDto = _mapper.Map<FacturaDto>(factura);
            return facturaDto;

        }

        public async Task ProcesarFacturaAsync(string facturaId)
        {

            var factura = await _facturascollection.Find(x => x.CodigoFactura == facturaId).FirstOrDefaultAsync();

            if (factura != null)
            {
                  
                if(factura.Estado == Estado.Desactivado)
                {
                    throw new InvalidOperationException("La factura esta en estado abandonado y no se puede procesar");
                }
                     
                var correoCliente = factura.Cliente.CorreoElectronico;

                if (factura.Estado == Estado.primerRecordatorio)
                {

                    await _emailService.SendEmailAsync(correoCliente, "Segundo Recordatorio",factura.Cliente.Nombre, factura.CodigoFactura,"SegundoRecordatorio");

                    factura.Estado = Estado.SegundoRecordatorio;
                }
                else if (factura.Estado == Estado.SegundoRecordatorio)
                {
                    await _emailService.SendEmailAsync(correoCliente, "Desactivación",factura.Cliente.Nombre, factura.CodigoFactura, "Desactivado");
                    factura.Estado = Estado.Desactivado;
                }

                await _facturascollection.ReplaceOneAsync(x => x.CodigoFactura == facturaId, factura);
            }
        }

    }
}
