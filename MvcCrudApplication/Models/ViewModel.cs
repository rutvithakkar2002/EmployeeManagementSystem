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

          // [Required]
          // public string FirstName { get; set; }

          // [Required(ErrorMessage = "Please enter LastName value")]    
          //  public string LastName { get; set; }

          //  [Required(ErrorMessage = "Please enter Gender value")]    
          //  public string Gender { get; set; }
            
          //  [Required(ErrorMessage = "Please enter valid address")]    
          //  public string Address { get; set; }

          //  [Required(ErrorMessage = "Please enter MobileNumber")]    
          //  public string MobileNumber { get; set; }

          //  [Required(ErrorMessage = "Please enter EmailAddress value")]    
          //  public string EmailAddress { get; set; }

          //  [Required(ErrorMessage = "Please attach imageprofile")]
          //   public string ProfileImage { get; set; }

           


        public Employee Employee { get; set; }
        //    public List<Department> Departments { get; set; }

        //    public EmployeeViewModel EmployeeViewModel { get; set; }

            public List<Department> Departments { get; set; }

            public int[] DepartmentIds { get; set; } 

            public List<SelectListItem> Department { get; set; }
            public string DepartmentName {  get; set; }
            public int DepartmentId { get; set;}
            public int SelectedDepartmentId { get; set; }

    }
}