﻿namespace DataAccessLayer.DTOs.OutputModels
{
    public class RoomDTO
    {
        public int RoomID { get; set; }
        public decimal AveragePrice { get; set; }
        public int MaxOccupancy { get; set; }
        public bool IsAvailable { get; set; }
        public int HotelID { get; set; }
        public int RoomTypeID { get; set; }
        public int RoomSubTypeID { get; set; }
    }
}
