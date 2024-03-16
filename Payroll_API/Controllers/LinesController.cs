using BLL.Interfaces;
using BOL.Models;
using Microsoft.AspNetCore.Mvc;

namespace Payroll_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LinesController : ControllerBase
    {
        private readonly IGlobalMaster _globalMaster;
        private readonly ILogger<LinesController> _logger;
        public LinesController(IGlobalMaster globalMaster, ILogger<LinesController> logger)
        {
            _globalMaster = globalMaster;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetDgPayLines(string userName)
        {
            try
            {
                //var data = _globalMaster.lines.GetAll().Select(x => DgPayLine.DbToCustomModel(x)).ToList();
                var data = await _globalMaster.lines.GetLineByUserName(userName);
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetDgPayLines)}");
                return StatusCode(500, "Internal Server Error, Please Try Again Later!");
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDgPayLine(int id)
        {
            try
            {
                var data = await _globalMaster.lines.GetFirstOrDefaultAsync(x => x.Line_Code == id);
                var nData = DgPayLine.DbToCustomModel(data);
                return Ok(nData);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetDgPayLine)}");
                return StatusCode(500, "Internal Server Error, Please Try Again Later!");
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDgPayLine(int id, DgPayLine obj)
        {
            try
            {
                var nData = DgPayLine.CustomToDbModel(obj);
                await _globalMaster.lines.UpdateAsync(nData);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(PutDgPayLine)}");
                return StatusCode(500, "Internal Server Error, Please Try Again Later!");
            }
        }
        [HttpPost]
        public async Task<IActionResult> PostDgPayLine(DgPayLine obj)
        {
            try
            {
                var nData = DgPayLine.CustomToDbModel(obj);
                await _globalMaster.lines.AddAsync(nData);
                return CreatedAtAction("GetDgPayLine", new { id = nData.Line_Code }, nData);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(PostDgPayLine)}");
                return StatusCode(500, "Internal Server Error, Please Try Again Later!");
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDgPayLine(int id)
        {
            try
            {
                var obj = await _globalMaster.lines.GetFirstOrDefaultAsync(c => c.Line_Code == id);
                if (obj == null)
                {
                    _logger.LogError($"Invalid Delete Attempt In {nameof(DeleteDgPayLine)}");
                    return BadRequest("Submitted Data Is Invalid");
                }
                await _globalMaster.lines.DeleteAsync(obj);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(DeleteDgPayLine)}");
                return StatusCode(500, "Internal Server Error, Please Try Again Later!");
            }
        }
    }
}
