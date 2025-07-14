import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { PedidoService } from './pedidos.service';
import { Pedido } from './models/pedido.model';
import { TabelaPedidosComponent } from './tabela-pedidos/tabela-pedidos.component';

@Component({
  selector: 'app-pedidos',
  standalone: true,
  imports: [CommonModule, FormsModule, TabelaPedidosComponent], 
  templateUrl: './pedidos.component.html',
  styleUrls: ['./pedidos.component.css']
})
export class PedidosComponent  implements OnInit  {
 ngOnInit(): void {
    this.carregarPedidos();
  }
  

  novoPedido = {
    customerId: '',
    itens: [
      { productId: '', quantity: 1 }
    ]
    
  };
  pedidos: Pedido[] = [];
  constructor(private pedidoService: PedidoService) {}

   adicionarItem() {
    this.novoPedido.itens.push({ productId: '', quantity: 1 });
  }

  removerItem(index: number) {
    this.novoPedido.itens.splice(index, 1);
  }

  criarPedido() {
    console.log('Pedido enviado:', this.novoPedido);
    // Aqui depois você chama o serviço que envia o pedido pra API
  }

   carregarPedidos() {
  this.pedidoService.listar().subscribe({
    next: (dados) => {
      this.pedidos = dados;
    },
    error: (erro) => {
      console.error('Erro ao carregar pedidos:', erro);
    }
  });
}
}

