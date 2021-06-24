using PowerService.Util;
using System;

namespace PowerService.Services
{
    public interface IUriService
    {
        public Uri GetPageUri(PaginationFilter filter, string route);
    }
}
