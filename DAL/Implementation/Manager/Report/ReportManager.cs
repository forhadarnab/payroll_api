using BLL.Interfaces.Manager.Report;
using BLL.Utility;
using BOL.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Reporting.NETCore;
using System.Data;
using System.Data.SqlClient;
using System.Drawing.Imaging;
using System.Drawing;
using BarcodeLib;

namespace DAL.Implementation.Manager.Report
{
    public class ReportManager : IReportManager
    {
        private readonly Dg_Common _dgCommon;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly SqlConnection _connection;
        public ReportManager(Dg_Common dgCommon, IWebHostEnvironment webHostEnvironment)
        {
            _dgCommon = dgCommon;
            _webHostEnvironment = webHostEnvironment;
            _connection = new SqlConnection(Getway.Dg_Payroll);
        }
        
        #region"Employee"
        public byte[] Export_Report_Employee_Details_InActive(string reportType) //ok
        {
            var ReportTitle = this.ReportTitle("dg_print_employeelist", _connection);
            var data = _dgCommon.get_InformationDataTable("Dg_Pay_Rep_EmployeeDetails_inactive", _connection);
            string dataset = "DataSet1";
            string path = $"{_webHostEnvironment.WebRootPath}\\Report\\dg_Inactiveemp_details.rdlc";
            ReportParameterCollection reportParameters = new ReportParameterCollection
            {
                new ReportParameter("Title",string.Concat("Employee InActive List - ",ReportTitle.DepartmentName,ReportTitle.SectionName,ReportTitle.BuildingName,ReportTitle.LineName)),
                new ReportParameter("PrintUser",ReportTitle.PrintUser)
            };
            byte[] reportBytes = _dgCommon.GenerateReport(data, dataset, path, reportType, reportParameters);
            return reportBytes;
        }
        public byte[] Export_Report_Employee_Details_Active(string reportType) //ok
        {
            var ReportTitle = this.ReportTitle("dg_print_employeelist", _connection);
            var data = _dgCommon.get_InformationDataTable("Dg_Pay_Rep_EmployeeDetails", _connection);
            string dataset = "DataSet1";
            string path = $"{_webHostEnvironment.WebRootPath}\\Report\\dg_emp_details.rdlc";
            ReportParameterCollection reportParameters = new ReportParameterCollection
            {
                new ReportParameter("PrintUser",ReportTitle.PrintUser),
                new ReportParameter("Title",string.Concat("Employee Active List - ",ReportTitle.DepartmentName,ReportTitle.SectionName,ReportTitle.BuildingName,ReportTitle.LineName))
            };
            byte[] reportBytes = _dgCommon.GenerateReport(data, dataset, path, reportType, reportParameters);
            return reportBytes;
        }
        public byte[] Dg_ActiveEmployeesWithImage(string reportType) //ok
        {
            var ReportTitle = this.ReportTitle("dg_print_employeelist", _connection);
            var data = _dgCommon.get_InformationDataTable("Dg_Pay_rpt_activeEmployeeListImg", _connection);
            string dataset = "Attinout";
            string path = $"{_webHostEnvironment.WebRootPath}\\Report\\Dg_ActiveEmployeeImage.rdlc";
            string imgPath = new Uri($"{_webHostEnvironment.WebRootPath}\\EmployeeImage\\").AbsoluteUri;
            ReportParameterCollection reportParameters = new ReportParameterCollection
            {
                new ReportParameter("EmpImagePath",imgPath),
                new ReportParameter("Title",string.Concat("Active Employee Image - ",ReportTitle.DepartmentName,ReportTitle.SectionName,ReportTitle.BuildingName,ReportTitle.LineName)),
            };
            byte[] reportBytes = _dgCommon.GenerateReport(data, dataset, path, reportType, reportParameters);
            return reportBytes;
        }
        public byte[] Dg_JoinDateWise_Employee_Details_Active(string reportType) //ok
        {
            var ReportTitle = this.ReportTitle("dg_print_employeelist", _connection);
            var data = _dgCommon.get_InformationDataTable("Dg_Pay_Rep_JoinDateWiseEmployeeDetails", _connection);
            string dataset = "DataSet1";
            string path = $"{_webHostEnvironment.WebRootPath}\\Report\\Dg_JoinDateWiseEmp_details.rdlc";
            ReportParameterCollection reportParameters = new ReportParameterCollection
            {
                new ReportParameter("Title",string.Concat("Join Date wise Employee Active List - ",ReportTitle.DepartmentName,ReportTitle.SectionName,ReportTitle.BuildingName,ReportTitle.LineName)),
                new ReportParameter("PrintUser",ReportTitle.PrintUser)
            };
            byte[] reportBytes = _dgCommon.GenerateReport(data, dataset, path, reportType, reportParameters);
            return reportBytes;
        }
        public byte[] Dg_CreateReportFileIDCARD(string reportType) //ok
        {
            var data = _dgCommon.get_InformationDataTable("Dg_Pay_Rep_EmployeeDetails", _connection);
            this.AddDataColumnWithBarcode(data, 4);
            string dataset = "ID";
            string path = $"{_webHostEnvironment.WebRootPath}\\Report\\Dg_IDCard.rdlc";
            string imgPath = new Uri($"{_webHostEnvironment.WebRootPath}\\EmployeeImage\\").AbsoluteUri;
            string imgPathQR = new Uri($"{_webHostEnvironment.WebRootPath}\\Employee_QRCode\\DG_QR_Code.jpg").AbsoluteUri;
            string imgPathEmpSign = new Uri($"{_webHostEnvironment.WebRootPath}\\EmployeeSignature\\").AbsoluteUri;
            string imgPathAuthSign = new Uri($"{_webHostEnvironment.WebRootPath}\\AuthSign\\").AbsoluteUri;
            ReportParameterCollection reportParameters = new ReportParameterCollection
            {
                new ReportParameter("EmpImagePath",imgPath),
                new ReportParameter("QR",imgPathQR),
                new ReportParameter("EmpSign",imgPathEmpSign),
                new ReportParameter("AuthSign",imgPathAuthSign)
            };
            byte[] reportBytes = _dgCommon.GenerateReport(data, dataset, path, reportType, reportParameters);
            return reportBytes;
        }
        public byte[] Dg_CreateReportFileIDCARD_BN(string reportType) //ok
        {
            var data = _dgCommon.get_InformationDataTable("Dg_Pay_Rep_EmpIdCardBangla", _connection);
            this.AddDataColumnWithBarcode(data, 3);
            string dataset = "IDCard";
            string path = $"{_webHostEnvironment.WebRootPath}\\Report\\Dg_IDCardBangla.rdlc";
            string imgPath = new Uri($"{_webHostEnvironment.WebRootPath}\\EmployeeImage\\").AbsoluteUri;
            string imgPathEmpSign = new Uri($"{_webHostEnvironment.WebRootPath}\\EmployeeSignature\\").AbsoluteUri;
            string imgPathAuthSign = new Uri($"{_webHostEnvironment.WebRootPath}\\AuthSign\\").AbsoluteUri;
            ReportParameterCollection reportParameters = new ReportParameterCollection
            {
                new ReportParameter("EmpImagePath",imgPath),
                new ReportParameter("EmpSign",imgPathEmpSign),
                new ReportParameter("AuthSign",imgPathAuthSign)
            };
            byte[] reportBytes = _dgCommon.GenerateReport(data, dataset, path, reportType, reportParameters);
            return reportBytes;
        }
        public byte[] Dg_EmpAgeCertificate(string reportType) //ok
        {
            var data = _dgCommon.get_InformationDataTable("Dg_Pay_Rep_AgeCertificate", _connection);
            string dataset = "AgeCertificate";
            string path = $"{_webHostEnvironment.WebRootPath}\\Report\\Dg_Emp_AgeCertificate.rdlc";
            byte[] reportBytes = _dgCommon.GenerateReport(data, dataset, path, reportType);
            return reportBytes;
        }
        public byte[] Dg_IncrementLetter(string reportType) //ok
        {
            var data = _dgCommon.get_InformationDataTable("Dg_Rep_IncrementLetter", _connection);
            string dataset = "IncrementLetter";
            string path = $"{_webHostEnvironment.WebRootPath}\\Report\\Dg_IncrementLetter.rdlc";
            byte[] reportBytes = _dgCommon.GenerateReport(data, dataset, path, reportType);
            return reportBytes;
        }
        public byte[] Dg_PromotionWithIncrementLetter(string reportType) //ok
        {
            var data = _dgCommon.get_InformationDataTable("Dg_Rep_PromotionWithIncrementLetter", _connection);
            string dataset = "IncrementLetter";
            string path = $"{_webHostEnvironment.WebRootPath}\\Report\\Dg_PromotionWithIncrementLetter.rdlc";
            byte[] reportBytes = _dgCommon.GenerateReport(data, dataset, path, reportType);
            return reportBytes;
        }
        public byte[] Dg_IncrementLetter_Bangla(string reportType) //ok
        {
            var data = _dgCommon.get_InformationDataTable("Dg_Rep_IncrementLetter", _connection);
            string dataset = "IncrementLetter";
            string path = $"{_webHostEnvironment.WebRootPath}\\Report\\Dg_IncrementLetter_Bangla.rdlc";
            byte[] reportBytes = _dgCommon.GenerateReport(data, dataset, path, reportType);
            return reportBytes;
        }
        public byte[] Dg_PromotionWithIncrementLetter_Bangla(string reportType) //ok
        {
            var data = _dgCommon.get_InformationDataTable("Dg_Rep_PromotionWithIncrementLetter", _connection);
            string dataset = "IncrementLetter";
            string path = $"{_webHostEnvironment.WebRootPath}\\Report\\Dg_PromotionWithIncrementLetter_Bangla.rdlc";
            byte[] reportBytes = _dgCommon.GenerateReport(data, dataset, path, reportType);
            return reportBytes;
        }
        public byte[] Dg_AppointmentLetter(string reportType) //ok
        {
            var data = _dgCommon.get_InformationDataTable("Dg_Rep_AppointmentLetter", _connection);
            string dataset = "AppLttrWorker";
            string path = $"{_webHostEnvironment.WebRootPath}\\Report\\Dg_AppointmentLetter.rdlc";
            string groupLogo = new Uri($"{_webHostEnvironment.WebRootPath}\\AuthSign\\GroupLogo.png").AbsoluteUri;
            ReportParameterCollection reportParameters = new ReportParameterCollection
            {
                new ReportParameter("groupLogo",groupLogo)
            };
            byte[] reportBytes = _dgCommon.GenerateReport(data, dataset, path, reportType, reportParameters);
            return reportBytes;
        }
        public byte[] Dg_AppointmentLetter_IFL(string reportType) //ok
        {
            var data = _dgCommon.get_InformationDataTable("Dg_Rep_AppointmentLetter", _connection);
            string dataset = "AppLttrWorker";
            string path = $"{_webHostEnvironment.WebRootPath}\\Report\\Dg_AppointmentLetter_IFL.rdlc";
            string groupLogo = new Uri($"{_webHostEnvironment.WebRootPath}\\AuthSign\\GroupLogo.png").AbsoluteUri;
            string autSign = new Uri($"{_webHostEnvironment.WebRootPath}\\AuthSign\\53.jpg").AbsoluteUri;
            ReportParameterCollection reportParameters = new ReportParameterCollection
            {
                new ReportParameter("groupLogo",groupLogo),
                new ReportParameter("authSign",autSign)
            };
            byte[] reportBytes = _dgCommon.GenerateReport(data, dataset, path, reportType, reportParameters);
            return reportBytes;
        }
        public byte[] Dg_AppointmentLetter_StaffBangla(string reportType) //ok
        {
            var data = _dgCommon.get_InformationDataTable("Dg_Rep_AppointmentLetter_StaffBN", _connection);
            string dataset = "AppLttrWorker";
            string path = $"{_webHostEnvironment.WebRootPath}\\Report\\Dg_AppointmentLetter_StaffBN.rdlc";
            string groupLogo = new Uri($"{_webHostEnvironment.WebRootPath}\\AuthSign\\GroupLogo.png").AbsoluteUri;
            string authSign = new Uri($"{_webHostEnvironment.WebRootPath}\\AuthSign\\").AbsoluteUri;
            ReportParameterCollection reportParameters = new ReportParameterCollection
            {
                new ReportParameter("groupLogo",groupLogo),
                new ReportParameter("AuthSign",authSign)
            };
            byte[] reportBytes = _dgCommon.GenerateReport(data, dataset, path, reportType, reportParameters);
            return reportBytes;
        }
        public byte[] Dg_AppointmentLetter_EN(string reportType)
        {
            var data = _dgCommon.get_InformationDataTable("Dg_Rep_AppointmentLetter", _connection);
            string dataset = "AppLttrWorker";
            string path = $"{_webHostEnvironment.WebRootPath}\\Report\\Dg_AppointmentLetter_EN.rdlc";
            byte[] reportBytes = _dgCommon.GenerateReport(data, dataset, path, reportType);
            return reportBytes;
        }
        public byte[] Dg_NoticeLetter_1st(string reportType) //ok
        {
            var data = _dgCommon.get_InformationDataTable("Dg_Rep_NoticeLetter", _connection);
            string dataset = "NoticeLetter";
            string path = $"{_webHostEnvironment.WebRootPath}\\Report\\Dg_NoticeLetter_1st.rdlc";
            byte[] reportBytes = _dgCommon.GenerateReport(data, dataset, path, reportType);
            return reportBytes;
        }
        public byte[] Dg_NoticeLetter_2nd(string reportType) //ok
        {
            var data = _dgCommon.get_InformationDataTable("Dg_Rep_NoticeLetter", _connection);
            string dataset = "NoticeLetter";
            string path = $"{_webHostEnvironment.WebRootPath}\\Report\\Dg_NoticeLetter_2nd.rdlc";
            byte[] reportBytes = _dgCommon.GenerateReport(data, dataset, path, reportType);
            return reportBytes;
        }
        public byte[] Dg_MaleFemaleDetails(string reportType) //ok
        {
            var ReportTitle = this.ReportTitle("dg_print_employeelist", _connection);
            var data = _dgCommon.get_InformationDataTable("Dg_Rep_MaleFemale_Detailed", _connection);
            string dataset = "MaleFemaleDetailed";
            string path = $"{_webHostEnvironment.WebRootPath}\\Report\\Dg_MaleFemaleDetails.rdlc";
            ReportParameterCollection reportParameters = new ReportParameterCollection
            {
                new ReportParameter("PrintUser",ReportTitle.PrintUser),
                new ReportParameter("Title",string.Concat("Male / Female Detailed Report - ",ReportTitle.DepartmentName,ReportTitle.SectionName,ReportTitle.BuildingName,ReportTitle.LineName)),
            };
            byte[] reportBytes = _dgCommon.GenerateReport(data, dataset, path, reportType, reportParameters);
            return reportBytes;
        }
        public byte[] Dg_MaleFemaleSummary(string reportType) //ok
        {
            var ReportTitle = this.ReportTitle("dg_print_employeelist", _connection);
            var data = _dgCommon.get_InformationDataTable("Dg_Rep_MaleFemale_Summary", _connection);
            string dataset = "MaleFemale";
            string path = $"{_webHostEnvironment.WebRootPath}\\Report\\Dg_MaleFemaleSummary.rdlc";
            ReportParameterCollection reportParameters = new ReportParameterCollection
            {
                new ReportParameter("PrintUser",ReportTitle.PrintUser),
                new ReportParameter("Title",string.Concat("Male / Female Summary Report - ",ReportTitle.DepartmentName,ReportTitle.SectionName,ReportTitle.BuildingName,ReportTitle.LineName)),
            };
            byte[] reportBytes = _dgCommon.GenerateReport(data, dataset, path, reportType, reportParameters);
            return reportBytes;
        }
        public byte[] Dg_EmployeeDetailsRegligion(string reportType) //ok
        {
            var ReportTitle = this.ReportTitle("dg_print_employeelist", _connection);
            var data = _dgCommon.get_InformationDataTable("Dg_Rep_EmployeeDetails_Regligion", _connection);
            string dataset = "EmployeeDetails_Religion";
            string path = $"{_webHostEnvironment.WebRootPath}\\Report\\Dg_EmployeeDetailsRegligion.rdlc";
            ReportParameterCollection reportParameters = new ReportParameterCollection
            {
                new ReportParameter("PrintUser",ReportTitle.PrintUser),
                new ReportParameter("Title",string.Concat("EmployeeDetails_Religion - ",ReportTitle.DepartmentName,ReportTitle.SectionName,ReportTitle.BuildingName,ReportTitle.LineName)),
            };
            byte[] reportBytes = _dgCommon.GenerateReport(data, dataset, path, reportType, reportParameters);
            return reportBytes;
        }
        public byte[] Dg_ProximityCardChecklist(string reportType) //ok
        {
            var ReportTitle = this.ReportTitle("dg_print_employeelist", _connection);
            var data = _dgCommon.get_InformationDataTable("Dg_Rep_EmployeeProxIDChkList", _connection);
            string dataset = "ProxIDList";
            string path = $"{_webHostEnvironment.WebRootPath}\\Report\\Dg_ProximityCardChecklist.rdlc";
            ReportParameterCollection reportParameters = new ReportParameterCollection
            {
                new ReportParameter("PrintUser",ReportTitle.PrintUser),
                new ReportParameter("Title",string.Concat("Proximity Card Checklist - ",ReportTitle.DepartmentName,ReportTitle.SectionName,ReportTitle.BuildingName,ReportTitle.LineName)),
            };
            byte[] reportBytes = _dgCommon.GenerateReport(data, dataset, path, reportType, reportParameters);
            return reportBytes;
        }
        public byte[] Dg_EmployeewiseIncrementDetails(string reportType) //ok
        {
            var data = _dgCommon.get_InformationDataTable("Dg_Rep_EmpIncrement_Details", _connection);
            string dataset = "rpt_empIncrement_Details";
            string path = $"{_webHostEnvironment.WebRootPath}\\Report\\Dg_EmployeewiseIncrementDetails.rdlc";
            byte[] reportBytes = _dgCommon.GenerateReport(data, dataset, path, reportType);
            return reportBytes;
        }
        public byte[] Dg_CutoffDatewiseIncrementPendingList(string reportType) //ok
        {
            var data = _dgCommon.get_InformationDataTable("Dg_Rep_empIncrement_PendingList", _connection);
            string dataset = "rpt_empIncrement_PendingList";
            string path = $"{_webHostEnvironment.WebRootPath}\\Report\\Dg_CutoffDatewiseIncrementPendingList.rdlc";
            byte[] reportBytes = _dgCommon.GenerateReport(data, dataset, path, reportType);
            return reportBytes;
        }
        public byte[] Dg_CutoffDatewiseIncrementApprovedList(string reportType) //ok
        {
            var data = _dgCommon.get_InformationDataTable("Dg_Rep_empIncremented_List", _connection);
            string dataset = "rpt_empIncrement_ApproveList";
            string path = $"{_webHostEnvironment.WebRootPath}\\Report\\Dg_CutoffDatewiseIncrementApprove.rdlc";
            byte[] reportBytes = _dgCommon.GenerateReport(data, dataset, path, reportType);
            return reportBytes;
        }
        public byte[] Dg_EmployeeWiseDetailedInformation(string reportType) //ok
        {
            var ReportTitle = this.ReportTitle("dg_print_employeelist", _connection);
            var data = _dgCommon.get_InformationDataTable("Dg_Rep_emp_CV_Single 0", _connection);
            string dataset = "rpt_emp_CV";
            string path = $"{_webHostEnvironment.WebRootPath}\\Report\\Dg_EmployeeInfo.rdlc";
            string imgPath = new Uri($"{_webHostEnvironment.WebRootPath}\\EmployeeImage\\").AbsoluteUri;
            ReportParameterCollection reportParameters = new ReportParameterCollection
            {
                new ReportParameter("EmpImagePath",imgPath),
                new ReportParameter("PrintUser",ReportTitle.PrintUser)
            };
            byte[] reportBytes = _dgCommon.GenerateReport(data, dataset, path, reportType, reportParameters);
            return reportBytes;
        }
        //public byte[] Dg_EmployeeWiseDetailedInformation(string reportType) //ok
        //{
        //    var data = _sqlCommon.get_InformationDataTable("Dg_Rep_emp_CV", _connection);
        //    string dataset = "rpt_emp_CV";
        //    string path = $"{_webHostEnvironment.WebRootPath}\\Report\\Dg_EmployeeWiseDetailedInformation.rdlc";
        //    string imgPath = new Uri($"{_webHostEnvironment.WebRootPath}\\EmployeeImage\\").AbsoluteUri;
        //    ReportParameterCollection reportParameters = new ReportParameterCollection
        //    {
        //        new ReportParameter("EmpImagePath",imgPath),
        //    };
        //    byte[] reportBytes = _dgCommon.GenerateReport(data, dataset, path, reportType, reportParameters);
        //    return reportBytes;
        //}
        public byte[] Dg_Emp_TiffinBillStatus(string reportType) //ok
        {
            var ReportTitle = this.ReportTitle("dg_print_employeelist", _connection);
            var data = _dgCommon.get_InformationDataTable("dg_rpt_tiffinbill_status", _connection);
            string dataset = "DataSet1";
            string path = $"{_webHostEnvironment.WebRootPath}\\Report\\Dg_EmpTiffinBillStatus.rdlc";
            ReportParameterCollection reportParameters = new ReportParameterCollection
            {
                new ReportParameter("PrintUser",ReportTitle.PrintUser),
                new ReportParameter("Title",string.Concat("Employee Tiffin Bill Status - ",ReportTitle.DepartmentName,ReportTitle.SectionName,ReportTitle.BuildingName,ReportTitle.LineName)),
            };
            byte[] reportBytes = _dgCommon.GenerateReport(data, dataset, path, reportType, reportParameters);
            return reportBytes;
        }
        public byte[] Dg_Emp_NightBillStatus(string reportType) //ok
        {
            var ReportTitle = this.ReportTitle("dg_print_employeelist", _connection);
            var data = _dgCommon.get_InformationDataTable("dg_rpt_nightbill_status", _connection);
            string dataset = "DataSet1";
            string path = $"{_webHostEnvironment.WebRootPath}\\Report\\Dg_EmpNightBillStatus.rdlc";
            ReportParameterCollection reportParameters = new ReportParameterCollection
            {
                new ReportParameter("PrintUser",ReportTitle.PrintUser),
                new ReportParameter("Title",string.Concat("Employee Night Bill Status - ",ReportTitle.DepartmentName,ReportTitle.SectionName,ReportTitle.BuildingName,ReportTitle.LineName)),
            };
            byte[] reportBytes = _dgCommon.GenerateReport(data, dataset, path, reportType, reportParameters);
            return reportBytes;
        }
        public byte[] Export_Report_Employee_shiftchange_history(string reportType)
        {
            var ReportTitle = this.ReportTitle("dg_print_employeelist", _connection);
            var data = _dgCommon.get_InformationDataTable("Dg_Pay_Rep_Shift_change_history", _connection);
            string dataset = "DataSet1";
            string path = $"{_webHostEnvironment.WebRootPath}\\Report\\dg_emp_shiftchange_history.rdlc";
            ReportParameterCollection reportParameters = new ReportParameterCollection
            {
                new ReportParameter("PrintUser",ReportTitle.PrintUser),
                new ReportParameter("Title",string.Concat("Employee shiftchange history - ",ReportTitle.DepartmentName,ReportTitle.SectionName,ReportTitle.BuildingName,ReportTitle.LineName))
            };
            byte[] reportBytes = _dgCommon.GenerateReport(data, dataset, path, reportType, reportParameters);
            return reportBytes;
        }
        public byte[] Export_Report_Employee_shiftchange_history_New(ReportParameterModel obj)
        {
            string empID = string.Empty;
            string salCatID = string.Empty;
            if (obj.empNoFilter.Count > 0)
            {
                var empArr = new List<int>();
                foreach (var item in obj.empNoFilter)
                {
                    if (item.isGet == true)
                    {
                        empArr.Add(item.EmpNo);
                        empID = " and emp_no in(" + string.Join(",", empArr) + ")";
                    }
                    else
                    {
                        empArr.Add(item.EmpNo);
                        empID = " and emp_no not in(" + string.Join(",", empArr) + ")";
                    }
                }
            }
            if (obj.salcat.Length > 0)
            {
                var catList = new List<int>();
                string catParameter = string.Empty;
                foreach (var itemSalCat in obj.salcat)
                {
                    catList.Add(itemSalCat);
                    catParameter = string.Join(",", catList);
                    salCatID = " and oi_salcategory in(" + catParameter + ")";
                }
            }

            var data = _dgCommon.get_InformationDataTable("Dg_Pay_Rep_Shift_change_history_New '" + obj.Compid + "','"+ obj.Department + "','" + obj.section + "','" + obj.Building + "','" + obj.Floor + "','" + obj.Line + "','" + obj.Shift + "','" + obj.Grade + "','" + obj.Start_date + "','" + obj.End_date + "','" + obj.User + "','" + empID + "','" + salCatID + "'", _connection);
            string dataset = "DataSet1";
            string path = $"{_webHostEnvironment.WebRootPath}\\Report\\dg_emp_shiftchange_history.rdlc";
            ReportParameterCollection reportParameters = new ReportParameterCollection
            {
                new ReportParameter("PrintUser",obj.User),
                new ReportParameter("Title",string.Concat("Employee shiftchange history - From : ",Convert.ToDateTime(obj.Start_date).ToString("dd/MMM/yyyy")," To : ",Convert.ToDateTime(obj.End_date).ToString("dd/MMM/yyyy")))
            };
            byte[] reportBytes = _dgCommon.GenerateReport(data, dataset, path, obj.reportType, reportParameters);
            return reportBytes;
        }
        public byte[] EmployeeNominationForm(string reportType)
        {
            var ReportTitle = this.ReportTitle("dg_print_employeelist", _connection);
            var data = _dgCommon.get_InformationDataTable("Dg_Pay_Rep_NominationForm", _connection);
            DataColumn nomineeCol = new DataColumn("IsShowNomineeImg",typeof(bool));
            data.Columns.Add(nomineeCol);
            for (int i = 0; i < data.Rows.Count; i++)
            {
                string nomineeImagePath = $"{_webHostEnvironment.WebRootPath}\\NomineeImage\\" + data.Rows[i]["compid"].ToString() + "\\" + data.Rows[i]["emp_no"].ToString() + ".jpg";
                if (File.Exists(nomineeImagePath))
                {
                    data.Rows[i]["IsShowNomineeImg"] = true;
                }
                else
                {
                    data.Rows[i]["IsShowNomineeImg"] = false;
                }
            }
            string dataset = "NominationForm";
            string path = $"{_webHostEnvironment.WebRootPath}\\Report\\Dg_Pay_NominationForm.rdlc";
            string imgPath = new Uri($"{_webHostEnvironment.WebRootPath}\\NomineeImage\\").AbsoluteUri;
            string groupLogo = new Uri($"{_webHostEnvironment.WebRootPath}\\AuthSign\\GroupLogo.png").AbsoluteUri;
            ReportParameterCollection reportParameters = new ReportParameterCollection
            {
                new ReportParameter("EmpImagePath",imgPath),
                new ReportParameter("groupLogo",groupLogo)
            };
            byte[] reportBytes = _dgCommon.GenerateReport(data, dataset, path, reportType, reportParameters);
            return reportBytes;
        }
        public byte[] EmployeeJoiningLetter(string reportType)
        {
            var data = _dgCommon.get_InformationDataTable("dg_pay_rep_JoiningLetter", _connection);
            string dataset = "joiningLater";
            string path = $"{_webHostEnvironment.WebRootPath}\\Report\\Dg_Pay_JoiningLetter_Bangla.rdlc";
            string groupLogoImgPath = new Uri($"{_webHostEnvironment.WebRootPath}\\AuthSign\\GroupLogo.png").AbsoluteUri;
            ReportParameterCollection reportParameters = new ReportParameterCollection
            {
                new ReportParameter("GroupLogo",groupLogoImgPath)
            };
            byte[] reportBytes = _dgCommon.GenerateReport(data, dataset, path, reportType, reportParameters);
            return reportBytes;
        }
        public byte[] EmployeeJobApplication(string reportType)
        {
            var data = _dgCommon.get_InformationDataTable("dg_pay_rep_JobApplication", _connection);
            string dataset = "jobApplicationBN";
            string path = $"{_webHostEnvironment.WebRootPath}\\Report\\Dg_Pay_JobApplicationForm_Bangla.rdlc";
            string groupLogoImgPath = new Uri($"{_webHostEnvironment.WebRootPath}\\AuthSign\\GroupLogo.png").AbsoluteUri;
            string empImage = new Uri($"{_webHostEnvironment.WebRootPath}\\EmployeeImage\\").AbsoluteUri;
            ReportParameterCollection reportParameters = new ReportParameterCollection
            {
                new ReportParameter("EmpImage",empImage),
                new ReportParameter("GroupLogo",groupLogoImgPath)
            };
            byte[] reportBytes = _dgCommon.GenerateReport(data, dataset, path, reportType, reportParameters);
            return reportBytes;
        }
        public byte[] EmployeeDetailsExcel(string reportType)
        {
            var data = _dgCommon.get_InformationDataTable("Dg_Pay_Rep_EmployeeExcel", _connection);
            string dataset = "EmpDetailsExcel";
            string path = $"{_webHostEnvironment.WebRootPath}\\Report\\Dg_Pay_Rep_EmployeeDetailsExcel.rdlc";
            byte[] reportBytes = _dgCommon.GenerateReport(data, dataset, path, reportType);
            return reportBytes;
        }
        public byte[] EmployeeBackgroundCheck(string reportType)
        {
            var data = _dgCommon.get_InformationDataTable("Dg_Pay_Rep_Emp_background", _connection);
            string dataset = "BackgroundCheckBN";
            string path = $"{_webHostEnvironment.WebRootPath}\\Report\\Dg_Pay_Rep_EmpBackgroundChk.rdlc";
            string groupLogoImgPath = new Uri($"{_webHostEnvironment.WebRootPath}\\AuthSign\\GroupLogo.png").AbsoluteUri;
            ReportParameterCollection reportParameters = new ReportParameterCollection
            {
                new ReportParameter("GroupLogo",groupLogoImgPath)
            };
            byte[] reportBytes = _dgCommon.GenerateReport(data, dataset, path, reportType, reportParameters);
            return reportBytes;
        }
        public byte[] EmployeeIvelatution_From(string reportType)
        {
            var data = _dgCommon.get_InformationDataTable("Dg_Pay_Rep_Emp_the_interview_Ivelatution_From", _connection);
            string dataset = "Ivelatution_From";
            string path = $"{_webHostEnvironment.WebRootPath}\\Report\\Dg_Pay_rep_Emp_Ivelatution_From.rdlc";
            string groupLogoImgPath = new Uri($"{_webHostEnvironment.WebRootPath}\\AuthSign\\GroupLogo.png").AbsoluteUri;
            string authSign = new Uri($"{_webHostEnvironment.WebRootPath}\\AuthSign\\").AbsoluteUri;
            ReportParameterCollection reportParameters = new ReportParameterCollection
            {
                new ReportParameter("GroupLogo",groupLogoImgPath),
                new ReportParameter("AuthSign",authSign)
            };
            byte[] reportBytes = _dgCommon.GenerateReport(data, dataset, path, reportType, reportParameters);
            return reportBytes;
        }

