using GigHub.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GigHub.ViewModels
{
    public class GigFormViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Venue { get; set; }

        [Required]
        [FutureDate]
        public string Date { get; set; }

        [Required]
        [ValidTime]
        public string Time { get; set; }

        [Required]
        public byte Genre { get; set; }

        public IEnumerable<Genre> Genres { get; set; }

        //property for dynamic heading
        public string Heading { get; set; }
        
        //property for dynamic form usage
        //if Id is not 0 call Update action of Gigs contoller othwerwise Create action
        //the Id will be rendered as a hidden field so when we post the form we know which
        //gig we are modifying see Gigform for hidden field
        public string Action => (Id != 0) ? "Update" : "Create";

        public DateTime GetDateTime()
        {
            return DateTime.Parse(string.Format("{0} {1}", Date, Time));
        }
    }
}