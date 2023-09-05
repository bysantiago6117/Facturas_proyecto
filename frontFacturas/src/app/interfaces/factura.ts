export interface factura {
   codigoFactura: string;
   nombreCliente: string;
   ciudad: string;
   nit: string;
   totalFactura: number;
   subTotal: number;
   iva: number;
   retencion: number;
   fechaCreacion: Date;
   estado: Estado;
   pagada: boolean;
   fechaPago: Date | null;
}

export enum Estado {
    primerrecordatorio = 0,
    segundorecordatorio = 1,
    Desactivado = 2,
}