# 🧾 Pedidos API

API RESTful para gerenciamento de pedidos de uma loja online, construída com foco em escalabilidade e boas práticas de arquitetura de software.

---

## 🧠 Arquitetura

- **Clean Architecture**
- **CQRS (Command Query Responsibility Segregation)**
- **DDD (Domain-Driven Design)**
- **Persistência de escrita**: SQL Server com Entity Framework Core
- **Persistência de leitura**: MongoDB
- **MediatR** para desacoplamento via comandos/queries
- **.NET 8**, 100% com projetos separados

---

## 🛠️ Requisitos

### 💻 Tecnologias

| Tecnologia        | Versão mínima |
|-------------------|---------------|
| [.NET SDK](https://dotnet.microsoft.com/en-us/download) | 8.0           |
| [Visual Studio](https://visualstudio.microsoft.com/) | 2022 (com workload ASP.NET + EF) |
| [SQL Server](https://www.microsoft.com/pt-br/sql-server/sql-server-downloads) | Express ou superior |
| [MongoDB Community Server](https://www.mongodb.com/try/download/community) | 6.0+           |
| [MongoDB Compass (opcional)](https://www.mongodb.com/try/download/compass) | -             |
| dotnet-ef CLI (global) | `dotnet tool install --global dotnet-ef` |

---

## 🧱 Estrutura de Projetos

Pedidos.sln

│
├── Pedidos.API # Camada de apresentação (controllers, Program.cs)

├── Pedidos.Application # Camada de aplicação (comandos, queries, handlers)

├── Pedidos.Domain # Entidades de domínio, interfaces, enums

├── Pedidos.Infrastructure # Acesso a dados (SQL Server + MongoDB), repositórios, seeds
