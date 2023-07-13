using AutoMapper;
using FCI_API.Dto.Post;
using FCI_API.Helper;
using FCI_API.Models;
using FCI_DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Net;

namespace FCI_API.Controllers
{
    /// <summary>
    /// Controller for managing blog posts.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;
        private readonly ResponseModel _responseModel;
        private readonly IWebHostEnvironment _webHostEnvironment;

        /// <summary>
        /// Initializes a new instance of the <see cref="PostController"/> class.
        /// </summary>
        /// <param name="postRepository">The post repository.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="webHostEnvironment">The web host environment.</param>
        public PostController(IPostRepository postRepository, IMapper mapper, IWebHostEnvironment webHostEnvironment)
        {
            _postRepository = postRepository;
            _mapper = mapper;
            _responseModel = new ResponseModel();
            _webHostEnvironment = webHostEnvironment;
        }

        /// <summary>
        /// Gets all blog posts.
        /// </summary>
        /// <returns>An ActionResult containing a list of PostDto objects.</returns>
        [HttpGet("GetAllPosts")]
        public async Task<ActionResult<ResponseModel>> GetAllPosts()
        {
            try
            {
                // Get all posts from the repository, order them by AddedOn descending, and project to PostDto objects
                var res = (await _postRepository.GetAllAsync()).OrderByDescending(p => p.AddedOn).Select(p => new PostDto()
                {
                    ImageURL = GetFullUrlToImage(p.ImageURL),
                    Body = p.Body,
                    Titles = p.Titles,
                    Id = p.Id
                });

                // Map the IEnumerable<PostDto> to List<PostDto> using AutoMapper
                var Result = (_mapper.Map<List<PostDto>>(res));

                _responseModel.Result = Result;
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

        /// <summary>
        /// Creates a new blog post.
        /// </summary>
        /// <param name="dto">The CreatePostDto containing the post data.</param>
        /// <returns>An ActionResult containing the newly created PostDto object.</returns>
        [HttpPost("CreateNewPost")]
        [Authorize(Roles = "Admin")]

        public async Task<ActionResult<ResponseModel>> CreateNewPost([FromForm] CreatePostDto dto)
        {
            try
            {
                // Generate a unique file name and save the uploaded image to the wwwroot/images directory
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(dto.Image.FileName);
                var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", fileName);
                var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}";
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    await dto.Image.CopyToAsync(stream);
                }

                // Create a new Post entity and save it to the repository
                var createdPost = new Post()
                {
                    Titles = dto.Titles,
                    Body = dto.Body,
                    ImageURL = Path.Combine("images", fileName),
                };
                var savedPost = await _postRepository.CreateAsync(createdPost);

                // Create a PostDto object and return it as the result
                _responseModel.Result = new PostDto()
                {
                    Body = savedPost.Body,
                    Titles = savedPost.Titles,
                    ImageURL = GetFullUrlToImage(savedPost.ImageURL),
                    Id = savedPost.Id
                };
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

        /// <summary>
        /// Updates an existing blog post.
        /// </summary>
        /// <param name="dto">The UpdatePostDto containing the updated post data.</param>
        /// <returns>An ActionResult containing the updated PostDto object.</returns>
        [HttpPut("UpdatePost")]
        [Authorize(Roles = "Admin")]

        public async Task<ActionResult<ResponseModel>> UpdatePost([FromForm] UpdatePostDto dto)
        {
            try
            {
                // Get the existing post from the repository based on the provided ID
                var existingPost = await _postRepository.GetAsync(p => p.Id == dto.Id);

                if (existingPost == null)
                {
                    // Return appropriate response if the post does not exist
                    _responseModel.IsSuccess = false;
                    _responseModel.Result = null;
                    _responseModel.StatusCode = HttpStatusCode.NotFound;
                    _responseModel.ErrorMessages = new List<string> { "Post not found." };
                    return _responseModel;
                }

                // Update the post properties with the provided values
                existingPost.Titles = dto.Titles;
                existingPost.Body = dto.Body;

                if (dto.Image != null)
                {
                    // Generate a unique file name and save the uploaded image to the wwwroot/images directory
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(dto.Image.FileName);
                    var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", fileName);

                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        await dto.Image.CopyToAsync(stream);
                    }

                    // Delete the previous image file, if needed
                    if (!string.IsNullOrEmpty(existingPost.ImageURL))
                    {
                        var previousImagePath = Path.Combine(_webHostEnvironment.WebRootPath, existingPost.ImageURL);
                        if (System.IO.File.Exists(previousImagePath))
                        {
                            System.IO.File.Delete(previousImagePath);
                        }
                    }

                    existingPost.ImageURL = Path.Combine("images", fileName);
                }
                else if (!string.IsNullOrEmpty(existingPost.ImageURL))
                {
                    // No image provided, remove the existing image file
                    var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, existingPost.ImageURL);
                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }

                    existingPost.ImageURL = null;
                }

                // Update the post in the repository
                var updatedPost = await _postRepository.UpdatePostAsync(existingPost);

                // Create a PostDto object and return it as the result
                _responseModel.Result = new PostDto()
                {
                    Body = updatedPost.Body,
                    Titles = updatedPost.Titles,
                    ImageURL = GetFullUrlToImage(updatedPost.ImageURL),
                    Id = updatedPost.Id
                };
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

        /// <summary>
        /// Deletes a blog post.
        /// </summary>
        /// <param name="id">The ID of the post to delete.</param>
        /// <returns>An ActionResult indicating the success or failure of the delete operation.</returns>
        [HttpDelete("DeletePost")]
        [Authorize(Roles = "Admin")]

        public async Task<ActionResult<ResponseModel>> DeletePost(int id)
        {
            try
            {
                // Get the existing post from the repository based on the provided ID
                var existingPost = await _postRepository.GetAsync(p => p.Id == id);

                if (existingPost == null)
                {
                    // Return appropriate response if the post does not exist
                    _responseModel.IsSuccess = false;
                    _responseModel.Result = null;
                    _responseModel.StatusCode = HttpStatusCode.NotFound;
                    _responseModel.ErrorMessages = new List<string> { "Post not found." };
                    return _responseModel;
                }

                // Delete the associated image file, if any
                if (!string.IsNullOrEmpty(existingPost.ImageURL))
                {
                    var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, existingPost.ImageURL);
                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }
                }

                // Delete the post from the repository
                await _postRepository.DeleteAsync(existingPost);

                // Return the success response
                _responseModel.Result = new { IsDeleted = true };
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

        /// <summary>
        /// Gets the full URL to an image using the current HttpContext.
        /// </summary>
        /// <param name="imageUrl">The relative URL of the image.</param>
        /// <returns>The full URL to the image.</returns>
        private string GetFullUrlToImage(string? imageUrl)
        {
            if (imageUrl is null)
                return "";

            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}";
            return $"{baseUrl}/{imageUrl}";
        }
    }
}
