using JobListingAPI.Models;
using JobListingAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace JobListingAPI.Services
{
    public class JobService : IJobService
    {
        private readonly AppDbContext _context;

        public JobService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Job>> GetAllAsync()
        {
            return await _context.Jobs.ToListAsync();
        }

        public async Task<Job?> GetByIdAsync(int id)
        {
            return await _context.Jobs.FindAsync(id);
        }

        public async Task<Job> CreateAsync(Job job)
        {
            _context.Jobs.Add(job);
            await _context.SaveChangesAsync();
            return job;
        }

        public async Task<bool> UpdateAsync(int id, Job job)
        {
            if (id != job.Id)
                return false;

            _context.Entry(job).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var job = await _context.Jobs.FindAsync(id);
            if (job == null)
                return false;

            _context.Jobs.Remove(job);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
