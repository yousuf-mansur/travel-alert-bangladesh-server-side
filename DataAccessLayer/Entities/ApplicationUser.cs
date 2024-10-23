using Microsoft.AspNetCore.Identity;


namespace DataAccessLayer.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string ApplicationUserID { get; set; } = "";
        public string Name { get; set; } = "";

        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();

        public virtual ICollection<PackageUser> PackageUsers { get; set; } = new List<PackageUser>();
    }
}
