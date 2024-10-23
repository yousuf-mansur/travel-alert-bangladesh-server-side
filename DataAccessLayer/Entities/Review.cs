namespace DataAccessLayer.Entities
{
    public class Review
    {
        public int ReviewID { get; set; } // Primary Key

        // Foreign Key to the Package
        public int PackageID { get; set; }
        public Package Package { get; set; } // Navigation property to Package

        // Foreign Key to the User
        public string ApplicationUserID { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public int Rating { get; set; } // Rating for the package
        public string Comment { get; set; } // User's comment
        public DateTime DatePosted { get; set; } // Date of the review
    }
}
