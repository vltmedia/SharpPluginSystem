
using System.Net.Sockets;
using System.Net;
using System.Text;

namespace SharpREST
{

    public sealed class TcpServer
    {
        private static readonly Lazy<TcpServer> _instance = new Lazy<TcpServer>(() => new TcpServer());
        private TcpListener _tcpListener;
        private Thread _listenerThread;
        private bool _isServerRunning;
        private int _currentPort;

        // Singleton instance
        public static TcpServer Instance => _instance.Value;

        // Private constructor to prevent instantiation
        private TcpServer()
        {
            _isServerRunning = false;
            _currentPort = 8080; // Default starting port
        }
        private TcpServer(int port)
        {
            _isServerRunning = false;
            _currentPort = port; // Default starting port
        }

        public bool IsServerRunning => _isServerRunning;
        public int CurrentPort => _currentPort; // Expose current port for reference

        // Method to start the TCP server
        public void StartServer(int startingPort = 8080)
        {
            if (_isServerRunning)
            {
                Console.WriteLine("Server is already running.");
                return;
            }

            _currentPort = FindAvailablePort(startingPort);

            // Start the TCPListener on an available port
            _tcpListener = new TcpListener(IPAddress.Any, _currentPort);
            _tcpListener.Start();

            _isServerRunning = true;
            _listenerThread = new Thread(ListenForClients);
            _listenerThread.IsBackground = true;
            _listenerThread.Start();

            Console.WriteLine($"Server started on port {_currentPort}.");
        }

        // Method to stop the TCP server
        public void StopServer()
        {
            if (!_isServerRunning)
            {
                Console.WriteLine("Server is not running.");
                return;
            }

            _isServerRunning = false;
            _tcpListener?.Stop();
            _listenerThread?.Join();

            Console.WriteLine("Server stopped.");
        }

        // Check for an open port starting from a specified port
        private int FindAvailablePort(int startingPort)
        {
            int port = startingPort;
            while (true)
            {
                try
                {
                    // Attempt to create a listener on the current port
                    TcpListener testListener = new TcpListener(IPAddress.Any, port);
                    testListener.Start();
                    testListener.Stop();
                    return port; // Port is available
                }
                catch (SocketException)
                {
                    // Port is already in use, try the next one
                    port++;
                }
            }
        }

        // Listener method for handling incoming clients
        private void ListenForClients()
        {
            while (_isServerRunning)
            {
                try
                {
                    TcpClient client = _tcpListener.AcceptTcpClient();
                    Thread clientThread = new Thread(() => HandleClientComm(client));
                    clientThread.IsBackground = true;
                    clientThread.Start();
                }
                catch (SocketException ex)
                {
                    Console.WriteLine("SocketException: " + ex.Message);
                }
            }
        }

        // Method to handle client communication
        private void HandleClientComm(TcpClient client)
        {
            NetworkStream clientStream = client.GetStream();
            byte[] message = new byte[4096];
            int bytesRead;

            try
            {
                while ((bytesRead = clientStream.Read(message, 0, message.Length)) > 0)
                {
                    string clientMessage = Encoding.ASCII.GetString(message, 0, bytesRead);
                    Console.WriteLine("Received: " + clientMessage);

                    // Send an acknowledgment back to the client
                    byte[] response = Encoding.ASCII.GetBytes("Message received.");
                    clientStream.Write(response, 0, response.Length);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
            finally
            {
                client.Close();
            }
        }
    }
}
