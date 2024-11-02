<a name='assembly'></a>
# SharpREST

## Contents

- [RestRequestType](#T-SharpREST-RestRequestType 'SharpREST.RestRequestType')
- [RestRouteBase](#T-SharpREST-RestRouteBase 'SharpREST.RestRouteBase')
  - [#ctor()](#M-SharpREST-RestRouteBase-#ctor 'SharpREST.RestRouteBase.#ctor')
  - [#ctor(requestType,url,callback)](#M-SharpREST-RestRouteBase-#ctor-SharpREST-RestRequestType,System-String,System-Action{System-Net-HttpListenerContext}- 'SharpREST.RestRouteBase.#ctor(SharpREST.RestRequestType,System.String,System.Action{System.Net.HttpListenerContext})')
  - [RequestType](#P-SharpREST-RestRouteBase-RequestType 'SharpREST.RestRouteBase.RequestType')
  - [URL](#P-SharpREST-RestRouteBase-URL 'SharpREST.RestRouteBase.URL')
  - [AddRoute(route)](#M-SharpREST-RestRouteBase-AddRoute-SharpREST-RestRouteBase- 'SharpREST.RestRouteBase.AddRoute(SharpREST.RestRouteBase)')
  - [ProcessRequest(request)](#M-SharpREST-RestRouteBase-ProcessRequest-System-Net-HttpListenerContext- 'SharpREST.RestRouteBase.ProcessRequest(System.Net.HttpListenerContext)')
- [RestServer](#T-SharpREST-RestServer 'SharpREST.RestServer')
  - [CurrentPort](#P-SharpREST-RestServer-CurrentPort 'SharpREST.RestServer.CurrentPort')
  - [IsServerRunning](#P-SharpREST-RestServer-IsServerRunning 'SharpREST.RestServer.IsServerRunning')
  - [AddRoute(requestType,url,callback)](#M-SharpREST-RestServer-AddRoute-SharpREST-RestRequestType,System-String,System-Action{System-Net-HttpListenerContext}- 'SharpREST.RestServer.AddRoute(SharpREST.RestRequestType,System.String,System.Action{System.Net.HttpListenerContext})')
  - [AddRoute(routeBase)](#M-SharpREST-RestServer-AddRoute-SharpREST-RestRouteBase- 'SharpREST.RestServer.AddRoute(SharpREST.RestRouteBase)')
  - [AddRoutes(newRoutes)](#M-SharpREST-RestServer-AddRoutes-System-Collections-Generic-List{SharpREST-RestRouteBase}- 'SharpREST.RestServer.AddRoutes(System.Collections.Generic.List{SharpREST.RestRouteBase})')
  - [GetRequestType(method)](#M-SharpREST-RestServer-GetRequestType-System-String- 'SharpREST.RestServer.GetRequestType(System.String)')
  - [GetRoute(url,requestType)](#M-SharpREST-RestServer-GetRoute-System-String,SharpREST-RestRequestType- 'SharpREST.RestServer.GetRoute(System.String,SharpREST.RestRequestType)')
  - [ProcessResult(context,responseText,statusCode)](#M-SharpREST-RestServer-ProcessResult-System-Net-HttpListenerContext,System-String,System-Int32- 'SharpREST.RestServer.ProcessResult(System.Net.HttpListenerContext,System.String,System.Int32)')
  - [RemoveRoute(route)](#M-SharpREST-RestServer-RemoveRoute-SharpREST-RestRouteBase- 'SharpREST.RestServer.RemoveRoute(SharpREST.RestRouteBase)')
  - [RunServer(port)](#M-SharpREST-RestServer-RunServer-System-Int32- 'SharpREST.RestServer.RunServer(System.Int32)')
  - [StartServer(startingPort)](#M-SharpREST-RestServer-StartServer-System-Int32- 'SharpREST.RestServer.StartServer(System.Int32)')
  - [StopServer()](#M-SharpREST-RestServer-StopServer 'SharpREST.RestServer.StopServer')

<a name='T-SharpREST-RestRequestType'></a>
## RestRequestType `type`

##### Namespace

SharpREST

##### Summary

Represents the types of REST requests.

<a name='T-SharpREST-RestRouteBase'></a>
## RestRouteBase `type`

##### Namespace

SharpREST

##### Summary

Represents a base class for REST routes.

<a name='M-SharpREST-RestRouteBase-#ctor'></a>
### #ctor() `constructor`

##### Summary

Initializes a new instance of the [RestRouteBase](#T-SharpREST-RestRouteBase 'SharpREST.RestRouteBase') class.

##### Parameters

This constructor has no parameters.

<a name='M-SharpREST-RestRouteBase-#ctor-SharpREST-RestRequestType,System-String,System-Action{System-Net-HttpListenerContext}-'></a>
### #ctor(requestType,url,callback) `constructor`

##### Summary

Initializes a new instance of the [RestRouteBase](#T-SharpREST-RestRouteBase 'SharpREST.RestRouteBase') class with the specified request type, URL, and callback.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| requestType | [SharpREST.RestRequestType](#T-SharpREST-RestRequestType 'SharpREST.RestRequestType') | The request type of the route. |
| url | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The URL of the route. |
| callback | [System.Action{System.Net.HttpListenerContext}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Action 'System.Action{System.Net.HttpListenerContext}') | The callback method to handle the route. |

<a name='P-SharpREST-RestRouteBase-RequestType'></a>
### RequestType `property`

##### Summary

Gets or sets the request type of the route.

<a name='P-SharpREST-RestRouteBase-URL'></a>
### URL `property`

##### Summary

Gets or sets the URL of the route.

<a name='M-SharpREST-RestRouteBase-AddRoute-SharpREST-RestRouteBase-'></a>
### AddRoute(route) `method`

##### Summary

Adds the route to the REST server.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| route | [SharpREST.RestRouteBase](#T-SharpREST-RestRouteBase 'SharpREST.RestRouteBase') | The route to add. |

<a name='M-SharpREST-RestRouteBase-ProcessRequest-System-Net-HttpListenerContext-'></a>
### ProcessRequest(request) `method`

##### Summary

Processes the HTTP request using the callback method.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| request | [System.Net.HttpListenerContext](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Net.HttpListenerContext 'System.Net.HttpListenerContext') | The HTTP listener context. |

<a name='T-SharpREST-RestServer'></a>
## RestServer `type`

##### Namespace

SharpREST

##### Summary

Represents a REST server that listens for incoming HTTP requests and routes them to appropriate handlers.

<a name='P-SharpREST-RestServer-CurrentPort'></a>
### CurrentPort `property`

##### Summary

Gets the current port number on which the REST server is running.

<a name='P-SharpREST-RestServer-IsServerRunning'></a>
### IsServerRunning `property`

##### Summary

Gets a value indicating whether the REST server is currently running.

<a name='M-SharpREST-RestServer-AddRoute-SharpREST-RestRequestType,System-String,System-Action{System-Net-HttpListenerContext}-'></a>
### AddRoute(requestType,url,callback) `method`

##### Summary

Adds a new route to the REST server.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| requestType | [SharpREST.RestRequestType](#T-SharpREST-RestRequestType 'SharpREST.RestRequestType') | The request type of the route. |
| url | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The URL of the route. |
| callback | [System.Action{System.Net.HttpListenerContext}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Action 'System.Action{System.Net.HttpListenerContext}') | The callback method to handle the route. |

<a name='M-SharpREST-RestServer-AddRoute-SharpREST-RestRouteBase-'></a>
### AddRoute(routeBase) `method`

##### Summary

Adds a new route to the REST server.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| routeBase | [SharpREST.RestRouteBase](#T-SharpREST-RestRouteBase 'SharpREST.RestRouteBase') | The route to add. |

<a name='M-SharpREST-RestServer-AddRoutes-System-Collections-Generic-List{SharpREST-RestRouteBase}-'></a>
### AddRoutes(newRoutes) `method`

##### Summary

Adds multiple routes to the REST server.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| newRoutes | [System.Collections.Generic.List{SharpREST.RestRouteBase}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{SharpREST.RestRouteBase}') | The routes to add. |

<a name='M-SharpREST-RestServer-GetRequestType-System-String-'></a>
### GetRequestType(method) `method`

##### Summary

Maps the specified HTTP method to the corresponding [RestRequestType](#T-SharpREST-RestRequestType 'SharpREST.RestRequestType') enum value.

##### Returns

The corresponding [RestRequestType](#T-SharpREST-RestRequestType 'SharpREST.RestRequestType') enum value.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| method | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The HTTP method to map. |

<a name='M-SharpREST-RestServer-GetRoute-System-String,SharpREST-RestRequestType-'></a>
### GetRoute(url,requestType) `method`

##### Summary

Gets the route that matches the specified URL and request type.

##### Returns

The matching route, or null if no route is found.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| url | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The URL to match. |
| requestType | [SharpREST.RestRequestType](#T-SharpREST-RestRequestType 'SharpREST.RestRequestType') | The request type to match. |

<a name='M-SharpREST-RestServer-ProcessResult-System-Net-HttpListenerContext,System-String,System-Int32-'></a>
### ProcessResult(context,responseText,statusCode) `method`

##### Summary

Processes the result of an HTTP request and sends the response.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| context | [System.Net.HttpListenerContext](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Net.HttpListenerContext 'System.Net.HttpListenerContext') | The HTTP listener context. |
| responseText | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The response text to send. |
| statusCode | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | The status code of the response. |

<a name='M-SharpREST-RestServer-RemoveRoute-SharpREST-RestRouteBase-'></a>
### RemoveRoute(route) `method`

##### Summary

Removes a route from the REST server.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| route | [SharpREST.RestRouteBase](#T-SharpREST-RestRouteBase 'SharpREST.RestRouteBase') | The route to remove. |

<a name='M-SharpREST-RestServer-RunServer-System-Int32-'></a>
### RunServer(port) `method`

##### Summary

Runs the REST server on the specified port.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| port | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | The port number to run the server on. Default is 8088. |

<a name='M-SharpREST-RestServer-StartServer-System-Int32-'></a>
### StartServer(startingPort) `method`

##### Summary

Starts the REST server on the specified port.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| startingPort | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | The port number to start the server on. Default is 8080. |

<a name='M-SharpREST-RestServer-StopServer'></a>
### StopServer() `method`

##### Summary

Stops the running REST server.

##### Parameters

This method has no parameters.
