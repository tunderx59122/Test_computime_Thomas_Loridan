using Microsoft.EntityFrameworkCore;
using template_API.Data;
using template_API.Models;

namespace template_API.Services
{
    public class SampleService
    {
        private readonly SampleDbContext _dbContext;

        public SampleService(SampleDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool Post(Sample sample)
        {
            this._dbContext.Samples.Add(sample);
            return _dbContext.Save();

        }

        public Task<Sample> GetById(int Id)
        {
            var sample = this._dbContext.Samples.FirstOrDefaultAsync(x => x.Id == Id);
            return sample;
        }
        public Task<List<Sample>> GetAll()
        {
            var samples = this._dbContext.Samples.ToListAsync();
            return samples;
        }


        public async Task<Sample?> Update(Sample sample)
        {
            var existingSample = await _dbContext.Samples.FindAsync(sample.Id);
            if (existingSample == null) return null;

            existingSample.Name = sample.Name; // Exemple de mise à jour d'une propriété
            await _dbContext.SaveChangesAsync();

            return existingSample;
        }

        public async Task<bool> Delete(int id)
        {
            var sample = await _dbContext.Samples.FindAsync(id);
            if (sample == null) return false;

            _dbContext.Samples.Remove(sample);
            await _dbContext.SaveChangesAsync();

            return true;
        }




    }
}