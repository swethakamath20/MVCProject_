using System;
using System.Collections.Generic;
using System.Linq;
using MVCProject.DomainModels;


namespace MVCProject.Repositories
{
    public interface ILeaveRepository
    {
        void InsertLeaveRequest(Leave l);
        Employee UpdateStatusByLeaveID(Leave l);
        List<Leave> GetLeaves();
        List<Leave> GetLeavesByEmployeeID(int eid);
        int GetLatestLeaveID();
        List<Leave> GetLeaveByLeaveID(int lid);

    }
    public class LeaveRepository:ILeaveRepository
    {
        EmployeeDbContext db;
        public LeaveRepository()
        {
            db = new EmployeeDbContext();
        }
        public void InsertLeaveRequest(Leave l)
        {
            db.Leaves.Add(l);
            db.SaveChanges();
        }
        public Employee UpdateStatusByLeaveID(Leave l)
        {
            Leave newl = db.Leaves.Where(temp => temp.LeaveID == l.LeaveID).FirstOrDefault();
            if (newl != null)
            {
                newl.LeaveStatus = l.LeaveStatus;
                db.SaveChanges();
            }
            Employee emp = db.Employees.Where(x => x.EmployeeID == l.EmployeeID).ToList().FirstOrDefault();
            return emp;

        }

        public List<Leave> GetLeaves()
        {
            List<Leave> ll = db.Leaves.OrderByDescending(temp=>temp.LeaveID).ToList();
            return ll;
        }
        public List<Leave> GetLeavesByEmployeeID(int eid)
        {
            List<Leave> ll = db.Leaves.Where(Temp => Temp.EmployeeID == eid).OrderByDescending(s=>s.LeaveID).ToList();
            return ll;
        }
        public int GetLatestLeaveID()
        {
            int lid = db.Leaves.Select(temp => temp.LeaveID).Max();
            return lid;
        }
        public List<Leave> GetLeaveByLeaveID(int lid)
        {
            List<Leave> ll = db.Leaves.Where(Temp => Temp.LeaveID == lid).ToList();
            return ll;
        }
    }
}
