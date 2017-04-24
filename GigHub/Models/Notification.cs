using System;
using System.ComponentModel.DataAnnotations;

namespace GigHub.Models
{
    public class Notification
    {
        public int Id { get; private set; } 
        public DateTime DateTime { get; private set; }
        public NotificationType Type { get; private set; }
    
        //only need use when a gig is updated hence the nullable prop
        public DateTime? OriginalDateTime { get; private set; } 
        public string OriginalVenue { get; private set; }

        //each notfication is for one and only gig
        //using DA so that the corresponding column in the DB is not nullable
        [Required]
        public Gig Gig { get; private set; }

        private Notification(NotificationType type, Gig gig)
        {
            if (gig == null)
                throw new ArgumentNullException("gig");

            Type = type;
            Gig = gig;
            DateTime = DateTime.Now;
        }


        #region
        //factory methods that return a Notificiation ensuring object validity
        public static Notification GigCreated(Gig gig)
        {
            return new Notification(NotificationType.GigCreated, gig);
        }

        public static Notification GigUpdated(Gig newGig, DateTime originalDateTime, string originalVenue)
        {
            var notification = new Notification(NotificationType.GigUpdated, newGig);
            notification.OriginalDateTime = originalDateTime; //would get null reference error if not storing originals
            notification.OriginalVenue = originalVenue;
            return notification; 
        }

        public static Notification GigCanceled(Gig gig)
        {
            return new Notification(NotificationType.GigCanceled, gig);
        }
        #endregion
    }
}