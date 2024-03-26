using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DashboardApi.Data;
using DashboardApi.Models;
using DashboardApi.Dtos;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using AutoMapper;

namespace DashboardApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SectionsController : ControllerBase
    {
        private readonly Context _context;
        IMapper _mapper;

        ISectionRepo _sectionRepo;

        public SectionsController(IConfiguration context, ISectionRepo sectionRepo)
        {
            _context = new Context(context);

            _sectionRepo = sectionRepo;

            _mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<SectionDto, Section>();
            }));

        }

        // GET: api/Sections
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Section>>> GetSections()
        {
            return await _context.Sections.ToListAsync();
        }

        // GET: api/Sections/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Section>> GetSection(long id)
        {
            var section = await _context.Sections.FindAsync(id);

            if (section == null)
            {
                return NotFound();
            }

            return section;
        }

        // PUT: api/Sections/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSection(long id, Section section)
        {
            if (id != section.Id)
            {
                return BadRequest();
            }

            _context.Entry(section).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SectionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Sections
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Section>> PostSection(SectionDto section)
        {
            Section sectionDb = _mapper.Map<Section>(section);

            _context.Sections.Add(sectionDb);
            await _context.SaveChangesAsync();
            // calls GetSection with the id of the object and returns sectionDb
            return CreatedAtAction(nameof(GetSection), new { id = sectionDb.Id }, sectionDb);
        }

        // DELETE: api/Sections/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSection(long id)
        {
            var section = await _context.Sections.FindAsync(id);
            if (section == null)
            {
                return NotFound();
            }

            _context.Sections.Remove(section);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SectionExists(long id)
        {
            return _context.Sections.Any(e => e.Id == id);
        }
    }
}
