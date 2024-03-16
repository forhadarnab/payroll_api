using BLL.Interfaces;
using BOL.Models;
using CoreApiResponse;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Payroll_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MasterSetupController : BaseController
    {
        private readonly IGlobalMaster _globalMaster;
        private readonly ILogger<MasterSetupController> _logger;
        public MasterSetupController(IGlobalMaster globalMaster,ILogger<MasterSetupController> logger)
        {
            _globalMaster = globalMaster;
            _logger = logger;
        }


        //Payroll Setup
        #region"Divisions"
        [HttpPost("AddOrEditDivision")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SaveOrUpdateDivision(DivisionPayload obj)
        {
            try
            {
                var response = await _globalMaster.divisions.AddOrEditDivision(obj);
                if (response.IsSuccess == true)
                {
                    return CustomResult(response.Message,HttpStatusCode.OK);
                }
                return CustomResult(response.Message,HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(SaveOrUpdateDivision)}");
                return CustomResult("Internal Server Error, Please Try Again Later!",HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet("GetAllDivisions")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllDivisions()
        {
            try
            {
                var response = await _globalMaster.divisions.GetAllDivisions();
                if (response.IsSuccess == true)
                {
                    return CustomResult(response.Message,response.ListData, HttpStatusCode.OK);
                }
                return CustomResult(response.Message, HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetAllDivisions)}");
                return CustomResult("Internal Server Error, Please Try Again Later!", HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet("GetAllDivisionById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllDivisionById(int id)
        {
            try
            {
                var response = await _globalMaster.divisions.GetDivisionById(id);
                if (response.IsSuccess == true)
                {
                    return CustomResult(response.Message, response.SingleData, HttpStatusCode.OK);
                }
                return CustomResult(response.Message, HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetAllDivisionById)}");
                return CustomResult("Internal Server Error, Please Try Again Later!", HttpStatusCode.InternalServerError);
            }
        }
        #endregion

        #region"District"
        [HttpPost("AddOrEditDistrict")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddOrEditDistrict(DistrictPayload obj)
        {
            try
            {
                var response = await _globalMaster.districts.AddOrEditDistrict(obj);
                if (response.IsSuccess == true)
                {
                    return CustomResult(response.Message, HttpStatusCode.OK);
                }
                return CustomResult(response.Message, HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(AddOrEditDistrict)}");
                return CustomResult("Internal Server Error, Please Try Again Later!", HttpStatusCode.InternalServerError);
            }
        }
        [HttpGet("GetAllDistricts")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllDistricts()
        {
            try
            {
                var response = await _globalMaster.districts.GetAllDistrict();
                if (response.IsSuccess == true)
                {
                    return CustomResult(response.Message, response.ListData, HttpStatusCode.OK);
                }
                return CustomResult(response.Message, HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetAllDistricts)}");
                return CustomResult("Internal Server Error, Please Try Again Later!", HttpStatusCode.InternalServerError);
            }
        }
        #endregion
        
        #region"Thana"
        [HttpPost("AddOrEditThana")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddOrEditThana(ThanaPayload obj)
        {
            try
            {
                var response = await _globalMaster.thanas.AddOrEditThana(obj);
                if (response.IsSuccess == true)
                {
                    return CustomResult(response.Message, HttpStatusCode.OK);
                }
                return CustomResult(response.Message, HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(AddOrEditThana)}");
                return CustomResult("Internal Server Error, Please Try Again Later!", HttpStatusCode.InternalServerError);
            }
        }
        [HttpGet("GetAllThanaList")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllThanaList()
        {
            try
            {
                var response = await _globalMaster.thanas.GetAllThana();
                if (response.IsSuccess == true)
                {
                    return CustomResult(response.Message, response.ListData, HttpStatusCode.OK);
                }
                return CustomResult(response.Message, HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetAllThanaList)}");
                return CustomResult("Internal Server Error, Please Try Again Later!", HttpStatusCode.InternalServerError);
            }
        }
        #endregion

        #region"Designation"
        [HttpPost("AddOrEditDesignation")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddOrEditDesignation(DesignationPayload obj)
        {
            try
            {
                var response = await _globalMaster.designations.AddOrEditDesignation(obj);
                if (response.IsSuccess == true)
                {
                    return Ok(response);
                }
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(AddOrEditDesignation)}");
                return CustomResult("Internal Server Error, Please Try Again Later!", HttpStatusCode.InternalServerError);
            }
        }
        [HttpGet("GetAllDesignationList")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllDesignationList()
        {
            try
            {
                var response = await _globalMaster.designations.GetAllDesignation();
                if (response.IsSuccess == true)
                {
                    return Ok(response);
                }
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetAllDesignationList)}");
                return CustomResult("Internal Server Error, Please Try Again Later!", HttpStatusCode.InternalServerError);
            }
        }
        #endregion

        #region"Department"
        [HttpPost("AddOrEditDepartment")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddOrEditDepartment(DepartmentPayload obj)
        {
            try
            {
                var response = await _globalMaster.departments.AddOrEditDepartment(obj);
                if (response.IsSuccess == true)
                {
                    return Ok(response);
                }
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(AddOrEditDepartment)}");
                return CustomResult("Internal Server Error, Please Try Again Later!", HttpStatusCode.InternalServerError);
            }
        }
        [HttpGet("GetAllDepartmentListByCompany")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllDepartmentList(int compnayID)
        {
            try
            {
                var response = await _globalMaster.departments.GetAllDepartmentByCompany(compnayID);
                if (response.IsSuccess == true)
                {
                    return Ok(response);
                }
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetAllDepartmentList)}");
                return CustomResult("Internal Server Error, Please Try Again Later!", HttpStatusCode.InternalServerError);
            }
        }
        #endregion

        #region"Section"
        [HttpPost("AddOrEditSection")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddOrEditSection(SectionPayload obj)
        {
            try
            {
                var response = await _globalMaster.sections.AddOrEditSection(obj);
                if (response.IsSuccess == true)
                {
                    return Ok(response);
                }
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(AddOrEditSection)}");
                return CustomResult("Internal Server Error, Please Try Again Later!", HttpStatusCode.InternalServerError);
            }
        }
        [HttpGet("GetAllSectionListByCompany")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllSectionListByCompany(int compnayID)
        {
            try
            {
                var response = await _globalMaster.sections.GetAllSectionByCompany(compnayID);
                if (response.IsSuccess == true)
                {
                    return Ok(response);
                }
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetAllSectionListByCompany)}");
                return CustomResult("Internal Server Error, Please Try Again Later!", HttpStatusCode.InternalServerError);
            }
        }
        #endregion

        #region"BuildingUnit"
        [HttpPost("AddOrEditBuildingUnit")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddOrEditBuildingUnit(BuildingUnitPayload obj)
        {
            try
            {
                var response = await _globalMaster.buildingUnit.AddOrEditBuildingUnit(obj);
                if (response.IsSuccess == true)
                {
                    return Ok(response);
                }
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(AddOrEditBuildingUnit)}");
                return CustomResult("Internal Server Error, Please Try Again Later!", HttpStatusCode.InternalServerError);
            }
        }
        [HttpGet("GetAllBuildingUnitByCompany")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllBuildingUnitByCompany(int compnayID)
        {
            try
            {
                var response = await _globalMaster.buildingUnit.GetAllBuildingUnitByCompany(compnayID);
                if (response.IsSuccess == true)
                {
                    return Ok(response);
                }
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetAllBuildingUnitByCompany)}");
                return CustomResult("Internal Server Error, Please Try Again Later!", HttpStatusCode.InternalServerError);
            }
        }
        #endregion

        #region"Floor"
        [HttpPost("AddOrEditFloor")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddOrEditFloor(FloorPayload obj)
        {
            try
            {
                var response = await _globalMaster.floors.AddOrEditFloor(obj);
                if (response.IsSuccess == true)
                {
                    return Ok(response);
                }
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(AddOrEditFloor)}");
                return CustomResult("Internal Server Error, Please Try Again Later!", HttpStatusCode.InternalServerError);
            }
        }
        [HttpGet("GetAllFloorByCompany")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllFloorByCompany(int compnayID)
        {
            try
            {
                var response = await _globalMaster.floors.GetAllFloorByCompany(compnayID);
                if (response.IsSuccess == true)
                {
                    return Ok(response);
                }
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetAllFloorByCompany)}");
                return CustomResult("Internal Server Error, Please Try Again Later!", HttpStatusCode.InternalServerError);
            }
        }
        #endregion

        #region"Line"
        [HttpPost("AddOrEditLine")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddOrEditLine(LinePayload obj)
        {
            try
            {
                var response = await _globalMaster.lines.AddOrEditLine(obj);
                if (response.IsSuccess == true)
                {
                    return Ok(response);
                }
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(AddOrEditLine)}");
                return CustomResult("Internal Server Error, Please Try Again Later!", HttpStatusCode.InternalServerError);
            }
        }
        [HttpGet("GetAllLineByCompany")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllLineByCompany(int compnayID)
        {
            try
            {
                var response = await _globalMaster.lines.GetAllLineByCompany(compnayID);
                if (response.IsSuccess == true)
                {
                    return Ok(response);
                }
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetAllLineByCompany)}");
                return CustomResult("Internal Server Error, Please Try Again Later!", HttpStatusCode.InternalServerError);
            }
        }
        #endregion

        #region"Allowance"
        [HttpPost("AddOrEditAllowance")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddOrEditAllowance(AllowancesdepPayload obj)
        {
            try
            {
                var response = await _globalMaster.allowancesde.AddOrEditAllowance(obj);
                if (response.IsSuccess == true)
                {
                    return Ok(response);
                }
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(AddOrEditAllowance)}");
                return CustomResult("Internal Server Error, Please Try Again Later!", HttpStatusCode.InternalServerError);
            }
        }
        [HttpGet("GetAllAllowances")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllAllowances()
        {
            try
            {
                var response = await _globalMaster.allowancesde.GetAllAllowances();
                if (response.IsSuccess == true)
                {
                    return Ok(response);
                }
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetAllAllowances)}");
                return CustomResult("Internal Server Error, Please Try Again Later!", HttpStatusCode.InternalServerError);
            }
        }
        #endregion

        #region"SalaryCategory"
        [HttpPost("AddOrEditSalaryCategory")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddOrEditSalaryCategory(SalarycategoryPayload obj)
        {
            try
            {
                var response = await _globalMaster.salaryCategories.AddOrEditSalaryCategory(obj);
                if (response.IsSuccess == true)
                {
                    return Ok(response);
                }
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(AddOrEditSalaryCategory)}");
                return CustomResult("Internal Server Error, Please Try Again Later!", HttpStatusCode.InternalServerError);
            }
        }
        [HttpGet("GetAllSalaryCategoryByCompany")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllSalaryCategoryByCompany(int companyID)
        {
            try
            {
                var response = await _globalMaster.salaryCategories.GetAllSalaryCategoryByCompany(companyID);
                if (response.IsSuccess == true)
                {
                    return Ok(response);
                }
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetAllSalaryCategoryByCompany)}");
                return CustomResult("Internal Server Error, Please Try Again Later!", HttpStatusCode.InternalServerError);
            }
        }
        #endregion

        #region"Grade"
        [HttpPost("AddOrEditGrade")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddOrEditGrade(GradePayload obj)
        {
            try
            {
                var response = await _globalMaster.grades.AddOrEditGrade(obj);
                if (response.IsSuccess == true)
                {
                    return Ok(response);
                }
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(AddOrEditGrade)}");
                return CustomResult("Internal Server Error, Please Try Again Later!", HttpStatusCode.InternalServerError);
            }
        }
        [HttpGet("GetAllGrade")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllGrade()
        {
            try
            {
                var response = await _globalMaster.grades.GetAllGrade();
                if (response.IsSuccess == true)
                {
                    return Ok(response);
                }
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetAllGrade)}");
                return CustomResult("Internal Server Error, Please Try Again Later!", HttpStatusCode.InternalServerError);
            }
        }
        #endregion

        #region"Deduction"
        [HttpPost("AddOrEditDeductions")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddOrEditDeductions(DeductionPayload obj)
        {
            try
            {
                var response = await _globalMaster.deduction.AddOrEditDeduction(obj);
                if (response.IsSuccess == true)
                {
                    return Ok(response);
                }
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(AddOrEditDeductions)}");
                return CustomResult("Internal Server Error, Please Try Again Later!", HttpStatusCode.InternalServerError);
            }
        }
        [HttpGet("GetAllDeductions")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllDeductions()
        {
            try
            {
                var response = await _globalMaster.deduction.GetAllDeduction();
                if (response.IsSuccess == true)
                {
                    return Ok(response);
                }
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetAllDeductions)}");
                return CustomResult("Internal Server Error, Please Try Again Later!", HttpStatusCode.InternalServerError);
            }
        }
        #endregion

        //ERP Setup

    }
}