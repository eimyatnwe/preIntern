namespace API.Models.DTO{
    public class MemberDto
    {
        public Guid Id {get;set;}
        public string Name {get;set;}
        public string Email {get;set;}
        public List<BorrowRecordDto> BorrowRecords {get;set;} 
    }
}