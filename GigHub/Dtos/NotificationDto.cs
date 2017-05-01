using GigHub.Models;
using System;

namespace GigHub.Dtos
{
    public class NotificationDto
    {
        
        public DateTime DateTime { get; set; }
        public NotificationType Type { get; set; }

        //only need use when a gig is updated hence the nullable prop
        public DateTime? OriginalDateTime { get; set; }
        public string OriginalVenue { get; set; }
        public GigDto Gig { get; set; }

               
    }
}