        //Audit
        public byte[] Report_preriodical_present_absent_leave_Weekly_holiday_special_holiday(string reportType)
        {
            var data = _dgCommon.get_InformationDataTable("dg_rpt_excel_Audit_Attendance_Report_preriodical_present_absent_leave_Weekly_holiday_special_holiday", _connection);
            string dataset = "Audit_AttendanceExcel";
            string path = $"{_webHostEnvironment.WebRootPath}\\Report\\Dg_Pay_Rep_AttendanceAudit_Excel.rdlc";
            byte[] reportBytes = _dgCommon.GenerateReport(data, dataset, path, reportType);
            return reportBytes;
        }
        public byte[] Report_Salary_info_Audit_Excel(string reportType)
        {
            var data = _dgCommon.get_InformationDataTable("dg_rpt_excel_Audit_Salary_info", _connection);
            string dataset = "Audit_salary_info";
            string path = $"{_webHostEnvironment.WebRootPath}\\Report\\Dg_Pay_Rep_Salary_info_Audit_Excel.rdlc";
            byte[] reportBytes = _dgCommon.GenerateReport(data, dataset, path, reportType);
            return reportBytes;
        }
        public byte[] Report_Joindate_wise_info_Audit_Excel(string reportType)
        {
            var data = _dgCommon.get_InformationDataTable("dg_rpt_excel_Audit_joindate_wise_emp_info", _connection);
            string dataset = "Audit_Joindate_wise_Empinfo";
            string path = $"{_webHostEnvironment.WebRootPath}\\Report\\Dg_Pay_Rep_joindate_wise_Audit_Excel.rdlc";
            byte[] reportBytes = _dgCommon.GenerateReport(data, dataset, path, reportType);
            return reportBytes;
        }
        public byte[] Report_manual_attendsance_Audit_Excel(string reportType)
        {
            var data = _dgCommon.get_InformationDataTable("dg_rpt_excel_Audit_manual_attendance", _connection);
            string dataset = "Audit_manualAttendance_wise_Empinfo";
            string path = $"{_webHostEnvironment.WebRootPath}\\Report\\Dg_Pay_Rep_manual_attendance_Audit_Excel.rdlc";
            byte[] reportBytes = _dgCommon.GenerateReport(data, dataset, path, reportType);
            return reportBytes;
        }
        public byte[] Report_TiffinBill_Audit_Excel(string reportType)
        {
            var data = _dgCommon.get_InformationDataTable("dg_rpt_excel_Audit_Tiffin_bill_Night_bill_Periodical", _connection);
            string dataset = "Audit_TiffinBill_Empinfo";
            string path = $"{_webHostEnvironment.WebRootPath}\\Report\\Dg_Pay_Rep_Tiffin_Bill_Audit_Excel.rdlc";
            byte[] reportBytes = _dgCommon.GenerateReport(data, dataset, path, reportType);
            return reportBytes;
        }
        public byte[] Report_Night_Audit_Excel(string reportType)
        {
            var data = _dgCommon.get_InformationDataTable("dg_rpt_excel_Audit_Tiffin_bill_Night_bill_Periodical", _connection);
            string dataset = "Audit_TiffinBill_Empinfo";
            string path = $"{_webHostEnvironment.WebRootPath}\\Report\\Dg_Pay_Rep_Night_Bill_Audit_Excel.rdlc";
            byte[] reportBytes = _dgCommon.GenerateReport(data, dataset, path, reportType);
            return reportBytes;
        }
        public byte[] Report_Leave_transction_Audit_Excel(string reportType)
        {
            var data = _dgCommon.get_InformationDataTable("dg_rpt_excel_Audit_leave_transection", _connection);
            string dataset = "Audit_LeaveTransction";
            string path = $"{_webHostEnvironment.WebRootPath}\\Report\\Dg_Pay_Rep_LeaveTransaction_Audit_Excel.rdlc";
            byte[] reportBytes = _dgCommon.GenerateReport(data, dataset, path, reportType);
            return reportBytes;
        }
        public byte[] Report_In_Activeemp_Date_wise_Audit_Excel(string reportType)
        {
            var data = _dgCommon.get_InformationDataTable("dg_rpt_excel_Audit_In_active_emp_info_dateTOdate", _connection);
            string dataset = "Audit_In_active";
            string path = $"{_webHostEnvironment.WebRootPath}\\Report\\Dg_Pay_Rep_In_Active_DatewiseEmp_Audit_Excel.rdlc";
            byte[] reportBytes = _dgCommon.GenerateReport(data, dataset, path, reportType);
            return reportBytes;
        }
        //Audit
        #endregion

