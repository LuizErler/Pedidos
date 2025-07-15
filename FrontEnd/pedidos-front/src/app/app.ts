import { Component } from '@angular/core';
import { RouterModule, RouterOutlet } from '@angular/router';
import { CommonModule } from '@angular/common';
import { PedidosComponent } from './pages/pedidos/pedidos.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, RouterModule, RouterOutlet, PedidosComponent],
  templateUrl: './app.html',
  styleUrls: ['./app.css'],
})
export class App {}
