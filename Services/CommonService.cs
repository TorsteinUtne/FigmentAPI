using PowerService.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PowerService.Services
{
    public class CommonService : ICommonService
    {
        Task ICommonService.CreateRecord(CommonModel record)
        {
            throw new NotImplementedException();
        }

        Task ICommonService.DeleteRecord(CommonModel record)
        {
            throw new NotImplementedException();
        }

        Task ICommonService.RetrieveRecord(CommonModel record)
        {
            throw new NotImplementedException();
        }

        Task ICommonService.UpdateRecord(CommonModel record)
        {
            throw new NotImplementedException();
        }

        Task ICommonService.UpsertRecord(CommonModel record)
        {
            throw new NotImplementedException();
        }
    }
}
