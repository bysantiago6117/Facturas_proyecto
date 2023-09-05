import { AfterViewInit, Component, ViewChild } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';

import { factura } from 'src/app/interfaces/factura';
import { Estado } from 'src/app/interfaces/factura';
import { FacturasService } from 'src/app/services/facturas.service';




@Component({
  selector: 'app-listado-facturas',
  templateUrl: './listado-facturas.component.html',
  styleUrls: ['./listado-facturas.component.css']
})
export class ListadoFacturasComponent{
 
  displayedColumns: string[] = ['codigoFactura',
   'nombreCliente', 'ciudad','nit', 'totalFactura', 
   'subTotal','iva','retencion', 'fechaCreacion','estado', 
   'pagada', 'fechaPago','Acciones'];
  dataSource = new MatTableDataSource<factura>()

  

  constructor(protected _facturaService: FacturasService){

  }

  ngOnInit(): void {
    this.obtenerFacturas();
   }

  obtenerFacturas(){
    this._facturaService.getFacturas().subscribe(data => {this.dataSource.data = data})
  }


}
