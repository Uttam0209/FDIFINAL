<%@ Application Language="C#" %>
<%@ Import Namespace="System.Web.Routing" %>
<%@ Import Namespace="System.Net" %>

<script RunAt="server">
    void Application_Start(object sender, EventArgs e)
    {
        System.Net.ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12;
        RegisterRoutes(RouteTable.Routes);
    }

    protected void Application_BeginRequest(object sender, EventArgs e)
    {
        string currentUrl = HttpContext.Current.Request.Url.ToString().ToLower();
        var app = sender as HttpApplication;
        if (app != null && app.Context != null)
        {
            app.Context.Response.Headers.Remove("Server");
        }
    }
    void Application_Error(object sender, EventArgs e)
    {
        var serverError = Server.GetLastError() as HttpException;
        if (null != serverError)
        {
            //int errorCode = serverError.GetHttpCode();
            //if (404 == errorCode)
            //{
            //    Server.ClearError();
            //    Response.Redirect("~/PageNotFound");
            //}
            //else
            //{
            //    Server.ClearError();
            //    Response.Redirect("~/Error");

            //}
        }
        // An error has occured on a .Net page.
    }
    void Session_Start(object sender, EventArgs e)
    {
        Session.Timeout = 20;
    }
    void Session_Remove(object sender, EventArgs e)
    {
        Session.Clear();
        Session.Abandon();
    }
    static void RegisterRoutes(RouteCollection routes)
    {
        routes.MapPageRoute("Login", "Login", "~/Default.aspx", true);
        routes.MapPageRoute("PageNotFound", "PageNotFound", "~/404.aspx", true);
        routes.MapPageRoute("Error", "Error", "~/Error.aspx", true);
        routes.MapPageRoute("FDIRegistration", "FDIRegistration", "~/Admin/frmAddFDI.aspx", true);
        routes.MapPageRoute("ChangePassword", "ChangePassword", "~/Admin/ChangePassword.aspx", true);
        routes.MapPageRoute("ChngePass", "ChngePass", "~/Admin/CreatePasswordCompany.aspx", true);
        routes.MapPageRoute("ViewFDI-Detail", "ViewFDI-Detail", "~/Admin/DetailFDIRegistration.aspx", true);
        routes.MapPageRoute("Add-Company", "Add-Company", "~/Admin/CompanyDetail.aspx", true);
        routes.MapPageRoute("Detail-Company", "Detail-Company", "~/Admin/DetailofMasterCompany.aspx", true);
        routes.MapPageRoute("Dashboard", "Dashboard", "~/Admin/Dashboard.aspx", true);
        routes.MapPageRoute("Dashboard-View", "Dashboard-View", "~/Admin/ViewDashboard.aspx", true);
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
        //routes.MapPageRoute("Industry", "Industry", "~/Admin/Industry.html", true);
        routes.MapPageRoute("FAQ", "FAQ", "~/Admin/faq.aspx", true);
        routes.MapPageRoute("ViewDashboard", "ViewDashboard", "~/Admin/ViewDashboard.aspx", true);
        //routes.MapPageRoute("Test", "Test", "~/Admin/HeadDropdown.aspx", true);
        routes.MapPageRoute("Test1", "Test1", "~/Admin/EditHeadDropdown.aspx", true);
        routes.MapPageRoute("Test", "Test", "~/Admin/Test.aspx", true);
        routes.MapPageRoute("UExcel", "UExcel", "~/frmUploadExcel.aspx", true);
        //routes.MapPageRoute("VendorRegistration", "VendorRegistration", "~/Vendor/VendorRegistration.aspx", true);
        routes.MapPageRoute("VendorRegistration", "VendorRegistration", "~/Vendor/VendorDetailRegistration.aspx", true);
        routes.MapPageRoute("VendorRegistrationStep", "VendorRegistrationStep", "~/Vendor/VendorRegistrationStep1.aspx", true);
        routes.MapPageRoute("CreateVenPass", "CreateVenPass", "~/Vendor/CreateVendorPassword.aspx", true);
        routes.MapPageRoute("ForgotPassVendor", "ForgotPassVendor", "~/Vendor/VendorForgotPassword.aspx", true);
        routes.MapPageRoute("VendorLogin", "VendorLogin", "~/Vendor/VendorLogin.aspx", true);
        routes.MapPageRoute("Vendor-Dashobard", "Vendor-Dashobard", "~/Vendor/DashboardVendor.aspx", true);
        routes.MapPageRoute("View-VendorRegis", "View-VendorRegis", "~/Vendor/ViewVendorRegistrationDetail.aspx", true);
        routes.MapPageRoute("Vendor-Detail", "Vendor-Detail", "~/Admin/VendorDetail.aspx", true);


        routes.MapPageRoute("CompanyInformation_I", "CompanyInformation_I", "~/Vendor/V_CompInfo.aspx", true);
        routes.MapPageRoute("CompanyInformation_II", "CompanyInformation_II", "~/Vendor/V_CompInfo2.aspx", true);
        routes.MapPageRoute("DetailsofDefenceStores", "DetailsofDefenceStores", "~/Vendor/V_DetailofDefence.aspx", true);
        routes.MapPageRoute("RegistrationNoDetails", "RegistrationNoDetails", "~/Vendor/V_RegistrationNo.aspx", true);
        routes.MapPageRoute("FinancialInformation", "FinancialInformation", "~/Vendor/V_FinanceInfo.aspx", true);
        routes.MapPageRoute("CheckList", "CheckList", "~/Vendor/V_CheckList.aspx", true);
        routes.MapPageRoute("Declarations", "Declarations", "~/Vendor/V_Declaration.aspx", true);
        routes.MapPageRoute("GeneralInformation", "GeneralInformation", "~/Vendor/V_GeneralInfo.aspx", true);
        routes.MapPageRoute("VendorDetails", "VendorDetails", "~/Vendor/View_V_AllDetails.aspx", true);
        
        
        
        routes.MapPageRoute("News", "News", "~/Admin/ImportantNews.aspx", true);
        routes.MapPageRoute("AddNews", "AddNews", "~/Admin/AddImpNews.aspx", true);
        routes.MapPageRoute("CatStatus", "CatStatus", "~/Admin/UpdateCatstatus.aspx", true);
        routes.MapPageRoute("ActiveLogout", "ActiveLogout", "~/Admin/ActiveLogin.aspx", true);
        routes.MapPageRoute("ProdVerifiUpdate", "ProdVerifiUpdate", "~/Admin/ProductApprovedDisApproved.aspx", true);
        routes.MapPageRoute("ViewProductFilterDetail", "ViewProductFilterDetail", "~/Admin/ViewProductFilter.aspx", true);
        routes.MapPageRoute("ProductGraph", "ProductGraph", "~/Admin/ProductGraph.aspx", true);
        routes.MapPageRoute("ProductGraphNSN", "ProductGraphNSN", "~/Admin/ProductNSNWise.aspx", true);
        routes.MapPageRoute("NatoCodeSearch", "NatoCodeSearch", "~/Admin/NatoCodeSearch.aspx", true);
        routes.MapPageRoute("SearchProductFilter", "SearchProductFilter", "~/Admin/SearchProductFilter.aspx", true);
        routes.MapPageRoute("UProductList", "UProductList", "~/User/U_ProductList.aspx", true);
        routes.MapPageRoute("UCart", "U_Cart", "~/User/U_Cart.aspx", true);
    }    
</script>
