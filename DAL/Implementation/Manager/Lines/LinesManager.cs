using BLL.Interfaces.Manager.Lines;
using BLL.Utility;
using BOL.Models;
using DAL.Data;
using DAL.Implementation.Repository.Lines;
using EF.Core.Repository.Manager;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using System.Data.SqlClient;
using static System.Collections.Specialized.BitVector32;

namespace DAL.Implementation.Manager.Lines
{
    public class LinesManager : CommonManager<Line_DbModel>,ILinesManager
    {
        //private readonly dg_SpecFoContext _context;
        private readonly dg_hrpayrollContext _payContext;
        private readonly Dg_Common _dgCommon;
        private readonly SqlConnection _connection;
        public LinesManager(dg_SpecFoContext context, dg_hrpayrollContext payContext, Dg_Common dgCommon) : base(new LinesRepository(context))
        {
            //_context = context;
            _payContext = payContext;
            _dgCommon = dgCommon;
            _connection = new SqlConnection(Getway.SpecFoCon);
        }

        public async Task<List<DgPayLine>> GetLineByUserName(string userName)
        {
            var lst = new List<DgPayLine>();
            var data = await _dgCommon.get_InformationDataTableAsync("select Line_Code,Line_No,Line_Name_Bangla,CompanyID,CompanyName,DepartmentID,DepartmentName,SectionID,SectionName,BuildingUnitID,BuildingUnitName,FloorID,FloorName from Smt_Line inner join dg_hrpayroll.dbo.dg_pay_companyaccess as compAccs on Smt_Line.CompanyID=compAccs.ca_compid where compAccs.ca_accessuser='" + userName + "'", _connection);
            for (int i = 0; i < data.Rows.Count; i++)
            {
                lst.Add(new DgPayLine
                {
                    LineCode = Convert.ToInt32(data.Rows[i]["Line_Code"]),
                    LineNo = data.Rows[i]["Line_No"].ToString(),
                    LineNameBangla = data.Rows[i]["Line_Name_Bangla"].ToString(),
                    CompanyId = Convert.ToInt32(data.Rows[i]["CompanyID"]),
                    CompanyName = data.Rows[i]["CompanyName"].ToString(),
                    DepartmentId = Convert.ToInt32(data.Rows[i]["DepartmentID"]),
                    DepartmentName = data.Rows[i]["DepartmentName"].ToString(),
                    SectionId = Convert.ToInt32(data.Rows[i]["SectionID"]),
                    SectionName = data.Rows[i]["SectionName"].ToString(),
                    BuildingUnitId = Convert.ToInt32(data.Rows[i]["BuildingUnitID"]),
                    BuildingUnitName = data.Rows[i]["BuildingUnitName"].ToString(),
                    FloorId = Convert.ToInt32(data.Rows[i]["FloorID"]),
                    FloorName = data.Rows[i]["FloorName"].ToString(),                   
                });
            }
            return lst;
        }
        public async Task<ReturnObject> AddOrEditLine(LinePayload obj)
        {
            var result = new ReturnObject();
            try
            {
                var dbData = new Line_DbModel();
                if (obj.LineID == 0)
                {
                    var isExists = GetFirstOrDefault(x => x.CompanyID == obj.companyID && x.DepartmentID == obj.departmentID && x.SectionID == obj.sectionID && x.BuildingUnitID == obj.buildingID && x.FloorID == obj.floorID && x.Line_No == obj.lineName && x.Line_Name_Bangla == obj.lineNameBD);
                    if (isExists == null)
                    {
                        dbData = LinePayload.PayloadToLineDb_Obj(obj);
                        bool isSave = await AddAsync(dbData);
                        if (isSave)
                        {
                            result.IsSuccess = true;
                            result.Message = "Line Save Successfully !!";
                        }
                        else
                        {
                            result.IsSuccess = false;
                            result.Message = "Can Not Saved Line !!";
                        }
                    }
                    else
                    {
                        result.IsSuccess = false;
                        result.Message = "Line Already Exists !!";
                    }
                }
                else
                {
                    var dbObj = GetFirstOrDefault(x => x.Line_Code == obj.LineID);
                    dbData = LinePayload.PayloadToLineDb_Obj(obj, dbObj);
                    bool isUpdate = await UpdateAsync(dbData);
                    if (isUpdate)
                    {
                        result.IsSuccess = true;
                        result.Message = "Line Update Successfully !!";
                    }
                    else
                    {
                        result.IsSuccess = false;
                        result.Message = "Can Not Update Line !!";
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                result.IsSuccess = false;
                result.Message = "Something Went Wrong !!";
            }
            return result;
        }
        public async Task<ReturnObject<LineList>> GetAllLineByCompany(int compnayID)
        {
            var result = new ReturnObject<LineList>();
            try
            {
                var lineLs = await GetAsync(x => x.CompanyID == compnayID);
                var userLs = await _payContext.Tbl_User.ToListAsync();
                var mainList = from a in lineLs
                join b in userLs on (!string.IsNullOrEmpty(a?.CreatedBy) ? a?.CreatedBy.Trim() : string.Empty) equals b?.FullName.Trim() into userFullName
                from a1 in userFullName.DefaultIfEmpty()
                join c in userLs on a.UpdateBy equals c.FullName.Trim() into userFullName2
                from a2 in userFullName2.DefaultIfEmpty()
                select (new LineList
                {
                    LineID = a.Line_Code,
                    LineName = a.Line_No,
                    LineNameBangla = !string.IsNullOrEmpty(a?.Line_Name_Bangla) ? a?.Line_Name_Bangla.Trim() : null,
                    CompanyID = a?.CompanyID,
                    CompanyName = a?.CompanyName,
                    DepartmentID = a?.DepartmentID,
                    DepartmentName = a?.DepartmentName,
                    SectionID = a?.SectionID,
                    SectionName = a?.SectionName,
                    BuildingID = a?.BuildingUnitID,
                    BuildingName = a?.BuildingUnitName,
                    FloorID = a?.FloorID,
                    FloorName = a?.FloorName,
                    CreateUser = a1?.UserFullname.Trim(),
                    CreateDate = a?.Createdt,
                    UpdateUser = a2?.UserFullname.Trim(),
                    UpdateDate = a?.Updatedt
                });
                if (mainList.ToList() != null)
                {
                    result.IsSuccess = true;
                    result.Message = "Data Loaded Successfully !!";
                    result.ListData = mainList.ToList();
                }
                else
                {
                    result.IsSuccess = false;
                    result.Message = "Data Loaded Fail !!";
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                result.IsSuccess = false;
                result.Message = "Something Went Wrong !!";
            }
            return result;
        }
    }
}