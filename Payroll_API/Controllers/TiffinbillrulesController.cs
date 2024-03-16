﻿using BLL.Interfaces;
using BOL.Models;
using Microsoft.AspNetCore.Mvc;

namespace Payroll_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TiffinbillrulesController : ControllerBase
    {
        private readonly IGlobalMaster _globalMaster;
        private readonly ILogger<TiffinbillrulesController> _logger;
        public TiffinbillrulesController(IGlobalMaster globalMaster, ILogger<TiffinbillrulesController> logger)
        {
            _globalMaster = globalMaster;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetDgPayTiffinbillrules()
        {
            try
            {
                var data = _globalMaster.tiffinbillrules.GetAll().Select(x => DgPayTiffinbillrule.DbToCustomModel(x)).ToList();
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetDgPayTiffinbillrules)}");
                return StatusCode(500, "Internal Server Error, Please Try Again Later!");
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDgPayTiffinbillrule(int id)
        {
            try
            {
                var data = await _globalMaster.tiffinbillrules.GetFirstOrDefaultAsync(x => x.serial == id);
                var nData = DgPayTiffinbillrule.DbToCustomModel(data);
                return Ok(nData);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetDgPayTiffinbillrule)}");
                return StatusCode(500, "Internal Server Error, Please Try Again Later!");
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDgPayTiffinbillrule(int id, DgPayTiffinbillrule obj)
        {
            try
            {
                var nData = DgPayTiffinbillrule.CustomToDbModel(obj);
                await _globalMaster.tiffinbillrules.UpdateAsync(nData);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(PutDgPayTiffinbillrule)}");
                return StatusCode(500, "Internal Server Error, Please Try Again Later!");
            }
        }
        [HttpPost]
        public async Task<IActionResult> PostDgPayTiffinbillrule(DgPayTiffinbillrule obj)
        {
            try
            {
                var nData = DgPayTiffinbillrule.CustomToDbModel(obj);
                await _globalMaster.tiffinbillrules.AddAsync(nData);
                return CreatedAtAction("GetDgPayTiffinbillrule", new { id = nData.serial }, nData);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(PostDgPayTiffinbillrule)}");
                return StatusCode(500, "Internal Server Error, Please Try Again Later!");
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDgPayTiffinbillrule(int id)
        {
            try
            {
                var obj = await _globalMaster.tiffinbillrules.GetFirstOrDefaultAsync(x => x.serial == id);
                if (obj == null)
                {
                    _logger.LogError($"Invalid Delete Attempt In {nameof(DeleteDgPayTiffinbillrule)}");
                    return BadRequest("Submitted Data Is Invalid");
                }
                await _globalMaster.tiffinbillrules.DeleteAsync(obj);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(DeleteDgPayTiffinbillrule)}");
                return StatusCode(500, "Internal Server Error, Please Try Again Later!");
            }
        }
    }
}
