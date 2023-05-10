﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.RequestFeatures
{
    public class BookRequestParameters : RequestParameters
    {
        public BookRequestParameters()
        {
            OrderBy = "id";
        }
        public String? TitleSearchTerm { get; set; }
    }
}
