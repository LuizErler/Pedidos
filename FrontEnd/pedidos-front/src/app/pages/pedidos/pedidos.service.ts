import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Pedido } from './models/pedido.model';

@Injectable({
  providedIn: 'root'
})
export class PedidoService {

  private apiUrl = 'http://localhost:5265/api/pedidos'; 

  constructor(private http: HttpClient) { }

  listar(): Observable<Pedido[]> {
    return this.http.get<Pedido[]>(this.apiUrl);
  }

  obterPorId(id: string): Observable<Pedido> {
    return this.http.get<Pedido>(`${this.apiUrl}/${id}`);
  }

  criar(pedido: Partial<Pedido>): Observable<{ id: string }> {
    return this.http.post<{ id: string }>(this.apiUrl, pedido);
  }

atualizarStatus(id: string, status: number): Observable<void> {
  return this.http.put<void>(
    `${this.apiUrl}/${id}/status`,
    status, 
    {
      headers: {
        'Content-Type': 'application/json'
      }
    }
  );
}

  remover(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
