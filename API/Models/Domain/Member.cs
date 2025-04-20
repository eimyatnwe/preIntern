namespace API.Models.Domain
{
    public class Member
    {
        public Guid Id {get;set;}
        public string Name {get;set;}
        public string Email {get;set;}
        public ICollection<BorrowRecord> BorrowRecords { get; set; }
    }
}