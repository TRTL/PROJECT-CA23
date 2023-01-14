using PROJECT_CA23.Models;

namespace PROJECT_CA23.Repositories.IRepositories
{
    public interface IGenreRepository : IRepository<Genre>
    {
        Task<List<Genre>?> AddNewAndGetExistingGenresOfThisMedia(string genreString);
    }
}
