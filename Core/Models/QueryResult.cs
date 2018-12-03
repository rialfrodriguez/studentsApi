
using System.Collections.Generic;

namespace studentsApi.Core.Models
{
     public class QueryResult<T>
    {
        public int TotalItems { get; set; }
        public IEnumerable<T> Items { get; set; }
    }
}