using Microsoft.IdentityModel.Tokens;
using PROJECT_CA23.Models;
using PROJECT_CA23.Repositories.IRepositories;
using PROJECT_CA23.Repositories.RepositoryServices.IRepositoryServices;

namespace PROJECT_CA23.Repositories.RepositoryServices
{
    public class GenreRepoService : IGenreRepoService
    {
        private readonly IGenreRepository _genreRepo;

        public GenreRepoService(IGenreRepository genreRepo)
        {
            _genreRepo = genreRepo;
        }

        public async Task<List<Genre>?> AddNewAndGetExistingGenres(string genresInString)
        {
            if (genresInString.IsNullOrEmpty()) return null;
            List<Genre> listOfGenres = new List<Genre>();

            var genreArr = genresInString.Split(", ");
            foreach (var genre in genreArr)
            {
                var foundGenre = await _genreRepo.FindByName(genre);
                if (foundGenre != null)
                {
                    listOfGenres.Add(foundGenre);
                }
                else
                {
                    var newGenre = new Genre()
                    {
                        Name = genre
                    };

                    await _genreRepo.CreateAsync(newGenre);                    
                    listOfGenres.Add(newGenre);
                }
            }
            return listOfGenres;
        }



    }
}
