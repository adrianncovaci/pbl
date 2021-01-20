using System.Collections.Generic;

namespace BankingApp.API.Models.Pagination
{
    public class PagedResponse<T> where T : class
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int Total { get; set; }
        public IList<T> Data { get; set; }
    }
}