        #region"Leave"
        public byte[] Dg_LeaveBalances(string reportType) //ok
        {
            var ReportTitle = this.ReportTitle("dg_print_employeelist_leave",_connection);
            var data = _dgCommon.get_InformationDataTable("Dg_Rep_Lev_Balances", _connection);
            var year = Convert.ToDateTime(ReportTitle.StartDate).Year;
            string dataset = "DataSet1";
            string path = $"{_webHostEnvironment.WebRootPath}\\Report\\Dg_LeaveBalances.rdlc";
            ReportParameterCollection reportParameters = new ReportParameterCollection
            {
                new ReportParameter("PrintUser",ReportTitle.PrintUser),
                new ReportParameter("Title",string.Concat("Leave Balances - ",year," ",ReportTitle.DepartmentName,ReportTitle.SectionName,ReportTitle.BuildingName,ReportTitle.LineName)),
            };
            byte[] reportBytes = _dgCommon.GenerateReport(data, dataset, path, reportType, reportParameters);
            return reportBytes;
        }
        public byte[] Dg_LeaveTransactions(string reportType) //ok
        {
            var ReportTitle = this.ReportTitle("dg_print_employeelist_leave",_connection);
            var data = _dgCommon.get_InformationDataTable("Dg_Rep_Lev_Trans", _connection);
            string dataset = "LeaveTrans";
            string path = $"{_webHostEnvironment.WebRootPath}\\Report\\Dg_LeaveTransactions.rdlc";
            ReportParameterCollection reportParameters = new ReportParameterCollection
            {
                new ReportParameter("PrintUser",ReportTitle.PrintUser),
                new ReportParameter("Title",string.Concat("Leave Transactions - ",ReportTitle.StartDate," To ",ReportTitle.EndDate," ",ReportTitle.DepartmentName,ReportTitle.SectionName,ReportTitle.BuildingName,ReportTitle.LineName)),
            };
            byte[] reportBytes = _dgCommon.GenerateReport(data, dataset, path, reportType, reportParameters);
            return reportBytes;
        }
        public byte[] Dg_IndividualLeaveStatement(string reportType) //ok
        {
            var ReportTitle = this.ReportTitle("dg_print_employeelist_leave",_connection);
            var data = _dgCommon.get_InformationDataTable("Dg_Rep_Lev_Balances", _connection);
            string dataset = "LeaveBal";
            string path = $"{_webHostEnvironment.WebRootPath}\\Report\\Dg_IndividualLeaveStatement.rdlc";
            ReportParameterCollection reportParameters = new ReportParameterCollection
            {
                new ReportParameter("PrintUser",ReportTitle.PrintUser),
            };
            byte[] reportBytes = _dgCommon.GenerateReport(data, dataset, path, reportType, reportParameters);
            return reportBytes;
        }
        public byte[] Dg_MaternityLeaveList(string reportType) //ok
        {
            var ReportTitle = this.ReportTitle("dg_print_employeelist_leave",_connection);
            var data = _dgCommon.get_InformationDataTable("Dg_Rep_maternity_leave", _connection);
            string dataset = "rpt_maternity_leave";
            string path = $"{_webHostEnvironment.WebRootPath}\\Report\\Dg_MaternityLeaveList.rdlc";
            ReportParameterCollection reportParameters = new ReportParameterCollection
            {
                new ReportParameter("PrintUser",ReportTitle.PrintUser),
                new ReportParameter("Title",string.Concat("Maternity Leave Details- From Date:",ReportTitle.StartDate," End Date:",ReportTitle.EndDate)),
            };
            byte[] reportBytes = _dgCommon.GenerateReport(data, dataset, path, reportType, reportParameters);
            return reportBytes;
        }
        public byte[] Dg_MaternityLeavePayment(string reportType) //ok
        {
            var data = _dgCommon.get_InformationDataTable("Dg_Rep_maternityLeave_payment", _connection);
            string dataset = "maternityLeave_payment";
            string path = $"{_webHostEnvironment.WebRootPath}\\Report\\Dg_MaternityLeavePayment.rdlc";
            byte[] reportBytes = _dgCommon.GenerateReport(data, dataset, path, reportType);
            return reportBytes;
        }
        public byte[] Dg_LeaveForm(string reportType) //ok
        {
            var data = _dgCommon.get_InformationDataTable("Dg_Pay_Rep_Leave_from", _connection);
            string dataset = "leaveForm";
            string path = $"{_webHostEnvironment.WebRootPath}\\Report\\Dg_LeaveForm.rdlc";
            string groupLogoImgPath = new Uri($"{_webHostEnvironment.WebRootPath}\\AuthSign\\GroupLogo.png").AbsoluteUri;
            ReportParameterCollection reportParameters = new ReportParameterCollection
            {
                new ReportParameter("groupLogo",groupLogoImgPath)
            };
            byte[] reportBytes = _dgCommon.GenerateReport(data, dataset, path, reportType, reportParameters);
            return reportBytes;
        }
        #endregion

