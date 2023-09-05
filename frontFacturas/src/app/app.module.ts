import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

//components
import { ListadoFacturasComponent } from './components/listado-facturas/listado-facturas.component';
import { VerFacturaComponent } from './components/ver-factura/ver-factura.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

//modulos de angular-material

import {MatTableModule} from '@angular/material/table';
import {MatIconModule } from '@angular/material/icon';
import {MatTooltipModule} from '@angular/material/tooltip';
import { HttpClientModule } from '@angular/common/http';
import {MatCardModule} from '@angular/material/card';
import {MatButtonModule} from '@angular/material/button';
import {MatSnackBarModule} from '@angular/material/snack-bar';


@NgModule({
  declarations: [
    AppComponent,
    ListadoFacturasComponent,
    VerFacturaComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MatTableModule, 
    MatIconModule,
    MatTooltipModule,
    HttpClientModule, 
    MatCardModule,
    MatButtonModule,
    MatSnackBarModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
