﻿using System;
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

        public List<Amenity> Amenities { get; set; }
        public List<RoomAmenities> RoomAmenities { get; set; }
    }
}