        #region"Attendance"
        public byte[] CreateReportFile_Attendance_Present(string reportType)
        {
            var ReportTitle = this.ReportTitle("dg_print_employeelist_atttendance", _connection);
            var data = _dgCommon.get_InformationDataTable("Dg_Pay_Rep_Att_InOut", _connection);
            string dataset = "DataSet1";
            string path = $"{_webHostEnvironment.WebRootPath}\\Report\\Att_inout.rdlc";
            ReportParameterCollection reportParameters = new ReportParameterCollection
            {
                new ReportParameter("PrintUser",ReportTitle.PrintUser),
                new ReportParameter("Title",string.Concat("Attendance Present - ",ReportTitle.DepartmentName,ReportTitle.SectionName,ReportTitle.BuildingName,ReportTitle.LineName)),
            };
            byte[] reportBytes = _dgCommon.GenerateReport(data, dataset, path, reportType, reportParameters);
            return reportBytes;
        }
        public byte[] Dg_Att_PresentWithImages(string reportType)
        {
            var ReportTitle = this.ReportTitle("dg_print_employeelist_atttendance", _connection);
            var data = _dgCommon.get_InformationDataTable("Dg_Rep_Att_InOut", _connection);
            string dataset = "Attinout";
            string path = $"{_webHostEnvironment.WebRootPath}\\Report\\Dg_Att_PresentWithImages.rdlc";
            string imgPath = new Uri($"{_webHostEnvironment.WebRootPath}\\EmployeeImage\\").AbsoluteUri;
            ReportParameterCollection reportParameters = new ReportParameterCollection
            {
                new ReportParameter("EmpImagePath",imgPath),
                new ReportParameter("PrintUser",ReportTitle.PrintUser),
                new ReportParameter("Title",string.Concat(ReportTitle.DepartmentName,ReportTitle.SectionName,ReportTitle.BuildingName,ReportTitle.LineName," - From - ",ReportTitle.StartDate," To ",ReportTitle.EndDate)),
            };
            byte[] reportBytes = _dgCommon.GenerateReport(data, dataset, path, reportType, reportParameters);
            return reportBytes;
        }
        public byte[] CreateReportFile_Attendance_Absent(string reportType)
        {
            var ReportTitle = this.ReportTitle("dg_print_employeelist_atttendance", _connection);
            var data = _dgCommon.get_InformationDataTable("Dg_Pay_Rep_Att_Absent", _connection);
            string dataset = "DataSet1";
            string path = $"{_webHostEnvironment.WebRootPath}\\Report\\Attendance_Absent.rdlc";
            ReportParameterCollection reportParameters = new ReportParameterCollection
            {
                new ReportParameter("PrintUser",ReportTitle.PrintUser),
                new ReportParameter("Title",string.Concat("Attendance Absent - ",ReportTitle.DepartmentName,ReportTitle.SectionName,ReportTitle.BuildingName,ReportTitle.LineName)),
            };
            byte[] reportBytes = _dgCommon.GenerateReport(data, dataset, path, reportType, reportParameters);
            return reportBytes;
        }
        public byte[] Dg_Att_AbsentWithImages(string reportType)
        {
            var ReportTitle = this.ReportTitle("dg_print_employeelist_atttendance", _connection);
            var data = _dgCommon.get_InformationDataTable("Dg_Rep_Att_Absent", _connection);
            string dataset = "Attinout";
            string path = $"{_webHostEnvironment.WebRootPath}\\Report\\Dg_Att_AbsentWithImages.rdlc";
            string imgPath = new Uri($"{_webHostEnvironment.WebRootPath}\\EmployeeImage\\").AbsoluteUri;
            ReportParameterCollection reportParameters = new ReportParameterCollection
            {
                new ReportParameter("EmpImagePath",imgPath),
                new ReportParameter("PrintUser",ReportTitle.PrintUser),
                new ReportParameter("Title",string.Concat(ReportTitle.DepartmentName,ReportTitle.SectionName,ReportTitle.BuildingName,ReportTitle.LineName," - From - ",ReportTitle.StartDate," To ",ReportTitle.EndDate)),
            };
            byte[] reportBytes = _dgCommon.GenerateReport(data, dataset, path, reportType, reportParameters);
            return reportBytes;
        }
        public byte[] CreateReportFile_Attendance_Late(string reportType)
        {
            var ReportTitle = this.ReportTitle("dg_print_employeelist_atttendance", _connection);
            var data = _dgCommon.get_InformationDataTable("Dg_Pay_Rep_Att_Late", _connection);
            string dataset = "DataSet1";
            string path = $"{_webHostEnvironment.WebRootPath}\\Report\\Attendance_Late.rdlc";
            ReportParameterCollection reportParameters = new ReportParameterCollection
            {
                new ReportParameter("PrintUser",ReportTitle.PrintUser),
                new ReportParameter("Title",string.Concat("Attendance Late From-",ReportTitle.StartDate," To-",ReportTitle.EndDate))
            };
            byte[] reportBytes = _dgCommon.GenerateReport(data, dataset, path, reportType, reportParameters);
            return reportBytes;
        }
        public byte[] CreateReportFile_Attendance_Innotpunch(string reportType)
        {
            var ReportTitle = this.ReportTitle("dg_print_employeelist_atttendance", _connection);
            var data = _dgCommon.get_InformationDataTable("Dg_Pay_Rep_Att_InTimeNotPunch", _connection); //Dg_Pay_Rep_Att_NoInPunch
            string dataset = "DataSet1";
            string path = $"{_webHostEnvironment.WebRootPath}\\Report\\Attendance_In_Notpunch.rdlc";
            ReportParameterCollection reportParameters = new ReportParameterCollection
            {
                new ReportParameter("PrintUser",ReportTitle.PrintUser),
            };
            byte[] reportBytes = _dgCommon.GenerateReport(data, dataset, path, reportType, reportParameters);
            return reportBytes;
        }
        public byte[] CreateReportFile_Attendance_Outnotpunch(string reportType)
        {
            var ReportTitle = this.ReportTitle("dg_print_employeelist_atttendance", _connection);
            var data = _dgCommon.get_InformationDataTable("Dg_Pay_Rep_Att_OutTimeNotPunch", _connection); //Dg_Pay_Rep_Att_NoOutPunch
            string dataset = "DataSet1";
            string path = $"{_webHostEnvironment.WebRootPath}\\Report\\Attendance_Out_Notpunch.rdlc";
            ReportParameterCollection reportParameters = new ReportParameterCollection
            {
                new ReportParameter("PrintUser",ReportTitle.PrintUser),
            };
            byte[] reportBytes = _dgCommon.GenerateReport(data, dataset, path, reportType, reportParameters);
            return reportBytes;
        }
        public byte[] CreateReportFile_Attendance_InOutnotpunch(string reportType)
        {
            var ReportTitle = this.ReportTitle("dg_print_employeelist_atttendance", _connection);
            var data = _dgCommon.get_InformationDataTable("Dg_Pay_Rep_Att_InOutNotPunch", _connection);
            string dataset = "DataSet1";
            string path = $"{_webHostEnvironment.WebRootPath}\\Report\\Attendance_InOut_Notpunch.rdlc";
            ReportParameterCollection reportParameters = new ReportParameterCollection
            {
                new ReportParameter("PrintUser",ReportTitle.PrintUser),
            };
            byte[] reportBytes = _dgCommon.GenerateReport(data, dataset, path, reportType, reportParameters);
            return reportBytes;
        }
        public byte[] Dg_Att_SectionWiseSummary(string reportType)
        {
            var ReportTitle = this.ReportTitle("dg_print_employeelist_atttendance", _connection);
            var data = _dgCommon.get_InformationDataTable("Dg_Rep_Att_SecSummary", _connection);
            string dataset = "AttSecSum";
            string path = $"{_webHostEnvironment.WebRootPath}\\Report\\Dg_Att_SectionWiseSummary.rdlc";
            ReportParameterCollection reportParameters = new ReportParameterCollection
            {
                new ReportParameter("PrintUser",ReportTitle.PrintUser),
                new ReportParameter("Title",string.Concat("Section wise Attendance Summary - ",ReportTitle.StartDate," - ",ReportTitle.DepartmentName,ReportTitle.SectionName,ReportTitle.BuildingName,ReportTitle.LineName)),
            };
            byte[] reportBytes = _dgCommon.GenerateReport(data, dataset, path, reportType, reportParameters);
            return reportBytes;
        }
        public byte[] Dg_CreateReportFile_Ecard_u1(string reportType)
        {
            var data = _dgCommon.get_InformationDataTable("dg_Ecard_Report", _connection);
            string dataset = "Ecard";
            string path = $"{_webHostEnvironment.WebRootPath}\\Report\\Ecard_u1.rdlc";
            byte[] reportBytes = _dgCommon.GenerateReport(data, dataset, path, reportType);
            return reportBytes;
        }
        public byte[] Dg_CreateReportFile_Ecard_Date_To_Date(string reportType)
        {
            var data = _dgCommon.get_InformationDataTable("dg_Ecard_Report_date_to_date", _connection);
            string dataset = "Ecard";
            string path = $"{_webHostEnvironment.WebRootPath}\\Report\\Ecard_Date_To_Date.rdlc";
            byte[] reportBytes = _dgCommon.GenerateReport(data, dataset, path, reportType);
            return reportBytes;
        }
        public byte[] Dg_CreateReportFile_Ecard_complaince_2hour(string reportType)
        {
            var data = _dgCommon.get_InformationDataTable("dg_Ecard_Report_complaince", _connection);
            string dataset = "Ecard";
            string path = $"{_webHostEnvironment.WebRootPath}\\Report\\Ecard_c.rdlc";
            byte[] reportBytes = _dgCommon.GenerateReport(data, dataset, path, reportType);
            return reportBytes;
        }
        public byte[] Dg_CreateReportFile_Ecard_bayer_4hour(string reportType)
        {
            var data = _dgCommon.get_InformationDataTable("dg_Ecard_Report_bayer", _connection);
            string dataset = "Ecard";
            string path = $"{_webHostEnvironment.WebRootPath}\\Report\\Ecard_b.rdlc";
            byte[] reportBytes = _dgCommon.GenerateReport(data, dataset, path, reportType);
            return reportBytes;
        }
        public byte[] Dg_Att_MonthlyAttendance(string reportType)
        {
            var ReportTitle = this.ReportTitle("dg_print_employeelist_atttendance", _connection);
            var data = _dgCommon.get_InformationDataTable("Dg_Pay_Rep_Att_AttMonthly", _connection);
            string dataset = "AttMonthly";
            string path = $"{_webHostEnvironment.WebRootPath}\\Report\\Dg_Att_MonthlyAttendance.rdlc";
            int num = 1;
            do
            {
                if (!data.Columns.Contains(string.Format("D{0}", num)))
                {
                    data.Columns.Add(string.Format("D{0}", num), typeof(string));
                }
                num = checked(num + 1);
            }
            while (num <= 31);
            ReportParameterCollection reportParameters = new ReportParameterCollection
            {
                new ReportParameter("PrintUser",ReportTitle.PrintUser),
                new ReportParameter("TotDays",Convert.ToString(GetMonthDays(Convert.ToDateTime(ReportTitle.StartDate)))),
                new ReportParameter("Title",string.Concat("Monthly Attendance Summary - ",ReportTitle.StartDate," - ",ReportTitle.DepartmentName,ReportTitle.SectionName,ReportTitle.BuildingName,ReportTitle.LineName)),
            };
            byte[] reportBytes = _dgCommon.GenerateReport(data, dataset, path, reportType, reportParameters);
            return reportBytes;
        }
        public byte[] EmployeeMenualAttnList(string reportType)
        {
            var ReportTitle = this.ReportTitle("dg_print_employeelist_atttendance", _connection);
            var data = _dgCommon.get_InformationDataTable("Dg_Pay_Rep_EmpMenualAtt_List", _connection);
            string dataset = "MenualAttnList";
            string path = $"{_webHostEnvironment.WebRootPath}\\Report\\Dg_Pay_Rep_EmpMenualAtt_List.rdlc";
            ReportParameterCollection reportParameters = new ReportParameterCollection
            {
                new ReportParameter("PrintUser",ReportTitle.PrintUser),
                new ReportParameter("Title",string.Concat("Employee Menual Attendance From ",ReportTitle.StartDate," To ",ReportTitle.EndDate)),
            };
            byte[] reportBytes = _dgCommon.GenerateReport(data, dataset, path, reportType, reportParameters);
            return reportBytes;
        }
        public byte[] Date_wise_total_ot_details(string reportType)
        {
            var ReportTitle = this.ReportTitle("dg_print_employeelist_atttendance", _connection);
            var data = _dgCommon.get_InformationDataTable("Dg_Pay_Rep_Att_OT_Exot_TotalOT_Details", _connection);
            string dataset = "OT_Exot_TotalOT";
            string path = $"{_webHostEnvironment.WebRootPath}\\Report\\Att_Total_OT_details.rdlc";
            ReportParameterCollection reportParameters = new ReportParameterCollection
            {
                new ReportParameter("PrintUser",ReportTitle.PrintUser),
                new ReportParameter("Title",string.Concat("OT Details Excel")),
            };
            byte[] reportBytes = _dgCommon.GenerateReport(data, dataset, path, reportType, reportParameters);
            return reportBytes;
        }
        #endregion

