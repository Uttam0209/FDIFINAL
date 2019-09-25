using BusinessLayer;
using Encryption;
using System;
using System.Collections.Specialized;
using System.Data;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Web.Helpers;
public partial class Admin_AddMasterCompany : System.Web.UI.Page
{
    private Logic Lo = new Logic();
    private Cryptography Enc = new Cryptography();
    private DataUtility Co = new DataUtility();
    private long id = 0;
    private string _sysMsg = string.Empty;
    private string _msg = string.Empty;
    private string intrestedare = "";
    private string Masterallowed = "";
    private string role = "";
    private DropDownList mddlControl;
    private HybridDictionary HySave = new HybridDictionary();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["Type"] != null)
            {
                try
                {
                    if (Request.QueryString["mu"] != null)
                    {
                        string strid = Request.QueryString["id"].ToString().Replace(" ", "+");
                        string strPageName = Enc.DecryptData(strid);
                        StringBuilder strheadPage = new StringBuilder();
                        strheadPage.Append("<ul class='breadcrumb'>");
                        string[] MCateg = strPageName.Split(new string[] { ">>" }, StringSplitOptions.RemoveEmptyEntries);
                        string MmCval = "";
                        for (int x = 0; x < MCateg.Length; x++)
                        {
                            MmCval = MCateg[x];
                            if (MmCval == " View ")
                            {
                                MmCval = "Add";
                            }

                            strheadPage.Append("<li class=''><span>" + MmCval + "</span></li>");
                        }
                        divHeadPage.InnerHtml = strheadPage.ToString();
                        strheadPage.Append("</ul");
                        divOfficerEmail.Visible = false;
                        ViewState["UserLoginEmail"] = Session["User"].ToString();
                        if (Enc.DecryptData(Request.QueryString["mu"].ToString()) == "Panel1")
                        {
                            mastercompany.Visible = true;
                            masterfacotry.Visible = false;
                            lblName.Text = "Company";
                            btnsubmit.Text = "Save Company";
                            BindMasterCompany();
                            BindMasterData();
                            Intrested.Visible = true;
                            MenuAlot.Visible = true;
                            divRole.Visible = true;
                            gvcompanydetailsave.Visible = false;
                        }
                        else if (Enc.DecryptData(Request.QueryString["mu"].ToString()) == "Panel2")
                        {
                            mastercompany.Visible = true;
                            masterfacotry.Visible = false;
                            BindMasterCompany();
                            lblName.Text = "Division/Plant";
                            btnsubmit.Text = "Save Division";
                            GridcompanyVisible();
                        }
                        else if (Enc.DecryptData(Request.QueryString["mu"].ToString()) == "Panel3")
                        {
                            mastercompany.Visible = true;
                            masterfacotry.Visible = true;
                            BindMasterCompany();
                            ddlmaster_SelectedIndexChanged(sender, e);
                            lblName.Text = "Unit";
                            btnsubmit.Text = "Save Unit";
                            GridcompanyVisible();
                        }
                        else
                        {
                            mastercompany.Visible = false;
                            masterfacotry.Visible = false;
                            lblName.Text = "Company/Organization";
                            BindMasterData();
                            BindMasterCompany();
                            Intrested.Visible = true;
                            MenuAlot.Visible = true;
                            if (Enc.DecryptData(Session["Type"].ToString()) == "Admin")
                            {
                                divRole.Visible = false;
                            }
                            else
                            {
                                divRole.Visible = true;
                            }

                            gvcompanydetailsave.Visible = true;
                            GridCompanyBind();
                        }
                        lblMastcompany.Text = "Select Company ";
                        lblfactoryName.Text = "Select Divison/Plant ";
                        chkrole.Attributes.Add("onclick", "radioMe(event);");
                    }
                    else
                    { Response.RedirectToRoute("login"); }
                }
                catch (Exception ex)
                {
                    string error = ex.ToString();
                    string Page = Request.Url.AbsolutePath.ToString();
                    Response.Redirect("Error?techerror=" + HttpUtility.UrlEncode(Enc.EncryptData(error)) + "&page=" +
                                      HttpUtility.UrlEncode(Enc.EncryptData(Page)));
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "ErrorMssgPopup('Session Expired,Please login again');window.location='Login'", true);
            }
        }
    }
    public void GridcompanyVisible()
    {
        if (Enc.DecryptData(Session["Type"].ToString()) == "Admin" || Enc.DecryptData(Session["Type"].ToString()) == "SuperAdmin")
        {
        }
        else
        {
            gvcompanydetailsave.Visible = true;
            GridCompanyBind();
            Intrested.Visible = false;
            MenuAlot.Visible = false;
            divRole.Visible = false;
        }
    }
    public void GridCompanyBind()
    {
        DataTable DtGrid = new DataTable();
        if (Enc.DecryptData(Request.QueryString["mu"].ToString()) == "Panel2")
        {
            DtGrid = Lo.RetriveAllCompany(ddlmaster.SelectedValue, "Division");
        }
        else if (Enc.DecryptData(Request.QueryString["mu"].ToString()) == "Panel3")
        {
            DtGrid = Lo.RetriveAllCompany(ddlfacotry.SelectedValue, "Unit");
        }
        else
        {
            DtGrid = Lo.RetriveAllCompany("", "Company");
        }
        if (DtGrid.Rows.Count > 0)
        {
            gvcompanydetailsave.DataSource = DtGrid;
            gvcompanydetailsave.DataBind();
            CompGrid();
        }
        else
        {
            gvcompanydetailsave.Visible = false;
        }
    }
    protected void CompGrid()
    {
        if (Enc.DecryptData(Request.QueryString["mu"].ToString()) == "Panel2")
        {
            gvcompanydetailsave.Columns[1].Visible = true;
            gvcompanydetailsave.Columns[2].Visible = false;
            gvcompanydetailsave.Columns[3].Visible = true;
            //this.gvcompanydetailsave.Columns[4].Visible = true;
            gvcompanydetailsave.Columns[5].Visible = false;
            // this.gvcompanydetailsave.Columns[6].Visible = false;
            gvcompanydetailsave.Columns[8].Visible = false;
            gvcompanydetailsave.Columns[9].Visible = true;
            gvcompanydetailsave.Columns[10].Visible = false;
        }
        else if (Enc.DecryptData(Request.QueryString["mu"].ToString()) == "Panel3")
        {
            gvcompanydetailsave.Columns[1].Visible = true;
            gvcompanydetailsave.Columns[2].Visible = false;
            gvcompanydetailsave.Columns[3].Visible = true;
            gvcompanydetailsave.Columns[4].Visible = false;
            gvcompanydetailsave.Columns[5].Visible = true;
            //this.gvcompanydetailsave.Columns[6].Visible = true;
            gvcompanydetailsave.Columns[8].Visible = false;
            gvcompanydetailsave.Columns[9].Visible = false;
            gvcompanydetailsave.Columns[10].Visible = true;
        }
        else
        {
            gvcompanydetailsave.Columns[3].Visible = false;
            gvcompanydetailsave.Columns[4].Visible = false;
            gvcompanydetailsave.Columns[5].Visible = false;
            gvcompanydetailsave.Columns[6].Visible = false;
            gvcompanydetailsave.Columns[8].Visible = true;
            gvcompanydetailsave.Columns[9].Visible = false;
            gvcompanydetailsave.Columns[10].Visible = false;
        }




    }
    protected void BindMasterCompany()
    {
        string sType = "", sName = "", sID = "", mSID = "";
        short id = 0;
        string sRole = "";
        if (Session["Type"] != null)
        {
            if (Enc.DecryptData(Session["Type"].ToString()) == "SuperAdmin" || Enc.DecryptData(Session["Type"].ToString()) == "Admin")
            {
                sType = "Select";
                mSID = "";
                id = 0;
                sRole = "SuperAdmin";

            }

            else if (Enc.DecryptData(Session["Type"].ToString()) == "Company")
            {

                sType = "CompanyName";
                mSID = Session["CompanyRefNo"].ToString();
                id = 2;
                sRole = "Company";
            }
            else if (Enc.DecryptData(Session["Type"].ToString()) == "Factory" || Enc.DecryptData(Session["Type"].ToString()) == "Division")
            {

                sType = "CompanyName";
                mSID = Session["CompanyRefNo"].ToString();
                id = 3;
                sRole = "Factory";
            }
            else if (Enc.DecryptData(Session["Type"].ToString()) == "Unit")
            {

                sType = "CompanyName";
                mSID = Session["CompanyRefNo"].ToString();
                id = 4;
                sRole = "Unit";
            }
            else
            {
                sType = "CompanyName";
                mSID = Session["CompanyRefNo"].ToString();
            }
            sName = "CompanyName";
            sID = "CompanyRefNo";
            mddlControl = ddlmaster;
            DataTable Dtchkintrestedarea = Lo.RetriveMasterData(id, mSID, sRole, 0, "", "", sType);
            if (Dtchkintrestedarea.Rows.Count > 0 && Dtchkintrestedarea != null)
            {
                Co.FillDropdownlist(mddlControl, Dtchkintrestedarea, sName, sID);
                if (Enc.DecryptData(Session["Type"].ToString()) == "SuperAdmin" || Enc.DecryptData(Session["Type"].ToString()) == "Admin")
                {
                    mddlControl.Items.Insert(0, "Select");
                    mddlControl.Enabled = true;
                }
                else
                {

                    mddlControl.Enabled = false;
                }
            }

        }
    }
    protected void BindMasterData()
    {
        DataTable Dtchkintrestedarea = Lo.RetriveMasterData(0, "", "", 0, "", "I", "IntrestedAreaCheck");
        if (Dtchkintrestedarea != null)
        {
            Co.FillCheckBox(chkintrestedarea, Dtchkintrestedarea, "InterestArea", "Id");
        }
        DataTable Dtchkmastermenuallot = Lo.RetriveMasterData(0, "", "", 0, "", "M", "IntrestedAreaCheck");
        if (Dtchkmastermenuallot != null)
        {
            Co.FillCheckBox(chkmastermenuallot, Dtchkmastermenuallot, "InterestArea", "Id");
        }
    }
    protected void Cleartext()
    {
        txtemail.Text = "";
        txtcomp.Text = "";
        Masterallowed = "";
        role = "";
        chkintrestedarea.ClearSelection();
        chkmastermenuallot.ClearSelection();
        chkrole.ClearSelection();
    }
    protected void SaveComp()
    {
        string StrSaveComp = "";
        HySave["CompanyID"] = id;
        HySave["CompanyName"] = Co.RSQandSQLInjection(txtcomp.Text.Trim(), "soft");
        HySave["CreatedBy"] = Enc.DecryptData(ViewState["UserLoginEmail"].ToString());
        HySave["ContactPersonEmailID"] = Co.RSQandSQLInjection(txtemail.Text.Trim(), "soft");
        if (btnsubmit.Text == "Save Division")
        {
            HySave["CompanyRefNo"] = ddlmaster.SelectedItem.Value;
            HySave["Role"] = "Factory";
            StrSaveComp = Lo.SaveMasterDivision(HySave, out _sysMsg, out _msg);
        }
        else if (btnsubmit.Text == "Save Unit")
        {
            HySave["CompanyRefNo"] = ddlfacotry.SelectedItem.Value;
            HySave["Role"] = "Unit";
            StrSaveComp = Lo.SaveMasterUnit(HySave, out _sysMsg, out _msg);
        }
        else
        {
            foreach (ListItem li in chkintrestedarea.Items)
            {
                if (li.Selected == true)
                {
                    intrestedare = intrestedare + "," + li.Value;
                }
            }
            HySave["InterestedArea"] = Co.RSQandSQLInjection(intrestedare.Substring(1).ToString(), "soft");
            foreach (ListItem chkmasallow in chkmastermenuallot.Items)
            {
                if (chkmasallow.Selected == true)
                {
                    Masterallowed = Masterallowed + "," + chkmasallow.Value;
                }
            }
            HySave["MasterAllowed"] = Co.RSQandSQLInjection(Masterallowed.Substring(1).ToString(), "soft");
            foreach (ListItem chkro in chkrole.Items)
            {
                if (chkro.Selected == true)
                {
                    role = role + "," + chkro.Value;
                }
            }
            if (Enc.DecryptData(Session["Type"].ToString()) == "Admin")
            {
                HySave["Role"] = "Company";
            }
            else
            {
                HySave["Role"] = Co.RSQandSQLInjection(role.Substring(1).ToString(), "soft");
            }

            StrSaveComp = Lo.SaveMasterComp(HySave, out _sysMsg, out _msg);
        }
        if (StrSaveComp != "")
        {

            gvcompanydetailsave.Visible = true;
            GridCompanyBind();
            if (btnsubmit.Text == "Save Division")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "SuccessfullPop('Division added successfully , to fill all details of company,please click on view >> company >> division >> edit and fill other company details')", true);
            }
            else if (btnsubmit.Text == "Save Unit")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "SuccessfullPop('Unit added successfully, to fill all details of company,please click on view >> company >> unit >> edit and fill other unit details')", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "SuccessfullPop('Company added successfully, to fill all details of company,please click on view >> company >> edit and fill other division details')", true);
            }
            Cleartext();
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('Record not saved.')", true);
        }
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        Cleartext();
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtcomp.Text.Trim() != "")
            {
                string strIsEmail = Lo.VerifyEmailandCompany(txtemail.Text, txtcomp.Text, out _msg);
                if (_msg != "0" && _msg != "")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert",
                        "ErrorMssgPopup('" + txtcomp.Text + " name already exist !')", true);
                }
                else
                {
                    if (btnsubmit.Text == "Save Division")
                    {
                        if (ddlmaster.SelectedItem.Value != "Select")
                        {
                            SaveComp();
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert",
                                "ErrorMssgPopup('Select Company !')", true);
                        }
                    }
                    else if (btnsubmit.Text == "Save Unit")
                    {
                        if (ddlmaster.SelectedItem.Value != "Select" && ddlfacotry.SelectedItem.Value != "Select")
                        {
                            SaveComp();
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert",
                                "ErrorMssgPopup('Select company and division !')", true);
                        }
                    }

                    else
                    {
                        SaveComp();
                    }
                }

            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert",
                    "ErrorMssgPopup('" + lblName.Text + " can not be empty !')", true);
            }
        }
        catch (Exception ex)
        {
            string error = ex.ToString();
            string Page = Request.Url.AbsolutePath.ToString();
            Response.Redirect("Error?techerror=" + HttpUtility.UrlEncode(Enc.EncryptData(error)) + "&page=" + HttpUtility.UrlEncode(Enc.EncryptData(Page)));
        }
    }
    protected void ddlmaster_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable DtDDL = Lo.RetriveMasterData(0, ddlmaster.SelectedItem.Value, "Factory1", 0, "", "M", "CompanyName");
        if (DtDDL.Rows.Count > 0)
        {
            Co.FillDropdownlist(ddlfacotry, DtDDL, "FactoryName", "FactoryRefNo");
        }

        DataTable DtBindSubFactory = new DataTable();
        if (Enc.DecryptData(Session["Type"].ToString()) == "Factory")
        {
            ddlfacotry.Enabled = false;
            DtBindSubFactory = Lo.RetriveMasterData(0, Session["CompanyRefNo"].ToString(), "", 0, "", "", "FactoryJoin");
        }
        else
        {
            ddlfacotry.Enabled = true;
            DtBindSubFactory = Lo.RetriveMasterData(0, ddlmaster.SelectedItem.Value, "", 0, "", "M", "FactoryJoin1");
        }
        if (DtBindSubFactory.Rows.Count > 0)
        {
            if (DtBindSubFactory.Rows[0]["FactoryName"].ToString() != "")
            {
                //  Co.FillDropdownlist(ddlfacotry, DtBindSubFactory, "FactoryName", "FactoryRefNo");
                if (Enc.DecryptData(Request.QueryString["mu"].ToString()) == "Panel3")
                {
                    gvcompanydetailsave.Visible = false;
                    if (ddlfacotry.Enabled == true)
                    {
                        ddlfacotry.Items.Insert(0, "Select");
                    }
                    else
                    {
                        gvcompanydetailsave.Visible = true;
                        GridCompanyBind();
                    }
                }
                else
                {
                    gvcompanydetailsave.Visible = true;
                    GridCompanyBind();
                    ddlfacotry.Items.Insert(0, "Select");
                }

            }
            else
            {
                gvcompanydetailsave.Visible = false;
            }
        }
        else
        {
            gvcompanydetailsave.Visible = false;

        }
    }
    protected void ddlfacotry_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvcompanydetailsave.Visible = true;
        GridCompanyBind();
    }
    protected void gvcompanydetailsave_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "viewComp")
        {
            DataTable DtIntrested = Lo.RetriveIntresteData(e.CommandArgument.ToString());
            if (DtIntrested.Rows.Count > 0)
            {
                lblintrestedin.Text = DtIntrested.Rows[0][0].ToString();
                lblmenuallot.Text = DtIntrested.Rows[0][1].ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "changePass", "showPopup();", true);
            }
        }
    }
    #region ReturnUrl Long"
    public string Resturl(int length)
    {
        const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
        StringBuilder res = new StringBuilder();
        Random rnd = new Random();
        while (0 < length--)
        {
            res.Append(valid[rnd.Next(valid.Length)]);
        }
        return res.ToString();
    }
    #endregion
    protected void gvcompanydetailsave_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[10].Text == "Factory")
            {
                e.Row.Cells[10].Text = "Division";
            }
            if (ddlmaster.Visible == true)
            {
                gvcompanydetailsave.Columns[13].Visible = false;
            }
            if (ddlfacotry.Visible == true && ddlmaster.Visible == true)
            {
                gvcompanydetailsave.Columns[13].Visible = false;
            }
        }
    }
    protected void gvcompanydetailsave_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.TableSection = TableRowSection.TableBody;
        }
        else if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.TableSection = TableRowSection.TableHeader;
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.TableSection = TableRowSection.TableFooter;
        }
    }
}