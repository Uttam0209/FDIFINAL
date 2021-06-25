<%@ Application Language="C#" %>
<%@ Import Namespace="System.Web.Routing" %>
<%@ Import Namespace="System.Net" %>
<%@ Import Namespace="System.Web.Optimization" %>

<script RunAt="server">
    void Application_Start(object sender, EventArgs e)
    {
        System.Net.ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12;
        RegisterRoutes(RouteTable.Routes);
        Application["SiteVisitedCounter"] = 0;
        //to check how many users have currently opened our site write the following line
        Application["OnlineUserCounter"] = 0;
    }
    protected void Application_BeginRequest(object sender, EventArgs e)
    {
        //string currentUrl = HttpContext.Current.Request.Url.ToString().ToLower();
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
        }
        Exception ex = Server.GetLastError().GetBaseException();
        ErrorLogService.LogError(ex);
        Server.Transfer("~/ErrorPages/Error.aspx");
    }
    public static class ErrorLogService
    {
        public static void LogError(Exception ex)
        {
            try
            {
                HybridDictionary hyLog = new HybridDictionary();
                string _msg = string.Empty;
                string _sysMsg = string.Empty;
                hyLog["ExceptionMsg"] = ex.Message;
                hyLog["ExceptionType"] = ex.GetType().Name.ToString();
                hyLog["ExceptionSource"] = ex.StackTrace;
                hyLog["ExceptionURL"] = System.Web.HttpContext.Current.Request.Url.ToString();
                hyLog["Logdate"] = DateTime.Now;
                hyLog["Browser"] = System.Web.HttpContext.Current.Request.Browser.Type.ToString();
                hyLog["IPAddress"] = System.Web.HttpContext.Current.Request.UserHostAddress;
                BusinessLayer.Logic Lo = new BusinessLayer.Logic();
                string id = Lo.Add_ErrorLog(hyLog, out _sysMsg, out _msg);
            }
            catch
            {

            }

        }
    }
    void Session_Start(object sender, EventArgs e)
    {

        Response.Redirect("ProductList");
        Session.Timeout = 20;
        Application.Lock();
        Application["SiteVisitedCounter"] = Convert.ToInt32(Application["SiteVisitedCounter"]) + 1;
        Application["OnlineUserCounter"] = Convert.ToInt32(Application["OnlineUserCounter"]) + 1;
        Application.UnLock();

    }
    //void Session_End(object sender, EventArgs e)
    //{
    //    Session.Abandon();
    //    Session.Remove("Type");
    //    Session.Remove("User");
    //    Session.Remove("CompanyRefNo");
    //    Session.Remove("SFToken");
    //    Session.RemoveAll();
    //    Session.Contents.RemoveAll();
    //    Session.Clear();
    //    try
    //    {
    //        Response.Cache.SetCacheability(HttpCacheability.NoCache);
    //        Response.Cookies["DefaultDpsu"].Expires = DateTime.Now;
    //        Response.Buffer = true;
    //        Response.AppendHeader("Cache-Control", "no-cache, no-store, must-revalidate"); // HTTP 1.1.
    //        Response.AppendHeader("Pragma", "no-cache"); // HTTP 1.0.
    //        Response.AppendHeader("Expires", "0"); // Proxies.
    //        Response.Cookies.Remove("DefaultDpsu");
    //        Response.Cookies.Remove("Dashboard");
    //        Response.Cookies.Remove("ProductList");
    //        if (Request.Cookies["User"] != null)
    //        {
    //            Response.Cookies["User"].Value = string.Empty;
    //            Response.Cookies["User"].Expires = DateTime.Now.AddMonths(-20);
    //        }
    //        if (Request.Cookies["SFToken"] != null)
    //        {
    //            Response.Cookies["SFToken"].Value = string.Empty;
    //            Response.Cookies["SFToken"].Expires = DateTime.Now.AddMonths(-20);
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //    }
    //}
    //void Session_Remove(object sender, EventArgs e)
    //{
    //    Session.Abandon();
    //    Session.Remove("Type");
    //    Session.Remove("User");
    //    Session.Remove("CompanyRefNo");
    //    Session.Remove("SFToken");
    //    Session.RemoveAll();
    //    Session.Contents.RemoveAll();
    //    Session.Clear();
    //    Response.Cache.SetCacheability(HttpCacheability.NoCache);
    //    Response.Cookies["DefaultDpsu"].Expires = DateTime.Now;
    //    Response.Cookies.Remove("DefaultDpsu");
    //    Response.Cookies.Remove("Dashboard");
    //    Response.Cookies.Remove("ProductList");
    //    Response.Buffer = true;
    //    Response.AppendHeader("Cache-Control", "no-cache, no-store, must-revalidate"); // HTTP 1.1.
    //    Response.AppendHeader("Pragma", "no-cache"); // HTTP 1.0.
    //    Response.AppendHeader("Expires", "0"); // Proxies.
    //    if (Request.Cookies["User"] != null)
    //    {
    //        Response.Cookies["User"].Value = string.Empty;
    //        Response.Cookies["User"].Expires = DateTime.Now.AddMonths(-20);
    //    }
    //    if (Request.Cookies["SFToken"] != null)
    //    {
    //        Response.Cookies["SFToken"].Value = string.Empty;
    //        Response.Cookies["SFToken"].Expires = DateTime.Now.AddMonths(-20);
    //    }
    //}
    void Session_End(object sender, EventArgs e)
    {
        Session.Clear();
        Session.Abandon();
        Session.RemoveAll();
        Session.Remove("Type");
        Session.Remove("User");
        Session.Remove("CompanyRefNo");
        Session.Remove("SFToken");
        Session.Contents.RemoveAll();
        try
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cookies["DefaultDpsu"].Expires = DateTime.Now;
            Response.Buffer = true;
            Response.AppendHeader("Cache-Control", "no-cache, no-store, must-revalidate"); // HTTP 1.1.
            Response.AppendHeader("Pragma", "no-cache"); // HTTP 1.0.
            Response.AppendHeader("Expires", "0"); // Proxies.
            Response.Cookies.Remove("DefaultDpsu");
            Response.Cookies.Remove("Dashboard");
            Response.Cookies.Remove("ProductList");
            if (Request.Cookies["User"] != null)
            {
                Response.Cookies["User"].Value = string.Empty;
                Response.Cookies["User"].Expires = DateTime.Now.AddMonths(-20);
            }
            if (Request.Cookies["SFToken"] != null)
            {
                Response.Cookies["SFToken"].Value = string.Empty;
                Response.Cookies["SFToken"].Expires = DateTime.Now.AddMonths(-20);
            }
            Application.Lock();
            Application["OnlineUserCounter"] = Convert.ToInt32(Application["OnlineUserCounter"]) - 1;
            Application.UnLock();
        }

        catch (Exception ex)
        {
        }
    }
    void Session_Remove(object sender, EventArgs e)
    {
        Session.Clear();
        Session.Abandon();
        Session.RemoveAll();
        Session.Remove("Type");
        Session.Remove("User");
        Session.Remove("CompanyRefNo");
        Session.Remove("SFToken");
        Session.Contents.RemoveAll();
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cookies["DefaultDpsu"].Expires = DateTime.Now;
        Response.Cookies.Remove("DefaultDpsu");
        Response.Cookies.Remove("Dashboard");
        Response.Cookies.Remove("ProductList");
        Response.Buffer = true;
        Response.AppendHeader("Cache-Control", "no-cache, no-store, must-revalidate"); // HTTP 1.1.
        Response.AppendHeader("Pragma", "no-cache"); // HTTP 1.0.
        Response.AppendHeader("Expires", "0"); // Proxies.
        if (Request.Cookies["User"] != null)
        {
            Response.Cookies["User"].Value = string.Empty;
            Response.Cookies["User"].Expires = DateTime.Now.AddMonths(-20);
        }
        if (Request.Cookies["SFToken"] != null)
        {
            Response.Cookies["SFToken"].Value = string.Empty;
            Response.Cookies["SFToken"].Expires = DateTime.Now.AddMonths(-20);
        }
    }
    static void RegisterRoutes(RouteCollection routes)
    {
        routes.MapPageRoute("Login", "Login", "~/DefaultDpsu.aspx", true);
        // routes.MapPageRoute("Login", "Login", "~/MaintancePage.aspx", true);
        routes.MapPageRoute("PasswordPolicy", "PasswordPolicy", "~/User/Change_password.aspx", true);
        routes.MapPageRoute("ShowInterest", "ShowInterest", "~/Admin/ViewRequestInfo.aspx", true);
        //routes.MapPageRoute("PageNotFound", "PageNotFound", "~/404.aspx", true);
        routes.MapPageRoute("Error", "Error", "~/ErrorPages/Error.aspx", true);
        routes.MapPageRoute("PageNotFound", "PageNotFound", "~/ErrorPages/404_Error_page.aspx", true);
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
        routes.MapPageRoute("InterestedVendor", "InterestedVendor", "~/Admin/ViewInterestedVendor.aspx", true);
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
        routes.MapPageRoute("mTest", "mTest", "~/Admin/Test.aspx", true);
        routes.MapPageRoute("UExcel", "UExcel", "~/frmUploadExcel.aspx", true);
        routes.MapPageRoute("UExcelforSHQ", "UExcelforSHQ", "~/SHQfrmUploadExcel.aspx", true);
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
        routes.MapPageRoute("Profile_Management", "Profile_Management", "~/Vendor/Profile_Management.aspx", true);
        routes.MapPageRoute("ReferrerVendorRegistration", "ReferrerVendorRegistration", "~/Vendor/ReferrerVendorRegistration.aspx", true);
        routes.MapPageRoute("ProfileMigration", "ProfileMigration", "~/Vendor/ProfileMigration.aspx", true);
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
        routes.MapPageRoute("ProductList", "ProductList", "~/User/U_ProductList.aspx", true);
        routes.MapPageRoute("UproductListr", "UproductListr", "~/User/Redirect.aspx", true);
        routes.MapPageRoute("UCart", "U_Cart", "~/User/U_Cart.aspx", true);
        routes.MapPageRoute("HomeRemove", "HomeRemove", "~/User/Login.aspx", true);
        routes.MapPageRoute("Popup", "Popup", "~/ViewPopup.aspx", true);
        routes.MapPageRoute("TempTable", "TempTable", "~/Admin/CreatetempTable.aspx", true);
        routes.MapPageRoute("ViewRequest", "ViewRequest", "~/Admin/ViewRequestInfo.aspx", true);
        routes.MapPageRoute("Contact-Us", "Contact-Us", "~/User/Contact_us.html", true);
        routes.MapPageRoute("iDex", "iDex", "~/User/More_Details_iDEX.html", true);
        routes.MapPageRoute("IGA", "IGA", "~/User/More_Details_IGA.html", true);
        routes.MapPageRoute("MakeII", "MakeII", "~/User/More_Details_Make_II.html", true);
        routes.MapPageRoute("FAQs", "FAQs", "~/User/FAQ.aspx", true);
        routes.MapPageRoute("IntrestedProduct", "IntrestedProduct", "~/User/IntrestedProduct.aspx", true);
        routes.MapPageRoute("EOIStatus", "EOIStatus", "~/User/EOIStatus.aspx", true);
        routes.MapPageRoute("DPSUHome", "DPSUHome", "~/User/HomePage.aspx", true);
        routes.MapPageRoute("ProgressReport", "ProgressReport", "~/User/S_Story.aspx", true);
        routes.MapPageRoute("SuccessStory", "SuccessStory", "~/User/S_Story2.aspx", true);
        routes.MapPageRoute("TestHome", "TestHome", "~/User/TestHome.aspx", true);
        //routes.MapPageRoute("PReport", "PReport", "~/User/ProgressReportNew.aspx", true);
        routes.MapPageRoute("FeedBack", "FeedBack", "~/User/Feedback.aspx", true);
        routes.MapPageRoute("ViewFeedBack", "ViewFeedBack", "~/Admin/CompaniesFeedBack.aspx", true);
        routes.MapPageRoute("About", "About", "~/User/About.aspx", true);
        //routes.MapPageRoute("PReport", "PReport", "~/User/ProgressReport.aspx", true);
        routes.MapPageRoute("ViewRemark", "ViewRemark", "~/Admin/ProductRemark.aspx", true);
        //routes.MapPageRoute("ViewProd", "ViewProd", "~/Admin/ViewProductNew.aspx", true);
        routes.MapPageRoute("PReport2", "PReport2", "~/User/ProgressReport2.aspx", true);
        routes.MapPageRoute("PUpdate", "PUpdate", "~/User/U_ProductList1.aspx", true);
        routes.MapPageRoute("SimilarProd", "SimilarProd", "~/User/SimilerProduct.aspx", true);
        routes.MapPageRoute("ItemStatus", "ItemStatus", "~/User/TargetValue2021.aspx", true);
        //routes.MapPageRoute("ItemStatus", "ItemStatus", "~/User/StatusOnDate.aspx", true);
        routes.MapPageRoute("UpdateExcelData", "UpdateExcelData", "~/User/UpdateExcelData.aspx", true);
        routes.MapPageRoute("TestSimiler", "TestSimiler", "~/User/ProgressReportUpdation.aspx", true);
        routes.MapPageRoute("SuccessStory2", "SuccessStory2", "~/User/S_Story2update.aspx", true);
        routes.MapPageRoute("SuccessStoryupdate", "SuccessStoryupdate", "~/User/S_Story2update.aspx", true);
        routes.MapPageRoute("UpdateBulkRecordExcel", "UpdateBulkRecordExcel", "~/frmUpdateProductBulkExcel.aspx", true);
        routes.MapPageRoute("SHQUpdateBulkRecordExcel", "SHQUpdateBulkRecordExcel", "~/SHQfrmUpdateProductBulkExcel.aspx", true);
        routes.MapPageRoute("ResetDuplicateInterest", "ResetDuplicateInterest", "~/Admin/ResetDuplicateInterest.aspx", true);
        routes.MapPageRoute("Participate", "Participate", "~/User/Participate.aspx", true);
        //routes.MapPageRoute("Test", "Test", "~/Test_Lab/Test_details.aspx", true);
        //routes.MapPageRoute("Testdetails", "Testdetails", "~/Test_Lab/ViewTest_details.aspx", true);
        //routes.MapPageRoute("TestDashboard", "TestDashboard", "~/Test_Lab/TestLabDashboard.aspx", true);
        //routes.MapPageRoute("ViewOrder", "ViewOrder", "~/Test_Lab/ViewBookedOrder.aspx", true);
        //routes.MapPageRoute("ViewOrderforvendor", "ViewOrderforvendor", "~/Test_Lab/ViewBookedOrderforVendor.aspx", true);
        //routes.MapPageRoute("UserLogin", "UserLogin", "~/Test_Lab/UserLogin.aspx", true);
        //routes.MapPageRoute("HomePage", "HomePage", "~/Test_Lab/HomePage.aspx", true);
        //routes.MapPageRoute("LabDashboard", "LabDashboard", "~/Test_Lab/LabDashboard.aspx", true);
        //routes.MapPageRoute("TestLabLogin", "TestLabLogin", "~/Test_Lab/Login.aspx", true);
        //routes.MapPageRoute("TestLab", "TestLab", "~/Test_Lab/HomePage.aspx", true);
        //routes.MapPageRoute("UserTestDetails", "UserTestDetails", "~/Test_Lab/UserTestDetails.aspx", true);
        //routes.MapPageRoute("CurrentBookings", "CurrentBookings", "~/Test_Lab/UserCurrentBookings.aspx", true);
        //routes.MapPageRoute("AppndRejectedBookings", "AppndRejectedBookings", "~/Test_Lab/UserApprovedndRejectedBookings.aspx", true);
        //routes.MapPageRoute("Userbookingdetails", "Userbookingdetails", "~/Test_Lab/Userbookingdetails.aspx", true);
        //routes.MapPageRoute("ApprovendRejectbookings", "ApprovendRejectbookings", "~/Test_Lab/DPSUApprovendRejectedbookings.aspx", true);
        //routes.MapPageRoute("Booking", "Booking", "~/Test_Lab/Booking_details.aspx", true);
        //routes.MapPageRoute("Register", "Register", "~/Test_Lab/Registration.aspx", true);
        routes.MapPageRoute("Make2Report", "Make2Report", "~/Report/Make2_Report.aspx", true);
        routes.MapPageRoute("CategoryWiseRep", "CategoryWiseRep", "~/Report/CategoryWise_Report.aspx", true);
        routes.MapPageRoute("SONOIndig", "SONOIndig", "~/Report/SONOIndigi_Report.aspx", true);
        routes.MapPageRoute("EOINOSOINDIG", "EOINOSOINDIG", "~/Report/EOIDataNoSOIndi_Report.aspx", true);
        routes.MapPageRoute("Video", "Video", "~/Videos/Videos.aspx", true);
        routes.MapPageRoute("Waiting", "Waiting", "~/StopSite.aspx", true);
        // routes.MapPageRoute("ProductList", "ProductList", "~/MaintancePage.aspx", true);        
        routes.MapPageRoute("Summery", "Summery", "~/Report/SummaryDetails.aspx", true);
        routes.MapPageRoute("GHelpDesk", "GHelpDesk", "~/Grievance/G_HelpDesk.aspx", true);
        routes.MapPageRoute("GOfficialLogin", "GOfficialLogin", "~/Grievance/G_Login.aspx", true);
        routes.MapPageRoute("GADashboard", "GADashboard", "~/Grievance/G_Dashboard.aspx", true);
        routes.MapPageRoute("GViewRecords", "GViewRecords", "~/Grievance/G_ViewGrivanceRecord.aspx", true);
        routes.MapPageRoute("GUpdateReply", "GUpdateReply", "~/Grievance/G_AssignTicketUpdateStatus.aspx", true);
        routes.MapPageRoute("AssignTicket", "AssignTicket", "~/Grievance/GAssignTicket.aspx", true);
        routes.MapPageRoute("AssignJob", "AssignJob", "~/Grievance/GAssignJob.aspx", true);
        routes.MapPageRoute("Howtosearch", "Howtosearch", "~/User/HowToSearch.aspx", true);
        routes.MapPageRoute("V_Add_User", "V_Add_User", "~/Vendor/V_Add_User.aspx", true);
        routes.MapPageRoute("V_User_List", "V_User_List", "~/Vendor/V_User_List.aspx", true);
        routes.MapPageRoute("V_NodalOfficer_List", "V_NodalOfficer_List", "~/Vendor/V_NodalOfficer_List.aspx", true);
        routes.MapPageRoute("ViewProfileMigrationList", "ViewProfileMigrationList", "~/Vendor/ViewProfileMigrationList.aspx", true);
        routes.MapPageRoute("SiteMap", "SiteMap", "~/User/SiteMap.aspx", true);
        routes.MapPageRoute("HViewStatusTicket", "HViewStatusTicket", "~/Grievance/G_ViewTicketsSingleClick.aspx", true);
        routes.MapPageRoute("V_Update_Pincode", "V_Update_Pincode", "~/Vendor/V_Update_Pincode.aspx", true);
        routes.MapPageRoute("ProductWizard", "ProductWizard", "~/User/UpdateWizard.aspx", true);
       routes.MapPageRoute("Registration", "Registration", "~/Vendor/VendorRegistration.aspx", true); 
        routes.MapPageRoute("EOIU", "EOIU", "~/User/MasterUpdateEOI.aspx", true);
        routes.MapPageRoute("NotDisplayPortalProduct", "NotDisplayPortalProduct", "~/User/NotDisplayProductOnPortal.aspx", true);
        routes.MapPageRoute("SupplyOrderU", "SupplyOrderU", "~/User/MasterUpdateSupplyOrder.aspx", true);
        routes.MapPageRoute("SuccessStoryU", "SuccessStoryU", "~/User/MasterUpdateSuccessStory.aspx", true);
        routes.MapPageRoute("Exception", "Exception", "~/ViewException.aspx", true);
        routes.MapPageRoute("CreateUserPassword", "CreateUserPassword", "~/Vendor/CreateUserPassword.aspx", true);
        routes.MapPageRoute("confirmEmailupdation", "confirmEmailupdation", "~/Vendor/confirmEmailupdation.aspx", true);
        routes.MapPageRoute("confirmProfileMigration", "confirmProfileMigration", "~/Vendor/confirmProfileMigration.aspx", true);
        routes.MapPageRoute("V_Add_NodalOfficer", "V_Add_NodalOfficer", "~/Vendor/V_Add_NodalOfficer.aspx", true);
        routes.MapPageRoute("MasterReasoneUpdate", "MasterReasoneUpdate", "~/User/MasterReasoneUpdate.aspx", true);        
    }
</script>
