using System;
using System.Web;
using System.Web.UI;
using Encryption;
using BusinessLayer;
using System.Text;
using System.Data;
public partial class Vendor_VendorMaster2 : System.Web.UI.MasterPage
{
    Cryptography ObjEnc = new Cryptography();
    Logic Lo = new Logic();
    DataUtility Co = new DataUtility();
    private object divregistration;
    bool MsgStatus;
    Int64 MId = 0;
    public string userID;
    public string usertype;
    protected void Page_Load (object sender, EventArgs e)
    {
        userID = ObjEnc.DecryptData(Session["User"].ToString());
        usertype = ObjEnc.DecryptData(Session["Type"].ToString());
    }
    protected void lbllogout_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Session.Clear();
        Response.RedirectToRoute("VendorLogin");
    }
   


}
