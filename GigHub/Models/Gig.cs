using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace GigHub.Models
{
    public class Gig
    {
        public ICollection<Attendance> Attendances { get;private set; }//making sure no overwriting is happening elsewhere

        public int Id { get; set; }

        public bool IsCanceled { get; set; }

        public ApplicationUser Artist { get; set; }

        [Required]
        public string ArtistId { get; set; }

        public DateTime DateTime { get; set; }

        [Required]
        [StringLength(255)]
        public string Venue { get; set; }

        public Genre Genre { get; set; }

        [Required]
        public byte GenreId { get; set; }

        
        public Gig()
        {
            Attendances = new Collection<Attendance>();
        }


        //extracted method from controller for cohesion purposes
        //methods relate together and Gig will be responsible to create notification
        public void Cancel()
        {
            IsCanceled = true;

            //When gig is canceled create Notification
            var notification = new Notification(NotificationType.GigCanceled, this);


            //iterate over the collection of attendees
            foreach (var attendee in Attendances.Select(a => a.Attendee))
            {
                //call Notify() of ApplicationUser class as attendee is of that type
                //and the behaviour is refactored into the model class
                attendee.Notify(notification);
            }
        }



    }
}