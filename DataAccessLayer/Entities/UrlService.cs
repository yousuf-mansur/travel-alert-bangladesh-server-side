namespace DataAccessLayer.Entities
{
    public class UrlService
    {
        public int UrlServiceId { get; set; }
        public int CurrentUrlId { get; set; }
        public int RequestUrlId { get; set; }
        public string? Description { get; set; }
        public RequestUrl RequestUrl { get; set; }
        public CurrentUrl CurrentUrl { get; set; }
    }
}
