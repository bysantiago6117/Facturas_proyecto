import { Component } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute } from '@angular/router';
import { factura } from 'src/app/interfaces/factura';
import { FacturasService } from 'src/app/services/facturas.service';

@Component({
  selector: 'app-ver-factura',
  templateUrl: './ver-factura.component.html',
  styleUrls: ['./ver-factura.component.css']
})
export class VerFacturaComponent {

  codigo: string;
  factura!: factura
  accesoDesdeCorreo = false;

   constructor(protected _facturaServices: FacturasService, 
    private aRoutre: ActivatedRoute, private _snackBar: MatSnackBar){
     this.codigo =  this.aRoutre.snapshot.paramMap.get('codigoFactura') ?? '';
     console.log(this.codigo);
     
    };

    ngOnInit(){
      
      if (this.codigo.startsWith('correo_')) {
        this.accesoDesdeCorreo = true;
        this.codigo = this.codigo.substr(7)
        this.obtenerFactura(); 
      } else {
        this.obtenerFactura();
      }
    }

   obtenerFactura(){
     this._facturaServices.getFactura(this.codigo).subscribe(data => this.factura = data);
   }

   enviarConfirmacion() {
    if (this.factura && (this.factura.estado === 0 || this.factura.estado === 1)) {
      const codigoFactura = this.factura.codigoFactura;
  
      if (codigoFactura) { 
        this._facturaServices.enviarConfirmacion(codigoFactura).subscribe(
          (response) => {
            console.log(response);
            this.obtenerFactura();
            this._snackBar.open('Confirmación enviada con éxito', 'Cerrar', {
              duration: 5000,
            });
          },
          (error) => {
            this._snackBar.open('Error al enviar la confirmación', 'Cerrar', {
              duration: 5000,
              panelClass: ['error-snackbar'],
            });
          }
        );
      } else {
        console.error('La propiedad codigoFactura es undefined en this.factura');
        
      }
    }
  }

  onPagarclick(){
    if(this.accesoDesdeCorreo){
      window.alert("proximamente: Funcionalidad de pago desde el correo electronio")
    }
  }


}
