using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DTO
{
    public class RoomOcupancyDTO
    {
        public int RoomId { get; set; }
        public int UsedHours { get; set; }
        public double OcupancyPercent { get; set; }
    }
}