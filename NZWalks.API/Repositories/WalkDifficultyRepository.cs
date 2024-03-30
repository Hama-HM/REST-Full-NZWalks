using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Repositories.IRepositories;

namespace NZWalks.API.Repositories
{
    public class WalkDifficultyRepository : IWalkDifficultyRepository
    {
        private readonly NZWalksDbContext _context;
        public WalkDifficultyRepository(NZWalksDbContext context)
        {
            _context = context;
        }
        public async Task<Difficulty> CreateAsync(Difficulty difficulty)
        {
            difficulty.Id = Guid.NewGuid();
            _context.Difficulties.Add(difficulty);
            await _context.SaveChangesAsync();

            return difficulty;
        }

        public async Task<Difficulty?> DeleteAsync(Guid id)
        {
            var existingDiffilculty =await _context.Difficulties.FirstOrDefaultAsync(x => x.Id == id);

            if (existingDiffilculty == null)
            { return null; }

            _context.Difficulties.Remove(existingDiffilculty);
            await _context.SaveChangesAsync();
            return existingDiffilculty;
        }

        public async Task<List<Difficulty>> GetAllAsync()
        {
            return await _context.Difficulties.ToListAsync();
        }

        public async Task<Difficulty?> GetByIdAsync(Guid id)
        {
            //return await _context.Difficulties.FindAsync(id);
            return await _context.Difficulties.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Difficulty?> UpdateAsync(Guid id, Difficulty difficulty)
        {
            var existingDifficulty = await _context.Difficulties.FirstOrDefaultAsync(x => x.Id == id);
            if (existingDifficulty == null)
            {
                return null;
            }
            existingDifficulty.Name = difficulty.Name;
            await _context.SaveChangesAsync();
            return existingDifficulty;
        }
    }
}
