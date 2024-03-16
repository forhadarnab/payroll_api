﻿namespace BOL.Models
{
    public class DesignationWiseTiffinAndNightBill
    {
        public int compnayID { get; set; }
        public string companyName { get; set; }
        public List<DesiInfo> DesiInfos { get; set; }
        public string userName { get; set; }
    }
    public class DesiInfo
    {
        public int designationID { get; set; }
        public string designationName { get; set; }
        public int tiffinAmount { get; set; }
        public int nightAmount { get; set; }
    }
}