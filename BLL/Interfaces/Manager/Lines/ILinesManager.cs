using BOL.Models;
using EF.Core.Repository.Interface.Manager;

namespace BLL.Interfaces.Manager.Lines
{
    public interface ILinesManager : ICommonManager<Line_DbModel>
    {
        Task<List<DgPayLine>> GetLineByUserName(string userName);
        Task<ReturnObject> AddOrEditLine(LinePayload obj);
        Task<ReturnObject<LineList>> GetAllLineByCompany(int compnayID);
    }
}
