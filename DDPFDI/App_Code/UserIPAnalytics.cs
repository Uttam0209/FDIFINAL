using Antlr.Runtime;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Web;

/// <summary>
/// Summary description for UserIPAnalytics
/// </summary>
public class UserIPAnalytics
{
    public UserIPAnalytics()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public void GetAnalytics()
    {
        try
        {            
            HybridDictionary hysave = new HybridDictionary();
            string _msg = string.Empty;
            string _sysMsg = string.Empty;
            hysave["Logdate"] = DateTime.Now;
            hysave["Browser"] = GetBrowserNameWithVersion();
            hysave["IPAddress"] = HttpContext.Current.Request.UserHostAddress;
            hysave["PageURL"] = HttpContext.Current.Request.Url.ToString();
            hysave["UrlReferrer"] = GetReferrerPageName();
            BusinessLayer.Logic Lo = new BusinessLayer.Logic();
            string id = Lo.AddUserAnalytics(hysave, out _sysMsg, out _msg);
        }
        catch
        {

        }
    } 
    private string GetBrowserNameWithVersion()
    {
        var userAgent = HttpContext.Current.Request.UserAgent;
        var browserWithVersion = "";
        if (userAgent.IndexOf("Edge") > -1)
        {
            //Edge
            browserWithVersion = "Edge Browser Version : " + userAgent.Split(new string[] { "Edge/" }, StringSplitOptions.None)[1].Split('.')[0];
        }
        else if (userAgent.IndexOf("Chrome") > -1)
        {
            //Chrome
            browserWithVersion = "Chrome Browser Version : " + userAgent.Split(new string[] { "Chrome/" }, StringSplitOptions.None)[1].Split('.')[0];
        }
        else if (userAgent.IndexOf("Safari") > -1)
        {
            //Safari
            browserWithVersion = "Safari Browser Version : " + userAgent.Split(new string[] { "Safari/" }, StringSplitOptions.None)[1].Split('.')[0];
        }
        else if (userAgent.IndexOf("Firefox") > -1)
        {
            //Firefox
            browserWithVersion = "Firefox Browser Version : " + userAgent.Split(new string[] { "Firefox/" }, StringSplitOptions.None)[1].Split('.')[0];
        }
        else if (userAgent.IndexOf("rv") > -1)
        {
            //IE11
            browserWithVersion = "Internet Explorer Browser Version : " + userAgent.Split(new string[] { "rv:" }, StringSplitOptions.None)[1].Split('.')[0];
        }
        else if (userAgent.IndexOf("MSIE") > -1)
        {
            //IE6-10
            browserWithVersion = "Internet Explorer Browser  Version : " + userAgent.Split(new string[] { "MSIE" }, StringSplitOptions.None)[1].Split('.')[0];
        }
        else if (userAgent.IndexOf("Other") > -1)
        {
            //Other
            browserWithVersion = "Other Browser Version : " + userAgent.Split(new string[] { "Other" }, StringSplitOptions.None)[1].Split('.')[0];
        }

        return browserWithVersion;
    }
    public static string GetReferrerPageName()
    {
        string functionReturnValue = null;

        if ((((System.Web.HttpContext.Current.Request.UrlReferrer) != null)))
        {
            functionReturnValue = HttpContext.Current.Request.UrlReferrer.ToString();
        }
        else
        {
            functionReturnValue = "N/A";
        }
        return functionReturnValue;
    }
}
