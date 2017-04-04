using System;
using System.ComponentModel.DataAnnotations;

namespace GigHub.Models
{
    //class to representa notification
    public class Notification
    {
        public int Id { get; set; }

        public DateTime DateTime { get; set; }

        public NotificationType Type { get; set; }

        //nullable, will only be used when gig is updated
        public DateTime? OriginalDateTime { get; set; }

        public string OriginalVenue { get; set; }
         
        [Required]  //making sure corresponding column is not nullable
        public Gig Gig { get; set; }

    }
}