        #region"Salary"
        public byte[] CreateReportFile_salarysheet_D(string reportType)
        {
            var data = _dgCommon.get_InformationDataTable("dg_Pay_Rep_Sal_SalarySheet_D", _connection);
            string dataset = "DataSet1";
            string path = $"{_webHostEnvironment.WebRootPath}\\Report\\Sal_Salarysheet.rdlc";
            byte[] reportBytes = _dgCommon.GenerateReport(data, dataset, path, reportType);
            return reportBytes;
        }
        public byte[] Sal_salarysheet_Details(string reportType)
        {
            var ReportTitle = this.ReportTitle("dg_print_employeelist_salary", _connection);
            var data = _dgCommon.get_InformationDataTable("dg_Pay_Rep_Sal_SalarySheet_D", _connection);
            string dataset = "DataSet1";
            string path = $"{_webHostEnvironment.WebRootPath}\\Report\\Sal_Salarysheet_Details.rdlc";
            ReportParameterCollection reportParameters = new ReportParameterCollection
            {
                new ReportParameter("PrintUser",ReportTitle.PrintUser),
                new ReportParameter("Title",string.Concat("For The Month Of - ",Convert.ToDateTime(ReportTitle.StartDate).ToString("MMMM-yyyy"))),
                new ReportParameter("TotDays",Convert.ToString(GetMonthDays(Convert.ToDateTime(ReportTitle.StartDate)))),
                new ReportParameter("TotWorkDays",Convert.ToString(GetTotWorkingDays(Convert.ToDateTime(ReportTitle.StartDate),ReportTitle.Compid))),
                new ReportParameter("TotWLH",Convert.ToString(GetTotWeeklyHolidays(Convert.ToDateTime(ReportTitle.StartDate),ReportTitle.Compid))),
                new ReportParameter("TotSPH",Convert.ToString(GetTotSpecialHolidays(Convert.ToDateTime(ReportTitle.StartDate),ReportTitle.Compid)))
            };
            byte[] reportBytes = _dgCommon.GenerateReport(data, dataset, path, reportType, reportParameters);
            return reportBytes;
        }
        public byte[] Sal_salarysheet_Details_Excel(string reportType)
        {
            var ReportTitle = this.ReportTitle("dg_print_employeelist_salary", _connection);
            var data = _dgCommon.get_InformationDataTable("dg_Pay_Rep_Sal_SalarySheet_D", _connection);
            string dataset = "SalDetailsExcel";
            string path = $"{_webHostEnvironment.WebRootPath}\\Report\\Sal_Salarysheet_DetailsExcel.rdlc";
            ReportParameterCollection reportParameters = new ReportParameterCollection
            {
                new ReportParameter("PrintUser",ReportTitle.PrintUser),
                new ReportParameter("Title",string.Concat("For The Month Of - ",Convert.ToDateTime(ReportTitle.StartDate).ToString("MMMM-yyyy"))),
                new ReportParameter("TotDays",Convert.ToString(GetMonthDays(Convert.ToDateTime(ReportTitle.StartDate)))),
                new ReportParameter("TotWorkDays",Convert.ToString(GetTotWorkingDays(Convert.ToDateTime(ReportTitle.StartDate),ReportTitle.Compid))),
                new ReportParameter("TotWLH",Convert.ToString(GetTotWeeklyHolidays(Convert.ToDateTime(ReportTitle.StartDate),ReportTitle.Compid))),
                new ReportParameter("TotSPH",Convert.ToString(GetTotSpecialHolidays(Convert.ToDateTime(ReportTitle.StartDate),ReportTitle.Compid)))
            };
            byte[] reportBytes = _dgCommon.GenerateReport(data, dataset, path, reportType, reportParameters);
            return reportBytes;
        }
        public byte[] Sal_salarysheet_DetailsReport(string reportType)
        {
            var ReportTitle = this.ReportTitle("dg_print_employeelist_salary", _connection);
            var data = _dgCommon.get_InformationDataTable("dg_Pay_Rep_Sal_SalarySheet_D", _connection);
            string dataset = "DataSet1";
            string path = $"{_webHostEnvironment.WebRootPath}\\Report\\Sal_Salarysheet_Details_Report.rdlc";
            ReportParameterCollection reportParameters = new ReportParameterCollection
            {
                new ReportParameter("PrintUser",ReportTitle.PrintUser),
                new ReportParameter("Title",string.Concat("For The Month Of - ",Convert.ToDateTime(ReportTitle.StartDate).ToString("MMMM-yyyy"))),
                new ReportParameter("TotDays",Convert.ToString(GetMonthDays(Convert.ToDateTime(ReportTitle.StartDate)))),
                new ReportParameter("TotWorkDays",Convert.ToString(GetTotWorkingDays(Convert.ToDateTime(ReportTitle.StartDate),ReportTitle.Compid))),
                new ReportParameter("TotWLH",Convert.ToString(GetTotWeeklyHolidays(Convert.ToDateTime(ReportTitle.StartDate),ReportTitle.Compid))),
                new ReportParameter("TotSPH",Convert.ToString(GetTotSpecialHolidays(Convert.ToDateTime(ReportTitle.StartDate),ReportTitle.Compid)))
            };
            byte[] reportBytes = _dgCommon.GenerateReport(data, dataset, path, reportType, reportParameters);
            return reportBytes;
        }
        public byte[] Sal_salarysheet_ReportDetails(string reportType)
        {
            var ReportTitle = this.ReportTitle("dg_print_employeelist_salary", _connection);
            var data = _dgCommon.get_InformationDataTable("dg_Pay_Rep_Sal_SalarySheet_D", _connection);
            string dataset = "DataSet1";
            string path = $"{_webHostEnvironment.WebRootPath}\\Report\\Sal_Salarysheet_Report_Details.rdlc";
            ReportParameterCollection reportParameters = new ReportParameterCollection
            {
                new ReportParameter("PrintUser",ReportTitle.PrintUser),
                new ReportParameter("Title",string.Concat("For The Month Of - ",Convert.ToDateTime(ReportTitle.StartDate).ToString("MMMM-yyyy"))),
                new ReportParameter("TotDays",Convert.ToString(GetMonthDays(Convert.ToDateTime(ReportTitle.StartDate)))),
                new ReportParameter("TotWorkDays",Convert.ToString(GetTotWorkingDays(Convert.ToDateTime(ReportTitle.StartDate),ReportTitle.Compid))),
                new ReportParameter("TotWLH",Convert.ToString(GetTotWeeklyHolidays(Convert.ToDateTime(ReportTitle.StartDate),ReportTitle.Compid))),
                new ReportParameter("TotSPH",Convert.ToString(GetTotSpecialHolidays(Convert.ToDateTime(ReportTitle.StartDate),ReportTitle.Compid)))
            };
            byte[] reportBytes = _dgCommon.GenerateReport(data, dataset, path, reportType, reportParameters);
            return reportBytes;
        }
        public byte[] SalaryShert_LineWise_Summary(string reportType)
        {
            var ReportTitle = this.ReportTitle("dg_print_employeelist_salary", _connection);
            var data = _dgCommon.get_InformationDataTable("dg_Pay_Rep_Sal_SalarySheet_summary", _connection);
            string dataset = "DataSet1";
            string path = $"{_webHostEnvironment.WebRootPath}\\Report\\Sal_Salarysheet_LineWise_Summary.rdlc";
            ReportParameterCollection reportParameters = new ReportParameterCollection
            {
                new ReportParameter("PrintUser",ReportTitle.PrintUser),
                new ReportParameter("Title",string.Concat("For The Month Of - ",Convert.ToDateTime(ReportTitle.StartDate).ToString("MMMM-yyyy"))),
                new ReportParameter("TotDays",Convert.ToString(GetMonthDays(Convert.ToDateTime(ReportTitle.StartDate)))),
                new ReportParameter("TotWorkDays",Convert.ToString(GetTotWorkingDays(Convert.ToDateTime(ReportTitle.StartDate),ReportTitle.Compid))),
                new ReportParameter("TotWLH",Convert.ToString(GetTotWeeklyHolidays(Convert.ToDateTime(ReportTitle.StartDate),ReportTitle.Compid))),
                new ReportParameter("TotSPH",Convert.ToString(GetTotSpecialHolidays(Convert.ToDateTime(ReportTitle.StartDate),ReportTitle.Compid)))
            };
            byte[] reportBytes = _dgCommon.GenerateReport(data, dataset, path, reportType, reportParameters);
            return reportBytes;
        }
        public byte[] CreateReportFile_salarysheet_D_53(string reportType)
        {
            var ReportTitle = this.ReportTitle("dg_print_employeelist_salary", _connection);
            var data = _dgCommon.get_InformationDataTable("dg_Pay_Rep_Sal_SalarySheet_D", _connection);
            string dataset = "DataSet1";
            string path = $"{_webHostEnvironment.WebRootPath}\\Report\\Sal_Salarysheet_53.rdlc";
            ReportParameterCollection reportParameters = new ReportParameterCollection
            {
                new ReportParameter("PrintUser",ReportTitle.PrintUser),
                new ReportParameter("Title",string.Concat("Monthly Salary Sheet Month Of - ",Convert.ToDateTime(ReportTitle.StartDate).ToString("MMMM-yyyy")," - ",ReportTitle.DepartmentName,ReportTitle.SectionName,ReportTitle.BuildingName,ReportTitle.LineName)),
                new ReportParameter("TotDays",Convert.ToString(GetMonthDays(Convert.ToDateTime(ReportTitle.StartDate)))),
                new ReportParameter("TotWorkDays",Convert.ToString(GetTotWorkingDays(Convert.ToDateTime(ReportTitle.StartDate),ReportTitle.Compid))),
                new ReportParameter("TotWLH",Convert.ToString(GetTotWeeklyHolidays(Convert.ToDateTime(ReportTitle.StartDate),ReportTitle.Compid))),
                new ReportParameter("TotSPH",Convert.ToString(GetTotSpecialHolidays(Convert.ToDateTime(ReportTitle.StartDate),ReportTitle.Compid)))
            };
            byte[] reportBytes = _dgCommon.GenerateReport(data, dataset, path, reportType, reportParameters);
            return reportBytes;
        }
        public byte[] CreateReportFile_salarysheet_D_53_2(string reportType)
        {
            var ReportTitle = this.ReportTitle("dg_print_employeelist_salary", _connection);
            var data = _dgCommon.get_InformationDataTable("dg_Pay_Rep_Sal_SalarySheet_D", _connection);
            string dataset = "DataSet1";
            string path = $"{_webHostEnvironment.WebRootPath}\\Report\\Sal_Salarysheet_53_2.rdlc";
            ReportParameterCollection reportParameters = new ReportParameterCollection
            {
                new ReportParameter("PrintUser",ReportTitle.PrintUser),
                new ReportParameter("Title",string.Concat("Monthly Salary Sheet Month Of - ",Convert.ToDateTime(ReportTitle.StartDate).ToString("MMMM-yyyy")," - ",ReportTitle.DepartmentName,ReportTitle.SectionName,ReportTitle.BuildingName,ReportTitle.LineName)),
                new ReportParameter("TotDays",Convert.ToString(GetMonthDays(Convert.ToDateTime(ReportTitle.StartDate)))),
                new ReportParameter("TotWorkDays",Convert.ToString(GetTotWorkingDays(Convert.ToDateTime(ReportTitle.StartDate),ReportTitle.Compid))),
                new ReportParameter("TotWLH",Convert.ToString(GetTotWeeklyHolidays(Convert.ToDateTime(ReportTitle.StartDate),ReportTitle.Compid))),
                new ReportParameter("TotSPH",Convert.ToString(GetTotSpecialHolidays(Convert.ToDateTime(ReportTitle.StartDate),ReportTitle.Compid)))
            };
            byte[] reportBytes = _dgCommon.GenerateReport(data, dataset, path, reportType, reportParameters);
            return reportBytes;
        }
        public byte[] CreateReportFile_salarysheet_D_53_4(string reportType)
        {
            var ReportTitle = this.ReportTitle("dg_print_employeelist_salary", _connection);
            var data = _dgCommon.get_InformationDataTable("dg_Pay_Rep_Sal_SalarySheet_D", _connection);
            string dataset = "DataSet1";
            string path = $"{_webHostEnvironment.WebRootPath}\\Report\\Sal_Salarysheet_53_4.rdlc";
            ReportParameterCollection reportParameters = new ReportParameterCollection
            {
                new ReportParameter("PrintUser",ReportTitle.PrintUser),
                new ReportParameter("Title",string.Concat("Monthly Salary Sheet Month Of - ",Convert.ToDateTime(ReportTitle.StartDate).ToString("MMMM-yyyy")," - ",ReportTitle.DepartmentName,ReportTitle.SectionName,ReportTitle.BuildingName,ReportTitle.LineName)),
                new ReportParameter("TotDays",Convert.ToString(GetMonthDays(Convert.ToDateTime(ReportTitle.StartDate)))),
                new ReportParameter("TotWorkDays",Convert.ToString(GetTotWorkingDays(Convert.ToDateTime(ReportTitle.StartDate),ReportTitle.Compid))),
                new ReportParameter("TotWLH",Convert.ToString(GetTotWeeklyHolidays(Convert.ToDateTime(ReportTitle.StartDate),ReportTitle.Compid))),
                new ReportParameter("TotSPH",Convert.ToString(GetTotSpecialHolidays(Convert.ToDateTime(ReportTitle.StartDate),ReportTitle.Compid)))
            };
            byte[] reportBytes = _dgCommon.GenerateReport(data, dataset, path, reportType, reportParameters);
            return reportBytes;
        }
        public byte[] CreateReportFile_Payslip(string reportType)
        {
            var data = _dgCommon.get_InformationDataTable("dg_Rep_Sal_PaySlip", _connection);
            string dataset = "PaySlip";
            string path = $"{_webHostEnvironment.WebRootPath}\\Report\\Sal_PaySlip_Debonir.rdlc";
            byte[] reportBytes = _dgCommon.GenerateReport(data, dataset, path, reportType);
            return reportBytes;
        }
        public byte[] CreateReportFile_Payslip_complaince_2hour(string reportType)
        {
            var data = _dgCommon.get_InformationDataTable("dg_Rep_Sal_PaySlip", _connection);
            string dataset = "PaySlip";
            string path = $"{_webHostEnvironment.WebRootPath}\\Report\\Sal_PaySlip_ReportDetails.rdlc";
            byte[] reportBytes = _dgCommon.GenerateReport(data, dataset, path, reportType);
            return reportBytes;
        }
        public byte[] CreateReportFile_Payslip_bayer_4hour(string reportType)
        {
            var data = _dgCommon.get_InformationDataTable("dg_Rep_Sal_PaySlip", _connection);
            string dataset = "PaySlip";
            string path = $"{_webHostEnvironment.WebRootPath}\\Report\\Sal_PaySlip_Debonirrr.rdlc";
            byte[] reportBytes = _dgCommon.GenerateReport(data, dataset, path, reportType);
            return reportBytes;
        }
        public byte[] CreateReportFile_SalarySheetBank(string reportType)
        {
            var ReportTitle = this.ReportTitle("dg_print_employeelist_salary", _connection);
            var data = _dgCommon.get_InformationDataTable("dg_Pay_Rep_Sal_SalarySheet_Bank", _connection);
            string dataset = "SalarySheet_Bank";
            string path = $"{_webHostEnvironment.WebRootPath}\\Report\\SalarySheet_Bank.rdlc";
            ReportParameterCollection reportParameters = new ReportParameterCollection
            {
                new ReportParameter("PrintUser",ReportTitle.PrintUser),
            };
            byte[] reportBytes = _dgCommon.GenerateReport(data, dataset, path, reportType, reportParameters);
            return reportBytes;
        }
        public byte[] CreateReportFile_SalarySheetEXOT(string reportType)
        {
            var ReportTitle = this.ReportTitle("dg_print_employeelist_salary", _connection);
            var data = _dgCommon.get_InformationDataTable("Dg_Pay_Rep_Att_EXTODetails", _connection);
            string dataset = "EXOT_Sheet";
            string path = $"{_webHostEnvironment.WebRootPath}\\Report\\Sal_EXOT_Sheet.rdlc";
            ReportParameterCollection reportParameters = new ReportParameterCollection
            {
                new ReportParameter("PrintUser",ReportTitle.PrintUser),
                new ReportParameter("Title",string.Concat("EXOT Sheet - ",Convert.ToDateTime(ReportTitle.StartDate).ToString("MMMM-yyyy")," - ",ReportTitle.DepartmentName,ReportTitle.SectionName,ReportTitle.BuildingName,ReportTitle.LineName)),
            };
            byte[] reportBytes = _dgCommon.GenerateReport(data, dataset, path, reportType, reportParameters);
            return reportBytes;
        }
        public byte[] CreateReportFile_SalarySheetOTEXOT(string reportType)
        {
            var ReportTitle = this.ReportTitle("dg_print_employeelist_salary", _connection);
            var data = _dgCommon.get_InformationDataTable("Dg_Pay_Rep_Sal_SalarySheet_D_EXOT", _connection);
            string dataset = "SalSheet";
            string path = $"{_webHostEnvironment.WebRootPath}\\Report\\Sal_OTEXOT_Sheet.rdlc";
            ReportParameterCollection reportParameters = new ReportParameterCollection
            {
                new ReportParameter("PrintUser",ReportTitle.PrintUser),
                new ReportParameter("Title",string.Concat("Salary Sheet - ",Convert.ToDateTime(ReportTitle.StartDate).ToString("MMMM-yyyy")," - ",ReportTitle.DepartmentName,ReportTitle.SectionName,ReportTitle.BuildingName,ReportTitle.LineName)),
                new ReportParameter("TotDays",Convert.ToString(GetMonthDays(Convert.ToDateTime(ReportTitle.StartDate)))),
                new ReportParameter("TotWorkDays",Convert.ToString(GetTotWorkingDays(Convert.ToDateTime(ReportTitle.StartDate),ReportTitle.Compid))),
                new ReportParameter("TotWLH",Convert.ToString(GetTotWeeklyHolidays(Convert.ToDateTime(ReportTitle.StartDate),ReportTitle.Compid))),
                new ReportParameter("TotSPH",Convert.ToString(GetTotSpecialHolidays(Convert.ToDateTime(ReportTitle.StartDate),ReportTitle.Compid)))
            };
            byte[] reportBytes = _dgCommon.GenerateReport(data, dataset, path, reportType, reportParameters);
            return reportBytes;
        }
        public byte[] CreateReportFile_OTDetails(string reportType)
        {
            var ReportTitle = this.ReportTitle("dg_print_employeelist_salary", _connection);
            var data = _dgCommon.get_InformationDataTable("Dg_Pay_Rep_Att_OTDetails", _connection);
            string dataset = "OT";
            string path = $"{_webHostEnvironment.WebRootPath}\\Report\\Sal_OTDetails.rdlc";
            ReportParameterCollection reportParameters = new ReportParameterCollection
            {
                new ReportParameter("PrintUser",ReportTitle.PrintUser),
                new ReportParameter("Title",string.Concat("OT Detailed - ",Convert.ToDateTime(ReportTitle.StartDate).ToString("dd-MMM-yyyy")," - ",ReportTitle.DepartmentName,ReportTitle.SectionName,ReportTitle.BuildingName,ReportTitle.LineName)),         
            };
            byte[] reportBytes = _dgCommon.GenerateReport(data, dataset, path, reportType, reportParameters);
            return reportBytes;
        }
        public byte[] CreateReportFile_OTSummary(string reportType)
        {
            var ReportTitle = this.ReportTitle("dg_print_employeelist_salary", _connection);
            var data = _dgCommon.get_InformationDataTable("Dg_Pay_Rep_Sal_OT", _connection);
            string dataset = "OT";
            string path = $"{_webHostEnvironment.WebRootPath}\\Report\\Sal_OTSummary.rdlc";
            ReportParameterCollection reportParameters = new ReportParameterCollection
            {
                new ReportParameter("PrintUser",ReportTitle.PrintUser),
                new ReportParameter("Title",string.Concat("OT Summary -  Month of ",Convert.ToDateTime(ReportTitle.StartDate).ToString("MMMM-yyyy")," - ",ReportTitle.DepartmentName,ReportTitle.SectionName,ReportTitle.BuildingName,ReportTitle.LineName)),
            };
            byte[] reportBytes = _dgCommon.GenerateReport(data, dataset, path, reportType, reportParameters);
            return reportBytes;
        }
        public byte[] CreateReportFile_SalarySheetSummary(string reportType)
        {
            var ReportTitle = this.ReportTitle("dg_print_employeelist_salary", _connection);
            var data = _dgCommon.get_InformationDataTable("Dg_Pay_Rep_Sal_SalarySheet_S", _connection);
            string dataset = "DataSet1";
            string path = $"{_webHostEnvironment.WebRootPath}\\Report\\SalarySheet_Summary.rdlc";
            ReportParameterCollection reportParameters = new ReportParameterCollection
            {
                new ReportParameter("PrintUser",ReportTitle.PrintUser),
                new ReportParameter("Title",string.Concat("Salary Summary for Month of ",Convert.ToDateTime(ReportTitle.StartDate).ToString("MMMM-yyyy")," - ",ReportTitle.DepartmentName,ReportTitle.SectionName,ReportTitle.BuildingName,ReportTitle.LineName)),
            };
            byte[] reportBytes = _dgCommon.GenerateReport(data, dataset, path, reportType, reportParameters);
            return reportBytes;
        }
        public byte[] CreateReportFile_SalarySheetOtExotSummary(string reportType)
        {
            var ReportTitle = this.ReportTitle("dg_print_employeelist_salary", _connection);
            var data = _dgCommon.get_InformationDataTable("Dg_Pay_Rep_Sal_SalarySheet_S_OT_EXOT", _connection);
            string dataset = "DataSet1";
            string path = $"{_webHostEnvironment.WebRootPath}\\Report\\SalarySheet_OTEXOTSum_Summary.rdlc";
            ReportParameterCollection reportParameters = new ReportParameterCollection
            {
                new ReportParameter("PrintUser",ReportTitle.PrintUser),
                new ReportParameter("Title",string.Concat("OT And EXOT Salary Summary for Month of ",Convert.ToDateTime(ReportTitle.StartDate).ToString("MMMM-yyyy")," - ",ReportTitle.DepartmentName,ReportTitle.SectionName,ReportTitle.BuildingName,ReportTitle.LineName)),
            };
            byte[] reportBytes = _dgCommon.GenerateReport(data, dataset, path, reportType, reportParameters);
            return reportBytes;
        }
        public byte[] Sal_SalarySheetBankExcel(string reportType)
        {
            var ReportTitle = this.ReportTitle("dg_print_employeelist_salary", _connection);
            var data = _dgCommon.get_InformationDataTable("Dg_Pay_Rep_SalarySheetBankFor_Excel", _connection);
            string dataset = "SalBankExcel";
            string path = $"{_webHostEnvironment.WebRootPath}\\Report\\Sal_SalarySheetBank_Excel.rdlc";
            ReportParameterCollection reportParameters = new ReportParameterCollection
            {
                new ReportParameter("PrintUser",ReportTitle.PrintUser),
                new ReportParameter("Title",string.Concat("Bank List for the Month of ",Convert.ToDateTime(ReportTitle.StartDate).ToString("MMMM-yyyy"))),
            };
            byte[] reportBytes = _dgCommon.GenerateReport(data, dataset, path, "excel", reportParameters);
            return reportBytes;
        }
        public byte[] Sal_EmployeeTiffinBillSummary(string reportType)
        {
            var ReportTitle = this.ReportTitle("dg_print_employeelist_salary", _connection);
            var data = _dgCommon.get_InformationDataTable("Dg_Pay_Rep_EmployeeTiffinBill_Summary", _connection);
            string dataset = "TiffinBillSum";
            string path = $"{_webHostEnvironment.WebRootPath}\\Report\\Sal_TiffinBillSummary.rdlc";
            ReportParameterCollection reportParameters = new ReportParameterCollection
            {
                new ReportParameter("PrintUser",ReportTitle.PrintUser),
                new ReportParameter("Title",string.Concat("Tiffin Bill Summary for Month of ",Convert.ToDateTime(ReportTitle.StartDate).ToString("MMMM-yyyy"))),
            };
            byte[] reportBytes = _dgCommon.GenerateReport(data, dataset, path, reportType, reportParameters);
            return reportBytes;
        }
        public byte[] Sal_EmployeeNightBillSummary(string reportType)
        {
            var ReportTitle = this.ReportTitle("dg_print_employeelist_salary", _connection);
            var data = _dgCommon.get_InformationDataTable("Dg_Pay_Rep_EmployeeNightBill_Summary", _connection);
            string dataset = "NightBillSum";
            string path = $"{_webHostEnvironment.WebRootPath}\\Report\\Sal_NightBillSummary.rdlc";
            ReportParameterCollection reportParameters = new ReportParameterCollection
            {
                new ReportParameter("PrintUser",ReportTitle.PrintUser),
                new ReportParameter("Title",string.Concat("Night Bill Summary for Month of ",Convert.ToDateTime(ReportTitle.StartDate).ToString("MMMM-yyyy"))),
            };
            byte[] reportBytes = _dgCommon.GenerateReport(data, dataset, path, reportType, reportParameters);
            return reportBytes;
        }
        public byte[] Sal_EmployeeTiffinBillAmount(string reportType)
        {
            var ReportTitle = this.ReportTitle("dg_print_employeelist_salary", _connection);
            var data = _dgCommon.get_InformationDataTable("Dg_Pay_Rep_EmpWiseTiffinBillAmt", _connection);
            string dataset = "TiffinBillAmt";
            string path = $"{_webHostEnvironment.WebRootPath}\\Report\\Sal_EmpTiffinBillAmt.rdlc";
            ReportParameterCollection reportParameters = new ReportParameterCollection
            {
                new ReportParameter("PrintUser",ReportTitle.PrintUser),
                new ReportParameter("Title",string.Concat("Employee Wise Tiffin Bill for Month of ",Convert.ToDateTime(ReportTitle.StartDate).ToString("MMMM-yyyy"))),
            };
            byte[] reportBytes = _dgCommon.GenerateReport(data, dataset, path, reportType, reportParameters);
            return reportBytes;
        }
        public byte[] Sal_EmployeeNightBillAmount(string reportType)
        {
            var ReportTitle = this.ReportTitle("dg_print_employeelist_salary", _connection);
            var data = _dgCommon.get_InformationDataTable("Dg_Pay_Rep_EmpWiseNightBillAmt", _connection);
            string dataset = "NightBillAmt";
            string path = $"{_webHostEnvironment.WebRootPath}\\Report\\Sal_EmpNightBillAmt.rdlc";
            ReportParameterCollection reportParameters = new ReportParameterCollection
            {
                new ReportParameter("PrintUser",ReportTitle.PrintUser),
                new ReportParameter("Title",string.Concat("Employee Wise Night Bill for Month of ",Convert.ToDateTime(ReportTitle.StartDate).ToString("MMMM-yyyy"))),
            };
            byte[] reportBytes = _dgCommon.GenerateReport(data, dataset, path, reportType, reportParameters);
            return reportBytes;
        }
        #endregion

