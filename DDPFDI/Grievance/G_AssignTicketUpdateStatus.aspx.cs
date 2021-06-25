using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessLayer;
using Encryption;
using System.IO;

public partial class Grievance_G_AssignTicketUpdateStatus : System.Web.UI.Page
{
    Logic Lo = new Logic();
    DataTable dt = new DataTable();
    Cryptography Enc = new Cryptography();
    DataUtility Co = new DataUtility();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["me"] != null && Request.QueryString["msb"] != null && Request.QueryString["mqu"] != null && Request.QueryString["iismref"] != null && Request.QueryString["mque"] != null)
            {
                txtsubject.Text = Enc.DecryptData(Request.QueryString["msb"].ToString());
                txtquery.Text = Enc.DecryptData(Request.QueryString["mqu"].ToString());
                hfemail.Value = Enc.DecryptData(Request.QueryString["me"].ToString());
                hfticketclose.Value = Enc.DecryptData(Request.QueryString["iismref"].ToString());
                divotp.Visible = false;
                ddlticketstatus2();
            }
            else
            {
                Response.Redirect("AssignJob");
            }
        }
    }
    #region OTPCode
    protected void GenerateOTP()
    {
        try
        {
            string numbers = "1234567890";
            string characters = numbers;
            int length = int.Parse("6");
            string otp = string.Empty;
            for (int i = 0; i < length; i++)
            {
                string character = string.Empty;
                do
                {
                    int index = new Random().Next(0, characters.Length);
                    character = characters.ToCharArray()[index].ToString();
                } while (otp.IndexOf(character) != -1);
                otp += character;
            }
            hfotp.Value = otp;
            txtotp.Text = hfotp.Value;
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }
    private void sendMailOTP(string email)
    {
        try
        {
            string body;
            using (StreamReader reader = new StreamReader(Server.MapPath("~/emailPage/OTP.html")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{OTP}", hfotp.Value);
            SendMail s;
            s = new SendMail();
            s.CreateMail("noreply-srijandefence@gov.in", email.ToString(), "Ticket Close OTP.", body);
            s.sendMail();
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }

    }
    #endregion
    protected void lbupdate_Click(object sender, EventArgs e)
    {
        try
        {
            if (chkstatus.SelectedIndex != -1 && txtreply.Text != "" && ddlticketstatus.SelectedItem.Text != "Select")
            {
                if (chkstatus.SelectedValue == "Y")
                {
                    if (fufile.HasFile != false)
                    {
                        string FileType = Path.GetExtension(fufile.FileName);
                        Int64 FileSize = fufile.FileContent.Length;
                        if (DataUtility.Instance.GetFileFilter1(fufile.FileName) != false)
                        {
                            if (FileSize < 1000000)
                            {
                                string FilePathName = hfotp.Value + "_" + DateTime.Now.ToString("hh_mm_ss") + fufile.FileName;
                                fufile.SaveAs(HttpContext.Current.Server.MapPath("/Grievance/GFiles/") + "\\" + FilePathName);
                                hfimage.Value = "Grievance/GFiles/" + FilePathName;
                                GenerateOTP();
                                sendMailOTP(Enc.DecryptData(Session["GUser"].ToString()));
                                divupdate.Visible = false;
                                divotp.Visible = true;
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Six digit no sent on your registerd email id.Enter 6 digit ticket close no to close ticket.')", true);
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Invalid file size only 1 MB file allowed.')", true);
                            }
                        }
                        else
                        { ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Invalid file format only .pdf or .doc allow.')", true); }
                    }
                    else
                    {
                        divupdate.Visible = false;
                        divotp.Visible = true;
                        GenerateOTP();
                        sendMailOTP(Enc.DecryptData(Session["GUser"].ToString()));
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Six digit no sent on your registerd email id.Enter 6 digit ticket close no to close ticket.')", true);

                    }
                }
                else
                {
                    divupdate.Visible = true;
                    divotp.Visible = false;
                    string m_file = "";
                    if (fufile.HasFile != false)
                    {
                        string FileType = Path.GetExtension(fufile.FileName);
                        Int64 FileSize = fufile.FileContent.Length;
                        if (DataUtility.Instance.GetFileFilter1(fufile.FileName) != false)
                        {
                            if (FileSize < 1000000)
                            {
                                string FilePathName = hfotp.Value + "_" + DateTime.Now.ToString("hh_mm_ss") + fufile.FileName;
                                fufile.SaveAs(HttpContext.Current.Server.MapPath("/Grievance/GFiles/") + "\\" + FilePathName);
                                m_file = "Grievance/GFiles/" + FilePathName;
                                DataTable dtupdateticketreply = Lo.RetriveHelpdesk(Convert.ToInt32(ddlticketstatus.SelectedItem.Value), 0, 2, hfticketclose.Value, txtreply.Text, m_file.ToString(),
                                    Enc.DecryptData(Session["Gid"].ToString()), "updatereply");
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Invalid file size only 1 MB file allowed.')", true);
                            }
                        }
                        else
                        { ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Invalid file format only .pdf or .doc allow.')", true); }
                    }
                    else
                    {
                        DataTable dtupdateticketreply = Lo.RetriveHelpdesk(Convert.ToInt32(ddlticketstatus.SelectedItem.Value), 0, 2, hfticketclose.Value, txtreply.Text, m_file.ToString(),
                                    Enc.DecryptData(Session["Gid"].ToString()), "updatereply");
                    }
                    SendCloseMail();
                    cleartext();
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Successfully sent mail or update reply.')", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Fill all field manadatory.')", true);
            }
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }
    protected void cleartext()
    {
        hfemail.Value = ""; hfimage.Value = ""; hfotp.Value = "";
        txtotp.Text = ""; txtquery.Text = ""; txtreply.Text = "";
        txtsubject.Text = ""; chkstatus.SelectedIndex = -1; hfticketclose.Value = "";
    }
    protected void btnotp_Click(object sender, EventArgs e)
    {
        if (hfotp.Value == txtotp.Text)
        {
            try
            {
                DataTable dtupdateticketreply = Lo.RetriveHelpdesk(Convert.ToInt32(ddlticketstatus.SelectedItem.Value), 1, 2, hfticketclose.Value, txtreply.Text, hfimage.Value,
                Enc.DecryptData(Session["Gid"].ToString()), "updatereply");
                SendCloseMail();
                cleartext();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Successfully sent mail or issue/feedback close.');window.location='AssignJob';", true);
            }
            catch (Exception ex)
            { }
        }
        else
        { ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Invalid OTP.')", true); }
    }
    protected void SendCloseMail()
    {
        try
        {
            string body;
            if (Enc.DecryptData(Request.QueryString["mque"].ToString()) == "FeedBack")
            {
                using (StreamReader reader = new StreamReader(Server.MapPath("~/emailPage/G_CloseTicket.html")))
                {
                    body = reader.ReadToEnd();
                }

                body = body.Replace("{ticketno}", hfticketclose.Value);
                body = body.Replace("{sub}", txtsubject.Text);
                body = body.Replace("{reply}", txtreply.Text);
                SendMail s;
                s = new SendMail();
                s.CreateMail("noreply-srijandefence@gov.in", hfemail.Value, "Thanks Mail from Srijan FeedBack " + hfticketclose.Value + ".", body);
                s.sendMail();
            }
            else
            {
                if (chkstatus.SelectedItem.Value == "N")
                {
                    using (StreamReader reader = new StreamReader(Server.MapPath("~/emailPage/G_CloseTicket.html")))
                    {
                        body = reader.ReadToEnd();
                    }
                    body = body.Replace("{ticketno}", hfticketclose.Value);
                    body = body.Replace("{sub}", txtsubject.Text);
                    body = body.Replace("{issue}", txtquery.Text);
                    body = body.Replace("{reply}", ddlticketstatus.SelectedItem.Text);
                    SendMail s;
                    s = new SendMail();
                    s.CreateMail("noreply-srijandefence@gov.in", hfemail.Value, "Ticket" + ddlticketstatus.SelectedItem.Text + " " + hfticketclose.Value + ".", body);
                    s.sendMail();

                }
                else
                {
                    using (StreamReader reader = new StreamReader(Server.MapPath("~/emailPage/G_CloseTicket.html")))
                    {
                        body = reader.ReadToEnd();
                    }
                    body = body.Replace("{ticketno}", hfticketclose.Value);
                    body = body.Replace("{sub}", txtsubject.Text);
                    body = body.Replace("{issue}", txtquery.Text);
                    body = body.Replace("{reply}", ddlticketstatus.SelectedItem.Text);
                    SendMail s;
                    s = new SendMail();
                    s.CreateMail("noreply-srijandefence@gov.in", hfemail.Value, "Ticket Close " + hfticketclose.Value + ".", body);
                    s.sendMail();
                }
            }
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }
    protected void ddlticketstatus2()
    {
        DataTable dtticket = Lo.RetriveHelpdesk(0, 0, 0, "", "", "", "", "Status");
        if (dtticket.Rows.Count > 0)
        {
            Co.FillDropdownlist(ddlticketstatus, dtticket, "Status", "HelpStatus");
            ddlticketstatus.Items.Insert(0, "Select");
        }
    }
}