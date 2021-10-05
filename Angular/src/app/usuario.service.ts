import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class UsuarioService {

  constructor(private httpClient: HttpClient) { }

  getUsuarios() {
    const resultado = this.httpClient.get<any[]>("https://jsonplaceholder.typicode.com/todos");
    console.log(resultado);
    return resultado;
  }
}
