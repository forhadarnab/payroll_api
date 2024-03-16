using BLL.Interfaces.Manager.Designations;
using BOL.Models;
using DAL.Data;
using DAL.Implementation.Repository.Designations;
using EF.Core.Repository.Manager;
using Microsoft.EntityFrameworkCore;

namespace DAL.Implementation.Manager.Designations
{
    public class DesignationsManager : CommonManager<Designation_DbModel>,IDesignationsManager
    {
        private readonly dg_hrpayrollContext _context;
        public DesignationsManager(dg_hrpayrollContext context) : base(new DesignationsRepository(context))
        {
            _context = context;
        }

        public async Task<ReturnObject> AddOrEditDesignation(DesignationPayload obj)
        {
            var result = new ReturnObject();
            try
            {
                var dbData = new Designation_DbModel();
                if (obj.designationID == 0)
                {
                    var isExists = GetFirstOrDefault(x => x.dec_name == obj.designationName && x.dec_name_bangla == obj.designationNameBN);
                    if (isExists == null)
                    {
                        dbData = DesignationPayload.PayloadToDesignationDb_Obj(obj);
                        bool isSave = await AddAsync(dbData);
                        if (isSave)
                        {
                            result.IsSuccess = true;
                            result.Message = "Designation Save Successfully !!";
                        }
                        else
                        {
                            result.IsSuccess = false;
                            result.Message = "Can Not Saved Designation !!";
                        }
                    }
                    else
                    {
                        result.IsSuccess = false;
                        result.Message = "Designation Already Exists !!";
                    }
                }
                else
                {
                    var dbObj = GetFirstOrDefault(x => x.dec_id == obj.designationID);
                    dbData = DesignationPayload.PayloadToDesignationDb_Obj(obj, dbObj);
                    bool isUpdate = await UpdateAsync(dbData);
                    if (isUpdate)
                    {
                        result.IsSuccess = true;
                        result.Message = "Designation Update Successfully !!";
                    }
                    else
                    {
                        result.IsSuccess = false;
                        result.Message = "Can Not Update Designation !!";
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
        public async Task<ReturnObject<Designation_DbModel>> GetAllDesignation()
        {
            var result = new ReturnObject<Designation_DbModel>();
            try
            {
                var designationLs = await GetAllAsync();
                var userLs = await _context.Tbl_User.ToListAsync();
                var mainList = from a in designationLs
                join b in userLs on (!string.IsNullOrEmpty(a?.dec_user) ? a?.dec_user.Trim():string.Empty) equals b?.FullName.Trim() into userFullName
                from a1 in userFullName.DefaultIfEmpty()
                join c in userLs on a.dec_updateBy equals c.FullName.Trim() into userFullName2
                from a2 in userFullName2.DefaultIfEmpty()
                select (new Designation_DbModel
                {
                    dec_id = a.dec_id,
                    dec_groupid = a?.dec_groupid,
                    dec_name = a?.dec_name,
                    dec_name_bangla = a?.dec_name_bangla,
                    dec_user = a1?.UserFullname.Trim(),
                    dec_udate = a?.dec_udate,
                    dec_updateBy = a2?.UserFullname.Trim(),
                    dec_update = a?.dec_update
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