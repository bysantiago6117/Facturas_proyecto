namespace prueba_facturas.Modelos
{
    public class FacturaStoreDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string FacturaCollectionName { get; set; } = null!;

    }
}
