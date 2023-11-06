using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;


namespace MvcCrudApplication.Models
{

    // [Bind(Exclude = "EmployeeId")]
    public class ViewModel
    {
        [Required(ErrorMessage = "FirstName is required.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "The LastName is required.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Gender value is required.")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        public string Address { get; set; }

        [RegularExpression(@"^\d{10}$", ErrorMessage = "Mobile number must be exactly 10 digits.")]
        public string MobileNumber { get; set; }

        [Required(ErrorMessage = "The Employee Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "pls ProfileImage attached.")]
        public string ProfileImage { get; set; }



        public Employee Employee { get; set; }
        //    public List<Department> Departments { get; set; }
        public EmployeeViewModel EmployeeViewModel { get; set; }
        public List<Department> Departments { get; set; }
        public int[] DepartmentIds { get; set; }

        public List<Employee> EmployeeList { get; set; }
        public List<SelectListItem> Department { get; set; }
        public string DepartmentNames { get; set; }
        public int DepartmentId { get; set; }
        public int SelectedDepartmentId { get; set; }

        [Required(ErrorMessage = "- Select item -")]
        public List<int> PreselectedDepartmentIds { get; set; }

        //public ViewModel()
        //{
        //    // Initialize EmployeeId as nullable (int?)
        //    Employee = new Employee { EmployeeId = null };
        //}
    }
}