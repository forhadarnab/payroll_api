using BOL.Models;

namespace BLL.Interfaces.Manager.Report
{
    public interface IReportManager
    {
        #region"Employee"
        byte[] Export_Report_Employee_Details_InActive(string reportType);
        byte[] Export_Report_Employee_Details_Active(string reportType);
        byte[] Dg_ActiveEmployeesWithImage(string reportType);
        byte[] Dg_JoinDateWise_Employee_Details_Active(string reportType);
        byte[] Dg_CreateReportFileIDCARD(string reportType);
        byte[] Dg_CreateReportFileIDCARD_BN(string reportType);
        byte[] Dg_EmpAgeCertificate(string reportType);
        byte[] Dg_IncrementLetter(string reportType);
        byte[] Dg_PromotionWithIncrementLetter(string reportType);
        byte[] Dg_IncrementLetter_Bangla(string reportType);
        byte[] Dg_PromotionWithIncrementLetter_Bangla(string reportType);
        byte[] Dg_AppointmentLetter(string reportType);
        byte[] Dg_AppointmentLetter_IFL(string reportType);
        byte[] Dg_AppointmentLetter_StaffBangla(string reportType);
        byte[] Dg_AppointmentLetter_EN(string reportType);
        byte[] Dg_NoticeLetter_1st(string reportType);
        byte[] Dg_NoticeLetter_2nd(string reportType);
        byte[] Dg_MaleFemaleDetails(string reportType);
        byte[] Dg_MaleFemaleSummary(string reportType);
        byte[] Dg_EmployeeDetailsRegligion(string reportType);
        byte[] Dg_ProximityCardChecklist(string reportType);
        byte[] Dg_EmployeewiseIncrementDetails(string reportType);
        byte[] Dg_CutoffDatewiseIncrementPendingList(string reportType);
        byte[] Dg_CutoffDatewiseIncrementApprovedList(string reportType);
        byte[] Dg_EmployeeWiseDetailedInformation(string reportType);
        byte[] Dg_Emp_TiffinBillStatus(string reportType);
        byte[] Dg_Emp_NightBillStatus(string reportType);
        byte[] Export_Report_Employee_shiftchange_history(string reportType);
        byte[] Export_Report_Employee_shiftchange_history_New(ReportParameterModel obj);
        byte[] EmployeeNominationForm(string reportType);
        byte[] EmployeeJoiningLetter(string reportType);
        byte[] EmployeeJobApplication(string reportType);
        byte[] EmployeeDetailsExcel(string reportType);
        byte[] EmployeeBackgroundCheck(string reportType);
        byte[] EmployeeIvelatution_From(string reportType);

        //Audit
        byte[] Report_preriodical_present_absent_leave_Weekly_holiday_special_holiday(string reportType);
        byte[] Report_Salary_info_Audit_Excel(string reportType);
        byte[] Report_Joindate_wise_info_Audit_Excel(string reportType);
        byte[] Report_manual_attendsance_Audit_Excel(string reportType);
        byte[] Report_TiffinBill_Audit_Excel(string reportType);
        byte[] Report_Night_Audit_Excel(string reportType);
        byte[] Report_Leave_transction_Audit_Excel(string reportType);
        byte[] Report_In_Activeemp_Date_wise_Audit_Excel(string reportType);
        //Audit
        #endregion

        #region"Leave"
        byte[] Dg_LeaveBalances(string reportType);
        byte[] Dg_LeaveTransactions(string reportType);
        byte[] Dg_IndividualLeaveStatement(string reportType);
        byte[] Dg_MaternityLeaveList(string reportType);
        byte[] Dg_MaternityLeavePayment(string reportType);
        byte[] Dg_LeaveForm(string reportType);
        #endregion

        #region"Attendance"
        byte[] CreateReportFile_Attendance_Present(string reportType);
        byte[] Dg_Att_PresentWithImages(string reportType);
        byte[] CreateReportFile_Attendance_Absent(string reportType);
        byte[] Dg_Att_AbsentWithImages(string reportType);
        byte[] CreateReportFile_Attendance_Late(string reportType);
        byte[] CreateReportFile_Attendance_Innotpunch(string reportType);
        byte[] CreateReportFile_Attendance_Outnotpunch(string reportType);
        byte[] CreateReportFile_Attendance_InOutnotpunch(string reportType);
        byte[] Dg_Att_SectionWiseSummary(string reportType);
        byte[] Dg_CreateReportFile_Ecard_u1(string reportType);
        byte[] Dg_CreateReportFile_Ecard_Date_To_Date(string reportType);
        byte[] Dg_CreateReportFile_Ecard_complaince_2hour(string reportType);
        byte[] Dg_CreateReportFile_Ecard_bayer_4hour(string reportType);
        byte[] Dg_Att_MonthlyAttendance(string reportType);
        byte[] EmployeeMenualAttnList(string reportType);
        byte[] Date_wise_total_ot_details(string reportType);
        #endregion

        #region"Salary"
        byte[] CreateReportFile_salarysheet_D(string reportType);
        byte[] Sal_salarysheet_Details(string reportType);
        byte[] Sal_salarysheet_Details_Excel(string reportType);
        byte[] Sal_salarysheet_DetailsReport(string reportType);
        byte[] Sal_salarysheet_ReportDetails(string reportType);
        byte[] SalaryShert_LineWise_Summary(string reportType);
        byte[] CreateReportFile_salarysheet_D_53(string reportType);
        byte[] CreateReportFile_salarysheet_D_53_2(string reportType);
        byte[] CreateReportFile_salarysheet_D_53_4(string reportType);
        byte[] CreateReportFile_Payslip(string reportType);
        byte[] CreateReportFile_Payslip_complaince_2hour(string reportType);
        byte[] CreateReportFile_Payslip_bayer_4hour(string reportType);
        byte[] CreateReportFile_SalarySheetBank(string reportType);
        byte[] CreateReportFile_SalarySheetEXOT(string reportType);
        byte[] CreateReportFile_SalarySheetOTEXOT(string reportType);
        byte[] CreateReportFile_OTDetails(string reportType);
        byte[] CreateReportFile_OTSummary(string reportType);
        byte[] CreateReportFile_SalarySheetSummary(string reportType);
        byte[] CreateReportFile_SalarySheetOtExotSummary(string reportType);
        byte[] Sal_SalarySheetBankExcel(string reportType);
        byte[] Sal_EmployeeTiffinBillSummary(string reportType);
        byte[] Sal_EmployeeNightBillSummary(string reportType);
        byte[] Sal_EmployeeTiffinBillAmount(string reportType);
        byte[] Sal_EmployeeNightBillAmount(string reportType);
        #endregion

        #region"Others"
        byte[] CreateReportFile_salarysheet_D_Bank(string reportType);
        byte[] CreateReportFile_Ecard(string reportType);       
        byte[] CreateReportFile_Payslip_U1(string reportType);
        byte[] Export_Report_SalarySheet(string reportType);   
        byte[] testBarcode(string reportType);
        #endregion
    }
}