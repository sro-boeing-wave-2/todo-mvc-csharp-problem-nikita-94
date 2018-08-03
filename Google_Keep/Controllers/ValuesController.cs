using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Web;
using Microsoft.AspNetCore.Http;
using Google_Keep.Models;

namespace Google_Keep.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
       static List<Notes> n = new List<Notes>() {
            new Notes(){   }

        };
        // GET api/values
        //[HttpGet]
        //public ActionResult<List<Notes>> GetAll()
        //{
        //    return n.ToList();
        //}

        //// GET api/values/5
        //[HttpGet("{id}",Name ="Notes")]
        //public ActionResult<List<Notes>> GetById(int id)
        //{
        //    var item = n.Find(id);
        //    if (item == null)
        //    {
        //        return NotFound();
        //    }
        //    return n.Find(id);
        //}

        //// POST api/values
        //[HttpPost]
        //public void Post([FromBody] Notes value)
        //{
        //    n.Add(value);
        //}

        //// PUT api/values/5
        //[HttpPut("{id}")]
        //public IActionResult Update(int id, [FromBody] Notes value)
        ////{
        ////    var todo = n.Find(id);
        ////    if (todo == null)
        ////    {
        ////        return NotFound();
        ////    }

        ////    todo.IsPinned = value.IsPinned;
        ////    todo.title = value.title;

        ////    n.Update(todo);
        ////    n.SaveChanges();
        ////    return NoContent();

        //}
        
        //// DELETE api/values/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
