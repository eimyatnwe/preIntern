namespace API.Models.DTO
{
    public class UpdateBookRequestDto
    {
        
        public string Title {get; set;}
        public string Author {get;set;}
        public string Publisher{get;set;}
        public string Category{get;set;}
        public int AvailableCopies{get;set;}
        public string FeaturedImageUrl{get;set;}
        // public ICollection<BorrowRecord> BorrowRecords{get;set;}
    }
}