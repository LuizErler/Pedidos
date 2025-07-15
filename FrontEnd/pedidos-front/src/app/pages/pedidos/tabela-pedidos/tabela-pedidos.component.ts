import { Component, EventEmitter, Input, OnChanges, Output, SimpleChanges } from '@angular/core';
import { Pedido } from '../models/pedido.model';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-tabela-pedidos',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './tabela-pedidos.component.html',
  styleUrls: ['./tabela-pedidos.component.css']
})
export class TabelaPedidosComponent implements OnChanges {
  @Input() pedidos: Pedido[] = [];

  @Output() excluir = new EventEmitter<string>();
  @Output() atualizarStatus = new EventEmitter<{ id: string, status: number }>();

  statusOptions = [
    { label: 'Pendente', value: 0 },
    { label: 'Processando', value: 1 },
    { label: 'Concluído', value: 2 },
    { label: 'Cancelado', value: 3 }
  ];

  statusEnumMap: { [key: string]: number } = {
    Pending: 0,
    Processing: 1,
    Completed: 2,
    Cancelled: 3
  };

  statusMap: { [id: string]: number } = {};

  ngOnChanges(changes: SimpleChanges) {
    if (changes['pedidos'] && this.pedidos) {
      this.pedidos.forEach(pedido => {
        this.statusMap[pedido.id] = this.statusEnumMap[pedido.status] ?? 0;
      });
    }
  }

  removerPedido(id: string) {
    this.excluir.emit(id);
  }

  onStatusChange(id: string, status: number) {
    this.statusMap[id] = status;  // Atualiza localmente
    this.atualizarStatus.emit({ id, status });
  }
}
