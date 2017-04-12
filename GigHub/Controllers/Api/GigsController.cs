using GigHub.Models;
using Microsoft.AspNet.Identity;
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
            _context = new ApplicationDbContext();
        }

        //method called from client when clicked Cancel gig in UI using AJAX

        [HttpDelete]    //attribute because we want it the action only to be used with DELETE HTTP verb
        public IHttpActionResult Cancel(int id)
        {
            var userId = User.Identity.GetUserId();

            //getting gig from DB and making sure the user who created that gig
            //can cancel gig
            var gig = _context.Gigs.Single(g => g.Id == id && g.ArtistId == userId);

            gig.IsCanceled = true;

            _context.SaveChanges();

            return Ok();
        }
    }
}
