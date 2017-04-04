using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GigHub.Models
{
    public class UserNotification
    {
        public int Id { get; set; }

        public bool IsRead { get; set; }
        
        //nav property
        public ApplicationUser User { get; set; }
        //nav property
        public Notification Notification { get; set; }

        //primary key
        [Key] //for composite primary key definition
        [Column(Order = 1)] //defining order for primary keys
        public string UserId { get; set; }

        //primary key
        [Key]
        [Column(Order = 2)]
        public int NotficationId { get; set; }

    }
}