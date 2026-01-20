using BancoAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BancoAPI.Controllers
{
    [ApiController]
    [Route("api/accounts")]
    public class AccountsController : ControllerBase
    {
        private static List<Account> accounts = new();

        // tokens de login ativos
        private static Dictionary<string, Account> loggedInAccounts = new();

        // criar conta
        [HttpPost("register")]
        public IActionResult Register(Account account)
        {
            account.Id = accounts.Count + 1;
            account.AccountNumber = $"ACC-{account.Id:0000}";
            account.Balance = 0;
            account.Transactions = new List<Transaction>();

            accounts.Add(account);

            return Ok(account);
        }

        // fazer login
        [HttpPost("login")]
        public IActionResult Login(LoginRequest request)
        {
            var account = accounts.FirstOrDefault(a =>
                a.Email == request.Email &&
                a.Password == request.Password
            );

            if (account == null)
                return Unauthorized("Email ou senha inválidos");

            // gerar token
            var random = new Random();
            var token = random.Next(100000, 999999).ToString(); // gera um número de 6 dígitos
            loggedInAccounts[token] = account;


            return Ok(new
            {
                Message = "Login realizado com sucesso",
                Token = token
            });
        }

        // verificar saldo (só com token)
        [HttpGet("balance")]
        public IActionResult GetBalance([FromHeader] string token)
        {
            if (!loggedInAccounts.ContainsKey(token))
                return Unauthorized("Token inválido ou não logado");

            var account = loggedInAccounts[token];

            return Ok(new
            {
                accountNumber = account.AccountNumber,
                balance = account.Balance
            });
        }

        // visualizar extrato (só com token)
        [HttpGet("statement")]
        public IActionResult GetStatement([FromHeader] string token)
        {
            if (!loggedInAccounts.ContainsKey(token))
                return Unauthorized("Token inválido ou não logado");

            var account = loggedInAccounts[token];

            return Ok(new
            {
                accountNumber = account.AccountNumber,
                transactions = account.Transactions.OrderByDescending(t => t.Date)
            });
        }

        // fazer depósito (só na conta logada)
        [HttpPost("deposit")]
        public IActionResult Deposit([FromHeader] string token, [FromBody] decimal amount)
        {
            if (!loggedInAccounts.ContainsKey(token))
                return Unauthorized("Token inválido ou não logado");

            var account = loggedInAccounts[token];

            if (amount <= 0)
                return BadRequest("O valor do depósito deve ser maior que zero");

            if (account.Transactions == null)
                account.Transactions = new List<Transaction>();

            account.Balance += amount;

            account.Transactions.Add(new Transaction
            {
                Date = DateTime.Now,
                Amount = amount,
                Description = "Depósito"
            });

            return Ok(new
            {
                Message = "Depósito realizado com sucesso",
                NewBalance = account.Balance
            });
        }

        // transferência entre contas (origem: conta logada)
        [HttpPost("transfer")]
        public IActionResult Transfer([FromHeader] string token, [FromBody] TransferRequest request)
        {
            if (!loggedInAccounts.ContainsKey(token))
                return Unauthorized("Token inválido ou não logado");

            var sourceAccount = loggedInAccounts[token];
            var destinationAccount = accounts.FirstOrDefault(a => a.Id == request.DestinationAccountId);

            if (destinationAccount == null)
                return NotFound("Conta de destino não encontrada");

            if (sourceAccount.Balance < request.Amount)
                return BadRequest("Saldo insuficiente na conta de origem.");

            // Executar transferência
            sourceAccount.Balance -= request.Amount;
            destinationAccount.Balance += request.Amount;

            // registrar transações
            if (sourceAccount.Transactions == null) sourceAccount.Transactions = new List<Transaction>();
            if (destinationAccount.Transactions == null) destinationAccount.Transactions = new List<Transaction>();

            sourceAccount.Transactions.Add(new Transaction
            {
                Date = DateTime.Now,
                Amount = -request.Amount,
                Description = $"Transferência para {destinationAccount.AccountNumber}"
            });

            destinationAccount.Transactions.Add(new Transaction
            {
                Date = DateTime.Now,
                Amount = request.Amount,
                Description = $"Recebido de {sourceAccount.AccountNumber}"
            });

            return Ok(new
            {
                Message = "Transferência realizada com sucesso!",
                SourceAccountBalance = sourceAccount.Balance,
                DestinationAccountBalance = destinationAccount.Balance
            });
        }
    }
}
