using System;
using System.Collections.Generic;
using BusinessLayer;
using Encryption;
using System.Collections.Specialized;
using System.Data;
using System.Text;
using System.Web.UI;
using System.IO;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using context = System.Web.HttpContext;

public partial class User_TargetValue2021 : System.Web.UI.Page
{
    #region Pagevariable
    private Logic Lo = new Logic();
    private DataTable DtGrid = new DataTable();
    private string _msg = string.Empty;
    private string _sysMsg = string.Empty;
    DataUtility Co = new DataUtility();
    private Cryptography Encrypt = new Cryptography();
    private Cryptography objEnc = new Cryptography();
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindGridStatus();
        }
    }
    DataTable DtGrid1 = new DataTable();
    protected void BindGridStatus()
    {
        DtGrid = Lo.NewRetriveFilterCode("ProdCountformoredetails", "", "", "", "", 0, 0, 0);
        if (DtGrid.Rows.Count > 0)
        {
            try
            {
                gvstatus.DataSource = DtGrid;
                gvstatus.DataBind();
                DtGrid1 = Lo.NewRetriveFilterCode("ProdCountformoredetails1", "", "", "", "", 0, 0, 0);
                GridView1.DataSource = DtGrid1;
                GridView1.DataBind();
                GridView1.HeaderRow.Cells[1].Text = "Sub-Total";
                GridView1.HeaderRow.Cells[1].HorizontalAlign = HorizontalAlign.Left;
                object sumObjectn = DtGrid.Compute("Sum(TotalProd)", string.Empty);
                GridView1.HeaderRow.Cells[2].Text = sumObjectn.ToString();
                //1
                int sumo = 0;
                for (int i = 0; i < gvstatus.Rows.Count; i++)
                {
                    TextBox Mylabel = (TextBox)gvstatus.Rows[i].FindControl("texttarget");
                    sumo += int.Parse(Mylabel.Text.Trim());
                }
                GridView1.HeaderRow.Cells[3].Text = sumo.ToString();
                // object sumObjectn1 = DtGrid.Compute("Sum(Target)", string.Empty);
                // GridView1.HeaderRow.Cells[3].Text = sumObjectn1.ToString();
                //2
                object sumObjectn2 = DtGrid.Compute("Sum(Inhouseindigenization)", string.Empty);
                GridView1.HeaderRow.Cells[4].Text = sumObjectn2.ToString();
                //3
                object sumObjectn3 = DtGrid.Compute("Sum(MakeiiIndigenized)", string.Empty);
                GridView1.HeaderRow.Cells[5].Text = sumObjectn3.ToString();
                //4
                object sumObjectn4 = DtGrid.Compute("Sum(OtherthanMakeii)", string.Empty);
                GridView1.HeaderRow.Cells[6].Text = sumObjectn4.ToString();
                //5
                object sumObjectn5 = DtGrid.Compute("Sum(Totalindigenized)", string.Empty);
                GridView1.HeaderRow.Cells[7].Text = sumObjectn5.ToString();
                //6
                object sumObjectn6 = DtGrid.Compute("Sum(Total2425)", string.Empty);
                GridView1.HeaderRow.Cells[8].Text = sumObjectn6.ToString();
                // 7               
                //Two grid on for each other sum data
                GridView1.FooterRow.Cells[1].Text = "Total";
                GridView1.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Left;
                object sumObjectne = DtGrid1.Compute("Sum(TotalProd)", string.Empty);
                int sum = Convert.ToInt32(sumObjectne.ToString()) + Convert.ToInt32(sumObjectn.ToString());
                GridView1.FooterRow.Cells[2].Text = sum.ToString();
                //1

                int sum2o = 0;
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    TextBox Mylabel = (TextBox)GridView1.Rows[i].FindControl("texttarget1");
                    sum2o += int.Parse(Mylabel.Text);
                }
                GridView1.FooterRow.Cells[3].Text = sum2o.ToString();


                //  object sumObjectne1 = DtGrid1.Compute("Sum(Target)", string.Empty);
                //int sum1 = Convert.ToInt32(sumObjectne1.ToString()) + Convert.ToInt32(sumObjectn1.ToString());
                // GridView1.FooterRow.Cells[3].Text = sum1.ToString();
                //2
                object sumObjectne2 = DtGrid1.Compute("Sum(Inhouseindigenization)", string.Empty);
                int sum2 = Convert.ToInt32(sumObjectne2.ToString()) + Convert.ToInt32(sumObjectn2.ToString());
                GridView1.FooterRow.Cells[4].Text = sum2.ToString();
                //3
                object sumObjectne3 = DtGrid1.Compute("Sum(MakeiiIndigenized)", string.Empty);
                int sum3 = Convert.ToInt32(sumObjectne3.ToString()) + Convert.ToInt32(sumObjectn3.ToString());
                GridView1.FooterRow.Cells[5].Text = sum3.ToString();
                //4
                object sumObjectne4 = DtGrid1.Compute("Sum(OtherthanMakeii)", string.Empty);
                int sum4 = Convert.ToInt32(sumObjectne4.ToString()) + Convert.ToInt32(sumObjectn4.ToString());
                GridView1.FooterRow.Cells[6].Text = sum4.ToString();
                //5
                object sumObjectne5 = DtGrid1.Compute("Sum(Totalindigenized)", string.Empty);
                int sum5 = Convert.ToInt32(sumObjectne5.ToString()) + Convert.ToInt32(sumObjectn5.ToString());
                GridView1.FooterRow.Cells[7].Text = sum5.ToString();
                //6
                object sumObjectne6 = DtGrid1.Compute("Sum(Total2425)", string.Empty);
                Decimal sum6 = Convert.ToDecimal(sumObjectne6.ToString()) + Convert.ToDecimal(sumObjectn6.ToString());
                GridView1.FooterRow.Cells[8].Text = sum6.ToString();
                // 7


            }
            catch (Exception ex)
            { }
        }
    }
    protected void lbupdatestatus_Click(object sender, EventArgs e)
    {

    }
    protected void gvstatus_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "action")
        {
            DataTable dtproducts = new DataTable();
            GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
            int rowIndex = gvr.RowIndex;

            string hfproc = e.CommandArgument.ToString();  /*(gvstatus.Rows[rowIndex].FindControl("hfproc") as Label).Text;*/
            string lblComp = (gvstatus.Rows[rowIndex].FindControl("lblComp") as Label).Text;
            string TotalProd = (gvstatus.Rows[rowIndex].FindControl("TotalProd") as Label).Text;
            string target = (gvstatus.Rows[rowIndex].FindControl("texttarget") as TextBox).Text;
            string lblinhouse = (gvstatus.Rows[rowIndex].FindControl("lblinhouse") as Label).Text;
            string lblmakeii = (gvstatus.Rows[rowIndex].FindControl("lblmakeii") as Label).Text;
            string lblotherthan = (gvstatus.Rows[rowIndex].FindControl("lblotherthan") as Label).Text;
            string Totalindigenized = (gvstatus.Rows[rowIndex].FindControl("Totalindigenized") as Label).Text;
            string lblannual = (gvstatus.Rows[rowIndex].FindControl("lblannual") as Label).Text;

            string str = Lo.TargetValueUpdate(hfproc.ToString(), lblComp.ToString(), TotalProd.ToString(), target.ToString(), lblinhouse.ToString(), lblmakeii.ToString(),
                lblotherthan.ToString(), Totalindigenized.ToString(), lblannual.ToString());
            if (str == "Save")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Updated Successfully!!!');window.location ='ItemStatus';", true);
                BindGridStatus();
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record not successfully updated.!!!')", true);
            }

        }
        else
        {

        }
    }
    protected void gvstatus_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvstatus.EditIndex = e.NewEditIndex;
        BindGridStatus();
        TextBox texttarget = (TextBox)gvstatus.Rows[e.NewEditIndex].FindControl("texttarget");
        texttarget.Attributes.Add("onkeypress", "return onlynNumeric(event)");
    }
    protected void gvstatus_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        gvstatus.EditIndex = -1;
        BindGridStatus();
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "action")
        {
            DataTable dtproducts = new DataTable();
            GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
            int rowIndex = gvr.RowIndex;

            string hfproc = e.CommandArgument.ToString();  /*(gvstatus.Rows[rowIndex].FindControl("hfproc") as Label).Text;*/
            string lblComp = (GridView1.Rows[rowIndex].FindControl("lblComp") as Label).Text;
            string TotalProd = (GridView1.Rows[rowIndex].FindControl("TotalProd") as Label).Text;
            string target = (GridView1.Rows[rowIndex].FindControl("texttarget1") as TextBox).Text;
            string lblinhouse = (GridView1.Rows[rowIndex].FindControl("lblinhouse") as Label).Text;
            string lblmakeii = (GridView1.Rows[rowIndex].FindControl("lblmakeii") as Label).Text;
            string lblotherthan = (GridView1.Rows[rowIndex].FindControl("lblotherthan") as Label).Text;
            string Totalindigenized = (GridView1.Rows[rowIndex].FindControl("Totalindigenized") as Label).Text;
            string lblannual = (GridView1.Rows[rowIndex].FindControl("lblannual") as Label).Text;

            string str = Lo.TargetValueUpdate(hfproc.ToString(), lblComp.ToString(), TotalProd.ToString(), target.ToString(), lblinhouse.ToString(), lblmakeii.ToString(),
                lblotherthan.ToString(), Totalindigenized.ToString(), lblannual.ToString());
            if (str == "Save")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Updated Successfully!!!');window.location ='ItemStatus';", true);
                BindGridStatus();
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record not successfully updated.!!!')", true);
            }

        }
        else
        {

        }
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvstatus.EditIndex = e.NewEditIndex;
        BindGridStatus();
        TextBox texttarget = (TextBox)gvstatus.Rows[e.NewEditIndex].FindControl("texttarget");
        texttarget.Attributes.Add("onkeypress", "return onlynNumeric(event)");
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        gvstatus.EditIndex = -1;
        BindGridStatus();
    }
    protected void gvstatus_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TextBox txttar = (e.Row.FindControl("texttarget") as TextBox);
            HiddenField hfref = (e.Row.FindControl("hfproc") as HiddenField);
            DataTable dtget = Lo.NewRetriveFilterCode("getvalueitem", hfref.Value, "", "", "", 0, 0, 0);
            if (dtget.Rows.Count > 0)
            {
                txttar.Text = dtget.Rows[0]["TargetIndigenization202021"].ToString();
            }
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TextBox txttar1 = (e.Row.FindControl("texttarget1") as TextBox);
            HiddenField hfref1 = (e.Row.FindControl("hfproc1") as HiddenField);
            DataTable dtgetm = Lo.NewRetriveFilterCode("getvalueitem", hfref1.Value, "", "", "", 0, 0, 0);
            if (dtgetm.Rows.Count > 0)
            {
                txttar1.Text = dtgetm.Rows[0]["TargetIndigenization202021"].ToString();
            }
        }
    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        try
        {
            #region FirstGrid
            foreach (GridViewRow row in gvstatus.Rows)
            {
                CheckBox chk = (CheckBox)row.FindControl("chk");
                if (chk.Checked)
                {
                    HiddenField hfproc = (HiddenField)row.FindControl("hfproc");
                    Label lblComp = (Label)row.FindControl("lblComp");
                    Label TotalProd = (Label)row.FindControl("TotalProd");
                    TextBox target = (TextBox)row.FindControl("texttarget");
                    Label lblinhouse = (Label)row.FindControl("lblinhouse");
                    Label lblmakeii = (Label)row.FindControl("lblmakeii");
                    Label lblotherthan = (Label)row.FindControl("lblotherthan");
                    Label Totalindigenized = (Label)row.FindControl("Totalindigenized");
                    Label lblannual = (Label)row.FindControl("lblannual");
                    string str = Lo.TargetValueUpdate(hfproc.Value.ToString(), lblComp.Text.Trim().ToString(), TotalProd.Text.Trim().ToString(), target.Text.Trim().ToString(), lblinhouse.Text.Trim().ToString(), lblmakeii.Text.Trim().ToString(),
                        lblotherthan.Text.Trim().ToString(), Totalindigenized.Text.Trim().ToString(), lblannual.Text.Trim().ToString());
                    if (str == "Save")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Updated Successfully!!!');window.location ='ItemStatus';", true);
                        BindGridStatus();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record not successfully updated.!!!')", true);
                    }
                }
            }
            #endregion
            #region SecondGrid
            foreach (GridViewRow row in GridView1.Rows)
            {
                CheckBox chk = (CheckBox)row.FindControl("chk1");
                if (chk.Checked)
                {
                    HiddenField hfproc1 = (HiddenField)row.FindControl("hfproc1");
                    Label lblComp1 = (Label)row.FindControl("lblComp");
                    Label TotalProd1 = (Label)row.FindControl("TotalProd");
                    TextBox target1 = (TextBox)row.FindControl("texttarget1");
                    Label lblinhouse1 = (Label)row.FindControl("lblinhouse");
                    Label lblmakeii1 = (Label)row.FindControl("lblmakeii");
                    Label lblotherthan1 = (Label)row.FindControl("lblotherthan");
                    Label Totalindigenized1 = (Label)row.FindControl("Totalindigenized");
                    Label lblannual1 = (Label)row.FindControl("lblannual");
                    string str = Lo.TargetValueUpdate(hfproc1.Value.ToString(), lblComp1.Text.Trim().ToString(), TotalProd1.Text.Trim().ToString(), target1.Text.Trim().ToString(), lblinhouse1.Text.Trim().ToString(), lblmakeii1.Text.Trim().ToString(),
                        lblotherthan1.Text.Trim().ToString(), Totalindigenized1.Text.Trim().ToString(), lblannual1.Text.Trim().ToString());
                    if (str == "Save")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Updated Successfully!!!');window.location ='ItemStatus';", true);
                        BindGridStatus();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record not successfully updated.!!!')", true);
                    }
                }
            }
            #endregion
        }
        catch (Exception ex)
        { }

    }
}