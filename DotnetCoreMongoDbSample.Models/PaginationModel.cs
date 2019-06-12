
using System;
using System.Collections.Generic;
using System.Text;

namespace DotnetCoreMongoDbSample.Models
{
    public class PaginationModel<T>
    {
        public int CurrentPage { get; set; }
        public int Count { get; set; }
        public int PageSize { get; set; }

        public int TotalPages => (int)Math.Ceiling(decimal.Divide(Count, PageSize));

        public List<T> Data { get; set; }
    }
}
