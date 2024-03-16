using BLL.Interfaces.Manager.Leavetransactions;
using BLL.Utility;
using BOL.Models;
using DAL.Data;
using DAL.Implementation.Repository.Leavetransactions;
using EF.Core.Repository.Manager;
using System.Data;
using System.Data.SqlClient;

namespace DAL.Implementation.Manager.Leavetransactions
{
    public class LeavetransactionsManager : CommonManager<Leavetransaction_DbModel>,ILeavetransactionsManager
    {
        private readonly dg_hrpayrollContext _context;
        private readonly Dg_Common _dgCommon;
        private readonly SqlConnection _sqlConnection;
        public LeavetransactionsManager(dg_hrpayrollContext context, Dg_Common dgCommon) : base(new LeavetransactionsRepository(context))
        {
            _context = context;
            _dgCommon = dgCommon;
            _sqlConnection = new SqlConnection(Getway.Dg_Payroll);
        }

        public async Task<DataSet> Getleave_info_comdatewise(int CompID, DateTime Sdate, DateTime Edate)
        {
            var data = await _dgCommon.get_InformationDtasetAsync("D_leave_info_comdatewise "+ CompID + ",'"+ Sdate + "','"+ Edate + "'", _sqlConnection);
            return data;
        }
        public async Task<DataTable> GetEmployeeNo(int compID)
        {
            var data = await _dgCommon.get_InformationDataTableAsync("select distinct emp_serial,emp_no from dg_pay_leaveInfor inner join dg_pay_Employee on lev_emp_serial=emp_serial where lev_compid="+ compID + " and lev_year=YEAR(Getdate()) and oi_active=1", _sqlConnection);
            return data;
        }
        //public async Task<bool> SaveLeaveLeavetransaction(LeaveTransactionPayload obj)
        //{
        //    Leavetransaction_DbModel table1 = null;
        //    LevTransFdateTdate_DbModel table2 = null;
        //    using (var transaction = _context.Database.BeginTransaction())
        //    {
        //        bool flag = false;
        //        try
        //        {
        //            table1 = new Leavetransaction_DbModel
        //            {
        //                ltr_compid = obj.companyId,
        //                ltr_emp_serial = obj.empSerial,
        //                ltr_date = obj.fromDate,
        //                ltr_casual = obj.casual,
        //                ltr_anual = obj.anual,
        //                ltr_medical = obj.medical,
        //                ltrNp = obj.notPayable,
        //                ltr_Csl=obj.comlev,
        //                ltr_user = obj.user,
        //                ltr_udate = DateTime.Now
        //            };
        //            _context.dg_pay_leavetransaction.Add(table1);
        //            await _context.SaveChangesAsync();
        //            table2 = new LevTransFdateTdate_DbModel
        //            {
        //                lev_compid = obj.companyId,
        //                lev_emp_serial = obj.empSerial,
        //                lev_from_date = obj.fromDate,
        //                lev_to_date = obj.toDate,
        //                lev_type = obj.levType,
        //                leave_days = obj.leaveDays,
        //                lev_reasion = obj.levReasion,
        //                lev_leavetime_location = obj.levLocation,
        //                lev_user = obj.user,
        //                lev_add_date = DateTime.Now,
        //            };
        //            _context.dg_pay_leave_info_forfromdatetodate.Add(table2);
        //            await _context.SaveChangesAsync();
        //            transaction.Commit();
        //            flag = true;
        //        }
        //        catch (Exception ex)
        //        {
        //            ex.ToString();
        //            await transaction.RollbackAsync();
        //            flag = false;
        //        }
        //        finally
        //        {
        //            await transaction.DisposeAsync();
        //        }
        //        return flag;
        //    }
        //}

        public async Task<ReturnObject> SaveLeaveLeavetransaction(LeaveTransactionPayload obj)
        {
            var result = new ReturnObject();
            try
            {
                var isAttn = _dgCommon.get_InformationDataTable("select top 1 at_emp_serial from dg_pay_attendance where at_date between '" + obj.fromDate + "' and '" + obj.toDate + "' and at_emp_serial=" + obj.empSerial + " and (at_intime>0 or at_outtime>0)", _sqlConnection);
                if (isAttn.Rows.Count > 0)
                {
                    result.Message = "Employee In/Out Time Already Exists This Date Range !!";
                    return result;
                }
                var queryArr = new string[] { "Dg_Pay_SaveLeaveFromDateToDate", "Dg_Pay_SaveLeaveTransaction" };
                bool isSave = await _dgCommon.saveChangesAsync(queryArr, _sqlConnection, obj);
                if (isSave)
                {
                    result.IsSuccess = true;
                    result.Message = "Save Successfully !!";
                }
                else
                {
                    result.Message = "Save Fail";
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                result.Message = "Something Went Wrong !!";
            }
            return result;
        }
        public async Task<bool> deleteLevTrans(int id)
        {
            bool delete = false;
            await _sqlConnection.OpenAsync();
            try
            {
                SqlCommand cmd = new SqlCommand("delete dg_pay_leave_info_forfromdatetodate where sl=" + id, _sqlConnection);
                await cmd.ExecuteNonQueryAsync();
                delete = true; 
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                await _sqlConnection.CloseAsync();
            }
            return delete;
        }        
    }
}
