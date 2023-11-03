using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcCrudApplication.Models
{
    public class EmpListViewModel
    {
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string MobileNumber { get; set; }
        public string EmailAddress { get; set; }
        public string ProfileImage { get; set; }    
        public string Departments { get; set; }  
    }
}