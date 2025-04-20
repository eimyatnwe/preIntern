using System;

namespace API.Models.DTO
{
    public class BorrowRecordDto
    {
        public Guid Id { get; set; } 
        public string BookTitle { get; set; } 
        public Guid MemberId { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public string Status { get; set; }
    }
}