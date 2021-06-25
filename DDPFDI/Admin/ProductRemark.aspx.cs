using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Encryption;
using BusinessLayer;
using DataAccessLayer;
using System.Text;
using System.IO;

public partial class Admin_ProductRemark : System.Web.UI.Page
{
    private DataUtility Co = new DataUtility();
    Cryptography objCrypto = new Cryptography();
    Logic Lo = new Logic();
    private string mRefNo = "";
    private Cryptography Encrypt = new Cryptography();
    private DataTable DtCompanyDDL = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Type"] != null || Session["User"] != null)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    string strid = Request.QueryString["id"].ToString().Replace(" ", "+");
                    string strPageName = objCrypto.DecryptData(strid);
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
                }
                if (objCrypto.DecryptData(Session["Type"].ToString()) == "Admin" || objCrypto.DecryptData(Session["Type"].ToString()) == "SuperAdmin")
                {
                    BindProductRemark();
                }
                else
                {
                    Response.RedirectToRoute("ProductList");
                }
            }
        }
    }
    public void BindProductRemark()
    {
        DataTable dtremark = new DataTable();
        dtremark = Lo.RetriveFilterCode("", "", "GetProductRemark");
        if (dtremark.Rows.Count > 0)
        {
            gvproductremark.DataSource = dtremark;
            gvproductremark.DataBind();
        }
    }

    protected void gvproductremark_RowCreated(object sender, GridViewRowEventArgs e)
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

    //protected void ddlcompany_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddlcompany.SelectedItem.Text != "Select")
    //    {
    //        DtCompanyDDL = Lo.RetriveMasterData(0, ddlcompany.SelectedItem.Value, "", 0, "", "", "FactoryCompanyID");
    //        if (DtCompanyDDL.Rows.Count > 0)
    //        {
    //            Co.FillDropdownlist(ddldivision, DtCompanyDDL, "FactoryName", "FactoryRefNo");
    //            ddldivision.Items.Insert(0, "Select");
    //            lblselectdivison.Visible = true;
    //            ddldivision.Visible = true;
    //            lblselectunit.Visible = false;
    //            hidCompanyRefNo.Value = ddlcompany.SelectedItem.Value;
    //            hidType.Value = "Company";

    //        }
    //        else
    //        {
    //            ddldivision.Visible = false;
    //            lblselectdivison.Visible = false;
    //            gvproductremark.Visible = false;

    //        }
    //    }
    //    else if (ddlcompany.SelectedItem.Text == "Select")
    //    {
    //        lblselectdivison.Visible = false;
    //        lblselectunit.Visible = false;

    //    }
    //    hfcomprefno.Value = "";
    //    hfcomprefno.Value = ddlcompany.SelectedItem.Value;
    //}

    //protected void BindCompany()
    //{
    //    if (hidType.Value == "Company")
    //    {
    //        DtCompanyDDL = Lo.RetriveMasterData(0, mRefNo, "Company", 0, "", "", "CompanyName");
    //        if (DtCompanyDDL.Rows.Count > 0)
    //        {
    //            Co.FillDropdownlist(ddlcompany, DtCompanyDDL, "CompanyName", "CompanyRefNo");
    //            ddlcompany.Enabled = false;
    //        }
    //        else
    //        {
    //            ddlcompany.Enabled = false;
    //        }
    //        DtCompanyDDL = Lo.RetriveMasterData(0, ddlcompany.SelectedItem.Value, "Factory1", 0, "", "", "CompanyName");
    //        if (DtCompanyDDL.Rows.Count > 0)
    //        {
    //            Co.FillDropdownlist(ddldivision, DtCompanyDDL, "FactoryName", "FactoryRefNo");
    //            ddldivision.Items.Insert(0, "Select");
    //            if (hidType.Value == "Company")
    //            {
    //                lblselectdivison.Visible = true;
    //                ddldivision.Enabled = true;
    //                ddlunit.Visible = false;
    //                lblselectunit.Visible = false;
    //            }
    //            else
    //            {
    //                ddldivision.Enabled = false;
    //            }
    //        }
    //        else
    //        {
    //            lblselectdivison.Visible = false;
    //            lblselectunit.Visible = false;
    //        }
    //    }
    //    else if (hidType.Value == "Factory" || hidType.Value == "Division")
    //    {
    //        DtCompanyDDL = Lo.RetriveMasterData(0, mRefNo, "Company1", 0, "", "", "CompanyName");
    //        if (DtCompanyDDL.Rows.Count > 0)
    //        {
    //            Co.FillDropdownlist(ddlcompany, DtCompanyDDL, "CompanyName", "CompanyRefNo");
    //            ddlcompany.Enabled = false;
    //        }
    //        else
    //        {
    //            ddlcompany.Enabled = false;
    //        }
    //        DtCompanyDDL = Lo.RetriveMasterData(0, ddlcompany.SelectedItem.Value, "Factory1", 0, "", "", "CompanyName");
    //        if (DtCompanyDDL.Rows.Count > 0)
    //        {
    //            Co.FillDropdownlist(ddldivision, DtCompanyDDL, "FactoryName", "FactoryRefNo");
    //            // code by gk to select indivisual division for the particular unit
    //            DataTable dt = Lo.RetriveMasterData(0, mRefNo, "Factory2", 0, "", "", "CompanyName");
    //            if (dt.Rows.Count > 0)
    //            {
    //                ddldivision.SelectedValue = dt.Rows[0]["FactoryRefNo"].ToString();
    //            }
    //            //end code
    //            lblselectdivison.Visible = true;
    //            ddldivision.Enabled = false;
    //        }
    //        else
    //        {
    //            ddldivision.Enabled = false;
    //        }
    //        DtCompanyDDL = Lo.RetriveMasterData(0, ddldivision.SelectedItem.Value, "Unit1", 0, "", "", "CompanyName");
    //        if (DtCompanyDDL.Rows.Count > 0)
    //        {
    //            Co.FillDropdownlist(ddlunit, DtCompanyDDL, "UnitName", "UnitRefNo");
    //            ddlunit.Items.Insert(0, "Select");
    //            ddlunit.Enabled = true;
    //            lblselectunit.Visible = true;
    //        }
    //        else
    //        {
    //            lblselectunit.Visible = false;
    //        }
    //    }
    //    else if (hidType.Value == "Unit")
    //    {
    //        DtCompanyDDL = Lo.RetriveMasterData(0, mRefNo, "Company2", 0, "", "", "CompanyName");
    //        if (DtCompanyDDL.Rows.Count > 0)
    //        {
    //            Co.FillDropdownlist(ddlcompany, DtCompanyDDL, "CompanyName", "CompanyRefNo");
    //            ddlcompany.Enabled = false;
    //        }
    //        else
    //        {
    //            ddlcompany.Enabled = false;
    //        }
    //        DtCompanyDDL = Lo.RetriveMasterData(0, ddlcompany.SelectedItem.Value, "Factory1", 0, "", "", "CompanyName");
    //        if (DtCompanyDDL.Rows.Count > 0)
    //        {
    //            Co.FillDropdownlist(ddldivision, DtCompanyDDL, "FactoryName", "FactoryRefNo");
    //            // code by gk to select indivisual division for the particular unit
    //            DataTable dt = Lo.RetriveMasterData(0, mRefNo, "Factory3", 0, "", "", "CompanyName");
    //            if (dt.Rows.Count > 0)
    //            {
    //                ddldivision.SelectedValue = dt.Rows[0]["FactoryRefNo"].ToString();
    //            }
    //            //end code
    //            lblselectdivison.Visible = true;
    //            ddldivision.Enabled = false;
    //        }
    //        else
    //        {
    //            ddldivision.Enabled = false;
    //        }
    //        DtCompanyDDL = Lo.RetriveMasterData(0, ddldivision.SelectedItem.Value, "Unit1", 0, "", "", "CompanyName");
    //        if (DtCompanyDDL.Rows.Count > 0)
    //        {
    //            Co.FillDropdownlist(ddlunit, DtCompanyDDL, "UnitName", "UnitRefNo");
    //            ddlunit.SelectedValue = mRefNo.ToString();
    //            ddlunit.Enabled = false;
    //            lblselectunit.Visible = true;
    //        }
    //        else
    //        {
    //            ddlunit.Enabled = false;
    //        }
    //    }
    //    else if (hidType.Value == "SuperAdmin" || hidType.Value == "Admin")
    //    {
    //        DtCompanyDDL = Lo.RetriveMasterData(0, "", "", 0, "", "", "Select");
    //        if (DtCompanyDDL.Rows.Count > 0)
    //        {
    //            Co.FillDropdownlist(ddlcompany, DtCompanyDDL, "CompanyName", "CompanyRefNo");
    //            ddlcompany.Items.Insert(0, "Select");
    //            ddlcompany.Enabled = true;
    //        }
    //        else
    //        {
    //            ddlcompany.Enabled = false;
    //        }
    //        lblselectdivison.Visible = false;
    //        lblselectunit.Visible = false;
    //    }
    //    if (hidType.Value == "SuperAdmin" || hidType.Value == "Admin")
    //    { }
    //    else
    //    {
    //        BindProductRemark();
    //    }
    //}
    //protected void ddldivision_OnSelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddldivision.SelectedItem.Text != "Select")
    //    {
    //        DtCompanyDDL = Lo.RetriveMasterData(0, ddldivision.SelectedItem.Value, "", 0, "", "", "UnitSelectID");
    //        if (DtCompanyDDL.Rows.Count > 0)
    //        {
    //            Co.FillDropdownlist(ddlunit, DtCompanyDDL, "UnitName", "UnitRefNo");
    //            ddlunit.Items.Insert(0, "Select");
    //            ddlunit.Visible = true;
    //            lblselectunit.Visible = true;
    //            if (ddlunit.SelectedItem.Text == "Select")
    //            {
    //                ddldivision.Enabled = true;
    //            }
    //            else
    //            { ddldivision.Enabled = false; }
    //            hidCompanyRefNo.Value = ddldivision.SelectedItem.Value;
    //            hidType.Value = "Division";
    //            BindGridView();
    //        }
    //        else
    //        {
    //            lblselectunit.Visible = false;
    //            ddlunit.Visible = false;
    //            hidType.Value = "Division";
    //            BindGridView();
    //        }
    //        hfcomprefno.Value = "";
    //        hfcomprefno.Value = ddldivision.SelectedItem.Value;
    //    }
    //    else if (ddldivision.SelectedItem.Text == "Select")
    //    {
    //        ddlcompany.Enabled = false;
    //        lblselectunit.Visible = false;
    //        hidCompanyRefNo.Value = ddlcompany.SelectedItem.Value;
    //        hidType.Value = "Company";
    //        hfcomprefno.Value = "";
    //        hfcomprefno.Value = ddlcompany.SelectedItem.Value;
    //        BindGridView();
    //    }
    //}
    //protected void ddlunit_OnSelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddlunit.SelectedItem.Text != "Select")
    //    {
    //        hidCompanyRefNo.Value = ddlunit.SelectedItem.Value;
    //        hidType.Value = "Unit";
    //        hfcomprefno.Value = "";
    //        hfcomprefno.Value = ddlunit.SelectedItem.Value;
    //        BindGridView();
    //    }
    //    else
    //    {
    //        hidCompanyRefNo.Value = ddldivision.SelectedItem.Value;
    //        hidType.Value = "Division";
    //        if (hidType.Value == "Unit")
    //        {
    //            ddldivision.Enabled = false;
    //        }
    //        else
    //        {
    //            ddldivision.Enabled = true;
    //        }
    //        hfcomprefno.Value = "";
    //        hfcomprefno.Value = ddldivision.SelectedItem.Value;
    //        BindGridView();
    //    }
    //}
    //protected void txtsearchbyrefid_TextChanged(object sender, EventArgs e)
    //{
    //    if (txtsearchbyrefid.Text != "")
    //    {
    //        BindGridView();
    //    }
    //}
    //protected void BindGridView()
    //{
    //    try
    //    {
    //        DtGrid = Lo.GetDashboardData("Product", "");
    //        if (DtGrid.Rows.Count > 0)
    //        {
    //            UpdateDtGridValue();
    //            DataView dv = new DataView(DtGrid)
    //            {
    //                RowFilter = "CompanyRefNo='" + ddlcompany.SelectedItem.Value + "'"
    //            };
    //            DataTable dtnew = dv.ToTable();
    //            string mProdRefesti = "";
    //            if (dtnew.Rows.Count > 0)
    //            {
    //                if (ddlcompany.SelectedItem.Text != "Select" && ddldivision.Visible == false || ddldivision.SelectedItem.Text == "Select")
    //                {
    //                    if (txtsearchbyrefid.Text != "")
    //                    {
    //                        dv.RowFilter = "ProductRefNo='" + txtsearchbyrefid.Text + "' and CompanyName='" + ddlcompany.SelectedItem.Text + "'";
    //                        mProdRefesti = ddlcompany.SelectedItem.Value;
    //                    }
    //                    else
    //                    {
    //                        dv.RowFilter = "CompanyName='" + ddlcompany.SelectedItem.Text + "'";
    //                        mProdRefesti = ddlcompany.SelectedItem.Value;
    //                    }
    //                }
    //                else if (ddlcompany.SelectedItem.Text != "Select" && ddldivision.SelectedItem.Text != "Select" && ddlunit.Visible == false || ddlunit.SelectedItem.Text == "Select")
    //                {
    //                    if (txtsearchbyrefid.Text != "")
    //                    {
    //                        dv.RowFilter = "ProductRefNo='" + txtsearchbyrefid.Text + "' and FactoryName='" + ddldivision.SelectedItem.Text + "'";
    //                        mProdRefesti = ddldivision.SelectedItem.Value;
    //                    }
    //                    else
    //                    {
    //                        dv.RowFilter = "FactoryName='" + ddldivision.SelectedItem.Text + "'";
    //                        mProdRefesti = ddldivision.SelectedItem.Value;
    //                    }
    //                }
    //                else if (ddlcompany.SelectedItem.Text != "Select" && ddldivision.SelectedItem.Text != "Select" && ddlunit.SelectedItem.Text != "Select")
    //                {
    //                    if (txtsearchbyrefid.Text != "")
    //                    {
    //                        dv.RowFilter = "ProductRefNo='" + txtsearchbyrefid.Text + "' and UnitName='" + ddlunit.SelectedItem.Text + "'";
    //                        mProdRefesti = ddlunit.SelectedItem.Value;
    //                    }
    //                    else
    //                    {
    //                        dv.RowFilter = "UnitName='" + ddlunit.SelectedItem.Text + "'";
    //                        mProdRefesti = ddlunit.SelectedItem.Value;
    //                    }
    //                }
    //                else
    //                {
    //                    dv.Sort = "LastUpdated desc,CompanyName asc,FactoryName asc";
    //                }
    //                DataTable dtads = dv.ToTable();
    //                lbltotfilter.Text = dtads.Rows.Count.ToString();
    //                dtads.Columns.Add("1718", typeof(decimal));
    //                dtads.Columns.Add("1819", typeof(decimal));
    //                dtads.Columns.Add("1920", typeof(decimal));
    //                dtads.Columns.Add("2021", typeof(decimal));
    //                for (int i = 0; dtads.Rows.Count > i; i++)
    //                {
    //                    DataTable dtEstimate1 = Lo.RetriveProductCode("", dtads.Rows[i]["ProductRefNo"].ToString(), "estimate", "");
    //                    if (dtEstimate1.Rows.Count > 0)
    //                    {
    //                        for (int es = 0; dtEstimate1.Rows.Count > es; es++)
    //                        {
    //                            if (dtEstimate1.Rows[es]["FYear"].ToString() == "2017-18" && dtEstimate1.Rows[es]["Type"].ToString() == "O")
    //                            {
    //                                dtads.Rows[i]["1718"] = dtEstimate1.Rows[es]["EstimatedPrice"].ToString();
    //                            }
    //                            if (dtEstimate1.Rows[es]["FYear"].ToString() == "2018-19" && dtEstimate1.Rows[es]["Type"].ToString() == "O")
    //                            {
    //                                dtads.Rows[i]["1819"] = dtEstimate1.Rows[es]["EstimatedPrice"].ToString();
    //                            }
    //                            if (dtEstimate1.Rows[es]["FYear"].ToString() == "2019-20" && dtEstimate1.Rows[es]["Type"].ToString() == "O")
    //                            {
    //                                dtads.Rows[i]["1920"] = dtEstimate1.Rows[es]["EstimatedPrice"].ToString();
    //                            }
    //                            if (dtEstimate1.Rows[es]["FYear"].ToString() == "2020-21" && dtEstimate1.Rows[es]["Type"].ToString() == "F")
    //                            {
    //                                dtads.Rows[i]["2021"] = dtEstimate1.Rows[es]["EstimatedPrice"].ToString();
    //                            }
    //                        }
    //                    }
    //                }
    //                pgsource.DataSource = dtads.DefaultView;
    //                pgsource.AllowPaging = true;
    //                pgsource.PageSize = 100;
    //                pgsource.CurrentPageIndex = pagingCurrentPage;
    //                ViewState["totpage"] = pgsource.PageCount;
    //                lblpaging.Text = "Page " + (pagingCurrentPage + 1) + " of " + pgsource.PageCount;
    //                lnkbtnPgPrevious.Enabled = !pgsource.IsFirstPage;
    //                lnkbtnPgNext.Enabled = !pgsource.IsLastPage;
    //                pgsource.DataSource = dtads.DefaultView;
    //                gvproductItem.DataSource = pgsource;
    //                gvproductItem.DataBind();
    //                gvproductItem.Visible = true;
    //                lbltotalshowpageitem.Text = pgsource.FirstIndexInPage + 1 + " - " + (pgsource.FirstIndexInPage + pgsource.Count);
    //                divpageindex.Visible = true;
    //                divproductgridview.Visible = true;
    //            }
    //            else
    //            {
    //                gvproductItem.Visible = false;
    //                divpageindex.Visible = false;
    //                lbltotalshowpageitem.Text = "";
    //                divproductgridview.Visible = false;
    //            }
    //        }
    //        else
    //        {
    //            lbltotalshowpageitem.Text = "";
    //            gvproductItem.Visible = false;
    //            divpageindex.Visible = false;
    //            divproductgridview.Visible = false;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert(Something went wrong please refresh page. " + ex.Message + ")", true);

    //    }
    //}

    protected void gvproductremark_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Reply")
        {
            GridViewRow item = (GridViewRow)(((Control)(e.CommandSource)).NamingContainer);
            DataTable dtrequestreply = Lo.NewRetriveFilterCode("GetCompanyRemarks", "", "", "", "", Convert.ToInt32(e.CommandArgument.ToString()), 0, 0);
            if (dtrequestreply.Rows.Count > 0)
            {
                string reqid = dtrequestreply.Rows[0]["RequestID"].ToString();
                //  Label username = (Label)item.FindControl("RequestBy");
                txtusername.Text = dtrequestreply.Rows[0]["RequestBy"].ToString();
                string CompName = dtrequestreply.Rows[0]["RequestCompName"].ToString();
                // Label mobileno = (Label)item.FindControl("RequestMobileNo");
                //  Label address = (Label)item.FindControl("RequestAddress");
                //  Label email = (Label)item.FindControl("lblemail");
                txtemail.Text = dtrequestreply.Rows[0]["RequestEmail"].ToString();
                lblrefno.Text = dtrequestreply.Rows[0]["ProductRefNo"].ToString();
                txtremark.Text = dtrequestreply.Rows[0]["Remark"].ToString();

            }

            ScriptManager.RegisterStartupScript(this, GetType(), "reply", "showPopup2();", true);
        }
    }

    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        try
        {
            Lo.SaveCompRemarkReply(lblrefno.Text, txtusername.Text, txtemail.Text, txtremark.Text, txtreply.Text);
            SendEmail();
            Cleartext();
            BindProductRemark();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);
        }
    }

    public void SendEmail()
    {
        try
        {
            string body;
            using (StreamReader reader = new StreamReader(Server.MapPath("~/emailPage/CompanyRemarkReply.html")))
            {
                body = reader.ReadToEnd();
            }
             body = body.Replace("{Name}", txtusername.Text);
             body = body.Replace("{CompanyName}", txtremark.Text);
          //   body = body.Replace("{YourRemark}", txtremark.Text);
             body = body.Replace("{Reply}", txtreply.Text);

            SendMail s;
            s = new SendMail();
            s.CreateMail(objCrypto.DecryptData(Session["User"].ToString()),txtemail.Text, "Remark Revert", body);
            s.sendMail();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Send Successfully!!!')", true);

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);
        }
    }

    public void Cleartext()
    {
        txtusername.Text = "";
        txtemail.Text = "";
        txtremark.Text = "";
        txtreply.Text = "";
    }
}
