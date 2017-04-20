using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GigHub.Models
{
    //association class for User and Notification
    public class UserNotification
    {
        public int Id { get; set; }
        public bool IsRead { get; set; }
        
        //primary keys
        [Key]                   //for composite primary key definition
        [Column(Order = 1)]     //define order for primary key
        public string UserId { get; set; }

        [Key]   
        [Column(Order = 2)]
        public int NotificationId { get; set; }

        
        //navigation properties
        public ApplicationUser User { get; private set; }

        public Notification Notification { get; private set; }

        //creating default constructor as EF cannot call the below constructor
        //protected to have it for EF but not for elsewhere in code
        protected UserNotification()
        {

        }

        //constructor to ensure valid objects
        //when UserNotification is constructed it must have a User and Notification associated to it
        public UserNotification(ApplicationUser user, Notification notification)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            if (notification == null)
                throw new ArgumentNullException("notification");
            User = user;
            Notification = notification
        }
    }
} 