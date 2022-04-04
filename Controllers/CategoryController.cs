using BulkyBookWeb.Data;
using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        // Need an implementation of the above DbContext where the connection to the db is alreayd made, and we can retrieve some records right away
        public CategoryController(ApplicationDbContext db)
        {
            // Whatever is registered inside the container, we can access
            _db = db;
        }

        public IActionResult Index()
        {
            // Retrieve our categories
            IEnumerable<Category> objCategoryList = _db.Categories;
            return View(objCategoryList);
        }

        //GET Action Method
        public IActionResult Create()
        {
            // When someone hits the create button, give them the option to enter the name and display order and create a category.
            return View();
        }

        // POST Action Method
        [HttpPost]
        [ValidateAntiForgeryToken] // This is here to help prevent a cross site request forgery attack. 
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name.");
            }
            if (ModelState.IsValid) // Need this to make sure our model is actually valid, AKA make sure they actually entered in all the information needed
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index"); // Return to the Index action in this controller, basically our list
            }
            return View(obj);
        }


        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound(); // Invalid ID
            }
            var categoryFromDb = _db.Categories.Find(id); // Find the category based on our ID and assign that to the variable

            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }
        
        // UPDATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name.");
            }
            if (ModelState.IsValid) // Need this to make sure our model is actually valid, AKA make sure they actually entered in all the information needed
            {
                _db.Categories.Update(obj);
                _db.SaveChanges(); // ALWAYS MAKE SURE TO SAVE THE CHANGES
                TempData["success"] = "Category updated successfully";
                return RedirectToAction("Index"); // Return to the Index action in this controller, basically our list
            }
            return View(obj);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound(); // Invalid ID
            }
            var categoryFromDb = _db.Categories.Find(id); // Find the category based on our ID and assign that to the variable

            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }

        // UPDATE
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _db.Categories.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            _db.Categories.Remove(obj);
            _db.SaveChanges(); // ALWAYS MAKE SURE TO SAVE THE CHANGES
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index"); // Return to the Index action in this controller, basically our list
        }
    }
}
