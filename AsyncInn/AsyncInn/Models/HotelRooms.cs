using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncInn.Models
{
    public class HotelRooms
    {
        public int HotelId { get; set; }

        public int RoomNumber { get; set; }

        public int RoomId { get; set; }

        public decimal Rate { get; set; }

        public bool Petfriendly { get; set; }

        //Nav props

        public Hotel Hotel { get; set; }
        public Room Room { get; set; }
    }
}
