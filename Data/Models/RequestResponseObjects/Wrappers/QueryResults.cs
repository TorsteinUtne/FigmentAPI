﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PowerService.Data.Models.RequestResponseObjects.Wrappers
{
    public class QueryResults<T>
    {
        public int TotalRecords;
        public T Results;
    }
}
