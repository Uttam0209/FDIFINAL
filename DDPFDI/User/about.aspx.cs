﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class User_About : System.Web.UI.Page
{
    UserIPAnalytics userip = new UserIPAnalytics();
    protected void Page_Load(object sender, EventArgs e)
    {
        userip.GetAnalytics();
    }
}