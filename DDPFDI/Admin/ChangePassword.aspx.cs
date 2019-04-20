using System;
using System.Web.UI;
using Encryption;
using BusinessLayer;

public partial class Admin_ChangePassword : System.Web.UI.Page
{
    Logic Lo = new Logic();
    Cryptography ObjEnc = new Cryptography();
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    protected void btnsub_Click(object sender, EventArgs e)
    {
        if (txtoldpass.Text != "" && txtnewpass.Text != "" && txtreppass.Text != "")
        {
            if (txtnewpass.Text == txtreppass.Text)
            {
                string UpdatePassword = Lo.UpdateLoginPassword(ObjEnc.EncryptData(txtnewpass.Text), ObjEnc.EncryptData(txtoldpass.Text), ObjEnc.DecryptData(Session["User"].ToString()), "LoginOld");
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