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
        routes.MapPageRoute("FDIRegistration", "FDIRegistration", "~/Admin/frmAddFDI.aspx", true);
        routes.MapPageRoute("ChangePassword", "ChangePassword", "~/Admin/ChangePassword.aspx", true);
        routes.MapPageRoute("ViewFDI-Detail", "ViewFDI-Detail", "~/Admin/DetailFDIRegistration.aspx", true);
        routes.MapPageRoute("Add-Company", "Add-Company", "~/Admin/CompanyDetail.aspx", true);
        routes.MapPageRoute("Detail-Company", "Detail-Company", "~/Admin/DetailofMasterCompany.aspx", true);
        routes.MapPageRoute("Dashboard", "Dashboard", "~/Admin/Dashboard.aspx", true);
        routes.MapPageRoute("AdminLogin", "AdminLogin", "~/Admin/AdminLogin.aspx", true);
        routes.MapPageRoute("D", "D", "~/Admin/Default2.aspx", true);
        //----UserControl SectionInformation Rewrite------------------------------------------//
        routes.MapPageRoute("FDI-Inflow", "FDI-Inflow", "~/Admin/FDIInflow.aspx", true);
        routes.MapPageRoute("General-Info", "General-Info", "~/Admin/GeneralInformation.aspx", true);
        routes.MapPageRoute("MSME", "MSME", "~/Admin/MSME.aspx", true);
        routes.MapPageRoute("Profile", "Profile", "~/Admin/Profile.aspx", true);
        routes.MapPageRoute("AddProduct", "Add-Product", "~/Admin/AddProduct.aspx", true);
        routes.MapPageRoute("Registerd-As", "Registerd-As", "~/Admin/RegisterdAs.aspx", true);
        routes.MapPageRoute("Startup", "Startup", "~/Admin/Startup.aspx", true);
        routes.MapPageRoute("Specilization", "Specilization", "~/Admin/Specilization.aspx", true);
    }    
</script>
