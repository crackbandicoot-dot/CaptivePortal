namespace Core
{
    using Core.Models;
    using System;
    using System.IO;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;
    using System.Threading.Tasks;

    public abstract class AsyncServerBase
    {
        private readonly Socket _listener;
        private bool _isRunning = false;
        private static readonly int _bufferSize = 1024;

        public AsyncServerBase(EndPoint endPoint)
        {
            
            _listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _listener.Bind(endPoint);
        }

        public async Task StartAsync()
        {
            _listener.Listen(); // Start listening for client requests
            _isRunning = true;
           
            while (_isRunning)
            {
                try
                {
                    Socket client = await _listener.AcceptAsync();
                    _ = HandleClientAsync(client);
                }
                catch (SocketException ex)
                {
                    Console.WriteLine($"Socket exception: {ex.Message}");
                    break;
                }
            }
        }

        private async Task HandleClientAsync(Socket client)
        {
            
            using (client)
            {
                try
                {
                    var ip = client.RemoteEndPoint as IPEndPoint;
                    var buffer = new byte[_bufferSize];
                    int requestBytes = await client.ReceiveAsync(buffer);
                    string requestString = Encoding.UTF8.GetString(buffer,0,requestBytes);
                    Console.WriteLine(requestString);
                    var response = await ProccesRequest(new Request(new HttpRequest(requestString),ip!.Address.ToString()));
                    string responseString = response.ToHttpString();
                    byte[] responseBytes = Encoding.UTF8.GetBytes(responseString);
                    int sendedBytes =await client.SendAsync(responseBytes);
                }
                catch (IOException)
                {
                    // Handle client disconnection or network issues
                    Console.WriteLine("Client disconnected due to an I/O error.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error handling client: {ex.Message}");
                }
            }
        }
        protected abstract Task<HttpResponse> ProccesRequest(Request httpRequest);

        public void Stop()
        {
            _isRunning = false;
            _listener.Close();
        }
    }
}
