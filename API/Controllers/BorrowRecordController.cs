using Microsoft.AspNetCore.Mvc;
using API.Models.DTO;
using API.Models.Domain;
using API.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;


namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowRecordController : ControllerBase
    {
        private readonly IBorrowRecordRepository borrowRecordRepository;
        private readonly IBooksRepository booksRepository;
        private readonly IMembersRepository memberRepository;

        public BorrowRecordController(IBorrowRecordRepository borrowRecordRepository, IBooksRepository booksRepository, IMembersRepository memberRepository)
        {
            this.borrowRecordRepository = borrowRecordRepository;
            this.booksRepository = booksRepository;
            this.memberRepository = memberRepository;
        }
        

        [HttpPost]
        public async Task<IActionResult> CreateBorrowRecord([FromBody] BorrowRecordDto borrowRecordDto)
        {
            

            var borrowRecord = new BorrowRecord
            {
                Id = Guid.NewGuid(),
                BookTitle = borrowRecordDto.BookTitle,
                MemberId = borrowRecordDto.MemberId,
                BorrowDate = DateTime.UtcNow,
                DueDate = DateTime.UtcNow.AddDays(14),
                Status = "Borrowed"
            };

            await borrowRecordRepository.CreateAsync(borrowRecord);
            return Ok(borrowRecord);
        }

        
        [HttpGet]
        
        public async Task<IActionResult> GetAllBorrowRecords()
        {
            var borrowRecords = await borrowRecordRepository.GetAllAsync();
            var response = new List<BorrowRecordDto>();
            foreach (var record in borrowRecords)
            {
                response.Add(new BorrowRecordDto
                {
                    Id = record.Id,
                    BookTitle = record.BookTitle,
                    MemberId = record.MemberId,
                    BorrowDate = record.BorrowDate,
                    DueDate = record.DueDate,
                    Status = record.Status
                });
            }

            return Ok(response);
        }


    }
}