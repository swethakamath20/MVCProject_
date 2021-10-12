using System;
using System.Collections.Generic;
using System.Linq;
using MVCProject.DomainModels;

namespace MVCProject.Repositories
{
    public interface IRolesRepository
    {
        void InsertRole(Role r);
        void DeleteRole(int rid);
        List<Role> GetRoles();
        List<Role> GetRoleByRoleID(int rid);
    }
    public class RolesRepository:IRolesRepository
    {
        EmployeeDbContext db;
        public RolesRepository()
        {
            db = new EmployeeDbContext();
        }
        public void InsertRole(Role r)
        {
            db.Roles.Add(r);
            db.SaveChanges();
        }
        public void DeleteRole(int rid)
        {
            Role role = db.Roles.Where(temp => temp.RoleID == rid).FirstOrDefault();
            if (role != null)
            {
                db.Roles.Remove(role);
                db.SaveChanges();
            }
        }
        public List<Role> GetRoles()
        {
            List<Role> role = db.Roles.ToList();
            return role;
        }
        public List<Role> GetRoleByRoleID(int rid)
        {
            List<Role> role = db.Roles.Where(temp => temp.RoleID == rid).ToList();
            return role;
        }
    }
}
