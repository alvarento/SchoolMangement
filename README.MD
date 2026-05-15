# 📚 SchoolManagement API

API robusta desenvolvida com **.NET 8** (preparada para .NET 10) voltada para o gerenciamento escolar. O projeto aplica conceitos avançados de **Domain-Driven Design (DDD)**, **Clean Architecture** e técnicas modernas de persistência com **Entity Framework Core**.

---

## 🚀 Diferenciais Técnicos e Inovações

Este projeto não é apenas um CRUD. Ele implementa padrões de mercado utilizados em aplicações escaláveis:

* **Herança Table-Per-Type (TPT):** Implementação de hierarquia entre `Pessoa`, `Aluno` e `Professor`, garantindo a normalização do banco de dados enquanto mantém a integridade referencial.
* **Value Objects (VOs) Blindados:** Uso de VOs para `Nome`, `CPF`, `Email` e `Idade`, garantindo que regras de negócio (como validação de formato e lógica de cálculo) nunca saiam do domínio.
* **Operadores de Conversão Implícita:** Os Value Objects utilizam `implicit operator` para permitir o uso fluído no código (ex: `aluno.Idade` retorna o objeto, mas se comporta como `int` em atribuições), mantendo o código limpo e expressivo.
* **Identificadores UUID v7:** Uso de Guids sequenciais baseados em tempo para a tabela de Usuários, melhorando drasticamente a performance de inserção e indexação em comparação ao UUID v4 tradicional.

---

## 🏗️ Arquitetura e Organização

O sistema foi dividido em quatro camadas principais seguindo a **Clean Architecture**:

1.  **Domain:** O coração do sistema. Contém as entidades, interfaces de repositório e os Value Objects com suas regras de autovalidação.
2.  **Application:** Contém os Use Cases (Casos de Uso), DTOs e serviços de aplicação que orquestram a lógica do sistema.
3.  **Infrastructure:** Implementação da persistência com EF Core, configurações de Fluent API para TPT e implementações de repositórios.
4.  **API:** Camada de entrada, utilizando `Controllers`, `Filters` para exceções globais e middlewares de autenticação JWT.

---

## ⚙️ Tecnologias Utilizadas

* **Back-end:** ASP.NET Core 10
* **ORM:** Entity Framework Core (MySQL)
* **Segurança:** Autenticação JWT (Stateless)
* **Validação:** FluentValidation integrado ao pipeline da API
* **Documentação:** Swagger/OpenAPI com suporte a autenticação
* **Logs:** Suporte a filtros de exceção globais para tratamento padronizado

---

## 🛠️ Como Executar o Projeto

### Pré-requisitos
* SDK do .NET 10.
* Instância de MySQL.

### Passo a Passo
1.  **Clone o repositório:**
    ```bash
    git clone https://github.com/alvarento/SchoolMangement.git
    ```
2.  **Configuração do Banco:**
    Ajuste a `Connection String` no arquivo `appsettings.json` da camada de API.
3.  **Execução em Lote (Visual Studio):**
    Para uma experiência completa, configure o Visual Studio para iniciar múltiplos projetos:
    * Clique com o botão direito na **Solution** > **Propriedades**.
    * Selecione **Projetos de Inicialização Comuns**.
    * Configure `SchoolManagement.Api` e `SchoolManagement.WebApp` para **Iniciar**.
4.  **Migrations:**
    O sistema está configurado para aplicar as migrations automaticamente no Startup. Caso prefira via terminal:
    ```bash
    dotnet ef database update --project SchoolManagement.Infrastructure --startup-project SchoolManagement.Api
    ```

---

## 🔐 Credenciais e Acesso
Para acessar endpoints protegidos:
1.  Crie um usuário via `POST /usuarios`.
2.  Obtenha o Token via `POST /login`.
3.  No Swagger, clique em **Authorize** e insira: `Bearer <seu_token>`.
4.  Gerar CPF válido sem pontuação em: [4devs.com/gerador_de_cpf](https://www.4devs.com.br/gerador_de_cpf)

---


## 🛠️ Configurações de Exceção (Exception Settings)
A aplicação tem exceções customizadas que passam por um filtro, mas precisa configurar uma questão do VisualStudio.
Você pode desativar a pausa automática para tipos específicos de exceção sem afetar o resto do sistema.
Abra a janela Exception Settings:
Use o atalho Ctrl + Alt + E
Ou acesse o menu superior: Debug > Windows > Exception Settings.
Localize a categoria da sua exceção (geralmente Common Language Runtime Exceptions).
Use a barra de pesquisa da janela para buscar o nome da sua exceção customizada.
Desmarque a caixa de seleção ao lado do nome da sua exceção.
Isso impede que o depurador pare quando ela for lançada (Thrown), permitindo que seu código siga para o bloco catch.


⚙️ Ajustar o Just My Code (Apenas Meu Código) 
Se as exceções customizadas ocorrem em bibliotecas de terceiros ou projetos secundários:
Acesse Tools > Options.
Vá em Debugging > General.
Marque a opção Enable Just My Code.
O depurador passará a ignorar exceções geradas fora do seu código principal, desde que estejam devidamente tratadas.

## 👨‍💻 Autor
**Álvaro Nascimento** - Salvador, Bahia – Brasil 🌎
[![LinkedIn](https://img.shields.io/badge/LinkedIn-0077B5?style=for-the-badge&logo=linkedin&logoColor=white)](https://linkedin.com/in/alvarento)