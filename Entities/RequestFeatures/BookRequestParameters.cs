﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.RequestFeatures
{
    public class BookRequestParameters : RequestParameters
    {
        public String? TitleSearchTerm { get; set; }
    }
}
