using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZeroHunger.Auth;
using ZeroHunger.Entity;

namespace ZeroHunger.Controllers
{
    [EmployeeAccess]
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Dashboard()
        {
            return View();
        }


        [HttpGet]
        public ActionResult AssignedRequest()
        {
            int id = Convert.ToInt32(Session["employeeID"]);
            DB_Zero_HungerEntities requestDB = new DB_Zero_HungerEntities();
            var request = (from req in requestDB.Requests
                           where req.employee_id.Equals(id) && req.status.Equals("Assigned")
                          select req);
            return View(request);
        }
        
        [HttpPost]
        public ActionResult AssignedRequest(Request req)
        {
            var requestDB = new DB_Zero_HungerEntities();
            var request = (from r in requestDB.Requests
                           where r.id.Equals(req.id)
                           select r).SingleOrDefault();

            requestDB.Entry(request).CurrentValues.SetValues(req);
            requestDB.SaveChanges();
            return RedirectToAction("AssignedRequest", "Employee");
        }


        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login", "Home");
        }
    }
}