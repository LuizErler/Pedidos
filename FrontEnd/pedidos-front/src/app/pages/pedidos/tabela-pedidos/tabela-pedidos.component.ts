import { Component, Input } from '@angular/core';
import { Pedido } from '../models/pedido.model';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-tabela-pedidos',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './tabela-pedidos.component.html',
  styleUrls: ['./tabela-pedidos.component.css']
})
export class TabelaPedidosComponent {
  @Input() pedidos: Pedido[] = [];
}
