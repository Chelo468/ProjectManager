import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { routing, appRoutingProviders } from './app.routing';

import { AppComponent } from './app.component';

//Importacion de componentes
import { UsuarioComponent } from './usuario/usuario.component';
import { LoginComponent } from './login/login.component';
import { ProyectoComponent } from './proyecto/proyecto.component';
import { ProyectoNuevoComponent } from './proyecto/proyecto-nuevo.component';
import { ProyectoDetalleComponent } from './proyecto/proyecto-detalle.component';
import { ProyectoEditarComponent } from './proyecto/proyecto-editar.component';
import { NavbarComponent } from './navbar/navbar.component';


import { Comun } from './comun/comun';

@NgModule({
  declarations: [
    AppComponent,
    UsuarioComponent,
    LoginComponent,
    ProyectoComponent,
    ProyectoNuevoComponent,
    ProyectoDetalleComponent,
    ProyectoEditarComponent,
    NavbarComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpModule,
    routing
  ],
  providers: [appRoutingProviders, Comun],
  bootstrap: [AppComponent]
})
export class AppModule { }
