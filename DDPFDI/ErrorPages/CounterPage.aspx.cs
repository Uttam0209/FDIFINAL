using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ErrorPages_CounterPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblSiteVisited.Text = "No of times site visited=" + Application["SiteVisitedCounter"].ToString();
        lblOnlineUsers.Text = "No of users online on the site=" + Application["OnlineUserCounter"].ToString();
    }

    private void GetIpValue(out string ipAdd)
    {
        ipAdd = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

        if (string.IsNullOrEmpty(ipAdd))
        {
            ipAdd = Request.ServerVariables["REMOTE_ADDR"];
        }
        else
        {
            lblIPAddress.Text = ipAdd;
        }
    }

    private void GetIpAddress(out string userip)
    {
        userip = Request.UserHostAddress;
        if (Request.UserHostAddress != null)
        {
            Int64 macinfo = new Int64();
            string macSrc = macinfo.ToString("X");
            if (macSrc == "0")
            {
                if (userip == "127.0.0.1")
                {
                    Response.Write("visited Localhost!");
                }
                else
                {
                    lblIPAdd.Text = userip;
                }
            }
        }
    }


    void Application_Start(object sender, EventArgs e)
    {
        // Code that runs on application startup
        Application["SiteVisitedCounter"] = 0;
        //to check how many users have currently opened our site write the following line
        Application["OnlineUserCounter"] = 0;
    }

    void Session_Start(object sender, EventArgs e)
    {
        // Code that runs when a new session is started
        Application.Lock();
        Application["SiteVisitedCounter"] = Convert.ToInt32(Application["SiteVisitedCounter"]) + 1;
        Application["OnlineUserCounter"] = Convert.ToInt32(Application["OnlineUserCounter"]) + 1;
        Application.UnLock();
    }

    void Session_End(object sender, EventArgs e)
    {
        // Code that runs when a session ends.
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer
        // or SQLServer, the event is not raised.
        Application.Lock();
        Application["OnlineUserCounter"] = Convert.ToInt32(Application["OnlineUserCounter"]) - 1;
        Application.UnLock();
    }
    protected void btnClearSesson_Click(object sender, EventArgs e)
    {
        Session.Abandon();
    }
}