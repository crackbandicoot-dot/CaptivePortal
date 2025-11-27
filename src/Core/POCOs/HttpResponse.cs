namespace Core.POCOs
{
    public class HttpResponse
    {
        public string HttpVersion { get; set; } = "HTTP/1.1";
        public int StatusCode { get; set; }
        public string ReasonPhrase { get; set; }
        public Dictionary<string, string> Headers { get; set; } = new Dictionary<string, string>();
        public string Body { get; set; }
        public byte[] ToBytes()
        {
            throw new NotImplementedException();
        }
    }
    
}