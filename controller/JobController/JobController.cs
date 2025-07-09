using HRIS.data;
using HRIS.models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HRIS.Controller
{
    [ApiController]
    [Route("api")]

    public class JobController : ControllerBase
    {

        private readonly DataContextEF _db;
        public JobController(DataContextEF db)
        {
            _db = db;
        }

        [Authorize(Roles = "HRD")]
        [HttpPost("job")]
        public async Task<IActionResult> CreateJob(JobDTO dto)
        {

            if (await _db.Job.AnyAsync(u => u.Title == dto.Title))
            {
                return BadRequest("Judul Pekerjaan Sudah Dibuat");
            }

            Job job = new Job
            {
                Title = dto.Title,
                Description = dto.Description,
                SalaryRange = dto.SalaryRange
            };

            _db.Job.Add(job);
            await _db.SaveChangesAsync();
            return Ok(new
            {
                status = true,
                statusCode = 200,
                message = "Berhasil membuat pekerjaan",
                data = new
                {
                    job.Title,
                    job.Description,
                    job.SalaryRange
                }
            });
        }

        [Authorize]
        [HttpGet("jobs")]
        public async Task<ActionResult<object>> GetJobs()
        {
            List<Job> jobs = await _db.Job.ToListAsync();

            return Ok(new
            {
                status = true,
                statusCode = 200,
                message = "Berhasil mengambil daftar pekerjaan",
                data = jobs.Select(job => new
                {
                    job.JobID,
                    job.Title,
                    job.Description,
                    job.SalaryRange,
                    job.Employees
                })
            });
        }

        [Authorize]
        [HttpGet("job/{id}")]
        public async Task<ActionResult<object>> GetDetailJob(int id)
        {
            Job? job = await _db.Job.FindAsync(id);

            if (job == null)
            {
                return NotFound(new
                {
                    status = false,
                    statusCode = 404,
                    message = "Pekerjaan tidak ditemukan"
                });
            }

            return Ok(new
            {
                status = true,
                statusCode = 200,
                message = "Detail pekerjaan ditemukan",
                data = new
                {
                    job.JobID,
                    job.Title,
                    job.Description,
                    job.SalaryRange
                }
            });
        }

        [Authorize(Roles = "HRD")]
        [HttpPut("job/{id}")]
        public async Task<ActionResult<object>> EditJob(int id, JobDTO dto)
        {
            Job? job = await _db.Job.FindAsync(id);

            if (job == null)
            {
                return NotFound(new
                {
                    status = false,
                    statusCode = 404,
                    message = "Pekerjaan tidak ditemukan"
                });
            }

            // Update data dari DTO
            job.Title = dto.Title;
            job.Description = dto.Description;
            job.SalaryRange = dto.SalaryRange;

            _db.Job.Update(job);
            await _db.SaveChangesAsync();

            return Ok(new
            {
                status = true,
                statusCode = 200,
                message = "Data pekerjaan berhasil diperbarui",
                data = new
                {
                    job.JobID,
                    job.Title,
                    job.Description,
                    job.SalaryRange
                }
            });

        }
    }

}