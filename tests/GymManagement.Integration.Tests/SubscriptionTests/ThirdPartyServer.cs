using WireMock.Server;

namespace GymManagement.Integration.Tests.SubscriptionTests;

public class ThirdPartyServer : IDisposable
{
    private WireMockServer _wireMockServer;
    public void Start()
    {
        _wireMockServer = WireMockServer.Start();
    }

    public void Dispose()
    {
        _wireMockServer.Stop();
        _wireMockServer.Dispose();
    }

}