using System.Net;
using System.Text;

namespace SharpREST
{

    /// <summary>
    /// Represents a REST server that listens for incoming HTTP requests and routes them to appropriate handlers.
    /// </summary>
    public class RestServer
    {
        private static readonly Lazy<RestServer> _instance = new Lazy<RestServer>(() => new RestServer());
        public static List<RestRouteBase> routes = new List<RestRouteBase>();
        private HttpListener _httpListener;
        private Thread _listenerThread;
        private bool _isServerRunning;
        private int _currentPort;

        // Singleton instance
        public static RestServer Instance => _instance.Value;

        // Private constructor to prevent instantiation
        private RestServer()
        {
            _isServerRunning = false;
            _currentPort = 8080; // Default starting port
        }

        /// <summary>
        /// Gets a value indicating whether the REST server is currently running.
        /// </summary>
        public bool IsServerRunning => _isServerRunning;

        /// <summary>
        /// Gets the current port number on which the REST server is running.
        /// </summary>
        public int CurrentPort => _currentPort;

        /// <summary>
        /// Starts the REST server on the specified port.
        /// </summary>
        /// <param name="startingPort">The port number to start the server on. Default is 8080.</param>
        public void StartServer(int startingPort = 8080)
        {
            if (_isServerRunning)
            {
                Console.WriteLine("Server is already running.");
                return;
            }

            _currentPort = FindAvailablePort(startingPort);

            try
            {
                // Start the HttpListener on an available port
                _httpListener = new HttpListener();
                _httpListener.Prefixes.Add($"http://localhost:{_currentPort}/");
                _httpListener.Start();

                _isServerRunning = true;
                _listenerThread = new Thread(ListenForRequests) { IsBackground = true };
                _listenerThread.Start();

                Console.WriteLine($"REST server started on port {_currentPort}.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error starting server: " + ex.Message);
                _isServerRunning = false;
            }
        }

        /// <summary>
        /// Stops the running REST server.
        /// </summary>
        public void StopServer()
        {
            if (!_isServerRunning)
            {
                Console.WriteLine("Server is not running.");
                return;
            }

            _isServerRunning = false;
            _httpListener?.Stop();
            _listenerThread?.Join();

            Console.WriteLine("REST server stopped.");
        }

        // Check for an open port starting from a specified port
        private int FindAvailablePort(int startingPort)
        {
            int port = startingPort;
            while (true)
            {
                try
                {
                    using (var testListener = new HttpListener())
                    {
                        testListener.Prefixes.Add($"http://localhost:{port}/");
                        testListener.Start();
                        testListener.Stop();
                        return port; // Port is available
                    }
                }
                catch (HttpListenerException)
                {
                    port++; // Port is already in use, try the next one
                }
            }
        }
        public string GetRequestBody( HttpListenerContext request){
            var body = new StreamReader(request.Request.InputStream).ReadToEnd();

           
            return body;

        }
        // Listener method for handling incoming HTTP requests
        private void ListenForRequests()
        {
            while (_isServerRunning)
            {
                try
                {
                    HttpListenerContext context = _httpListener.GetContext();
                    ThreadPool.QueueUserWorkItem(state => ProcessRequest(context));
                }
                catch (HttpListenerException ex)
                {
                    if (_isServerRunning)
                    {
                        Console.WriteLine("HttpListenerException: " + ex.Message);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.Message);
                }
            }
        }

        /// <summary>
        /// Gets the route that matches the specified URL and request type.
        /// </summary>
        /// <param name="url">The URL to match.</param>
        /// <param name="requestType">The request type to match.</param>
        /// <returns>The matching route, or null if no route is found.</returns>
        public static RestRouteBase GetRoute(string url, RestRequestType requestType)
        {
            // Ensure URL is matched correctly by adding a leading slash if needed
            if (!url.StartsWith("/"))
            {
                url = "/" + url;
            }
            return routes.FirstOrDefault(x => x.URL == url && x.RequestType == requestType);
        }

        /// <summary>
        /// Maps the specified HTTP method to the corresponding <see cref="RestRequestType"/> enum value.
        /// </summary>
        /// <param name="method">The HTTP method to map.</param>
        /// <returns>The corresponding <see cref="RestRequestType"/> enum value.</returns>
        public static RestRequestType GetRequestType(string method)
        {
            return method switch
            {
                "GET" => RestRequestType.GET,
                "POST" => RestRequestType.POST,
                "PUT" => RestRequestType.PUT,
                "DELETE" => RestRequestType.DELETE,
                _ => RestRequestType.GET,
            };
        }

