using System;

namespace BankingApp.API.Models.Pagination
{
    public class DateFilter
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}