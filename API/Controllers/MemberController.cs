using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API.Models.DTO;
using API.Models.Domain;
using API.Data;
using API.Repositories.Interfaces;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;


namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly IMembersRepository memberRepository;
        public MemberController(IMembersRepository memberRepository){
            this.memberRepository = memberRepository;
        }

        //GET:/api/member
        [HttpGet]
        public async Task<IActionResult> GetAllMembers(){
            var members = await memberRepository.GetAllAsync();
            var response = new List<MemberDto>();
            foreach (var member in members)
            {
                var record = member.BorrowRecords?.Select(br => new BorrowRecordDto{
                    Id = br.Id,
                    BookTitle = br.BookTitle ?? "",
                    MemberId = br.MemberId,
                    BorrowDate = br.BorrowDate,
                    DueDate = br.DueDate,
                    Status = br.Status,
                }).ToList() ?? new List<BorrowRecordDto>(); 

                response.Add(new MemberDto{
                    Id = member.Id,
                    Name = member.Name,
                    Email = member.Email,
                    BorrowRecords = record
                });
            }

            return Ok(response);
        }

        

    }
}