namespace DataAccessLayer.Entities
{
    public class Location
    {
        public int LocationID { get; set; }
        public string LocationName { get; set; }
        public string LocationDescription { get; set; }

        // Foreign Key
        public int StateID { get; set; }
        public State State { get; set; }

        // Navigation property
        public ICollection<LocationGallery> LocationGalleries { get; set; }
        public ICollection<Hotel> Hotels { get; set; }
    }

}