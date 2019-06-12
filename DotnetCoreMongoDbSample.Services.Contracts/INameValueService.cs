using DotnetCoreMongoDbSample.Models;
using DotnetCoreMongoDbSample.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotnetCoreMongoDbSample.Services.Contracts
{
    public interface INameValueService
    {
        Task<PaginationModel<NameValueDetailViewModel>> GetAllAsync(int currentPage);
        Task<NameValueDetailViewModel> GetByIdAsync(string id);
        Task CreateAsync(NameValueCreateViewModel model);
        Task<bool> UpdateAsync(string id, NameValueCreateViewModel model);
        Task<bool> DeleteByIdAsync(string id);
        Task CreateAThousandRandomDataAsync();
    } 
}
