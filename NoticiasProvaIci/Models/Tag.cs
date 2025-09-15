using System.ComponentModel.DataAnnotations;

namespace NoticiasProvaIci.Models
{
    public class Tag
    {
        public Guid Id { get; init; }

        [Required, StringLength(100)]
        public string Name { get; set; } = null!;

        public ICollection<NewsTag> NewsTag { get; set; } = new List<NewsTag>();
    }
}