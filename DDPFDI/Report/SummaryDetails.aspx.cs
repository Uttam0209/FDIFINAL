using BusinessLayer;
using Encryption;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Report_SummaryDetails : System.Web.UI.Page
{
    #region Pagevariable
    private Logic Lo = new Logic();
    DataUtility Co = new DataUtility();
    Cryptography objEnc = new Cryptography();
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindSummery();
        }
    }
    protected void BindSummery()
    {
        try
        {
            DataTable DtGrid = new DataTable();
            if (objEnc.DecryptData(Session["Type"].ToString()) == "Admin" || objEnc.DecryptData(Session["Type"].ToString()) == "SuperAdmin")
            {
                DtGrid = Lo.RetriveSummery("Admin", "");
            }
            else if (objEnc.DecryptData(Session["Type"].ToString()) == "Company")
            {
                DtGrid = Lo.RetriveSummery("CompanyRefNo", Session["CompanyRefNo"].ToString());
            }
            else if (objEnc.DecryptData(Session["Type"].ToString()) == "Factory" || objEnc.DecryptData(Session["Type"].ToString()) == "Division")
            {
                DtGrid = Lo.RetriveSummery("FactoryRefNo", Session["CompanyRefNo"].ToString());
            }
            else if (objEnc.DecryptData(Session["Type"].ToString()) == "Unit")
            {
                DtGrid = Lo.RetriveSummery("UnitRefNo", Session["CompanyRefNo"].ToString());
            }
            if (DtGrid.Rows.Count > 0)
            {
                DataView dv = new DataView(DtGrid);
                if (ddlmonth.SelectedValue != "0")
                {
                    dv.RowFilter = "MntName='" + ddlmonth.SelectedItem.Text + "'";
                    DtGrid = dv.ToTable();
                }
                if (rbyear.SelectedItem.Text != "Select")
                {
                    dv.RowFilter = "MYear='" + rbyear.SelectedItem.Value + "'";
                    DtGrid = dv.ToTable();
                }
                gv_summary.DataSource = DtGrid;
                gv_summary.DataBind();
                gv_summary.FooterRow.Cells[0].Text = "Total";
                gv_summary.FooterRow.Cells[0].HorizontalAlign = HorizontalAlign.Center;
                object sumObjectn = DtGrid.Compute("Sum(TotalProd)", string.Empty);
                gv_summary.FooterRow.Cells[2].Text = sumObjectn.ToString();
                //1
                object sumObjectn1 = DtGrid.Compute("Sum(MakeII)", string.Empty);
                gv_summary.FooterRow.Cells[3].Text = sumObjectn1.ToString();
                //2
                object sumObjectn2 = DtGrid.Compute("Sum(OtherThenMakeII)", string.Empty);
                gv_summary.FooterRow.Cells[4].Text = sumObjectn2.ToString();
                //2
                object sumObjectn3 = DtGrid.Compute("Sum(InHouse)", string.Empty);
                gv_summary.FooterRow.Cells[5].Text = sumObjectn3.ToString();
                //2
                object sumObjectn4 = DtGrid.Compute("Sum(Yettobe)", string.Empty);
                gv_summary.FooterRow.Cells[6].Text = sumObjectn4.ToString();
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('No Record Found')", true);
            }
        }
        catch (Exception ex)
        { ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert(" + ex.Message + ")", true); }
    }
    protected void ddlmonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindSummery();
    }
    protected void rbyear_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindSummery();
    }
}