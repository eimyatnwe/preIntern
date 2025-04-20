using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API.Models.DTO;
using API.Models.Domain;
using API.Data;
using API.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;


namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class BookController: ControllerBase{
        
        private readonly IBooksRepository bookRepository;
        
        public BookController(IBooksRepository bookRepository){
            this.bookRepository = bookRepository;
        }

        //Get all books
        //GET: /api/Book
        [HttpGet]
        
        public async Task<IActionResult> GetAllBooks()
        {
            
                var books = await bookRepository.GetAllAsync();
                var response = new List<BookDto>();
                foreach (var book in books)
                {
                    response.Add(new BookDto{
                        Id = book.Id,
                        Title = book.Title,
                        Author = book.Author,
                        Publisher = book.Publisher,
                        Category = book.Category,
                        AvailableCopies = book.AvailableCopies,
                        FeaturedImageUrl = book.FeaturedImageUrl
                    });
                }

            return Ok(response);
        }

        //GET:/api/Book/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        

        public async Task<IActionResult> GetBookById([FromRoute] Guid id){
            var existingBook = await bookRepository.GetById(id);
            if (existingBook is null){
                return NotFound();
            }
            var response = new BookDto{
                Id = existingBook.Id,
                Title = existingBook.Title,
                Author = existingBook.Author,
                Publisher = existingBook.Publisher,
                Category = existingBook.Category,
                AvailableCopies = existingBook.AvailableCopies,
                FeaturedImageUrl = existingBook.FeaturedImageUrl

            };
            return Ok(response);
        }

        //GET:/api/Book/{category}
        [HttpGet]
        [Route("{category}")]
        
        public async Task<IActionResult> GetBookByCategory([FromRoute] string category){
            var existingBooks = await bookRepository.GetByCategory(category);
            if(existingBooks is null){
                return NotFound();
            }

            var response = new List<BookDto>();
            foreach (var book in existingBooks)
            {   
                response.Add(new BookDto{
                    Id = book.Id,
                    Title = book.Title,
                    Author = book.Author,
                    Publisher = book.Publisher,
                    Category = book.Category,
                    AvailableCopies = book.AvailableCopies,
                    FeaturedImageUrl = book.FeaturedImageUrl
                });
            }

            return Ok(response);
        }

        //POST:/api/Book/add
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateBook([FromBody] CreateBookRequestDto request){
            //DTO => Domain model
            var book = new Book{
                Title = request.Title,
                Author = request.Author,
                Publisher = request.Publisher,
                Category = request.Category,
                AvailableCopies = request.AvailableCopies,
                FeaturedImageUrl = request.FeaturedImageUrl
            };

            await bookRepository.CreateAsync(book);
            //domain model to DTO
            var response = new BookDto{
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                Publisher = book.Publisher,
                Category = book.Category,
                AvailableCopies = book.AvailableCopies,
                FeaturedImageUrl = book.FeaturedImageUrl
                
            };

            return Ok(response);
        }

        //DELETE: /api/Book/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize]
        public async Task<IActionResult> DeleteBook([FromRoute] Guid id){
            var existingBook = await bookRepository.DeleteAsync(id);
            if(existingBook is null){
                return NotFound();
            }

            //domain model to dto
            var response = new BookDto{
                
                Id = existingBook.Id,
                Title = existingBook.Title,
                Author = existingBook.Author,
                Publisher = existingBook.Publisher,
                Category = existingBook.Category,
                AvailableCopies = existingBook.AvailableCopies,
                FeaturedImageUrl = existingBook.FeaturedImageUrl
            };

            return Ok(response);
        }

        //PUT:/api/Book
        [HttpPut]
        [Route("{id:Guid}")]
        [Authorize]
        public async Task<IActionResult> UpdateBook([FromRoute] Guid id, UpdateBookRequestDto request){
            //from DTO to Domain model
            var book = new Book{
                Id = id,
                Title = request.Title,
                Author = request.Author,
                Publisher = request.Publisher,
                Category = request.Category,
                AvailableCopies = request.AvailableCopies,
                FeaturedImageUrl = request.FeaturedImageUrl
            };

            //call repository to update book info
            var updated = await bookRepository.UpdateAsync(book);
            
            //from domain model to dto
            var response = new BookDto{
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                Publisher = book.Publisher,
                Category = book.Category,
                AvailableCopies = book.AvailableCopies,
                FeaturedImageUrl = book.FeaturedImageUrl
                
            };

            return Ok(response);
        }
    } 
}