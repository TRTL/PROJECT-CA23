using System.ComponentModel.DataAnnotations;

namespace PROJECT_CA23.Models.Dto.GenreDtos
{
    public class GenreDto
    {
        public GenreDto(Genre genre)
        {
            GenreId = genre.GenreId;
            Name = genre.Name;
        }

        /// <summary>
        /// Genre Id
        /// </summary>
        public int GenreId { get; set; }

        /// <summary>
        /// Name of media genre
        /// </summary>
        public string Name { get; set; }
    }
}
