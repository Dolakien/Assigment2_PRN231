using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IRoleRepo
    {
        public Role GetRole(int id);
        public List<Role> GetRoles();
        public bool addRole(Role role);
        public bool removeRole(int roleId);
        public bool updateRole(Role role); 
    }
}
