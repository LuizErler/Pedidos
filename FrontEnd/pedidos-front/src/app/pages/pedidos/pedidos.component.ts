import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
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
modalAberto = false;
mensagemModal = '';
mensagemSucessoModal = true;
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
  constructor(private pedidoService: PedidoService, private cdr: ChangeDetectorRef) {}

   adicionarItem() {
    this.novoPedido.itens.push({ productId: '', quantity: 1 });
  }

  removerItem(index: number) {
    this.novoPedido.itens.splice(index, 1);
  }

  criarPedido() {
  const pedidoParaEnviar: Partial<Pedido> = {
    customerId: this.novoPedido.customerId,
    itens: this.novoPedido.itens.map(item => ({
      productId: item.productId,
      quantity: item.quantity,
      productName: '',
      unitPrice: 0,
      totalPrice: 0
    }))
  };

  this.pedidoService.criar(pedidoParaEnviar).subscribe({
    next: (res) => {
      this.mensagemModal = 'Pedido criado com sucesso! ID: ' + res.id;
      this.mensagemSucessoModal = true;
      this.modalAberto = true;
      this.cdr.detectChanges(); // <-- força atualização da view

      this.novoPedido = {
        customerId: '',
        itens: [{ productId: '', quantity: 1 }]
      };

      this.carregarPedidos();
    },
    error: (err) => {
      this.mensagemModal = 'Erro ao criar pedido. Tente novamente.';
      this.mensagemSucessoModal = false;
      this.modalAberto = true;
      this.cdr.detectChanges(); // <-- força também no erro
      console.error('Erro ao criar pedido:', err);
    }
  });
}

fecharModal() {
  this.modalAberto = false;
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
removerPedido(id: string) {
  this.pedidoService.remover(id).subscribe({
    next: () => {
      this.mensagemModal = 'Pedido removido com sucesso!';
      this.mensagemSucessoModal = true;
      this.modalAberto = true;

      this.carregarPedidos();
      this.cdr.detectChanges(); // Atualiza a view pra garantir que o modal apareça
    },
    error: (erro) => {
      console.error('Erro ao remover pedido:', erro);
      this.mensagemModal = 'Erro ao remover o pedido. Tente novamente.';
      this.mensagemSucessoModal = false;
      this.modalAberto = true;
      this.cdr.detectChanges();
    }
  });
}

atualizarStatusPedido(payload: { id: string, status: number }) {
  this.pedidoService.atualizarStatus(payload.id, payload.status).subscribe({
    next: () => {
      this.mensagemModal = 'Status do pedido atualizado com sucesso!';
      this.mensagemSucessoModal = true;
      this.modalAberto = true;

      this.carregarPedidos();
      this.cdr.detectChanges();
    },
    error: (erro) => {
      console.error('Erro ao atualizar status:', erro);
      this.mensagemModal = 'Erro ao atualizar o status. Tente novamente.';
      this.mensagemSucessoModal = false;
      this.modalAberto = true;
      this.cdr.detectChanges();
    }
  });
}


}

