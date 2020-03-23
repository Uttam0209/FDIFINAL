﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessLayer;
using Encryption;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Collections.Specialized;

public partial class Vendor_V_FinanceInfo : System.Web.UI.Page
{
    #region Variable
    Logic Lo = new Logic();
    Cryptography Enc = new Cryptography();
    DataUtility Co = new DataUtility();
    HybridDictionary HySaveVendorRegisdetail = new HybridDictionary();
    private string _msg = string.Empty;
    private string _sysMsg = string.Empty;
    Int64 Mid = 0;
    #endregion
    #region PageLoad
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["User"] != null)
        {
            if (!IsPostBack)
            {
                PgL();
            }
        }
        else
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert",
                   "alert('Session Expired,Please login again');window.location='VendorLogin'", true);
    }
    protected void PgL()
    {
        SetInitialRowType();
        DataTable DtCheckSavedetails = Lo.RetriveVendor(0, Enc.DecryptData(Session["VendorRefNo"].ToString()), "", "CheckRegis");
        if (DtCheckSavedetails.Rows.Count > 0)
        {
            btnsubmit.Text = "Update";
            ViewState["Mid"] = Convert.ToInt64(DtCheckSavedetails.Rows[0]["VendorDetailID"].ToString());
            DataTable dtcheckmultigriddata = Lo.RetriveVendor(0, Enc.DecryptData(Session["VendorRefNo"].ToString()), "", "RetriveMultigrid");
            if (dtcheckmultigriddata.Rows.Count > 0)
            {
                DataView dv = new DataView(dtcheckmultigriddata);
                dv.RowFilter = "Type='TurnOver'";
                if (dv.Count > 0)
                {
                    gvturnoveredit.DataSource = dv;
                    gvturnoveredit.DataBind();
                    gvTURNOVERDURINGLAST3YEARS.Visible = false;
                    gvturnoveredit.Visible = true;
                }
                else
                {
                    gvturnoveredit.Visible = false;
                    gvTURNOVERDURINGLAST3YEARS.Visible = true;
                    SetInitialRowTurnOverLast3Years();
                }
                dv.RowFilter = "Type='AccountInfo'";
                if (dv.Count > 0)
                {
                    gvaccountedit.DataSource = dv;
                    gvaccountedit.DataBind();
                    gvaccount.Visible = false;
                    gvaccountedit.Visible = true;
                }
                else
                {
                    gvaccountedit.Visible = false;
                    gvaccount.Visible = true;
                    SetInitialRowAccountDetails();
                }
            }
            else
            {

            }
        }
        else
        {
            SetInitialRowTurnOverLast3Years();
            SetInitialRowAccountDetails();
        }
    }
    #endregion
    //test type
    private void SetInitialRowType()
    {
        //Create false table
        DataTable dttype = new DataTable();
        DataRow drtype = null;
        dttype.Columns.Add(new DataColumn("SNo", typeof(string)));
        dttype.Columns.Add(new DataColumn("Type", typeof(string)));
        dttype.Columns.Add(new DataColumn("Valid", typeof(string)));
        drtype = dttype.NewRow();
        drtype["SNo"] = 1;
        drtype["Type"] = "Financial";
        drtype["Valid"] = string.Empty;
        dttype.Rows.Add(drtype);
        drtype = dttype.NewRow();
        drtype["SNo"] = 2;
        drtype["Type"] = "Banning";
        drtype["Valid"] = string.Empty;
        dttype.Rows.Add(drtype);
        drtype = dttype.NewRow();
        drtype["SNo"] = 3;
        drtype["Type"] = "Suspension";
        drtype["Valid"] = string.Empty;
        dttype.Rows.Add(drtype);
        drtype = dttype.NewRow();
        drtype["SNo"] = 4;
        drtype["Type"] = "Tender holiday";
        drtype["Valid"] = string.Empty;
        dttype.Rows.Add(drtype);
        ViewState["Type"] = dttype;
        gvtype.DataSource = dttype;
        gvtype.DataBind();
    }
    //Add Grid of Turn Over During Last 3 years
    #region Turn Over during last three year
    private void SetInitialRowTurnOverLast3Years()
    {
        //Create false table
        DataTable dtTurnOver = new DataTable();
        DataRow drTurnOver = null;
        dtTurnOver.Columns.Add(new DataColumn("SNo", typeof(string)));
        dtTurnOver.Columns.Add(new DataColumn("FinancialYear", typeof(string)));
        dtTurnOver.Columns.Add(new DataColumn("CurrentAsset", typeof(string)));
        dtTurnOver.Columns.Add(new DataColumn("CurrentLiblities", typeof(string)));
        dtTurnOver.Columns.Add(new DataColumn("ProfitLoss", typeof(string)));
        dtTurnOver.Columns.Add(new DataColumn("BalanceSheet", typeof(string)));
        drTurnOver = dtTurnOver.NewRow();
        drTurnOver["SNo"] = 1;
        drTurnOver["FinancialYear"] = string.Empty;
        drTurnOver["CurrentAsset"] = string.Empty;
        drTurnOver["CurrentLiblities"] = string.Empty;
        drTurnOver["ProfitLoss"] = string.Empty;
        drTurnOver["BalanceSheet"] = string.Empty;
        dtTurnOver.Rows.Add(drTurnOver);
        //Store the DataTable in ViewState or bind or show false grid
        ViewState["TurnOver"] = dtTurnOver;
        gvTURNOVERDURINGLAST3YEARS.DataSource = dtTurnOver;
        gvTURNOVERDURINGLAST3YEARS.DataBind();
    }
    private void AddNewRowToGridTurnOver()
    {
        int TOrowIndex = 0;
        if (ViewState["TurnOver"] != null)
        {
            DataTable dtCurrentTableTuOver = (DataTable)ViewState["TurnOver"];
            DataRow drCurrentRowNameTuOver = null;
            if (dtCurrentTableTuOver.Rows.Count > 0 && dtCurrentTableTuOver.Rows.Count <= 2)
            {
                for (int i = 1; i <= dtCurrentTableTuOver.Rows.Count; i++)
                {
                    //extract the TextBox values

                    TextBox TextBox1TO = (TextBox)gvTURNOVERDURINGLAST3YEARS.Rows[TOrowIndex].Cells[1].FindControl("txtfinyear");
                    TextBox TextBox2TO = (TextBox)gvTURNOVERDURINGLAST3YEARS.Rows[TOrowIndex].Cells[2].FindControl("txtcurrentasset");
                    TextBox TextBox3TO = (TextBox)gvTURNOVERDURINGLAST3YEARS.Rows[TOrowIndex].Cells[3].FindControl("txtcurrentlibilites");
                    TextBox TextBox4TO = (TextBox)gvTURNOVERDURINGLAST3YEARS.Rows[TOrowIndex].Cells[4].FindControl("txtprofitloss");
                    FileUpload FuTO = (FileUpload)gvTURNOVERDURINGLAST3YEARS.Rows[TOrowIndex].Cells[5].FindControl("fufileprofitloss");
                    HiddenField hfTurner = (HiddenField)gvTURNOVERDURINGLAST3YEARS.Rows[TOrowIndex].Cells[5].FindControl("hfprofitloss");
                    hfTurner.Value = FuTO.FileName.ToString();
                    drCurrentRowNameTuOver = dtCurrentTableTuOver.NewRow();
                    drCurrentRowNameTuOver["SNo"] = i + 1;
                    dtCurrentTableTuOver.Rows[i - 1]["FinancialYear"] = TextBox1TO.Text;
                    dtCurrentTableTuOver.Rows[i - 1]["CurrentAsset"] = TextBox2TO.Text;
                    dtCurrentTableTuOver.Rows[i - 1]["CurrentLiblities"] = TextBox3TO.Text;
                    dtCurrentTableTuOver.Rows[i - 1]["ProfitLoss"] = TextBox4TO.Text;
                    dtCurrentTableTuOver.Rows[i - 1]["BalanceSheet"] = hfTurner.Value;
                    TOrowIndex++;
                }
                dtCurrentTableTuOver.Rows.Add(drCurrentRowNameTuOver);
                ViewState["TurnOver"] = dtCurrentTableTuOver;
                gvTURNOVERDURINGLAST3YEARS.DataSource = dtCurrentTableTuOver;
                gvTURNOVERDURINGLAST3YEARS.DataBind();
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Max three record add.')", true);
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }
        SetPreviousDataGovtTurnOver();
    }
    private void SetPreviousDataGovtTurnOver()
    {
        int rowIndexTover = 0;
        if (ViewState["TurnOver"] != null)
        {
            DataTable dtGovrTurnOver = (DataTable)ViewState["TurnOver"];
            if (dtGovrTurnOver.Rows.Count > 0)
            {
                for (int i = 0; i < dtGovrTurnOver.Rows.Count; i++)
                {
                    TextBox TextBox_1 = (TextBox)gvTURNOVERDURINGLAST3YEARS.Rows[rowIndexTover].Cells[1].FindControl("txtfinyear");
                    TextBox TextBox_2 = (TextBox)gvTURNOVERDURINGLAST3YEARS.Rows[rowIndexTover].Cells[2].FindControl("txtcurrentasset");
                    TextBox TextBox_3 = (TextBox)gvTURNOVERDURINGLAST3YEARS.Rows[rowIndexTover].Cells[3].FindControl("txtcurrentlibilites");
                    TextBox TextBox_4 = (TextBox)gvTURNOVERDURINGLAST3YEARS.Rows[rowIndexTover].Cells[4].FindControl("txtprofitloss");
                    HiddenField hfprofloss = (HiddenField)gvTURNOVERDURINGLAST3YEARS.Rows[rowIndexTover].Cells[5].FindControl("hfprofitloss");
                    TextBox_1.Text = dtGovrTurnOver.Rows[i]["FinancialYear"].ToString();
                    TextBox_2.Text = dtGovrTurnOver.Rows[i]["CurrentAsset"].ToString();
                    TextBox_3.Text = dtGovrTurnOver.Rows[i]["CurrentLiblities"].ToString();
                    TextBox_4.Text = dtGovrTurnOver.Rows[i]["ProfitLoss"].ToString();
                    hfprofloss.Value = dtGovrTurnOver.Rows[i]["BalanceSheet"].ToString();
                    rowIndexTover++;
                }
            }
        }
    }
    protected void btnturnover_Click(object sender, EventArgs e)
    {
        AddNewRowToGridTurnOver();
    }
    protected void gvTURNOVERDURINGLAST3YEARS_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataTable dtgridTOverrowcreated = (DataTable)ViewState["TurnOver"];
            LinkButton lbTurnOver = (LinkButton)e.Row.FindControl("lbturnover");
            if (lbTurnOver != null)
            {
                if (dtgridTOverrowcreated.Rows.Count > 1)
                {
                    if (e.Row.RowIndex == dtgridTOverrowcreated.Rows.Count - 1)
                    {
                        lbTurnOver.Visible = false;
                    }
                }
                else
                {
                    lbTurnOver.Visible = false;
                }
            }
        }
    }
    protected void lbturnover_Click(object sender, EventArgs e)
    {
        LinkButton lbto = (LinkButton)sender;
        GridViewRow gvRowto = (GridViewRow)lbto.NamingContainer;
        int rowID = gvRowto.RowIndex;
        if (ViewState["TurnOver"] != null)
        {
            DataTable dtremovegridto = (DataTable)ViewState["TurnOver"];
            if (dtremovegridto.Rows.Count > 1)
            {
                if (gvRowto.RowIndex < dtremovegridto.Rows.Count - 1)
                {
                    dtremovegridto.Rows.Remove(dtremovegridto.Rows[rowID]);
                    ResetRowIDTurnOver(dtremovegridto);
                }
            }
            ViewState["TurnOver"] = dtremovegridto;
            gvTURNOVERDURINGLAST3YEARS.DataSource = dtremovegridto;
            gvTURNOVERDURINGLAST3YEARS.DataBind();
        }
        SetPreviousDataGovtTurnOver();
    }
    private void ResetRowIDTurnOver(DataTable dtTOver)
    {
        int rowNumberTO = 1;
        if (dtTOver.Rows.Count > 0)
        {
            foreach (DataRow row in dtTOver.Rows)
            {
                row[0] = rowNumberTO;
                rowNumberTO++;
            }
        }
    }
    protected void SaveCodeForTurnOver()
    {
        int rowIndex = 0;
        DataTable dttos = new DataTable();
        dttos.Columns.Add(new DataColumn("SNo", typeof(string)));
        dttos.Columns.Add(new DataColumn("FinancialYear", typeof(string)));
        dttos.Columns.Add(new DataColumn("CurrentAsset", typeof(string)));
        dttos.Columns.Add(new DataColumn("CurrentLiblities", typeof(string)));
        dttos.Columns.Add(new DataColumn("ProfitLoss", typeof(string)));
        dttos.Columns.Add(new DataColumn("BalanceSheet", typeof(string)));
        DataRow drtos = null;
        for (int i = 0; gvTURNOVERDURINGLAST3YEARS.Rows.Count > i; i++)
        {
            TextBox TextBox1TO = (TextBox)gvTURNOVERDURINGLAST3YEARS.Rows[i].Cells[1].FindControl("txtfinyear");
            TextBox TextBox2TO = (TextBox)gvTURNOVERDURINGLAST3YEARS.Rows[i].Cells[2].FindControl("txtcurrentasset");
            TextBox TextBox3TO = (TextBox)gvTURNOVERDURINGLAST3YEARS.Rows[i].Cells[3].FindControl("txtcurrentlibilites");
            TextBox TextBox4TO = (TextBox)gvTURNOVERDURINGLAST3YEARS.Rows[i].Cells[4].FindControl("txtprofitloss");
            FileUpload FuTO = (FileUpload)gvTURNOVERDURINGLAST3YEARS.Rows[i].Cells[5].FindControl("fufileprofitloss");
            if (TextBox1TO.Text != "" && TextBox2TO.Text != "")
            {
                drtos = dttos.NewRow();
                drtos["FinancialYear"] = TextBox1TO.Text;
                drtos["CurrentAsset"] = TextBox2TO.Text;
                drtos["CurrentLiblities"] = TextBox3TO.Text;
                drtos["ProfitLoss"] = TextBox4TO.Text;
                drtos["BalanceSheet"] = FuTO.PostedFile.FileName;
                dttos.Rows.Add(drtos);
            }
        }
        ViewState["TurnOver"] = dttos;
    }
    #endregion
    //Add Grid of Account Details
    #region Account Details Comapany
    private void SetInitialRowAccountDetails()
    {
        //Create false table
        DataTable dtAccount = new DataTable();
        DataRow drAccount = null;
        dtAccount.Columns.Add(new DataColumn("SNo", typeof(string)));
        dtAccount.Columns.Add(new DataColumn("NameofBank", typeof(string)));
        dtAccount.Columns.Add(new DataColumn("TypeofAccount", typeof(string)));
        dtAccount.Columns.Add(new DataColumn("AccountNo", typeof(string)));
        dtAccount.Columns.Add(new DataColumn("MICRCode", typeof(string)));
        dtAccount.Columns.Add(new DataColumn("IFSCCode", typeof(string)));
        dtAccount.Columns.Add(new DataColumn("Certificate", typeof(string)));
        drAccount = dtAccount.NewRow();
        drAccount["SNo"] = 1;
        drAccount["NameofBank"] = string.Empty;
        drAccount["TypeofAccount"] = string.Empty;
        drAccount["AccountNo"] = string.Empty;
        drAccount["MICRCode"] = string.Empty;
        drAccount["IFSCCode"] = string.Empty;
        drAccount["Certificate"] = string.Empty;
        dtAccount.Rows.Add(drAccount);
        //Store the DataTable in ViewState or bind or show false grid
        ViewState["AccountDetails"] = dtAccount;
        gvaccount.DataSource = dtAccount;
        gvaccount.DataBind();
    }
    private void AddNewRowToGridAccountDetails()
    {
        int AccrowIndex = 0;
        if (ViewState["AccountDetails"] != null)
        {
            DataTable dtCurrentTableAccount = (DataTable)ViewState["AccountDetails"];
            DataRow drCurrentRowNameAccount = null;
            if (dtCurrentTableAccount.Rows.Count > 0 && dtCurrentTableAccount.Rows.Count <= 2)
            {
                for (int i = 1; i <= dtCurrentTableAccount.Rows.Count; i++)
                {
                    //extract the TextBox values

                    TextBox TextBox1AO = (TextBox)gvaccount.Rows[AccrowIndex].Cells[1].FindControl("txtnameofbank");
                    DropDownList TextBox2AO = (DropDownList)gvaccount.Rows[AccrowIndex].Cells[2].FindControl("ddltypeofaccount");
                    TextBox TextBox3AO = (TextBox)gvaccount.Rows[AccrowIndex].Cells[3].FindControl("txtaccountno");
                    TextBox TextBox4AO = (TextBox)gvaccount.Rows[AccrowIndex].Cells[4].FindControl("txtmicrcode");
                    TextBox TextBox5AO = (TextBox)gvaccount.Rows[AccrowIndex].Cells[4].FindControl("txtifsccode");
                    FileUpload FuTO = (FileUpload)gvaccount.Rows[AccrowIndex].Cells[5].FindControl("fusolvencycertificate");
                    HiddenField hfprofloss = (HiddenField)gvaccount.Rows[AccrowIndex].Cells[5].FindControl("hffusolvencycertificate");
                    hfprofloss.Value = FuTO.FileName;
                    drCurrentRowNameAccount = dtCurrentTableAccount.NewRow();
                    drCurrentRowNameAccount["SNo"] = i + 1;
                    dtCurrentTableAccount.Rows[i - 1]["NameofBank"] = TextBox1AO.Text;
                    dtCurrentTableAccount.Rows[i - 1]["TypeofAccount"] = TextBox2AO.Text;
                    dtCurrentTableAccount.Rows[i - 1]["AccountNo"] = TextBox3AO.Text;
                    dtCurrentTableAccount.Rows[i - 1]["MICRCode"] = TextBox4AO.Text;
                    dtCurrentTableAccount.Rows[i - 1]["IFSCCode"] = TextBox5AO.Text;
                    dtCurrentTableAccount.Rows[i - 1]["Certificate"] = hfprofloss.Value;
                    AccrowIndex++;
                }
                dtCurrentTableAccount.Rows.Add(drCurrentRowNameAccount);
                ViewState["AccountDetails"] = dtCurrentTableAccount;
                gvaccount.DataSource = dtCurrentTableAccount;
                gvaccount.DataBind();
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Max three record add.')", true);
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }
        SetPreviousDataGovtAccount();
    }
    private void SetPreviousDataGovtAccount()
    {
        int rowIndexAcc = 0;
        if (ViewState["AccountDetails"] != null)
        {
            DataTable dtGovrAccount = (DataTable)ViewState["AccountDetails"];
            if (dtGovrAccount.Rows.Count > 0)
            {
                for (int i = 0; i < dtGovrAccount.Rows.Count; i++)
                {
                    TextBox TextBox_1 = (TextBox)gvaccount.Rows[rowIndexAcc].Cells[1].FindControl("txtnameofbank");
                    DropDownList TextBox_2 = (DropDownList)gvaccount.Rows[rowIndexAcc].Cells[2].FindControl("ddltypeofaccount");
                    TextBox TextBox_3 = (TextBox)gvaccount.Rows[rowIndexAcc].Cells[3].FindControl("txtaccountno");
                    TextBox TextBox_4 = (TextBox)gvaccount.Rows[rowIndexAcc].Cells[4].FindControl("txtmicrcode");
                    TextBox TextBox_5 = (TextBox)gvaccount.Rows[rowIndexAcc].Cells[4].FindControl("txtifsccode");
                    HiddenField hfprofloss = (HiddenField)gvaccount.Rows[rowIndexAcc].Cells[5].FindControl("hffusolvencycertificate");
                    TextBox_1.Text = dtGovrAccount.Rows[i]["NameofBank"].ToString();
                    TextBox_2.Text = dtGovrAccount.Rows[i]["TypeofAccount"].ToString();
                    TextBox_3.Text = dtGovrAccount.Rows[i]["AccountNo"].ToString();
                    TextBox_4.Text = dtGovrAccount.Rows[i]["MICRCode"].ToString();
                    TextBox_4.Text = dtGovrAccount.Rows[i]["IFSCCode"].ToString();
                    hfprofloss.Value = dtGovrAccount.Rows[i]["Certificate"].ToString();
                    rowIndexAcc++;
                }
            }
        }
    }
    protected void lbNewAccount_Click(object sender, EventArgs e)
    {
        AddNewRowToGridAccountDetails();
    }
    protected void gvaccount_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataTable dtgridACverrowcreated = (DataTable)ViewState["AccountDetails"];
            LinkButton lbACC = (LinkButton)e.Row.FindControl("lbNewAccountxx");
            if (lbACC != null)
            {
                if (dtgridACverrowcreated.Rows.Count > 1)
                {
                    if (e.Row.RowIndex == dtgridACverrowcreated.Rows.Count - 1)
                    {
                        lbACC.Visible = false;
                    }
                }
                else
                {
                    lbACC.Visible = false;
                }
            }
        }
    }
    protected void lbNewAccountxx_Click(object sender, EventArgs e)
    {
        LinkButton lbAC = (LinkButton)sender;
        GridViewRow gvRowto = (GridViewRow)lbAC.NamingContainer;
        int rowID = gvRowto.RowIndex;
        if (ViewState["AccountDetails"] != null)
        {
            DataTable dtremovegridac = (DataTable)ViewState["AccountDetails"];
            if (dtremovegridac.Rows.Count > 1)
            {
                if (gvRowto.RowIndex < dtremovegridac.Rows.Count - 1)
                {
                    dtremovegridac.Rows.Remove(dtremovegridac.Rows[rowID]);
                    ResetRowIDAcc(dtremovegridac);
                }
            }
            ViewState["AccountDetails"] = dtremovegridac;
            gvaccount.DataSource = dtremovegridac;
            gvaccount.DataBind();
        }
        SetPreviousDataGovtAccount();
    }
    private void ResetRowIDAcc(DataTable dtACC)
    {
        int rowNumberTO = 1;
        if (dtACC.Rows.Count > 0)
        {
            foreach (DataRow row in dtACC.Rows)
            {
                row[0] = rowNumberTO;
                rowNumberTO++;
            }
        }
    }
    protected void SaveCodeForAccount()
    {
        int rowIndex = 0;
        DataTable dtaccounts = new DataTable();
        dtaccounts.Columns.Add(new DataColumn("SNo", typeof(string)));
        dtaccounts.Columns.Add(new DataColumn("NameofBank", typeof(string)));
        dtaccounts.Columns.Add(new DataColumn("TypeofAccount", typeof(string)));
        dtaccounts.Columns.Add(new DataColumn("AccountNo", typeof(string)));
        dtaccounts.Columns.Add(new DataColumn("MICRCode", typeof(string)));
        dtaccounts.Columns.Add(new DataColumn("IFSCCode", typeof(string)));
        dtaccounts.Columns.Add(new DataColumn("Certificate", typeof(string)));
        DataRow draccounts = null;
        for (int i = 0; gvaccount.Rows.Count > i; i++)
        {
            TextBox TextBox1AO = (TextBox)gvaccount.Rows[i].Cells[1].FindControl("txtnameofbank");
            DropDownList TextBox2AO = (DropDownList)gvaccount.Rows[i].Cells[2].FindControl("ddltypeofaccount");
            TextBox TextBox3AO = (TextBox)gvaccount.Rows[i].Cells[3].FindControl("txtaccountno");
            TextBox TextBox4AO = (TextBox)gvaccount.Rows[i].Cells[4].FindControl("txtmicrcode");
            TextBox TextBox5AO = (TextBox)gvaccount.Rows[i].Cells[4].FindControl("txtifsccode");
            FileUpload FuTO = (FileUpload)gvaccount.Rows[i].Cells[5].FindControl("fusolvencycertificate");
            if (TextBox1AO.Text != "" && TextBox3AO.Text != "")
            {
                draccounts = dtaccounts.NewRow();
                draccounts["NameofBank"] = TextBox1AO.Text;
                draccounts["TypeofAccount"] = TextBox2AO.Text;
                draccounts["AccountNo"] = TextBox3AO.Text;
                draccounts["MICRCode"] = TextBox4AO.Text;
                draccounts["IFSCCode"] = TextBox5AO.Text;
                draccounts["Certificate"] = FuTO.PostedFile.FileName;
                dtaccounts.Rows.Add(draccounts);
            }
        }
        ViewState["AccountDetails"] = dtaccounts;
    }
    #endregion
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        try
        {
            SaveRegistration();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message.ToString() + "')", true);
        }
    }
    protected void SaveRegistration()
    {

        DataTable dtturnover = new DataTable();
        DataTable dtaccount = new DataTable();
        if (btnsubmit.Text == "Update")
        {
            if (gvaccount.Visible == true && gvTURNOVERDURINGLAST3YEARS.Visible == true)
            {
                SaveCodeForTurnOver();
                dtturnover = (DataTable)ViewState["TurnOver"];
                SaveCodeForAccount();
                dtaccount = (DataTable)ViewState["AccountDetails"];
            }
            else
            {
            }
        }
        else
        {
            SaveCodeForTurnOver();
            dtturnover = (DataTable)ViewState["TurnOver"];
            SaveCodeForAccount();
            dtaccount = (DataTable)ViewState["AccountDetails"];
        }
        string str = Lo.SaveVendorAccountInfo(dtturnover, dtaccount, Enc.DecryptData(Session["VendorRefNo"].ToString()), out _sysMsg, out _msg);
        if (str != "")
        {
            if (btnsubmit.Text == "Update")
            {
                PgL();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Successfully update company information')", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Successfully save company information')", true);
            }
        }
        else
        { ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record not saved.')", true); }
    }
    protected void gvturnoveredit_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "newsave")
        {
            btnsubmit.Text = "Submit";
            txtfinancialyear.Text = "";
            txtcurrasst.Text = "";
            txtcurrliablities.Text = "";
            txtprofitloss.Text = "";
            hffileaudit.Value = "";
            ViewState["editturn"] = null;
            PgL();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "divturnover", "showPopup();", true);
        }
        else if (e.CommandName == "newedit")
        {
            btnsubmit.Text = "Edit & Update";
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gvturnoveredit.Rows[rowIndex];
            HiddenField hfn = (HiddenField)gvturnoveredit.Rows[rowIndex].FindControl("hfturnedit");
            txtfinancialyear.Text = row.Cells[1].Text;
            txtcurrasst.Text = row.Cells[2].Text;
            txtcurrliablities.Text = row.Cells[3].Text;
            txtprofitloss.Text = row.Cells[4].Text;
            hffileaudit.Value = row.Cells[5].Text;
            ViewState["editturn"] = hfn.Value;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "divturnover", "showPopup();", true);
        }
        else if (e.CommandName == "newdel")
        {
            Int32 Delid = Lo.DeleteEditGrid(Convert.ToInt32(e.CommandArgument.ToString()));
            if (Delid != 0)
            {
                PgL();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record deleted successfull.')", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record not deleted.')", true);
            }
        }
    }
    protected void gvaccountedit_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "newsave")
        {
            btnsubmit.Text = "Submit";
            txtnameofbank.Text = "";
            txttypeofaccount.Text = "";
            txtaccountno.Text = "";
            txtmicrcode.Text = "";
            txtifsc.Text = "";
            hfsolencycertificate.Value = "";
            ViewState["editaccount"] = null;
            PgL();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "divbank", "showPopup1();", true);
        }
        else if (e.CommandName == "newedit")
        {
            btnsubmit.Text = "Edit & Update";
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gvaccountedit.Rows[rowIndex];
            HiddenField hfn = (HiddenField)gvaccountedit.Rows[rowIndex].FindControl("hfeditgovt");
            txtnameofbank.Text = row.Cells[1].Text;
            txttypeofaccount.Text = row.Cells[2].Text;
            txtaccountno.Text = row.Cells[3].Text;
            txtmicrcode.Text = row.Cells[4].Text;
            txtifsc.Text = row.Cells[5].Text;
            hfsolencycertificate.Value = row.Cells[6].Text;
            ViewState["editaccount"] = hfn.Value;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "divbank", "showPopup1();", true);
        }
        else if (e.CommandName == "newdel")
        {
            Int32 Delid = Lo.DeleteEditGrid(Convert.ToInt32(e.CommandArgument.ToString()));
            if (Delid != 0)
            {
                PgL();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record deleted successfull.')", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record not deleted.')", true);
            }
        }
    }
    protected void lblsub2_Click(object sender, EventArgs e)
    {
        try
        {
            if (lbsub.Text == "Edit & Update")
            {
                if (txtnameofbank.Text != "")
                {
                    Int32 ESaveID = Lo.UpdateAccount(Convert.ToInt64(ViewState["editaccount"]), Enc.DecryptData(Session["VendorRefNo"].ToString()), txtnameofbank.Text, txttypeofaccount.Text, txtaccountno.Text, txtmicrcode.Text, txtifsc.Text, hfsolencycertificate.Value);
                    if (ESaveID != 0)
                    {
                        txtnameofbank.Text = "";
                        txttypeofaccount.Text = "";
                        txtaccountno.Text = "";
                        txtmicrcode.Text = "";
                        txtifsc.Text = "";
                        hfsolencycertificate.Value = "";
                        ViewState["editaccount"] = null;
                        PgL();
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record update successfully')", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record not update successfully')", true);
                    }
                }
            }
            else if (lbsub.Text == "Submit")
            {
                Int32 ESaveID = Lo.InsertAccount(Enc.DecryptData(Session["VendorRefNo"].ToString()), "AccountInfo", txtnameofbank.Text, txttypeofaccount.Text, txtaccountno.Text, txtmicrcode.Text, txtifsc.Text, hfsolencycertificate.Value);
                if (ESaveID != 0)
                {
                    txtnameofbank.Text = "";
                    txttypeofaccount.Text = "";
                    txtaccountno.Text = "";
                    txtmicrcode.Text = "";
                    txtifsc.Text = "";
                    hfsolencycertificate.Value = "";
                    ViewState["editaccount"] = null;
                    PgL();
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record save successfully')", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record not save successfully')", true);
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);
        }
    }
    protected void lbsub_Click(object sender, EventArgs e)
    {
        try
        {
            if (lbsub.Text == "Edit & Update")
            {
                if (txtfinancialyear.Text != "")
                {
                    Int32 ESaveID = Lo.UpdateTurnOver(Convert.ToInt64(ViewState["editturn"]), Enc.DecryptData(Session["VendorRefNo"].ToString()), txtfinancialyear.Text, txtcurrasst.Text, txtcurrliablities.Text, txtprofitloss.Text, hffileaudit.Value);
                    if (ESaveID != 0)
                    {
                        txtfinancialyear.Text = "";
                        txtcurrasst.Text = "";
                        txtcurrliablities.Text = "";
                        txtprofitloss.Text = "";
                        hffileaudit.Value = "";
                        ViewState["editturn"] = null;
                        PgL();
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record update successfully')", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record not update successfully')", true);
                    }
                }
            }
            else if (lbsub.Text == "Submit")
            {
                Int32 ESaveID = Lo.InsertTurnOver(Enc.DecryptData(Session["VendorRefNo"].ToString()), "TurnOver", txtfinancialyear.Text, txtcurrasst.Text, txtcurrliablities.Text, txtprofitloss.Text, hffileaudit.Value);
                if (ESaveID != 0)
                {
                    txtfinancialyear.Text = "";
                    txtcurrasst.Text = "";
                    txtcurrliablities.Text = "";
                    txtprofitloss.Text = "";
                    hffileaudit.Value = "";
                    ViewState["editturn"] = null;
                    PgL();
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record save successfully')", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record not save successfully')", true);
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);
        }
    }
}