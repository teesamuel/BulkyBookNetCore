using BulkyBookWeb.Data;
using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            this._db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Category> objCategoryList =_db.Categories.ToList();
            return View(objCategoryList);
        }
        //GET
        public IActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if(obj.Name == obj.DisplayOrder.ToString())
            {
             //   ModelState.AddModelError("CustomError", "The Display Order Cannot exactly match the Name");
                ModelState.AddModelError("name", "The Display Order Cannot exactly match the Name");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Category Created Successfully";
                return RedirectToAction("Index");
            }
            return View();
        }
        //GET
        //[HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var categorybyFind = _db.Categories.Find(id);
            //var categoryFistOrDefault = _db.Categories.FirstOrDefault(c => c.Id == id);
            //var categorySingle = _db.Categories.SingleOrDefault(c => c.Id == id);
            if(categorybyFind == null)
            {
                return NotFound();  
            }
            return View(categorybyFind);
        }


        //Put
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                //   ModelState.AddModelError("CustomError", "The Display Order Cannot exactly match the Name");
                ModelState.AddModelError("name", "The Display Order Cannot exactly match the Name");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Category Edited Successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var categorybyFind = _db.Categories.Find(id);
            //var categoryFistOrDefault = _db.Categories.FirstOrDefault(c => c.Id == id);
            //var categorySingle = _db.Categories.SingleOrDefault(c => c.Id == id);
            if (categorybyFind == null)
            {
                return NotFound();
            }
            return View(categorybyFind);
        }

        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _db.Categories.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Categories.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Category Deleted Successfully";
            return RedirectToAction("Index");
    
        }


    }
}
