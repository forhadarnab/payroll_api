using BLL.Interfaces;
using BOL.Models;
using CoreApiResponse;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace Payroll_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : BaseController
    {
        private readonly IGlobalMaster _globalMaster;
        private readonly IConfiguration _configuration;
        private readonly ILogger<LoginController> _logger;
        public LoginController(IGlobalMaster globalMaster, IConfiguration configuration, ILogger<LoginController> logger)
        {
            _globalMaster = globalMaster;
            _configuration = configuration;
            _logger = logger;
        }

        [HttpPost("PostLoginDetails")]
        //[Route("PostLoginDetails")]
        public async Task<IActionResult> PostLoginDetails(UserModel _userData)
        {
            try
            {
                if (_userData != null)
                {
                    var resultLoginCheck = await _globalMaster.login.GetFirstOrDefaultAsync(e => e.FullName == _userData.FullName && e.Password == _userData.Password && Convert.ToBoolean(e.Active_status)==true);
                    if (resultLoginCheck == null)
                    {
                        return BadRequest("Invalid Credentials");
                    }
                    else
                    {
                        _userData.UserMessage = "Login Success";
                        var claims = new[] {
                            new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                            new Claim("UserId", _userData.ID.ToString()),
                            new Claim("DisplayName", _userData.FullName)
                        };
                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                        var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                        var token = new JwtSecurityToken(
                            _configuration["Jwt:Issuer"],
                            _configuration["Jwt:Audience"],
                            claims,
                            expires: DateTime.UtcNow.AddMinutes(1000000),
                            signingCredentials: signIn);
                        _userData.AccessToken = new JwtSecurityTokenHandler().WriteToken(token);
                        var accessToken = _userData.AccessToken;
                        return Ok(new { accessToken = accessToken, UserFullName = resultLoginCheck.UserFullname, resultLoginCheck.CompId, EmpId = resultLoginCheck.Emp_ID,userName= resultLoginCheck.FullName });
                    }
                }
                else
                {
                    return BadRequest("No Data Posted");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(PostLoginDetails)}");
                return StatusCode(500, "Internal Server Error, Please Try Again Later!");
            }
        }
        [HttpPost("Save_PasswordChange")]
        public async Task<IActionResult> Save_PasswordChange(int compID, string loginID, string confPassword, string newPassword)
        {
            try
            {
                var response = await _globalMaster.login.SaveUserPasswordChange(compID, loginID, confPassword, newPassword);
                if (response == "Password Change Successfully !!")
                {
                    return CustomResult(response, HttpStatusCode.OK);
                }
                return CustomResult(response,HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(Save_PasswordChange)}");
                return StatusCode(500, "Internal Server Error, Please Try Again Later!");
            }
        }
        [HttpPost("userMenuParmission")]
        public async Task<IActionResult> GetUserMenuPermission(List<MenuList> obj)
        {
            try
            {
                var result = await _globalMaster.login.GetPermitedMenuList(obj);
                if (result.Count > 0)
                {
                    return CustomResult("Menu Loaded !!",HttpStatusCode.OK);
                }
                return CustomResult("Menu Not Loaded !!", HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(Save_PasswordChange)}");
                return StatusCode(500, "Internal Server Error, Please Try Again Later!");
            }
        }
    }
}
