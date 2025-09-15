using System.ComponentModel.DataAnnotations;

namespace NoticiasProvaIci.Models
{
    public class User
    {
        public Guid Id { get; init; }

        [Required, StringLength(250)]
        public string Name { get; set; } = null!;

        [Required, StringLength(50)]
        public string Password { get; set; } = null!;

        [Required, StringLength(250), EmailAddress]
        public string Email { get; set; } = null!;
    
        public ICollection<News> News { get; set; } = new List<News>();
    }
}