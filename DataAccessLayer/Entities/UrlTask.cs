namespace DataAccessLayer.Entities
{
    public class UrlTask
    {
        public static string RemoveLastSegment(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                return url;
            }


            url = url.TrimStart('/');
            if (url.StartsWith("api/"))
            {
                url = url.Substring(4);
            }

            var segments = url.Split('/');

            if (segments.Length > 1)
            {
                var lastSegment = segments[^1];

                if (int.TryParse(lastSegment, out _))
                {
                    return string.Join("/", segments, 0, segments.Length - 1);
                }
            }

            return url;
        }
    }
}
