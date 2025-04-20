using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API.Models.Domain;
using API.Models.DTO;
using API.Repositories.Interfaces;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }

        /// <summary>
        /// Upload an image file
        /// </summary>
        /// <param name="file">The image file to upload</param>
        /// <param name="fileName">Name of the file</param>
        /// <param name="title">Title of the image</param>
        /// <returns>Returns the uploaded image details</returns>
        /// <response code="200">Returns the uploaded image details</response>
        /// <response code="400">If the file is invalid</response>
        [HttpPost]
        [Consumes("multipart/form-data")]
        [ProducesResponseType(typeof(BookImageDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UploadImage(
            [FromForm] ImageUploadRequest request)
        {
            if (request.File == null)
            {
                return BadRequest("No file uploaded");
            }

            ValidateFileUpload(request.File);
            if (ModelState.IsValid)
            {
                var bookImage = new BookImage
                {
                    FileExtension = Path.GetExtension(request.File.FileName).ToLower(),
                    FileName = request.FileName,
                    Title = request.Title,
                    DateCreated = DateTime.Now
                };
                
                bookImage = await imageRepository.Upload(request.File, bookImage);
                
                var response = new BookImageDto
                {
                    Id = bookImage.Id,
                    Title = bookImage.Title,
                    DateCreated = bookImage.DateCreated,
                    FileExtension = bookImage.FileExtension,
                    FileName = bookImage.FileName,
                    Url = bookImage.Url
                };
                return Ok(response);
            }

            return BadRequest(ModelState);
        }

        private void ValidateFileUpload(IFormFile file)
        {
            var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png" };
            if (!allowedExtensions.Contains(Path.GetExtension(file.FileName).ToLower()))
            {
                ModelState.AddModelError("file", "Unsupported file format");
            }

            if (file.Length > 10485760)
            {
                ModelState.AddModelError("file", "File size cannot be more than 10MB");
            }
        }


        //GET: {apibaseURL}/api/images
        [HttpGet]
        public async Task<IActionResult> GetAllImages(){
            //call image repository
            var images = await this.imageRepository.GetAll();

            //domain model to dto
            var response = new List<BookImageDto>();
            foreach (var image in images)
            {
                response.Add(new BookImageDto{
                    Id = image.Id,
                    Title = image.Title,
                    DateCreated = image.DateCreated,
                    FileExtension = image.FileExtension,
                    FileName = image.FileName,
                    Url = image.Url
                });
            }
            return Ok(response);
        }
    }
}