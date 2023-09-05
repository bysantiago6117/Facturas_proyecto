using Microsoft.AspNetCore.Mvc;
using prueba_facturas.Dto;
using Microsoft.Extensions.Logging;
using prueba_facturas.Servicios.Implementacion;
using prueba_facturas.Servicios.Interfaces;
using prueba_facturas.Modelos;
using Microsoft.AspNetCore.Cors;

namespace prueba_facturas.Controllers
{


    [Route("api/factura")]
    [ApiController]
    [EnableCors("AllowAnyOrigin")]
    public class FacturaController : ControllerBase
    {

        private readonly IFacturaServices _facturaServicios;

        private readonly ILogger<FacturaController> _logger;


        public FacturaController(ILogger<FacturaController> logger,IFacturaServices facturaServicios)
        {
            _logger = logger;
            _facturaServicios = facturaServicios;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                var facturasDto = await _facturaServicios.GetAsync();
                return Ok(facturasDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener las facturas.");
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        [HttpGet("{codigoFactura}")]
        public async Task<IActionResult> ObtenerFacturaporCodig( string codigoFactura)
        {
            try
            {
                var facturaDto = await _facturaServicios.ObtenerFacturaDtoPorcodigoAsync(codigoFactura);

                if (facturaDto == null)
                {
                    return BadRequest("No se encuentra una factura por ese código");
                }

                return Ok(facturaDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la factura por código");
                return StatusCode(500, "Error interno del servidor al obtener la factura por código");
            }
        }


        [HttpPost("enviar-confirmacion")]
        public async Task<IActionResult> EnviarConfirmacion([FromQuery] string codigoFactura)
        {
            try
            {
                await _facturaServicios.ProcesarFacturaAsync(codigoFactura);

                return Ok(new {message = "Correo de confirmación enviado y estado actualizado correctamente." });
            }
            catch(InvalidOperationException ex) {

                return BadRequest("la factura está en estado Desactivado y no se puede procesar");
            
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al enviar el correo de confirmación.");
                return StatusCode(500, "Error interno del servidor al enviar el correo de confirmación.");
            }
        }
    }
}
