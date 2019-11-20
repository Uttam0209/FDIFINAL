using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using Encryption;
using System.Data;

public partial class Vendor_ViewVendorRegistrationDetail : System.Web.UI.Page
{
    Logic Lo = new Logic();
    Cryptography Enc = new Cryptography();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindGrid();
        }
    }
    protected void BindGrid()
    {
        DataTable DtGrid = Lo.RetriveVendor(0, "", "", "RetVendorDetails");
        if (DtGrid.Rows.Count > 0)
        {
            gvVendorDetails.DataSource = DtGrid;
            gvVendorDetails.DataBind();
            lbltotalcount.Text = "Total Record:-" + DtGrid.Rows.Count.ToString();
            gvVendorDetails.Visible = true;
        }
        else
        { lbltotalcount.Text = "Total Record:-" + DtGrid.Rows.Count.ToString(); gvVendorDetails.Visible = false; }
    }
    protected void gvVendorDetails_RowCreated(object sender, GridViewRowEventArgs e)
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
    protected void gvVendorDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "viewdetails")
        {
            DataTable DtGrid = Lo.RetriveVendor(Convert.ToInt64(e.CommandArgument.ToString()), "", "", "RetVendorDetailsbyID");
            if (DtGrid.Rows.Count > 0)
            {
                lbladdress.Text = DtGrid.Rows[0]["Street_Address"].ToString() + " " + DtGrid.Rows[0]["Street_Address_Line_2"].ToString();
                lblbuissector.Text = DtGrid.Rows[0]["BusinessSecName"].ToString();
                lblcategory.Text = DtGrid.Rows[0]["RegistrationCategory"].ToString();
                lblcity.Text = DtGrid.Rows[0]["City"].ToString();
                lblcompane.Text = DtGrid.Rows[0]["FirmCompanyName"].ToString();
                if (DtGrid.Rows[0]["Date_Incorportaion_Company"].ToString() != "")
                {
                    DateTime date = Convert.ToDateTime(DtGrid.Rows[0]["Date_Incorportaion_Company"].ToString());
                    string m = date.ToString("dd-MM-yyyy");
                    lblDate_Incorportaion_Company.Text = m.ToString();
                }
                lblemail.Text = DtGrid.Rows[0]["Email"].ToString();
                lblname.Text = DtGrid.Rows[0]["FirstName"].ToString() + " " + DtGrid.Rows[0]["MiddleName"].ToString() + " " + DtGrid.Rows[0]["LastName"].ToString();
                lblownership.Text = DtGrid.Rows[0]["OwnerShip"].ToString();
                if (lblownership.Text == "MSME")
                {
                    lblscaleofbuisness.Text = DtGrid.Rows[0]["ScaleofBuisness"].ToString();
                    lblh_ownership.Text = DtGrid.Rows[0]["H_OwnerShip"].ToString();
                    lblper.Text = DtGrid.Rows[0]["PercentofOwnership"].ToString();
                    lbldocu.Text = "http://srijandefence.in/upload/" + DtGrid.Rows[0]["FileofOwnership"].ToString();
                    divown.Visible = true;
                }
                lblpincode.Text = DtGrid.Rows[0]["Pincode"].ToString();
                lblrefno.Text = DtGrid.Rows[0]["VendorRefNo"].ToString();
                lblstate.Text = DtGrid.Rows[0]["State"].ToString();
                lblfaxno.Text = DtGrid.Rows[0]["FaxNo"].ToString();
                lblcontactno.Text = DtGrid.Rows[0]["ContactNo"].ToString();
                DataTable DtGridBind = Lo.RetriveVendor(Convert.ToInt64(e.CommandArgument.ToString()), "", "", "RetVendorMulGrid1");
                if (DtGridBind.Rows.Count > 0)
                {
                    gvnameof.DataSource = DtGridBind;
                    gvnameof.DataBind();
                }
                else
                { gvnameof.Visible = false; }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "VendorDetail", "showPopup();", true);
            }
        }

    }
}