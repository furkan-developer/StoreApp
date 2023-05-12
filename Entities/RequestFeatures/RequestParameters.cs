using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.RequestFeatures
{
    public abstract class RequestParameters
    {
		const int maxPageSize = 50;
		
		private int _pageSize;

		public int PageSize
		{
			get { return _pageSize; }
			set { _pageSize = value > maxPageSize ? maxPageSize : value; }
		}

        public int PageNumber { get; set; }
        public uint MinPrice { get; set; } = 0;
		public uint MaxPrice { get; set; } = 1000;
		public String? OrderBy { get; set; }
		public String? Fields { get; set; }
    }
}
