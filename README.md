# 📘 Projeto: Ganho de Capital

Este projeto tem como objetivo **calcular o imposto a ser pago sobre lucros ou prejuízos de operações no mercado financeiro de ações**, seguindo os princípios de **DDD (Domain-Driven Design)** e **Clean Architecture**.

---

## 🏗️ Estrutura do Projeto

```plaintext
src/
 ├── Application/                     # Camada de aplicação (UseCases)
 │    └── UseCases/
 │          └── ApurarGanhoCapitalUseCase.cs
 │
 ├── Domain/                          # Camada de domínio (Regra de negócio)
 │    ├── Entities/                   # Entidades do domínio
 │    │     ├── Operacao.cs
 │    │     ├── Portfolio.cs
 │    │     └── ImpostoGanhoCapital.cs
 │    │
 │    ├── Services/                   # Serviços de domínio
 │    │     └── CalculoImpostoGanhoCapitalService.cs
 │    │
 │    └── Enums/                      # Enums do domínio
 │          └── TipoOperacaoEnum.cs
 │
 └── Infrastructure/                  # Camada de infraestrutura
      ├── Persistence/                # Repositórios / Data access
      ├── Migrations/                 # (se usar banco relacional)
      └── ExternalServices/           # Integrações externas
```

---

## 📌 Decisões Técnicas e Arquiteturais

1. **Clean Architecture**:

   * O projeto é dividido em **Application**, **Domain** e **Infrastructure**, mantendo isolamento entre regras de negócio e detalhes técnicos.
   * Facilita testes, manutenção e evolução do sistema.

2. **DDD (Domain-Driven Design)**:

   * Entidades (`Operacao`, `Portfolio`, `ImpostoGanhoCapital`) refletem a linguagem do negócio.
   * Domain Service (`CalculoImpostoGanhoCapitalService`) encapsula regras complexas de cálculo de imposto.
   * Enums (`TipoOperacaoEnum`) tornam explícito o conceito de compra ou venda, evitando "números mágicos" ou strings soltas.

3. **Separação de responsabilidades**:

   * **Use Case** (`ApurarGanhoCapitalUseCase`) → orquestra o fluxo de operações e resultados.
   * **Domain Service** → calcula imposto e aplica regras de negócio.
   * **Entities** → mantêm estado e comportamento do domínio.

4. **Extensibilidade**:

   * Fácil adicionar novos tipos de operações, impostos ou regras sem impactar a infraestrutura.
   * Testes unitários independentes da camada de aplicação ou de persistência.

---

## ⚙️ Justificativa do Uso de Frameworks/Bibliotecas

* **.NET 9**: escolha por ser moderno, com suporte a CLI, testes integrados e alta produtividade.
* **xUnit / NUnit / MSTest** (dependendo da escolha para testes): permitem criar testes unitários claros e isolados para Domain Services e Use Cases.
* Nenhuma biblioteca externa foi usada para cálculo do imposto, garantindo que toda regra de negócio esteja **totalmente controlada pelo domínio**.

---

## 🚀 Como Compilar e Executar

### Pré-requisitos

* [.NET 9 SDK](https://dotnet.microsoft.com/download) instalado.

### Compilação

```bash
# Restaurar dependências
dotnet restore

# Compilar a solução
dotnet build
```

### Execução

```bash
# Executar o projeto (exemplo de CLI)
dotnet run --project src/Application
```

---

## 🧪 Executando os Testes

```bash
# Executar todos os testes
dotnet test
```

Exemplo de estrutura de testes:

```plaintext
tests/
 ├── Domain.Tests/
 │    ├── OperacaoTests.cs
 │    ├── PortfolioTests.cs
 │    └── CalculoImpostoGanhoCapitalServiceTests.cs
 │
 └── Application.Tests/
      └── ApurarGanhoCapitalUseCaseTests.cs
```

---

## 📝 Notas Adicionais

1. **Isolamento do Domínio**:

   * Toda regra de cálculo está em `CalculoImpostoGanhoCapitalService`.
   * Nenhum cálculo depende de banco, UI ou APIs externas.

2. **Testabilidade**:

   * Entidades e serviços podem ser testados isoladamente, garantindo alta confiabilidade.

3. **Nomes Ubiquos**:

   * `Operacao`, `Portfolio` e `ImpostoGanhoCapital` refletem diretamente os conceitos do negócio de ações.
   * Enum `TipoOperacaoEnum` melhora a legibilidade e mantém consistência do domínio.

4. **Extensibilidade futura**:

   * Caso surjam novos tipos de ativos, impostos ou regras de negócio, é possível adicionar sem alterar o fluxo existente do Use Case.

5. **Legibilidade e manutenção**:

   * Seguindo DDD + Clean Architecture, o projeto facilita onboarding de novos desenvolvedores e manutenção contínua.