using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PowerService.Data.Models.Queries
{
    public class SearchParameter
    {
        const int maxPageSize = 250; //TODO: COnfig value
        public int PageNumber { get; set; } = 1;
        private int _pageSize = 10;
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }

        public string SortingField { get; set; } = "";

        public string Order { get; set; } = "asc";

        public string SearchValue { get; set; } = ""; //
        public string SearchField { get; set; } = ""; //
        //public string RecordName { get; set; } //TODO: Implement an interface in each data model so that we can use this interface to refer to class name and such
    }
}
