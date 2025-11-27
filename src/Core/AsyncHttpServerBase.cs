namespace Core
{
    using Core.POCOs;
    using System;
    using System.IO;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;
    using System.Threading.Tasks;
    
    public abstract class AsyncHttpServerBase
    {
        private readonly Socket _listener;
        private bool _isRunning = false;
        private static readonly int _bufferSize = 1024;

        public AsyncHttpServerBase(EndPoint endPoint)
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
                    var buffer = new byte[_bufferSize];
                    int receivedBytes = await client.ReceiveAsync(buffer);
                    var response = await ProccesRequest(new HttpRequest(buffer));
                    int sendedBytes =await client.SendAsync(response.ToBytes());
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
        protected abstract Task<HttpResponse> ProccesRequest(HttpRequest httpRequest);

        public void Stop()
        {
            _isRunning = false;
            _listener.Close();
        }
    }
}
