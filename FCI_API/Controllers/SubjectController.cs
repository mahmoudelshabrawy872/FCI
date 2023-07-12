using AutoMapper;
using FCI_API.Dto.Subject;
using FCI_API.Helper;
using FCI_DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FCI_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectRepository _repository;
        private readonly IMapper _mapper;
        private readonly ResponseModel _responseModel;
        public SubjectController(ISubjectRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _responseModel = new ResponseModel();
        }

        // GET: api/subjects
        [HttpGet("GetSubjects")]
        public async Task<ActionResult<ResponseModel>> GetSubjects()
        {
            try
            {
                var subjects = _mapper.Map<IEnumerable<SubjectDto>>((await _repository.GetAllAsync()));
                _responseModel.Result = subjects;
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

        // GET: api/subjects/{id}
        [HttpGet("GetSubjectById")]
        public async Task<ActionResult<ResponseModel>> GetSubjectById(int id)
        {
            try
            {

                var subject = await _repository.GetAsync(s => s.Id == id);

                if (subject == null)
                {
                    _responseModel.IsSuccess = false;
                    _responseModel.Result = null;
                    _responseModel.StatusCode = HttpStatusCode.NotFound;
                    _responseModel.ErrorMessages = new List<string>() { "This Subject Not Found" };
                    return _responseModel;
                }
                else
                {
                    _responseModel.Result = subject;
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
        //// POST: api/subjects
        [HttpPost("CreateSubject")]
        public async Task<ActionResult<ResponseModel>> CreateSubject(CreateSubjectDto dto)
        {
            try
            {
                var createdSubject = await _repository.CreateAsync(new() { Name = dto.Name });
                _responseModel.Result = createdSubject;
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

        //// PUT: api/subjects/{id}
        [HttpPut("UpdateSubject")]
        public async Task<ActionResult<ResponseModel>> UpdateSubjectNameById([FromQuery] int id, [FromQuery] CreateSubjectDto dto)
        {
            try
            {
                var subject = await _repository.GetAsync(s => s.Id == id);

                if (subject == null)
                {
                    _responseModel.IsSuccess = false;
                    _responseModel.Result = null;
                    _responseModel.StatusCode = HttpStatusCode.NotFound;
                    _responseModel.ErrorMessages = new List<string>() { "This Subject Not Found" };
                    return _responseModel;
                }
                else
                {
                    subject.Name = dto.Name;
                    var res = await _repository.UpdateSubjectAsync(subject);
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


        //// DELETE: api/subjects/{id}
        [HttpDelete("DeleteSubjectById")]
        public async Task<ActionResult<ResponseModel>> DeleteSubjectById(int id)
        {
            try
            {
                var subject = await _repository.GetAsync(s => s.Id == id);

                if (subject == null)
                {
                    _responseModel.IsSuccess = false;
                    _responseModel.Result = null;
                    _responseModel.StatusCode = HttpStatusCode.NotFound;
                    _responseModel.ErrorMessages = new List<string>() { "This Subject Not Found" };
                    return _responseModel;
                }
                else
                {
                    var res = await _repository.DeleteAsync(subject);
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
