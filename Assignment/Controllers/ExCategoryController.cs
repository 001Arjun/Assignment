using Assignment.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment.Controllers
{
    public class ExCategoryController : Controller
    {
        // GET: ExCategory
        DatabaseConnection Db_Conn = new DatabaseConnection();
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddCategory(ExCategory e)
        {
            if (ModelState.IsValid)
            {
                ExCategory e1 = new ExCategory();
                // e1.Name = "Harsh";
                //e1.Ex_Limit = 2;
                e1.Name = e.Name;
                e1.Ex_Limit = e.Ex_Limit;

                Db_Conn.Model_Category.Add(e1);
                TempData["AlertMsg"] = "New Category Inserted...";
                Db_Conn.SaveChanges();
                return RedirectToAction("ListCategory");
            }
            ModelState.Clear();

            return View("Create");
        }
        public ActionResult Listcategory()
        {
            var list = Db_Conn.Model_Category.ToList();
            Db_Conn.SaveChanges();

            return View(list);
        }
        public ActionResult Dashboard()
        {
            var list = Db_Conn.Model_Category.ToList();
            Db_Conn.SaveChanges();
            return View(list);   
        }


        public ActionResult Delete(int id)
        {
            var del = Db_Conn.Model_Category.Where(h => h.Id == id).First();
            Db_Conn.Model_Category.Remove(del);
            Db_Conn.SaveChanges();
            TempData["AlertMsg"] = "Record Deleted...";

            return RedirectToAction("ListCategory");

            //var list = Db_Conn.Model_Category.ToList();
            //Db_Conn.SaveChanges();

            return View("Create");
        }
        [HttpGet]
        public ActionResult EditCategory(int id)
        {
            var row = Db_Conn.Model_Category.Where(h => h.Id == id).FirstOrDefault();
            return View(row);
        }

        [HttpPost]
        public ActionResult EditCategory(ExCategory e)
        {
            ExCategory e1 = new ExCategory();
            if (ModelState.IsValid)
            {
                e1.Id = e.Id;
                e1.Name = e.Name;
                e1.Ex_Limit = e.Ex_Limit;

                Db_Conn.Entry(e1).State = EntityState.Modified;
                Db_Conn.SaveChanges();
                TempData["AlertMsg"] = "Record Updated Successful...";

                return RedirectToAction("ListCategory");
            }
            ModelState.Clear();
            return View("Create");
            // var row = Db_Conn.Model_Category.Where(h => h.Id == id).FirstOrDefault();
        }
        //--------------------------


        public ActionResult CreateExp()
        {
            var delist = Db_Conn.Model_Category.ToList();
            ViewBag.Id = new SelectList(delist, "Id", "Name");

            return View();
        }

        [HttpPost]
        public ActionResult AddExpense(Expenss ex)
        {
            Expenss e_obj = new Expenss();

            decimal total_exp = Db_Conn.Model_Expense.Where(h => h.Id == ex.Id).ToList().Sum(h => h.Amount);
            ExCategory exCat = Db_Conn.Model_Category.FirstOrDefault(h => h.Id == ex.Id);

            total_exp = total_exp + ex.Amount;

            decimal remain = Db_Conn.Model_Expense.Where(h => h.Id == ex.Id).ToList().Sum(h => h.Amount);
    
            

            

            if (total_exp > exCat.Ex_Limit)
            {
                TempData["AlertMssg"] = "out of limit Your Limit is " + total_exp ;
            }

            else
            {
                if (ModelState.IsValid)
                {
                    var delist = Db_Conn.Model_Category.ToList();
                    ViewBag.Id = new SelectList(delist, "Id", "Name");

                    e_obj.Title = ex.Title;
                    e_obj.Description = ex.Description;
                    e_obj.Amount = ex.Amount;
                    e_obj.Id = ex.Id;
                    e_obj.Date = DateTime.Now;

                    Db_Conn.Model_Expense.Add(e_obj);
                    TempData["AlertMsg"] = "New Category Inserted...";
                    Db_Conn.SaveChanges();
                    return RedirectToAction("ListExpense");
                }
            }
            ModelState.Clear();




            return RedirectToAction("ListExpense");
        }
        public ActionResult ListExpense()
        {
            List<Expenss> exp = Db_Conn.Model_Expense.ToList();
            List<ExCategory> excat = Db_Conn.Model_Category.ToList();

            var list = from e in exp
                       join d in excat on e.Id equals d.Id into Table1
                       from d in Table1.ToList()
                       select new ViewModel
                       {
                           expense = e,
                           exCategory = d
                       };

            return View(list);
        }
      
        public ActionResult CatNameSearch(int id)
        {
            List<Expenss> exp = Db_Conn.Model_Expense.ToList();
            List<ExCategory> excat = Db_Conn.Model_Category.ToList();



            var list = from e in exp
                       join d in excat on e.Id equals d.Id into Table1
                       from d in Table1.Where(h => h.Id == id).ToList()
                       select new ViewModel
                       {
                           expense = e,
                           exCategory = d
                       };

            //ViewBag.totalrow = Db_Conn.Model_Category.Count();


            return View(list);
        }



        public ActionResult DeleteEx(int exp_id)
        {
            var del = Db_Conn.Model_Expense.Where(h => h.Exp_Id == exp_id).First();
            Db_Conn.Model_Expense.Remove(del);
            Db_Conn.SaveChanges();
            TempData["AlertMsg"] = "Record Deleted...";
            return RedirectToAction("ListExpense");

            return View("CreateExp");
        }

        [HttpGet]
        public ActionResult EditExpense(int exp_id)
        {
            var delist = Db_Conn.Model_Category.ToList();
            ViewBag.Id = new SelectList(delist, "Id", "Name");


            var row = Db_Conn.Model_Expense.Where(h => h.Exp_Id == exp_id).FirstOrDefault();
            return View(row);
        }

        [HttpPost]
        public ActionResult EditExpense(Expenss ex)
        {
            var delist = Db_Conn.Model_Category.ToList();
            ViewBag.Id = new SelectList(delist, "Id", "Name");


            Expenss e1 = new Expenss();
            if (ModelState.IsValid)
            {
                e1.Exp_Id = ex.Exp_Id;
                e1.Title = ex.Title;
                e1.Description = ex.Description;
                e1.Amount = ex.Amount;
                e1.Id = ex.Id;
                e1.Date = DateTime.Now;

                Db_Conn.Entry(e1).State = EntityState.Modified;
                Db_Conn.SaveChanges();
                TempData["AlertMsg"] = "Record Updated Successful...";

                return RedirectToAction("ListExpense");
            }
            ModelState.Clear();

            return View();
        }



    }
}