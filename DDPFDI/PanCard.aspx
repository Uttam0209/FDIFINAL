<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PanCard.aspx.cs" Inherits="PanCard" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:TextBox ID="txtpanno" runat="server" AutoPostBack="true" OnTextChanged="txtpanno_TextChanged"></asp:TextBox>
            <%--<asp:Button ID="btnsub" runat="server" Text="Submit" OnClick="btnsub_Click" />--%>
            <asp:Label ID="lblmsg" runat="server"></asp:Label>
            <asp:HiddenField runat="server" ID="hfpanname" />

            <br />
            <br />
             <asp:TextBox ID="TXTDEC" runat="server" Width="500px" PLACEHOLDER="Encrypt/Decrypt" ></asp:TextBox>
            <asp:Button ID="btnenc" runat="server" Text="ENCRYPT" OnClick="btnenc_Click" />
             <asp:Button ID="btndec" runat="server" Text="DECRYPT" OnClick="btndec_Click" />
            <asp:Label ID="Label1" runat="server"></asp:Label>


        </div>
    </form>
</body>
</html>
