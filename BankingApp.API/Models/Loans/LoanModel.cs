namespace BankingApp.API.Models.Loans {
    public class LoanModel {
        public int Id { get; set; }
        public string Type { get; set; }
        public int LoanTypeId { get; set; }
        public decimal InterestRate { get; set; }
        public int Period { get; set; }
        public bool FixedRate { get; set; }
    }
}