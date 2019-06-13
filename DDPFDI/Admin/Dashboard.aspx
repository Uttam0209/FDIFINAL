<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Dashboard.aspx.cs" Inherits="Admin_Dashboard" MasterPageFile="~/Admin/MasterPage.master" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<asp:Content ID="conhead" runat="server" ContentPlaceHolderID="head">
    <%-- Here We need to write some js code for load google chart with database data --%>
    <script src="assets/js/jquery-1.7.1.js"></script>
    <script type="text/javascript" src="https://www.google.com/jsapi"></script>

    <script>
        var chartData; // globar variable for hold chart data
        google.load("visualization", "1", { packages: ["corechart"] });

        // Here We will fill chartData

        $(document).ready(function () {

            $.ajax({
                url: "Admin/Dashboard.aspx/GetChartData",
                data: "",
                dataType: "json",
                type: "POST",
                contentType: "application/json; chartset=utf-8",
                success: function (data) {
                    chartData = data.d;
                },
                error: function () {
                    alert("Error loading data! Please try again.");
                }
            }).done(function () {
                // after complete loading data
                google.setOnLoadCallback(drawChart);
                drawChart();
            });
        });

        function drawChart() {
            var data = google.visualization.arrayToDataTable(chartData);

            var options = {
                pointSize: 5,
                seriesType: "bars",
                series: { 3: { type: "line" } }
            };

            var pieChart = new google.visualization.PieChart(document.getElementById('chart_div'));
            pieChart.draw(data, options);

            var lineChart = new google.visualization.ComboChart(document.getElementById('chart_div1'));
            lineChart.draw(data, options);

        }
    </script>

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
                                        <div class="col-lg-3 col-sm-6 col-xs-12">
                                            <div class="white-box analytics-info total-comp">
                                                <ul class="list-inline two-part">
                                                    <li>
                                                        <div class="icon-box">
                                                            <i class="far fa-building"></i>
                                                        </div>
                                                        <div class="compName">
                                                            <h3 class="box-title">Organization</h3>
                                                            <div class="Number">
                                                                <asp:LinkButton ID="lnkbtnTotComp" CssClass="comp_number" runat="server" Text="0" OnClick="lnkbtnTotComp_Click"></asp:LinkButton>
                                                            </div>
                                                        </div>
                                                    </li>
                                                </ul>
                                                <div class="file-export">
                                                    <asp:LinkButton ID="lblComp" Visible="true" runat="server" class="fa fa-file-export" data-toggle="tooltip" ToolTip="Export to Excel" OnClick="lblComp_Click"></asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                        <!--- box End----->
                                        <div class="col-lg-3 col-sm-6 col-xs-12">
                                            <div class="white-box analytics-info total-fdi">
                                                <ul class="list-inline two-part">
                                                    <li>
                                                        <div class="icon-box">
                                                            <i class="far fa-building"></i>
                                                        </div>
                                                        <div class="compName">
                                                            <h3 class="box-title">org-Divsions</h3>
                                                            <div class="Number">
                                                                <asp:LinkButton ID="lnkbtnTotDiv" runat="server" CssClass="comp_number" Text="0" OnClick="lnkbtnTotDiv_Click"></asp:LinkButton>
                                                            </div>
                                                        </div>
                                                    </li>
                                                </ul>
                                                <div class="file-export">
                                                    <asp:LinkButton ID="lnkDiv" runat="server" Visible="true" class="fa fa-file-export" data-toggle="tooltip" ToolTip="Export to Excel" OnClick="lnkDiv_Click"></asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-3 col-sm-6 col-xs-12">
                                            <div class="white-box analytics-info last-fdi">
                                                <ul class="list-inline two-part">
                                                    <li>
                                                        <div class="icon-box">
                                                            <i class="far fa-building"></i>
                                                        </div>
                                                        <div class="compName">
                                                            <h3 class="box-title">org-Units</h3>
                                                            <div class="Number">
                                                                <asp:LinkButton ID="lnkbtnTotUnit" runat="server" CssClass="comp_number" Text="0" OnClick="lnkbtnTotUnit_Click"></asp:LinkButton>
                                                            </div>
                                                        </div>
                                                    </li>
                                                </ul>
                                                <div class="file-export">
                                                    <asp:LinkButton ID="lnkUnit" runat="server" Visible="true" class="fa fa-file-export" data-toggle="tooltip" ToolTip="Export to Excel" OnClick="lnkUnit_Click"></asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-3 col-sm-6 col-xs-12">
                                            <div class="white-box analytics-info last-fdi">
                                                <ul class="list-inline two-part">
                                                    <li>
                                                        <div class="icon-box">
                                                            <i class="fa fa-users" aria-hidden="true"></i>
                                                        </div>
                                                        <div class="compName">
                                                            <h3 class="box-title">org-Employee</h3>
                                                            <div class="Number">
                                                                <asp:LinkButton ID="lnkbtnTotEmp" runat="server" CssClass="comp_number" Text="0" OnClick="lnkbtnTotEmp_Click"></asp:LinkButton>
                                                            </div>
                                                        </div>
                                                    </li>
                                                </ul>
                                                <div class="file-export">
                                                    <asp:LinkButton ID="lnkEmp" runat="server" Visible="true" class="fa fa-file-export" data-toggle="tooltip" ToolTip="Export to Excel" OnClick="lnkEmp_Click"></asp:LinkButton>

                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" style="margin-top: 10px;">

                                        <div class="col-lg-6 col-sm-6 col-xs-12">
                                            <div class="white-box analytics-info last-fdi">
                                                <ul class="list-inline two-part">
                                                    <li>
                                                        <div class="icon-box">
                                                            <i class="fab fa-product-hunt"></i>
                                                        </div>
                                                        <div class="compName">
                                                            <h3 class="box-title">Items Uploaded</h3>
                                                            <div class="Number">
                                                                <asp:LinkButton ID="lnkbtnProduct" runat="server" CssClass="comp_number" Text="0" OnClick="lnkbtnProduct_Click"></asp:LinkButton>
                                                            </div>
                                                        </div>
                                                    </li>
                                                </ul>
                                                <div class="file-export">
                                                    <asp:LinkButton ID="lnkProduct" runat="server" Visible="true" class="fa fa-file-export" data-toggle="tooltip" ToolTip="Export to Excel" OnClick="lnkProduct_Click"></asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-6 col-sm-6 col-xs-12">
                                            <div class="white-box analytics-info last-fdi">
                                                <ul class="list-inline two-part">
                                                    <li>
                                                        <div class="icon-box">
                                                            <i class="fa fa-users" aria-hidden="true"></i>
                                                        </div>
                                                        <div class="compName">
                                                            <h3 class="box-title">Items Indigenized</h3>
                                                            <div class="Number">
                                                                <asp:LinkButton ID="lnkbtnIndigenizedProduct" runat="server" CssClass="comp_number" Text="0" OnClick="lnkbtnIndigenizedProduct_Click"></asp:LinkButton>
                                                            </div>
                                                        </div>
                                                    </li>
                                                </ul>
                                                <div class="file-export">
                                                    <asp:LinkButton ID="LinkButton2" runat="server" Visible="true" class="fa fa-file-export" data-toggle="tooltip" ToolTip="Export to Excel" OnClick="lnkProduct_Click"></asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row" style="margin-top: 20px;">
                                        <div class="col-lg-12 col-sm-6 col-xs-12">

                                            <h4 class="box-title">ORGANIZATION PERFORMANCE ON ITEMS UPLOADED</h4>

                                        </div>
                                    </div>

                                    <div class="row" style="margin-top: 5px;">

                                        <div class="col-lg-6 col-sm-6 col-xs-12">

                                            <div class="white-box analytics-info last-fdi">

                                                <div id="chart_div" style="width: 400px; height: 200px">
                                                </div>

                                            </div>
                                        </div>
                                        <%--</div>
                                         <div class="row" style="margin-top: 10px;">--%>
                                        <div class="col-lg-6 col-sm-6 col-xs-12">
                                            <div class="white-box analytics-info last-fdi">
                                                <div id="chart_div1" style="width: 400px; height: 200px">
                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" style="margin-top: 20px;">
                                        <div class="col-lg-6 col-sm-6 col-xs-12">
                                            <div class="white-box analytics-info last-fdi">

                                                <div class="table-wrapper table-responsive" id="divfactorygrid" runat="server">
                                                    <asp:GridView ID="gvPrdoct" runat="server" AutoGenerateColumns="false" Class="commonAjaxTbl master-company-table ViewProductTable table 
                                        display responsive no-wrap table-hover manage-user Grid table-responsive">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="S.No.">
                                                                <ItemTemplate>
                                                                    <%#Container.DataItemIndex+1 %>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="CompName" HeaderText="ORGANIZATION" NullDisplayText="#" />

                                                            <asp:BoundField DataField="TotalProd" HeaderText="Total ITEMS" NullDisplayText="#" />
                                                            <asp:BoundField DataField="IsIndiginised" HeaderText="ITEMS INDIGENIZED" NullDisplayText="#" />
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>
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
