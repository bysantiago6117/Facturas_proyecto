using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Core.Events;
using Moq;
using prueba_facturas.Controllers;
using prueba_facturas.Dto;
using prueba_facturas.Modelos;
using prueba_facturas.Servicios.Implementacion;
using prueba_facturas.Servicios.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Pruebas_unitarias.ServicesShould
{
    public class FacturaControllerTest
    {
        [Fact]
        public async Task GetAsync_DebeRetornarOkConFacturasDto()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<FacturaController>>();
            var facturaServicesMock = new Mock<IFacturaServices>();

            var facturaController = new FacturaController(loggerMock.Object, facturaServicesMock.Object);

            var facturasDto = new List<FacturaDto>(); 
            facturaServicesMock.Setup(s => s.GetAsync()).ReturnsAsync(facturasDto);

            // Act
            var result = await facturaController.GetAsync();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<FacturaDto>>(okResult.Value);
            Assert.Equal(facturasDto.Count, model.Count());
        }

        [Fact]
        public async Task ObtenerFacturaporCodig_DebeRetornarOkConFacturaDto()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<FacturaController>>();
            var facturaServicesMock = new Mock<IFacturaServices>();

            var facturaController = new FacturaController(loggerMock.Object, facturaServicesMock.Object);

            var codigoFactura = "F001"; 
            var facturaDto = new FacturaDto(); 
            facturaServicesMock.Setup(s => s.ObtenerFacturaDtoPorcodigoAsync(codigoFactura)).ReturnsAsync(facturaDto);

            // Act
            var result = await facturaController.ObtenerFacturaporCodig(codigoFactura);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsType<FacturaDto>(okResult.Value);
            Assert.Same(facturaDto, model);
        }

        [Theory]
        [InlineData(Estado.Desactivado)]
        [InlineData(Estado.primerRecordatorio)]
        [InlineData(Estado.SegundoRecordatorio)]
        public async Task EnviarConfirmacion_InvalidEstado_ReturnsExpectedResult(Estado estado)
        {
            // Arrange
            var _logger = new Mock<ILogger<FacturaController>>();
            var _facturaServicios = new Mock<IFacturaServices>();

            if (estado == Estado.Desactivado)
            {
                _facturaServicios.Setup(x => x.ProcesarFacturaAsync(estado.ToString()))
                                 .ThrowsAsync(new InvalidOperationException($"la factura está en estado {estado} y no se puede procesar"));
            }
            else
            {
              
                _facturaServicios.Setup(x => x.ProcesarFacturaAsync(estado.ToString()));
            }

            var controller = new FacturaController(_logger.Object, _facturaServicios.Object);

            // Act
            var result = await controller.EnviarConfirmacion(estado.ToString());

            // Assert
            if (estado == Estado.Desactivado)
            {
                var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
                Assert.Equal($"la factura está en estado {estado} y no se puede procesar", badRequestResult.Value);
            }
            else
            {
                var okResult = Assert.IsType<OkObjectResult>(result);
                var message = Assert.IsType<string>(okResult.Value.GetType().GetProperty("message").GetValue(okResult.Value, null));
                Assert.Equal("Correo de confirmación enviado y estado actualizado correctamente.", message);
            }
        }


    }
}
