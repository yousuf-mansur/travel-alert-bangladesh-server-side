namespace DataAccessLayer.DTOs.OutputModels
{
    public class GetPackageVoucher
    {
        public int TourVoucherID { get; set; }
        public string TourVoucherCode { get; set; } = "";
        public string VoucherUrl { get; set; } = "";
        public int PackageID { get; set; }
    }
}
