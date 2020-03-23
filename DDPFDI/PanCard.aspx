<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PanCard.aspx.cs" Inherits="PanCard" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div style="text-align: center;">
                <asp:RadioButtonList ID="rbcheck" runat="server" RepeatColumns="4" RepeatDirection="Horizontal" AutoPostBack="true" RepeatLayout="Flow" OnSelectedIndexChanged="rbcheck_SelectedIndexChanged">
                    <asp:ListItem Value="1" Selected="True">PAN CARD</asp:ListItem>
                    <asp:ListItem Value="2">GST NO</asp:ListItem>
                    <asp:ListItem Value="3">TIN & OTHER</asp:ListItem>
                    <asp:ListItem Value="4">ENC & DEC</asp:ListItem>
                </asp:RadioButtonList>
            </div>
            <br />
            <br />
            <div style="text-align: center;">
                <asp:Panel runat="server" ID="panelpan" Visible="false">
                    <asp:TextBox ID="txtpanno" runat="server"></asp:TextBox>
                    <asp:Button runat="server" ID="btnpanno" CssClass="btn btn-primary" Text="PAN" OnClick="btnpanno_Click" /><br />
                    <br />
                    <asp:Label ID="lblmsg" runat="server"></asp:Label><br />
                    <br />
                    <asp:Label ID="Label15" runat="server"></asp:Label><br />
                    <br />
                    <asp:Label ID="Label16" runat="server"></asp:Label><br />
                    <br />
                    <asp:Label ID="Label17" runat="server"></asp:Label><br />
                    <br />
                    <asp:Label ID="Label18" runat="server"></asp:Label><br />
                    <br />
                    <asp:Label ID="Label19" runat="server"></asp:Label><br />
                    <br />
                    <asp:Label ID="Label20" runat="server"></asp:Label><br />
                    <br />
                    <asp:Label ID="Label21" runat="server"></asp:Label><br />
                    <br />
                    <asp:Label ID="Label22" runat="server"></asp:Label><br />
                    <br />
                    <asp:Label ID="Label23" runat="server"></asp:Label><br />
                    <br />
                    <asp:Label ID="Label24" runat="server"></asp:Label><br />
                    <br />
                </asp:Panel>
            </div>
            <div style="text-align: center;">
                <asp:Panel runat="server" ID="panelgst" Visible="false">
                    <asp:TextBox ID="txtgst" runat="server"></asp:TextBox>
                    <asp:Button runat="server" ID="btngst" CssClass="btn btn-primary" Text="GST" OnClick="btngst_Click" />
                    <br />
                    <br />
                    <asp:Label ID="lblGstmsg" runat="server"></asp:Label>
                    <br />
                    <br />
                    <asp:Label ID="Label2" runat="server"></asp:Label>
                    <br />
                    <br />
                    <asp:Label ID="Label3" runat="server"></asp:Label>
                    <br />
                    <br />
                    <asp:Label ID="Label4" runat="server"></asp:Label>
                    <br />
                    <br />
                    <asp:Label ID="Label5" runat="server"></asp:Label>
                    <br />
                    <br />
                    <asp:Label ID="Label6" runat="server"></asp:Label>
                    <br />
                    <br />
                </asp:Panel>
            </div>
            <div style="text-align: center;">
                <asp:Panel runat="server" ID="paneltinother" Visible="false">
                    <asp:TextBox ID="txtTinOther" runat="server"></asp:TextBox>
                    <asp:Button runat="server" ID="btntinother" CssClass="btn btn-primary" Text="TIN & OTHER" OnClick="btntinother_Click" />
                    <br />
                    <br />
                    <asp:Label ID="lblTinother" runat="server"></asp:Label>
                    <br />
                    <br />
                    <asp:Label ID="Label7" runat="server"></asp:Label>
                    <br />
                    <br />
                    <asp:Label ID="Label8" runat="server"></asp:Label>
                    <br />
                    <br />
                    <asp:Label ID="Label9" runat="server"></asp:Label>
                    <br />
                    <br />
                    <asp:Label ID="Label10" runat="server"></asp:Label>
                    <br />
                    <br />
                    <asp:Label ID="Label11" runat="server"></asp:Label>
                    <br />
                    <br />
                    <asp:Label ID="Label12" runat="server"></asp:Label>
                    <br />
                    <br />
                    <asp:Label ID="Label13" runat="server"></asp:Label>
                    <br />
                    <br />
                    <asp:Label ID="Label14" runat="server"></asp:Label>
                    <br />
                    <br />
                </asp:Panel>
            </div>
            <div style="text-align: center;">
                <asp:Panel ID="panelenc" runat="server" Visible="false">
                    <asp:TextBox ID="TXTDEC" runat="server" Width="500px" PLACEHOLDER="Encrypt/Decrypt"></asp:TextBox>
                    <asp:Button ID="btnenc" runat="server" Text="ENCRYPT" OnClick="btnenc_Click" />
                    <asp:Button ID="btndec" runat="server" Text="DECRYPT" OnClick="btndec_Click" />
                    <asp:Label ID="Label1" runat="server"></asp:Label>
                </asp:Panel>
            </div>
            <br />
            <br />
        </div>
    </form>
</body>
</html>
