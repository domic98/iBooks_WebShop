using DomicWeb.DataAccess.Data;
using DomicWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace DomicWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db; //to sve možemo jer smo prvo registrirali u Services u Program.cs
        public CategoryController(ApplicationDbContext db) //kako da vidimo u index view naše podatke
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<Category> objCategoryList = _db.Categories.ToList(); //radimo listu za prikaz kategorija
            return View(objCategoryList);
        }

        public IActionResult Create() //ako ne dodamo HttpGet, po defaulte je Get
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "Display Order cannot exactly match the Name"); //key se odnosi na šta govorimo pogreškiu, pod unos Name
            }

            if (ModelState.IsValid) //ode u model Category i provjeri jesu li svi uvjeti zadovoljeni(npr.not null, dužina imena, itd)
                                    //ako jesu onda dodaje objekt u Categories i sprema promjene u db
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                TempData["success"]= "Category created successfully";
                return RedirectToAction("Index");
            }

            return View();//ako želimo preusmjerit na drugu akciju koja nije u ovom kontroleru
                          //onda moramo napisati i u koji kontroler da ode: return RedirectToAction("Index","Category")
        }
        public IActionResult Edit(int? id)
        {
            if (id==null || id == 0)
            {
                return NotFound();
            }
            Category? categoryFromDb = _db.Categories.Find(id); //3 opcije kako naći jednu od kategorija po id-u
            //Category ?categoryFromDb1 = _db.Categories.FirstOrDefault(u=>u.Id==id);
            //Category? categoryFromDb2 = _db.Categories.Where(u => u.Id==id).FirstOrDefault();
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }

        [HttpPost]
        public IActionResult Edit(Category obj)
        {

            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
                TempData["success"]= "Category updated successfully";
                return RedirectToAction("Index");
            }

            return View();

        }
        public IActionResult Delete(int? id)
        {
            if (id==null || id == 0)
            {
                return NotFound();
            }
            Category? categoryFromDb = _db.Categories.Find(id); 
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Category obj = _db.Categories.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Categories.Remove(obj);
            _db.SaveChanges();
            TempData["success"]= "Category deleted successfully";
            return RedirectToAction("Index");
        }



    }
}
