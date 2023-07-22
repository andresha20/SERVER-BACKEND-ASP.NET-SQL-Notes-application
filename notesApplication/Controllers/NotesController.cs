using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using notesApplication.Models;
using System.Net;

namespace notesApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotesController : ControllerBase
    {
        private readonly NoteContext note_Context;
        public NotesController(NoteContext noteContext)
        {
            note_Context = noteContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Note>>> GetNotes()
        {
            if (note_Context.Notes == null)
            {
                return NotFound();
            }
            return await note_Context.Notes.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Note>> GetNote(int id)
        {
            try
            {
                if (note_Context.Notes == null)
                {
                    return NotFound();
                }
                var note = await note_Context.Notes.FindAsync(id);
                if (note == null)
                {
                    return NotFound();
                }
                return note;
            } catch(Exception ex) {
                throw new System.Web.Http.HttpResponseException(HttpStatusCode.NotFound);
            }        
        }

        [HttpPost]
        public async Task<ActionResult<Note>> PostNote(Note note)
        {
            try
            {
                note_Context.Notes.Add(note);
                await note_Context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetNote), new { id = note.ID }, note);
            } catch (Exception ex)
            {
                throw new System.Web.Http.HttpResponseException(HttpStatusCode.NotFound);

            }

        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Note>> EditNote(Note note, int id)
        {
            if (id != note.ID)
            {
                return BadRequest();
            }
            note_Context.Entry(note).State = EntityState.Modified;
            try
            {
                await note_Context.SaveChangesAsync();
                return Ok();
            } catch(Exception ex)
            {
                throw;
            }
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteNote(int id)
        {
            try
            {
                if (note_Context.Notes == null)
                {
                    return NotFound();
                }
                var note = await note_Context.Notes.FindAsync(id);
                if (note == null)
                {
                    return NotFound();
                }
                note_Context.Notes.Remove(note);
                await note_Context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                throw new System.Web.Http.HttpResponseException(HttpStatusCode.NotFound);
            }
        }

    }
}
