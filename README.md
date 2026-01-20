BancoAPI

BancoAPI é uma API bancária desenvolvida em C# com ASP.NET 10, criada para gerenciar contas, autenticação e transações de forma simples.

A API possui documentação interativa via Swagger e pode ser executada facilmente com Docker, garantindo portabilidade e facilidade de testes.

Funcionalidades:

- Cadastro de contas com número de conta aleatório

- Login com e-mail e senha, com geração de token de 6 dígitos

- Consulta de saldo da conta

- Consulta de extrato com histórico de transações

- Depósitos em conta

- Transferências entre contas

Tecnologias Utilizadas:

- C# / .NET 10

- ASP.NET Web API

- Collections e listas para armazenamento temporário (em memória)

- Swagger para documentação e testes de endpoints

- Docker para execução em contêiner

Como Rodar o Projeto:

- Opção 1: Localmente

Clone o repositório:
git clone https://github.com/stellar-uou/BancoAPI

Abra no Visual Studio ou VS Code

Execute a API:
Visual Studio: pressione F5

Terminal: dotnet run

Acesse o Swagger:
https://localhost:{porta}/swagger

Teste os endpoints usando Swagger, Postman ou outro cliente HTTP

- Opção 2: Usando Docker

Certifique-se de ter o Docker instalado

Na raiz do projeto, construa a imagem:
docker build -t bancoapi .

Execute o container:
docker run -p 5000:5000 bancoapi

A API estará disponível em:
http://localhost:5000

Acesse o Swagger:
http://localhost:5000/swagger

Observações:

Este projeto é voltado para aprendizado e portfólio, não sendo indicado para produção sem adaptações de segurança e persistência real em banco de dados.
O token gerado para login é de 6 dígitos, apenas para fins didáticos.
