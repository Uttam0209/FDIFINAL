using System;
using Encryption;
using System.Web;
using System.Data;
using BusinessLayer;

public partial class Error : System.Web.UI.Page
{
    Cryptography Enc = new Cryptography();
    Logic Lo = new Logic();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {           
            Load();
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }
    protected void Load()
    {
        string generalErrorMsg = "An Error occured while rendering this page. Please try again." +
            "If this error continues, Please contact Help Desk at +011-20836145";
        string httpErrorMsg = "An HTTP error occurred. Page Not found. Please try again.";
        string unhandledErrorMsg = "The error was unhandled by application code.";
        // Display safe error message.
        FriendlyErrorMsg.Text = generalErrorMsg;

        // Determine where error was handled.
        string errorHandler = Request.QueryString["handler"];
        if (errorHandler == null)
        {
            errorHandler = "Error Page";
        }
        // Get the last error from the server.
        Exception ex = Server.GetLastError();
        // Get the error number passed as a querystring value.
        string errorMsg = Request.QueryString["msg"];
        if (errorMsg == "404")
        {
            ex = new HttpException(404, httpErrorMsg, ex);
            FriendlyErrorMsg.Text = ex.Message;
        }
        // If the exception no longer exists, create a generic exception.
        if (ex == null)
        {
            ex = new Exception(unhandledErrorMsg);
        }     
        DataTable dt = new DataTable();
        dt = Lo.RetriveFilterCode("", "", "GetExceptionLog");
        ErrorDetailedMsg.Text = "#" + dt.Rows[0]["LogId"].ToString();
      
        ExceptionUtility.LogException(ex, errorHandler);
        // Clear the error from the server.
        Server.ClearError();
    }

    protected void btnbackerror_Click(object sender, EventArgs e)
    {
        Response.RedirectToRoute("ProductList");
    }
}
