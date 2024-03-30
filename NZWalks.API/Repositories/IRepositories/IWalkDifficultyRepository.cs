using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories.IRepositories
{
    public interface IWalkDifficultyRepository
    {
        Task<List<Difficulty>> GetAllAsync();

        Task<Difficulty?> GetByIdAsync(Guid id);

        Task<Difficulty> CreateAsync(Difficulty difficulty);

        Task<Difficulty?> UpdateAsync(Guid id, Difficulty difficulty);

        Task<Difficulty?> DeleteAsync(Guid id);
    }
}
