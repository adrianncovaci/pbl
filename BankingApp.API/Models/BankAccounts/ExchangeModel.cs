using System;
using System.Collections.Generic;

namespace BankingApp.API.Models.BankAccounts
{
    public class ExchangeModel
    {
        public bool Success { get; set; }
        public DateTime Date { get; set; }
        public string Base { get; set; }
        public IDictionary<string, decimal> Rates { get; set; }
    }
}