using BOL.Models;
using EF.Core.Repository.Interface.Manager;
using System.Data;

namespace BLL.Interfaces.Manager.ManualAttendances
{
    public interface IManualAttendancesManager : ICommonManager<Attendance_DbModel>
    {
        Task<DataSet> ManualAttendance(int Emp_serial, DateTime date, decimal intime, DateTime outdate, decimal outtime);
        Task<DataSet> ManualAttendance_select(int Emp_serial, DateTime date);
        Task<DataSet> ManualAttendance_viw(int comp, DateTime Sdate, DateTime Edate, int IND);
        Task<DataSet> ManualAttendancefilter(int? Compid = null, int? Department = null, int? section = null, int? Building = null, int? Floor = null, int? Line = null);
        Task<DataSet> shiftlist(int CompID);
        Task<List<ManualAttSeletedList>> GetManualAttList(List<ManualAttPara> para);
        Task<bool> SaveManualAtt(List<ManualAttSavePara> para);
        Task<string> UpdateMenualAttendanceAbsent(List<AttendanceAbsentPaylod> objArr);
        Task<List<GetListAttendanceRowDel>> GetEmployeeListForAttDel(GetEmployeeListPayload obj);
    }
}
