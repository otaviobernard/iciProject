using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace NoticiasProvaIci.Models
{
    public class News
    {
        public Guid Id { get; init; }

        [Required, StringLength(250)]
        public string Title { get; set; } = null!;

        [Required]
        public string Content { get; set; } = null!;

        public Guid UserId { get; set; }
        
        [ValidateNever]
        public User User { get; set; } = null!;

        public ICollection<NewsTag> NewsTag { get; set; } = new List<NewsTag>();
    }
}