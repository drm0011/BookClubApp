namespace BookClubApp.DTOs
{
    public class AddToReadingListDto
    {
        public int UserId { get; init; }

        public string Author { get; set; }

        public string Title { get; set; }

        public int PublishYear { get; set; }
    }
}
