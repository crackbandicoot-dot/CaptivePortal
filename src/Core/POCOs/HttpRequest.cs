namespace Core.POCOs
{
    public class HttpRequest
    {
        public string Method { get; set; }
        public string Uri { get; set; }
        public string HttpVersion { get; set; }
        public Dictionary<string, string> Headers { get; set; } = new Dictionary<string, string>();
        public string Body { get; set; } // The actual DTO data (e.g., JSON string)
        public HttpRequest(byte[] data)
        {

        }
       
    }
}