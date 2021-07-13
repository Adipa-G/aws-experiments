using Amazon.Lambda.AspNetCoreServer;
using Amazon.XRay.Recorder.Handlers.AwsSdk;
using Microsoft.AspNetCore.Hosting;

namespace HealthAPI
{
    public class LambdaEntryPoint : APIGatewayProxyFunction
    {
        static LambdaEntryPoint()
        {
            AWSSDKHandler.RegisterXRayForAllServices();
        }

        protected override void Init(IWebHostBuilder builder)
        {
            builder
                .UseStartup<Startup>();
        }
    }
}
