# Desafio Técnico: Solução dos 5 Exercícios

Este repositório contém a implementação e a resolução do desafio técnico da AMcom composto por 5 questões distintas, abordando diferentes áreas do desenvolvimento de software com foco no ecossistema .NET.

## 📝 Soluções Detalhadas

Cada questão proposta foi resolvida e documentada abaixo. O código-fonte das soluções práticas encontra-se neste repositório.

---

### Questão 1: Classe ContaBancaria

**▶️ Objetivo:** Implementar uma classe em C# para gerenciar uma conta bancária, com operações de depósito, saque e regras de negócio específicas.

**✅ Solução:**
Foi criada a classe `ContaBancaria` utilizando princípios de Programação Orientada a Objetos para garantir o encapsulamento e a segurança dos dados.

- **Encapsulamento:** O saldo (`Saldo`) só pode ser modificado internamente através dos métodos `Deposito()` e `Saque()`, que aplicam as regras de negócio (como a taxa de saque).
- **Construtores:** Foram implementados dois construtores para permitir a criação de contas com e sem um depósito inicial.

---

### Questão 2: Consumo de API Externa

**▶️ Objetivo:** Desenvolver uma aplicação para calcular o total de gols de um time em um ano, consumindo uma API REST externa.

**✅ Solução:**
Foi desenvolvido um método assíncrono em C# que consome a API de partidas de futebol.

- **Cliente HTTP:** Utiliza `HttpClient` para realizar as requisições `GET`.
- **Manipulação de JSON:** O pacote `Newtonsoft.Json` é usado para desserializar a resposta da API em objetos C#.
- **Paginação:** A lógica implementada percorre todas as páginas de resultados da API para garantir que todos os jogos sejam contabilizados.

---

### Questão 3: Análise de Comandos Git

**▶️ Objetivo:** Analisar uma sequência de comandos `git` e determinar quais arquivos estariam no diretório de trabalho ao final.

**✅ Solução:**
 A resposta correta é **`style.css`, apenas**.

**Justificativa:**
1.  `default.html` é criado no "Commit 1", mas removido pelo comando `git rm` no "Commit 2".
2.  `style.css` é criado e adicionado no "Commit 2" na branch `master`.
3.  `script.js` é criado na nova branch `testing`.
4.  Ao executar `git checkout master` no final, o Git reverte o diretório de trabalho para o estado do último commit da `master`, que contém apenas `README.md` e `style.css`. O arquivo `script.js` existe apenas na branch `testing`.

---

### Questão 4: Consulta SQL Agregada

**▶️ Objetivo:** Escrever uma query SQL para agrupar e filtrar dados de atendimento de uma tabela.

**✅ Solução:**
Foi escrita uma query em SQL que utiliza agregações e filtros avançados.

- **Agrupamento:** `GROUP BY assunto, ano` para agrupar as ocorrências.
- **Contagem:** `COUNT(*)` para totalizar os atendimentos em cada grupo.
- **Filtragem de Grupo:** `HAVING COUNT(*) > 3` para exibir apenas os grupos com mais de 3 ocorrências.
- **Ordenação:** `ORDER BY ano DESC, quantidade DESC` para classificar o resultado.

---

### Questão 5: API REST para Conta Corrente

**▶️ Objetivo:** Desenvolver uma API REST com duas funcionalidades: movimentação e consulta de saldo de conta corrente.

**✅ Solução:**
O corpo principal deste repositório é a solução para esta questão. Foi construída uma API ASP.NET Core seguindo padrões de mercado:

- **Arquitetura:** A solução utiliza Clean Architecture e o padrão CQRS para separar responsabilidades de leitura e escrita.
- **Tecnologias:** .NET 6, Dapper para acesso a dados, MediatR para orquestração e SQLite como banco de dados.
- **Funcionalidades:**
    - **Endpoint de Movimentação:** Garante a **idempotência** através de uma chave no header, evitando transações duplicadas.
    - **Endpoint de Saldo:** Realiza o cálculo em tempo real a partir dos movimentos registrados.
- **Como Executar:** O projeto principal da solução pode ser executado para iniciar o servidor. A API é documentada via Swagger para facilitar os testes.
