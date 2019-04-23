<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Dashboard.aspx.cs" Inherits="Admin_Dashboard" MasterPageFile="~/Admin/MasterPage.master" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<asp:Content ID="conhead" runat="server" ContentPlaceHolderID="head">
</asp:Content>
<asp:Content ID="coninner" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

    <asp:ScriptManager ID="sc" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="up" runat="server">
        <ContentTemplate>
            <div class="content oem-content">
                <div class="sideBg">
                    <div class="col-mod-12">
                        <ul class="breadcrumb">
                            <li><asp:Label ID="lblPageName" runat="server" Text=""></asp:Label></li>
                        </ul>
                    </div>
                    <form method="post" class="addfdi">
                        <div class="admin-dashboard">
                            <div class="row">
                                <div class="col-md-12">


                                    <div class="row">
                                        <div class="col-lg-4 col-sm-6 col-xs-12">
                                            <div class="white-box analytics-info total-comp">
                                                <h3 class="box-title">Total Companies</h3>
                                                <ul class="list-inline two-part">

                                                    <li><i class="ti-arrow-up"></i>
                                                        <asp:LinkButton ID="lnkbtnTotComp" runat="server" Text="0"></asp:LinkButton></li>

                                                </ul>
                                                <div class="file-export">
                                                    <button class="btn btn-primary pull-right">Export</button>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-sm-6 col-xs-12">
                                            <div class="white-box analytics-info total-fdi">
                                                <h3 class="box-title">Total FDI</h3>
                                                <ul class="list-inline two-part">

                                                    <li><i class="ti-arrow-up text-purple"></i>
                                                        <asp:LinkButton ID="lnkbtnFDI" runat="server" Text="0"></asp:LinkButton></li>

                                                </ul>
                                                <div class="file-export">
                                                    <button class="btn btn-primary pull-right">Export</button>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-sm-6 col-xs-12">
                                            <div class="white-box analytics-info last-fdi">
                                                <h3 class="box-title">Last Year FDI</h3>
                                                <ul class="list-inline two-part">

                                                    <li>


                                                        <asp:LinkButton ID="lnkbtnLYFDI" runat="server" Text="0"></asp:LinkButton></li>

                                                </ul>
                                                <div class="file-export">
                                                    <button class="btn btn-primary pull-right">Export</button>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" style="margin-top: 10px;">
                                        <div class="col-md-6">
                                            <asp:DropDownList ID="ddlGraphType" runat="server" CssClass="form-control GraphType">
                                                <asp:ListItem Value="Pie Chart">Pie Chart</asp:ListItem>
                                                <asp:ListItem Value="Line Chart">Line Chart</asp:ListItem>
                                                <%--<asp:ListItem>Trend Chart</asp:ListItem>--%>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-6">
                                            <asp:DropDownList ID="ddlRange" runat="server" CssClass="form-control">
                                                <asp:ListItem>All</asp:ListItem>
                                                <%-- <asp:ListItem>2001-02</asp:ListItem>
                                                        <asp:ListItem>2002-03</asp:ListItem>
                                                        <asp:ListItem>2003-04</asp:ListItem>
                                                        <asp:ListItem>2004-05</asp:ListItem>
                                                        <asp:ListItem>2005-06</asp:ListItem>
                                                        <asp:ListItem>2006-07</asp:ListItem>
                                                        <asp:ListItem>2007-08</asp:ListItem>
                                                        <asp:ListItem>2008-09</asp:ListItem>
                                                        <asp:ListItem>2009-10</asp:ListItem>
                                                        <asp:ListItem>2010-11</asp:ListItem>
                                                        <asp:ListItem>2011-12</asp:ListItem>
                                                        <asp:ListItem>2012-13</asp:ListItem>
                                                        <asp:ListItem>2013-14</asp:ListItem>
                                                        <asp:ListItem>2014-15</asp:ListItem>
                                                        <asp:ListItem>20015-16</asp:ListItem>
                                                        <asp:ListItem>2016-17</asp:ListItem>
                                                        <asp:ListItem>20017-18</asp:ListItem>
                                                        <asp:ListItem>2018-19</asp:ListItem>--%>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div id="divPieChart">
                                                <div id="chartPie" style="height: 370px; width: 100%; margin-top: 20px"></div>
                                            </div>
                                            <div id="divLineChart" style="display: none">
                                                <div id="chartLine" style="height: 370px; width: 100%; margin-top: 20px"></div>
                                            </div>
                                        </div>
                                        <div>
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>

                    </form>
                </div>
                <div class="footer">© 2019 <a href="#">Department of Defence Production</a> </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
