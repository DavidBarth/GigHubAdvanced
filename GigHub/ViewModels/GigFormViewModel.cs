using GigHub.Controllers;
using GigHub.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Web.Mvc;

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
        /*
        public string Action => (Id != 0) ? "Update" : "Create";
        */

        public string Action
        {
            get
            {   //going from step 1 to the new solution
                // 1. return Id != 0 ? "Update" : "Create"; //magic strings - to fragile because 
                //when you rename any of the methods the code will break

                //2. var update = (c => c.Update()); //anyonmus method can be represented with a Func delegate

                //3. c represent GigsController therefor the arguments in the delegate are
                //the GigsController and return type of its method Update
                //Func<GigsController,ActionResult> update = (c => c.Update(this));
                //Func<GigsController, ActionResult> create = (c => c.Create(this));

                //wrapping the delegate with expression because we don't want to call the delegate 
                //just representing the methods via an expression
                Expression<Func<GigsController,ActionResult>> update = (c => c.Update(this));
                Expression<Func<GigsController, ActionResult>> create = (c => c.Create(this));

                //determining action with this expression
                var action = (Id != 0) ? update : create;

                return (action.Body as MethodCallExpression).Method.Name;
               
            }

        } 

        public DateTime GetDateTime()
        {
            return DateTime.Parse(string.Format("{0} {1}", Date, Time));
        }
    }
}