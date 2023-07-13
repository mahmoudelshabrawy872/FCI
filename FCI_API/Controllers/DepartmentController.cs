using AutoMapper;
using FCI_API.Data;
using FCI_API.Dto.Department;
using FCI_API.Helper;
using FCI_DataAccess.Models;
using FCI_DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace FCI_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {

        private readonly IDepartmentRepository _repository;
        private readonly ISubjectRepository _subjectRepository;
        private readonly IMapper _mapper;
        private readonly ResponseModel _responseModel;
        private readonly ApplicationDbContext _context;
        public DepartmentController(IDepartmentRepository repository, IMapper mapper, ISubjectRepository subjectRepository, ApplicationDbContext context)
        {
            _repository = repository;
            _mapper = mapper;
            _responseModel = new ResponseModel();
            _subjectRepository = subjectRepository;
            _context = context;
        }

        // GET: api/Departments
        [HttpGet("GetDepartments")]
        public async Task<ActionResult<ResponseModel>> GetDepartments()
        {
            try
            {

                var departments = await _repository.GetAllIncludeProperties("SubjectDepartments");
                _responseModel.Result = departments;
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

        // GET: api/Departments/{id}
        [HttpGet("GetDepartmentById")]
        public async Task<ActionResult<ResponseModel>> GetDepartmentById(int id)
        {
            try
            {


                var departments = await _repository.GetAllIncludeProperties("SubjectDepartments");
                departments.FirstOrDefault(m => m.Id == id);

                if (departments == null)
                {
                    _responseModel.IsSuccess = false;
                    _responseModel.Result = null;
                    _responseModel.StatusCode = HttpStatusCode.NotFound;
                    _responseModel.ErrorMessages = new List<string>() { "This Department Not Found" };
                    return _responseModel;
                }
                else
                {
                    _responseModel.Result = departments;
                    return _responseModel;
                }

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


        //CreatePostDto
        //// POST: api/Departments
        [HttpPost("CreateDepartment")]
        [Authorize(Roles = "Admin")]

        public async Task<ActionResult<ResponseModel>> CreateDepartment(CreateDepartmentDto dto)
        {
            try
            {
                var createdDepartment = await _repository.CreateAsync(new() { Name = dto.Name });
                _responseModel.Result = createdDepartment;
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
        [HttpPost("AddSubjectToDepartment")]
        [Authorize(Roles = "Admin")]

        public async Task<ActionResult<ResponseModel>> AddSubjectToDepartment(int subjectId, int departmentId)
        {
            try
            {
                var subject = await _context.Subjects.FirstOrDefaultAsync(s => s.Id == subjectId);
                var department = await _context.Departments.FirstOrDefaultAsync(s => s.Id == departmentId);
                if (subject == null || department == null)
                {
                    _responseModel.IsSuccess = false;
                    _responseModel.Result = null;
                    _responseModel.StatusCode = HttpStatusCode.NotFound;
                    _responseModel.ErrorMessages = new List<string>() { "This Department Or Subject Not Found " };
                    return _responseModel;
                }

                var subjectDepartment = new SubjectDepartment
                {
                    SubjectId = subject.Id,
                    DepartmentId = department.Id
                };

                await _context.SubjectDepartments.AddAsync(subjectDepartment);
                await _context.SaveChangesAsync();
                _responseModel.Result = new { isAdded = true };
                return _responseModel;
            }
            catch (DbUpdateException e)
            {
                _responseModel.IsSuccess = false;
                _responseModel.Result = null;
                _responseModel.StatusCode = HttpStatusCode.BadRequest;
                _responseModel.ErrorMessages = new List<string> { e.Message.ToString() };
                return _responseModel;
            }
        }







        //// PUT: api/Departments/{id}
        [HttpPut("UpdateDepartment")]
        [Authorize(Roles = "Admin")]

        public async Task<ActionResult<ResponseModel>> UpdateDepartmentNameById([FromQuery] int id, [FromQuery] CreateDepartmentDto dto)
        {
            try
            {
                var Department = await _repository.GetAsync(s => s.Id == id);

                if (Department == null)
                {
                    _responseModel.IsSuccess = false;
                    _responseModel.Result = null;
                    _responseModel.StatusCode = HttpStatusCode.NotFound;
                    _responseModel.ErrorMessages = new List<string>() { "This Department Not Found" };
                    return _responseModel;
                }
                else
                {
                    Department.Name = dto.Name;
                    var res = await _repository.UpdateDepartmentAsync(Department);
                    _responseModel.Result = res;
                    return _responseModel;
                }

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


        //// DELETE: api/Departments/{id}
        [HttpDelete("DeleteDepartmentById")]
        [Authorize(Roles = "Admin")]

        public async Task<ActionResult<ResponseModel>> DeleteDepartmentById(int id)
        {
            try
            {
                var Department = await _repository.GetAsync(s => s.Id == id);

                if (Department == null)
                {
                    _responseModel.IsSuccess = false;
                    _responseModel.Result = null;
                    _responseModel.StatusCode = HttpStatusCode.NotFound;
                    _responseModel.ErrorMessages = new List<string>() { "This Department Not Found" };
                    return _responseModel;
                }
                else
                {
                    var res = await _repository.DeleteAsync(Department);
                    _responseModel.Result = new { isDeleted = res };
                    return _responseModel;
                }

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
