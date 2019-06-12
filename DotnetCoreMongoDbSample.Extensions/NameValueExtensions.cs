using DotnetCoreMongoDbSample.Data.Entities;
using DotnetCoreMongoDbSample.Models;
using DotnetCoreMongoDbSample.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetCoreMongoDbSample.Extensions
{
    public static class NameValueExtensions
    {
        public static PaginationModel<NameValueDetailViewModel> ToNameValueDetailPaginationModel(this List<NameValue> nameValues, int currentPage = 1  , int pageSize = 10)
        {
            return new PaginationModel<NameValueDetailViewModel>()
            {
                CurrentPage = currentPage,
                PageSize = pageSize,
                Count = nameValues.Count,
                Data = currentPage == 0 ? 
                        nameValues.ToNameValueDetailViewModelList() :
                        nameValues.Skip((currentPage - 1) * pageSize).Take(pageSize).ToNameValueDetailViewModelList()
            };
        }

        public static NameValueDetailViewModel ToNameValueDetailViewModel(this NameValue nameValue)
        {
            return new NameValueDetailViewModel()
            {
                Id = nameValue.Id,
                Name = nameValue.Name,
                Value = nameValue.Value
            };
        }

        public static NameValue ToNameValue(this NameValueCreateViewModel model)
        {
            return new NameValue()
            {
                Name = model.Name,
                Value = model.Value
            };
        }

        private static List<NameValueDetailViewModel> ToNameValueDetailViewModelList(this IEnumerable<NameValue> nameValues)
        {
            return nameValues.Select(p => new NameValueDetailViewModel()
            {
                Id = p.Id,
                Name = p.Name,
                Value = p.Value
            }).ToList();
        }
    }
}
