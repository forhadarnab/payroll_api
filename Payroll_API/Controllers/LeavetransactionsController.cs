using BLL.Interfaces;
using BOL.Models;
using Microsoft.AspNetCore.Mvc;

namespace Payroll_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeavetransactionsController : ControllerBase
    {
        private readonly IGlobalMaster _globalMaster;
        private readonly ILogger<LeavetransactionsController> _logger;
        public LeavetransactionsController(IGlobalMaster globalMaster, ILogger<LeavetransactionsController> logger)
        {
            _globalMaster = globalMaster;
            _logger = logger;
        }

        [HttpGet("get_leave_info_comdatewise")]
        public async Task<IActionResult> Getleave_info_comdatewise(int CompID, DateTime Sdate, DateTime Edate)
        {
            try
            {
                var data = await _globalMaster.leavetransactions.Getleave_info_comdatewise(CompID, Sdate, Edate);
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(Getleave_info_comdatewise)}");
                return StatusCode(500, "Internal Server Error, Please Try Again Later!");
            }
        }
        [HttpGet("get_EmployeeNo")]
        public async Task<IActionResult> GetEmployeeNo(int compID)
        {
            try
            {
                var data = await _globalMaster.leavetransactions.GetEmployeeNo(compID);
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetEmployeeNo)}");
                return StatusCode(500, "Internal Server Error, Please Try Again Later!");
            }
        }
        [HttpGet]
        public IActionResult GetDgPayLeavetransactions()
        {
            try
            {
                var data = _globalMaster.leavetransactions.GetAll().Select(x => DgPayLeavetransaction.DbToCustomModel(x)).ToList();
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetDgPayLeavetransactions)}");
                return StatusCode(500, "Internal Server Error, Please Try Again Later!");
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDgPayLeavetransaction(DateTime id)
        {
            try
            {
                var data = await _globalMaster.leavetransactions.GetFirstOrDefaultAsync(x => x.ltr_date == id);
                var nData = DgPayLeavetransaction.DbToCustomModel(data);
                return Ok(nData);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetDgPayLeavetransaction)}");
                return StatusCode(500, "Internal Server Error, Please Try Again Later!");
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDgPayLeavetransaction(DateTime id, DgPayLeavetransaction obj)
        {
            try
            {
                var nData = DgPayLeavetransaction.CustomToDbModel(obj);
                await _globalMaster.leavetransactions.UpdateAsync(nData);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(PutDgPayLeavetransaction)}");
                return StatusCode(500, "Internal Server Error, Please Try Again Later!");
            }
        }
        [HttpPost]
        public async Task<IActionResult> SaveLeaveTransaction(LeaveTransactionPayload obj)
        {
            try
            {
                var response = await _globalMaster.leavetransactions.SaveLeaveLeavetransaction(obj);
                if (response.IsSuccess)
                {
                    return Ok(new {response.IsSuccess,response.Message});
                }
                return BadRequest(new { response.IsSuccess, response.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(SaveLeaveTransaction)}");
                return StatusCode(500, "Internal Server Error, Please Try Again Later!");
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteTransInfo(int id)
        {
            try
            {
                bool isDelete = await _globalMaster.leavetransactions.deleteLevTrans(id);
                if (!isDelete)
                {
                    _logger.LogError($"Invalid Delete Attempt In {nameof(deleteTransInfo)}");
                    return BadRequest("Submitted Data Is Invalid");
                }
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(deleteTransInfo)}");
                return StatusCode(500, "Internal Server Error, Please Try Again Later!");
            }
        }
    }
}