        #region"Others"
        public byte[] CreateReportFile_salarysheet_D_Bank(string reportType)
        {
            var data = _dgCommon.get_InformationDataTable("dg_Pay_Rep_Sal_SalarySheet_D", _connection);
            string dataset = "DataSet1";
            string path = $"{_webHostEnvironment.WebRootPath}\\Report\\SalarySheet_Bank_DG.rdlc";
            byte[] reportBytes = _dgCommon.GenerateReport(data, dataset, path, reportType);
            return reportBytes;
        }
        public byte[] CreateReportFile_Ecard(string reportType)
        {
            var data = _dgCommon.get_InformationDataTable("dg_Ecard_Report", _connection);
            string dataset = "Ecard";
            string path = $"{_webHostEnvironment.WebRootPath}\\Report\\Ecard.rdlc";
            byte[] reportBytes = _dgCommon.GenerateReport(data, dataset, path, reportType);
            return reportBytes;
        }
        public byte[] CreateReportFile_Payslip_U1(string reportType)
        {
            var data = _dgCommon.get_InformationDataTable("dg_Rep_Sal_PaySlip", _connection);
            string dataset = "PaySlip";
            string path = $"{_webHostEnvironment.WebRootPath}\\Report\\Sal_PaySlip_Debonir_U1.rdlc";
            byte[] reportBytes = _dgCommon.GenerateReport(data, dataset, path, reportType);
            return reportBytes;
        }
        public byte[] Export_Report_SalarySheet(string reportType)
        {
            var data = _dgCommon.get_InformationDataTable("dg_Pay_Rep_Sal_SalarySheet_D", _connection);
            var data2 = _dgCommon.get_InformationDataTable("select com_name from dg_pay_company", _connection);
            string dataset = "DataSet1";
            string path = $"{_webHostEnvironment.WebRootPath}\\Report\\Sal_Salarysheet_U1.rdlc";
            ReportParameterCollection reportParameters = new ReportParameterCollection
            {
                new ReportParameter("comp",data2.Rows[0]["com_name"].ToString()),

            };
            byte[] reportBytes = _dgCommon.GenerateReport(data, dataset, path, reportType, reportParameters);
            return reportBytes;
        }
        public byte[] testBarcode(string reportType)
        {
            DataTable data = new DataTable();
            DataColumn newCol = new DataColumn("BarcodeGen", typeof(byte[]));
            DataColumn newCol1 = new DataColumn("sl_no", typeof(int));
            newCol.AllowDBNull = true;
            data.Columns.Add(newCol);
            data.Columns.Add(newCol1);
            for (int i = 0; i < 1000; i++)
            {
                var row = data.NewRow();
                row["BarcodeGen"] = this.barcodeGenerator("12345");
                row["sl_no"] = i;
                data.Rows.Add(row);
            }
            string dataset = "testBarcode";
            string path = $"{_webHostEnvironment.WebRootPath}\\Report\\testBarcode.rdlc";
            byte[] reportBytes = _dgCommon.GenerateReport(data, dataset, path, reportType);
            return reportBytes;
        }
        #endregion



