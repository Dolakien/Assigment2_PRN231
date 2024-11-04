using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Contract.Request
{
    public class CreateAccountRequest
    {
        [Required(ErrorMessage = "Account ID is required.")]
        public int AccountId { get; set; }

        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "FullName is required.")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string AccountPassword { get; set; }

        [Required(ErrorMessage = "Role is required.")]
        public int RoleID { get; set; }
    }
}
