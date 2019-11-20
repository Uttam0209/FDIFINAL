<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProductGraph.aspx.cs" Inherits="Admin_ProductGraph" MasterPageFile="~/Admin/MasterPage.master" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<asp:Content ID="head" runat="server" ContentPlaceHolderID="head">
</asp:Content>
<asp:Content ID="inner" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

    <asp:HiddenField runat="server" ID="hidType" />
    <asp:HiddenField runat="server" ID="mRefNo" />
    <asp:HiddenField runat="server" ID="sundomaintype" />
    <div class="content oem-content">
        <div class="sideBg">
            <div class="row">
                <div class="col-mod-12 padding_0">
                    <ul class="breadcrumb">
                        <li>
                            <asp:Label ID="lblPageName" runat="server" Text=""></asp:Label>
                        </li>
                    </ul>
                </div>
                <div class="col-md-12">
                    <div class="clearfix"></div>
                    <div style="margin-top: 5px;">
                        <a class="fa fa-arrow-circle-left pull-right" href="javascript: history.go(-1)">&nbsp; &nbsp;Back</a>
                    </div>
                    <div class="clearfix"></div>
                </div>
            </div>
            <div class="clearfix"></div>
            <div class="addfdi">
                <div class="row">
                    <div class="col-md-12">
                        <div class="text-center">
                            <asp:Label ID="lblmsg" runat="server" CssClass="label label-default text-center"></asp:Label>
                        </div>
                        <div class="clearfix mt10"></div>
                        <asp:Panel ID="pan1" runat="server">
                            <div class="text-center" style="overflow-x: auto;">
                                <asp:Chart ID="crtCompGraph" runat="server" Height="500px" Width="1100px" OnClick="crtCompGraph_Click">
                                    <Titles>
                                        <asp:Title ShadowOffset="3" Name="Company Items Product Industry domain" />
                                    </Titles>
                                    <Legends>
                                        <asp:Legend Alignment="Center" Docking="Bottom" IsTextAutoFit="true" Title="PRODUCT (INDUSTRY DOMAIN) WISE VALUE" Name="Default" LegendStyle="Row" />
                                    </Legends>
                                    <Series>
                                        <asp:Series Name="Default" IsValueShownAsLabel="true" Color="#669999" ChartType="StackedColumn" YValuesPerPoint="2"></asp:Series>
                                    </Series>
                                    <ChartAreas>
                                        <asp:ChartArea Name="ChartArea1" BorderWidth="0"></asp:ChartArea>
                                    </ChartAreas>
                                </asp:Chart>
                            </div>
                        </asp:Panel>
                        <div class="clearfix mt10"></div>
                        <asp:Panel ID="pan2" runat="server" Visible="false">
                            <div class="text-center">
                                <asp:Chart ID="crtSubdomain" runat="server" Height="500px" Width="1100px" OnClick="crtSubdomain_Click">
                                    <Titles>
                                        <asp:Title ShadowOffset="3" Name="Company Items Product Industry domain" />
                                    </Titles>
                                    <Legends>
                                        <asp:Legend Alignment="Center" Docking="Bottom" IsTextAutoFit="true" Title="PRODUCT (INDUSTRY SUB DOMAIN) WISE VALUE" Name="SubDomian" LegendStyle="Row" />
                                    </Legends>
                                    <Series>
                                        <asp:Series Name="SubDomian" IsValueShownAsLabel="true" Color="#ffff99" ChartType="StackedColumn"></asp:Series>
                                    </Series>
                                    <ChartAreas>
                                        <asp:ChartArea Name="crtsub" BorderWidth="0"></asp:ChartArea>
                                    </ChartAreas>
                                </asp:Chart>
                            </div>
                        </asp:Panel>
                      <div class="clearfix"></div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