        //For Report Title Details
        private Dg_ReportTitle ReportTitle(string tableName, SqlConnection sqlCon)
        {
            Dg_ReportTitle titleList;
            DataTable dt = _dgCommon.get_InformationDataTable("select * from " + tableName + "", sqlCon);
            if (dt.Rows.Count > 0)
            {
                titleList = new Dg_ReportTitle()
                {
                    Compid = Convert.ToInt32(dt.Rows[0]["pl_compid"]),
                    DepartmentName = !string.IsNullOrEmpty(dt.Rows[0]["pl_department_name"].ToString()) ? "Department : " + dt.Rows[0]["pl_department_name"].ToString() + "" : "ALL",
                    SectionName = !string.IsNullOrEmpty(dt.Rows[0]["pl_section_name"].ToString()) ? ", Section : " + dt.Rows[0]["pl_section_name"].ToString() + "" : string.Empty,
                    BuildingName = !string.IsNullOrEmpty(dt.Rows[0]["pl_building_name"].ToString()) ? ", Building : " + dt.Rows[0]["pl_building_name"].ToString() + "" : string.Empty,
                    FloorName = !string.IsNullOrEmpty(dt.Rows[0]["pl_floor_name"].ToString()) ? ", Floor : " + dt.Rows[0]["pl_floor_name"].ToString() + "" : string.Empty,
                    LineName = !string.IsNullOrEmpty(dt.Rows[0]["pl_line_name"].ToString()) ? ", Line : " + dt.Rows[0]["pl_line_name"].ToString() + "" : string.Empty,
                    ShiftName = !string.IsNullOrEmpty(dt.Rows[0]["pl_shift_name"].ToString()) ? ", Shift : " + dt.Rows[0]["pl_shift_name"].ToString() + "" : string.Empty,
                    SalcatName = !string.IsNullOrEmpty(dt.Rows[0]["pl_salcat_name"].ToString()) ? "Salary Category : " + dt.Rows[0]["pl_salcat_name"].ToString() + "" : string.Empty,
                    StartDate = Convert.ToDateTime(dt.Rows[0]["pl_Startdate"]).ToString("dd-MMM-yyyy"),
                    EndDate = Convert.ToDateTime(dt.Rows[0]["pl_Enddate"]).ToString("dd-MMM-yyyy"),
                    PrintUser = dt.Rows[0]["pl_user"].ToString()
                };
                return titleList;
            }           
            return null;
        }
        private int GetMonthDays(DateTime xDate)
        {
            return DateTime.DaysInMonth(xDate.Year, xDate.Month);
        }
        private int GetTotWorkingDays(DateTime xDate, int CompID)
        {
            int num;
            DateTime.DaysInMonth(xDate.Year, xDate.Month);
            string[] str = new string[] { "SELECT COUNT(distinct sh_date) AS SH FROM dg_pay_specialholidays_empWise WHERE month(sh_date) =", Convert.ToString(xDate.Month), " AND YEAR(sh_date) =", Convert.ToString(xDate.Year), " AND sh_compid =", Convert.ToString(CompID) };
            SqlCommand sqlCommand = new SqlCommand(string.Concat(str), _connection);
            _connection.Open();
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            sqlDataReader.Read();
            num = (sqlDataReader.IsDBNull(0) ? 0 : Convert.ToInt32(sqlDataReader[0]));
            _connection.Close();
            int num1 = 0;
            int num2 = DateTime.DaysInMonth(xDate.Year, xDate.Month);
            for (int i = 1; i <= num2; i = checked(i + 1))
            {
                str = new string[] { i.ToString(), "/", Convert.ToString(xDate.Month), "/", Convert.ToString(xDate.Year) };
                if (DateTime.ParseExact(string.Concat(str), "d/M/yyyy", null).DayOfWeek == DayOfWeek.Friday)
                {
                    num1 = checked(num1 + 1);
                }
            }
            int num3 = checked(checked(DateTime.DaysInMonth(xDate.Year, xDate.Month) - (checked(num + num1))) + GetCoveringDays(xDate, CompID));
            return num3;
        }
        private int GetTotWeeklyHolidays(DateTime xDate, int CompID)
        {
            DateTime.DaysInMonth(xDate.Year, xDate.Month);
            int num = 0;
            int num1 = DateTime.DaysInMonth(xDate.Year, xDate.Month);
            for (int i = 1; i <= num1; i = checked(i + 1))
            {
                string[] str = new string[] { i.ToString(), "/", Convert.ToString(xDate.Month), "/", Convert.ToString(xDate.Year) };
                if (DateTime.ParseExact(string.Concat(str), "d/M/yyyy", null).DayOfWeek == DayOfWeek.Friday)
                {
                    num = checked(num + 1);
                }
            }
            return checked(num - GetCoveringDays(xDate, CompID));
        }
        private int GetTotSpecialHolidays(DateTime xDate, int CompID)
        {
            int num;
            DateTime.DaysInMonth(xDate.Year, xDate.Month);
            string[] str = new string[] { "SELECT COUNT(distinct sh_date) AS SH FROM dg_pay_specialholidays_empWise WHERE month(sh_date) =", Convert.ToString(xDate.Month), " AND YEAR(sh_date) =", Convert.ToString(xDate.Year), " AND sh_compid =", Convert.ToString(CompID) };
            SqlCommand sqlCommand = new SqlCommand(string.Concat(str), _connection);
            _connection.Open();
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            sqlDataReader.Read();
            num = (sqlDataReader.IsDBNull(0) ? 0 : Convert.ToInt32(sqlDataReader[0]));
            _connection.Close();
            return num;
        }        
        private int GetCoveringDays(DateTime xDate, int Compid)
        {
            int num;
            string[] str = new string[] { "SELECT COUNT(distinct cd_covDate) AS CD FROM dg_pay_attcovering_days_empWise WHERE month(cd_covDate) =month('", Convert.ToString(xDate), "') and year(cd_covDate) =year('", Convert.ToString(xDate), "') and cd_compid =", Convert.ToString(Compid) };
            SqlCommand sqlCommand = new SqlCommand(string.Concat(str), _connection);
            _connection.Open();
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            sqlDataReader.Read();
            num = (sqlDataReader.IsDBNull(0) ? 0 : Convert.ToInt32(sqlDataReader[0]));
            _connection.Close();
            return num;
        }

