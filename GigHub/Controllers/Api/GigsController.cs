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
            _context= new ApplicationDbContext();
        }

        [HttpDelete]//decorating because we want to use it only HTTP verb Delete
        public IHttpActionResult Cancel(int id)
        {
            var userId = User.Identity.GetUserId();
            var gig = _context.Gigs.Single(g => g.Id == id && g.ArtistId == userId);
            
            //although it's a logical delete we should behave as it's a physical delete
            if (gig.IsCanceled)
            {
                return NotFound();
            }

            gig.IsCanceled = true;

            //When gig is canceled create Notification
            var notificaion = new Notification()
            {
                DateTime = DateTime.Now,
                Gig = gig,
                Type = NotificationType.GigCanceled,
            };

            //getting all attendees for the canceled gig
            var attendees = _context.Attendances
                .Where(a => a.GigId == gig.Id) //this query returns attendance objects for the given gig
                .Select(a => a.Attendee)    //selects attendee that is the ApplicationUser object
                .ToList();

            //iterate over the collection of attendees
            //and create a userNotification object for each user 
            //as it is the instance of the notfication for a particular user
            foreach (var attendee in attendees)
            {
                var userNotification = new UserNotification()
                {
                    User = attendee,
                    Notification = notificaion
                };

                _context.UserNotifications.Add(userNotification);
            }


            _context.SaveChanges();
            return Ok();
        }
    }
}
