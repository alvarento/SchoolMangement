📚 SchoolManagement API
API desenvolvida em .NET 10 para gerenciamento escolar, com foco em boas práticas de arquitetura, validação de domínio e segurança.

🚀 Tecnologias utilizadas
ASP.NET Core 10 – construção da API REST

Entity Framework Core – acesso e mapeamento ao banco de dados

FluentValidation – validação de dados

JWT (JSON Web Token) – autenticação e autorização

Swagger / OpenAPI – documentação interativa

ProblemDetails (RFC 7807) – padronização das respostas de erro

🏗️ Arquitetura
O projeto segue princípios de Clean Architecture e DDD (Domain-Driven Design):

Domain: entidades e value objects (ex.: Email, Nome, Idade)

Application: regras de negócio e casos de uso

Infrastructure: persistência e configurações do EF Core

API: camada de apresentação, controllers e filtros

⚙️ Funcionalidades principais
Cadastro e gerenciamento de usuários e alunos

Autenticação via JWT

Validação de regras de domínio (ex.: e-mail válido, cálculo de idade)

Tratamento global de exceções com ExceptionFilter

Retorno de erros padronizados em formato ProblemDetails

📑 Exemplo de resposta de erro (RFC 7807)
json
{
  "type": "https://example.com/errors/invalid-email",
  "title": "E-mail inválido",
  "status": 400,
  "detail": "O e-mail informado não está em um formato válido.",
  "instance": "/api/usuarios"
}
🔐 Autenticação
As requisições protegidas exigem envio do token JWT no header:

Código
Authorization: Bearer <seu_token>
🛠️ Como rodar o projeto
Clone o repositório:

bash
git clone https://github.com/seuusuario/SchoolManagement.git
Configure a connection string no appsettings.json.

Execute as migrations:

bash
dotnet ef database update
Rode a aplicação:

bash
dotnet run
Acesse a documentação Swagger em:

Código
https://localhost:5001/swagger
📌 Próximos passos
Implementar testes automatizados

Melhorar logs e observabilidade

Expandir módulos de gestão (turmas, disciplinas, notas)

👨‍💻 Autor
Desenvolvido por Álvaro  
Salvador, Bahia – Brasil 🌎