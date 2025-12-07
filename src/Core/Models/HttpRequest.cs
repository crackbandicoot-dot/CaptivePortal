namespace Core.Models
{
    public class HttpRequest
    {
        public string Method { get; set; }
        public string Uri { get; set; }
        public string HttpVersion { get; set; } = "HTTP/1.1";
        public Dictionary<string, string> Headers { get; set; } = new Dictionary<string, string>();
        public string Body { get; set; } // The actual DTO data (e.g., JSON string)
        public HttpRequest(string data)
        {
            var lines = data.Split("\r\n");
            var requestLine = lines[0].Split(' ');
            Method = requestLine[0];
            Uri = requestLine[1];
            HttpVersion = requestLine[2];

            int i = 1;
            while (!string.IsNullOrEmpty(lines[i]))
            {
                var header = lines[i].Split(':', 2);
                Headers[header[0].Trim()] = header[1].Trim();
                i++;
            }

            Body = string.Join("\r\n", lines.Skip(i + 1));
        }

    }
}