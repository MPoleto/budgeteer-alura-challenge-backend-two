# Budgeteer

**Alura Challenges Back-end 2ª Edição**

Desafio para simular o dia-a-dia de uma pessoa desenvolvedora back-end em uma empresa, com um projeto proposto para ser desenvolvido em 4 semanas.
Projeto de uma API de controle financeiro do orçamento familiar

> Descrição  
> Foi requisitada a primeira versão de uma aplicação para controle de orçamento familiar. A aplicação deve permitir que uma pessoa cadastre suas receitas e despesas do mês, bem como gerar um relatório mensal.
> As principais funcionalidades a serem implementadas são:
> 1. API com rotas implementadas seguindo as boas práticas do modelo REST;
> 2. Validações feitas conforme as regras de negócio;
> 3. Implementação de base de dados para persistência das informações;
> 4. Serviço de autenticação/autorização para restringir acesso às informações.


## Ferramentas e Tecnologias
- Trello - com a descrição do projeto, para controle das tarefas, cada semana um quadro com as atividades para serem realizadas.
- Visual Studio Code
- Linguagem C#
- .NET 8
- Entity Framework Core
- MySql
- Insomnia

## Desenvolvimento

### Semana 1

#### Banco de dados da aplicação

**Etapas:**

- Criar solução para a aplicação.
- Criar projeto de biblioteca de classes `Budgeteer.Shared.Models` e definir as classes modelos *Income (Receita)* e *Expense (Despesa)*
- Criar outro projeto biblioteca de classes `Budgeteer.Shared.Data` para definir as classes *BudgetContext* e *DAL*. 
  - Instalar os pacotes necessários para a configurar e conectar com o banco de dados: 
  - `Pomelo.EntityFrameworkCore.MySql 8.0.2`, 
  - `Microsoft.EntityFrameworkCore.Design 8.0.2`,
  - `Microsoft.EntityFrameworkCore.Tools 8.0.2`
- Criar projeto de API minimal (template ASP.NET Core Empty) `Budgeteer.API`
  - Configurar a conexão com o banco de dados
    - Adicionar o pacote `Microsoft.EntityFrameworkCore.Design 8.0.2`
  - Executar os comandos para a criação do banco de dados
    - `dotnet ef migrations add InitialCreate --project Budgeteer.Shared.Data --startup-project Budgeteer.API`
    - `dotnet ef database update`
  - Usar o gerenciador de segredos para guardar a string de conexão com o banco de dados.

#### Endpoints da API

- Criar diretório *Endpoints* para os arquivos onde as rotas HTTP vão ser definidas e configuradas.
  - Criar as classes estáticas *IncomeEndpoins*, para os endpoints referentes à receita, e *ExpenseEndpoints*, para os endpoints referentes à despesa.
    - Usar mapGroup para o nome das rotas e diminuir a repetição.
- Crias os diretórios *Requests* e *Responses* onde vão estar os *DTOs*
- Adicionar serviço da classe DAL no Programs.cs
- Chamar as classes de endpoints na Programs.cs

- Requisitos do projeto:
  - Para as receitas os endpoints iniciam com `URI/receitas`
  - Para as despesas os endpoints iniciam com `URI/despesas`
  - As requisições e as respostas devem ser no formato `JSON`.

**Cadastro**
  - Criar um *Record IncomeRequest (receitas)* e um *Record ExpenseRequest (despesas)* para usar nas rotas POST.
  - Requisito do projeto: Dentro de um mesmo mês não pode ter um cadastro com a mesma descrição. 
    - Todas as informações são obrigatórias.

**Listagem**
  - Criar um *Record IncomeResponse (receitas)* e um *Record ExpenseResponse (despesas)* para usar nas rotas GET
  - Criar métodos para passar o objeto para DTO e usar no retorno das requisições GET

**Detalhamento**
  - As rotas GET dos endpoints para exibir os detalhes deve ser `/{id}`.

**Atualização**
  - Criar um *Record IncomeRequestUpdate* que herda de *IncomeRequest* para as receitas.
  - Criar um *Record ExpenseRequestUpdate* que herda de *ExpenseRequest* para as despesas.
  - Os endpoints para as rotas PUT para atualizar deve ser `/{id}`.
  - Requisito do projeto: Possui as mesmas regras de negócio do cadastro.

**Exclusão**
  - Os endpoints para as rotas DELETE para exclusão deve ser `/{id}`.