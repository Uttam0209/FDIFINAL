<%@ Application Language="C#" %>
<%@ Import Namespace="System.Web.Routing" %>
<%@ Import Namespace="System.Net" %>

<script RunAt="server">

    void Application_Start(object sender, EventArgs e)
    {
        System.Net.ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12;
        RegisterRoutes(RouteTable.Routes);
    }
    protected void Application_BeginRequest(Object sender, EventArgs e)
    {
        string currentUrl = HttpContext.Current.Request.Url.ToString().ToLower();
    }
    void Application_Error(object sender, EventArgs e)
    {
        var serverError = Server.GetLastError() as HttpException;
        if (null != serverError)
        {
            int errorCode = serverError.GetHttpCode();
            if (404 == errorCode)
            {
                Server.ClearError();
                Response.Redirect("~/Error");
            }
        }
    }
    void Session_Start(object sender, EventArgs e)
    {
    }

    static void RegisterRoutes(RouteCollection routes)
    {
        routes.MapPageRoute("Login", "Login", "~/Default.aspx", true);
        routes.MapPageRoute("Error", "Error", "~/Error.aspx", true);
        routes.MapPageRoute("FDIRegistration", "FDIRegistration", "~/frmAddFDI.aspx", true);
        routes.MapPageRoute("ChangePassword", "ChangePassword", "~/ChangePassword.aspx", true);
        routes.MapPageRoute("ViewFDI-Detail", "ViewFDI-Detail", "~/DetailFDIRegistration.aspx", true);
        routes.MapPageRoute("Add-Company", "Add-Company", "~/CompanyDetail.aspx", true);
        routes.MapPageRoute("Detail-Company", "Detail-Company", "~/DetailofMasterCompany.aspx", true);
    }
       
</script>
