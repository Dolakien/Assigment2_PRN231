using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObject
{
    public class RoleDAO
    {
        private SilverJewelry2023DbContext _context;
        private static RoleDAO instance;

        public RoleDAO()
        {
            _context = new SilverJewelry2023DbContext();
        }

        public static RoleDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new RoleDAO();
                }
                return instance;
            }
        }

        public List<Role> GetRoles()
        {
            return _context.Roles.ToList();
        }

        public bool addRole(Role role)
        {
            bool result = false;
            Role role1 = this.GetRole(role.RoleId);
            if (role1 == null)
            {
                try
                {
                    _context.Roles.Add(role);
                    _context.SaveChanges();
                    result = true;
                }
                catch (Exception ex)
                {
                }
            }
            return result;
        }

        public bool updateRole(Role role)
        {
            bool result = false;
            Role role1 = this.GetRole(role.RoleId);
            if (role1 != null)
            {
                try
                {
                    _context.Entry(role1).CurrentValues.SetValues(role);
                    _context.SaveChanges();
                    result = true;
                }
                catch (Exception ex)
                {
                }
            }
            return result;
        }

        public bool removeRole(int roleId)
        {
            bool result = false;
            Role role1 = this.GetRole(roleId);
            if (role1 != null)
            {
                try
                {
                    _context.Roles.Remove(role1);
                    _context.SaveChanges();
                    result = true;
                }
                catch (Exception ex)
                {
                }
            }
            return result;
        }

        public Role GetRole(int roleId)
        {
            return _context.Roles.SingleOrDefault(m => m.RoleId.Equals(roleId));

        }







    }
}
