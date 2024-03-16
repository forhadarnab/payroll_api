namespace BOL.Models
{
    public class ReportParameterModel
    {
        public int Compid { get; set; }
        public int Department { get; set; }
        public int section { get; set; }
        public int Building { get; set; }
        public int Floor { get; set; }
        public int Line { get; set; }
        public int Shift { get; set; }
        public int Grade { get; set; }
        public int[] salcat { get; set; }
        public DateTime Start_date { get; set; }
        public DateTime End_date { get; set; }
        public int oi_bank { get; set; }
        public string Inactive_reson { get; set; }
        public List<reportEmpData> empNoFilter { get; set; }
        public string reportType { get; set; }=string.Empty;
        public string User { get; set; }
    }
    public class EmployeeCheckPaylod
    {
        public int compid { get; set; }
        public int emp_no { get; set; }
        public int department { get; set; }
        public int section { get; set; }
        public int building { get; set; }
        public int floor { get; set; }
        public int line { get; set; }
        public int[] salCategory { get; set; }
    }   
    public class reportEmpData
    {
        public int EmpNo { get; set; }
        public string empName { get; set; } = null;
        public bool? isGet { get; set; } = null;
    }

    //New Action 11/23/2023 Forhad
    public class Rep_EmpIdNoCheckPaylod
    {
        public int compid { get; set; }
        public int emp_no { get; set; }
        public int[] department { get; set; }
        public int[] section { get; set; }
        public int building { get; set; }
        public int[] floor { get; set; }
        public int[] line { get; set; }
        public int[] salCategory { get; set; }
    }
    public class ReportPrintPayload
    {
        public int Compid { get; set; }
        public int[] Department { get; set; }
        public int[] section { get; set; }
        public int Building { get; set; }
        public int[] Floor { get; set; }
        public int[] Line { get; set; }
        public int Shift { get; set; }
        public int Grade { get; set; }
        public int[] salcat { get; set; }
        public DateTime Start_date { get; set; }
        public DateTime End_date { get; set; }
        public int oi_bank { get; set; }
        public string Inactive_reson { get; set; }
        public List<reportEmpData> empNoFilter { get; set; }
        public string User { get; set; }
    }
    public class DepartmenIdtArr
    {
        public int companyID { get; set; }
        public int[] dptID { get; set; }
    }
    public class floorIdArr
    {
        public int[] floorID { get; set; }
    }
    public class floorIdPayload
    {
        public int companyID { get; set; }
        public int buldingID { get; set; }
        public int[] departmentID { get; set; }
        public int[] sectionID { get; set; }
    }
    public class lineIdPayload
    {
        public int companyID { get; set; }
        public int[] departmentID { get; set; }
        public int[] sectionID { get; set;}
        public int[] floorID { get; set; }
    }
    public class ShiftIdPayload
    {
        public int companyID { get; set; }
        public int[] departmentID { get; set; }
        public int[] sectionID { get; set; }
        public int[] floorID { get; set; }
        public int[] lineID { get; set; }
    }
    public class GradeIdPayload
    {
        public int companyID { get; set; }
        public int[] departmentID { get; set; }
        public int[] sectionID { get; set; }
        public int[] floorID { get; set; }
        public int[] lineID { get; set; }
        public int[] shiftID { get; set;}
    }
    public class SalCatIdPayload
    {
        public int companyID { get; set; }
        public int[] departmentID { get; set; }
        public int[] sectionID { get; set; }
        public int[] floorID { get; set; }
        public int[] lineID { get; set; }
        public int[] shiftID { get; set; }
        public int[] gradeID { get; set; }
    }
}