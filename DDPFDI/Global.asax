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
                //Response.Redirect("~/Error");
            }
        }
    }
    void Session_Start(object sender, EventArgs e)
    {
    }
    static void RegisterRoutes(RouteCollection routes)
    {
        //routes.MapPageRoute("Faq", "Faq", "~/Admin/website manual.pdf", true);
        routes.MapPageRoute("Login", "Login", "~/Default.aspx", true);
        routes.MapPageRoute("Error", "Error", "~/Error.aspx", true);
        routes.MapPageRoute("FDIRegistration", "FDIRegistration", "~/Admin/frmAddFDI.aspx", true);
        routes.MapPageRoute("ChangePassword", "ChangePassword", "~/Admin/ChangePassword.aspx", true);
        routes.MapPageRoute("ChngePass", "ChngePass", "~/Admin/CreatePasswordCompany.aspx", true);
        routes.MapPageRoute("ViewFDI-Detail", "ViewFDI-Detail", "~/Admin/DetailFDIRegistration.aspx", true);
        routes.MapPageRoute("Add-Company", "Add-Company", "~/Admin/CompanyDetail.aspx", true);
        routes.MapPageRoute("Detail-Company", "Detail-Company", "~/Admin/DetailofMasterCompany.aspx", true);
        routes.MapPageRoute("Dashboard", "Dashboard", "~/Admin/Dashboard.aspx", true);
        routes.MapPageRoute("AdminLogin", "AdminLogin", "~/Admin/AdminLogin.aspx", true);
        routes.MapPageRoute("AddMasterCompany", "AddMasterCompany", "~/Admin/AddMasterCompany.aspx", true);
        routes.MapPageRoute("FDI-Inflow", "FDI-Inflow", "~/Admin/FDIInflow.aspx", true);
        routes.MapPageRoute("General-Info", "General-Info", "~/Admin/GeneralInformation.aspx", true);
        routes.MapPageRoute("MSME", "MSME", "~/Admin/MSME.aspx", true);
        routes.MapPageRoute("Profile", "Profile", "~/Admin/Profile.aspx", true);
        routes.MapPageRoute("AddProduct", "AddProduct", "~/Admin/AddProduct.aspx", true);
        routes.MapPageRoute("Master-Category", "Master-Category", "~/Admin/RegisterdAs.aspx", true);
        routes.MapPageRoute("Startup", "Startup", "~/Admin/Startup.aspx", true);
        routes.MapPageRoute("Specilization", "Specilization", "~/Admin/Specilization.aspx", true);
        routes.MapPageRoute("View-Category", "View-Category", "~/Admin/ViewCategory.aspx", true);
        routes.MapPageRoute("View-Product", "View-Product", "~/Admin/ViewProduct.aspx", true);
        routes.MapPageRoute("Add-Nodal", "Add-Nodal", "~/Admin/AddNodalOfficer.aspx", true);
        routes.MapPageRoute("Add-Designation", "Add-Designation", "~/Admin/AddDesignation.aspx", true);
        routes.MapPageRoute("View-Designation", "View-Designation", "~/Admin/ViewDesignation.aspx", true);
        routes.MapPageRoute("View-NodalOfficer", "View-NodalOfficer", "~/Admin/ViewNodalOfficer.aspx", true);
        routes.MapPageRoute("Company-Category", "Company-Category", "~/Admin/AddCompanyCategory.aspx", true);
        routes.MapPageRoute("ViewCompany-Category", "ViewCompany-Category", "~/Admin/ViewCompanyCategory.aspx", true);
        routes.MapPageRoute("Industry", "Industry", "~/Admin/Industry.aspx", true);
        routes.MapPageRoute("FAQ", "FAQ", "~/Admin/faq.aspx", true);
        routes.MapPageRoute("Test", "Test", "~/Admin/TestProduct.aspx", true);

    }    
</script>