        // Method to handle individual HTTP requests
        private void ProcessRequest(HttpListenerContext context)
        {
            try
            {
                string responseText = "";
                int statusCode = (int)HttpStatusCode.OK;
                RestRouteBase route = GetRoute(context.Request.Url.LocalPath, GetRequestType(context.Request.HttpMethod));

                if (route != null)
                {
                    route.ProcessRequest(context);
                }
                else
                {
                    responseText = "{\"error\": \"Route not found\"}";
                    statusCode = (int)HttpStatusCode.NotFound;
                    ProcessResult(context, responseText, statusCode);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error processing request: " + ex.Message);
            }
        }

        /// <summary>
        /// Adds a new route to the REST server.
        /// </summary>
        /// <param name="requestType">The request type of the route.</param>
        /// <param name="url">The URL of the route.</param>
        /// <param name="callback">The callback method to handle the route.</param>
        public static void AddRoute(RestRequestType requestType, string url, Action<HttpListenerContext> callback)
        {
            AddRoute(new RestRouteBase(requestType, url, callback));
        }

        /// <summary>
        /// Adds a new route to the REST server.
        /// </summary>
        /// <param name="routeBase">The route to add.</param>
        public static void AddRoute(RestRouteBase routeBase)
        {
            routes.Add(routeBase);
        }

        /// <summary>
        /// Adds multiple routes to the REST server.
        /// </summary>
        /// <param name="newRoutes">The routes to add.</param>
        public static void AddRoutes(List<RestRouteBase> newRoutes)
        {
            routes.AddRange(newRoutes);
        }

        /// <summary>
        /// Runs the REST server on the specified port.
        /// </summary>
        /// <param name="port">The port number to run the server on. Default is 8088.</param>
        public static async void RunServer(int port = 8088)
        {
            await Task.Run(() => RestServer.Instance.StartServer(port));
        }

        /// <summary>
        /// Removes a route from the REST server.
        /// </summary>
        /// <param name="route">The route to remove.</param>
        public static void RemoveRoute(RestRouteBase route)
        {
            routes.Remove(route);
        }

        /// <summary>
        /// Processes the result of an HTTP request and sends the response.
        /// </summary>
        /// <param name="context">The HTTP listener context.</param>
        /// <param name="responseText">The response text to send.</param>
        /// <param name="statusCode">The status code of the response.</param>
        public static void ProcessResult(HttpListenerContext context, string responseText, int statusCode)
        {
            try
            {
                byte[] buffer = Encoding.UTF8.GetBytes(responseText);
                context.Response.ContentLength64 = buffer.Length;
                context.Response.StatusCode = statusCode;
                context.Response.ContentType = "application/json";
                context.Response.OutputStream.Write(buffer, 0, buffer.Length);
                context.Response.OutputStream.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error sending response: " + ex.Message);
            }
        }
    }

    /// <summary>
    /// Represents a base class for REST routes.
    /// </summary>
    [System.Serializable]
    public class RestRouteBase
    {
        /// <summary>
        /// Gets or sets the request type of the route.
        /// </summary>
        public RestRequestType RequestType { get; set; } = RestRequestType.GET;

        /// <summary>
        /// Gets or sets the callback method to handle the route.
        /// </summary>
        public event Action<HttpListenerContext> Callback;

        /// <summary>
        /// Gets or sets the URL of the route.
        /// </summary>
        public string URL { get; set; } = "items";

        /// <summary>
        /// Initializes a new instance of the <see cref="RestRouteBase"/> class.
        /// </summary>
        public RestRouteBase()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RestRouteBase"/> class with the specified request type, URL, and callback.
        /// </summary>
        /// <param name="requestType">The request type of the route.</param>
        /// <param name="url">The URL of the route.</param>
        /// <param name="callback">The callback method to handle the route.</param>
        public RestRouteBase(RestRequestType requestType, string url, Action<HttpListenerContext> callback)
        {
            RequestType = requestType;
            URL = url;
            Callback = callback;
            AddRoute(this);
        }

        /// <summary>
        /// Adds the route to the REST server.
        /// </summary>
        /// <param name="route">The route to add.</param>
        public static void AddRoute(RestRouteBase route)
        {
            if (!RestServer.routes.Contains(route))
            {
                RestServer.routes.Add(route);
            }
        }

        /// <summary>
        /// Processes the HTTP request using the callback method.
        /// </summary>
        /// <param name="request">The HTTP listener context.</param>
        public virtual void ProcessRequest(HttpListenerContext request)
        {
            Callback?.Invoke(request);
        }
    }

    /// <summary>
    /// Represents the types of REST requests.
    /// </summary>
    [System.Serializable]
    public enum RestRequestType
    {
        GET,
        POST,
        PUT,
        DELETE
    }
}
