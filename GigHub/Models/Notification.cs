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
        public DateTime? OriginalDateTime { get; set; } 
        public string OriginalVenue { get; set; }

        //each notfication is for one and only gig
        //using DA so that the corresponding column in the DB is not nullable
        [Required]
        public Gig Gig { get; private set; }

        public Notification(NotificationType type, Gig gig)
        {
            if (gig == null)
                throw new ArgumentNullException("gig");

            Type = type;
            Gig = gig;
            DateTime = DateTime.Now;
        }
       
    }
}