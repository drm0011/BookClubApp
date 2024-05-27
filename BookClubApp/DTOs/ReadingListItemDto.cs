namespace BookClubApp.DTOs
{
    public class ReadingListItemDto
    {
        public int Id { get; init; }

        public string Author { get; set; }

        public string Title { get; set; }

        public int PublishYear { get; set; }

    }
}
