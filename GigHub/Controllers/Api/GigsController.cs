using GigHub.Models;
using Microsoft.AspNet.Identity;
using System;
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
            
            //if calling the cancel method second time -> acting as record wouldn't exist anymore
            if (gig.IsCanceled)
                return NotFound();

            gig.IsCanceled = true;

            var notification = new Notification
            {
                DateTime = DateTime.Now,
                Gig = gig,
                Type = NotificationType.GigCanceled
            };

            var attendees = _context.Attendances
                //return Attendance objects for the gig
                .Where(a => a.GigId == gig.Id)
                //select Attendee that is type ApplicationUser
                .Select(a => a.Attendee)
                .ToList();

            //iterate over attendees and create
            //userNotification for each user as it is the 
            //instance of the notification for a particular user
            foreach (var attendee in attendees)
            {
                attendee.Notify(notification);
            }

            _context.SaveChanges();

            return Ok();
        }
    }
}
