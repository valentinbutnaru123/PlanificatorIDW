using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(PlanificatorMVC.Areas.Identity.IdentityHostingStartup))]

namespace PlanificatorMVC.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
            });
        }
    }
}