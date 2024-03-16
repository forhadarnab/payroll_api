using BLL.Interfaces;
using BLL.Utility;
using BOL.Models;
using Microsoft.AspNetCore.Mvc;

namespace Payroll_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TiffinNightBillDesignationController : ControllerBase
    {
        private readonly IGlobalMaster _globalMaster;
        private readonly ILogger<TiffinNightBillDesignationController> _logger;
        public TiffinNightBillDesignationController(IGlobalMaster globalMaster, ILogger<TiffinNightBillDesignationController> logger)
        {
            _globalMaster = globalMaster;
            _logger = logger;
        }

        [HttpGet("GetALLDesignationForTiffinNightBill")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetALLDesignationForTiffinNightBill(int companyID)
        {
            var response = await _globalMaster.tiffinNightDescWise.GetAllDesignationByComp_ForNightAndTiffinBill(companyID);
            if (response.IsSuccess == true)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
        [HttpPost("addOrEditDesignationForTiffinNightBill")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> addOrEditDesignationForTiffinNightBill(DesignationWiseTiffinAndNightBill obj)
        {
            var response = await _globalMaster.tiffinNightDescWise.AddOrEditDescWiseTiffinAndNightBill(obj);
            if (response.IsSuccess == true)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
    }
}
