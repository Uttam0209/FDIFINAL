using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using BusinessLayer;
using Encryption;
using System.IO;
using System.Collections.Specialized;

public partial class Admin_AddDesignation : System.Web.UI.Page
{
    Logic Lo = new Logic();
    Cryptography Enc = new Cryptography();
    DataUtility Co = new DataUtility();
    private Int64 id = 0;
    private string _sysMsg = string.Empty;
    private string _msg = string.Empty;
    HybridDictionary HySave = new HybridDictionary();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
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
                    strheadPage.Append("<li class=''><span>" + MmCval + "</span></li>");
                }
                divHeadPage.InnerHtml = strheadPage.ToString();
                strheadPage.Append("</ul");

                BindMasterCompany();
                
            }
            if (Request.QueryString["mcurrentcompRefNo"] != null)
            {
                EditCode();
            }
        }

    }

    protected void BindMasterCompany()
    {
        string sType = "", sName = "", sID = "", mSID = "";
        Int16 id = 0;
        string sRole = "";
        if (Enc.DecryptData(Session["Type"].ToString()) == "SuperAdmin")
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
        else if (Enc.DecryptData(Session["Type"].ToString()) == "Factory")
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
            ddlmaster.Items.Insert(0, "Select");
        }

    }

    protected void btncancel_Click(object sender, EventArgs e)
    {
        txtDesignation.Text = "";
    }
    protected void SaveComp()
    {
        string StrSaveComp = "";
        if (Request.QueryString["mrcreaterole"] != null)
        {
            string str = Enc.DecryptData(Request.QueryString["mrcreaterole"].ToString());
            HySave["DesignationId"] = Convert.ToInt16(str);
            
        }
        else
        {
            HySave["DesignationId"] = 0;
        }
        HySave["CompanyRefNo"] = ddlmaster.SelectedItem.Value;
        HySave["Designation"] = Co.RSQandSQLInjection(txtDesignation.Text.Trim(), "soft");
        StrSaveComp = Lo.SaveCompDesignation(HySave, out _sysMsg, out _msg);
        if (StrSaveComp != "")
        {

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + btnsubmit.Text + " Save successfully !')", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record not saved.')", true);
        }
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        if (txtDesignation.Text.Trim() != "")
        {
            if (ddlmaster.Enabled == true)
            {
                DataTable dtIsDesignation = Lo.RetriveMasterData(0, ddlmaster.SelectedItem.Value, txtDesignation.Text, 0, "", "", "ValidDesignation");
                if (dtIsDesignation.Rows.Count > 0)
                {

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Designation already exists !')", true);
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
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Field can not be empty !')", true);
        }
    }

    protected void EditCode()
    {
        if (Session["CompanyRefNo"] != null)
        {
            DataTable DtView = Lo.RetriveMasterData(0, Enc.DecryptData(Request.QueryString["mrcreaterole"].ToString()), "", 0, "", "", "AllDesignation");
            if (DtView.Rows.Count > 0)
            {
                id = Convert.ToInt16(DtView.Rows[0]["DesignationId"].ToString());
                DataTable DtCompanyDDL = Lo.RetriveMasterData(0, DtView.Rows[0]["CompanyRefNo"].ToString(), "Company", 0, "", "", "CompanyName");
                if (DtCompanyDDL.Rows.Count > 0)
                {
                    Co.FillDropdownlist(ddlmaster, DtCompanyDDL, "CompanyName", "CompanyRefNo");
                    ddlmaster.Enabled = false;
                }
                lblMastcompany.Text = "Select Company ";
                
                txtDesignation.Text = DtView.Rows[0]["Designation"].ToString();
                btnsubmit.Text = "Edit";
            }
        }
    }
}