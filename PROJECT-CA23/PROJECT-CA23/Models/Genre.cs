using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace PROJECT_CA23.Models
{
    public class Genre
    {
        public int GenreId { get; set; }
        public string Name { get; set; }
        public virtual List<Media> Medias { get; set; }
    }
}
