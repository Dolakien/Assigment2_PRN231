using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Contract.Request
{
    public class CreateAccountRequest
    {
        public int AccountId { get; set; }

        public string EmailAddress { get; set; }

        public string FullName { get; set; }

        public string AccountPassword { get; set; }

        public int RoleID { get; set; }
    }
}
