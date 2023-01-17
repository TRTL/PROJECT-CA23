using PROJECT_CA23.Models;

namespace PROJECT_CA23.Repositories.RepositoryServices.IRepositoryServices
{
    public interface IGenreRepoService
    {
        Task<List<Genre>?> AddNewAndGetExistingGenres(string genresInString);
    }
}
