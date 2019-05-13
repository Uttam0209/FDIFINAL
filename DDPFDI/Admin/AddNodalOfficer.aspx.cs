﻿using BusinessLayer;
using Encryption;
using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Text;
using System.Web.UI;
using System.IO;

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
    private string mType = "";
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
                    mType = objEnc.DecryptData(Session["Type"].ToString());
                    mRefNo = Session["CompanyRefNo"].ToString();


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
            DivNodalRole.Visible = false;
        }
        else
        {
            DivNodalRole.Visible = true;
        }
    }

    protected void BindCompany()
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
        else if (mType == "Factory")
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
                ddldivision.Enabled = false;
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
                ddlunit.Visible = false;
            }
        }
        else if (mType == "Unit")
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
        else if (mType == "Factory")
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
            }
            else
            {
                ddldivision.Enabled = false;
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
                ddlunit.Visible = false;
            }
        }
        else if (mType == "Unit")
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
                lblselectunit.Visible = true;
                ddlunit.Enabled = false;
            }
            else
            {
                ddlunit.Enabled = false;
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
        if (ddldivision.SelectedItem.Text == "Select")
        {
            ddldivision_OnSelectedIndexChanged(sender, e);
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
            ddldesignation.Items.Insert(0, "Select");
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
        }
        else if (ddlcompany.SelectedValue != "Select" && ddldivision.SelectedValue != "Select" && ddlunit.SelectedValue == "Select")
        {
            hySaveNodal["CompanyRefNo"] = ddldivision.SelectedItem.Value;
            RefNo = ddldivision.SelectedItem.Value;
        }
        else if (ddlcompany.SelectedValue != "Select" && ddldivision.SelectedValue != "Select" && ddlunit.SelectedValue != "Select")
        {
            hySaveNodal["CompanyRefNo"] = ddlunit.SelectedItem.Value;
            RefNo = ddlunit.SelectedItem.Value;
        }
        hySaveNodal["Type"] = hidType.Value;
        if (chkrole.Checked)
        {
            hySaveNodal["IsNodalOfficer"] = "Y";
        }
        else
        {
            hySaveNodal["IsNodalOfficer"] = "N";
        }

        string Str = Lo.SaveMasterNodal(hySaveNodal, out _sysMsg, out _msg);
        if (Str == "Save")
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
            if (chkrole.Checked)
            {
                SendEmailCode();
            }
            else
            {
            }
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
    public void SendEmailCode()
    {
        try
        {
            string body;
            using (StreamReader reader = new StreamReader(Server.MapPath("~/emailPage/GeneratePassword.html")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{UserName}", txtemailid.Text);
            body = body.Replace("{refno}", objEnc.EncryptData(RefNo));
            body = body.Replace("{mcurid}", Resturl(56));
            SendMail s;
            s = new SendMail();
            s.CreateMail("aeroindia-ddp@gov.in", txtemailid.Text, "Create Password", body);
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
        if (txtemailid.Text == "" && txtname.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Email id and name can not be empty !')", true);
        }
        else
        {
            if (ddlcompany.SelectedItem.Value != "Select")
            {

                if (btnsub.Text != "Edit")
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
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        Cleartext();
    }
    protected void EditCode()
    {
        if (Session["CompanyRefNo"] != null)
        {
            DataTable DtView = Lo.RetriveMasterData(Convert.ToInt16(objEnc.DecryptData(Request.QueryString["mcurrentcompRefNo"].ToString())), "", "", 0, "", "", "SearchNodalOfficerID");
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
                btnsub.Text = "Edit";
            }
        }
    }
}