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
          //public int EmployeeId { get; set; }

          [Required(ErrorMessage ="pls enter the firstname.")]
          public string FirstName { get; set; }

           [Required]
           public string LastName { get; set; }
           [Required]
           public string Gender { get; set; }

            [Required]
            public string Address { get; set; }

            [Required]
            public string MobileNumber { get; set; }
    
            [Required]
            [EmailAddress]
            public string EmailAddress { get; set; }
            
            [Required]
            public string ProfileImage { get; set; }

            public Employee Employee { get; set; }
        //    public List<Department> Departments { get; set; }

        //    public EmployeeViewModel EmployeeViewModel { get; set; }

            public List<Department> Departments { get; set; }

            [Required]
            public int[] DepartmentIds { get; set; } 

            public List<SelectListItem> Department { get; set; }
            public string DepartmentName {  get; set; }
            public int DepartmentId { get; set;}
            public int SelectedDepartmentId { get; set; }


  


    }
}