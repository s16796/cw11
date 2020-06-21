using cw11.models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cw11.Controllers
{

    [Route("api/clinic")]
    [ApiController]
    public class ClinicController : ControllerBase
    {

        private readonly ClinicDBContext _context;
        public ClinicController(ClinicDBContext context)
        {
            _context = context;
        }



        /*
         * "która pozwoli nam pobierać dane lekarze"
         * Nie wiem czy to oznacza jednego czy wszystkich wiec implementuje obydwa
         */
        [HttpGet]
        public IActionResult GetAlldoctors()
        {
            var output = _context.Doctors.ToList();
            return Ok(output);
        }

        [Route("get")]
        [HttpGet("{id}")]
        public IActionResult Getdoctor([FromQuery] int id)
        {
            var output = _context.Doctors.Where(doc => doc.IdDoctor.Equals(id)).FirstOrDefault();
            if(output != null)
            {
                return Ok(output);
            }
            else
            {
                return NotFound("No doctor with that id");
            }
        }

        [Route("add")]
        [HttpPost]
        public IActionResult AddDoctor(Doctor doctor)
        {
            if(doctor.FirstName == null && doctor.LastName == null && doctor.Email == null)
            {
                return BadRequest("Don't enter empty entities");
            }
            else
            {
                _context.Add(doctor);
                _context.SaveChanges();
                return Ok(doctor);
            }
        }

        [Route("modify")]
        [HttpPost("{id}")]
        public IActionResult ModifyDoctor([FromQuery] int id, [FromBody] Doctor modificationsDoctor)
        {
            var output = _context.Doctors.Where(doc => doc.IdDoctor.Equals(id)).FirstOrDefault();
            if (output != null)
            {
                if (modificationsDoctor.Email != null)
                {
                    output.Email = modificationsDoctor.Email;
                }
                if (modificationsDoctor.FirstName != null)
                {
                    output.FirstName = modificationsDoctor.FirstName;
                }
                if (modificationsDoctor.LastName != null)
                {
                    output.LastName = modificationsDoctor.LastName;
                }
                _context.SaveChanges();
                return Ok(output);
            }
            else
            {
                return NotFound("No doctor with that id");
            }
        }

        [Route("delete")]
        [HttpDelete("{id}")]
        public IActionResult DeleteDoctor([FromQuery] int id)
        {
            var output = _context.Doctors.Where(doc => doc.IdDoctor.Equals(id)).FirstOrDefault();
            if(output != null)
            {
                _context.Remove(output);
                _context.SaveChanges();
                return Ok(output);
            }
            else
            {
                return NotFound("No doctor with that id");
            }
        }

    }
}
