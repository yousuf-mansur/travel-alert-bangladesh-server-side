namespace DataAccessLayer.DTOs.InputModels
{
    // DTOs

    public class RequestUrlDTO
    {
        public int RequestUrlId { get; set; }
        public string Url { get; set; }
        public string? UrlName { get; set; }
    }

    public class CurrentUrlDTO
    {
        public int CurrentUrlId { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
    }

    public class UrlServiceDTO
    {
        public int UrlServiceId { get; set; }
        public int CurrentUrlId { get; set; }
        public int RequestUrlId { get; set; }
        public string? Description { get; set; }
    }


}
