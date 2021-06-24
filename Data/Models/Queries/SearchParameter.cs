using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PowerService.Data.Models.Queries
{
    public class SearchParameter
    {
        public int PageNumber { get; set; } = 1;
        private Int32? _pageSize = null;
        public Int32 PageSize
        {
            get
            {
                _pageSize ??= Startup.MaxPageSize;
                return _pageSize.Value;
            }
            set => _pageSize = (value > Startup.MaxPageSize) ? Startup.MaxPageSize : value;
        }

        public string SortingField { get; set; } = "";

        public string Order { get; set; } = "asc";

        public string SearchValue { get; set; } = ""; //
        public string SearchField { get; set; } = ""; //
        //public string RecordName { get; set; } //TODO: Implement an interface in each data model so that we can use this interface to refer to class name and such
    }
}
