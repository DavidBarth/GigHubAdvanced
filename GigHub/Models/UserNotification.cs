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
        public ApplicationUser User { get; set; }

        public Notification Notification { get; set; }
    }
} 