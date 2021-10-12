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
    public interface ILeaveService
    {
        int InsertLeaveRequest(LeaveViewModel lvm);
        MailViewModel UpdateLeaveStatusByLeaveID(LeaveViewModel lrvm);
        List<LeaveViewModel> GetLeaves();
        List<LeaveViewModel> GetLeavesByEmployeeID(int eid);
        LeaveViewModel GetLeaveByLeaveID(int lid);
    }
    public class LeaveService:ILeaveService
    {
        ILeaveRepository lr;
        public LeaveService()
        {
            lr = new LeaveRepository();
        }
        public int InsertLeaveRequest(LeaveViewModel lvm)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<LeaveViewModel, Leave>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            Leave l = mapper.Map<LeaveViewModel, Leave>(lvm);
            lr.InsertLeaveRequest(l);
            int lid = lr.GetLatestLeaveID();
            return lid;
        }
        public MailViewModel UpdateLeaveStatusByLeaveID(LeaveViewModel lrvm)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<LeaveViewModel, Leave>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            Leave l = mapper.Map<LeaveViewModel, Leave>(lrvm);
            Employee e=lr.UpdateStatusByLeaveID(l);

            var config1 = new MapperConfiguration(cfg => { cfg.CreateMap<Employee, MailViewModel>(); cfg.IgnoreUnmapped(); });
            IMapper mapper1 = config1.CreateMapper();
            MailViewModel mvm = mapper1.Map<Employee, MailViewModel>(e);

            mvm.LeaveStatus = lrvm.LeaveStatus;

            return mvm;
        }
        public List<LeaveViewModel> GetLeaves()
        {
            List<Leave> l = lr.GetLeaves();
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<Leave, LeaveViewModel>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            List<LeaveViewModel> lvm = mapper.Map<List<Leave>, List<LeaveViewModel>>(l);
            return lvm;
        }
        public List<LeaveViewModel> GetLeavesByEmployeeID(int eid)
        {
            List<Leave> l = lr.GetLeavesByEmployeeID(eid);
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<Leave, LeaveViewModel>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            List<LeaveViewModel> lvm = mapper.Map<List<Leave>, List<LeaveViewModel>>(l);
            return lvm;
        }
        public LeaveViewModel GetLeaveByLeaveID(int lid)
        {
            Leave l = lr.GetLeaveByLeaveID(lid).FirstOrDefault();
            LeaveViewModel lvm = null;
            if (l != null)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<Leave, LeaveViewModel>(); cfg.IgnoreUnmapped(); });
                IMapper mapper = config.CreateMapper();
                lvm = mapper.Map<Leave, LeaveViewModel>(l);

            }
            return lvm;
        }
    }
}
