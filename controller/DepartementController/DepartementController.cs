using HRIS.data;
using HRIS.models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HRIS.Controller
{
    [ApiController]
    [Route("api")]

    public class DepartmentController : ControllerBase
    {
        private readonly DataContextEF _db;
        public DepartmentController(DataContextEF db)
        {
            _db = db;
        }

        [Authorize(Roles = "HRD")]
        [HttpPost("departement")]
        public async Task<IActionResult> CreateDepartement(DepartmentDto dto)
        {
            if (await _db.Department.AnyAsync(u => u.Name == dto.Name))
            {
                return BadRequest("Nama Departement Sudah dibuat");
            }

            Department department = new Department
            {
                Name = dto.Name,
                Location = dto.Location
            };

            _db.Department.Add(department);
            await _db.SaveChangesAsync();

            return Ok(new
            {
                status = true,
                statusCode = 200,
                message = "Berhasil membuat departement",
                data = new
                {
                    department.Name,
                    department.Location,
                }
            });

        }

        [Authorize]
        [HttpGet("departement")]
        public async Task<ActionResult<object>> GetDepartements()
        {
            List<Department> departements = await _db.Department.ToListAsync();

            return Ok(new
            {
                status = true,
                statusCode = 200,
                message = "Berhasil mengambil daftar departement",
                data = departements.Select(departement => new
                {
                    departement.DepID,
                    departement.Name,
                    departement.Location,
                })
            });
        }

        [Authorize]
        [HttpGet("departement/{id}")]
        public async Task<ActionResult<object>> GetDetailDepartements(int id)
        {
            Department? department = await _db.Department.FindAsync(id);

            if (department == null)
            {
                return NotFound(new
                {
                    status = false,
                    statusCode = 404,
                    message = "Departemnt tidak ditemukan"
                });
            }

            return Ok(new
            {
                status = true,
                statusCode = 200,
                message = "Berhasil mengambil Data Department",
                data = new
                {
                    department.DepID,
                    department.Name,
                    department.Location,
                }
            });
        }

        [Authorize(Roles = "HRD")]
        [HttpPut("departement/{id}")]
        public async Task<ActionResult<object>> EditDepartment(int id, DepartmentDto dto)
        {
            Department? department = await _db.Department.FindAsync(id);

            if (department == null)
            {
                return NotFound(new
                {
                    status = false,
                    statusCode = 404,
                    message = "Departemnt tidak ditemukan"
                });
            }

            department.Name = dto.Name;
            department.Location = dto.Location;

            _db.Department.Update(department);

            await _db.SaveChangesAsync();

            return Ok(new
            {
                status = true,
                statusCode = 200,
                message = "Berhasil mengambil Update Data Department",
                data = new
                {
                    department.Name,
                    department.Location,
                }
            });

        }
    }
}
