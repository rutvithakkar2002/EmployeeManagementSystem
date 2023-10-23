using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcCrudApplication.Models
{
    public class ViewModel
    {
     /*   public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string MobileNumber { get; set; }
        public string EmailAddress { get; set; }
        public int DepartmentId { get; set; }*/
        public Employee Employee { get; set; }
        //    public List<Department> Departments { get; set; }
        public List<Department> Departments { get; set; }

        public List<SelectListItem> Department { get; set; }
        public string DepartmentName {  get; set; }
         public int DepartmentId { get; set;}


        public int SelectedDepartmentId { get; set; }

    }
}