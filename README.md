# API - StoreManager

A API StoreManager é uma solução robusta para gerenciamento de lojas, desenvolvida em ASP.NET Core 8.0 com C#. Esta API facilita operações essenciais de comércio, incluindo o gerenciamento completo de clientes, produtos, pedidos e itens de pedidos. Com uma arquitetura bem estruturada seguindo padrões modernos de desenvolvimento, a API oferece endpoints organizados para todas as operações CRUD necessárias, consultas por faixas de preço e relacionamentos entre entidades. Implementada com Entity Framework Core e documentada via Swagger, a solução é ideal para sistemas de e-commerce e gestão de vendas que necessitam de um backend robusto e escalável.

## Tecnologias Utilizadas

- ASP.NET Core 8.0
- C# 11
- Entity Framework Core
- SQL Server
- Swagger/OpenAPI

## Pré-requisitos
- Visual Studio 2022
- .NET SDK 8.0
- SQL Server
- Git

## Endpoints da API

Acesse a documentação completa da API através do Swagger: `https://localhost:7066/swagger/`

Exemplos de endpoints disponíveis:

### Cliente
- `GET /api/clientes` - Lista todos os clientes
- `GET /api/clientes/{id}` - Obtém um cliente específico pelo ID
- `POST /api/cliente` - Cadastra um novo cliente por meio de um DTO
- `PUT /api/cliente/{id}` - Atualiza um cliente existente pelo ID
- `DELETE /api/cliente/{id}` - Remove um cliente existente pelo ID

### Produto
- `GET /api/produto` - Lista todos os produto
- `GET /api/produto/{id}` - Obtém um produto específico pelo ID
- `GET /api/produtos/preco?minimo=&maximo=` - Obtém uma lista de produtos de acordo com uma faixa de preco
- `POST /api/produto` - Cadastra um novo produto por meio de um DTO
- `PUT /api/produto/{id}` - Atualiza um produto existente pelo ID
- `DELETE /api/produto/{id}` - Remove um produto existente pelo ID

### Pedidos
- `GET /api/pedido` - Lista todos os pedidos
- `GET /api/pedido/{id}` - Obtém um pedido específico pelo ID
- `GET /api/pedido/cliente/{id}` - Obtém uma lista de pedidos de acordo com o ID do cliente
- `POST /api/pedido` - Cadastra um novo pedido por meio de um DTO
- `PUT /api/pedido/{id}` - Atualiza um pedido existente pelo ID
- `DELETE /api/pedido/{id}` - pedido um produto existente pelo ID

### Itens Pedidos
- `GET /api/itens` - Lista todos os itens pedidos
- `GET /api/itens/{id}` - Obtém um item pedido específico pelo ID
- `GET /api/itens/pedido/{id}` - Obtém uma lista de itens pedidos de acordo com o ID do pedido
- `POST /api/itens` - Cadastra um novo item pedido por meio de um DTO
- `PUT /api/itens/{id}` - Atualiza um item pedido existente pelo ID
- `DELETE /api/itens/{id}` - pedido um item pedido existente pelo ID


## Instalação
```bash
# Clone o repositório
git clone https://github.com/VictorBrasileiroo/StoreManager-API.git

# Entre na pasta do projeto
cd StoreManager-API

# Restaure os pacotes
dotnet restore

# Atualize o banco de dados com as migrations
dotnet ef database update
```


## Estrutura do Projeto

```
├── StoreManager.API.sln/
│   ├── StoreManager.API/            # Projeto principal da API
│   │   ├── Controllers/              # Endpoints da API
│   │   ├── Data/                     # Contexto do Banco de dados
│   │   ├── DTOs/                     # Objetos de Transferência de Dados
│   │   ├── Migrations/               # Migrações para o Banco de Dados
│   │   ├── Models/                   # Modelos/Entidades
│   │   ├── Services/                 # Serviços de negócio
│   │       ├── Interfaces/           # Interfaces
            ├── Services/             # Métodos
│   │   └── Program.cs                # Ponto de entrada da aplicação
```


## Configuração

1. Atualize a string de conexão no arquivo `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=seuServidor;Database=suaDatabase;User Id=seuUsuario;Password=suaSenha;"
  }
}
```

2. Configure quaisquer outros parâmetros necessários (chaves de API, configurações de autenticação, etc.)


## Como Executar

### Usando Visual Studio 2022:
1. Abra a solução no Visual Studio 2022
2. Pressione F5 ou utilize o botão "Executar"

### Usando linha de comando:
```bash
cd src/StoreManager.API
dotnet run
```

A API estará disponível em: `https://localhost:7066/` (ou a porta configurada)


## Contribuição

1. Faça um Fork do projeto
2. Crie uma Branch para sua Feature (`git checkout -b feature/AmazingFeature`)
3. Commit suas alterações (`git commit -m 'Add some AmazingFeature'`)
4. Push para a Branch (`git push origin feature/AmazingFeature`)
5. Abra um Pull Request

## Licença

Este projeto está licenciado sob a licença MIT - veja o arquivo LICENSE.md para detalhes.

## Contato

Seu Nome - Victor André Lopes Brasileiro - valb1@ic.ufal.br

Link do Projeto: [https://github.com/VictorBrasileiroo/StoreManager-AP]([https://github.com/seu-usuario/seu-repositorio](https://github.com/VictorBrasileiroo/StoreManager-AP))
