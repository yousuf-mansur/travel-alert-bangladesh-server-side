namespace DataAccessLayer.Entities
{
    public class PackageFAQ
    {
        public int PackageFAQID { get; set; }
        public string FAQTitle { get; set; }
        public string FAQDescription { get; set; }

        // Foreign Key
        public int PackageID { get; set; }
        public Package Package { get; set; }
    }

}
