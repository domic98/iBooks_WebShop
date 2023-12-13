using DomicWeb.DataAccess.Data;
using DomicWeb.DataAccess.Repository.IRepository;
using DomicWeb.Models;
using DomicWeb.Models.ViewModels;
using DomicWeb.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DomicWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = SD.Role_Admin)]
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            

        }
        public IActionResult Index()
        {
            List<Company> objCompanyList = _unitOfWork.Company.GetAll().ToList();
           
            return View(objCompanyList);
        }

        public IActionResult Upsert(int? id)

        {
            
            // ViewBag.CategoryList = CategoryList;
            // ViewData["CategoryList"] = CategoryList;

            //CompanyVM companyVM = new()
            //{
            //    CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
            //   {
            //       Text = u.Name,
            //       Value = u.Id.ToString()
            //   }),

            //    Company = new Company()
            
            if (id == null || id ==0 )
            {
                // create
                return View(new Company());
            }
            else
            {
                // update
                Company companyObj = _unitOfWork.Company.Get(u => u.Id == id);
                return View(companyObj);

            }

            
        }

        [HttpPost]
        public IActionResult Upsert(Company CompanyObj)
        {
         

            if (ModelState.IsValid)

            {
               
                if(CompanyObj.Id == 0)
                {
                    _unitOfWork.Company.Add(CompanyObj);
                }
                else
                {
                    _unitOfWork.Company.Update(CompanyObj);
                }
                
                _unitOfWork.Save();
                TempData["success"] = "Company created successfully";
                return RedirectToAction("Index");
            }
            else
            {

                return View(CompanyObj);
            }

            

        }
        /*
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Company? companyFromDb = _unitOfWork.Company.Get(u => u.Id == id); //3 opcije kako naći jednu od kategorija po id-u
            //Company ?companyFromDb1 = _db.Categories.FirstOrDefault(u=>u.Id==id);
            //Company? companyFromDb2 = _db.Categories.Where(u => u.Id==id).FirstOrDefault();
            if (companyFromDb == null)
            {
                return NotFound();
            }
            return View(companyFromDb);
        }
        */
        /*
        [HttpPost]
        public IActionResult Edit(Company obj)
        {

            if (ModelState.IsValid)
            {
                _unitOfWork.Company.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Company updated successfully";
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
            Company? companyFromDb = _unitOfWork.Company.Get(u => u.Id == id);
            if (companyFromDb == null)
            {
                return NotFound();
            }
            return View(companyFromDb);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Company obj = _unitOfWork.Company.Get(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.Company.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Company deleted successfully";
            return RedirectToAction("Index");
        }
        */

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Company> objCompanyList = _unitOfWork.Company.GetAll().ToList();
            return Json (new {data = objCompanyList });

        }


        [HttpDelete]
        public IActionResult Delete(int? id)
        {

            var companyToBeDeleted = _unitOfWork.Company.Get(u => u.Id == id);

            if ( companyToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting! " });
            }
            
          
            _unitOfWork.Company.Remove(companyToBeDeleted);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Delete Successful" });

        }

        #endregion

    }
}
