using AutoMapper;
using prueba_facturas.Modelos;

namespace prueba_facturas.Dto
{
    public class MapperProfile: Profile
    {
        public MapperProfile() {

            CreateMap<Factura, FacturaDto>().ForMember(dest => dest.NombreCliente, opt => opt.MapFrom(src => src.Cliente.Nombre));
        }
        
    }
}
