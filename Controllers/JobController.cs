using Microsoft.AspNetCore.Mvc;
using JobListingAPI.Models;
using JobListingAPI.Services;
using JobListingAPI.DTOs;

namespace JobListingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private readonly IJobService _jobService;

        public JobController(IJobService jobService)
        {
            _jobService = jobService;
        }

        // GET: api/Jobs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Job>>> GetJobs(
            string? search = null,
            int page = 1,
            int pageSize = 10,
            string? sortBy = null,
            bool desc = false)
        {
            var jobs = await _jobService.GetAllAsync(search, page, pageSize, sortBy, desc);
            return Ok(jobs);
        }


        // GET: api/Jobs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Job>> GetJob(int id)
        {
            var job = await _jobService.GetByIdAsync(id);
            if (job == null)
            {
                return NotFound();
            }
            return job;
        }

        // PUT: api/Jobs/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutJob(int id, UpdateJobDto dto)
        {
            var existingJob = await _jobService.GetByIdAsync(id);
            if (existingJob == null)
                return NotFound();

            // Map DTO to existing job
            existingJob.Title = dto.Title;
            existingJob.Description = dto.Description;
            existingJob.Company = dto.Company;
            existingJob.Salary = dto.Salary;

            var success = await _jobService.UpdateAsync(id, existingJob);
            if (!success)
                return NotFound();

            return NoContent();
        }


        [HttpPost]
        public async Task<ActionResult<Job>> PostJob(CreateJobDto dto)
        {
            var job = new Job
            {
                Title = dto.Title,
                Description = dto.Description,
                Company = dto.Company,
                Salary = dto.Salary,
                DatePosted = DateTime.UtcNow
            };

            var createdJob = await _jobService.CreateAsync(job);
            return CreatedAtAction(nameof(GetJob), new { id = createdJob.Id }, createdJob);
        }


        // DELETE: api/Jobs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJob(int id)
        {
            var success = await _jobService.DeleteAsync(id);
            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
