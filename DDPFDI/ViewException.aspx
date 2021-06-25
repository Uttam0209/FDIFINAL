<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewException.aspx.cs" Inherits="ViewException" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>View Exception</title>
    <link href="~/assets/css/bootstrap.css" rel="stylesheet">
    <link href="~/assets/css/all.min.css" rel="stylesheet" type="text/css">
    <link href="~/assets/css/style.css" rel="stylesheet" type="text/css">
    <link href="~/assets/css/custom.css" rel="stylesheet" type="text/css">
    <link href="~/assets/css/responsive.css" rel="stylesheet" type="text/css">
    <link href="~/assets/css/fonts-googleapis.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div id="top">
            <a href="#"><i class="fa fa-envelope" aria-hidden="true"></i>&nbsp; helpdesk-dpit@ddpmod.gov.in</a>
            <a href="#"><i class="fa fa-phone-square" aria-hidden="true"></i>&nbsp; 011-20836145 &nbsp;|&nbsp; 011-23019066</a>
            <li class="nav-item active" style="float: right; margin-right: 30px">
                <a class="nav-link" runat="server" href="~/ProductList"><i class="fa fa-home" aria-hidden="true"></i></a>
            </li>
        </div>
        <div class="container" style="min-height: 83vh;">
            <div class="row">
                <asp:GridView ID="gverror" runat="server" class="table table-responsive table-hover table-bordered" AutoGenerateColumns="false">
                    <Columns>
                        <asp:TemplateField HeaderText="Sr.NO">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="ExceptionMsg" HeaderText="Exception" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
        <div id="footer1" class="container-fluid" style="min-height: 50px; text-align: center; background: #373f50;">
            <div class="container">
                <div class="row">
                    <div class="col-12" style="padding-top: 10px; color: white;">
                        ©2020 <a href="https://srijandefence.gov.in/ProductList" style="color: white;">www.srijandefence.gov.in</a> | All Right Reserved. | Designed, Developed and Hosted by Department of Defence Production                           
                    </div>
                </div>
            </div>
        </div>
    </form>
    <script src="assets/js/jquery-3.4.1.js"></script>
    <script src="assets/js/bootstrap.min.js"></script>
    <script src="assets/js/custom.js"></script>

</body>
</html>
