using System.Web.UI;
using Encryption;
using BusinessLayer;
using System;

public partial class Vendor_VendorChangePassword : System.Web.UI.Page
{
    Logic Lo = new Logic();
    Cryptography ObjEnc = new Cryptography();
    Cryptography objEnc = new Cryptography();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["id"] != null)
        {
            string id = Request.QueryString["id"].ToString().Replace(" ", "+");
            lblPageName.Text = objEnc.DecryptData(id);
        }
    }
    protected void btnsub_Click(object sender, EventArgs e)
    {
        if (txtoldpass.Text != "" && txtnewpass.Text != "" && txtreppass.Text != "")
        {
            if (txtnewpass.Text == txtreppass.Text)
            {
                string UpdatePassword = Lo.UpdateLoginPassword(ObjEnc.EncryptData(txtnewpass.Text), ObjEnc.EncryptData(txtoldpass.Text), ObjEnc.DecryptData(Session["User"].ToString()),
                    "LoginOldVendor", txtnewpass.Text, "");
                if (UpdatePassword == "true")
                {
                    divmsg.InnerHtml = "Password change successfully";
                    divmsg.Attributes.Add("Class", "alert alert-success");
                    divmsg.Visible = true;

                }
                else
                {
                    divmsg.InnerHtml = "Password not change";
                    divmsg.Attributes.Add("Class", "alert alert-danger");
                    divmsg.Visible = true;
                }
            }
            else
            {
                divmsg.InnerHtml = "Password not match.";
                divmsg.Attributes.Add("Class", "alert alert-warning");
                divmsg.Visible = true;

            }
        }
        else
        {
            divmsg.InnerHtml = "All field fill mandatory.";
            divmsg.Attributes.Add("Class", "alert alert-warning");
            divmsg.Visible = true;

        }
    }
    protected void btncan_Click(object sender, EventArgs e)
    {
        cleartext();
    }
    protected void cleartext()
    {
        txtnewpass.Text = "";
        txtoldpass.Text = "";
        txtreppass.Text = "";
        divmsg.InnerHtml = "";
        divmsg.Visible = false;
    }
}