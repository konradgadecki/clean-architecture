using WireMock.Server;  // For WireMockServer
using WireMock.RequestBuilders;  // For Request.Create()
using WireMock.ResponseBuilders;  // For Response.Create()
using WireMock.Matchers;  // If you use advanced matching with WireMock

var server = WireMockServer.Start();

System.Console.WriteLine($"WireMock is now running on: {server.Url}");

server
  .Given(
    Request.Create().WithPath("/some/thing").UsingGet()
  )
  .RespondWith(
    Response.Create()
      .WithStatusCode(200)
      .WithHeader("Content-Type", "text/plain")
      .WithBody("Hello world!")
  );

Console.ReadKey();