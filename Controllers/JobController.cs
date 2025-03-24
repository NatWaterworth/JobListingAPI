using Microsoft.AspNetCore.Mvc;
using JobListingAPI.Models;
using JobListingAPI.Services;

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
        public async Task<IActionResult> PutJob(int id, Job job)
        {
            if (id != job.Id)
            {
                return BadRequest();
            }

            var success = await _jobService.UpdateAsync(id, job);
            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/Jobs
        [HttpPost]
        public async Task<ActionResult<Job>> PostJob(Job job)
        {
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
