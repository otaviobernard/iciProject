namespace NoticiasProvaIci.Models
{
    public class NewsTag
    {
        public Guid Id { get; init; }

        public Guid NewsId { get; set; }
        public News News { get; set; } = null!;

        public Guid TagId { get; set; }
        public Tag Tag { get; set; } = null!;
    }
}