using GigHub.Models;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;

namespace GigHub.Controllers.Api
{
    [Authorize]
    public class NotificationsController : ApiController
    {
       
        private ApplicationDbContext _context;

        public NotificationsController()
        {
            _context = new ApplicationDbContext(); 
        }


        public IEnumerable <NotificationDto> getNewNotifications()
        {
            var userId = User.Identity.GetUserId();

            var notifications = _context.UserNotifications
               .Where(un => un.UserId == userId)
               .Select(un => un.Notification)
               .Include(n => n.Gig.Artist)
               .ToList(); 

            //Select extension method
            //mapping notifciations to ndtos
            return notifications.Select(n => new NotificationDto() //lmd exp. to repr. anonym.() -- method takes not.obj. returns dto
            {
                DateTime = n.DateTime,
                Gig = new GigDto
                {
                    Artist = new UserDto
                    {
                        Id = n.Gig.ArtistId,
                        Name = n.Gig.Artist.Name
                    },

                    DateTime = n.Gig.DateTime,
                    Id = n.Gig.Id,
                    IsCanceled= n.Gig.IsCanceled,
                    Venue = n.Gig.Venue
                },

                OriginalDateTime=n.OriginalDateTime,
                OriginalVenue = n.OriginalVenue,
                Type=n.Type
            }
            );



        }
           
    }
}
