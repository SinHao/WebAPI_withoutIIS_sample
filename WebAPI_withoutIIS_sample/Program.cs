using System;
using System.Threading.Tasks;
using System.Web.Http.SelfHost;
using System.Web.Http;
using System.Net.Http;

namespace WebAPI_withoutIIS_sample
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new HttpSelfHostConfiguration(new Uri("http://localhost:5050"));
            config.Routes.MapHttpRoute(
                "DefaultApi",
                "{controller}/{action}",
                new { id = RouteParameter.Optional });

            config.MessageHandlers.Add(new CustomHeaderHandler());

            HttpSelfHostServer server = new HttpSelfHostServer(config);
            
            server.Configuration.Formatters.XmlFormatter.SupportedMediaTypes.Clear();
            
            server.OpenAsync().Wait();
            Console.Read();
            server.CloseAsync().Wait();
        }
    }

    public class CustomHeaderHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
        {
            return base.SendAsync(request, cancellationToken)
                .ContinueWith((task) =>
                {
                    HttpResponseMessage response = task.Result;
                    response.Headers.Add("Access-Control-Allow-Origin", "*");
                    return response;
                });
        }
    }
}
