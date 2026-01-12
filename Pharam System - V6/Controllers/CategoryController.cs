using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pharam_System___V6.Models;
using Pharam_System___V6.Repositry;

namespace Pharam_System___V6.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IRepositry<Category> _repository;
        public CategoryController(IRepositry<Category> repository)
        {
            _repository = repository;
        }
        // GET: CategoryController
        public ActionResult Index()
        {
            var categorys = _repository.GetAll();
            return View(categorys);
        }


        // GET: CategoryController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category category)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(category);
                }

                _repository.Add(category);
                TempData["msg"] = "Category added successfully.";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["msg"] = "An error occurred while adding the product.";
                return View(category);
            }
        }

        // GET: CategoryController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Category category)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(category);
                }
                _repository.Update(id,category);
                TempData["msg"] = "Category added successfully.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["msg"] = "An error occurred while adding the product.";
                return View(category);
            }
        }

        // GET: CategoryController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CategoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Category category)
        {
            try
            {
                _repository.Delete(id);
                TempData["msg"] = "category deleted successfully.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["msg"] = "An error occurred while deleting the category.";
                return RedirectToAction(nameof(Delete), new { id });
            }
        }
    }
}
