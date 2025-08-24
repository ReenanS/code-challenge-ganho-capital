# 📘 Projeto: Ganho de Capital

Este projeto tem como objetivo **calcular o imposto a ser pago sobre lucros ou prejuízos de operações no mercado financeiro de ações**, seguindo **Clean Architecture**, **DDD**, **SOLID** e **Clean Code**, com execução via **CLI (Command Line Interface)**.

---

## Pré-requisitos

Certifique-se de ter as seguintes ferramentas instaladas em sua máquina:

- [.NET 9 SDK](https://dotnet.microsoft.com/download) instalado.
- Um editor de código, como [Visual Studio](https://visualstudio.microsoft.com/) ou [VS Code](https://code.visualstudio.com/)
- PowerShell para executar a versão compilada do programa CLI (Command Line Interface), se estiver em um ambiente Windows.
- Docker caso prefira rodar o projeto em um container, garantindo portabilidade entre diferentes sistemas operacionais. Para instalação siga as instruções dos links:
   - **Ubuntu:** [Install Docker Engine on Ubuntu](https://docs.docker.com/engine/install/ubuntu/)
   - **Mac:** [Install Docker Engine on Mac](https://docs.docker.com/desktop/install/mac-install/)
   - **Windows:** [Install Docker Engine on Windows](https://docs.docker.com/desktop/install/windows-install/)

---

## 📌 Decisões Técnicas e Arquiteturais

- **Clean Architecture**: separa **Application** e **Domain**, facilitando manutenção, testes e evolução.  

- **DDD**: entidades e serviços refletem a linguagem do negócio, garantindo regras claras e consistentes.  

- **SOLID**: princípios aplicados em toda a arquitetura para manter o código modular, extensível e de fácil manutenção.  

- **Responsabilidades bem definidas**: Use Cases orquestram o fluxo, Domain Services aplicam regras e Entities mantêm estado do domínio.  

- **Extensível**: novos tipos de operações ou regras podem ser adicionados sem impactar o fluxo atual.

---

## Uso de Frameworks/Bibliotecas

* **.NET 9**: Utilizado como framework principal para desenvolvimento da aplicação, aproveitando seus recursos de desempenho, segurança e suporte a arquiteturas modernas de software. [Documentação oficial do .NET](https://learn.microsoft.com/dotnet/)

* **xUnit**: Framework de testes unitários adotado para garantir qualidade e confiabilidade do código, seguindo boas práticas de TDD (Test-Driven Development). [Documentação oficial do xUnit](https://xunit.net/)

---

## Como Compilar e Executar

### Compilação

1. Restaurar dependências:

```bash
dotnet restore GanhoCapital.sln
```

2. Compilar a solução:

```bash
dotnet build GanhoCapital.sln
```

### Execução

1. Executar via dotnet run

```bash
dotnet run --project src/CLI
```

2. Se estiver em um ambiente Windows, executar versão compilada na pasta bin/Debug/net9.0

```bash
src/CLI/bin/Debug/net9.0/CLI.exe
```

3. Se estiver em um ambiente Windows, executar versão compilada na pasta raíz do projeto

```bash
CLI.exe
```

## Exemplos de Uso

O programa solicitará operações em formato JSON via stdin e retornará o resultado do cálculo via stdout.

Exemplo de entrada:

```json
[{"operation":"buy", "unit-cost":10.00, "quantity": 10000}, {"operation":"sell", "unit-cost":2.00, "quantity": 5000}, {"operation":"sell", "unit-cost":20.00, "quantity": 2000}, {"operation":"sell", "unit-cost":20.00, "quantity": 2000}, {"operation":"sell", "unit-cost":25.00, "quantity": 1000}, {"operation":"buy", "unit-cost":20.00, "quantity": 10000}, {"operation":"sell", "unit-cost":15.00, "quantity": 5000}, {"operation":"sell", "unit-cost":30.00, "quantity": 4350}, {"operation":"sell", "unit-cost":30.00, "quantity": 650}]
```

Exemplo de saída:

```json
[{"tax":0.00},{"tax":0.00},{"tax":0.00},{"tax":0.00},{"tax":3000.00},{"tax":0.00},{"tax":0.00},{"tax":3700.00},{"tax":0.00}]
```

Para encerrar a aplicação, pressione as teclas Ctrl + C no console.

---

## Dockerização

O projeto foi dockerizado para simplificar a execução em qualquer ambiente.  
Ele conta com um único container, iniciado via **docker-compose**, e um **Dockerfile** responsável pelas configurações de ambiente.

### Rodando via Docker

1. Build e subir container:

```bash
docker compose up -d --build
```

2. Se estiver em um ambiente Windows, executar programa batch para build e subir container

```bash
init.services-local.bat
```

3. Executar a aplicação manualmente (input via stdin):

```bash
docker run -i --rm code-challenge-ganho-capital-cli
```

4. Caso prefira executar a aplicação utilizando um arquivo, inclua o arquivo na root do projeto em seguida rode os seguintes comandos:

```bash
docker run -i code-challenge-ganho-capital-cli < input.txt
```

---

## Testes e Cobertura de Código

#### Execução dos Testes Unitários

1. Acesse a pasta dos testes unitários:

```bash
cd src/tests/UnitTests
```

2. Executar testes em uma determinada solução:

```bash
dotnet test
```

#### Testes Unitários com Cobertura de Código

1. Acesse a pasta dos testes unitários:

```bash
cd src/tests/UnitTests
```

2. Execute os testes e colete a cobertura de código:

```bash
dotnet test --collect:"XPlat Code Coverage"
```

3. Instale a ferramenta ReportGenerator para gerar relatórios HTML:

```bash
dotnet tool install -g dotnet-reportgenerator-globaltool
```

4. Note que foi criada a pasta `TestResults` dentro de `src/tests/UnitTests`. Dentro dela, há uma subpasta com um GUID gerado automaticamente. Identifique esse GUID e execute o comando abaixo para gerar o relatório HTML:

```bash
reportgenerator -reports:"Path\Para\UnitTests\TestResults\{guid}\coverage.cobertura.xml" -targetdir:"coveragereport" -reporttypes:Html
```

5. Abra o relatório gerado:
- Navegue até a pasta `src/tests/UnitTests/coveragereport`
- Abra o arquivo `index.html` para visualizar a cobertura de código da aplicação.

---

## 📝 Conclusão

Durante o desenvolvimento da aplicação, adotei práticas como **Clean Architecture**, alinhada aos princípios de **DDD**, **SOLID** e **Clean Code**. Isso garantiu uma separação clara de responsabilidades, deixando o projeto simples de entender, e flexivel para evoluções futuras.  
Por fim, priorizei a **qualidade** com testes bem escritos, cobrindo a aplicação ponta a ponta para garantir robustez e sustentabilidade.
