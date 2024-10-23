namespace DataAccessLayer.Entities
{
    public class PackageUser
    {
        public int PackageUserID { get; set; }

        public int PackageID { get; set; }
        public Package? Package { get; set; }

        public string PackageResponsibility { get; set; }
        public string ApplicationUserID { get; set; }
        public ApplicationUser ApplicationUser { get; set; }


     
    }

}
