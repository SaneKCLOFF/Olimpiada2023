﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanatoriumApp.Models.Entities
{
    internal class SanatoriumRoomCategory
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public decimal Cost { get; set; }

        public ICollection<SanatoriumRoom> SanatoriumRooms { get; set; } = null!;
    }
}
