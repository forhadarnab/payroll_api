using BLL.Interfaces;
using BLL.Utility;
using BOL.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace Payroll_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IGlobalMaster _globalMaster;
        private readonly Dg_Common _dgCommon;
        public ReportController(IGlobalMaster globalMaster, Dg_Common dgCommon)
        {
            _globalMaster = globalMaster;
            _dgCommon = dgCommon;
        }

        #region"Employee"
        [HttpGet("InActiveList")]
        public IActionResult EmpINActiveList(string reportType)
        {
            try
            {
                var reportBytes = _globalMaster.reportManager.Export_Report_Employee_Details_InActive(reportType);
                if (reportBytes == null)
                {
                    return NotFound();
                }
                return File(reportBytes, MediaTypeNames.Application.Octet, _dgCommon.GetContentType(reportType));
            }
            catch (Exception ex)
            {
                ex.ToString();
                return StatusCode(500, "Internal Server Error, Please Try Again Later!");
            }
        }
        [HttpGet("ActiveList")]
        public IActionResult EmpActiveList(string reportType)
        {
            try
            {
                var reportBytes = _globalMaster.reportManager.Export_Report_Employee_Details_Active(reportType);
                if (reportBytes == null)
                {
                    return NotFound();
                }
                return File(reportBytes, MediaTypeNames.Application.Octet, _dgCommon.GetContentType(reportType));
            }
            catch (Exception ex)
            {
                ex.ToString();
                return StatusCode(500, "Internal Server Error, Please Try Again Later!");
            }
        }
        [HttpGet("Dg_ActiveEmployeeImage")]
        public IActionResult Dg_ActiveEmployeesWithImage(string reportType)
        {
            try
            {
                var reportBytes = _globalMaster.reportManager.Dg_ActiveEmployeesWithImage(reportType);
                if (reportBytes == null)
                {
                    return NotFound();
                }
                return File(reportBytes, MediaTypeNames.Application.Octet, _dgCommon.GetContentType(reportType));
            }
            catch (Exception ex)
            {
                ex.ToString();
                return StatusCode(500, "Internal Server Error, Please Try Again Later!");
            }
        }
        [HttpGet("Dg_JoinDateWiseEmployeeDetails")]
        public IActionResult JoinDateWiseEmployee_Details_InActive(string reportType)
        {
            try
            {
                var reportBytes = _globalMaster.reportManager.Dg_JoinDateWise_Employee_Details_Active(reportType);
                if (reportBytes == null)
                {
                    return NotFound();
                }
                return File(reportBytes, MediaTypeNames.Application.Octet, _dgCommon.GetContentType(reportType));
            }
            catch (Exception ex)
            {
                ex.ToString();
                return StatusCode(500, "Internal Server Error, Please Try Again Later!");
            }
        }
        [HttpGet("Dg_EmployeeIdCard")]
        public IActionResult Dg_EmployeeIdCard(string reportType)
        {
            try
            {
                var reportBytes = _globalMaster.reportManager.Dg_CreateReportFileIDCARD(reportType);
                if (reportBytes == null)
                {
                    return NotFound();
                }
                return File(reportBytes, MediaTypeNames.Application.Octet, _dgCommon.GetContentType(reportType));
            }
            catch (Exception ex)
            {
                ex.ToString();
                return StatusCode(500, "Internal Server Error, Please Try Again Later!");
            }
        }
        [HttpGet("Dg_EmployeeIdCard_Bangla")]
        public IActionResult Dg_EmployeeIdCard_Bangla(string reportType)
        {
            try
            {
                var reportBytes = _globalMaster.reportManager.Dg_CreateReportFileIDCARD_BN(reportType);
                if (reportBytes == null)
                {
                    return NotFound();
                }
                return File(reportBytes, MediaTypeNames.Application.Octet, _dgCommon.GetContentType(reportType));
            }
            catch (Exception ex)
            {
                ex.ToString();
                return StatusCode(500, "Internal Server Error, Please Try Again Later!");
            }
        }
        [HttpGet("Dg_EmpAgeCertificate")]
        public IActionResult Dg_EmpAgeCertificate(string reportType)
        {
            try
            {
                var reportBytes = _globalMaster.reportManager.Dg_EmpAgeCertificate(reportType);
                if (reportBytes == null)
                {
                    return NotFound();
                }
                return File(reportBytes, MediaTypeNames.Application.Octet, _dgCommon.GetContentType(reportType));
            }
            catch (Exception ex)
            {
                ex.ToString();
                return StatusCode(500, "Internal Server Error, Please Try Again Later!");
            }
        }
        [HttpGet("Dg_IncrementLetter")]
        public IActionResult Dg_IncrementLetter(string reportType)
        {
            try
            {
                var reportBytes = _globalMaster.reportManager.Dg_IncrementLetter(reportType);
                if (reportBytes == null)
                {
                    return NotFound();
                }
                return File(reportBytes, MediaTypeNames.Application.Octet, _dgCommon.GetContentType(reportType));
            }
            catch (Exception ex)
            {
                ex.ToString();
                return StatusCode(500, "Internal Server Error, Please Try Again Later!");
            }
        }
        [HttpGet("Dg_PromotionWithIncrementLetter")]
        public IActionResult Dg_PromotionWithIncrementLetter(string reportType)
        {
            try
            {
                var reportBytes = _globalMaster.reportManager.Dg_PromotionWithIncrementLetter(reportType);
                if (reportBytes == null)
                {
                    return NotFound();
                }
                return File(reportBytes, MediaTypeNames.Application.Octet, _dgCommon.GetContentType(reportType));
            }
            catch (Exception ex)
            {
                ex.ToString();
                return StatusCode(500, "Internal Server Error, Please Try Again Later!");
            }
        }
        [HttpGet("Dg_IncrementLetter_Bangla")]
        public IActionResult Dg_IncrementLetter_Bangla(string reportType)
        {
            try
            {
                var reportBytes = _globalMaster.reportManager.Dg_IncrementLetter_Bangla(reportType);
                if (reportBytes == null)
                {
                    return NotFound();
                }
                return File(reportBytes, MediaTypeNames.Application.Octet, _dgCommon.GetContentType(reportType));
            }
            catch (Exception ex)
            {
                ex.ToString();
                return StatusCode(500, "Internal Server Error, Please Try Again Later!");
            }
        }
        [HttpGet("Dg_PromotionWithIncrementLetter_Bangla")]
        public IActionResult Dg_PromotionWithIncrementLetter_Bangla(string reportType)
        {
            try
            {
                var reportBytes = _globalMaster.reportManager.Dg_PromotionWithIncrementLetter_Bangla(reportType);
                if (reportBytes == null)
                {
                    return NotFound();
                }
                return File(reportBytes, MediaTypeNames.Application.Octet, _dgCommon.GetContentType(reportType));
            }
            catch (Exception ex)
            {
                ex.ToString();
                return StatusCode(500, "Internal Server Error, Please Try Again Later!");
            }
        }
        [HttpGet("Dg_AppointmentLetter")]
        public IActionResult Dg_AppointmentLetter(string reportType)
        {
            try
            {
                var reportBytes = _globalMaster.reportManager.Dg_AppointmentLetter(reportType);
                if (reportBytes == null)
                {
                    return NotFound();
                }
                return File(reportBytes, MediaTypeNames.Application.Octet, _dgCommon.GetContentType(reportType));
            }
            catch (Exception ex)
            {
                ex.ToString();
                return StatusCode(500, "Internal Server Error, Please Try Again Later!");
            }
        }
        [HttpGet("Dg_AppointmentLetter_IFL")]
        public IActionResult Dg_AppointmentLetter_IFL(string reportType)
        {
            try
            {
                var reportBytes = _globalMaster.reportManager.Dg_AppointmentLetter_IFL(reportType);
                if (reportBytes == null)
                {
                    return NotFound();
                }
                return File(reportBytes, MediaTypeNames.Application.Octet, _dgCommon.GetContentType(reportType));
            }
            catch (Exception ex)
            {
                ex.ToString();
                return StatusCode(500, "Internal Server Error, Please Try Again Later!");
            }
        }
        [HttpGet("Dg_AppointmentLetter_StaffBangla")]
        public IActionResult Dg_AppointmentLetter_StaffBangla(string reportType)
        {
            try
            {
                var reportBytes = _globalMaster.reportManager.Dg_AppointmentLetter_StaffBangla(reportType);
                if (reportBytes == null)
                {
                    return NotFound();
                }
                return File(reportBytes, MediaTypeNames.Application.Octet, _dgCommon.GetContentType(reportType));
            }
            catch (Exception ex)
            {
                ex.ToString();
                return StatusCode(500, "Internal Server Error, Please Try Again Later!");
            }
        }
        [HttpGet("Dg_AppointmentLetter_EN")]
        public IActionResult Dg_AppointmentLetter_EN(string reportType)
        {
            try
            {
                var reportBytes = _globalMaster.reportManager.Dg_AppointmentLetter_EN(reportType);
                if (reportBytes == null)
                {
                    return NotFound();
                }
                return File(reportBytes, MediaTypeNames.Application.Octet, _dgCommon.GetContentType(reportType));
            }
            catch (Exception ex)
            {
                ex.ToString();
                return StatusCode(500, "Internal Server Error, Please Try Again Later!");
            }
        }
        [HttpGet("Dg_NoticeLetter_1st")]
        public IActionResult Dg_NoticeLetter_1st(string reportType)
        {
            try
            {
                var reportBytes = _globalMaster.reportManager.Dg_NoticeLetter_1st(reportType);
                if (reportBytes == null)
                {
                    return NotFound();
                }
                return File(reportBytes, MediaTypeNames.Application.Octet, _dgCommon.GetContentType(reportType));
            }
            catch (Exception ex)
            {
                ex.ToString();
                return StatusCode(500, "Internal Server Error, Please Try Again Later!");
            }
        }
        [HttpGet("Dg_NoticeLetter_2nd")]
        public IActionResult Dg_NoticeLetter_2nd(string reportType)
        {
            try
            {
                var reportBytes = _globalMaster.reportManager.Dg_NoticeLetter_2nd(reportType);
                if (reportBytes == null)
                {
                    return NotFound();
                }
                return File(reportBytes, MediaTypeNames.Application.Octet, _dgCommon.GetContentType(reportType));
            }
            catch (Exception ex)
            {
                ex.ToString();
                return StatusCode(500, "Internal Server Error, Please Try Again Later!");
            }
        }
        [HttpGet("Dg_MaleFemaleDetails")]
        public IActionResult Dg_MaleFemaleDetails(string reportType)
        {
            try
            {
                var reportBytes = _globalMaster.reportManager.Dg_MaleFemaleDetails(reportType);
                if (reportBytes == null)
                {
                    return NotFound();
                }
                return File(reportBytes, MediaTypeNames.Application.Octet, _dgCommon.GetContentType(reportType));
            }
            catch (Exception ex)
            {
                ex.ToString();
                return StatusCode(500, "Internal Server Error, Please Try Again Later!");
            }
        }
        [HttpGet("Dg_MaleFemaleSummary")]
        public IActionResult Dg_MaleFemaleSummary(string reportType)
        {
            try
            {
                var reportBytes = _globalMaster.reportManager.Dg_MaleFemaleSummary(reportType);
                if (reportBytes == null)
                {
                    return NotFound();
                }
                return File(reportBytes, MediaTypeNames.Application.Octet, _dgCommon.GetContentType(reportType));
            }
            catch (Exception ex)
            {
                ex.ToString();
                return StatusCode(500, "Internal Server Error, Please Try Again Later!");
            }
        }
        [HttpGet("Dg_EmployeeDetailsRegligion")]
        public IActionResult Dg_EmployeeDetailsRegligion(string reportType)
        {
            try
            {
                var reportBytes = _globalMaster.reportManager.Dg_EmployeeDetailsRegligion(reportType);
                if (reportBytes == null)
                {
                    return NotFound();
                }
                return File(reportBytes, MediaTypeNames.Application.Octet, _dgCommon.GetContentType(reportType));
            }
            catch (Exception ex)
            {
                ex.ToString();
                return StatusCode(500, "Internal Server Error, Please Try Again Later!");
            }
        }
        [HttpGet("Dg_ProximityCardChecklist")]
        public IActionResult Dg_ProximityCardChecklist(string reportType)
        {
            try
            {
                var reportBytes = _globalMaster.reportManager.Dg_ProximityCardChecklist(reportType);
                if (reportBytes == null)
                {
                    return NotFound();
                }
                return File(reportBytes, MediaTypeNames.Application.Octet, _dgCommon.GetContentType(reportType));
            }
            catch (Exception ex)
            {
                ex.ToString();
                return StatusCode(500, "Internal Server Error, Please Try Again Later!");
            }
        }
        [HttpGet("Dg_EmployeewiseIncrementDetails")]
        public IActionResult Dg_EmployeewiseIncrementDetails(string reportType)
        {
            try
            {
                var reportBytes = _globalMaster.reportManager.Dg_EmployeewiseIncrementDetails(reportType);
                if (reportBytes == null)
                {
                    return NotFound();
                }
                return File(reportBytes, MediaTypeNames.Application.Octet, _dgCommon.GetContentType(reportType));
            }
            catch (Exception ex)
            {
                ex.ToString();
                return StatusCode(500, "Internal Server Error, Please Try Again Later!");
            }
        }
        [HttpGet("Dg_CutoffDatewiseIncrementPendingList")]
        public IActionResult Dg_CutoffDatewiseIncrementPendingList(string reportType)
        {
            try
            {
                var reportBytes = _globalMaster.reportManager.Dg_CutoffDatewiseIncrementPendingList(reportType);
                if (reportBytes == null)
                {
                    return NotFound();
                }
                return File(reportBytes, MediaTypeNames.Application.Octet, _dgCommon.GetContentType(reportType));
            }
            catch (Exception ex)
            {
                ex.ToString();
                return StatusCode(500, "Internal Server Error, Please Try Again Later!");
            }
        }
        [HttpGet]
        [Route("Dg_CutoffDatewiseIncrementApprovedList")]
        public IActionResult Dg_CutoffDatewiseIncrementApprovedList(string reportType)
        {
            try
            {
                var reportBytes = _globalMaster.reportManager.Dg_CutoffDatewiseIncrementApprovedList(reportType);
                if (reportBytes == null)
                {
                    return NotFound();
                }
                return File(reportBytes, MediaTypeNames.Application.Octet, _dgCommon.GetContentType(reportType));
            }
            catch (Exception ex)
            {
                ex.ToString();
                return StatusCode(500, "Internal Server Error, Please Try Again Later!");
            }
        }
        [HttpGet("Dg_EmployeeWiseDetailedInformation")]
        public IActionResult Dg_EmployeeWiseDetailedInformation(string reportType)
        {
            try
            {
                var reportBytes = _globalMaster.reportManager.Dg_EmployeeWiseDetailedInformation(reportType);
                if (reportBytes == null)
                {
                    return NotFound();
                }
                return File(reportBytes, MediaTypeNames.Application.Octet, _dgCommon.GetContentType(reportType));
            }
            catch (Exception ex)
            {
                ex.ToString();
                return StatusCode(500, "Internal Server Error, Please Try Again Later!");
            }
        }
        [HttpGet("Dg_EmpTiffinBillStatus")]
        public IActionResult Dg_EmpTiffinBillStatus(string reportType)
        {
            try
            {
                var reportBytes = _globalMaster.reportManager.Dg_Emp_TiffinBillStatus(reportType);
                if (reportBytes == null)
                {
                    return NotFound();
                }
                return File(reportBytes, MediaTypeNames.Application.Octet, _dgCommon.GetContentType(reportType));
            }
            catch (Exception ex)
            {
                ex.ToString();
                return StatusCode(500, "Internal Server Error, Please Try Again Later!");
            }
        }
        [HttpGet("Dg_EmpNightBillStatus")]
        public IActionResult Dg_Emp_NightBillStatus(string reportType)
        {
            try
            {
                var reportBytes = _globalMaster.reportManager.Dg_Emp_NightBillStatus(reportType);
                if (reportBytes == null)
                {
                    return NotFound();
                }
                return File(reportBytes, MediaTypeNames.Application.Octet, _dgCommon.GetContentType(reportType));
            }
            catch (Exception ex)
            {
                ex.ToString();
                return StatusCode(500, "Internal Server Error, Please Try Again Later!");
            }
        }
        [HttpGet("shift_change_history")]
        public IActionResult EMPshift_change_history(string reportType)
        {
            try
            {
                var reportBytes = _globalMaster.reportManager.Export_Report_Employee_shiftchange_history(reportType);
                if (reportBytes == null)
                {
                    return NotFound();
                }
                return File(reportBytes, MediaTypeNames.Application.Octet, _dgCommon.GetContentType(reportType));
            }
            catch (Exception ex)
            {
                ex.ToString();
                return StatusCode(500, "Internal Server Error, Please Try Again Later!");
            }
        }
        [HttpPost("shift_change_history_new")]
        public IActionResult shift_change_history_new([Bind(nameof(ReportParameterModel))] ReportParameterModel obj)
        {
            try
            {
                var reportBytes = _globalMaster.reportManager.Export_Report_Employee_shiftchange_history_New(obj);
                if (reportBytes == null)
                {
                    return NotFound();
                }
                return File(reportBytes, MediaTypeNames.Application.Octet, _dgCommon.GetContentType(obj.reportType));
            }
            catch (Exception ex)
            {
                ex.ToString();
                return StatusCode(500, "Internal Server Error, Please Try Again Later!");
            }
        }
        [HttpGet("EmployeeNominationForm")]
        public IActionResult EmployeeNominationForm(string reportType)
        {
            try
            {
                var reportBytes = _globalMaster.reportManager.EmployeeNominationForm(reportType);
                if (reportBytes == null)
                {
                    return NotFound();
                }
                return File(reportBytes, MediaTypeNames.Application.Octet, _dgCommon.GetContentType(reportType));
            }
            catch (Exception ex)
            {
                ex.ToString();
                return StatusCode(500, "Internal Server Error, Please Try Again Later!");
            }
        }
        [HttpGet("EmployeeJoiningLetter")]
        public IActionResult EmployeeJoiningLetter(string reportType)
        {
            try
            {
                var reportBytes = _globalMaster.reportManager.EmployeeJoiningLetter(reportType);
                if (reportBytes == null)
                {
                    return NotFound();
                }
                return File(reportBytes, MediaTypeNames.Application.Octet, _dgCommon.GetContentType(reportType));
            }
            catch (Exception ex)
            {
                ex.ToString();
                return StatusCode(500, "Internal Server Error, Please Try Again Later!");
            }
        }
        [HttpGet("EmployeeJobApplication")]
        public IActionResult EmployeeJobApplication(string reportType)
        {
            try
            {
                var reportBytes = _globalMaster.reportManager.EmployeeJobApplication(reportType);
                if (reportBytes == null)
                {
                    return NotFound();
                }
                return File(reportBytes, MediaTypeNames.Application.Octet, _dgCommon.GetContentType(reportType));
            }
            catch (Exception ex)
            {
                ex.ToString();
                return StatusCode(500, "Internal Server Error, Please Try Again Later!");
            }
        }
        [HttpGet("EmployeeDetailsExcel")]
        public IActionResult EmployeeDetailsExcel(string reportType)
        {
            try
            {
                var reportBytes = _globalMaster.reportManager.EmployeeDetailsExcel(reportType);
                if (reportBytes == null)
                {
                    return NotFound();
                }
                return File(reportBytes, MediaTypeNames.Application.Octet, _dgCommon.GetContentType(reportType));
            }
            catch (Exception ex)
            {
                ex.ToString();
                return StatusCode(500, "Internal Server Error, Please Try Again Later!");
            }
        }
        [HttpGet("EmployeeBackgroundCheckBN")]
        public IActionResult EmployeeBackgroundCheckBN(string reportType)
        {
            try
            {
                var reportBytes = _globalMaster.reportManager.EmployeeBackgroundCheck(reportType);
                if (reportBytes == null)
                {
                    return NotFound();
                }
                return File(reportBytes, MediaTypeNames.Application.Octet, _dgCommon.GetContentType(reportType));
            }
            catch (Exception ex)
            {
                ex.ToString();
                return StatusCode(500, "Internal Server Error, Please Try Again Later!");
            }
        }
        [HttpGet("EmployeeIvelatution_From")]
        public IActionResult EmployeeIvelatution_From(string reportType)
        {
            try
            {
                var reportBytes = _globalMaster.reportManager.EmployeeIvelatution_From(reportType);
                if (reportBytes == null)
                {
                    return NotFound();
                }
                return File(reportBytes, MediaTypeNames.Application.Octet, _dgCommon.GetContentType(reportType));
            }
            catch (Exception ex)
            {
                ex.ToString();
                return StatusCode(500, "Internal Server Error, Please Try Again Later!");
            }
        }

        //Audit
        [HttpGet("excel_Audit_Attendance_preriodical")]
        public IActionResult Report_preriodical_present_absent_leave_Weekly_holiday_special_holiday(string reportType)
        {
            try
            {
                var reportBytes = _globalMaster.reportManager.Report_preriodical_present_absent_leave_Weekly_holiday_special_holiday(reportType);
                if (reportBytes == null)
                {
                    return NotFound();
                }
                return File(reportBytes, MediaTypeNames.Application.Octet, _dgCommon.GetContentType(reportType));
            }
            catch (Exception ex)
            {
                ex.ToString();
                return StatusCode(500, "Internal Server Error, Please Try Again Later!");
            }
        }
        [HttpGet("Report_salary_Info_Audit_Excel")]
        public IActionResult Report_Salary_info_Audit_Excel(string reportType)
        {
            try
            {
                var reportBytes = _globalMaster.reportManager.Report_Salary_info_Audit_Excel(reportType);
                if (reportBytes == null)
                {
                    return NotFound();
                }
                return File(reportBytes, MediaTypeNames.Application.Octet, _dgCommon.GetContentType(reportType));
            }
            catch (Exception ex)
            {
                ex.ToString();
                return StatusCode(500, "Internal Server Error, Please Try Again Later!");
            }
        }

        [HttpGet("Report_Joindate_wise_Emp_info_Audit_Excel")]
        public IActionResult Report_Joindate_wise_info_Audit_Excel(string reportType)
        {
            try
            {
                var reportBytes = _globalMaster.reportManager.Report_Joindate_wise_info_Audit_Excel(reportType);
                if (reportBytes == null)
                {
                    return NotFound();
                }
                return File(reportBytes, MediaTypeNames.Application.Octet, _dgCommon.GetContentType(reportType));
            }
            catch (Exception ex)
            {
                ex.ToString();
                return StatusCode(500, "Internal Server Error, Please Try Again Later!");
            }
        }
        [HttpGet("Report_manual_attendsance_Audit_Excel")]
        public IActionResult Report_manual_attendsance_Audit_Excel(string reportType)
        {
            try
            {
                var reportBytes = _globalMaster.reportManager.Report_manual_attendsance_Audit_Excel(reportType);
                if (reportBytes == null)
                {
                    return NotFound();
                }
                return File(reportBytes, MediaTypeNames.Application.Octet, _dgCommon.GetContentType(reportType));
            }
            catch (Exception ex)
            {
                ex.ToString();
                return StatusCode(500, "Internal Server Error, Please Try Again Later!");
            }
        }
        [HttpGet("Report_TiffinBill_Audit_Excel")]
        public IActionResult Report_TiffinBill_Audit_Excel(string reportType)
        {
            try
            {
                var reportBytes = _globalMaster.reportManager.Report_TiffinBill_Audit_Excel(reportType);
                if (reportBytes == null)
                {
                    return NotFound();
                }
                return File(reportBytes, MediaTypeNames.Application.Octet, _dgCommon.GetContentType(reportType));
            }
            catch (Exception ex)
            {
                ex.ToString();
                return StatusCode(500, "Internal Server Error, Please Try Again Later!");
            }
        }
        [HttpGet("Report_NightBill_Audit_Excel")]
        public IActionResult Report_Night_Audit_Excel(string reportType)
        {
            try
            {
                var reportBytes = _globalMaster.reportManager.Report_Night_Audit_Excel(reportType);
                if (reportBytes == null)
                {
                    return NotFound();
                }
                return File(reportBytes, MediaTypeNames.Application.Octet, _dgCommon.GetContentType(reportType));
            }
            catch (Exception ex)
            {
                ex.ToString();
                return StatusCode(500, "Internal Server Error, Please Try Again Later!");
            }
        }

        [HttpGet("Report_LeaveTransction_Audit_Excel")]
        public IActionResult Report_Leave_transction_Audit_Excel(string reportType)
        {
            try
            {
                var reportBytes = _globalMaster.reportManager.Report_Leave_transction_Audit_Excel(reportType);
                if (reportBytes == null)
                {
                    return NotFound();
                }
                return File(reportBytes, MediaTypeNames.Application.Octet, _dgCommon.GetContentType(reportType));
            }
            catch (Exception ex)
            {
                ex.ToString();
                return StatusCode(500, "Internal Server Error, Please Try Again Later!");
            }
        }
        [HttpGet("EmployeeDetailsExcel_audit")]
        public IActionResult EmployeeDetailsExcel_audit(string reportType)
        {
            try
            {
                var reportBytes = _globalMaster.reportManager.EmployeeDetailsExcel(reportType);
                if (reportBytes == null)
                {
                    return NotFound();
                }
                return File(reportBytes, MediaTypeNames.Application.Octet, _dgCommon.GetContentType(reportType));
            }
            catch (Exception ex)
            {
                ex.ToString();
                return StatusCode(500, "Internal Server Error, Please Try Again Later!");
            }
        }
        [HttpGet("Separation_Employee_Audit")]
        public IActionResult Report_In_Activeemp_Date_wise_Audit_Excel(string reportType)
        {
            try
            {
                var reportBytes = _globalMaster.reportManager.Report_In_Activeemp_Date_wise_Audit_Excel(reportType);
                if (reportBytes == null)
                {
                    return NotFound();
                }
                return File(reportBytes, MediaTypeNames.Application.Octet, _dgCommon.GetContentType(reportType));
            }
            catch (Exception ex)
            {
                ex.ToString();
                return StatusCode(500, "Internal Server Error, Please Try Again Later!");
            }
        }
        //Audit
        #endregion

        #region"Leave"
        [HttpGet("Dg_LeaveBalances")]
        public IActionResult Dg_LeaveBalances(string reportType)
        {
            try
            {
                var reportBytes = _globalMaster.reportManager.Dg_LeaveBalances(reportType);
                if (reportBytes == null)
                {
                    return NotFound();
                }
                return File(reportBytes, MediaTypeNames.Application.Octet, _dgCommon.GetContentType(reportType));
            }
            catch (Exception ex)
            {
                ex.ToString();
                return StatusCode(500, "Internal Server Error, Please Try Again Later!");
            }
        }
        [HttpGet("Dg_LeaveTransactions")]
        public IActionResult Dg_LeaveTransactions(string reportType)
        {
            try
            {
                var reportBytes = _globalMaster.reportManager.Dg_LeaveTransactions(reportType);
                if (reportBytes == null)
                {
                    return NotFound();
                }
                return File(reportBytes, MediaTypeNames.Application.Octet, _dgCommon.GetContentType(reportType));
            }
            catch (Exception ex)
            {
                ex.ToString();
                return StatusCode(500, "Internal Server Error, Please Try Again Later!");
            }
        }
        [HttpGet("Dg_IndividualLeaveStatement")]
        public IActionResult Dg_IndividualLeaveStatement(string reportType)
        {
            try
            {
                var reportBytes = _globalMaster.reportManager.Dg_IndividualLeaveStatement(reportType);
                if (reportBytes == null)
                {
                    return NotFound();
                }
                return File(reportBytes, MediaTypeNames.Application.Octet, _dgCommon.GetContentType(reportType));
            }
            catch (Exception ex)
            {
                ex.ToString();
                return StatusCode(500, "Internal Server Error, Please Try Again Later!");
            }
        }
        [HttpGet("Dg_MaternityLeaveList")]
        public IActionResult Dg_MaternityLeaveList(string reportType)
        {
            try
            {
                var reportBytes = _globalMaster.reportManager.Dg_MaternityLeaveList(reportType);
                if (reportBytes == null)
                {
                    return NotFound();
                }
                return File(reportBytes, MediaTypeNames.Application.Octet, _dgCommon.GetContentType(reportType));
            }
            catch (Exception ex)
            {
                ex.ToString();
                return StatusCode(500, "Internal Server Error, Please Try Again Later!");
            }
        }
        [HttpGet("Dg_MaternityLeavePayment")]
        public IActionResult Dg_MaternityLeavePayment(string reportType)
        {
            try
            {
                var reportBytes = _globalMaster.reportManager.Dg_MaternityLeavePayment(reportType);
                if (reportBytes == null)
                {
                    return NotFound();
                }
                return File(reportBytes, MediaTypeNames.Application.Octet, _dgCommon.GetContentType(reportType));
            }
            catch (Exception ex)
            {
                ex.ToString();
                return StatusCode(500, "Internal Server Error, Please Try Again Later!");
            }
        }
        [HttpGet("Dg_LeaveForm")]
        public IActionResult Dg_LeaveForm(string reportType)
        {
            try
            {
                var reportBytes = _globalMaster.reportManager.Dg_LeaveForm(reportType);
                if (reportBytes == null)
                {
                    return NotFound();
                }
                return File(reportBytes, MediaTypeNames.Application.Octet, _dgCommon.GetContentType(reportType));
            }
            catch (Exception ex)
            {
                ex.ToString();
                return StatusCode(500, "Internal Server Error, Please Try Again Later!");
            }
        }
        #endregion

        #region"Attendance"
        [HttpGet("Present")]
        public IActionResult Present(string reportType)
        {
            try
            {
                var reportBytes = _globalMaster.reportManager.CreateReportFile_Attendance_Present(reportType);
                if (reportBytes == null)
                {
                    return NotFound();
                }
                return File(reportBytes, MediaTypeNames.Application.Octet, _dgCommon.GetContentType(reportType));
            }
            catch (Exception ex)
            {
                ex.ToString();
                return StatusCode(500, "Internal Server Error, Please Try Again Later!");
            }
        }
        [HttpGet("Dg_Att_PresentWithImages")]
        public IActionResult Dg_Att_PresentWithImages(string reportType)
        {
            try
            {
                var reportBytes = _globalMaster.reportManager.Dg_Att_PresentWithImages(reportType);
                if (reportBytes == null)
                {
                    return NotFound();
                }
                return File(reportBytes, MediaTypeNames.Application.Octet, _dgCommon.GetContentType(reportType));
            }
            catch (Exception ex)
            {
                ex.ToString();
                return StatusCode(500, "Internal Server Error, Please Try Again Later!");
            }
        }
        [HttpGet("Absent")]
        public IActionResult Absent(string reportType)
        {
            try
            {
                var reportBytes = _globalMaster.reportManager.CreateReportFile_Attendance_Absent(reportType);
                if (reportBytes == null)
                {
                    return NotFound();
                }
                return File(reportBytes, MediaTypeNames.Application.Octet, _dgCommon.GetContentType(reportType));
            }
            catch (Exception ex)
            {
                ex.ToString();
                return StatusCode(500, "Internal Server Error, Please Try Again Later!");
            }
        }
        [HttpGet("Dg_Att_AbsentWithImages")]
        public IActionResult Dg_Att_AbsentWithImages(string reportType)
        {
            try
            {
                var reportBytes = _globalMaster.reportManager.Dg_Att_AbsentWithImages(reportType);
                if (reportBytes == null)
                {
                    return NotFound();
                }
                return File(reportBytes, MediaTypeNames.Application.Octet, _dgCommon.GetContentType(reportType));
            }
            catch (Exception ex)
            {
                ex.ToString();
                return StatusCode(500, "Internal Server Error, Please Try Again Later!");
            }
        }
        [HttpGet("Late")]
        public IActionResult Late(string reportType)
        {
            try
            {
                var reportBytes = _globalMaster.reportManager.CreateReportFile_Attendance_Late(reportType);
                if (reportBytes == null)
                {
                    return NotFound();
                }
                return File(reportBytes, MediaTypeNames.Application.Octet, _dgCommon.GetContentType(reportType));
            }
            catch (Exception ex)
            {
                ex.ToString();
                return StatusCode(500, "Internal Server Error, Please Try Again Later!");
            }
        }
        [HttpGet("Innotpunch")]
        public IActionResult Innotpunch(string reportType)
        {
            try
            {
                var reportBytes = _globalMaster.reportManager.CreateReportFile_Attendance_Innotpunch(reportType);
                if (reportBytes == null)
                {
                    return NotFound();
                }
                return File(reportBytes, MediaTypeNames.Application.Octet, _dgCommon.GetContentType(reportType));
            }
            catch (Exception ex)
            {
                ex.ToString();
                return StatusCode(500, "Internal Server Error, Please Try Again Later!");
            }
        }
        [HttpGet("Outnotpunch")]
        public IActionResult Outnotpunch(string reportType)
        {
            try
            {
                var reportBytes = _globalMaster.reportManager.CreateReportFile_Attendance_Outnotpunch(reportType);
                if (reportBytes == null)
                {
                    return NotFound();
                }
                return File(reportBytes, MediaTypeNames.Application.Octet, _dgCommon.GetContentType(reportType));
            }
            catch (Exception ex)
            {
                ex.ToString();
                return StatusCode(500, "Internal Server Error, Please Try Again Later!");
            }
        }
        [HttpGet("InOutnotpunch")]
        public IActionResult InOutnotpunch(string reportType)
        {
            try
            {
                var reportBytes = _globalMaster.reportManager.CreateReportFile_Attendance_InOutnotpunch(reportType);
                if (reportBytes == null)
                {
                    return NotFound();
                }
                return File(reportBytes, MediaTypeNames.Application.Octet, _dgCommon.GetContentType(reportType));
            }
            catch (Exception ex)
            {
                ex.ToString();
                return StatusCode(500, "Internal Server Error, Please Try Again Later!");
            }
        }
        [HttpGet("Dg_Att_SectionWiseSummary")]
        public IActionResult Dg_Att_SectionWiseSummary(string reportType)
        {
            try
            {
                var reportBytes = _globalMaster.reportManager.Dg_Att_SectionWiseSummary(reportType);
                if (reportBytes == null)
                {
                    return NotFound();
                }
                return File(reportBytes, MediaTypeNames.Application.Octet, _dgCommon.GetContentType(reportType));
            }
            catch (Exception ex)
            {
                ex.ToString();
                return StatusCode(500, "Internal Server Error, Please Try Again Later!");
            }
        }
        [HttpGet("Dg_CreateReportFile_Ecard_u1")]
        public IActionResult Dg_CreateReportFile_Ecard_u1(string reportType)
        {
            try
            {
                var reportBytes = _globalMaster.reportManager.Dg_CreateReportFile_Ecard_u1(reportType);
                if (reportBytes == null)
                {
                    return NotFound();
                }
                return File(reportBytes, MediaTypeNames.Application.Octet, _dgCommon.GetContentType(reportType));
            }
            catch (Exception ex)
            {
                ex.ToString();
                return StatusCode(500, "Internal Server Error, Please Try Again Later!");
            }
        }
        [HttpGet("Dg_Ecard_Date_To_Date")]
        public IActionResult Dg_CreateReportFile_Ecard_Date_To_Date(string reportType)
        {
            try
            {
                var reportBytes = _globalMaster.reportManager.Dg_CreateReportFile_Ecard_Date_To_Date(reportType);
                if (reportBytes == null)
                {
                    return NotFound();
                }
                return File(reportBytes, MediaTypeNames.Application.Octet, _dgCommon.GetContentType(reportType));
            }
            catch (Exception ex)
            {
                ex.ToString();
                return StatusCode(500, "Internal Server Error, Please Try Again Later!");
            }
        }
        [HttpGet("Dg_CreateReportFile_Ecard_complaince_2hour")]
        public IActionResult Dg_CreateReportFile_Ecard_complaince_2hour(string reportType)
        {
            try
            {
                var reportBytes = _globalMaster.reportManager.Dg_CreateReportFile_Ecard_complaince_2hour(reportType);
                if (reportBytes == null)
                {
                    return NotFound();
                }
                return File(reportBytes, MediaTypeNames.Application.Octet, _dgCommon.GetContentType(reportType));
            }
            catch (Exception ex)
            {
                ex.ToString();
                return StatusCode(500, "Internal Server Error, Please Try Again Later!");
            }
        }
        [HttpGet("Dg_CreateReportFile_Ecard_bayer_4hour")]
        public IActionResult Dg_CreateReportFile_Ecard_bayer_4hour(string reportType)
        {
            try
            {
                var reportBytes = _globalMaster.reportManager.Dg_CreateReportFile_Ecard_bayer_4hour(reportType);
                if (reportBytes == null)
                {
                    return NotFound();
                }
                return File(reportBytes, MediaTypeNames.Application.Octet, _dgCommon.GetContentType(reportType));
            }
            catch (Exception ex)
            {
                ex.ToString();
                return StatusCode(500, "Internal Server Error, Please Try Again Later!");
            }
        }
        [HttpGet("MonthlyAttendance")]
        public IActionResult Dg_MonthlyAttendance(string reportType)
        {
            try
            {
                var reportBytes = _globalMaster.reportManager.Dg_Att_MonthlyAttendance(reportType);
                if (reportBytes == null)
                {
                    return NotFound();
                }
                return File(reportBytes, MediaTypeNames.Application.Octet, _dgCommon.GetContentType(reportType));
            }
            catch (Exception ex)
            {
                ex.ToString();
                return StatusCode(500, "Internal Server Error, Please Try Again Later!");
            }
        }
        [HttpGet("EmployeeMenualAttnList")]
        public IActionResult EmployeeMenualAttnList(string reportType)
        {
            try
            {
                var reportBytes = _globalMaster.reportManager.EmployeeMenualAttnList(reportType);
                if (reportBytes == null)
                {
                    return NotFound();
                }
                return File(reportBytes, MediaTypeNames.Application.Octet, _dgCommon.GetContentType(reportType));
            }
            catch (Exception ex)
            {
                ex.ToString();
                return StatusCode(500, "Internal Server Error, Please Try Again Later!");
            }
        }
        [HttpGet("Date_wise_total_ot_details")]
        public IActionResult Date_wise_total_ot_details(string reportType)
        {
            try
            {
                var reportBytes = _globalMaster.reportManager.Date_wise_total_ot_details(reportType);
                if (reportBytes == null)
                {
                    return NotFound();
                }
                return File(reportBytes, MediaTypeNames.Application.Octet, _dgCommon.GetContentType(reportType));
            }
            catch (Exception ex)
            {
                ex.ToString();
                return StatusCode(500, "Internal Server Error, Please Try Again Later!");
            }
        }
        #endregion

        #region"Salary"
        [HttpGet("SalaryShert")]
        public IActionResult SalaryShert(string reportType)
        {
            try
            {
                var reportBytes = _globalMaster.reportManager.CreateReportFile_salarysheet_D(reportType);
                if (reportBytes == null)
                {
                    return NotFound();
                }
                return File(reportBytes, MediaTypeNames.Application.Octet, _dgCommon.GetContentType(reportType));
            }
            catch (Exception ex)
            {
                ex.ToString();
                return StatusCode(500, "Internal Server Error, Please Try Again Later!");
            }
        }
        [HttpGet("Sal_salarysheet_Details")]
        public IActionResult SalaryShert_Details_Actual(string reportType)
        {
            try
            {
                var reportBytes = _globalMaster.reportManager.Sal_salarysheet_Details(reportType);
                if (reportBytes == null)
                {
                    return NotFound();
                }
                return File(reportBytes, MediaTypeNames.Application.Octet, _dgCommon.GetContentType(reportType));
            }
            catch (Exception ex)
            {
                ex.ToString();
                return StatusCode(500, "Internal Server Error, Please Try Again Later!");
            }
        }
        [HttpGet("Sal_salarysheet_Details_Excel")]
        public IActionResult Sal_salarysheet_Details_Excel(string reportType)
        {
            try
            {
                var reportBytes = _globalMaster.reportManager.Sal_salarysheet_Details_Excel(reportType);
                if (reportBytes == null)
                {
                    return NotFound();
                }
                return File(reportBytes, MediaTypeNames.Application.Octet, _dgCommon.GetContentType(reportType));
            }
            catch (Exception ex)
            {
                ex.ToString();
                return StatusCode(500, "Internal Server Error, Please Try Again Later!");
            }
        }
        [HttpGet("Sal_salarysheet_DetailsReport")]
        public IActionResult SalaryShert_Detail_2Hour(string reportType)
        {
            try
            {
                var reportBytes = _globalMaster.reportManager.Sal_salarysheet_DetailsReport(reportType);
                if (reportBytes == null)
                {
                    return NotFound();
                }
                return File(reportBytes, MediaTypeNames.Application.Octet, _dgCommon.GetContentType(reportType));
            }
            catch (Exception ex)
            {
                ex.ToString();
                return StatusCode(500, "Internal Server Error, Please Try Again Later!");
            }
        }
        [HttpGet("Sal_salarysheet_ReportDetails")]
        public IActionResult SalaryShert_Detail_4Hour(string reportType)
        {
            try
            {
                var reportBytes = _globalMaster.reportManager.Sal_salarysheet_ReportDetails(reportType);
                if (reportBytes == null)
                {
                    return NotFound();
                }
                return File(reportBytes, MediaTypeNames.Application.Octet, _dgCommon.GetContentType(reportType));
            }
            catch (Exception ex)
            {
                ex.ToString();
                return StatusCode(500, "Internal Server Error, Please Try Again Later!");
            }
        }
        [HttpGet("SalaryShert_LineWise_Summary")]
        public IActionResult SalaryShert_LineWise_Summary(string reportType)
        {
            try
            {
                var reportBytes = _globalMaster.reportManager.SalaryShert_LineWise_Summary(reportType);
                if (reportBytes == null)
                {
                    return NotFound();
                }
                return File(reportBytes, MediaTypeNames.Application.Octet, _dgCommon.GetContentType(reportType));
            }
            catch (Exception ex)
            {
                ex.ToString();
                return StatusCode(500, "Internal Server Error, Please Try Again Later!");
            }
        }
        [HttpGet("SalaryShert_53")]
        public IActionResult SalaryShert_53(string reportType)
        {
            try
            {
                var reportBytes = _globalMaster.reportManager.CreateReportFile_salarysheet_D_53(reportType);
                if (reportBytes == null)
                {
                    return NotFound();
                }
                return File(reportBytes, MediaTypeNames.Application.Octet, _dgCommon.GetContentType(reportType));
            }
            catch (Exception ex)
            {
                ex.ToString();
                return StatusCode(500, "Internal Server Error, Please Try Again Later!");
            }
        }
        [HttpGet("SalaryShert_53_complaince_2hour")]
        public IActionResult SalaryShert_53_complaince_2hour(string reportType)
        {
            try
            {
                var reportBytes = _globalMaster.reportManager.CreateReportFile_salarysheet_D_53_2(reportType);
                if (reportBytes == null)
                {
                    return NotFound();
                }
                return File(reportBytes, MediaTypeNames.Application.Octet, _dgCommon.GetContentType(reportType));
            }
            catch (Exception ex)
            {
                ex.ToString();
                return StatusCode(500, "Internal Server Error, Please Try Again Later!");
            }
        }
        [HttpGet("SalaryShert_53_complaince_4hour")]
        public IActionResult SalaryShert_53_complaince_4hour(string reportType)
        {
            try
            {
                var reportBytes = _globalMaster.reportManager.CreateReportFile_salarysheet_D_53_4(reportType);
                if (reportBytes == null)
                {
                    return NotFound();
                }
                return File(reportBytes, MediaTypeNames.Application.Octet, _dgCommon.GetContentType(reportType));
            }
            catch (Exception ex)
            {
                ex.ToString();
                return StatusCode(500, "Internal Server Error, Please Try Again Later!");
            }
        }
        [HttpGet("Payslip")]
        public IActionResult Payslip(string reportType)
        {
            try
            {
                var reportBytes = _globalMaster.reportManager.CreateReportFile_Payslip(reportType);
                if (reportBytes == null)
                {
                    return NotFound();
                }
                return File(reportBytes, MediaTypeNames.Application.Octet, _dgCommon.GetContentType(reportType));
            }
            catch (Exception ex)
            {
                ex.ToString();
                return StatusCode(500, "Internal Server Error, Please Try Again Later!");
            }
        }
        [HttpGet("Payslip_complaince_2hour")]
        public IActionResult Payslip_complaince_2hour(string reportType)
        {
            try
            {
                var reportBytes = _globalMaster.reportManager.CreateReportFile_Payslip_complaince_2hour(reportType);
                if (reportBytes == null)
                {
                    return NotFound();
                }
                return File(reportBytes, MediaTypeNames.Application.Octet, _dgCommon.GetContentType(reportType));
            }
            catch (Exception ex)
            {
                ex.ToString();
                return StatusCode(500, "Internal Server Error, Please Try Again Later!");
            }
        }
        [HttpGet("Payslip_bayer_4hour")]
        public IActionResult Payslip_bayer_4hour(string reportType)
        {
            try
            {
                var reportBytes = _globalMaster.reportManager.CreateReportFile_Payslip_bayer_4hour(reportType);
                if (reportBytes == null)
                {
                    return NotFound();
                }
                return File(reportBytes, MediaTypeNames.Application.Octet, _dgCommon.GetContentType(reportType));
            }
            catch (Exception ex)
            {
                ex.ToString();
                return StatusCode(500, "Internal Server Error, Please Try Again Later!");
            }
        }
        [HttpGet("SalarySheet_Bank")]
        public IActionResult SalarySheet_Bank(string reportType)
        {
            try
            {
                var reportBytes = _globalMaster.reportManager.CreateReportFile_SalarySheetBank(reportType);
                if (reportBytes == null)
                {
                    return NotFound();
                }
                return File(reportBytes, MediaTypeNames.Application.Octet, _dgCommon.GetContentType(reportType));
            }
            catch (Exception ex)
            {
                ex.ToString();
                return StatusCode(500, "Internal Server Error, Please Try Again Later!");
            }
        }
        [HttpGet("SalarySheetEXOT")]
        public IActionResult SalarySheetEXOT(string reportType)
        {
            try
            {
                var reportBytes = _globalMaster.reportManager.CreateReportFile_SalarySheetEXOT(reportType);
                if (reportBytes == null)
                {
                    return NotFound();
                }
                return File(reportBytes, MediaTypeNames.Application.Octet, _dgCommon.GetContentType(reportType));
            }
            catch (Exception ex)
            {
                ex.ToString();
                return StatusCode(500, "Internal Server Error, Please Try Again Later!");
            }
        }
        [HttpGet("SalarySheetOTEXOT")]
        public IActionResult SalarySheetOTEXOT(string reportType)
        {
            try
            {
                var reportBytes = _globalMaster.reportManager.CreateReportFile_SalarySheetOTEXOT(reportType);
                if (reportBytes == null)
                {
                    return NotFound();
                }
                return File(reportBytes, MediaTypeNames.Application.Octet, _dgCommon.GetContentType(reportType));
            }
            catch (Exception ex)
            {
                ex.ToString();
                return StatusCode(500, "Internal Server Error, Please Try Again Later!");
            }
        }
        [HttpGet("OTDetails")]
        public IActionResult SalarySheetOTDetails(string reportType)
        {
            try
            {
                var reportBytes = _globalMaster.reportManager.CreateReportFile_OTDetails(reportType);
                if (reportBytes == null)
                {
                    return NotFound();
                }
                return File(reportBytes, MediaTypeNames.Application.Octet, _dgCommon.GetContentType(reportType));
            }
            catch (Exception ex)
            {
                ex.ToString();
                return StatusCode(500, "Internal Server Error, Please Try Again Later!");
            }
        }
        [HttpGet("OTSummary")]
        public IActionResult SalarySheetOTSummary(string reportType)
        {
            try
            {
                var reportBytes = _globalMaster.reportManager.CreateReportFile_OTSummary(reportType);
                if (reportBytes == null)
                {
                    return NotFound();
                }
                return File(reportBytes, MediaTypeNames.Application.Octet, _dgCommon.GetContentType(reportType));
            }
            catch (Exception ex)
            {
                ex.ToString();
                return StatusCode(500, "Internal Server Error, Please Try Again Later!");
            }
        }
        [HttpGet("SalarySheetSummary")]
        public IActionResult SalarySheetSummary(string reportType)
        {
            try
            {
                var reportBytes = _globalMaster.reportManager.CreateReportFile_SalarySheetSummary(reportType);
                if (reportBytes == null)
                {
                    return NotFound();
                }
                return File(reportBytes, MediaTypeNames.Application.Octet, _dgCommon.GetContentType(reportType));
            }
            catch (Exception ex)
            {
                ex.ToString();
                return StatusCode(500, "Internal Server Error, Please Try Again Later!");
            }
        }
        [HttpGet("SalarySheetOtExotSummary")]
        public IActionResult SalarySheetOtExotSummary(string reportType)
        {
            try
            {
                var reportBytes = _globalMaster.reportManager.CreateReportFile_SalarySheetOtExotSummary(reportType);
                if (reportBytes == null)
                {
                    return NotFound();
                }
                return File(reportBytes, MediaTypeNames.Application.Octet, _dgCommon.GetContentType(reportType));
            }
            catch (Exception ex)
            {
                ex.ToString();
                return StatusCode(500, "Internal Server Error, Please Try Again Later!");
            }
        }
        [HttpGet("SalarySheetBank_Excel")]
        public IActionResult Sal_SalarySheetBankExcel(string reportType)
        {
            try
            {
                var reportBytes = _globalMaster.reportManager.Sal_SalarySheetBankExcel(reportType);
                if (reportBytes == null)
                {
                    return NotFound();
                }
                return File(reportBytes, MediaTypeNames.Application.Octet, _dgCommon.GetContentType("excel"));
            }
            catch (Exception ex)
            {
                ex.ToString();
                return StatusCode(500, "Internal Server Error, Please Try Again Later!");
            }
        }
        [HttpGet("Sal_TiffinBillSummary")]
        public IActionResult Sal_TiffinBillSummary(string reportType)
        {
            try
            {
                var reportBytes = _globalMaster.reportManager.Sal_EmployeeTiffinBillSummary(reportType);
                if (reportBytes == null)
                {
                    return NotFound();
                }
                return File(reportBytes, MediaTypeNames.Application.Octet, _dgCommon.GetContentType(reportType));
            }
            catch (Exception ex)
            {
                ex.ToString();
                return StatusCode(500, "Internal Server Error, Please Try Again Later!");
            }
        }
        [HttpGet("Sal_NightBillSummary")]
        public IActionResult Sal_NightBillSummary(string reportType)
        {
            try
            {
                var reportBytes = _globalMaster.reportManager.Sal_EmployeeNightBillSummary(reportType);
                if (reportBytes == null)
                {
                    return NotFound();
                }
                return File(reportBytes, MediaTypeNames.Application.Octet, _dgCommon.GetContentType(reportType));
            }
            catch (Exception ex)
            {
                ex.ToString();
                return StatusCode(500, "Internal Server Error, Please Try Again Later!");
            }
        }
        [HttpGet("Sal_EmpTiffinBillAmount")]
        public IActionResult Sal_TiffinBillAmount(string reportType)
        {
            try
            {
                var reportBytes = _globalMaster.reportManager.Sal_EmployeeTiffinBillAmount(reportType);
                if (reportBytes == null)
                {
                    return NotFound();
                }
                return File(reportBytes, MediaTypeNames.Application.Octet, _dgCommon.GetContentType(reportType));
            }
            catch (Exception ex)
            {
                ex.ToString();
                return StatusCode(500, "Internal Server Error, Please Try Again Later!");
            }
        }
        [HttpGet("Sal_EmpNightBillAmount")]
        public IActionResult Sal_NightBillAmount(string reportType)
        {
            try
            {
                var reportBytes = _globalMaster.reportManager.Sal_EmployeeNightBillAmount(reportType);
                if (reportBytes == null)
                {
                    return NotFound();
                }
                return File(reportBytes, MediaTypeNames.Application.Octet, _dgCommon.GetContentType(reportType));
            }
            catch (Exception ex)
            {
                ex.ToString();
                return StatusCode(500, "Internal Server Error, Please Try Again Later!");
            }
        }
        #endregion

        #region"Others"
        [HttpGet("SalaryShert_Bank")]
        public IActionResult SalaryShert_Bank(string reportType)
        {
            try
            {
                var reportBytes = _globalMaster.reportManager.CreateReportFile_salarysheet_D_Bank(reportType);
                if (reportBytes == null)
                {
                    return NotFound();
                }
                return File(reportBytes, MediaTypeNames.Application.Octet, _dgCommon.GetContentType(reportType));
            }
            catch (Exception ex)
            {
                ex.ToString();
                return StatusCode(500, "Internal Server Error, Please Try Again Later!");
            }
        }
        [HttpGet("Ecard")]
        public IActionResult Ecard(string reportType)
        {
            try
            {
                var reportBytes = _globalMaster.reportManager.CreateReportFile_Ecard(reportType);
                if (reportBytes == null)
                {
                    return NotFound();
                }
                return File(reportBytes, MediaTypeNames.Application.Octet, _dgCommon.GetContentType(reportType));
            }
            catch (Exception ex)
            {
                ex.ToString();
                return StatusCode(500, "Internal Server Error, Please Try Again Later!");
            }
        }
        
        [HttpGet("Payslip_U1")]
        public IActionResult Payslip_U1(string reportType)
        {
            try
            {
                var reportBytes = _globalMaster.reportManager.CreateReportFile_Payslip_U1(reportType);
                if (reportBytes == null)
                {
                    return NotFound();
                }
                return File(reportBytes, MediaTypeNames.Application.Octet, _dgCommon.GetContentType(reportType));
            }
            catch (Exception ex)
            {
                ex.ToString();
                return StatusCode(500, "Internal Server Error, Please Try Again Later!");
            }
        }      
        [HttpGet("SalarySheet-NC")]
        public IActionResult Export_Report_SalarySheet(string reportType)
        {
            try
            {
                var reportBytes = _globalMaster.reportManager.Export_Report_SalarySheet(reportType);
                if (reportBytes == null)
                {
                    return NotFound();
                }
                return File(reportBytes, MediaTypeNames.Application.Octet, _dgCommon.GetContentType(reportType));
            }
            catch (Exception ex)
            {
                ex.ToString();
                return StatusCode(500, "Internal Server Error, Please Try Again Later!");
            }
        }
        [HttpGet("testBarcode")]
        public IActionResult testBarcodeRDLC(string reportType)
        {
            try
            {
                var reportBytes = _globalMaster.reportManager.testBarcode(reportType);
                if (reportBytes == null)
                {
                    return NotFound();
                }
                return File(reportBytes, MediaTypeNames.Application.Octet, _dgCommon.GetContentType(reportType));
            }
            catch (Exception ex)
            {
                ex.ToString();
                return StatusCode(500, "Internal Server Error, Please Try Again Later!");
            }
        }
        #endregion

    }
}