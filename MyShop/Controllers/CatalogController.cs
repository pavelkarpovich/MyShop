using Microsoft.AspNetCore.Mvc;
using MyShop.Interfaces;
using MyShop.Models;
using MyShop.Services;

namespace MyShop.Controllers
{
    public class CatalogController : Controller
    {
        private readonly ICatalogItemViewModelService _catalogItemViewModelService;
        private readonly IRepository<CatalogItem> _catalogRepository;

        public CatalogController()
        {
            //TODO: Replace to IoC approach
            _catalogItemViewModelService = new CatalogItemViewModelService();
            _catalogRepository = new LocalCatalogItemRepository();
        }
        public IActionResult Index()
        {
            var catalogItemsViewModel = _catalogRepository.GetAll().Select(x => new CatalogItemViewModel
            {
                Id = x.Id,
                Name = x.Name,
                PictureUrl = x.PictureUrl,
                Price = x.Price
            }).ToList();

            return View(catalogItemsViewModel);
        }

        public IActionResult Details(int id)
        {
            var item = _catalogRepository.GetById(id);

            if (item == null) RedirectToAction("Index");

            var result = new CatalogItemViewModel()
            {
                Id = item.Id,
                Name = item.Name,
                PictureUrl = item.PictureUrl,
                Price = item.Price
            };
            return View(result);
        }

        [HttpGet]
        //GET CatalogController/Edit/5
        public IActionResult Edit(int id)
        {
            var item = _catalogRepository.GetById(id);

            if (item == null) RedirectToAction("Index");

            var result = new CatalogItemViewModel()
            {
                Id = item.Id,
                Name = item.Name,
                PictureUrl = item.PictureUrl,
                Price = item.Price
            };
            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CatalogItemViewModel catalogItemViewModel)
        {
            try
            {
                _catalogItemViewModelService.UpdateCatalogItem(catalogItemViewModel);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {

                return View();
            }
        }
    }
}
