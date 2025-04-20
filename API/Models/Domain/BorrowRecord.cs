namespace API.Models.Domain
{
    public class BorrowRecord
    {
        public Guid Id { get; set; }
        public string BookTitle { get; set; }
        public Guid MemberId { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime DueDate { get; set; }
        public string Status { get; set; }
    }
}