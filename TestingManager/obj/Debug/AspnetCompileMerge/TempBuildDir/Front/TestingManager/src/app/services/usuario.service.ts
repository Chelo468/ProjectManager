import { Injectable } from '@angular/core';

//Modulos para trabajar con AJAX
import { Http, Response, Headers, RequestOptions} from '@angular/http';
import 'rxjs/add/operator/map';
import { Observable } from 'rxjs/Observable';

import { Comun } from '../comun/comun';

@Injectable()


export class UsuarioService{
	
	public url:string;

	constructor(
			private _http:Http,
			private _comun:Comun
		){
		this.url = _comun.url + "usuario/";
	}

	getUserById(idUser){
		//console.log(this.url + idUser);
		//idUser = idUser + "";
		/*console.log(this.url + "/login/mbrizuela/" + idUser);
		let _headers = new Headers();
		_headers.append('Content-Type', 'application/json');
		_headers.append('Access-Control-Allow-Origin', '*');
		_headers.append('Access-Control-Request-Method', '*');
	    _headers.append('login_name', 'mbrizuela');
	    _headers.append('password', '123456');*/
		return this._http.get(this.url + idUser).map(res => res.json());
	}

	logIn(login_name, password){

		/*var params = {'login_name': 'mbrizuela', 'password': '123456'};
		let headers = new Headers();
		headers.append('nombre', 'mbrizuela');
		headers.append('apellido', '123456');
		headers.append('Access-Control-Allow-Headers', 'Content-Type, Accept, X-Requested-With');
		headers.append('Access-Control-Allow-Origin', '*');
		//let params = new URLSearchParams();
		headers.append('Content-Type', 'application/json');
        //headers.append('login_name', 'mbrizuela');
        //headers.append('password', '123456');
		/*
		let headers = new Headers({ 'Content-Type': 'application/json'});
	    let options = new RequestOptions({ headers: headers });
	    let body = JSON.stringify(params);
		
		
	    return this._http({
				  method: 'GET',
				  url: this.url + "login/",
				  data: params
				}).then(function successCallback(response) {
							this.successLogIn(response);
				    // this callback will be called asynchronously
				    // when the response is available
				  }, function errorCallback(response) {
				    // called asynchronously if an error occurs
				    // or server returns response with an error status.
				  });*///, params: {'json': 'asd'}

		return this._http.get(this.url + "login/" + login_name+"/" + password ).map(res => res.json());
	}

	getArticulos(){
		return this._http.get(this.url).map(res => res.json());
	}



}