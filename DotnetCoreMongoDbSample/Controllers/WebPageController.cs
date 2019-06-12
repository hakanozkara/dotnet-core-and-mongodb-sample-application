using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DotnetCoreMongoDbSample.Models;
using DotnetCoreMongoDbSample.Models.ViewModels;
using DotnetCoreMongoDbSample.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace DotnetCoreMongoDbSample.Controllers
{
    public class WebPageController : Controller
    {
        private readonly INameValueService _nameValueService;

        public WebPageController(INameValueService nameValueService)
        {
            _nameValueService = nameValueService;
        }

        public async Task<IActionResult> Index(int currentPage = 1)
        {
            PaginationModel<NameValueDetailViewModel> model = await _nameValueService.GetAllAsync(currentPage);
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NameValueCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _nameValueService.CreateAsync(model);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id, string updated = null)
        {
            NameValueDetailViewModel nameValue = await _nameValueService.GetByIdAsync(id);
            NameValueCreateViewModel model = new NameValueCreateViewModel() { Name = nameValue.Name, Value = nameValue.Value };
            return View("Create", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, NameValueCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            bool updateResult = await _nameValueService.UpdateAsync(id, model);
            return RedirectToAction(nameof(Index), new { id = id, updated = updateResult });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            NameValueDetailViewModel nameValue = await _nameValueService.GetByIdAsync(id);
            if (nameValue == null)
            {
                return NotFound();
            }

            return View(nameValue);
        }

        [HttpPost, ActionName(nameof(Delete))]
        public async Task<ActionResult> DeleteConfirm(string id)
        {
            bool deleteResult = await _nameValueService.DeleteByIdAsync(id);
            return RedirectToAction(nameof(Index), new { deleted = deleteResult });
        }

        [HttpGet]
        public async Task<ActionResult> CreateAThousandRandomData()
        {
            await _nameValueService.CreateAThousandRandomDataAsync();
            return RedirectToAction(nameof(Index));
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}