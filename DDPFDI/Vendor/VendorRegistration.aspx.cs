using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessLayer;
using Encryption;

public partial class Vendor_VendorRegistration : System.Web.UI.Page
{
    Logic Lo = new Logic();
    Cryptography Enc = new Cryptography();
    DataUtility Co = new DataUtility();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["User"] != null)
        {
            if (!IsPostBack)
            {
                BindTypeOfBusiness();
                Bindbusinesssector();
                CreateDateTableGrid1();
                CreateDateTableGrid2();
                CreateDateTableGrid3();
                CreateDateTableGrid45();
                CreateDateTableGridturn();
                DataTable DtGetRegisVendor = Lo.RetriveVendor(0, Enc.DecryptData(Session["CompanyRefNo"].ToString()), "", "RetriveData");
                if (DtGetRegisVendor.Rows.Count > 0)
                {
                    txtbusinessname.Text = DtGetRegisVendor.Rows[0]["BusinessName"].ToString();
                    ddltypeofbusiness.Items.FindByValue(DtGetRegisVendor.Rows[0]["TypeOfBuisness"].ToString()).Selected = true;
                    ddlbusinesssector.Items.FindByValue(DtGetRegisVendor.Rows[0]["BusinessSector"].ToString()).Selected = true;
                    txtaddress.Text = DtGetRegisVendor.Rows[0]["StreetAddress"].ToString();
                    txtaddress2.Text = DtGetRegisVendor.Rows[0]["StreetAddressLine2"].ToString();
                    txtcity2.Text = DtGetRegisVendor.Rows[0]["City"].ToString();
                    txtstate2.Text = DtGetRegisVendor.Rows[0]["State"].ToString();
                    txtpostalzipcode2.Text = DtGetRegisVendor.Rows[0]["ZipCode"].ToString();
                    txtemail.Text = DtGetRegisVendor.Rows[0]["NodalOfficerEmail"].ToString();
                    txtstdcode.Text = DtGetRegisVendor.Rows[0]["ContactNo"].ToString().Substring(0, 3);
                    string a = DtGetRegisVendor.Rows[0]["ContactNo"].ToString();
                    var result = a.Substring(a.IndexOf('-') + 1);
                    txtphoneno.Text = result.ToString();
                    txtfaxstdcode.Text = DtGetRegisVendor.Rows[0]["FaxNo"].ToString().Substring(0, 3);
                    string b = DtGetRegisVendor.Rows[0]["FaxNo"].ToString();
                    var result1 = b.Substring(b.IndexOf('-') + 1);
                    txtfaxphoneno.Text = result1.ToString();
                    txtgstin.Text = DtGetRegisVendor.Rows[0]["GSTNo"].ToString();
                    txtfirstname.Text = DtGetRegisVendor.Rows[0]["NodalOfficerFirstName"].ToString();
                    txtmiddlename.Text = DtGetRegisVendor.Rows[0]["NodalOfficerMiddleName"].ToString();
                    txtlastname.Text = DtGetRegisVendor.Rows[0]["NodalOfficerLastName"].ToString();
                }
            }
        }
        else
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert",
                   "alert('Session Expired,Please login again');window.location='VendorLogin'", true);
    }
    protected void BindTypeOfBusiness()
    {
        DataTable DtMasterCategroyBusiness = Lo.RetriveMasterData(1, "", "", 0, "", "", "TypeofBusiness");
        if (DtMasterCategroyBusiness.Rows.Count > 0)
        {
            Co.FillDropdownlist(ddltypeofbusiness, DtMasterCategroyBusiness, "VendorSubCatName", "VendorSubCatID");
            ddltypeofbusiness.Items.Insert(0, "Select");
        }
    }
    protected void Bindbusinesssector()
    {
        DataTable DtMasterCategroyBusiness = Lo.RetriveMasterData(2, "", "", 0, "", "", "BuisnessSector");
        if (DtMasterCategroyBusiness.Rows.Count > 0)
        {
            Co.FillDropdownlist(ddlbusinesssector, DtMasterCategroyBusiness, "VendorSubCatName", "VendorSubCatID");
            ddlbusinesssector.Items.Insert(0, "Select");
        }
    }
    protected void ddlenternameof_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlenternameof.SelectedItem.Text == "Other")
        { divdesignation.Visible = true; }
        else { divdesignation.Visible = false; }
    }
    protected void btnnext_Click(object sender, EventArgs e)
    {
        //panstep1.Visible = false;
        //panstep2.Visible = true;
        //CreateDateTableGrid1();
        //CreateDateTableGrid2();
        //CreateDateTableGrid3();
        //CreateDateTableGrid45();
    }
    protected void btnnext2_Click(object sender, EventArgs e)
    {
        //panstep1.Visible = false;
        //panstep2.Visible = false;
        //panstep3.Visible = true;
    }
    protected void btnback_Click(object sender, EventArgs e)
    {
        panstep1.Visible = true;
        panstep2.Visible = false;
    }
    protected void btnbackpanel3_Click(object sender, EventArgs e)
    {
        panstep1.Visible = false;
        panstep2.Visible = true;
        panstep3.Visible = false;
    }
    protected void btnNextpanel3_Click(object sender, EventArgs e)
    {
        //panstep1.Visible = false;
        //panstep2.Visible = false;
        //panstep3.Visible = false;
        //panstep4.Visible = true;
        //CreateDateTableGridturn();
    }
    protected void rbfinnyear_SelectedIndexChanged(object sender, EventArgs e)
    {
        for (int i = 0; i < rbfinnyear.Items.Count; i++)
        {
            if (rbfinnyear.Items[i].Value == "1" && rbfinnyear.Items[i].Selected == true)
            {
                div19.Visible = true;
            }
            else if (rbfinnyear.Items[i].Value == "2" && rbfinnyear.Items[i].Selected == true)
            {
                div18.Visible = true;
            }
            else if (rbfinnyear.Items[i].Value == "3" && rbfinnyear.Items[i].Selected == true)
            {
                div17.Visible = true;
            }
            else
            {
                if (rbfinnyear.Items[i].Value == "1" && rbfinnyear.Items[i].Selected == false)
                { div19.Visible = false; }
                else if (rbfinnyear.Items[i].Value == "2" && rbfinnyear.Items[i].Selected == false)
                { div18.Visible = false; }
                else if (rbfinnyear.Items[i].Value == "3" && rbfinnyear.Items[i].Selected == false)
                { div17.Visible = false; }
            }
        }
    }
    protected void btnpanel4back_Click(object sender, EventArgs e)
    {
        panstep4.Visible = false;
        panstep1.Visible = false;
        panstep2.Visible = false;
        panstep3.Visible = true;
    }
    protected void btnpanel4next_Click(object sender, EventArgs e)
    {
        //panstep4.Visible = false;
        //panstep1.Visible = false;
        //panstep2.Visible = false;
        //panstep3.Visible = false;
        //panstep5.Visible = true;
    }
    protected void btnbackpanel5_Click(object sender, EventArgs e)
    {
        panstep5.Visible = false;
        panstep4.Visible = true;
        panstep3.Visible = false;
        panstep2.Visible = false;
        panstep1.Visible = false;
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Form successfully submit.')", true);
    }
    protected void btnclear_Click(object sender, EventArgs e)
    {
        txtaddress.Text = "";
        txtaddress2.Text = "";
        txtbusinessname.Text = "";
        txtCIN.Text = "";
        txtcity.Text = "";
        txtcity2.Text = "";
        txtdateofincorofthecompany.Text = "";
        txtdesignation.Text = "";
        txtdinno.Text = "";
        txtfaxphoneno.Text = "";
        txtfaxstdcode.Text = "";
        txtfirstname.Text = "";
        txtgstin.Text = "";
        txtlastname.Text = "";
        txtmiddlename.Text = "";
        txtmobno.Text = "";
        txtnameofbank.Text = "";
        txtphoneno.Text = "";
        txtpostalzipcode.Text = "";
        txtpostalzipcode2.Text = "";
        txtregisterdofficeaddress.Text = "";
        txtstate2.Text = "";
        txtstateprovince.Text = "";
        txtstdcode.Text = "";
        txtstreetaddress.Text = "";
        txtstreetaddressline2.Text = "";
        txtUAM.Text = "";
        ddlbusinessdpsuofb.SelectedIndex = -1;
        ddlbusinesssector.SelectedIndex = -1;
        ddldebarredgovtcont.SelectedIndex = -1;
        ddlDepartmentUndertakingPSU.SelectedIndex = -1;
        ddldetailofpantan.SelectedIndex = -1;
        ddlenternameof.SelectedIndex = -1;
        ddlforgingreasone.SelectedIndex = -1;
        ddljudicialofficer.SelectedIndex = -1;
        ddlpertainingdate.SelectedIndex = -1;
        ddlregiscategory.SelectedIndex = -1;
        ddltypeofaccount.SelectedIndex = -1;
        ddltypeofbusiness.SelectedIndex = -1;
        ddlwoundedup.SelectedIndex = -1;
        rbfinnyear.SelectedIndex = -1;
    }
    protected void btnprint_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Printing.....')", true);
    }
    protected void ddltypeofaccount_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddltypeofaccount.SelectedItem.Text == "Select")
        { divbankdetail.Visible = false; }
        else
        { divbankdetail.Visible = true; }
    }
    protected void ddldetailofpantan_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddldetailofpantan.SelectedItem.Text == "Select")
        { divpantan.Visible = false; }
        else if (ddldetailofpantan.SelectedItem.Text == "PAN")
        { lblpantan.Text = "PAN"; divpantan.Visible = true; }
        else if (ddldetailofpantan.SelectedItem.Text == "TAN")
        { lblpantan.Text = "TAN"; divpantan.Visible = true; }
    }
    protected void ddlDepartmentUndertakingPSU_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDepartmentUndertakingPSU.SelectedItem.Text == "YES")
        { divgovtundertaking.Visible = true; }
        else
        { divgovtundertaking.Visible = false; }
    }
    protected void CreateDateTableGrid1()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("Product Nomenclature", typeof(string));
        dt.Columns.Add("Nato Group", typeof(string));
        dt.Columns.Add("Nato Class", typeof(string));
        dt.Columns.Add("Item Code", typeof(string));
        DataRow dr = dt.NewRow();
        dr["Product Nomenclature"] = "";
        dr["Nato Group"] = "";
        dr["Nato Class"] = "";
        dr["Item Code"] = "";
        dt.Rows.Add(dr);
        gvproddetail.DataSource = dt;
        gvproddetail.DataBind();
    }
    protected void CreateDateTableGrid2()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("Product Nomenclature", typeof(string));
        dt.Columns.Add("Technology 1", typeof(string));
        dt.Columns.Add("Technology 2", typeof(string));
        dt.Columns.Add("Technology 3", typeof(string));
        DataRow dr = dt.NewRow();
        dr["Product Nomenclature"] = "";
        dr["Technology 1"] = "";
        dr["Technology 2"] = "";
        dr["Technology 3"] = "";
        dt.Rows.Add(dr);
        gvtechnology.DataSource = dt;
        gvtechnology.DataBind();
    }
    protected void CreateDateTableGrid3()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("Items", typeof(string));
        dt.Columns.Add("Basic Raw Material", typeof(string));
        dt.Columns.Add("Source of material", typeof(string));
        dt.Columns.Add("Name of Major Raw Material Suppliers", typeof(string));
        DataRow dr = dt.NewRow();
        dr["Items"] = "";
        dr["Basic Raw Material"] = "";
        dr["Source of material"] = "";
        dr["Name of Major Raw Material Suppliers"] = "";
        dt.Rows.Add(dr);
        gvSourceofRawMaterial.DataSource = dt;
        gvSourceofRawMaterial.DataBind();
    }
    protected void CreateDateTableGrid45()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("Name of Reputed Customer", typeof(string));
        dt.Columns.Add("Description of Store Supplied", typeof(string));
        dt.Columns.Add("S.O. No.and Date", typeof(string));
        dt.Columns.Add("Order Qty.", typeof(string));
        dt.Columns.Add("Value Qty Supplied", typeof(string));
        dt.Columns.Add("Date of Last Supply", typeof(string));
        DataRow dr = dt.NewRow();
        dr["Name of Reputed Customer"] = "";
        dr["Description of Store Supplied"] = "";
        dr["S.O. No.and Date"] = "";
        dr["Order Qty."] = "";
        dr["Value Qty Supplied"] = "";
        dr["Date of Last Supply"] = "";
        dt.Rows.Add(dr);
        gvItemProducedandSupplied.DataSource = dt;
        gvItemProducedandSupplied.DataBind();
        gvItemSuppliedbutnotproduced.DataSource = dt;
        gvItemSuppliedbutnotproduced.DataBind();
    }
    protected void CreateDateTableGridturn()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("Financial Year", typeof(string));
        dt.Columns.Add("Value of Current Assets", typeof(string));
        dt.Columns.Add("Value of Current Liabilites", typeof(string));
        dt.Columns.Add("Total Profit/Loss", typeof(string));
        dt.Columns.Add("Upload Sheet", typeof(string));
        DataRow dr = dt.NewRow();
        dr["Financial Year"] = "";
        dr["Value of Current Assets"] = "";
        dr["Value of Current Liabilites"] = "";
        dr["Total Profit/Loss"] = "";
        dr["Upload Sheet"] = "";
        dt.Rows.Add(dr);
        gvTURNOVERDURINGLAST3YEARS.DataSource = dt;
        gvTURNOVERDURINGLAST3YEARS.DataBind();
    }

}