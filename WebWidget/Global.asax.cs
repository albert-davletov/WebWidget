namespace WebWidget
{
    using System.Data.Entity;
    using System.Web.Http;
    using WebWidget.App_Start;
    using WebWidget.DataAccess;

    public class WebWidgetApi : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            // init widget db
            Database.SetInitializer(new WidgetDbInitializer());
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
