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
using AutoMapper;

namespace DashboardApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SectionsController : ControllerBase
    {
        private readonly ISectionRepo _sectionRepo;
        private readonly IMapper _mapper;

        public SectionsController(ISectionRepo sectionRepo, IMapper mapper)
        {
            _sectionRepo = sectionRepo;
            _mapper = mapper;
        }

        // GET: api/Sections
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Section>>> GetSections()
        {
            var sections = await _sectionRepo.GetSections();
            return Ok(sections);
        }

        // GET: api/Sections/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Section>> GetSection(long id)
        {
            var section = await _sectionRepo.GetSectionById(id);
            if (section == null)
            {
                return NotFound();
            }
            return section;
        }

        // PUT: api/Sections/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSection(long id, SectionDto sectionDto)
        {
            var section = await _sectionRepo.GetSectionById(id);
            if (section == null)
            {
                return NotFound();
            }

            _mapper.Map(sectionDto, section);

            try
            {
                await _sectionRepo.UpdateSection(section);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _sectionRepo.SectionExists(id))
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
        public async Task<ActionResult<Section>> PostSection(SectionDto sectionDto)
        {
            var section = _mapper.Map<Section>(sectionDto);
            await _sectionRepo.AddSection(section);

            return CreatedAtAction(nameof(GetSection), new { id = section.Id }, section);
        }

        // DELETE: api/Sections/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSection(long id)
        {
            var section = await _sectionRepo.GetSectionById(id);
            if (section == null)
            {
                return NotFound();
            }

            await _sectionRepo.DeleteSection(section);

            return NoContent();
        }
    }
}
