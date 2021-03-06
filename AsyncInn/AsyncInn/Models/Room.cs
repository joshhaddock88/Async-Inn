using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncInn.Models
{
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [Required]
        public int Layout { get; set; }

        // Nav props
        public List<Amenity> RoomAmenities { get; set; }
        public List<HotelRoom> Hotels { get; set; }
    }
}
