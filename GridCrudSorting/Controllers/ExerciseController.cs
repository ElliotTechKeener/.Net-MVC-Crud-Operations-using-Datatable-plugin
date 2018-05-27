using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using GridCrudSorting.Models;

namespace GridCrudSorting.Controllers
{
    public class ExerciseController : Controller
    {
        // GET: Exercise List
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetData()
        {
            using (DBModel db = new DBModel())
            {
                List<ExerciseRecord> exeList = db.ExerciseRecords.ToList<ExerciseRecord>();

                return Json(new {data = exeList}, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View(new ExerciseRecord());
        }

        [HttpPost]
        public ActionResult Add(ExerciseRecord obj)
        {
            using (DBModel db = new DBModel())
            {
                db.ExerciseRecords.Add(obj);
                db.SaveChanges();
                return Json(new { success = true, message = "Saved Successfully" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult ExerciseAlreadyExists(string newName)
        {
            using (DBModel db = new DBModel())
            {
                var result =  db.ExerciseRecords.Any(x => x.ExerciseName == newName);
                return Json(!result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            {
                using (DBModel db = new DBModel())
                {
                    return View(db.ExerciseRecords.Where(x => x.Id == id).FirstOrDefault<ExerciseRecord>());
                }
            }
        }

        [HttpPost]
        public ActionResult Edit(ExerciseRecord obj)
        {
            using (DBModel db = new DBModel())
            {
                db.Entry(obj).State = EntityState.Modified;
                db.SaveChanges();
                return Json(new { success = true, message = "Updated Successfully" }, JsonRequestBehavior.AllowGet);               
            }
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            using (DBModel db = new DBModel())
            {
                ExerciseRecord obj = db.ExerciseRecords.Where(x => x.Id == id).FirstOrDefault<ExerciseRecord>();
                db.ExerciseRecords.Remove(obj);
                db.SaveChanges();
                return Json(new { success = true, message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
} 