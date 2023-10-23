using Microsoft.Ajax.Utilities;
using MvcCrudApplication.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace MvcCrudApplication.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee

        /*    public ActionResult Home()
            {
                // return View();
                return RedirectToAction("AddEmployeeView");
            }
        */

        public ActionResult AddEmployeeView()
        {
            using (var context = new AvidclanCompanyEntities1())
            {
                var data = context.Departments.ToList();
                return View(data);
            }
        }

        [HttpPost]
        //    public ActionResult Saveemp(EmployeeModel employee)
        public ActionResult Saveemp(Employee employee, HttpPostedFileBase ProfileImage)
        {
            // To open a connection to the database
            try
            {
                using (var context = new AvidclanCompanyEntities1())
                {
                    // Add data to the particular table
                    context.Employees.Add(employee);
                    context.SaveChanges();
                    // save the changes

                }
                if (ProfileImage != null && ProfileImage.ContentLength > 0)
                {
                    string imagename = Path.GetFileName(ProfileImage.FileName);
              //      var fileName = Path.GetFileName(ProfileImage.FileName);
                    string imageext = Path.GetExtension(imagename);

                    //   string imgpath = Path.Combine(Server.MapPath("~/Images/Profile Images/"+employee.EmployeeId), imagename);

                    string rootfolder = Server.MapPath("~/Images/Profile Images/");
                    string subfolderName = employee.EmployeeId.ToString();
                    string subfolderpath = Path.Combine(rootfolder, subfolderName);

                    if (!Directory.Exists(subfolderpath))
                    {
                        try
                        {
                            Directory.CreateDirectory(subfolderpath);
                         //   string Text = "Subfolder created successfully.";

                            if (imageext == ".jpg" || imageext == ".png")
                            {
                                // Specify the folder where you want to store the image
                                // string folderPath = Server.MapPath(subfolderpath); // Change this path as needed

                                //string fullPath = Path.Combine(subfolderpath, imagename);
                                //string saveloc=Server.MapPath(fullPath+"\\"+ imagename);
                                string fullPath = Path.Combine(rootfolder, subfolderpath);
                                string saveloc = Path.Combine(fullPath, imagename);
                               
                                
                                ProfileImage.SaveAs(saveloc);

                                employee.ProfileImage=saveloc;
                                using (var context = new AvidclanCompanyEntities1())
                                {
                                    context.Employees.AddOrUpdate(employee);
                                    context.SaveChanges();
                                }


                            }
                        }
                        catch (Exception ex)
                        {
                            var Text = "Error creating subfolder: " + ex.Message;
                        }
                    }

                }
            }

            catch (DbEntityValidationException e)
            {
                Console.WriteLine(e);
            }
            return RedirectToAction("ListView");
            //return View();
        
        }
        [HttpGet]
        public ActionResult ListView()
        {
            using (var context = new AvidclanCompanyEntities1())
            {
                var data = context.Employees.ToList();
                return View(data);
            }
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            bool response=DeleteData(id);
            if (response==true)
            {
                return RedirectToAction("ListView");
            }
            return View();
               
        }
        public bool DeleteData(int EmployeeId)
        {
            using (var context = new AvidclanCompanyEntities1())
            {
                //context.Employees.Remove(EmployeeId);
                // context.SaveChanges();
                Console.WriteLine(EmployeeId);
                var data = context.Employees.FirstOrDefault(x => x.EmployeeId == EmployeeId);//fetching data
                
                context.Employees.Remove(data);
                context.SaveChanges();
                return true;  
            }
        }
        [HttpGet]
        public ActionResult Edit(int? Id)
        {
            using (var context = new AvidclanCompanyEntities1())
            {
                var data = context.Employees.Where(x => x.EmployeeId == Id).SingleOrDefault();

                // ViewData["Employee"] = data;

                ViewModel model = new ViewModel();
                
                var data1 = context.Departments.ToList();
                
                model.Departments = data1;
                
                model.Employee = data;

                return View(model);
            }
        }



       [HttpPost]
        public ActionResult Edit(ViewModel model, HttpPostedFileBase ProfileImage)
        {
            using (var context = new AvidclanCompanyEntities1())
            {

                //    // Use of lambda expression to access
                //    // particular record from a database
                var data = context.Employees.FirstOrDefault(x => x.EmployeeId == model.Employee.EmployeeId);

                // Checking if any such record exist 
                if (data != null)
                {

                    data.FirstName = model.Employee.FirstName;
                    data.LastName = model.Employee.LastName;
                    data.Address = model.Employee.Address;
                    data.EmailAddress = model.Employee.EmailAddress;
                    data.MobileNumber = model.Employee.MobileNumber;
                    data.Gender = model.Employee.Gender;
                    //model.Employee.ProfileImage = ProfileImage.ToString();
                    model.Employee.DepartmentId = model.DepartmentId;

                    try
                    {
                        if (ProfileImage != null && ProfileImage.ContentLength > 0)
                        {
                            string imagename = Path.GetFileName(ProfileImage.FileName);
                            string imageext = Path.GetExtension(imagename);

                            string rootfolder = Server.MapPath("~/Images/Profile Images/");
                            string subfolderName = model.Employee.EmployeeId.ToString();
                            string subfolderpath = Path.Combine(rootfolder, subfolderName);

                            string[] files = Directory.GetFiles(subfolderpath);

                            foreach (string file in files)
                            {
                                System.IO.File.Delete(file);
                            }
                            if (Directory.Exists(subfolderpath))
                            {
                                try
                                {
                                    if (imageext == ".jpg" || imageext == ".png")
                                    {
                                        string fullPath = Path.Combine(rootfolder, subfolderpath);
                                        string saveloc = Path.Combine(fullPath, imagename);
                                        ProfileImage.SaveAs(saveloc);
                                        model.Employee.ProfileImage = saveloc;                                      
                                    }
                                }
                                catch (Exception ex)
                                {
                                    var Text = "Error creating subfolder: " + ex.Message;
                                }
                            }
                        }
                        context.Employees.AddOrUpdate(model.Employee);
                        context.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        var Text = "Error: " + ex.Message;
                    }
                    return RedirectToAction("ListView");
                }
               return View();
            }
        }
  }
}