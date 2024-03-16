using BOL.Models;
using EF.Core.Repository.Interface.Manager;
using System.Data;

namespace BLL.Interfaces.Manager.Leavetransactions
{
    public interface ILeavetransactionsManager : ICommonManager<Leavetransaction_DbModel>
    {
        Task<DataSet> Getleave_info_comdatewise(int CompID, DateTime Sdate, DateTime Edate);
        Task<DataTable> GetEmployeeNo(int compID);
        Task<ReturnObject> SaveLeaveLeavetransaction(LeaveTransactionPayload obj);
        Task<bool> deleteLevTrans(int id);
    }
}
