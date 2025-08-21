<h1 align="center">🚗 Minimal API de Estacionamento</h1>

<p align="center">
Esta aplicação é uma <strong>Minimal API</strong> construída em <strong>.NET 9</strong> com foco em gestão de estacionamentos. <br>
Esta API ainda está sendo desenvolvida, refinada e testada conforme minha progressão nos estudos praticados.
</p>

### Linguagem e Framework
![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![.NET 9](https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)

### Banco de Dados
![EF Core](https://img.shields.io/badge/EF_Core-512BD4?style=for-the-badge&logo=entityframework&logoColor=white)
![MySQL](https://img.shields.io/badge/MySQL-4479A1?style=for-the-badge&logo=mysql&logoColor=white)

### Testes e Documentação
![Swagger](https://img.shields.io/badge/Swagger-85EA2D?style=for-the-badge&logo=swagger&logoColor=white)
![Postman](https://img.shields.io/badge/Postman-FF6C37?style=for-the-badge&logo=postman&logoColor=white)

## Funcionalidades

- Criar, listar e remover **contratos mensais** do estacionamento, com associação de veículos e vagas.
- Gerenciar **veículos**, permitindo apenas **listagem** e visualização dos detalhes.
- Criar, remover ou listar **vagas de estacionamento**, incluindo status de ocupação e veículo alocado.
- **Autenticação e autorização** via JWT, com papéis `Administrator` e `Editor`.
- Segue o padrão **Minimal API** do .NET para simplicidade e performance.
- As operações de exclusão garantem que veículos e vagas sejam atualizados corretamente.
- Estrutura de DTOs para controle de retorno e organização de dados.

## Próximos Passos

- Implementar **Rotative Contract** (não incluído por enquanto).
- Adicionar **testes unitários e de integração**.
- Melhorar validações, mensagens de erro e tratamento de exceções.
- Melhorar endpoints de consultas para permitir diversos filtros.
- Documentar a API com **Swagger / OpenAPI**.

## Como Rodar

```bash
# Clonar o repositório
git clone https://github.com/vinicciusbarbosa/DIO-minimal-api.git

# Entrar no diretório
cd minimal-api/Api

# Rodar a aplicação
dotnet watch run
```
Acesse os endpoints via **Postman**, **Insomnia** ou curl.
---

Desenvolvido por: **Vinícius Barbosa**  \
Bootcamp: **GFT Start #7 .NET** 
