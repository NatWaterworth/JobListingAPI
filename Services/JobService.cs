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

        public async Task<List<Job>> GetAllAsync(string? search, int page, int pageSize, string? sortBy, bool desc)
        {
            var query = _context.Jobs.AsQueryable();

            query = ApplySearch(query, search);
            query = ApplySorting(query, sortBy, desc);
            query = ApplyPagination(query, page, pageSize);

            return await query.ToListAsync();
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

        private IQueryable<Job> ApplySearch(IQueryable<Job> query, string? search)
        {
            if (string.IsNullOrWhiteSpace(search)) return query;

            return query.Where(j =>
                j.Title.Contains(search) ||
                j.Description.Contains(search) ||
                j.Company.Contains(search));
        }

        private IQueryable<Job> ApplySorting(IQueryable<Job> query, string? sortBy, bool desc)
        {
            return sortBy?.ToLower() switch
            {
                "title" => desc ? query.OrderByDescending(j => j.Title) : query.OrderBy(j => j.Title),
                "company" => desc ? query.OrderByDescending(j => j.Company) : query.OrderBy(j => j.Company),
                "salary" => desc ? query.OrderByDescending(j => j.Salary) : query.OrderBy(j => j.Salary),
                "description" => desc ? query.OrderByDescending(j => j.Description) : query.OrderBy(j => j.Description),
                "dateposted" => desc ? query.OrderByDescending(j => j.DatePosted) : query.OrderBy(j => j.DatePosted),
                _ => query.OrderBy(j => j.Id) // Default
            };
        }

        private IQueryable<Job> ApplyPagination(IQueryable<Job> query, int page, int pageSize)
        {
            return query.Skip((page - 1) * pageSize).Take(pageSize);
        }
    }
}
