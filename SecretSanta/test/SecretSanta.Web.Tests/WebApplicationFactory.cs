using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using SecretSanta.Web.Api;
using SecretSanta.Web.Tests.Api;

namespace SecretSanta.Web.Tests {
  public class WebApplicationFactory : WebApplicationFactory<Startup> {
    public TestableUsersClient Client { get; } = new();

    protected override void ConfigureWebHost(IWebHostBuilder builder) {
      builder.ConfigureServices(services => {
        services.AddScoped<IUsersClient, TestableUsersClient>(_ => Client);
      });
    }
  }
}