        private byte[] barcodeGenerator(string bData)
        {
            Barcode barcode = new Barcode();
            Image img = barcode.Encode(TYPE.CODE128, bData, Color.Black, Color.White, 300, 100);
            using (MemoryStream ms = new MemoryStream())
            {
                img.Save(ms, ImageFormat.Jpeg);
                byte[] imageBytes = ms.ToArray();
                return imageBytes;
            }
        }
        private void AddDataColumnWithBarcode(DataTable dt, int columnIndex)
        {
            DataColumn[] newCol = new DataColumn[]
            {
                new DataColumn("BarcodeGen", typeof(byte[])),
                new DataColumn("IsShowEmpSign", typeof(bool)),
                new DataColumn("IsShowAuthSign", typeof(bool))
            };
            dt.Columns.AddRange(newCol);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string imagePathEmp = $"{_webHostEnvironment.WebRootPath}\\EmployeeSignature\\" + dt.Rows[i]["compid"].ToString() + "\\" + dt.Rows[i]["emp_no"].ToString() + ".png";
                if (File.Exists(imagePathEmp))
                {
                    dt.Rows[i]["IsShowEmpSign"] = true;
                }
                else
                {
                    dt.Rows[i]["IsShowEmpSign"] = false;
                }
                string imagePathAuth = $"{_webHostEnvironment.WebRootPath}\\AuthSign\\" + dt.Rows[i]["compid"].ToString() + ".jpg";
                if (File.Exists(imagePathAuth))
                {
                    dt.Rows[i]["IsShowAuthSign"] = true;
                }
                else
                {
                    dt.Rows[i]["IsShowAuthSign"] = false;
                }
                Barcode barcode = new Barcode();
                Image img = barcode.Encode(TYPE.CODE128, dt.Rows[i][columnIndex].ToString(), Color.Black, Color.White, 300, 100);
                using (MemoryStream ms = new MemoryStream())
                {
                    img.Save(ms, ImageFormat.Jpeg);
                    byte[] imageBytes = ms.ToArray();
                    dt.Rows[i]["BarcodeGen"] = imageBytes;
                }
            }
        }
        //private void isShowImage(DataTable dt,DataColumn[] columnName,string imagePath)
        //{
        //    if (dt.Rows.Count > 0)
        //    {
        //        dt.Columns.AddRange(columnName);
        //        for (int i = 0; i < dt.Rows.Count; i++)
        //        {
        //            if (File.Exists(imagePath))
        //            {

        //            }
        //        }
        //    }
        //}
    }
}