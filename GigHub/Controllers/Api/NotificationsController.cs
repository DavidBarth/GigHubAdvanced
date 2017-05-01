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

            


            //returns a notificationDto to hide internalities of business objects
            //lamda exp. replaced with a reference of Map method of Mapper class
            return notifications.Select(AutoMapper.Mapper.Map<Notification, NotificationDto>);

        }
           
    }
}
