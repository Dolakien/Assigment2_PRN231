﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Contract.Response
{
    public class LoginResponse
    {
        public int AccountId { get; set; }

        public string Email { get; set; }

        public string FullName { get; set; }

        public string RoleName { get; set; }

        public string JwtToken { get; set; }
    }
}
