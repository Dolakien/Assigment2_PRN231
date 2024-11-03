using BusinessObject.Models;
using DataAccessObject;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RoleRepo : IRoleRepo
    {
        public bool addRole(Role role)
            => RoleDAO.Instance.addRole(role);

        public Role GetRole(int id)
            => RoleDAO.Instance.GetRole(id);


        public List<Role> GetRoles()
            => RoleDAO.Instance.GetRoles();


        public bool removeRole(int roleId)
            => RoleDAO.Instance.removeRole(roleId);

        public bool updateRole(Role role)
            => RoleDAO.Instance.updateRole(role);
    }
}
