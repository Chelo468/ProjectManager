//Modulos de angular para que funcione el router
import { ModuleWithProviders } from '@angular/core';
import { Routes,RouterModule } from '@angular/router';

//Importar los componentes a los que haremos route
import { UsuarioComponent } from './usuario/usuario.component';
import { LoginComponent } from './login/login.component';
import { ProyectoComponent } from './proyecto/proyecto.component';
import { ProyectoNuevoComponent } from './proyecto/proyecto-nuevo.component';
import { PROYECTO_ROUTES } from './proyecto/proyecto.routing'


const appRoutes: Routes = [

    {path: '', component: LoginComponent},
    {path: 'usuario', component: UsuarioComponent},
    {path: 'home', component: LoginComponent},
    {path: 'proyecto/nuevo', component: ProyectoNuevoComponent},
    {path: 'proyecto/:id', component: ProyectoComponent, children: PROYECTO_ROUTES},
    //{path: 'proyecto/:idUser', component: ProyectoComponent},
    {path: 'usuario/:idUser', component: UsuarioComponent},
    {path: '**', component: LoginComponent},

]

export const appRoutingProviders: any[] = [];


//Modulo de rutas
export const routing: ModuleWithProviders = RouterModule.forRoot(appRoutes);