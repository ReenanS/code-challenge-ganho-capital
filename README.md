# 📘 Projeto: Ganho de Capital

Este projeto tem como objetivo **calcular o imposto a ser pago sobre lucros ou prejuízos de operações no mercado financeiro de ações**, seguindo os princípios de **DDD (Domain-Driven Design)** e **Clean Architecture**, adaptado para execução via **CLI (Command Line Interface)**, sem necessidade de banco de dados ou integrações externas.

Toda a lógica de cálculo de imposto está isolada no **domínio**, garantindo **testabilidade, legibilidade e extensibilidade**.

---

## 🏗️ Estrutura do Projeto

```plaintext
src/
 ├── CLI/                              # Camada CLI
 │    ├── Program.cs                   # Ponto de entrada padrão .NET Core
 │    └── GanhoCapitalCLI.cs           # Recebe operações e retorna resultado via stdin/stdout
 ├── Application/                      # Camada de aplicação (Use Cases)
 │    ├── UseCases/
 │    |     └── ApurarGanhoCapitalUseCase.cs
 │    |     └── IApurarGanhoCapitalUseCase.cs
 │    └── DependencyInjection.cs       # DI
 ├── Domain/                            # Camada de domínio (Regra de negócio)
 │    ├── Entities/
 │    │     ├── Operacao.cs
 │    │     ├── Portfolio.cs
 │    │     └── ImpostoGanhoCapital.cs
 │    ├── Services/
 │    │     ├── CalculoImpostoGanhoCapitalService.cs
 │    │     └── ICalculoImpostoGanhoCapitalService.cs
 │    ├── Enums/
 │    |     └── TipoOperacaoEnum.cs
 │    └── DependencyInjection.cs       # DI
```

---

## 📌 Decisões Técnicas e Arquiteturais

1. **Clean Architecture**

   * Separação clara entre **Application** e **Domain**, sem acoplamento com infraestrutura.
   * Facilita manutenção, testes e evolução do sistema.

2. **DDD (Domain-Driven Design)**

   * Entidades (`Operacao`, `Portfolio`, `ImpostoGanhoCapital`) refletem a linguagem do negócio.
   * Domain Service (`CalculoImpostoGanhoCapitalService`) encapsula toda a regra de cálculo do imposto.
   * Enums (`TipoOperacaoEnum`) tornam explícito o tipo de operação.

3. **CLI como interface**

   * Entrada padrão (stdin) para receber operações em JSON.
   * Saída padrão (stdout) para retornar resultados.

4. **Separação de responsabilidades**

   * **Use Case** (`ApurarGanhoCapitalUseCase`) → orquestra fluxo de operações e resultado.
   * **Domain Service** → calcula imposto e aplica regras de negócio.
   * **Entities** → mantêm estado e comportamento do domínio.

5. **Extensibilidade**

   * Novos tipos de operações ou regras podem ser adicionados sem impactar o fluxo existente.

---

## ⚙️ Justificativa do Uso de Frameworks/Bibliotecas

* **.NET 9**: suporte moderno, testes integrados e produtividade elevada.
* **xUnit**: criação de testes unitários claros e isolados para Domain Services e Use Cases.
* Nenhuma biblioteca externa foi usada para cálculo de imposto.

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
# Executar via dotnet run
dotnet run --project src/CLI

# Executar versão compilada (exemplo no Windows)
src/CLI/bin/Debug/net9.0/CLI.exe
```

O programa solicitará operações em formato JSON via stdin e retornará o resultado do cálculo via stdout.

Exemplo de entrada:

```json
[{"operation":"buy", "unit-cost":10.00, "quantity": 10000},
 {"operation":"sell", "unit-cost":2.00, "quantity": 5000},
 {"operation":"sell", "unit-cost":20.00, "quantity": 2000},
 {"operation":"sell", "unit-cost":20.00, "quantity": 2000},
 {"operation":"sell", "unit-cost":25.00, "quantity": 1000},
 {"operation":"buy", "unit-cost":20.00, "quantity": 10000},
 {"operation":"sell", "unit-cost":15.00, "quantity": 5000},
 {"operation":"sell", "unit-cost":30.00, "quantity": 4350},
 {"operation":"sell", "unit-cost":30.00, "quantity": 650}]
```

Exemplo de saída:

```json
 [{"tax": 0.0},{"tax": 0.0},{"tax": 0.0},{"tax": 0.0},{"tax": 3000.0},
 {"tax": 0.0},{"tax": 0.0},{"tax": 3700.0},{"tax": 0.0}]
```

Para encerrar a aplicação, pressione as teclas Ctrl + C no console.

---

## 🧪 Testes e Cobertura de Código

```bash
# Executar todos os testes
dotnet test
```

Exemplo de estrutura de testes:

```plaintext
tests/

 ├── Tests.CLI/                             
 │    └── GanhoCapitalCLITests.cs          
 ├── Tests.Application/
 │    ├── UseCases/
 │    |    └── ApurarGanhoCapitalUseCaseTests.cs
 ├    └── DependencyInjectionTests.cs
 ├── Tests.Domain/
 │    ├── Entities/
 │    |    └── OperacaoTests.cs
 │    |    └── PortfolioTests.cs
 │    |    └── ImpostoGanhoCapitalTests.cs 
 │    ├── Services/
 │    |    └── CalculoImpostoGanhoCapitalServiceTests.cs
 ├    └── DependencyInjectionTests.cs
 ├── Tests.Fixtures/
 │    ├── Domain/
 │         ├── Entities/ 
 │                └── OperacoesFixture.cs      
```

**Cobertura de código:** coletada com **Coverlet**, gerando relatórios.

---

## 📝 Notas Adicionais

1. **Isolamento do domínio**

   * Nenhum cálculo depende de banco de dados, UI ou APIs externas.
   * Toda regra de cálculo está centralizada em `CalculoImpostoGanhoCapitalService`.

2. **Testabilidade**

   * Entidades e serviços podem ser testados isoladamente, garantindo alta confiabilidade.

3. **Nomes refletindo a linguagem do negócio**

   * Entidades e enums refletem a linguagem do negócio de ações, garantindo legibilidade e consistência.

4. **Extensibilidade futura**

   * Novos tipos de ativos, impostos ou regras podem ser adicionados sem alterar o fluxo do Use Case.

5. **Legibilidade e manutenção**

   * O projeto segue **DDD + Clean Architecture**, facilitando onboarding e manutenção contínua.
