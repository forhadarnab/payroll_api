using BLL.Interfaces;
using BOL.Models;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;

namespace Payroll_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadAttendancesController : ControllerBase
    {
        private readonly IGlobalMaster _globalMaster;
        private readonly ILogger<UploadAttendancesController> _logger;
        public UploadAttendancesController(IGlobalMaster globalMaster, ILogger<UploadAttendancesController> logger)
        {
            _globalMaster = globalMaster;
            _logger = logger;
        }

        [HttpPost]
        [Route("UploadFile")]
        public async Task<IActionResult> UploadFile([FromForm] UploadFile model)
        {
            try
            {
                var result = await _globalMaster.uploadAttendances.WriteFile(model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(UploadFile)}");
                return StatusCode(500, "Internal Server Error, Please Try Again Later!");
            }
        }
        [HttpPost]
        [Route("UploadFile1")]
        public async Task<IActionResult> UploadFile1([FromForm] UploadFile model)
        {
            try
            {
                var result = await _globalMaster.uploadAttendances.WriteFile1(model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(UploadFile1)}");
                return StatusCode(500, "Internal Server Error, Please Try Again Later!");
            }            
        }

        [HttpPost]
        [Route("UploadFile3")]
        public async Task<IActionResult> UploadFile3([FromForm] UploadFile model)
        {
            try
            {
                var result = await _globalMaster.uploadAttendances.ReadAttnTextFile(model);
                if (result.Count > 0)
                {
                    return Ok(result);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(UploadFile1)}");
                return StatusCode(500, "Internal Server Error, Please Try Again Later!");
            }
        }

        [HttpGet("TestString")]
        public IActionResult TestString(string testString)
        {
            string sValue = testString.Reverse().ToString();
            var alt_right6 = new string(testString.Reverse().Take(14).Reverse().ToArray());
            string tst = testString.Substring(0, testString.Length - alt_right6.Length);
            var newArr = new string[] { alt_right6, tst };
            string newString = string.Concat(newArr);
            return Ok(newString);
        }
    }
}