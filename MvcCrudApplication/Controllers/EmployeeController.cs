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

using System.Text.RegularExpressions;
using System.Reflection.Emit;
using System.Data.Entity.Infrastructure;

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

        //public ActionResult AddEmployeeView()
        //{
        //    using (var context = new AvidclanCompanyEntities1())
        //    {
        //        var data = context.Departments.ToList();

        //        try
        //        {
        //                Employee existingEmployee = context.Employees.Find(employee.EmployeeId);
        //                if (existingEmployee == null && Id == 0)
        //                {
        //                    Employee blankEmployee = new Employee();
        //                    return View(blankEmployee);
        //                }
        //                else
        //                {

        //                    var empdata = context.Employees.Where(x => x.EmployeeId == Id).SingleOrDefault();

        //                    ViewModel model = new ViewModel();

        //                    var data1 = context.Departments.ToList();

        //                    model.Departments = data1;

        //                    model.Employee = empdata;

        //                    return View(model);
        //                }

        //            }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine(ex);
        //        }
        //       return View(data);
        //    }
        //}



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
        public ActionResult Saveemp(Employee employee,EmployeeDepartment employeeDepartment, HttpPostedFileBase ProfileImage, int[] DepartmentsIds)
        {
            // To open a connection to the database
            try
            {
                using (var context = new AvidclanCompanyEntities1())
                {
                    // Add data to the particular table
                    context.Employees.Add(employee);
                    context.SaveChanges();  // save the changes


                    employeeDepartment.EmployeeId = employee.EmployeeId;
                    foreach (var value in DepartmentsIds)
                    {
                        employeeDepartment.DepartmentId= value;
                        context.EmployeeDepartment.Add(employeeDepartment);
                        context.SaveChanges();
                    }
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

                                string newPath = Regex.Replace(saveloc, @"\\", "/");

                           //     string path = "~/Images/Profile Images/" + employee.EmployeeId + "/"+imagename;

                                employee.ProfileImage=newPath;

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

                var data1 = context.Departments.ToList();


                //foreach (var item in data)
                //{
                //   var matchingRecords = context.EmployeeDepartment.Where(o => o.EmployeeId== item.EmployeeId).ToList();
                    
                //    foreach(var item2 in matchingRecords)
                //    {            
                //        foreach (var item1 in data1)
                //        {
                //            var matchingRecords1 = context.Departments.Where(d => d.DepartmentId == item1.DepartmentId && d.DepartmentId == item2.DepartmentId).ToList();

                //          List<string> departmentNames = matchingRecords1.Select(d => d.DepartmentName).ToList();

                //       }
                //    }
                //}

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
                Employee employee = context.Employees.Find(EmployeeId);
      
                EmployeeDepartment employeeDepartment = new EmployeeDepartment();

                var empdepdata = context.EmployeeDepartment.FirstOrDefault(x => x.EmployeeId == EmployeeId);//fetching data
                
                context.EmployeeDepartment.Remove(empdepdata);
                context.SaveChanges();

                context.Employees.Remove(employee);
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

                var departments = context.Departments.ToList();

                //var selectedDepartment = new SelectList(departments, "DepartmentID", "DepartmentName", employeedep.DepartmentId);
                //ViewBag.Departments = selectedDepartment;

                var data1 = context.Departments.ToList();
                
                model.Departments = data1;

                if(data!=null)
                {
                    Employee employee = context.Employees.Find(Id);
                    EmployeeDepartment employeedep = context.EmployeeDepartment.Find(Id);

                  //  var selectedDepartment = new SelectList(departments, "DepartmentID", "DepartmentName", employeedep.DepartmentId);
                   // ViewBag.Departments = selectedDepartment;
                    model.Employee = data;
                }
                else
                {
                    Employee blankemployee = new Employee();
                    model.Employee=blankemployee;
                }
                return View(model);
            }
        }

       [HttpPost]
        public ActionResult Edit(ViewModel model, HttpPostedFileBase ProfileImage, int[] DepartmentIds)
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

                    EmployeeDepartment employeeDepartment = new EmployeeDepartment();

                    var empdepdata = context.EmployeeDepartment.FirstOrDefault(x => x.EmployeeId == model.Employee.EmployeeId);//fetching data

                    context.EmployeeDepartment.Remove(empdepdata);
                    context.SaveChanges();

                    employeeDepartment.EmployeeId = model.Employee.EmployeeId;

                    foreach (var value in DepartmentIds)
                    {
                        employeeDepartment.DepartmentId = value;
                        context.EmployeeDepartment.Add(employeeDepartment);
                        context.SaveChanges();
                    }
                    try
                    {
                        if (ProfileImage != null && ProfileImage.ContentLength > 0 || ProfileImage==null)
                        {
                            string imagename = Path.GetFileName(ProfileImage.FileName);
                            string imageext = Path.GetExtension(imagename);

                            string rootfolder = Server.MapPath("~/Images/Profile Images/");
                            string subfolderName = model.Employee.EmployeeId.ToString();
                            string subfolderpath = Path.Combine(rootfolder, subfolderName);

                            string[] files = Directory.GetFiles(subfolderpath);
                            if (files.Length != 0)
                            {
                                foreach (string file in files)
                                {
                                    System.IO.File.Delete(file);
                                }
                            }
                                if (Directory.Exists(subfolderpath))
                                {
                                    try
                                    {
                                        if (imageext == ".jpg" || imageext == ".png")
                                        {
                                            string fullPath = Path.Combine(rootfolder, subfolderpath);
                                            string saveloc = Path.Combine(fullPath, imagename);

                                            string rootfolder1 = "/Images/Profile Images/";
                                            string subfolderName1 = model.Employee.EmployeeId.ToString();
                                            string subfolderpath1 = Path.Combine(rootfolder1, subfolderName1);

                                            string savenewloc = Path.Combine(subfolderpath1, imagename);

                                            string savenewloc1 = Regex.Replace(savenewloc, @"\\", "/");

                                            string physicalPath = Server.MapPath(savenewloc1);

                                            model.Employee.ProfileImage = savenewloc1;
                                        //ProfileImage.SaveAs(model.Employee.ProfileImage);
                                            
                                            ProfileImage.SaveAs(physicalPath);

                                            context.Employees.AddOrUpdate(model.Employee);
                                            context.SaveChanges();

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
                else
                {
                    try
                    {
                          //if(model.Employee.FirstName == null)
                            //{
                            //    ViewBag.FirstNameError = "First name is required.";
                            //}
                            // Add data to the particular table
                            context.Employees.Add(model.Employee);
                            context.SaveChanges();  // save the changes

                            EmployeeDepartment employeeDepartment = new EmployeeDepartment();

                            employeeDepartment.EmployeeId = model.Employee.EmployeeId;

                            foreach (var value in DepartmentIds)
                            {
                                employeeDepartment.DepartmentId = value;
                                context.EmployeeDepartment.Add(employeeDepartment);
                                context.SaveChanges();
                            }

                            if (ProfileImage != null && ProfileImage.ContentLength > 0)
                            {
                                string imagename = Path.GetFileName(ProfileImage.FileName);
                                //      var fileName = Path.GetFileName(ProfileImage.FileName);
                                string imageext = Path.GetExtension(imagename);

                                //   string imgpath = Path.Combine(Server.MapPath("~/Images/Profile Images/"+employee.EmployeeId), imagename);

                                string rootfolder = Server.MapPath("~/Images/Profile Images/");
                                string subfolderName = model.Employee.EmployeeId.ToString();
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

                                            string rootfolder1 = "/Images/Profile Images/";
                                            string subfolderName1 = model.Employee.EmployeeId.ToString();
                                            string subfolderpath1 = Path.Combine(rootfolder1, subfolderName1);
                                            string savenewloc = Path.Combine(subfolderpath1, imagename);
                                            string savenewloc1 = Regex.Replace(savenewloc, @"\\", "/");
                                            string physicalPath = Server.MapPath(savenewloc1);

                                            ProfileImage.SaveAs(physicalPath);
                                            //     string path = "~/Images/Profile Images/" + employee.EmployeeId + "/"+imagename;
                                            model.Employee.ProfileImage = savenewloc1;

                                            context.Employees.AddOrUpdate(model.Employee);
                                            context.SaveChanges();
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(ex.Message);
                                    }
                                }
                            }
                        
                        else
                        {
                            //var data1 = context.Departments.ToList();
                            //return View(data1);
                            return View(model);
                           // return RedirectToAction("Edit");
                            
                        }
                    }
                    catch (Exception ex) {
                        Console.WriteLine(ex.Message);
                    }
                }
                return RedirectToAction("ListView");
            }
        }
    }
}


//List<PgBean> pg = stmt.query(
//                "select p.*,u.firstname,u.lastname,s.timeduration,s.amount from pg p,users u,subscription s where p.userId=u.userId and s.subid=p.subid ",
//                new BeanPropertyRowMapper<PgBean>(PgBean.class));
//return pg;