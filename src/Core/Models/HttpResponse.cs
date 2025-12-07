using System.Text;

namespace Core.Models
{
    public class HttpResponse
    {
        public HttpResponse(int statusCode, string reasonPhrase, Dictionary<string, string> headers, string body, string httpVersion= "HTTP/1.1")
        {
            HttpVersion = httpVersion;
            StatusCode = statusCode;
            ReasonPhrase = reasonPhrase;
            Headers = headers;
            Body = body;
        }

        public string HttpVersion { get; set; }
        public int StatusCode { get; set; }
        public string ReasonPhrase { get; set; }
        public Dictionary<string, string> Headers { get; set; } = new Dictionary<string, string>();
        public string Body { get; set; }
        public string ToHttpString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"{HttpVersion} {StatusCode} {ReasonPhrase}");

            foreach (var header in Headers)
            {
                sb.AppendLine($"{header.Key}: {header.Value}");
            }

            sb.AppendLine();
            sb.Append(Body);

            return sb.ToString();
        }
    }
    
}