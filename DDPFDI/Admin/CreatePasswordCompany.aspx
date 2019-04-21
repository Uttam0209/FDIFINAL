<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CreatePasswordCompany.aspx.cs" Inherits="Admin_CreatePasswordCompany" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="text-align: center; margin-top: 50px;">
            <div>
                <asp:TextBox runat="server" ID="txtpassword"></asp:TextBox>
                <br />
                <asp:TextBox runat="server" ID="txttnewpass"></asp:TextBox>
                <br />
                <asp:Button runat="server" ID="btnchangepass" Text="Submit" OnClick="btnchangepass_Click" />
            </div>
        </div>
    </form>
</body>
</html>
