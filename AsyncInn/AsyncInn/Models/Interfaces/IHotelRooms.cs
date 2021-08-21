using AsyncInn.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncInn.Models.Interfaces
{
    public interface IHotelRooms
    {
        // Composite key 1
        public int HotelId { get; set; }
        // Composite key 2
        public int RoomId { get; set; }

        // Nav props
        public Hotel Hotel { get; set; }
        public Room Room { get; set; }
    }
}
