using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.RequestFeatures
{
    public class PagedList<T>: List<T>
    {
        public MetaData MetaData { get; set; }
        public PagedList(List<T> data, int count, int pageNumber,int pageSize)
        {
            MetaData = new MetaData()
            {
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalPage = (int)Math.Ceiling(count / (double)pageSize),
                TotolSize = count
            };

            AddRange(data);
        }

        public static PagedList<T> ToPagedList(IEnumerable<T> data,int pageNumber, int pageSize)
        {
            int count = data.Count();
            var currentPageData = data
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return new PagedList<T>(currentPageData, count, pageNumber, pageSize);
        }
    }
}
