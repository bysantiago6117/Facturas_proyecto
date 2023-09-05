import { Injectable } from '@angular/core';
import { enviroment } from '../environment/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { factura } from '../interfaces/factura';
import { Estado } from '../interfaces/factura';

@Injectable({
  providedIn: 'root'
})
export class FacturasService {
  private myappUrl: string = enviroment.endpoint; 
  private myapiurl: string = "api/factura";

  constructor(private http: HttpClient) { }

  getFacturas(): Observable<factura[]> {
    return this.http.get<factura[]>(`${this.myappUrl}/${this.myapiurl}`); 
  }

  getFactura(codigo: string): Observable<factura> {
    return this.http.get<factura>(`${this.myappUrl}/${this.myapiurl}/${codigo}`);
  }

  enviarConfirmacion(codigoFactura: string): Observable<any> {
    const url = `${this.myappUrl}/${this.myapiurl}/enviar-confirmacion?codigoFactura=${codigoFactura}`;

    return this.http.post(url, null);
  }

  
  getEstadoTexto(estado: Estado): string {
    switch (estado) {
      case Estado.primerrecordatorio:
        return 'Primer Recordatorio';
      case Estado.segundorecordatorio:
        return 'Segundo Recordatorio';
      case Estado.Desactivado:
        return 'Desactivado';
      default:
        return 'Desconocido';
    }
  }

  
}
