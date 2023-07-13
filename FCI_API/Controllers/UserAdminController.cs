using FCI_API.Helper;
using FCI_DataAccess.Models;
using FCI_DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FCI_API.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]

    public class UserAdminController : ControllerBase
    {
        private readonly IUserRepository _user;
        private readonly ResponseModel _responseModel;


        public UserAdminController(IUserRepository user)
        {
            _user = user;
            _responseModel = new ResponseModel();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromQuery] LoginRequestDto dto)
        {
            var loginResponse = await _user.Login(dto);
            if (loginResponse.Id == "" || string.IsNullOrEmpty(loginResponse.Token))
            {
                return BadRequest("user or password is incorrect");
            }

            return Ok(loginResponse);
        }


        [Authorize(Roles = "Admin")]
        [HttpPost("Registration")]
        public async Task<ActionResult<ResponseModel>> Registration([FromQuery] RegistrationRequestDto dto)
        {
            bool ifUserNameUnque = _user.IsuniqueUser(dto.UserName);
            if (!ifUserNameUnque)
            {

                return BadRequest("username is exists");
            }

            var user = await _user.Register(dto);
            if (user is null)
            {
                _responseModel.Result = null;
                _responseModel.ErrorMessages = new() { "error while register" };
                _responseModel.IsSuccess = false;
                _responseModel.StatusCode = HttpStatusCode.BadRequest;
                return _responseModel;
            }
            else
            {
                _responseModel.Result = new { isRegister = true };
                return _responseModel;
            }
        }
    }
}

