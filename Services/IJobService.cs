using JobListingAPI.Models;

namespace JobListingAPI.Services
{
    public interface IJobService
    {
        Task<List<Job>> GetAllAsync(string? search, int page, int pageSize, string? sortBy, bool desc);

        Task<Job?> GetByIdAsync(int id);
        Task<Job> CreateAsync(Job job);
        Task<bool> UpdateAsync(int id, Job job);
        Task<bool> DeleteAsync(int id);
    }
}
