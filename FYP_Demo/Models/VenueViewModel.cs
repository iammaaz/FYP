using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FYP_Demo.Models
{
    public class VenueViewModel 
    {
        public HallInfo Hall { get; set; }

        [NotMapped]
        public IEnumerable<int> HallTypeSelectedIDs { get; set; }

        [NotMapped]
        public IEnumerable<int> HallEventTypeSelectedIDs { get; set; }

        [NotMapped]
        public IEnumerable<int> HallActivitiesSelectedIDs { get; set; }

        [NotMapped]
        public IEnumerable<int> HallFacilitySelectedIDs { get; set; }

    }
}