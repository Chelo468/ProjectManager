import { Component } from '@angular/core';

//Importamos el router para aceptar parametros
import { Router, ActivatedRoute, Params} from '@angular/router';

//Importo el servicio de usuario
import { UsuarioService } from '../services/usuario.service';

@Component({

	selector: 'usuario',
	templateUrl: './usuario.component.html',
	providers: [UsuarioService]

	})

export class UsuarioComponent{

	public idUser;
	public usuario;

	constructor(
		private _route:ActivatedRoute,
		private _router:Router,
		private _usuarioService:UsuarioService
		){
		//this.usuario = new Usuario("","","","","","","","");
	}

	ngOnInit(){
		this._route.params.forEach((params:Params) => {
				this.idUser = params['idUser'];
				this.buscarEmpleadoPorId(this.idUser);

				//this.usuario.login_name = JSON.Parse(respuesta);
				//console.log(this.usuario);
			});	
	}

	buscarEmpleadoPorId(id){
		return this._usuarioService.getUserById(id).subscribe(
				result => {
					console.log(result);
					this.usuario = JSON.parse(result);
					},
				error => {
					var errorMessage = <any>error;
					console.log(errorMessage);
				}
			);
	}
}