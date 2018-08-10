using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Google_Keep.Models;
using Microsoft.EntityFrameworkCore;
namespace Google_Keep.Controllers
{
    [Route("api/Notes1")]
    [ApiController]
    public class Notes1Controller : ControllerBase
    {
        private readonly Google_KeepContext _context;

        public Notes1Controller(Google_KeepContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetNotes([FromQuery] string title, [FromQuery] string label, [FromQuery] bool? pinned)
        {
            var result = await _context.Note.Include(n => n.checks).Include(n => n.label)
                .Where(x => ((title == null || x.title == title) && (label == null || x.label.Exists(y => y.label == label)) && (pinned == null || x.IsPinned == pinned))).ToListAsync();
            return Ok(result);
        }
        //[Route("api/Notes1/GetAll")]
        //[HttpGet]
        //public async Task<IActionResult> GetAllNotes()
        //{
        //    var res = _context.Notes.Include(n => n.checks).Include(n => n.label);
        //    var reslist = res.ToList();
        //    return Ok(reslist);
        //}

        // GET: api/Notes1
        //[HttpGet]
        // public IEnumerable<Notes> GetNotes()
        // {
        //     return _context.Notes.Include(n => n.checks).Include(n => n.label);
        // }

        // GET: api/Notes1/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetNoteById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var notes = await _context.Note.FindAsync(id);

            if (notes == null)
            {
                return NotFound();
            }

            return Ok(notes);
        }

        // PUT: api/Notes1/5
        [HttpPut("EDIT/{id}")]
        public async Task<IActionResult> PutNotes([FromRoute] int id, [FromBody] Note notes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != notes.id)
            {
                return BadRequest();
            }

            _context.Note.Update(notes);
           // _context.Entry(notes).State = EntityState.Modified;
            // using(var entity= new NotesDBEntities)
           
            try
            {
                await _context.SaveChangesAsync();

                //using(var db=new MyContextDB())

            }
            catch (DbUpdateConcurrencyException e)
            {
                if (!NoteExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(notes);
        }

        // POST: api/Notes1
        [HttpPost]
        public async Task<IActionResult> PostNotes([FromBody] Note notes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Note.Add(notes);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNotesByID", new { id = notes.id }, notes);
        }

        // DELETE: api/Notes1/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNotes([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var notes = await _context.Note.Include(n=>n.checks).Include(n=>n.label).SingleOrDefaultAsync(c=>c.id==id);
            if (notes == null)
            {
                return NotFound();
            }

            _context.Note.Remove(notes);
            await _context.SaveChangesAsync();

            return Ok(notes);
        }
        [HttpDelete("DEL/{title}")]
        public async Task<IActionResult> Delete([FromRoute] string title)
        {
            //var result = await _context.Notes.Include(n => n.checks).Include(n => n.label)
            //   .Where(x => ((title == null || x.plain_text == title) && 
            //   (Label == null || x.label.Any(y => y.label == Label)) && 
            //   (pinned == null || x.pinned == pinned))).ToListAsync();
            var result = await _context.Note.Include(n => n.checks).Include(n => n.label)
                .Where(x => ((title == null || x.title == title))).ToListAsync();
            foreach (var note in result)
            {
                _context.Note.Remove(note);
            }
            await _context.SaveChangesAsync();
            return Ok();
            //DbContext db = new DbContext();
            //Notes n = db.notes.Single(p => p.title == title); // Find the item to remove

            //db.notes.Remove(n); // Remove from the context

            //db.SaveChanges();
            //  _context.Entry(title).State = EntityState.Deleted;

            //DatabaseEntities obj = new DatabaseEntities();
            //obj.notes.Where(x => x.title == title).ToList().ForEach(obj.notes.DeleteObject);
            //obj.SaveChanges();
            //_context.Notes.Include(n => n.checks).Include(n => n.label).Where(w => w.title == title).Remove();

            //_context.Notes.Where(p => p.title == title)
            //   .ToList().ForEach(p => _context.checks.Remove(p));
            // _context.Notes.Update();
            //using (var ctx = new Google_KeepContext())
            //{
            //    var list = _context.Notes.Where(x => ((title == null || x.title == title))).ToListAsync();
            //    ctx.Notes.RemoveRange(list);
            //    ctx.SaveChanges();
            //}
        }

        private bool NoteExists(int id)
        {
            return _context.Note.Any(e => e.id == id);
        }
    }
}