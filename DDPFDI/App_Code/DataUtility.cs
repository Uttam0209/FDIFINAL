using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Net;

/// <summary>
/// Summary description for DataUtility
/// </summary>
public class DataUtility
{
    public DataUtility()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    #region "Variables"
    private static DataUtility instance = new DataUtility();
    #endregion
    #region "Static"
    public static DataUtility Instance
    {
        get
        {
            return instance;
        }
    }
    #endregion
    #region methods
    public string RameshQueryString(string RawURLs, string QueryStringName)
    {
        //starting index at e.g http://x.aspx?Propid=123 will return 21
        RawURLs = RawURLs.Replace("%3d", "=");
        RawURLs = RawURLs.Replace("%2f", "/");
        int start = RawURLs.IndexOf(QueryStringName, 0) + QueryStringName.Length;

        int length = RawURLs.IndexOf("&", start);
        if (length > 0)
            length = length - start;
        else

            length = RawURLs.Length - start;
        return RawURLs.Substring(start, length);

    }
    public void FillDropdownlist(DropDownList cmb, DataTable dt, string TextField, string ValueField)
    {
        cmb.DataSource = dt;
        cmb.DataTextField = TextField;
        cmb.DataValueField = ValueField;
        cmb.DataBind();
    }
    public void FillListBoxlist(ListBox cmb, DataTable dt, string TextField, string ValueField)
    {
        cmb.DataSource = dt;
        cmb.DataTextField = TextField;
        cmb.DataValueField = ValueField;
        cmb.DataBind();
    }
    public void FillCheckBox(CheckBoxList CB, DataTable dt, string TextField, string ValueField)
    {
        CB.DataSource = dt;
        CB.DataTextField = TextField;
        CB.DataValueField = ValueField;
        CB.DataBind();
    }
    public void FillRadioBoxList(RadioButtonList RB, DataTable dt, string TextField, string ValueField)
    {
        RB.DataSource = dt;
        RB.DataTextField = TextField;
        RB.DataValueField = ValueField;
        RB.DataBind();
    }
    public string RSQandSQLInjection(string SingleQuote, string level)
    {
        if (level == "hard")
        {
            SingleQuote = SingleQuote.Replace("'where ", "");
            SingleQuote = SingleQuote.Replace(" where ", "");
            SingleQuote = SingleQuote.Replace(" is ", "");
            SingleQuote = SingleQuote.Replace(" or ", "");
            SingleQuote = SingleQuote.Replace("'or ", "");
            SingleQuote = SingleQuote.Replace(" from ", "");
            SingleQuote = SingleQuote.Replace("'table ", "");
            SingleQuote = SingleQuote.Replace(" table ", "");
            SingleQuote = SingleQuote.Replace("'view ", "");
            SingleQuote = SingleQuote.Replace(" view ", "");
            SingleQuote = SingleQuote.Replace("'exec ", "");
            SingleQuote = SingleQuote.Replace(" exec ", "");
            SingleQuote = SingleQuote.Replace("'and ", "");
            SingleQuote = SingleQuote.Replace(" and ", "");
            SingleQuote = SingleQuote.Replace("'drop ", "");
            SingleQuote = SingleQuote.Replace(" drop ", "");
            SingleQuote = SingleQuote.Replace("'delete ", "");
            SingleQuote = SingleQuote.Replace(" delete ", "");
            SingleQuote = SingleQuote.Replace("'alter ", "");
            SingleQuote = SingleQuote.Replace(" alter ", "");
            SingleQuote = SingleQuote.Replace("'update ", "");
            SingleQuote = SingleQuote.Replace(" update ", "");
            SingleQuote = SingleQuote.Replace("'select ", "");
            SingleQuote = SingleQuote.Replace(" select ", "");
            SingleQuote = SingleQuote.Replace("'insert ", "");
            SingleQuote = SingleQuote.Replace(" insert ", "");
            SingleQuote = SingleQuote.Replace("'create ", "");
            SingleQuote = SingleQuote.Replace(" create ", "");
            SingleQuote = SingleQuote.Replace("=", "");
            SingleQuote = SingleQuote.Replace("*", "");
            SingleQuote = SingleQuote.Replace(" from ", "");
            SingleQuote = SingleQuote.Replace("'from ", "");
            SingleQuote = SingleQuote.Replace(",", "");
            SingleQuote = SingleQuote.Replace(";", "");
            SingleQuote = SingleQuote.Replace(":", "");
            SingleQuote = SingleQuote.Replace("/", "");
            SingleQuote = SingleQuote.Replace("@@", "");
            //SingleQuote = SingleQuote.Replace("&", "");
            //SingleQuote = SingleQuote.Replace("%", "");
            SingleQuote = SingleQuote.Replace("'", "");
            return SingleQuote;
        }
        else
        {

            SingleQuote = SingleQuote.Replace("'", "");
            SingleQuote = SingleQuote.Replace("'drop ", "");
            SingleQuote = SingleQuote.Replace(" drop ", "");
            SingleQuote = SingleQuote.Replace("'delete ", "");
            SingleQuote = SingleQuote.Replace(" delete ", "");
            SingleQuote = SingleQuote.Replace("'alter ", "");
            SingleQuote = SingleQuote.Replace(" alter ", "");
            SingleQuote = SingleQuote.Replace("=", "");
            SingleQuote = SingleQuote.Replace("'", "");
            return SingleQuote;
        }
    }
    public bool GetImageFilter(string fileName)
    {
        if (fileName == "")
            return false;
        string fileType = fileName.Substring(fileName.LastIndexOf("."), fileName.Length - fileName.LastIndexOf("."));
        if (fileType.ToUpper() == ".JPEG" || fileType.ToUpper() == ".JPG" || fileType.ToUpper() == ".PNG" || fileType.ToUpper() == ".GIF" || fileType.ToUpper() == ".TIF")
            return true;
        else
            return false;
    }
    public bool GetFileFilter(string fileName)
    {
        if (fileName == "")
        {
            return false;
        }
        string fileType = fileName.Substring(fileName.LastIndexOf("."), fileName.Length - fileName.LastIndexOf("."));
        if (fileType.ToUpper() == ".PDF")
            return true;
        //    else if (fileType.ToUpper() == ".txt")
        //      return true;
        //else if (fileType.ToUpper() == ".xlxs")
        //  return true;
        else
            return false;
    }
    public int GetFileSizeInKB(Byte[] size)
    {
        return 0;
    }
    #endregion
    #region "OTP SMS"
    public void sendSMS(string _mobileNo, string _mOTP)
    {
        string StrMobile = "91" + _mobileNo;
        string strMsg = _mOTP + " is your AeroIndia mobile verification OTP";
        string requestUristring = string.Format("http://smsgw.sms.gov.in/failsafe/HttpLink?username=aeroindia.otp&pin=e086ta06&message=" + strMsg + "&mnumber=" + StrMobile + "&signature=AEROIN");
        HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(requestUristring);
        HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();
        System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());
        string responseString = respStreamReader.ReadToEnd();
        respStreamReader.Close();
        myResp.Close();
    }
    public void sendSMSMsg(string _mobileNo, string _mOTP, string _mMsg)
    {
        string strMsg = "";
        string StrMobile = "91" + _mobileNo;
        if (_mMsg == "")
            strMsg = _mOTP + " is your AeroIndia mobile verification OTP";
        else
            strMsg = _mMsg;
        string requestUristring = string.Format("http://smsgw.sms.gov.in/failsafe/HttpLink?username=aeroindia.otp&pin=e086ta06&message=" + strMsg + "&mnumber=" + StrMobile + "&signature=AEROIN");
        HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(requestUristring);
        HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();
        System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());
        string responseString = respStreamReader.ReadToEnd();
        respStreamReader.Close();
        myResp.Close();
    }
    #endregion
}