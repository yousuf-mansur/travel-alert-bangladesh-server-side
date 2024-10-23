using Microsoft.AspNetCore.Identity;

namespace DataAccessLayer.Entities
{
    public class Guide
    {
        public int GuideID { get; set; }
        public string ApplicationUserID { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public string NID { get; set; }
        public string Passport { get; set; }
        public string PresentAddress { get; set; }
        public string PermanentAddress { get; set; }
        public string Mobile { get; set; }
        public string HomeMobile { get; set; }
        public string ReferredBy { get; set; }

        // Navigation property
        public ICollection<PackageUser> PackageGuides { get; set; }
    }

}
