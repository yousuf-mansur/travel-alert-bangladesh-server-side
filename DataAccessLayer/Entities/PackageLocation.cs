using System.Reflection.Metadata.Ecma335;

namespace DataAccessLayer.Entities
{
    public class PackageLocation
    {
        public int PackageLocationID { get; set; }
        public int PackageID { get; set; }
        public int LocationID { get; set; }

        public Package? Package { get; set; }
        public Location? Location { get; set; }
    }
}
