using System;
using System.ComponentModel.DataAnnotations;

namespace GigHub.Models
{
    //class to representa notification
    public class Notification
    {
        public int Id { get; private set; }

        public DateTime DateTime { get; private set; }

        public NotificationType Type { get; private set; }

        //nullable, will only be used when gig is updated
        public DateTime? OriginalDateTime { get; set; }

        public string OriginalVenue { get; set; }
         
        [Required]  //making sure corresponding column is not nullable
        public Gig Gig { get; private set; } // cannot change property once it's set

        //for EF
        public Notification()
        {

        }

        //handling object creation by the class itself - protecting objext and system state
        public Notification(NotificationType type, Gig gig)
        {
            if (gig == null)
                throw new ArgumentNullException("gig");
            Type = type;
            DateTime = DateTime.Now;
            Gig = gig;

        }

    }
}