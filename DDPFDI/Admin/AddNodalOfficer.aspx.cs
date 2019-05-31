using BusinessLayer;
using Encryption;
using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Text;
using System.Web.UI;
using System.IO;
using System.Web.UI.WebControls;
using System.Web;

public partial class Admin_AddNodalOfficer : System.Web.UI.Page
{
    Cryptography objEnc = new Cryptography();
    DataUtility Co = new DataUtility();
    Logic Lo = new Logic();
    private string DisplayPanel = "";
    private string SessionID = "";
    private string _sysMsg = string.Empty;
    private string _msg = string.Empty;
    DataTable DtGrid = new DataTable();
    DataTable DtCompanyDDL = new DataTable();
    HybridDictionary hySaveNodal = new HybridDictionary();
    string UserName;
    string RefNo;
    string UserEmail;
    string currentPage = "";
    private string mRefNo = "";
    private Int16 Mid = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["Type"] != null)
            {
                if (Request.QueryString["id"] != null)
                {
                    string strid = Request.QueryString["id"].ToString().Replace(" ", "+");
                    string strPageName = objEnc.DecryptData(strid);
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
                    hidType.Value = objEnc.DecryptData(Session["Type"].ToString());
                    mRefNo = Session["CompanyRefNo"].ToString();
                    hidCompanyRefNo.Value = mRefNo.ToString();
                    ViewState["UserLoginEmail"] = objEnc.DecryptData(Session["User"].ToString());
                }
                if (Request.QueryString["mcurrentcompRefNo"] != null)
                {
                    EditCode();
                }
                else
                {
                    BindCompany();
                    BindMasterDesignation("");
                }
            }
        }
    }
    #region Load
    public void GridViewNodalOfficerBind(string mRefNo, string mRole)
    {
        DataTable DtGrid = Lo.RetriveAllNodalOfficer(mRefNo, mRole);
        if (DtGrid.Rows.Count > 0)
        {
            gvViewNodalOfficer.DataSource = DtGrid;
            gvViewNodalOfficer.DataBind();
            gvViewNodalOfficer.Visible = true;
        }
        else
        {
            gvViewNodalOfficer.Visible = false;
        }
        DataRow[] foundRows = DtGrid.Select("IsNodalOfficer='Y'");
        if (foundRows.Length != 0)
        {
            chkrole.Items[0].Enabled = false;
        }
        else
        {
            chkrole.Items[0].Enabled = true;
        }
    }
    protected void BindCompany()
    {
        if (hidType.Value == "SuperAdmin" || hidType.Value == "Admin")
        {
            DtCompanyDDL = Lo.RetriveMasterData(0, "", "", 0, "", "", "Select");
            if (DtCompanyDDL.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddlcompany, DtCompanyDDL, "CompanyName", "CompanyRefNo");
                ddlcompany.Items.Insert(0, "Select");
                ddlcompany.Enabled = true;
            }
            else
            {
                ddlcompany.Enabled = false;
            }
            lblselectdivison.Visible = false;
            lblselectunit.Visible = false;
        }
        else if (hidType.Value == "Company")
        {
            DtCompanyDDL = Lo.RetriveMasterData(0, mRefNo, "Company", 0, "", "", "CompanyName");
            if (DtCompanyDDL.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddlcompany, DtCompanyDDL, "CompanyName", "CompanyRefNo");
                GridViewNodalOfficerBind(mRefNo, "Company");
                ddlcompany.Enabled = false;
            }
            else
            {
                ddlcompany.Enabled = false;
            }
            DtCompanyDDL = Lo.RetriveMasterData(0, ddlcompany.SelectedItem.Value, "Factory1", 0, "", "", "CompanyName");
            if (DtCompanyDDL.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddldivision, DtCompanyDDL, "FactoryName", "FactoryRefNo");
                ddldivision.Items.Insert(0, "Select");
                if (hidType.Value == "Company")
                {
                    lblselectdivison.Visible = true;
                    ddldivision.Enabled = true;
                    ddlunit.Visible = false;
                    lblselectunit.Visible = false;

                }
                else
                {
                    ddldivision.Enabled = false;
                }
            }
            else
            {
                lblselectdivison.Visible = false;
                lblselectunit.Visible = false;
                ddldivision.Items.Insert(0, "Select");
                ddlunit.Items.Insert(0, "Select");
            }
        }
        else if (hidType.Value == "Factory" || hidType.Value == "Division")
        {
            DtCompanyDDL = Lo.RetriveMasterData(0, mRefNo, "Company1", 0, "", "", "CompanyName");
            if (DtCompanyDDL.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddlcompany, DtCompanyDDL, "CompanyName", "CompanyRefNo");
                ddlcompany.Enabled = false;
            }
            else
            {
                ddlcompany.Enabled = false;
            }
            DtCompanyDDL = Lo.RetriveMasterData(0, ddlcompany.SelectedItem.Value, "Factory1", 0, "", "", "CompanyName");
            if (DtCompanyDDL.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddldivision, DtCompanyDDL, "FactoryName", "FactoryRefNo");
                lblselectdivison.Visible = true;
                ddldivision.Enabled = false;
                GridViewNodalOfficerBind(ddldivision.SelectedItem.Value, "Division");
            }
            else
            {
                lblselectdivison.Visible = false;
            }
            DtCompanyDDL = Lo.RetriveMasterData(0, ddldivision.SelectedItem.Value, "Unit1", 0, "", "", "CompanyName");
            if (DtCompanyDDL.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddlunit, DtCompanyDDL, "UnitName", "UnitRefNo");
                ddlunit.Items.Insert(0, "Select");
                ddlunit.Enabled = true;
                lblselectunit.Visible = true;
            }
            else
            {
                lblselectunit.Visible = false;
            }
        }
        else if (hidType.Value == "Unit")
        {
            DtCompanyDDL = Lo.RetriveMasterData(0, mRefNo, "Company2", 0, "", "", "CompanyName");
            if (DtCompanyDDL.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddlcompany, DtCompanyDDL, "CompanyName", "CompanyRefNo");
                ddlcompany.Enabled = false;
            }
            else
            {
                ddlcompany.Enabled = false;
            }
            DtCompanyDDL = Lo.RetriveMasterData(0, ddlcompany.SelectedItem.Value, "Factory1", 0, "", "", "CompanyName");
            if (DtCompanyDDL.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddldivision, DtCompanyDDL, "FactoryName", "FactoryRefNo");
                // code by gk to select indivisual division for the particular unit
                DataTable dt = Lo.RetriveMasterData(0, mRefNo, "Factory3", 0, "", "", "CompanyName");
                if (dt.Rows.Count > 0)
                    ddldivision.SelectedValue = dt.Rows[0]["FactoryRefNo"].ToString();
                //end code
                lblselectdivison.Visible = true;
                ddldivision.Enabled = false;
            }
            else
            {
                ddldivision.Enabled = false;
            }
            DtCompanyDDL = Lo.RetriveMasterData(0, ddldivision.SelectedItem.Value, "Unit1", 0, "", "", "CompanyName");
            if (DtCompanyDDL.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddlunit, DtCompanyDDL, "UnitName", "UnitRefNo");
                // code by gk to select indivisual unit for the particular unit     
                ddlunit.SelectedValue = mRefNo.ToString();
                //end code
                lblselectunit.Visible = true;
                ddlunit.Enabled = false;
                GridViewNodalOfficerBind(ddlunit.SelectedItem.Value, "Unit");
            }
            else
            {
                ddlunit.Enabled = false;
            }
        }
    }
    protected void BindCompany(string mType)
    {
        if (mType == "SuperAdmin" || mType == "Admin")
        {
            DtCompanyDDL = Lo.RetriveMasterData(0, "", "", 0, "", "", "Select");
            if (DtCompanyDDL.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddlcompany, DtCompanyDDL, "CompanyName", "CompanyRefNo");
                ddlcompany.Items.Insert(0, "Select");
                ddlcompany.Enabled = true;
            }
            else
            {
                ddlcompany.Enabled = false;
            }
            lblselectdivison.Visible = false;
            lblselectunit.Visible = false;
        }

        else if (mType == "Company")
        {
            DtCompanyDDL = Lo.RetriveMasterData(0, mRefNo, "Company", 0, "", "", "CompanyName");
            if (DtCompanyDDL.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddlcompany, DtCompanyDDL, "CompanyName", "CompanyRefNo");
                ddlcompany.Enabled = false;
                if (Request.QueryString["mcurrentcompRefNo"] != null)
                {
                    BindMasterDesignation("");
                }
            }
            else
            {
                ddlcompany.Enabled = false;
            }
            DtCompanyDDL = Lo.RetriveMasterData(0, ddlcompany.SelectedItem.Value, "Factory1", 0, "", "", "CompanyName");
            if (DtCompanyDDL.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddldivision, DtCompanyDDL, "FactoryName", "FactoryRefNo");
                ddldivision.Items.Insert(0, "Select");
                if (mType == "Company")
                {
                    lblselectdivison.Visible = true;
                    ddldivision.Enabled = true;
                    ddlunit.Visible = false;
                    lblselectunit.Visible = false;

                }
                else
                {
                    ddldivision.Enabled = false;
                }
            }
            else
            {
                ddldivision.Enabled = false;
            }
        }
        else if (mType == "Factory" || mType=="Division")
        {
            DtCompanyDDL = Lo.RetriveMasterData(0, mRefNo, "Company1", 0, "", "", "CompanyName");
            if (DtCompanyDDL.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddlcompany, DtCompanyDDL, "CompanyName", "CompanyRefNo");
                ddlcompany.Enabled = false;
                if (Request.QueryString["mcurrentcompRefNo"] != null)
                {
                    BindMasterDesignation("");
                }
            }
            else
            {
                ddlcompany.Enabled = false;
            }
            DtCompanyDDL = Lo.RetriveMasterData(0, ddlcompany.SelectedItem.Value, "Factory1", 0, "", "", "CompanyName");
            if (DtCompanyDDL.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddldivision, DtCompanyDDL, "FactoryName", "FactoryRefNo");
                // code by gk to select indivisual division for the particular unit
                DataTable dt = Lo.RetriveMasterData(0, mRefNo, "Factory3", 0, "", "", "CompanyName");
                if (dt.Rows.Count > 0)
                    ddldivision.SelectedValue = dt.Rows[0]["FactoryRefNo"].ToString();
                //end code
                lblselectdivison.Visible = true;
                ddldivision.Enabled = false;
            }
            else
            {
                lblselectdivison.Visible = false;
            }
            DtCompanyDDL = Lo.RetriveMasterData(0, ddldivision.SelectedItem.Value, "Unit1", 0, "", "", "CompanyName");
            if (DtCompanyDDL.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddlunit, DtCompanyDDL, "UnitName", "UnitRefNo");
                ddlunit.Items.Insert(0, "Select");
                ddlunit.Enabled = true;
                lblselectunit.Visible = true;
            }
            else
            {
                lblselectunit.Visible = false;
            }
        }
        else if (mType == "Unit")
        {
            DtCompanyDDL = Lo.RetriveMasterData(0, mRefNo, "Company2", 0, "", "", "CompanyName");
            if (DtCompanyDDL.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddlcompany, DtCompanyDDL, "CompanyName", "CompanyRefNo");
                ddlcompany.Enabled = false;
                if (Request.QueryString["mcurrentcompRefNo"] != null)
                {
                    BindMasterDesignation("");
                }
            }
            else
            {
                ddlcompany.Enabled = false;
            }
            DtCompanyDDL = Lo.RetriveMasterData(0, ddlcompany.SelectedItem.Value, "Factory1", 0, "", "", "CompanyName");
            if (DtCompanyDDL.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddldivision, DtCompanyDDL, "FactoryName", "FactoryRefNo");
                // code by gk to select indivisual division for the particular unit
                DataTable dt = Lo.RetriveMasterData(0, mRefNo, "Factory3", 0, "", "", "CompanyName");
                if (dt.Rows.Count > 0)
                    ddldivision.SelectedValue = dt.Rows[0]["FactoryRefNo"].ToString();
                //end code
                lblselectdivison.Visible = true;
                ddldivision.Enabled = false;
            }
            else
            {
                lblselectdivison.Visible = false;
            }
            DtCompanyDDL = Lo.RetriveMasterData(0, ddldivision.SelectedItem.Value, "Unit1", 0, "", "", "CompanyName");
            if (DtCompanyDDL.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddlunit, DtCompanyDDL, "UnitName", "UnitRefNo");
                // code by gk to select indivisual unit for the particular unit     
                ddlunit.SelectedValue = mRefNo.ToString();
                //end code
                lblselectunit.Visible = true;
                ddlunit.Enabled = false;
            }
            else
            {
                lblselectunit.Visible = false;
            }
        }
    }
    #endregion
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
    #region DropDownList Code
    protected void ddlcompany_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlcompany.SelectedItem.Text != "Select")
        {
            DtCompanyDDL = Lo.RetriveMasterData(0, ddlcompany.SelectedItem.Value, "", 0, "", "", "FactoryCompanyID");
            if (DtCompanyDDL.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddldivision, DtCompanyDDL, "FactoryName", "FactoryRefNo");
                ddldivision.Items.Insert(0, "Select");
                lblselectdivison.Visible = true;
                ddldivision.Visible = true;
                hidCompanyRefNo.Value = ddlcompany.SelectedItem.Value;
                hidType.Value = "Company";
                GridViewNodalOfficerBind(ddlcompany.SelectedItem.Value, "Company");
            }
            else
            {
                ddldivision.Visible = false;
                lblselectdivison.Visible = false;
                ddldivision.Items.Insert(0, "Select");
                ddlunit.Items.Insert(0, "Select");

            }
        }
        else if (ddlcompany.SelectedItem.Text == "Select")
        {
            lblselectdivison.Visible = false;
            lblselectunit.Visible = false;
        }
        BindMasterDesignation("");
    }
    protected void ddldivision_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddldivision.SelectedItem.Text != "Select")
        {
            DtCompanyDDL = Lo.RetriveMasterData(0, ddldivision.SelectedItem.Value, "", 0, "", "", "UnitSelectID");
            if (DtCompanyDDL.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddlunit, DtCompanyDDL, "UnitName", "UnitRefNo");
                ddlunit.Items.Insert(0, "Select");
                ddlunit.Visible = true;
                lblselectunit.Visible = true;
                hidCompanyRefNo.Value = ddldivision.SelectedItem.Value;
                hidType.Value = "Division";
                GridViewNodalOfficerBind(ddldivision.SelectedItem.Value, "Division");
            }
            else
            {
                lblselectunit.Visible = false;
                ddlunit.Visible = false;
                GridViewNodalOfficerBind(ddldivision.SelectedItem.Value, "Division");
            }
        }
        else if (ddldivision.SelectedItem.Text == "Select")
        {
            DtCompanyDDL = Lo.RetriveMasterData(0, ddlcompany.SelectedItem.Value, "", 0, "", "", "FactoryCompanyID");
            if (DtCompanyDDL.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddldivision, DtCompanyDDL, "FactoryName", "FactoryRefNo");
                ddldivision.Items.Insert(0, "Select");
                lblselectdivison.Visible = true;
                ddldivision.Visible = true;
                lblselectdivison.Visible = false;
                GridViewNodalOfficerBind(ddlcompany.SelectedItem.Value, "Division");
            }
        }
        BindMasterDesignation("");
    }
    protected void ddlunit_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlunit.SelectedItem.Text == "Select")
        {
            //ddldivision_OnSelectedIndexChanged(sender, e);
            gvViewNodalOfficer.Visible = false;
        }
        else
        {
            hidCompanyRefNo.Value = ddlunit.SelectedItem.Value;
            hidType.Value = "Unit";
            BindMasterDesignation("");
            GridViewNodalOfficerBind(ddlunit.SelectedItem.Value, "Unit");
        }
    }
    #endregion
    #region For Department or Designation
    protected void BindMasterDesignation(string mCompanyRefNo)
    {
        ddldesignation.Items.Insert(0, "Select");
        DataTable DtMasterCategroy;
        if (mCompanyRefNo != "")
        {
            DtMasterCategroy = Lo.RetriveMasterData(0, mCompanyRefNo, "", 0, "", "", "ViewDesignation");
        }
        else
        {
            DtMasterCategroy = Lo.RetriveMasterData(0, ddlcompany.SelectedItem.Value, "", 0, "", "", "ViewDesignation");
        }
        if (DtMasterCategroy.Rows.Count > 0)
        {
            Co.FillDropdownlist(ddldesignation, DtMasterCategroy, "Designation", "DesignationId");
            if (Request.QueryString["mcurrentcompRefNo"] != null)
            {
                // ddldesignation.Items.Insert(0, "Select");
            }
            else
            { ddldesignation.Items.Insert(0, "Select"); }
        }
    }
    #endregion
    #region Save code
    protected void SaveNodal()
    {
        if (Request.QueryString["mcurrentcompRefNo"] != null)
        {
            hySaveNodal["NodalOfficerID"] = objEnc.DecryptData(Request.QueryString["mcurrentcompRefNo"].ToString());

        }
        else
        {
            hySaveNodal["NodalOfficerID"] = Mid;
        }

        hySaveNodal["NodalOfficerRefNo"] = "";
        hySaveNodal["NodalEmpCode"] = Co.RSQandSQLInjection(txtEmpCode.Text, "soft");
        hySaveNodal["NodalOficerName"] = Co.RSQandSQLInjection(txtname.Text, "soft");
        hySaveNodal["NodalOfficerDepartment"] = 0;
        hySaveNodal["NodalOfficerDesignation"] = Co.RSQandSQLInjection(ddldesignation.SelectedItem.Value, "soft");
        hySaveNodal["NodalOfficerEmail"] = Co.RSQandSQLInjection(txtemailid.Text, "soft");
        hySaveNodal["NodalOfficerMobile"] = Co.RSQandSQLInjection(txtmobile.Text, "soft");
        hySaveNodal["NodalOfficerTelephone"] = Co.RSQandSQLInjection(txttelephone.Text, "soft");
        hySaveNodal["NodalOfficerFax"] = Co.RSQandSQLInjection(txtfax.Text, "soft");
        if (ddlcompany.SelectedValue != "Select" && ddldivision.SelectedValue == "Select")
        {
            hySaveNodal["CompanyRefNo"] = ddlcompany.SelectedItem.Value;
            RefNo = ddlcompany.SelectedItem.Value;
            hySaveNodal["Type"] = "Company";
        }
        else if (ddlcompany.SelectedValue != "Select" && ddldivision.SelectedValue != "Select" && ddlunit.SelectedValue == "" || ddlunit.SelectedItem.Value == "Select")
        {
            hySaveNodal["CompanyRefNo"] = ddldivision.SelectedItem.Value;
            RefNo = ddldivision.SelectedItem.Value;
            hySaveNodal["Type"] = "Division";
        }
        else if (ddlcompany.SelectedValue != "Select" && ddldivision.SelectedValue != "Select" && ddlunit.SelectedValue != "Select")
        {
            hySaveNodal["CompanyRefNo"] = ddlunit.SelectedItem.Value;
            RefNo = ddlunit.SelectedItem.Value;
            hySaveNodal["Type"] = "Unit";
        }
        if (chkrole.Items[0].Selected == true)
        {
            hySaveNodal["IsNodalOfficer"] = "Y";
        }
        else
        {
            hySaveNodal["IsNodalOfficer"] = "N";
        }
        if (chkrole.Items[1].Selected == true)
        {
            hySaveNodal["IsLoginActive"] = "Y";
        }
        else
        {
            hySaveNodal["IsLoginActive"] = "N";
        }
        hySaveNodal["CreatedBy"] = ViewState["UserLoginEmail"].ToString();
        string Str = Lo.SaveMasterNodal(hySaveNodal, out _sysMsg, out _msg);
        if (Str != "" && Str != null)
        {
            if (ddlcompany.SelectedValue != "Select" && ddldivision.SelectedValue == "Select")
            {
                GridViewNodalOfficerBind(hidCompanyRefNo.Value, "Company");
            }
            else if (ddlcompany.SelectedValue != "Select" && ddldivision.SelectedValue != "Select" && ddlunit.SelectedValue == "Select")
            {
                GridViewNodalOfficerBind(hidCompanyRefNo.Value, "Division");
            }
            else if (ddlcompany.SelectedValue != "Select" && ddldivision.SelectedValue != "Select" && ddlunit.SelectedValue != "Select")
            {
                GridViewNodalOfficerBind(hidCompanyRefNo.Value, "Unit");
            }
            if (chkrole.Items[0].Selected == true)
            {
                SendEmailCode(Str);
            }
            else
            {
                if (chkrole.Items[1].Selected == true)
                {
                    SendEmailCode(Str);
                }
                else
                { }
            }
            //GridViewNodalOfficerBind(mRefNo, hidType.Value);
            Cleartext();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record saved successsfully')", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record not saved successsfully')", true);
        }
    }
    #endregion
    #region Send Mail
    public void SendEmailCode(string empid)
    {
        try
        {
            string body;
            using (StreamReader reader = new StreamReader(Server.MapPath("~/emailPage/GeneratePassword.html")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{UserName}", txtemailid.Text);
            body = body.Replace("{refno}", HttpUtility.UrlEncode(objEnc.EncryptData(empid)));
            body = body.Replace("{mcurid}", Resturl(56));
            SendMail s;
            s = new SendMail();
            s.CreateMail("aeroindia-ddp@gov.in", txtemailid.Text, "Create Password Email", body);
            s.sendMail();
            // ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Create password email send successfully.')", true);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);
        }

    }
    #endregion
    protected void btnsub_Click(object sender, EventArgs e)
    {
        if (txtemailid.Text == "" && txtname.Text == "" && txtemailid.Text != "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Email id and name can not be empty !')", true);
        }
        else
        {
            if (ddlcompany.SelectedItem.Value != "Select")
            {
                if (chkrole.Items[0].Selected == true && chkrole.Items[1].Selected == true)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('check only nodel or user any one.')", true);
                }
                else
                {
                    if (Request.QueryString["mcurrentcompRefNo"] == null)
                    {
                        if (ddldesignation.SelectedItem.Value != "Select")
                        {
                            DataTable dtNodalOfficerEmail = Lo.RetriveMasterData(0, txtemailid.Text, "", 0, "", "", "ValidEmail");
                            if (dtNodalOfficerEmail.Rows.Count > 0)
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Email id already exists !')", true);
                            }
                            else
                            {
                                SaveNodal();
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Select designation !')", true);
                        }
                    }
                    else
                    {
                        SaveNodal();
                    }
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Select company !')", true);
            }
        }
    }
    protected void Cleartext()
    {
        txtfax.Text = "";
        txtemailid.Text = "";
        txtmobile.Text = "";
        txtname.Text = "";
        txttelephone.Text = "";
        ddldesignation.SelectedIndex = 0;
        btnsub.Text = "Save";
        txtEmpCode.Text = "";
        if (chkrole.Items[0].Enabled == true)
        {
            chkrole.Items[0].Selected = false;
        }
        else if (chkrole.Items[0].Enabled == false)
        {
            chkrole.Items[0].Selected = false;
            chkrole.Items[0].Enabled = false;
        }
        else if (chkrole.Items[1].Selected == true)
        {
            chkrole.Items[1].Selected = false;
        }
        else if (chkrole.Items[0].Selected == true)
        {
            chkrole.Items[0].Selected = false;
        }
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        Cleartext();
    }
    protected void EditCode()
    {
        if (Session["CompanyRefNo"] != null)
        {
            DataTable DtView = Lo.RetriveMasterData(Convert.ToInt16(objEnc.DecryptData(Request.QueryString["mcurrentcompRefNo"].ToString().Trim())), "", "", 0, "", "", "SearchNodalOfficerID");
            if (DtView.Rows.Count > 0)
            {
                mRefNo = DtView.Rows[0]["CompanyRefNo"].ToString();
                txtname.Text = DtView.Rows[0]["NodalOficerName"].ToString();
                BindMasterDesignation(DtView.Rows[0]["CompanyRefNo"].ToString());
                ddldesignation.SelectedValue = DtView.Rows[0]["NodalOfficerDesignation"].ToString();
                txtEmpCode.Text = DtView.Rows[0]["NodalEmpCode"].ToString();
                txtemailid.Text = DtView.Rows[0]["NodalOfficerEmail"].ToString();
                txtmobile.Text = DtView.Rows[0]["NodalOfficerMobile"].ToString();
                txttelephone.Text = DtView.Rows[0]["NodalOfficerTelephone"].ToString();
                txtfax.Text = DtView.Rows[0]["NodalOfficerFax"].ToString();
                if (DtView.Rows[0]["IsLoginActive"].ToString() == "Y")
                {
                    DataTable DtCheckNodelorUnActive = Lo.RetriveMasterData(0, mRefNo, "", 0, "", "", "CheckNodalStatus");
                    if (DtCheckNodelorUnActive.Rows.Count > 0)
                    {
                        chkrole.Items[0].Enabled = false;
                    }
                    chkrole.Items[1].Selected = true;
                }
                else if (DtView.Rows[0]["IsNodalOfficer"].ToString() == "Y")
                {
                    chkrole.Items[0].Selected = true;
                }
                if (DtView.Rows[0]["Type"].ToString() == "Company")
                {
                    BindCompany("Company");
                }
                else if (DtView.Rows[0]["Type"].ToString() == "Division")
                {
                    BindCompany("Factory");
                }
                else if (DtView.Rows[0]["Type"].ToString() == "Unit")
                {
                    BindCompany("Unit");
                }
            }
        }
    }
    protected void gvViewNodalOfficer_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label s_lblnodalofficer = (Label)e.Row.FindControl("lblnodalofficer");
            Label s_lblnodallogactive = (Label)e.Row.FindControl("lblnodallogactive");
            LinkButton s_lbllogindetail = (LinkButton)e.Row.FindControl("lbllogindetail");
            if (s_lblnodalofficer.Text == "Y")
            {
                e.Row.Attributes.Add("Class", "bg-purple");
                s_lblnodalofficer.Text = "Nodal Officer";
                s_lblnodalofficer.Visible = true;
                // s_lbllogindetail.Visible = false;
            }
            else if (s_lblnodallogactive.Text == "Y")
            {
                s_lblnodallogactive.Text = "User";
                s_lblnodallogactive.Visible = true;
                //  s_lbllogindetail.Visible = false;
            }
            else if (s_lblnodallogactive.Text == "N" && s_lblnodalofficer.Text == "N")
            {
                s_lblnodalofficer.Text = "Employee";
                s_lblnodalofficer.Visible = true;
                //  s_lbllogindetail.Visible = true;
            }
        }
    }
}