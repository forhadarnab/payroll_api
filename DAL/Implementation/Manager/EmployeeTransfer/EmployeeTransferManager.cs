using BLL.Interfaces.Manager.EmployeeTransfer;
using BLL.Utility;
using BOL.Models;
using System.Data;
using System.Data.SqlClient;

namespace DAL.Implementation.Manager.EmployeeTransfer
{
    public class EmployeeTransferManager : IEmployeeTransferManager
    {
        private readonly Dg_Common _dgCommon;
        private readonly SqlConnection _payCon;
        public EmployeeTransferManager(Dg_Common dg_Common)
        {
            _dgCommon = dg_Common;
            _payCon = new SqlConnection(Getway.Dg_Payroll);
        }

        public async Task<ReturnObject> SaveEmployeeTransfer(EmployeeTransferModel obj)
        {
            var result = new ReturnObject();
            try
            {
                if (obj.transferType== "External" && obj.toCompanyID==0 && obj.newEmployeeID==0 && obj.newProxid=="" && obj.salaryCatID == 0)
                {
                    result.IsSuccess = false;
                    result.Message = "New Company Or New Employee Number Or New Proxid Is Required !!";
                    return result;
                }
                bool isSave = await _dgCommon.saveChangesAsync("Dg_Pay_Employee_Transfer", _payCon, obj);
                if (isSave)
                {
                    result.IsSuccess = true;
                    result.Message = "Employee(" + obj.employeeID + ") Save Successfully !!";
                }
                else
                {
                    result.IsSuccess = false;
                    result.Message = "Employee("+obj.employeeID+ ") Save Failed !!";
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                ex.ToString();
                result.IsSuccess = false;
                result.Message = "Something Went Wrong !!";
            }
            return result;
        }
        public async Task<DataTable> GetEmployeeTransferList(int companyID, int employeeID)
        {
            var data = await _dgCommon.get_InformationDataTableAsync("Dg_Pay_GetEmpTransferInfo "+ companyID + ","+ employeeID, _payCon);
            return data;
        }
        public async Task<DataTable> GetTransferToCompany(int fCompid)
        {
            var data = await _dgCommon.get_InformationDataTableAsync("select com_id,com_name FROM dg_pay_company where com_id !="+ fCompid, _payCon);
            return data;
        }
        public async Task<ReturnObject> ApproveEmployeeTransfer(int transID, string userName)
        {
            var result = new ReturnObject();
            try
            {
                if (transID !=0)
                {
                    bool isUpdate = await _dgCommon.saveChangesAsync("update dg_emp_transfer_info set [Status]=1,confirmedby='"+ userName + "',confirmed_date=getdate() where trans_id=" + transID, _payCon);
                    if (isUpdate)
                    {
                        result.IsSuccess = true;
                        result.Message = "Employee Transfer Approved Successfully !!";
                    }
                    else
                    {
                        result.IsSuccess = false;
                        result.Message = "Employee Transfer Approved Failed !!";
                    }
                }
                else
                {
                    result.IsSuccess = false;
                    result.Message = "Employee Transfer Not Approved !!";
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                ex.ToString();
                result.IsSuccess = false;
                result.Message = "Something Went Wrong !!";
            }
            return result;
        }
        public async Task<ReturnObject> DeleteEmployeeTransfer(int transID)
        {
            var result = new ReturnObject();
            try
            {
                if (transID !=0)
                {
                    bool isDelete = await _dgCommon.saveChangesAsync("delete dg_emp_transfer_info where [Status]=0 and trans_id="+ transID, _payCon);
                    if (isDelete)
                    {
                        result.IsSuccess = true;
                        result.Message = "Employee Transfer Delete Successfully !!";
                    }
                    else
                    {
                        result.IsSuccess = false;
                        result.Message = "Employee Transfer Delete Failed !!";
                    }
                }
                else
                {
                    result.IsSuccess = false;
                    result.Message = "Employee Transfer Not Deleted !!";
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                ex.ToString();
                result.IsSuccess = false;
                result.Message = "Something Went Wrong !!";
            }
            return result;
        }
    }
}