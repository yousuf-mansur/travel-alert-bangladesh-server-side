namespace DataAccessLayer.Entities
{
    public class Seats
    {
        public int SeatsID { get; set; }
        public string SeatsNumber { get; set; }
        public int PackageTransportationID { get; set; }
        public PackageTransportation PackageTransportation { get; set; }
    }

}
