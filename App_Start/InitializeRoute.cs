using Sitecore.Pipelines;
using System.Web.Routing;

namespace ChatGptApp.App_Start
{
    public class InitializeRoute
    {
        public void Process(PipelineArgs args)
        {
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}