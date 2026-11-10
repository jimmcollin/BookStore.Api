using BookStore.Api.Enums;

namespace BookStore.Api.Models.Dtos
{
    public class BookDto
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public required string Author { get; set; }
        public required string Isbn { get; set; }
        public int? PublishedYear { get; set; }
        public string? CoverUrl { get; set; }
        public string? OpenLibraryKeys { get; set; }
        public string? Description { get; set; }
        public Genre Genre { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }
}
