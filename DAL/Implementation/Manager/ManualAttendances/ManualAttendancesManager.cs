using BLL.Interfaces.Manager.ManualAttendances;
using BLL.Utility;
using BOL.Models;
using DAL.Data;
using DAL.Implementation.Repository.ManualAttendances;
using EF.Core.Repository.Manager;
using System.Data;
using System.Data.SqlClient;

namespace DAL.Implementation.Manager.ManualAttendances
{
    public class ManualAttendancesManager : CommonManager<Attendance_DbModel>,IManualAttendancesManager
    {
        private readonly Dg_Common _dgCommon;
        private readonly SqlConnection _connection;
        public ManualAttendancesManager(dg_hrpayrollContext context, Dg_Common dgCommon) : base(new ManualAttendancesRepository(context))
        {
            _dgCommon = dgCommon;
            _connection = new SqlConnection(Getway.Dg_Payroll);
        }

        public async Task<DataSet> ManualAttendance(int Emp_serial, DateTime date, decimal intime, DateTime outdate, decimal outtime)
        {
            var data = await _dgCommon.get_InformationDtasetAsync("D_Manual_Attendance "+ Emp_serial + ",'"+ date + "',"+ intime + ",'"+ outdate + "',"+ outtime + "", _connection);
            return data;
        }
        public async Task<DataSet> ManualAttendance_select(int Emp_serial, DateTime date)
        {
            var data = await _dgCommon.get_InformationDtasetAsync("Manual_Attendance_select_DATE_AND_IDWISE "+ Emp_serial + ",'"+ date + "'", _connection);
            return data;
        }
        public async Task<DataSet> ManualAttendance_viw(int comp, DateTime Sdate, DateTime Edate, int IND)
        {
            var data = await _dgCommon.get_InformationDtasetAsync("D_Manual_Attendance_VIW "+ comp + ",'"+ Sdate + "','"+ Edate + "',"+ IND + "", _connection);
            return data;
        }
        public async Task<DataSet> ManualAttendancefilter(int? Compid = null, int? Department = null, int? section = null, int? Building = null, int? Floor = null, int? Line = null)
        {
            var data = await _dgCommon.get_InformationDtasetAsync("Emp_filtering_manual_attendance "+ Compid + ","+ Department + ","+ section + ","+ Building + ","+ Floor + ","+ Line + "", _connection);
            return data;
        }
        public async Task<DataSet> shiftlist(int CompID)
        {
            var data = await _dgCommon.get_InformationDtasetAsync("Shift_list "+ CompID, _connection);
            return data;
        }
        public async Task<List<ManualAttSeletedList>> GetManualAttList(List<ManualAttPara> para)
        {
            var addList = new List<ManualAttSeletedList>();
            try
            {
                foreach (ManualAttPara item in para)
                {
                    var dt = await _dgCommon.get_InformationDataTableAsync("Emp_list_fromdatetodate_wise '" + item.emp_serial + "','" + item.fromDate + "','" + item.todate + "','" + item.weeklyholiday + "','" + item.leave + "'", _connection);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        addList.Add(new ManualAttSeletedList
                        {
                            compid = Convert.ToInt32(dt.Rows[i]["compid"]),
                            comp_name = dt.Rows[i]["comp_name"].ToString(),
                            emp_serial = Convert.ToInt32(dt.Rows[i]["emp_serial"]),
                            emp_no = Convert.ToInt32(dt.Rows[i]["emp_no"]),
                            emp_proxid = Convert.ToInt32(dt.Rows[i]["emp_proxid"]),
                            pi_fullname = dt.Rows[i]["pi_fullname"].ToString(),
                            //sh_code = Convert.ToInt32(dt.Rows[i]["sh_code"]),
                            sh_name = dt.Rows[i]["sh_name"].ToString(),
                            sh_status = dt.Rows[i]["Sh_statas"].ToString(),
                            //oi_department = Convert.ToInt32(dt.Rows[i]["oi_department"]),
                            oi_departmente_name = dt.Rows[i]["oi_departmente_name"].ToString(),
                            //oi_section = Convert.ToInt32(dt.Rows[i]["oi_section"]),
                            oi_section_name = dt.Rows[i]["oi_section_name"].ToString(),
                            //oi_designation = Convert.ToInt32(dt.Rows[i]["oi_designation"]),
                            oi_designation_name = dt.Rows[i]["oi_designation_name"].ToString(),
                            //oi_bulding = Convert.ToInt32(dt.Rows[i]["oi_bulding"]),
                            oi_bulding_name = dt.Rows[i]["oi_bulding_name"].ToString(),
                            oi_joineddate = dt.Rows[i]["oi_joineddate"].ToString(),
                            at_date = dt.Rows[i]["at_date"].ToString(),
                            at_intime = Convert.ToDecimal(dt.Rows[i]["at_intime"]),
                            at_outdate = dt.Rows[i]["at_outdate"].ToString(),
                            at_outtime = Convert.ToDecimal(dt.Rows[i]["at_outtime"]),
                            at_status = dt.Rows[i]["at_status"].ToString(),
                            sh_InTime = Convert.ToDecimal(dt.Rows[i]["sh_InTime"]),
                            sh_OutTime = Convert.ToDecimal(dt.Rows[i]["sh_OutTime"])
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return addList;
        }
        public async Task<bool> SaveManualAtt(List<ManualAttSavePara> para)
        {
            bool save = false;
            try
            {                
                foreach (ManualAttSavePara item in para)
                {
                    SqlCommand cmd = new SqlCommand("Emp_update_menualAttendance",_connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@at_emp_serial", item.at_emp_serial);
                    cmd.Parameters.AddWithValue("@at_date", item.at_date);
                    cmd.Parameters.AddWithValue("@at_intime", item.at_intime);
                    cmd.Parameters.AddWithValue("@at_outdate", item.at_outdate);
                    cmd.Parameters.AddWithValue("@at_outtime", item.at_outtime);
                    cmd.Parameters.AddWithValue("@User", item.User);
                    cmd.Parameters.AddWithValue("@addate", DateTime.Now);
                    await _connection.OpenAsync();
                    cmd.ExecuteNonQuery();
                    await _connection.CloseAsync();
                }
                save = true;
            }
            catch (Exception ex)
            {
                ex.ToString();
                return false;
            }
            finally
            {
                await _connection.CloseAsync();
            }
            return save;
        }
        public async Task<string> UpdateMenualAttendanceAbsent(List<AttendanceAbsentPaylod> objArr)
        {
            string message = string.Empty;
            try
            {
                var chk = DateTime.Now.AddDays(-10).Month;
                if ((Convert.ToDateTime(objArr[0].start_date.ToString()).Month == DateTime.Now.AddDays(-10).Month || Convert.ToDateTime(objArr[0].start_date.ToString()).Month == DateTime.Now.Month) && (Convert.ToDateTime(objArr[0].start_date.ToString()).Year == DateTime.Now.AddDays(-10).Year || Convert.ToDateTime(objArr[0].start_date.ToString()).Year == DateTime.Now.Year))
                {
                    foreach (var item in objArr)
                    {
                        await _dgCommon.saveChangesAsync("dg_pay_Update_MenualAbsentAttendance " + item.emp_serial + ",'" + item.start_date + "','" + item.end_date + "','" + item.userName + "'", _connection);
                    }
                    message = "Save Successfully !!";
                }
                else
                {
                    message = "Date Range(" + objArr[0].start_date +" To "+ objArr[0].end_date + ") Is Not Valid !!";
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                message = "Something is wrong,Data not save !!";
            }
            return message;
        }
        public async Task<List<GetListAttendanceRowDel>> GetEmployeeListForAttDel(GetEmployeeListPayload obj)
        {
            var data = new List<GetListAttendanceRowDel>();
            try
            {
                foreach (var item in obj.emp_no)
                {
                    var dataTable = await _dgCommon.get_InformationDataTableAsync("dg_pay_GetEmployeeListForAttDelete "+ obj.compid + ","+ item + "", _connection);
                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        data.Add(new GetListAttendanceRowDel
                        {
                            emp_serial = Convert.ToInt32(dataTable.Rows[i]["emp_serial"]),
                            compid = Convert.ToInt32(dataTable.Rows[i]["compid"]),
                            emp_no = Convert.ToInt32(dataTable.Rows[i]["emp_no"]),
                            emp_proxid = dataTable.Rows[i]["emp_proxid"].ToString(),
                            pi_fullname = dataTable.Rows[i]["pi_fullname"].ToString(),
                            oi_departmente_name = dataTable.Rows[i]["oi_departmente_name"].ToString(),
                            oi_section_name = dataTable.Rows[i]["oi_section_name"].ToString(),
                            oi_bulding_name = dataTable.Rows[i]["oi_bulding_name"].ToString(),
                            oi_floor_name = dataTable.Rows[i]["oi_floor_name"].ToString(),
                            oi_line_name = dataTable.Rows[i]["oi_line_name"].ToString(),
                            oi_shift_name = dataTable.Rows[i]["oi_shift_name"].ToString(),
                            oi_joineddate = dataTable.Rows[i]["oi_joineddate"].ToString(),
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return data;
        }
    }
}
