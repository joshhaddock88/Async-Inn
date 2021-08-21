using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncInn.Models.DTOs
{
    public class HotelRoomDTO
    {
        public int Id { get; set; }

        public int Room { get; set; }

        public RoomDTO Hotel { get; set; }
    }
}
