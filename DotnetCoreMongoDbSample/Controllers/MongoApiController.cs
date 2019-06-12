using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotnetCoreMongoDbSample.Models;
using DotnetCoreMongoDbSample.Models.ViewModels;
using DotnetCoreMongoDbSample.Services.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotnetCoreMongoDbSample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MongoApiController : ControllerBase
    {
        private readonly INameValueService _nameValueService;

        public MongoApiController(INameValueService nameValueService)
        {
            _nameValueService = nameValueService;
        }

        [HttpGet]
        public async Task<ActionResult<List<NameValueDetailViewModel>>> Get()
        {
            PaginationModel<NameValueDetailViewModel> nameValues = await _nameValueService.GetAllAsync(0);
            return Ok(nameValues.Data);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<NameValueDetailViewModel>> Get(string id)
        {
            NameValueDetailViewModel nameValue = await _nameValueService.GetByIdAsync(id);
            if (nameValue == null)
            {
                return NotFound();
            }

            return Ok(nameValue);
        }

        [HttpPost]
        public async Task<ActionResult> Create(NameValueCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _nameValueService.CreateAsync(model);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> Update(string id, NameValueCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool updateResult = await _nameValueService.UpdateAsync(id, model);
            return Ok(updateResult);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(string id)
        {
            bool deleteResult = await _nameValueService.DeleteByIdAsync(id);
            return Ok(deleteResult);
        }
    }
}