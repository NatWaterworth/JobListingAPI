using JobListingAPI.Models;

namespace JobListingAPI.Services
{
    public interface IJobService
    {
        Task<List<Job>> GetAllAsync();
        Task<Job?> GetByIdAsync(int id);
        Task<Job> CreateAsync(Job job);
        Task<bool> UpdateAsync(int id, Job job);
        Task<bool> DeleteAsync(int id);
    }
}
