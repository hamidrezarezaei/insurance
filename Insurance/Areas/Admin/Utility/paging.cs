using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Insurance
{
    public class pagedResult<T> where T : new()
    {
        public List<T> items { get; set; } = new List<T>();
        public PagingData pagingData { get; set; } = new PagingData();

    }
    public class PagingData
    {
        public int totalItems { get; set; }
        public int itemsPerPage { get; set; }
        public int currentPage { get; set; }
        public int totalPages => (int)Math.Ceiling((decimal)totalItems / itemsPerPage);
    }
}
