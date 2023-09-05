import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ListadoFacturasComponent } from './components/listado-facturas/listado-facturas.component';
import { VerFacturaComponent } from './components/ver-factura/ver-factura.component';

const routes: Routes = [
  { path: '', redirectTo: 'listaFacturas', pathMatch: "full" },
  {path: 'listaFacturas', component: ListadoFacturasComponent},
  {path: 'verFactura/:codigoFactura', component: VerFacturaComponent}

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
