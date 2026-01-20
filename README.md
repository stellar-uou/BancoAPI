# 🏦 BancoAPI

<p align="center">
  <img src="https://img.shields.io" alt=".NET 10">
  <img src="https://img.shields.io" alt="Docker">
  <img src="https://img.shields.io" alt="C#">
  <img src="https://img.shields.io" alt="Swagger">
</p>

O **BancoAPI** é uma solução moderna desenvolvida em **C# com ASP.NET 10**, projetada para gerenciar operações bancárias essenciais de forma ágil e segura. A API oferece autenticação simplificada, gestão de contas e transações, tudo documentado via Swagger para facilitar a integração.

---

## 🚀 Funcionalidades

- **Contas:** Cadastro automático com geração de número de conta aleatório.
- **Autenticação:** Login seguro com e-mail/senha e geração de Token de 6 dígitos.
- **Consultas:** Saldo atualizado e extrato detalhado com histórico.
- **Transações:** Depósitos e transferências entre contas da base.

## 🛠️ Tecnologias Utilizadas

- **Linguagem:** C# (.NET 10)
- **Framework:** ASP.NET Web API
- **Persistência:** Collections e Listas (Armazenamento em Memória)
- **Documentação:** Swagger (OpenAPI)
- **Containerização:** Docker

---

## 💻 Como Rodar o Projeto

### Opção 1: Localmente
Se você tem o SDK do .NET instalado:

1. Clone o repositório:
   ```bash
   git clone https://github.com/stellar-uou/BancoAPI
Use o código com cuidado.

Entre na pasta do projeto e execute:
bash
dotnet run
Use o código com cuidado.

Acesse a documentação interativa: https://localhost:{porta}/swagger
Opção 2: Via Docker 🐳
Para rodar em um ambiente isolado:
Construa a imagem:
bash
docker build -t bancoapi .
Use o código com cuidado.

Inicie o container:
bash
docker run -p 5000:5000 bancoapi
Use o código com cuidado.

A API estará disponível em: http://localhost:5000/swagger
⚠️ Observações Acadêmicas
Nota: Este projeto foi desenvolvido exclusivamente para fins de aprendizado e portfólio.
Os dados são armazenados em memória (serão perdidos ao reiniciar a aplicação).
O sistema de Token de 6 dígitos é simplificado para fins didáticos e não deve ser utilizado em sistemas de produção reais.
