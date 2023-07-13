using FCI_API.Data;
using FCI_API.Helper;
using FCI_DataAccess.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace FCI_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactUsMessageController : ControllerBase
    {
        private readonly ResponseModel _responseModel;
        private readonly ApplicationDbContext _context;
        public ContactUsMessageController(ApplicationDbContext context)
        {
            _responseModel = new ResponseModel();
            _context = context;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("GetContactUsMessage")]
        public async Task<ActionResult<ResponseModel>> GetContactUsMessage()
        {
            try
            {

                var ContactUsMessage = await _context.ContactUsMessages.ToListAsync();
                _responseModel.Result = ContactUsMessage;
                return _responseModel;

            }
            catch (Exception e)
            {
                _responseModel.IsSuccess = false;
                _responseModel.Result = null;
                _responseModel.StatusCode = HttpStatusCode.BadRequest;
                _responseModel.ErrorMessages = new List<string> { e.Message.ToString() };
                return _responseModel;
            }

        }
        [HttpPost("CreateContactUsMessage")]
        public async Task<ActionResult<ResponseModel>> CreateContactUsMessage(
             string name,
            string email,
                string message
            )
        {
            try
            {
                var newMessage = new ContactUsMessage
                {
                    Name = name,
                    Email = email,
                    Message = message,
                    CreatedAt = DateTime.Now
                };

                _context.ContactUsMessages.Add(newMessage);
                await _context.SaveChangesAsync();
                _responseModel.Result = newMessage;
                return _responseModel;
            }
            catch (Exception e)
            {
                _responseModel.IsSuccess = false;
                _responseModel.Result = null;
                _responseModel.StatusCode = HttpStatusCode.BadRequest;
                _responseModel.ErrorMessages = new List<string> { e.Message.ToString() };
                return _responseModel;
            }
        }







    }
}
