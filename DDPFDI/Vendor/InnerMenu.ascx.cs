using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Vendor_InnerMenu : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string fileName = System.IO.Path.GetFileName(Request.Url.AbsolutePath);
        if (fileName == "GeneralInformation")
        {
            gitab.Attributes.Add("Class", "nav-link active");
            ci1tab.Attributes.Add("Class", "nav-link");
            ci2tab.Attributes.Add("Class", "nav-link");
            ddstab.Attributes.Add("Class", "nav-link");
            fitab.Attributes.Add("Class", "nav-link");
            cltab.Attributes.Add("Class", "nav-link");
        }
        if (fileName == "CompanyInformation_I")
        {
            gitab.Attributes.Add("Class", "nav-link ");
            ci1tab.Attributes.Add("Class", "nav-link active");
            ci2tab.Attributes.Add("Class", "nav-link");
            ddstab.Attributes.Add("Class", "nav-link");
            fitab.Attributes.Add("Class", "nav-link");
            cltab.Attributes.Add("Class", "nav-link");
        }
        if (fileName == "CompanyInformation_II")
        {
            gitab.Attributes.Add("Class", "nav-link ");
            ci1tab.Attributes.Add("Class", "nav-link ");
            ci2tab.Attributes.Add("Class", "nav-link active");
            ddstab.Attributes.Add("Class", "nav-link");
            fitab.Attributes.Add("Class", "nav-link");
            cltab.Attributes.Add("Class", "nav-link");
        }      
        if (fileName == "DetailsofDefenceStores")
        {
            gitab.Attributes.Add("Class", "nav-link ");
            ci1tab.Attributes.Add("Class", "nav-link ");
            ci2tab.Attributes.Add("Class", "nav-link ");
            ddstab.Attributes.Add("Class", "nav-link active");
            fitab.Attributes.Add("Class", "nav-link");
            cltab.Attributes.Add("Class", "nav-link");
        }
        if (fileName == "FinancialInformation")
        {
            gitab.Attributes.Add("Class", "nav-link ");
            ci1tab.Attributes.Add("Class", "nav-link ");
            ci2tab.Attributes.Add("Class", "nav-link ");
            ddstab.Attributes.Add("Class", "nav-link ");
            fitab.Attributes.Add("Class", "nav-link active");
            cltab.Attributes.Add("Class", "nav-link");
        }
        if (fileName == "CheckList")
        {
            gitab.Attributes.Add("Class", "nav-link ");
            ci1tab.Attributes.Add("Class", "nav-link ");
            ci2tab.Attributes.Add("Class", "nav-link ");
            ddstab.Attributes.Add("Class", "nav-link ");
            fitab.Attributes.Add("Class", "nav-link ");
            cltab.Attributes.Add("Class", "nav-link active");
        }
    }
}