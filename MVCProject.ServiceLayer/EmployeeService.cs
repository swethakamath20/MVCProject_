using System;
using System.Collections.Generic;
using System.Linq;
using MVCProject.Repositories;
using MVCProject.ViewModels;
using MVCProject.DomainModels;
using AutoMapper;
using AutoMapper.Configuration;

namespace MVCProject.ServiceLayer
{
    public interface IEmployeeService
    {
        int InsertEmployee(EmployeeViewModel evm);
        void DeleteEmployee(int eid);
        List<EmployeeViewModel> GetEmployees();
        EmployeeViewModel GetEmployeesByEmailAndPassword(string Email, string Password);
        EmployeeViewModel GetEmployeesByEmail(string Email);
        EmployeeViewModel GetEmployeesByEmployeeID(int eid);
        void UpdateEmployeeDetails(EditEmployeeViewModel evm);
        
    }

    public class EmployeeService:IEmployeeService
    {
        IEmployeeRepository er;
        public EmployeeService()
        {
            er = new EmployeeRepository();
        }
        public int InsertEmployee(EmployeeViewModel evm)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<EmployeeViewModel, Employee>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            Employee e = mapper.Map<EmployeeViewModel, Employee>(evm);
            e.PasswordHash = SHA256HashGenerator.GenerateHash(evm.PasswordHash);
            er.InsertEmployee(e);
            int eid = er.GetLatestEmployeeID();
            return eid;
        }
        public void DeleteEmployee(int eid)
        {
            er.DeleteEmployee(eid);
        }
    
        public List<EmployeeViewModel> GetEmployees()
        {
            List<Employee> e = er.GetEmployees();
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<Employee, EmployeeViewModel>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            List<EmployeeViewModel> evm= mapper.Map<List<Employee>, List< EmployeeViewModel >> (e);
            return evm;

        }
        public EmployeeViewModel GetEmployeesByEmailAndPassword(string Email, string Password)
        {
            Employee e = er.GetEmployeesByEmailAndPassword(Email, SHA256HashGenerator.GenerateHash(Password)).FirstOrDefault();
            EmployeeViewModel evm = null;
            if (e != null)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<Employee, EmployeeViewModel>(); cfg.IgnoreUnmapped(); });
                IMapper mapper = config.CreateMapper();
                evm = mapper.Map<Employee,EmployeeViewModel>(e);
                
            }
            return evm;

        }
        public EmployeeViewModel GetEmployeesByEmail(string Email)
        {
            Employee e = er.GetEmployeesByEmail(Email).FirstOrDefault();
            EmployeeViewModel evm = null;
            if (e != null)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<Employee, EmployeeViewModel>(); cfg.IgnoreUnmapped(); });
                IMapper mapper = config.CreateMapper();
                evm = mapper.Map<Employee, EmployeeViewModel>(e);

            }
            return evm;

        }
        public EmployeeViewModel GetEmployeesByEmployeeID(int eid)
        {
            Employee e = er.GetEmployeesByEmployeeID(eid).FirstOrDefault();
            EmployeeViewModel evm = null;
            if (e != null)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<Employee, EmployeeViewModel>(); cfg.IgnoreUnmapped(); });
                IMapper mapper = config.CreateMapper();
                evm = mapper.Map<Employee, EmployeeViewModel>(e);

            }
            return evm;

        }
        public void UpdateEmployeeDetails(EditEmployeeViewModel evm)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<EditEmployeeViewModel, Employee>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            Employee e = mapper.Map<EditEmployeeViewModel, Employee>(evm);
            er.UpdateEmployeeDetails(e);
        }

    }
}
