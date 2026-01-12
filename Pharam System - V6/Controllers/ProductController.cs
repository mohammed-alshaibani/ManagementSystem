using Microsoft.AspNetCore.Mvc;
using Pharam_System___V6.CodeFuncation;
using Pharam_System___V6.Models;
using Pharam_System___V6.Repositry;
using Pharam_System___V6.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;

namespace Pharam_System___V6.Controllers
{
    public class ProductController : Controller
    {
        private readonly IRepositry<Product> _repository;
        private readonly IWebHostEnvironment webHost;
        private readonly UploadCode uploadCode;

        public ProductController(IRepositry<Product> repository,IWebHostEnvironment webHost)
        {
            _repository = repository;
            this.webHost = webHost;
            uploadCode = new UploadCode(this.webHost);
        }
    
        // GET: CategoryController
        public ActionResult Index()
        {
            var product = _repository.GetAll();
            return View(product);

        }
       public ActionResult Search(string searchTerm)
          {
           if(searchTerm == null)
            {
                return View("Index",_repository.GetAll());
            }
            else
            {
                return View("Index",_repository.Search(searchTerm));
            }
               }


        // GET: CategoryController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductViewModel productViewModel)
        {
            try
            {
               var product = new Product
               {
                        ProductId = productViewModel.ProductId,
                        ProductPrice = productViewModel.ProductPrice,
                        ProductName = productViewModel.ProductName,
                        ProductDescription = productViewModel.ProductDescription,
                        ProductImage = uploadCode.UploadFun(productViewModel.ProductImage, "images")
                };
                    _repository.Add(product);
                    return RedirectToAction(nameof(Index));

                TempData["msg"] = "An error occurred while adding the product.";
                return View();
            }
            catch (Exception ex)
            {
                TempData["msg"] = "An error occurred while adding the product: " + ex.Message;
                return View();
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
        public ActionResult Edit(int id, Product product)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(product);
                }
                _repository.Update(id, product);
                TempData["msg"] = "product added successfully.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["msg"] = "An error occurred while adding the product.";
                return View(product);
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
        public ActionResult Delete(int id, Product product)
        {
            try
            {
                _repository.Delete(id);
                TempData["msg"] = "product deleted successfully.";
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
