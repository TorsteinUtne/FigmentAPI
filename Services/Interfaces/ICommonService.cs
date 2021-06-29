using PowerService.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PowerService.Services
{
    interface ICommonService
    {
        Task CreateRecord(CommonModel record);

        Task RetrieveRecord(CommonModel record);

        Task UpdateRecord(CommonModel record);

        Task DeleteRecord(CommonModel record);

        Task UpsertRecord(CommonModel record);
    }
}
