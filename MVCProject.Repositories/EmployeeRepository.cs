using System;
using System.Collections.Generic;
using System.Linq;
using MVCProject.DomainModels;


namespace MVCProject.Repositories
{
    public interface IEmployeeRepository
    {
        void InsertEmployee(Employee e);
        void UpdateEmployeeDetails(Employee e);
        void UpdateEmployeePassword(Employee e);
        void DeleteEmployee(int eid);
        List<Employee> GetEmployees();
        List<Employee> GetEmployeesByEmailAndPassword(string Email, string Password);
        List<Employee> GetEmployeesByEmail(string Email);
        List<Employee> GetEmployeesByEmployeeID(int eid);
        int GetLatestEmployeeID();

    }
    public class EmployeeRepository : IEmployeeRepository
    {
        EmployeeDbContext db;
        public EmployeeRepository()
            {
                db=new EmployeeDbContext();
            }
        public void InsertEmployee(Employee e)
        {
            db.Employees.Add(e);
            db.SaveChanges();
        }
        public void UpdateEmployeeDetails(Employee e)
        {
            Employee em = db.Employees.Where(temp => temp.EmployeeID == e.EmployeeID).FirstOrDefault();
            if (em != null)
            {
                em.FirstName = e.FirstName;
                em.LastName = e.LastName;
                em.ContactNumber = e.ContactNumber;
                db.SaveChanges();
            }
        }
        public void UpdateEmployeePassword(Employee e)
        {
            Employee em = db.Employees.Where(temp => temp.EmployeeID == e.EmployeeID).FirstOrDefault();
            if (em != null)
            {
                em.PasswordHash = e.PasswordHash;//changes password
                db.SaveChanges();
            }
        }
        public void DeleteEmployee(int eid)
        {
            Employee em = db.Employees.Where(temp => temp.EmployeeID == eid).FirstOrDefault();
            if (em != null)
            {
                db.Employees.Remove(em);
                db.SaveChanges();
            }
        }
        public List<Employee> GetEmployees()//to return all the list of users
        {
            List<Employee> em = db.Employees.OrderBy(temp => temp.FirstName).ToList();
            return em;
        }
        public List<Employee> GetEmployeesByEmailAndPassword(string Email, string Password)
        {
            List<Employee> em = db.Employees.Where(temp => temp.Email == Email && temp.PasswordHash == Password).ToList();
            return em;

        }
        public List<Employee> GetEmployeesByEmail(string Email)
        {
            List<Employee> em = db.Employees.Where(temp => temp.Email == Email).ToList();
            return em;

        }
        public List<Employee> GetEmployeesByEmployeeID(int eid)
        {
            List<Employee> em = db.Employees.Where(temp => temp.EmployeeID == eid).ToList();
            return em;

        }
        public int GetLatestEmployeeID()
        {
            int eid = db.Employees.Select(temp => temp.EmployeeID).Max();
            return eid;
        }
    }
}
