using BusinessLayer;
using Encryption;
using System;
using System.Collections.Specialized;
using System.Data;
using System.Drawing;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_ViewDashboard : System.Web.UI.Page
{
    private Logic Lo = new Logic();
    private DataTable DtGrid = new DataTable();
    private HybridDictionary hySaveProdInfo = new HybridDictionary();
    private string _msg = string.Empty;
    private string _sysMsg = string.Empty;
    DataUtility Co = new DataUtility();
    private Cryptography Encrypt = new Cryptography();
    private PagedDataSource pgsource = new PagedDataSource();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["User"] != null)
            {
                if (Request.QueryString["id"] != null)
                {
                    if (Encrypt.DecryptData(Session["Type"].ToString()) == "Admin" || Encrypt.DecryptData(Session["Type"].ToString()) == "SuperAdmin")
                    {
                        if (Request.QueryString["strangone"] != null)
                        {
                            ControlGrid(Encrypt.DecryptData(Request.QueryString["id"].ToString()), Encrypt.DecryptData(Request.QueryString["strangone"].ToString()));
                        }
                        else
                        {
                            ControlGrid(Encrypt.DecryptData(Request.QueryString["id"].ToString()), "");
                        }
                    }
                    else
                    {
                        ControlGrid(Encrypt.DecryptData(Request.QueryString["id"].ToString()), Session["CompanyRefNo"].ToString());
                    }
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "ErrorMssgPopup('Session Expired,Please login again');window.location='Login'", true);
            }
        }
    }
    private void ControlGrid(string mVal, string RefNo)     
    {
        hfmtype.Value = mVal.ToString();
        hfmref.Value = RefNo.ToString();
        if (hfmtype.Value == "C")
        {
            gvcompanydetail.Visible = true;
            gvfactory.Visible = false;
            gvunit.Visible = false;
            gvViewNodalOfficer.Visible = false;
            BindCompany(hfmref.Value);
        }
        else if (hfmtype.Value == "D")
        {
            gvcompanydetail.Visible = false;
            gvfactory.Visible = true;
            gvunit.Visible = false;
            gvViewNodalOfficer.Visible = false;
            BindDivision(hfmref.Value);
        }
        else if (hfmtype.Value == "U")
        {
            gvcompanydetail.Visible = false;
            gvfactory.Visible = false;
            gvunit.Visible = true;
            gvViewNodalOfficer.Visible = false;
            BindUnit(hfmref.Value);
        }
        else if (hfmtype.Value == "E")
        {
            gvcompanydetail.Visible = false;
            gvfactory.Visible = false;
            gvunit.Visible = false;
            gvViewNodalOfficer.Visible = true;
            BindEmployee(hfmref.Value);
        }       
        else
        {
            BindCompany(hfmref.Value);
            BindDivision(hfmref.Value);
            BindUnit(hfmref.Value);
            BindEmployee(hfmref.Value);
        }
    }
    protected void BindCompany(string RefNo)
    {
        DtGrid = Lo.GetDashboardData("Company", txtsearch.Text.Trim());
        if (DtGrid.Rows.Count > 0)
        {
            if (hfmref.Value != "")
            {
                DataView dv = new DataView(DtGrid);
                //code to filter role wise 
                DataTable dtParentNode = Lo.RetriveParentNode(Encrypt.DecryptData(Session["Type"].ToString()).ToUpper(), hfmref.Value);
                if (Encrypt.DecryptData(Session["Type"].ToString()).ToUpper() == "COMPANY")
                {
                    dv.RowFilter = "CompanyRefNo='" + hfmref.Value + "'";
                }
                else if (Encrypt.DecryptData(Session["Type"].ToString()).ToUpper() == "DIVISION")
                {
                    dv.RowFilter = "CompanyRefNo='" + dtParentNode.Rows[0]["CompanyRefNo"].ToString() + "'";
                }
                else
                {
                    dv.RowFilter = "CompanyRefNo='" + dtParentNode.Rows[0]["CompanyRefNo"].ToString() + "'";
                }

                DataTable dtads = dv.ToTable();
                pgsource.DataSource = dtads.DefaultView;
                pgsource.AllowPaging = true;
                pgsource.PageSize = 100;
                pgsource.CurrentPageIndex = pagingCurrentPage;
                ViewState["totpage"] = pgsource.PageCount;
                lblpaging.Text = "Page " + (pagingCurrentPage + 1) + " of " + pgsource.PageCount;
                lnkbtnPgPrevious.Enabled = !pgsource.IsFirstPage;
                lnkbtnPgNext.Enabled = !pgsource.IsLastPage;
                pgsource.DataSource = dtads.DefaultView;
                gvcompanydetail.DataSource = pgsource;
                gvcompanydetail.DataBind();
                divpageindex.Visible = true;
                lbltotal.Text = "Showing  " + gvcompanydetail.Rows.Count.ToString() + " result from page " + (pagingCurrentPage + 1) + " out of " + pgsource.PageCount + " pages";
                divcompanyGrid.Visible = true;
            }
            else
            {
                DataTable dtads = DtGrid;
                pgsource.DataSource = dtads.DefaultView;
                pgsource.AllowPaging = true;
                pgsource.PageSize = 100;
                pgsource.CurrentPageIndex = pagingCurrentPage;
                ViewState["totpage"] = pgsource.PageCount;
                lblpaging.Text = "Page " + (pagingCurrentPage + 1) + " of " + pgsource.PageCount;
                lnkbtnPgPrevious.Enabled = !pgsource.IsFirstPage;
                lnkbtnPgNext.Enabled = !pgsource.IsLastPage;
                pgsource.DataSource = dtads.DefaultView;
                gvcompanydetail.DataSource = pgsource;
                gvcompanydetail.DataBind();
                divpageindex.Visible = true;
                lbltotal.Text = "Showing  " + gvcompanydetail.Rows.Count.ToString() + " result from page " + (pagingCurrentPage + 1) + " out of " + pgsource.PageCount + " pages";
                divcompanyGrid.Visible = true;
            }
        }
        else
        {
            divcompanyGrid.Visible = false;
        }
    }
    protected void BindDivision(string RefNo)
    {
        DtGrid = Lo.GetDashboardData("Division", txtsearch.Text.Trim());
        if (DtGrid.Rows.Count > 0)
        {
            if (hfmref.Value != "")
            {
                DataView dv = new DataView(DtGrid);
                //code to filter role wise 
                DataTable dtParentNode = Lo.RetriveParentNode(Encrypt.DecryptData(Session["Type"].ToString()).ToUpper(), hfmref.Value);
                if (Encrypt.DecryptData(Session["Type"].ToString()).ToUpper() == "COMPANY")
                {
                    dv.RowFilter = "CompanyRefNo='" + hfmref.Value + "'";
                }
                else if (Encrypt.DecryptData(Session["Type"].ToString()).ToUpper() == "DIVISION")
                {
                    dv.RowFilter = "FactoryRefNo='" + dtParentNode.Rows[0]["FactoryRefNo"].ToString() + "'";
                }
                else
                {
                    dv.RowFilter = "UnitRefNo='" + dtParentNode.Rows[0]["UnitRefNo"].ToString() + "'";
                }

                DataTable dtads = dv.ToTable();
                pgsource.DataSource = dtads.DefaultView;
                pgsource.AllowPaging = true;
                pgsource.PageSize = 100;
                pgsource.CurrentPageIndex = pagingCurrentPage;
                ViewState["totpage"] = pgsource.PageCount;
                lblpaging.Text = "Page " + (pagingCurrentPage + 1) + " of " + pgsource.PageCount;
                lnkbtnPgPrevious.Enabled = !pgsource.IsFirstPage;
                lnkbtnPgNext.Enabled = !pgsource.IsLastPage;
                pgsource.DataSource = dtads.DefaultView;
                gvfactory.DataSource = pgsource;
                gvfactory.DataBind();
                divpageindex.Visible = true;
                lbltotal.Text = "Showing  " + gvfactory.Rows.Count.ToString() + " result from page " + (pagingCurrentPage + 1) + " out of " + pgsource.PageCount + " pages";
                divfactorygrid.Visible = true;
            }
            else
            {
                DataTable dtads = DtGrid;
                pgsource.DataSource = dtads.DefaultView;
                pgsource.AllowPaging = true;
                pgsource.PageSize = 100;
                pgsource.CurrentPageIndex = pagingCurrentPage;
                ViewState["totpage"] = pgsource.PageCount;
                lblpaging.Text = "Page " + (pagingCurrentPage + 1) + " of " + pgsource.PageCount;
                lnkbtnPgPrevious.Enabled = !pgsource.IsFirstPage;
                lnkbtnPgNext.Enabled = !pgsource.IsLastPage;
                pgsource.DataSource = dtads.DefaultView;
                gvfactory.DataSource = pgsource;
                gvfactory.DataBind();
                divpageindex.Visible = true;
                lbltotal.Text = "Showing  " + gvfactory.Rows.Count.ToString() + " result from page " + (pagingCurrentPage + 1) + " out of " + pgsource.PageCount + " pages";
                divfactorygrid.Visible = true;
            }
        }
        else
        {
            divfactorygrid.Visible = true;
        }
    }
    protected void BindUnit(string RefNo)
    {
        DtGrid = Lo.GetDashboardData("Unit", txtsearch.Text.Trim());
        if (DtGrid.Rows.Count > 0)
        {
            if (hfmref.Value != "")
            {
                DataView dv = new DataView(DtGrid);
                //code to filter role wise 
                DataTable dtParentNode = Lo.RetriveParentNode(Encrypt.DecryptData(Session["Type"].ToString()).ToUpper(), hfmref.Value);
                if (Encrypt.DecryptData(Session["Type"].ToString()).ToUpper() == "COMPANY")
                {
                    dv.RowFilter = "CompanyRefNo='" + hfmref.Value + "'";
                }
                else if (Encrypt.DecryptData(Session["Type"].ToString()).ToUpper() == "DIVISION")
                {
                    dv.RowFilter = "FactoryRefNo='" + dtParentNode.Rows[0]["FactoryRefNo"].ToString() + "'";
                }
                else
                {
                    dv.RowFilter = "UnitRefNo='" + dtParentNode.Rows[0]["UnitRefNo"].ToString() + "'";
                }

                DataTable dtads = dv.ToTable();
                pgsource.DataSource = dtads.DefaultView;
                pgsource.AllowPaging = true;
                pgsource.PageSize = 100;
                pgsource.CurrentPageIndex = pagingCurrentPage;
                ViewState["totpage"] = pgsource.PageCount;
                lblpaging.Text = "Page " + (pagingCurrentPage + 1) + " of " + pgsource.PageCount;
                lnkbtnPgPrevious.Enabled = !pgsource.IsFirstPage;
                lnkbtnPgNext.Enabled = !pgsource.IsLastPage;
                pgsource.DataSource = dtads.DefaultView;
                gvunit.DataSource = pgsource;
                gvunit.DataBind();
                divpageindex.Visible = true;
                lbltotal.Text = "Showing  " + gvunit.Rows.Count.ToString() + " result from page " + (pagingCurrentPage + 1) + " out of " + pgsource.PageCount + " pages";
                divunitGrid.Visible = true;
            }
            else
            {
                DataTable dtads = DtGrid;
                pgsource.DataSource = dtads.DefaultView;
                pgsource.AllowPaging = true;
                pgsource.PageSize = 100;
                pgsource.CurrentPageIndex = pagingCurrentPage;
                ViewState["totpage"] = pgsource.PageCount;
                lblpaging.Text = "Page " + (pagingCurrentPage + 1) + " of " + pgsource.PageCount;
                lnkbtnPgPrevious.Enabled = !pgsource.IsFirstPage;
                lnkbtnPgNext.Enabled = !pgsource.IsLastPage;
                pgsource.DataSource = dtads.DefaultView;
                gvunit.DataSource = pgsource;
                gvunit.DataBind();
                divpageindex.Visible = true;
                lbltotal.Text = "Showing  " + gvunit.Rows.Count.ToString() + " result from page " + (pagingCurrentPage + 1) + " out of " + pgsource.PageCount + " pages";
                divunitGrid.Visible = true;
            }
        }
        else
        {
            divunitGrid.Visible = true;
        }
    }
    protected void BindEmployee(string RefNo)
    {
        DtGrid = Lo.GetDashboardData("Employee", txtsearch.Text.Trim());
        if (DtGrid.Rows.Count > 0)
        {
            UpdateDtGridValue();
            if (hfmref.Value != "")
            {
                DataView dv = new DataView(DtGrid);
                // code to filter row role wise
                if (Encrypt.DecryptData(Session["Type"].ToString()).ToUpper() == "COMPANY")
                {
                    dv.RowFilter = "CompanyRefNo='" + hfmref.Value + "'";
                }
                else if (Encrypt.DecryptData(Session["Type"].ToString()).ToUpper() == "DIVISION")
                {
                    dv.RowFilter = "FactoryRefNo='" + hfmref.Value + "'";
                }
                else
                {
                    dv.RowFilter = "UnitRefNo='" + hfmref.Value + "'";
                }

                dv.Sort = "CompanyName asc,FactoryName asc";

                DataTable dtads = dv.ToTable();
                pgsource.DataSource = dtads.DefaultView;
                pgsource.AllowPaging = true;
                pgsource.PageSize = 100;
                pgsource.CurrentPageIndex = pagingCurrentPage;
                ViewState["totpage"] = pgsource.PageCount;
                lblpaging.Text = "Page " + (pagingCurrentPage + 1) + " of " + pgsource.PageCount;
                lnkbtnPgPrevious.Enabled = !pgsource.IsFirstPage;
                lnkbtnPgNext.Enabled = !pgsource.IsLastPage;
                pgsource.DataSource = dtads.DefaultView;
                gvViewNodalOfficer.DataSource = pgsource;
                gvViewNodalOfficer.DataBind();
                divpageindex.Visible = true;
                lbltotal.Text = "Showing  " + gvViewNodalOfficer.Rows.Count.ToString() + " result from page " + (pagingCurrentPage + 1) + " out of " + pgsource.PageCount + " pages";
                divEmployeeNodalGrid.Visible = true;
            }
            else
            {
                DataView dv = new DataView(DtGrid)
                {
                    Sort = "CompanyName asc,FactoryName asc"
                };

                DataTable dtads = dv.ToTable();
                pgsource.DataSource = dtads.DefaultView;
                pgsource.AllowPaging = true;
                pgsource.PageSize = 100;
                pgsource.CurrentPageIndex = pagingCurrentPage;
                ViewState["totpage"] = pgsource.PageCount;
                lblpaging.Text = "Page " + (pagingCurrentPage + 1) + " of " + pgsource.PageCount;
                lnkbtnPgPrevious.Enabled = !pgsource.IsFirstPage;
                lnkbtnPgNext.Enabled = !pgsource.IsLastPage;
                pgsource.DataSource = dtads.DefaultView;
                gvViewNodalOfficer.DataSource = pgsource;
                gvViewNodalOfficer.DataBind();
                divpageindex.Visible = true;
                lbltotal.Text = "Showing  " + gvViewNodalOfficer.Rows.Count.ToString() + " result from page " + (pagingCurrentPage + 1) + " out of " + pgsource.PageCount + " pages";
                divEmployeeNodalGrid.Visible = true;
            }
        }
        else
        {
            divEmployeeNodalGrid.Visible = false;
        }
    }
    #region RowCommand
    protected void gvcompanydetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        DataTable DtView = Lo.RetriveGridViewCompany(e.CommandArgument.ToString(), "", "", "CompanyMainGridView");
        if (DtView.Rows.Count > 0)
        {
            lblrefno.Text = DtView.Rows[0]["CompanyRefNo"].ToString();
            lblcompanyname.Text = DtView.Rows[0]["CompanyName"].ToString();
            lbladdress.Text = DtView.Rows[0]["Address"].ToString();
            lblstate.Text = DtView.Rows[0]["StateName"].ToString();
            lblpanno.Text = DtView.Rows[0]["PANNo"].ToString();
            lblpincode.Text = DtView.Rows[0]["Pincode"].ToString();
            lblcinno.Text = DtView.Rows[0]["CINNo"].ToString();
            lblceoname.Text = DtView.Rows[0]["CEOName"].ToString();
            lblceoemail.Text = DtView.Rows[0]["CEOEmail"].ToString();
            lblTelephoneNo.Text = DtView.Rows[0]["TelephoneNo"].ToString();
            lblFaxNo.Text = DtView.Rows[0]["FaxNo"].ToString();
            lblEmailID.Text = DtView.Rows[0]["EmailID"].ToString();
            lblWebsite.Text = DtView.Rows[0]["Website"].ToString();
            lblGSTNo.Text = DtView.Rows[0]["GSTNo"].ToString();
            lblNodalEmail.Text = DtView.Rows[0]["ContactPersonEmailID"].ToString();
            lblNodalOfficerName.Text = DtView.Rows[0]["NodalOficerName"].ToString();
            lblAad_Mobile.Text = DtView.Rows[0]["latitude"].ToString();
            lblLongitude.Text = DtView.Rows[0]["longitude"].ToString();
            lblFacebook.Text = DtView.Rows[0]["Facebook"].ToString();
            lblInstagram.Text = DtView.Rows[0]["Instagram"].ToString();
            lblTwitter.Text = DtView.Rows[0]["Twitter"].ToString();
            lblLinkedin.Text = DtView.Rows[0]["Linkedin"].ToString();
            ScriptManager.RegisterStartupScript(this, GetType(), "divCompany", "showPopup();", true);
        }
    }
    protected void gvfactory_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        DataTable DtView = Lo.RetriveGridViewCompany("", e.CommandArgument.ToString(), "", "DisplayFactory");
        if (DtView.Rows.Count > 0)
        {
            lblfacrefno.Text = DtView.Rows[0]["FactoryRefNo"].ToString();
            lblDivName.Text = DtView.Rows[0]["FactoryName"].ToString();
            lblDivAddress.Text = DtView.Rows[0]["FactoryAddress"].ToString();
            lblDivState.Text = DtView.Rows[0]["StateName"].ToString();
            lblDivPincode.Text = DtView.Rows[0]["FactoryPincode"].ToString();
            lblDivCeoName.Text = DtView.Rows[0]["FactoryCEOName"].ToString();
            lblDivCeoEmail.Text = DtView.Rows[0]["FactoryCEOEmail"].ToString();
            lblDivNodalName.Text = DtView.Rows[0]["NodalOficerName"].ToString();
            lblDivNodalEMail.Text = DtView.Rows[0]["NodalOfficerEmail"].ToString();
            lblDivConNo.Text = DtView.Rows[0]["FactoryTelephoneNo"].ToString();
            lblDivFax.Text = DtView.Rows[0]["FactoryFaxNo"].ToString();
            lblDivlatitude.Text = DtView.Rows[0]["Factorylatitude"].ToString();
            lblDivLongitude.Text = DtView.Rows[0]["Factorylongitude"].ToString();
            ScriptManager.RegisterStartupScript(this, GetType(), "divfactoryshow", "showPopup1();", true);
        }

    }
    protected void gvunit_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        DataTable DtView = Lo.RetriveGridViewCompany("", "", e.CommandArgument.ToString(), "GVUnitID");
        if (DtView.Rows.Count > 0)
        {
            lblurefno.Text = DtView.Rows[0]["UnitRefNo"].ToString();
            lblUnitName.Text = DtView.Rows[0]["UnitName"].ToString();
            lblUnitAddress.Text = DtView.Rows[0]["UnitAddress"].ToString();
            lblUnitState.Text = DtView.Rows[0]["StateName"].ToString();
            lblUnitPin.Text = DtView.Rows[0]["UnitPincode"].ToString();
            lblUnitCeoName.Text = DtView.Rows[0]["UnitCEOName"].ToString();
            lblUnitCeoEmail.Text = DtView.Rows[0]["UnitCEOEmail"].ToString();
            lblUnitNodalName.Text = DtView.Rows[0]["NodalOficerName"].ToString();
            lblUnitNodalEmail.Text = DtView.Rows[0]["NodalOfficerEmail"].ToString();
            ScriptManager.RegisterStartupScript(this, GetType(), "divunitshow", "showPopup2();", true);
        }
    }
    protected void gvViewNodalOfficer_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
        int rowIndex = gvr.RowIndex;
        string Role = (gvViewNodalOfficer.Rows[rowIndex].FindControl("hfnodalrole") as HiddenField).Value;
        if (Role == "Unit")
        {
            Role = "UnitID";
        }
        else if (Role == "Division" || Role == "Factory")
        {
            Role = "DivisionID";
        }
        else if (Role == "Company")
        {
            Role = "CompanyID";
        }
        DataTable DtView = new DataTable();
        DtView = Lo.RetriveAllNodalOfficer(e.CommandArgument.ToString(), Role);
        if (DtView.Rows.Count > 0)
        {

            lblNodalComp.Text = DtView.Rows[0]["CompanyName"].ToString();
            if (Role == "CompanyID")
            {
                lblDivision.Text = "";
            }
            else
            {
                lblDivision.Text = DtView.Rows[0]["FactoryName"].ToString();
            }
            if (Role == "DivisionID")
            {
                lblUnit.Text = "";
            }
            else
            {
                lblUnit.Text = DtView.Rows[0]["UnitName"].ToString();
            }
            lblempNodalOfficerRefNo.Text = DtView.Rows[0]["NodalOfficerRefNo"].ToString();
            lblNodalOficerName.Text = DtView.Rows[0]["NodalOficerName"].ToString();
            lblempNodalEmpCode.Text = DtView.Rows[0]["NodalEmpCode"].ToString();
            lblEmail.Text = DtView.Rows[0]["NodalOfficerEmail"].ToString();
            lblMobile.Text = DtView.Rows[0]["NodalOfficerMobile"].ToString();
            lblTelephone.Text = DtView.Rows[0]["NodalOfficerTelephone"].ToString();
            lblFax.Text = DtView.Rows[0]["NodalOfficerFax"].ToString();
            ScriptManager.RegisterStartupScript(this, GetType(), "ViewNodalDetail", "showPopup3();", true);
        }
    }
    #endregion
    #region  Other Code
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
            }
            else if (s_lblnodallogactive.Text == "Y")
            {
                s_lblnodallogactive.Text = "User";
                s_lblnodallogactive.Visible = true;
            }
            else if (s_lblnodallogactive.Text == "N" && s_lblnodalofficer.Text == "N")
            {
                s_lblnodalofficer.Text = "Employee";
                s_lblnodalofficer.Visible = true;
            }
        }
    }
    protected void btnseach_Click(object sender, EventArgs e)
    {
        if (Encrypt.DecryptData(Session["Type"].ToString()) == "Admin" || Encrypt.DecryptData(Session["Type"].ToString()) == "SuperAdmin")
        {
            ControlGrid(Encrypt.DecryptData(Request.QueryString["id"].ToString()), "");
        }
        else
        {
            ControlGrid(Encrypt.DecryptData(Request.QueryString["id"].ToString()), Session["CompanyRefNo"].ToString());
        }
    }
    protected void UpdateDtGridValue()
    {
        for (int a = 0; a < DtGrid.Rows.Count; a++)
        {
            if (DtGrid.Rows[a]["UCompany"].ToString() != "")
            {
                DtGrid.Rows[a]["CompanyName"] = DtGrid.Rows[a]["UCompany"];
                DtGrid.Rows[a]["FactoryName"] = DtGrid.Rows[a]["UFactory"];
                DtGrid.Rows[a]["CompanyRefNo"] = DtGrid.Rows[a]["UCompRefNo"];
                DtGrid.Rows[a]["FactoryRefNo"] = DtGrid.Rows[a]["UFactoryRefNo"];
            }
            else if (DtGrid.Rows[a]["FCompany"].ToString() != "")
            {
                DtGrid.Rows[a]["CompanyName"] = DtGrid.Rows[a]["FCompany"];
                DtGrid.Rows[a]["CompanyRefNo"] = DtGrid.Rows[a]["FCompRefNo"];
            }
        }
    }
    protected void gvcompanydetail_RowCreated(object sender, GridViewRowEventArgs e)
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
    protected void gvfactory_RowCreated(object sender, GridViewRowEventArgs e)
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
    protected void gvunit_RowCreated(object sender, GridViewRowEventArgs e)
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
    protected void gvViewNodalOfficer_RowCreated(object sender, GridViewRowEventArgs e)
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
    #endregion
    #region //------------------------pageindex code--------------//
    protected void lnkbtnPgPrevious_Click(object sender, EventArgs e)
    {
        txtpageno.Text = "";
        pagingCurrentPage -= 1;
        if (hfmtype.Value == "C")
        {
            BindCompany(hfmref.Value);
        }
        else if (hfmtype.Value == "D")
        {
            BindDivision(hfmref.Value);
        }
        else if (hfmtype.Value == "U")
        {
            BindUnit(hfmref.Value);
        }
        else if (hfmtype.Value == "E")
        {
            BindEmployee(hfmref.Value);
        }
    }
    protected void lnkbtnPgNext_Click(object sender, EventArgs e)
    {
        pagingCurrentPage += 1;
        int txtpage = Convert.ToInt32(pagingCurrentPage) + 1;
        txtpageno.Text = txtpage.ToString();
        if (hfmtype.Value == "C")
        {
            BindCompany(hfmref.Value);
        }
        else if (hfmtype.Value == "D")
        {
            BindDivision(hfmref.Value);
        }
        else if (hfmtype.Value == "U")
        {
            BindUnit(hfmref.Value);
        }
        else if (hfmtype.Value == "E")
        {
            BindEmployee(hfmref.Value);
        }
    }
    private int pagingCurrentPage
    {
        get
        {
            if (ViewState["pagingCurrentPage"] == null)
            {
                return 0;
            }
            else
            {
                return ((int)ViewState["pagingCurrentPage"]);
            }
        }
        set
        {
            ViewState["pagingCurrentPage"] = value;
        }
    }
    protected void btngoto_Click(object sender, EventArgs e)
    {
        if (System.Text.RegularExpressions.Regex.IsMatch(txtpageno.Text, "[^0-9]"))
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('Please enter only number')", true);
        }
        else
        {
            int txtpage = Convert.ToInt32(txtpageno.Text) - 1;
            pagingCurrentPage = Convert.ToInt32(txtpage.ToString());
            if (hfmtype.Value == "C")
            {
                BindCompany(hfmref.Value);
            }
            else if (hfmtype.Value == "D")
            {
                BindDivision(hfmref.Value);
            }
            else if (hfmtype.Value == "U")
            {
                BindUnit(hfmref.Value);
            }
            else if (hfmtype.Value == "E")
            {
                BindEmployee(hfmref.Value);
            }
        }
    }
    //end page index---------------------------------------//
    #endregion
    protected void lblback_Click(object sender, EventArgs e)
    {
        Response.Redirect("Dashboard?mu=" + Encrypt.EncryptData(Session["Type"].ToString()) + "&id=" + Encrypt.EncryptData(Session["CompanyRefNo"].ToString()));
    }
}