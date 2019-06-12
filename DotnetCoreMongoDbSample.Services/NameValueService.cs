using DotnetCoreMongoDbSample.Data;
using DotnetCoreMongoDbSample.Data.Entities;
using DotnetCoreMongoDbSample.Extensions;
using DotnetCoreMongoDbSample.Models;
using DotnetCoreMongoDbSample.Models.ViewModels;
using DotnetCoreMongoDbSample.Services.Contracts;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotnetCoreMongoDbSample.Services
{
    public class NameValueService : INameValueService
    {
        private readonly IMongoCollection<NameValue> _nameValues;

        public NameValueService(SampleCollectionContext context)
        {
            _nameValues = context.NameValues;
        }

        public async Task<PaginationModel<NameValueDetailViewModel>> GetAllAsync(int currentPage)
        {
            var nameValues = await _nameValues.Find(p => true).ToListAsync();
            return nameValues.ToNameValueDetailPaginationModel(currentPage);
        }

        public async Task<NameValueDetailViewModel> GetByIdAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException("message", nameof(id));
            }

            var nameValue = await _nameValues.Find(p => p.Id == id).FirstOrDefaultAsync();
            if (nameValue == null)
            {
                return null;
            }

            return nameValue.ToNameValueDetailViewModel();
        }

        public async Task CreateAsync(NameValueCreateViewModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            NameValue nameValue = model.ToNameValue();
            await _nameValues.InsertOneAsync(nameValue);
        }

        public async Task<bool> UpdateAsync(string id, NameValueCreateViewModel model)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException("message", nameof(id));
            }

            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            NameValue nameValue = model.ToNameValue();
            nameValue.Id = id;
            var updateResult = await _nameValues.ReplaceOneAsync(p => p.Id == id, nameValue);
            return updateResult.IsAcknowledged && updateResult.MatchedCount > 0 && updateResult.ModifiedCount > 0;
        }

        public async Task<bool> DeleteByIdAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException("message", nameof(id));
            }

            var deleteResult = await _nameValues.DeleteOneAsync(p => p.Id == id);
            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }

        public async Task CreateAThousandRandomDataAsync()
        {
            List<NameValue> nameValues = new List<NameValue>();
            for (int i = 0; i < 1000; i++)
            {
                nameValues.Add(new NameValue()
                {
                    Name = Guid.NewGuid().ToString("N"),
                    Value = Guid.NewGuid().ToString("N")
                });
            }

            await _nameValues.InsertManyAsync(nameValues);
        }
    }
}
