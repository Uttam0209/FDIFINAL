using BusinessLayer;
using System;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Encryption;

public partial class Grievance_G_HelpDesk : System.Web.UI.Page
{
    #region Pagevariable
    private Logic Lo = new Logic();
    private DataTable DtGrid = new DataTable();
    DataUtility Co = new DataUtility();
    HybridDictionary hyhelp = new HybridDictionary();
    private string _msg = string.Empty;
    private string _sysMsg = string.Empty;
    DataTable dtnew = new DataTable();
    Cryptography Enc = new Cryptography();
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillCapctha();
            BindPortal();
            BindState();
            // BindSubject();
            divsum.Visible = false;
            divotp.Visible = false;
            divstatus.Visible = true;
            lblfrom.Text = ">> Check ticket Status";
            MenuLink();
        }
    }
    #region Captcha code
    public void FillCapctha()
    {
        try
        {
            Random random = new Random();
            string combination = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            StringBuilder captcha = new StringBuilder();
            for (int i = 0; i < 6; i++)
                captcha.Append(combination[random.Next(combination.Length)]);
            Session["captcha"] = captcha.ToString();
            imgCaptcha.ImageUrl = "../GenerateCaptcha.aspx?" + DateTime.Now.Ticks.ToString();
        }
        catch
        {
            throw;
        }
    }
    protected void btnCaptchaNew_Click(object sender, EventArgs e)
    {
        FillCapctha();
    }
    #endregion
    #region EmailVeryfi  
    public static bool VerifyEmailID(string email)
    {
        string expresion;
        expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
        if (Regex.IsMatch(email, expresion))
        {
            if (Regex.Replace(email, expresion, string.Empty).Length == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
    #endregion
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
            //txtotp.Text = otp;
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
            s.CreateMail("noreply-srijandefence@gov.in", email.ToString(), "OTP Verification HelpDesk.", body);
            s.sendMail();
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }

    }
    protected void lbresendotp_Click(object sender, EventArgs e)
    {
        try
        {
            GenerateOTP();
            if (Iusefor.Value == "Save")
            {
                sendMailOTP(txtemail.Text);
            }
            else
            {
                DataTable mdt = (DataTable)ViewState["mRetDt"];
                sendMailOTP(mdt.Rows[0]["Email"].ToString());
            }
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('OTP resend successfully.')", true);
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }
    #endregion
    #region SaveIssue
    public void SaveHelp()
    {
        hyhelp["HFrom"] = Co.RSQandSQLInjection(ddltype.SelectedItem.Text, "soft");
        hyhelp["QueryFor"] = Co.RSQandSQLInjection(rbselectsub.SelectedItem.Value, "soft");
        hyhelp["Name"] = Co.RSQandSQLInjection(txtname.Text, "soft");
        hyhelp["Email"] = Co.RSQandSQLInjection(txtemail.Text, "soft");
        hyhelp["MobileNo"] = Co.RSQandSQLInjection(txtmobile.Text, "soft");
        hyhelp["State"] = Co.RSQandSQLInjection(txtstate.SelectedItem.Text, "soft");
        hyhelp["Subject"] = Co.RSQandSQLInjection(txtsubject.Text, "soft");
        hyhelp["Address"] = Co.RSQandSQLInjection(txtaddress.Text, "soft");
        hyhelp["Issue"] = Co.RSQandSQLInjection(txtIssueorFeedback.Text, "hard");
        hyhelp["Files"] = hfimage.Value;
        hyhelp["SubSubjectId"] = Co.RSQandSQLInjection(ddlSubSubject.SelectedItem.Value, "soft"); ;
        string str = Lo.SaveHelpDesk(hyhelp, out _msg, out _sysMsg);
        if (str != "")
        {
            SendEmailCode(str);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert",
                "alert('Thank-you for your " + rbselectsub.SelectedItem.Text + ".Your ticket no is " + str.ToString()
                + " for future reference please save it and track your status using this ticket no.')", true);
        }
        else
        {
            if (File.Exists(hfimage.Value))
            {
                try
                {
                    File.Delete(hfimage.Value);
                }
                catch (Exception ex)
                {
                    //Do something
                }
            }
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Issue not rised.Please re-enter details.')", true);
        }
    }
    #endregion
    #region SendMailCode
    public void SendEmailCode(string refno)
    {
        try
        {
            string body;
            using (StreamReader reader = new StreamReader(Server.MapPath("~/emailPage/HelpDeskIssueMail.html")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{Name}", txtname.Text);
            body = body.Replace("{Issue}", txtIssueorFeedback.Text);
            body = body.Replace("{Refno}", refno.ToString());
            body = body.Replace("{portal}", ddltype.SelectedItem.Text);
            body = body.Replace("{rb}", rbselectsub.SelectedItem.Text);
            SendMail s;
            s = new SendMail();
            s.CreateMail("noreply-srijandefence@gov.in", txtemail.Text, "HelpDesk " + rbselectsub.SelectedItem.Text, body);
            s.sendMail();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);
        }

    }
    #endregion
    #region HelpdeskCode
    protected void cleartext()
    {
        txtIssueorFeedback.Text = "";
        txtaddress.Text = "";
        txtCaptcha.Text = "";
        txtemail.Text = "";
        txtmobile.Text = "";
        txtname.Text = "";
        txtotp.Text = "";
        txtstate.SelectedIndex = -1;
        txtsubject.SelectedIndex = -1;
        hfotp.Value = "";
        ddltype.SelectedIndex = -1;
        rbselectsub.SelectedIndex = -1;
        Session.Abandon();
        Session.Clear();
        FillCapctha();

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtemail.Text != "" && txtaddress.Text != "" && txtCaptcha.Text != "" && txtIssueorFeedback.Text != "" && txtmobile.Text != "" && txtname.Text != "" && txtstate.SelectedItem.Text != "Select" && txtsubject.SelectedItem.Text != "Select")
            {
                if (VerifyEmailID(txtemail.Text) == true)
                {
                    if (txtCaptcha.Text != Session["captcha"].ToString())
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Invalid Captcha')", true);
                    }
                    else
                    {
                        if (fuupload.HasFile != false)
                        {
                            string FileType = Path.GetExtension(fuupload.FileName);
                            Int64 FileSize = fuupload.FileContent.Length;
                            if (DataUtility.Instance.GetFileFilter1(fuupload.FileName) != false)
                            {
                                if (FileSize < 1000000)
                                {
                                    GenerateOTP();
                                    sendMailOTP(txtemail.Text);
                                    string FilePathName = hfotp.Value + "_" + DateTime.Now.ToString("hh_mm_ss") + hfimage.Value;
                                    fuupload.SaveAs(HttpContext.Current.Server.MapPath("/Grievance/GFiles/") + "\\" + FilePathName);
                                    hfimage.Value = "Grievance/GFiles/" + FilePathName;
                                    divsum.Visible = false;
                                    divotp.Visible = true;
                                    Iusefor.Value = "Save";
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
                            GenerateOTP();
                            sendMailOTP(txtemail.Text.Trim());
                            hfimage.Value = fuupload.FileName;
                            divsum.Visible = false;
                            divotp.Visible = true;
                            Iusefor.Value = "Save";
                        }
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Invalid Email format.')", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('All field fill mandatory.')", true);
            }
        }
        catch (Exception ex)
        { }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        cleartext();
    }
    protected void lbRiseaTicket_Click(object sender, EventArgs e)
    {
        divsum.Visible = true;
        divotp.Visible = false;
        divstatus.Visible = false;
        lblfrom.Text = ">> Raise ticket";
    }
    protected void BindPortal()
    {
        DataTable dtPortal = Lo.RetriveHelpdesk(0, 0, 0, "", "", "", "", "BindProtal");
        if (dtPortal.Rows.Count > 0)
        {
            Co.FillDropdownlist(ddltype, dtPortal, "PortalName", "PortalId");
            ddltype.Items.Insert(0, "Select");
        }
    }
    protected void BindState()
    {
        DataTable DtCountry = Lo.RetriveState("Select");
        if (DtCountry.Rows.Count > 0)
        {
            Co.FillDropdownlist(txtstate, DtCountry, "StateName", "StateId");
            txtstate.Items.Insert(0, "Select");
        }
    }
    protected void BindSubject()
    {
        DataTable DtSubject = Lo.RetriveHelpdesk(Convert.ToInt32(ddltype.SelectedItem.Value), 0, 0, "", "", "", "", "Subject");
        if (DtSubject.Rows.Count > 0)
        {
            Co.FillDropdownlist(txtsubject, DtSubject, "SubjectName", "SubjectId");
            txtsubject.Items.Insert(0, "Select");
        }
        else
        {
            txtsubject.Enabled = false;
        }
    }
    protected void BindSubSubject()
    {
        DataTable DtSubSubject = Lo.RetriveHelpdesk(Convert.ToInt32(txtsubject.SelectedItem.Value), 0, 0, "", "", "", "", "SubSubject");
        if (DtSubSubject.Rows.Count > 0)
        {
            Co.FillDropdownlist(ddlSubSubject, DtSubSubject, "SubSubjectName", "SubSubject");
            ddlSubSubject.Items.Insert(0, "Select");
            ddlSubSubject.Enabled = true;
        }
        else
        {
            ddlSubSubject.Enabled = false;
        }
    }
    protected void rbselectsub_SelectedIndexChanged(object sender, EventArgs e)
    {
        CheckEnableFalse();
    }
    protected void CheckEnableFalse()
    {
        if (rbselectsub.Enabled == true && rbselectsub.SelectedIndex != -1)
        {
            txtaddress.Enabled = true;
            txtCaptcha.Enabled = true;
            txtemail.Enabled = true;
            txtIssueorFeedback.Enabled = true;
            txtmobile.Enabled = true;
            txtname.Enabled = true;
            txtstate.Enabled = true;
            txtsubject.Enabled = true;
            btnCaptchaNew.Enabled = true;
            btnSubmit.Enabled = true;
            fuupload.Enabled = true;
        }
        else
        {
            txtaddress.Enabled = false;
            txtCaptcha.Enabled = false;
            txtemail.Enabled = false;
            txtIssueorFeedback.Enabled = false;
            txtmobile.Enabled = false;
            txtname.Enabled = false;
            txtstate.Enabled = false;
            txtsubject.Enabled = false;
            btnCaptchaNew.Enabled = false;
            btnSubmit.Enabled = false;
            fuupload.Enabled = false;
            rbselectsub.SelectedIndex = -1;
            ddlSubSubject.Enabled = false;
        }

    }
    protected void ddltype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddltype.SelectedItem.Text != "Select")
        {
            rbselectsub.Enabled = true;
            BindSubject();
        }
        else
        {
            rbselectsub.Enabled = false;
            CheckEnableFalse();
            txtsubject.Enabled = false;
        }
    }
    protected void txtsubject_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (txtsubject.SelectedItem.Text != "Select")
        {
            BindSubSubject();
        }
        else
        {
            ddlSubSubject.Enabled = false;
        }
    }
    #endregion
    #region OtpCodePanel
    protected void lbsubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtotp.Text != "" && txtotp.Text == hfotp.Value)
            {
                if (Iusefor.Value == "Save")
                {
                    try
                    {
                        SaveHelp();
                        cleartext();
                        divsum.Visible = true;
                        divotp.Visible = false;
                    }
                    catch (Exception ex)
                    { ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Technical error:- " + ex.Message + "Query not submitted some error occurd.')", true); }

                }
                else
                {
                    try
                    {
                        dtnew = (DataTable)ViewState["mRetDt"];
                        dlissue.DataSource = dtnew;
                        dlissue.DataBind();
                        divotp.Visible = false;
                        divsum.Visible = false;
                        divstatus.Visible = true;
                        if (dtnew.Rows[0]["Status"].ToString() == "4")
                        {
                            HtmlControl li = (HtmlGenericControl)FindControl("step1");
                            li.Attributes.Add("class", "step active");
                            HtmlControl li1 = (HtmlGenericControl)FindControl("step2");
                            li1.Attributes.Add("class", "step active");
                            HtmlControl li2 = (HtmlGenericControl)FindControl("step3");
                            li2.Attributes.Add("class", "step active");
                            HtmlControl li3 = (HtmlGenericControl)FindControl("step4");
                            li3.Attributes.Add("class", "step active");

                        }
                        else if (dtnew.Rows[0]["Status"].ToString() == "3")
                        {
                            HtmlControl li = (HtmlGenericControl)FindControl("step1");
                            li.Attributes.Add("class", "step active");
                            HtmlControl li1 = (HtmlGenericControl)FindControl("step2");
                            li1.Attributes.Add("class", "step active");
                            HtmlControl li2 = (HtmlGenericControl)FindControl("step3");
                            li2.Attributes.Add("class", "step active");
                            HtmlControl li3 = (HtmlGenericControl)FindControl("step4");
                            li3.Attributes.Add("class", "step");

                        }
                        else if (dtnew.Rows[0]["Status"].ToString() == "2")
                        {
                            HtmlControl li = (HtmlGenericControl)FindControl("step1");
                            li.Attributes.Add("class", "step active");
                            HtmlControl li1 = (HtmlGenericControl)FindControl("step2");
                            li1.Attributes.Add("class", "step active");
                            HtmlControl li2 = (HtmlGenericControl)FindControl("step3");
                            li2.Attributes.Add("class", "step");
                            HtmlControl li3 = (HtmlGenericControl)FindControl("step4");
                            li3.Attributes.Add("class", "step");
                        }
                        else if (dtnew.Rows[0]["Status"].ToString() == "1")
                        {
                            HtmlControl li = (HtmlGenericControl)FindControl("step1");
                            li.Attributes.Add("class", "step active");
                            HtmlControl li1 = (HtmlGenericControl)FindControl("step2");
                            li1.Attributes.Add("class", "step");
                            HtmlControl li2 = (HtmlGenericControl)FindControl("step3");
                            li2.Attributes.Add("class", "step");
                            HtmlControl li3 = (HtmlGenericControl)FindControl("step4");
                            li3.Attributes.Add("class", "step");
                        }
                        divtrack.Visible = true;
                    }
                    catch (Exception ex)
                    { ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Technical error:- " + ex.Message + " User Error:- No Record Found.')", true); }

                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Invalid OTP.')", true);
            }
        }
        catch (Exception ex)
        { ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert(" + ex.Message.ToString() + ")", true); }
    }
    #endregion
    #region StatusCode
    protected void btnTicketStatus_Click(object sender, EventArgs e)
    {
        divsum.Visible = false;
        divotp.Visible = false;
        divstatus.Visible = true;
        lblfrom.Text = ">> Check ticket Status";
    }
    protected void lbstatus_Click(object sender, EventArgs e)
    {
        DataTable DtStatus = Lo.RetriveHelpdesk(0, 0, 0, txtrefeno.Text, "", "", "", "RTrack");
        if (DtStatus.Rows.Count > 0)
        {
            ViewState["mRetDt"] = DtStatus;
            GenerateOTP();
            sendMailOTP(DtStatus.Rows[0]["Email"].ToString());
            divotp.Visible = true;
            divstatus.Visible = false;
            divsum.Visible = false;
            Iusefor.Value = "statusotp";
            lblrefno.Text = txtrefeno.Text;
            //  txtotp.Text = hfotp.Value;
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('No Record Found.')", true);
        }
    }
    #endregion
    #region MenuCode
    protected void MenuLink()
    {
        if (Session["User"] != null)
        {
            linkusername.Text = Enc.DecryptData(Session["User"].ToString());
            linkusername.Visible = true;
            linkusername.Text = "Welcome: " + linkusername.Text;
            lnkfeedback.Visible = true;
            linklogin.Visible = false;
            lblmis.Visible = true;
            lbllogout.Visible = true;
            lbSuccesstory.Visible = true;
            reportdiv.Visible = true;
            mhwparti.Visible = false;
        }
        else
        {
            lbSuccesstory.Visible = false;
            linklogin.Visible = true;
            linkusername.Visible = false;
            lblmis.Visible = false;
            lbllogout.Visible = false;
            PR.Visible = false;
            reportdiv.Visible = false;
            mhwparti.Visible = true;
        }
    }
    #endregion
    protected void lbllogout_Click(object sender, EventArgs e)
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
        Session.Remove("Type");
        Session.Remove("User");
        Session.Remove("CompanyRefNo");
        Session.Remove("SFToken");
        Session.RemoveAll();
        Session.Clear();
        Session.Contents.RemoveAll();
        Session.Abandon();
        Response.RedirectToRoute("Productlist");
    }
}