export interface ItemPedido {
  productId: string;
  productName: string;
  unitPrice: number;
  quantity: number;
  totalPrice: number;
}

export interface Pedido {
  id: string;
  customerId: string;
  customerName: string;
  orderDate: string; // ISO string, pode ser Date também se quiser
  totalAmount: number;
  status: string;
  itens: ItemPedido[];
}
