﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Google_Keep.Models;

namespace Google_Keep.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Notes1Controller : ControllerBase
    {
        private readonly Google_KeepContext _context;

        public Notes1Controller(Google_KeepContext context)
        {
            _context = context;
        }

        // GET: api/Notes1
        [HttpGet]
        public IEnumerable<Notes> GetNotes()
        {
            return _context.Notes.Include(n=>n.check).Include(n=>n.label);
        }

        // GET: api/Notes1/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetNotes([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var notes = await _context.Notes.FindAsync(id);

            if (notes == null)
            {
                return NotFound();
            }

            return Ok(notes);
        }

        // PUT: api/Notes1/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNotes([FromRoute] int id, [FromBody] Notes notes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != notes.id)
            {
                return BadRequest();
            }

            _context.Entry(notes).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NotesExists(id))
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

        // POST: api/Notes1
        [HttpPost]
        public async Task<IActionResult> PostNotes([FromBody] Notes notes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Notes.Add(notes);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNotes", new { id = notes.id }, notes);
        }

        // DELETE: api/Notes1/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNotes([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var notes = await _context.Notes.FindAsync(id);
            if (notes == null)
            {
                return NotFound();
            }

            _context.Notes.Remove(notes);
            await _context.SaveChangesAsync();

            return Ok(notes);
        }

        private bool NotesExists(int id)
        {
            return _context.Notes.Any(e => e.id == id);
        }
    }
}