using BOL.Models;
using Microsoft.AspNetCore.Mvc;

namespace BLL.Interfaces.Manager.UploadAttendances
{
    public interface IUploadAttendancesManager
    {
        Task<List<MyEntity>> WriteFile([FromForm] UploadFile model);
        Task<List<MyEntity>> WriteFile1([FromForm] UploadFile model);
        Task<List<object>> ReadAttnTextFile([FromForm] UploadFile model);
    }
}
