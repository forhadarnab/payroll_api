using BLL.Interfaces.Manager.WeeklyHolidaySetup;
using BLL.Utility;
using BOL.Models;
using System.Data.SqlClient;

namespace DAL.Implementation.Manager.WeeklyHolidaySetup
{
    public class WeeklyHolidaySetupManager : IWeeklyHolidaySetupManager
    {
        private readonly Dg_Common _dgCommon;
        private readonly SqlConnection _connection;
        public WeeklyHolidaySetupManager(Dg_Common dgCommon)
        {
            _dgCommon = dgCommon;
            _connection = new SqlConnection(Getway.Dg_Payroll);
        }

        public List<ReturnObject> SaveWeeklyHolidaySetup(WeeklyHolidayPayload obj)
        {
            var result = new List<ReturnObject>();
            try
            {                              
                if (obj.employeeBasic.Count > 0)
                {
                    foreach (var employee in obj.employeeBasic)
                    {
                        if(obj.whType== "Temporary")
                        {
                            if (obj.holiDate.Length > 0)
                            {
                                _dgCommon.saveChanges("DELETE dg_weekly_holiday_setup WHERE wh_emp_serial=" + employee.employeeSL + " AND wh_Type='Temporary' AND wh_month=" + Convert.ToInt32(Convert.ToDateTime(obj.monthYear).Month) + " AND wh_year=" + Convert.ToInt32(Convert.ToDateTime(obj.monthYear).Year), _connection);
                                foreach (var hDate in obj.holiDate)
                                {
                                    bool isSave = _dgCommon.saveChanges("Dg_Pay_SaveHolidaySetup " + employee.companyID + "," + employee.employeeNO + "," + employee.employeeSL + ",'" + hDate + "','" + obj.monthYear + "','" + obj.whType + "','" + obj.fixedDayName + "','" + obj.userName + "'", _connection);
                                    if (isSave)
                                    {
                                        result.Add(new ReturnObject
                                        {
                                            IsSuccess = true,
                                            Message = "Employee(" + employee.employeeNO + ") Date(" + Convert.ToDateTime(hDate).ToString("dd/MMM/yyyy") + ") Holiday Setup Successfully !!",
                                        });
                                    }
                                    else
                                    {
                                        result.Add(new ReturnObject
                                        {
                                            IsSuccess = false,
                                            Message = "Employee(" + employee.employeeNO + ") Date(" + Convert.ToDateTime(hDate).ToString("dd/MMM/yyyy") + ") Holiday Setup Failed !!",
                                        });
                                    }
                                }
                            }
                            else
                            {

                                result.Add(new ReturnObject
                                {
                                    IsSuccess = false,
                                    Message = "You Can Not Enter Any Weekly Holiday Date !!",
                                });
                            }
                        }
                        else
                        {
                            bool isSave = _dgCommon.saveChanges("Dg_Pay_SaveHolidaySetup " + employee.companyID + "," + employee.employeeNO + "," + employee.employeeSL + ",'','" + obj.monthYear + "','" + obj.whType + "','" + obj.fixedDayName + "','" + obj.userName + "'", _connection);
                            if (isSave)
                            {
                                result.Add(new ReturnObject
                                {
                                    IsSuccess = true,
                                    Message = "Employee(" + employee.employeeNO + ") " + obj.fixedDayName + " Holiday Setup Successfully !!",
                                });
                            }
                            else
                            {
                                result.Add(new ReturnObject
                                {
                                    IsSuccess = false,
                                    Message = "Employee(" + employee.employeeNO + ") " + obj.fixedDayName + " Holiday Setup Failed !!",
                                });
                            }
                        }
                    }
                }
                else
                {
                    result.Add(new ReturnObject
                    {
                        IsSuccess = false,
                        Message = "You Can Not Check Any Items !!",
                    });
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                result.Add(new ReturnObject
                {
                    IsSuccess = false,
                    Message = "Something Went Wrong !!",
                });
            }
            return result;
        }
        public async Task<ReturnObject> GetWeeklyHoliday(string monthYear, int empSerial)
        {
            var result = new ReturnObject();
            try
            {
                var data = await _dgCommon.get_InformationDataTableAsync("Dg_Pay_GetEmployeeWeeklyHoliday '"+ monthYear + "',"+ empSerial, _connection);
                if(data.Rows.Count > 0)
                {
                    result.IsSuccess = true;
                    result.Message = "Data Loaded !!";
                    result.dataTable = data;
                }
                else
                {
                    result.IsSuccess = false;
                    result.Message = "Data Not Found !!";
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
