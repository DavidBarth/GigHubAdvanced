using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GigHub.Models
{
    public class UserNotification
    {
        public int Id { get; set; }

        public bool IsRead { get; set; }
        
        //nav property
        public ApplicationUser User { get; private set; } //private to not be able change one end of the relationship
        //nav property                                      // because that's conceptually a different relationship
        public Notification Notification { get; private set; }

        //primary key
        [Key] //for composite primary key definition
        [Column(Order = 1)] //defining order for primary keys
        public string UserId { get; private set; }

        //primary key
        [Key]
        [Column(Order = 2)]
        public int NotficationId { get; private set; }

        //default constructor for EF because it can't call the below constructor to create UserNotification
        protected UserNotification()
        {

        }
        public UserNotification(ApplicationUser user, Notification notification)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            if (notification == null)
                throw new ArgumentNullException("notification");

            User = user;
            Notification = notification;

        }

    }
}