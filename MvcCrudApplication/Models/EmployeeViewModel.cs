using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MvcCrudApplication.Models
{
    public class EmployeeViewModel
    {
        public int EmployeeId { get; set; }

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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmployeeDepartment> EmployeeDepartments { get; set; }
    }

}
