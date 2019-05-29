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
                    <div class="row">
                        <div class="col-mod-12 padding_0">
                            <ul class="breadcrumb">
                                <li>
                                    <asp:Label ID="lblPageName" runat="server" Text="Dashboard"></asp:Label>
                                </li>
                            </ul>
                        </div>
                    </div>
                    <form method="post" class="addfdi">
                        <div class="admin-dashboard">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="row">
                                        <!--- box start----->
                                        <div class="col-lg-4 col-sm-6 col-xs-12">
                                            <div class="white-box analytics-info total-comp">
                                                <ul class="list-inline two-part">
                                                    <li>
                                                        <div class="icon-box">
                                                            <i class="far fa-building"></i>
                                                        </div>
                                                        <div class="compName">
                                                            <h3 class="box-title">Companies</h3>
                                                            <div class="Number">
                                                                <asp:LinkButton ID="lnkbtnTotComp" CssClass="comp_number" runat="server" Text="0" OnClick="lnkbtnTotComp_Click"></asp:LinkButton>
                                                            </div>
                                                        </div>
                                                    </li>
                                                </ul>
                                                <div class="file-export">
                                                    <asp:LinkButton ID="lblComp" runat="server" class="fa fa-file-export" data-toggle="tooltip" ToolTip="Export to Excel" OnClick="lblComp_Click"></asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                        <!--- box End----->
                                        <div class="col-lg-4 col-sm-6 col-xs-12">
                                            <div class="white-box analytics-info total-fdi">
                                                <ul class="list-inline two-part">
                                                    <li>
                                                        <div class="icon-box">
                                                            <i class="far fa-building"></i>
                                                        </div>
                                                        <div class="compName">
                                                            <h3 class="box-title">Divsions</h3>
                                                            <div class="Number">
                                                                <asp:LinkButton ID="lnkbtnTotDiv" runat="server" CssClass="comp_number" Text="0" OnClick="lnkbtnTotDiv_Click"></asp:LinkButton>
                                                            </div>
                                                        </div>
                                                    </li>
                                                </ul>
                                                <div class="file-export">
                                                    <asp:LinkButton ID="lnkDiv" runat="server" class="fa fa-file-export" data-toggle="tooltip" ToolTip="Export to Excel" OnClick="lnkDiv_Click"></asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-sm-6 col-xs-12">
                                            <div class="white-box analytics-info last-fdi">
                                                <ul class="list-inline two-part">
                                                    <li>
                                                        <div class="icon-box">
                                                            <i class="far fa-building"></i>
                                                        </div>
                                                        <div class="compName">
                                                            <h3 class="box-title">Units</h3>
                                                            <div class="Number">
                                                                <asp:LinkButton ID="lnkbtnTotUnit" runat="server" CssClass="comp_number" Text="0" OnClick="lnkbtnTotUnit_Click"></asp:LinkButton>
                                                            </div>
                                                        </div>
                                                    </li>
                                                </ul>
                                                <div class="file-export">
                                                    <asp:LinkButton ID="lnkUnit" runat="server" class="fa fa-file-export" data-toggle="tooltip" ToolTip="Export to Excel" OnClick="lnkUnit_Click"></asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" style="margin-top: 10px;">
                                        <div class="col-lg-4 col-sm-6 col-xs-12">
                                            <div class="white-box analytics-info last-fdi">
                                                <ul class="list-inline two-part">
                                                    <li>
                                                        <div class="icon-box">
                                                            <i class="fa fa-users" aria-hidden="true"></i>
                                                        </div>
                                                        <div class="compName">
                                                            <h3 class="box-title">Employees</h3>
                                                            <div class="Number">
                                                                <asp:LinkButton ID="lnkbtnTotEmp" runat="server" CssClass="comp_number" Text="0" OnClick="lnkbtnTotEmp_Click"></asp:LinkButton>
                                                            </div>
                                                        </div>
                                                    </li>
                                                </ul>
                                                <div class="file-export">
                                                    <asp:LinkButton ID="lnkEmp" runat="server" class="fa fa-file-export" data-toggle="tooltip" ToolTip="Export to Excel" OnClick="lnkEmp_Click"></asp:LinkButton>

                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-sm-6 col-xs-12">
                                            <div class="white-box analytics-info last-fdi">
                                                <ul class="list-inline two-part">
                                                    <li>
                                                        <div class="icon-box">
                                                            <i class="fa fa-users" aria-hidden="true"></i>
                                                        </div>
                                                        <div class="compName">
                                                            <h3 class="box-title">Products</h3>
                                                            <div class="Number">
                                                                <asp:LinkButton ID="lnkbtnProduct" runat="server" CssClass="comp_number" Text="0" OnClick="lnkbtnProduct_Click"></asp:LinkButton>
                                                            </div>
                                                        </div>
                                                    </li>
                                                </ul>
                                                <div class="file-export">
                                                    <asp:LinkButton ID="lnkProduct" runat="server" class="fa fa-file-export" data-toggle="tooltip" ToolTip="Export to Excel" OnClick="lnkProduct_Click"></asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-sm-6 col-xs-12 hidden">
                                            <div class="white-box analytics-info last-fdi">
                                                <ul class="list-inline two-part">
                                                    <li>
                                                        <div class="icon-box">
                                                            <i class="fa fa-users" aria-hidden="true"></i>
                                                        </div>
                                                        <div class="compName">
                                                            <h3 class="box-title">Total ??</h3>
                                                            <div class="Number">
                                                                <asp:LinkButton ID="lnkbtnNA" runat="server" Text="0"></asp:LinkButton>
                                                            </div>
                                                        </div>
                                                    </li>
                                                </ul>
                                                <div class="file-export">
                                                    <i class="fa fa-file-export" data-toggle="tooltip" title="Export to Excel"></i>
                                                </div>
                                            </div>
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
        <Triggers>
            <asp:PostBackTrigger ControlID="lblComp" />
            <asp:PostBackTrigger ControlID="lnkDiv" />
            <asp:PostBackTrigger ControlID="lnkUnit" />
            <asp:PostBackTrigger ControlID="lnkEmp" />
            <asp:PostBackTrigger ControlID="lnkProduct" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="up">
        <ProgressTemplate>

            <div class="overlay-progress">
                <div class="custom-progress-bar blue stripes">
                    <span></span>
                    <p>Processing</p>
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
