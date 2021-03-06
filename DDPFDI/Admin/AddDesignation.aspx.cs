﻿using System;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Text;
using BusinessLayer;
using Encryption;
using System.Collections.Specialized;
using System.Web.UI.WebControls;
using System.Web.Helpers;
using System.Net.Http;

public partial class Admin_AddDesignation : System.Web.UI.Page
{
    Logic Lo = new Logic();
    Cryptography Enc = new Cryptography();
    DataUtility Co = new DataUtility();
    private Int64 id = 0;
    private string _sysMsg = string.Empty;
    private string _msg = string.Empty;
    private string mType = "";
    private string mRefNo = "";
    HybridDictionary HySave = new HybridDictionary();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["User"] != null)
        {
            if (!IsPostBack)
            {
                try
                {
                    if (Request.QueryString["id"] != null)
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
                        BindMasterCompany();
                    }
                    ViewState["UserLoginEmail"] = Session["User"].ToString();
                    mType = Enc.DecryptData(Session["Type"].ToString());
                    mRefNo = Session["CompanyRefNo"].ToString();
                    if (Request.QueryString["mcurrentcompRefNo"] != null)
                    {
                        EditCode();
                    }
                }
                catch (Exception ex)
                {
                    string error = ex.ToString();
                    string Page = Request.Url.AbsolutePath.ToString();
                    Response.Redirect("Error?techerror=" + HttpUtility.UrlEncode(Enc.EncryptData(error)) + "&page=" + HttpUtility.UrlEncode(Enc.EncryptData(Page)));
                }
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert",
                "ErrorMssgPopup('Session Expired,Please login again');window.location='Login'", true);
        }

    }
    protected void ddlmaster_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewBindSelectedIndexchange();
    }
    public void GridViewBindSelectedIndexchange()
    {
        try
        {
            if (ddlmaster.SelectedItem.Text != "Select")
            {
                DataTable DtGrid = new DataTable();
                if (Enc.DecryptData(Session["Type"].ToString()) == "SuperAdmin" || Enc.DecryptData(Session["Type"].ToString()) == "Admin")
                {
                    DtGrid = Lo.RetriveMasterData(0, ddlmaster.SelectedItem.Value, "", 0, "", "", "ViewDesignation");
                    ddlmaster.Enabled = true;
                }
                else
                {
                    DtGrid = Lo.RetriveMasterData(0, Session["CompanyRefNo"].ToString(), "", 0, "", "", "ViewDesignation");
                    ddlmaster.Enabled = false;
                }
                if (DtGrid.Rows.Count > 0)
                {
                    gvViewDesignationSave.DataSource = DtGrid;
                    gvViewDesignationSave.DataBind();
                    gvViewDesignationSave.Visible = true;
                }
                else
                {
                    gvViewDesignationSave.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            string error = ex.ToString();
            string Page = Request.Url.AbsolutePath.ToString();
            Response.Redirect("Error?techerror=" + HttpUtility.UrlEncode(Enc.EncryptData(error)) + "&page=" + HttpUtility.UrlEncode(Enc.EncryptData(Page)));
        }
    }
    protected void BindMasterCompany()
    {
        string sType = "", sName = "", sID = "", mSID = "";
        Int16 id = 0;
        string sRole = "";
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
        ddlmaster.Enabled = true;
        lblMastcompany.Text = "Select Company ";
        DataTable Dtchkintrestedarea = Lo.RetriveMasterData(id, mSID, sRole, 0, "", "", sType);
        if (Dtchkintrestedarea.Rows.Count > 0 && Dtchkintrestedarea != null)
        {
            Co.FillDropdownlist(ddlmaster, Dtchkintrestedarea, sName, sID);
            if (Enc.DecryptData(Session["Type"].ToString()) == "SuperAdmin" || Enc.DecryptData(Session["Type"].ToString()) == "Admin")
            {
                ddlmaster.Items.Insert(0, "Select");
                ddlmaster.Enabled = true;
            }
            else
            {
                ddlmaster.Enabled = false;
                GridViewBindSelectedIndexchange();
            }
        }
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        txtDesignation.Text = "";
    }
    protected void SaveComp()
    {
        try
        {
            string StrSaveComp = "";
            if (hdid.Value != null && hdid.Value != "")
            {
                HySave["DesignationId"] = Convert.ToInt16(hdid.Value);
            }
            else
            {
                HySave["DesignationId"] = 0;
            }
            HySave["CompanyRefNo"] = ddlmaster.SelectedItem.Value;
            HySave["DesignationRefNo"] = "";
            HySave["Designation"] = Co.RSQandSQLInjection(txtDesignation.Text.Trim(), "soft");
            HySave["CreatedBy"] = Enc.DecryptData(ViewState["UserLoginEmail"].ToString());
            StrSaveComp = Lo.SaveCompDesignation(HySave, out _sysMsg, out _msg);
            if (_sysMsg != "")
            {
                if (hdid.Value == "")
                {
                    GridViewBindSelectedIndexchange();
                    txtDesignation.Text = "";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "SuccessfullPop('Save successfully !')", true);
                    // myhtmldiv.InnerHtml = AntiForgery.GetHtml().ToString();
                }
                else
                {
                    GridViewBindSelectedIndexchange();
                    txtDesignation.Text = "";
                    hdid.Value = "";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "SuccessfullPop('Designation updated successfully !')", true);
                    // myhtmldiv.InnerHtml = AntiForgery.GetHtml().ToString();
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('Record not saved.')", true);
            }
        }
        catch (Exception ex)
        {
            string error = ex.ToString();
            string Page = Request.Url.AbsolutePath.ToString();
            Response.Redirect("Error?techerror=" + HttpUtility.UrlEncode(Enc.EncryptData(error)) + "&page=" + HttpUtility.UrlEncode(Enc.EncryptData(Page)));
        }
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtDesignation.Text.Trim() != "")
            {
                if (ddlmaster.SelectedItem.Value != "Select")
                {
                    if (ddlmaster.Enabled == true)
                    {
                        DataTable dtIsDesignation = Lo.RetriveMasterData(0, ddlmaster.SelectedItem.Value, txtDesignation.Text, 0, "", "", "ValidDesignation");
                        if (dtIsDesignation.Rows.Count > 0)
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('Designation already exists !')", true);
                        }
                        else
                        {
                            SaveComp();
                        }
                    }
                    else
                    {
                        SaveComp();
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('Select company !')", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('Designation can not be empty !')", true);
            }
        }
        catch (Exception rx)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('" + rx.Message + "')", true);
        }
    }
    protected void EditCode()
    {
        if (Session["CompanyRefNo"] != null)
        {
            DataTable DtView = Lo.RetriveMasterData(0, Enc.DecryptData(Request.QueryString["mrcreaterole"].ToString()), "", 0, "", "", "AllDesignation");
            if (DtView.Rows.Count > 0)
            {
                hdid.Value = DtView.Rows[0]["DesignationId"].ToString();
                DataTable DtCompanyDDL = Lo.RetriveMasterData(0, DtView.Rows[0]["CompanyRefNo"].ToString(), "Company", 0, "", "", "CompanyName");
                if (DtCompanyDDL.Rows.Count > 0)
                {
                    Co.FillDropdownlist(ddlmaster, DtCompanyDDL, "CompanyName", "CompanyRefNo");
                    ddlmaster.Enabled = false;
                }
                lblMastcompany.Text = "Select Company ";
                txtDesignation.Text = DtView.Rows[0]["Designation"].ToString();
            }
        }
    }
    protected void gvViewDesignationSave_RowCreated(object sender, GridViewRowEventArgs e)
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