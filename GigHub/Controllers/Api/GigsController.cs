using GigHub.Models;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;

namespace GigHub.Controllers.Api
{
    [Authorize]
    public class GigsController : ApiController
    {
        
        private ApplicationDbContext _context;

        public GigsController()
        {
            _context= new ApplicationDbContext();
        }

        [HttpDelete]//decorating because we want to use it only HTTP verb Delete
        public IHttpActionResult Cancel(int id)
        {
            var userId = User.Identity.GetUserId();
            var gig = _context.Gigs
                //eager loading using lambda expression - System.Data.Entity
                //and getting the attendee from the collection of attendances
                .Include(g => g.Attendances.Select(a => a.Attendee))
                .Single(g => g.Id == id && g.ArtistId == userId);
            
            //although it's a logical delete we should behave as it's a physical delete
            if (gig.IsCanceled)
            {
                return NotFound();
            }

            gig.Cancel();
                                   
            _context.SaveChanges();
            return Ok();
        }
    }
}
