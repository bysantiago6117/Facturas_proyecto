namespace prueba_facturas.Servicios.Interfaces
{
    public interface IEmailServices
    {

        Task SendEmailAsync(string toEmail, string subject, string nombreCliente, string facturaCodigo, string estado);
    }
}
