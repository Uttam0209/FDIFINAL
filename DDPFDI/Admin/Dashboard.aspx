<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Dashboard.aspx.cs" Inherits="Admin_Dashboard" MasterPageFile="~/Admin/MasterPage.master" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<asp:Content ID="conhead" runat="server" ContentPlaceHolderID="head">
    <%-- Here We need to write some js code for load google chart with database data --%>
    <script src="assets/js/jquery-1.7.1.min.js"></script>
    <script src="assets/js/jsapi.js"></script>
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
                width: 545,
                height: 300,
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
    <script type="text/javascript">
        function showPopup() {
            $('#divCompany').modal('show');
        }
    </script>
    <style>
        .uploadedItem .incombox {
            margin-left: 5px;
            display: flex;
            align-items: center;
            position: relative;
        }

            .uploadedItem .incombox .Number a {
                top: 2px !important;
            }

            .uploadedItem .incombox .file-export a {
                right: -51px !important;
                bottom: -28px !important;
            }

        .ItemDivided .incombox a.comp_number {
            left: 39px !important;
            bottom: -22px !important;
            top: auto !important;
            font-size: 14px !important;
        }

        .ItemDivided {
            border-left: 1px solid #ddd;
        }

            .ItemDivided .box-title {
                font-size: 14px !important;
            }

            .ItemDivided a {
                margin-top: 0 !important;
            }

            .ItemDivided .file-export a {
                position: static !important;
            }

            .ItemDivided .Number {
                margin: 0 10px;
            }

            .ItemDivided .incombox {
                border-bottom: 1px solid #ddd;
            }

                .ItemDivided .incombox:last-child {
                    border-bottom: 0;
                }
    </style>
</asp:Content>
<asp:Content ID="coninner" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <asp:updatepanel id="up" runat="server">
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
                                                    <asp:LinkButton ID="lblComp" Visible="true" runat="server" class="fas fa-cloud-download-alt" data-toggle="tooltip" ToolTip="Export to Excel" OnClick="lblComp_Click"></asp:LinkButton>

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
                                                    <asp:LinkButton ID="lnkDiv" runat="server" Visible="true" class="fas fa-cloud-download-alt" data-toggle="tooltip" ToolTip="Export to Excel" OnClick="lnkDiv_Click"></asp:LinkButton>
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
                                                    <asp:LinkButton ID="lnkUnit" runat="server" Visible="true" class="fas fa-cloud-download-alt" data-toggle="tooltip" ToolTip="Export to Excel" OnClick="lnkUnit_Click"></asp:LinkButton>
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
                                                    <asp:LinkButton ID="lnkEmp" runat="server" Visible="true" class="fas fa-cloud-download-alt" data-toggle="tooltip" ToolTip="Export to Excel" OnClick="lnkEmp_Click"></asp:LinkButton>

                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" style="margin-top: 10px;">

                                        <div class="col-lg-6 col-sm-6 col-xs-12">
                                            <div class="white-box analytics-info last-fdi" style="padding: 2%">
                                                <ul class="list-inline two-part">
                                                    <li class="uploadedItem">
                                                        <div class="row" style="width:100%;">
                                                            <div class="col-md-6">
                                                                <div class="allItem">
                                                                <div class="incombox">
                                                            <div class="icon-box">
                                                                <i class="fab fa-product-hunt"></i>
                                                            </div>
                                                            <div class="compName">
                                                                <h3 class="box-title">Items Uploaded</h3>
                                                                <div class="Number">
                                                                    <asp:LinkButton ID="lnkbtnProduct" runat="server" CssClass="comp_number" Text="0" OnClick="lnkbtnProduct_Click"></asp:LinkButton>
                                                                </div>
                                                                <div class="file-export">
                                                                    <asp:LinkButton ID="lnkProduct" runat="server" Visible="true" class="fas fa-cloud-download-alt" data-toggle="tooltip" ToolTip="Export to Excel" OnClick="lnkProduct_Click"></asp:LinkButton>
                                                                </div>
                                                            </div>
                                                        </div>
                                                          </div>
                                                            </div>
                                                            <div class="col-md-6">
                                                                <div class="ItemDivided">
                                                            <div class="incombox">
                                                                <div class="icon-box" style="width:25px; height:25px; line-height:25px">
                                                                    <i class="fab fa-product-hunt"></i>
                                                                </div>
                                                                <div class="compName">
                                                                    <h3 class="box-title">Items Approved</h3>
                                                                </div>
                                                                <div class="Number">
                                                                        <asp:LinkButton ID="lbapproveditem" runat="server" CssClass="" Text="0" OnClick="lnkbtnProduct_Click"></asp:LinkButton>
                                                                    </div>
                                                                    <div class="file-export">
                                                                        <asp:LinkButton ID="lbdownloadapproved" runat="server" Visible="true" class="fas fa-cloud-download-alt" data-toggle="tooltip" ToolTip="Export to Excel" OnClick="lbdownloadapproved_Click"></asp:LinkButton>
                                                                    </div>
                                                            </div>                                                   
                                                            <div class="incombox">
                                                                <div class="icon-box" style="width:25px; height:25px; line-height:25px">
                                                                    <i class="fab fa-product-hunt"></i>
                                                                </div>
                                                                <div class="compName">
                                                                    <h3 class="box-title">Items Disapproved</h3>
                                                                    
                                                                </div>
                                                                <div class="Number">
                                                                        <asp:LinkButton ID="lbitemdisapproved" runat="server" CssClass="" Text="0" OnClick="lnkbtnProduct_Click"></asp:LinkButton>
                                                                    </div>
                                                                    <div class="file-export">
                                                                        <asp:LinkButton ID="lbitemdisapproveddown" runat="server" Visible="true" class="fas fa-cloud-download-alt" data-toggle="tooltip" ToolTip="Export to Excel" OnClick="lbitemdisapproveddown_Click"></asp:LinkButton>
                                                                    </div>
                                                            </div>
                                                        </div>
                                                            </div>
                                                        </div>
                                                        
                                                        
                                                      
                                                    </li>
                                                </ul>

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
                                                    <asp:LinkButton ID="LinkButton2" runat="server" Visible="true" class="fas fa-cloud-download-alt" data-toggle="tooltip" ToolTip="Export to Excel" OnClick="LinkButton2_Click"></asp:LinkButton>
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
                                                            <i class="fa fa-users" aria-hidden="true"></i>
                                                        </div>
                                                        <div class="compName">
                                                            <h3 class="box-title">Items MakeII</h3>
                                                            <div class="Number">
                                                                <asp:LinkButton ID="lbitemmake2" runat="server" CssClass="comp_number" Text="0" OnClick="lbitemmake2_Click"></asp:LinkButton>
                                                            </div>
                                                        </div>
                                                    </li>
                                                </ul>
                                                <div class="file-export">
                                                    <asp:LinkButton ID="lbexportmake2" runat="server" Visible="true" class="fas fa-cloud-download-alt" data-toggle="tooltip" ToolTip="Export to Excel" OnClick="lbexportmake2_Click"></asp:LinkButton>
                                                </div>
                                            </div>

                                        </div>
                                        <div class="col-lg-6 col-sm-6 col-xs-12">
                                            <div class="white-box analytics-info last-fdi">
                                                <ul class="list-inline two-part">
                                                    <li>
                                                        <div class="icon-box">
                                                            <i class="fab fa-product-hunt"></i>
                                                        </div>
                                                        <div class="compName">
                                                            <h3 class="box-title">Items Uploaded With Photos</h3>
                                                            <div class="Number">
                                                                <asp:Label ID="lbitemphoto" runat="server" CssClass="comp_number" Text="0"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </li>
                                                </ul>
                                                <div class="file-export">
                                                    <asp:Label ID="lbexportphoto" runat="server" Visible="true" data-toggle="tooltip" ToolTip="Export to Excel"></asp:Label>
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
                                                            <h3 class="box-title">Items Uploaded Without Photos</h3>
                                                            <div class="Number">
                                                                <asp:Label ID="lbitemwithoutphoto" runat="server" CssClass="comp_number" Text="0"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </li>
                                                </ul>
                                                <div class="file-export">
                                                    <asp:Label ID="lbexportwithoutphoto" runat="server" Visible="true" data-toggle="tooltip" ToolTip="Export to Excel"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-6 col-sm-6 col-xs-12" runat="server" id="divven" visible="false">
                                            <div class="white-box analytics-info last-fdi">
                                                <ul class="list-inline two-part">
                                                    <li>
                                                        <div class="icon-box">
                                                            <i class="fa fa-people-carry"></i>
                                                        </div>
                                                        <div class="compName">
                                                            <h3 class="box-title">Vendor</h3>
                                                            <div class="Number">
                                                                <asp:LinkButton ID="lblvendor" runat="server" CssClass="comp_number" Text="0" OnClick="lblvendor_Click"></asp:LinkButton>
                                                            </div>
                                                        </div>
                                                    </li>
                                                </ul>
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
                                                <div id="chart_div">
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-6 col-sm-6 col-xs-12">
                                            <div class="white-box analytics-info last-fdi">
                                                <div id="chart_div1">
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" style="margin-top: 20px;">
                                        <div class="col-lg-12 col-sm-6 col-xs-12">
                                            <div class="shadow-box analytics-info last-fdi">
                                                <div class="table-wrapper table-responsive" id="divfactorygrid" runat="server">
                                                    <asp:GridView ID="gvPrdoct" runat="server" AutoGenerateColumns="false" Class="commonAjaxTbl dataTable master-company-table ViewProductTable table 
                                                       display responsive no-wrap table-hover manage-user Grid table-responsive"
                                                        OnRowCreated="gvPrdoct_RowCreated" OnRowCommand="gvPrdoct_RowCommand">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="View">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton runat="server" ID="lblviewcompprod" CssClass="fa fa-eye" CommandName="ViewComp" CommandArgument='<%#Eval("CompanyRefNo") %>'></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="S.No.">
                                                                <ItemTemplate>
                                                                    <%#Container.DataItemIndex+1 %>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ORGANIZATION">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton runat="server" ID="lblorg" Text='<%#Eval("CompName")%>' NullDisplayText="#" CommandName="compgraph" CommandArgument='<%#Eval("CompanyRefNo") %>'></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="TotCompProd" HeaderText="Company ITEMS" NullDisplayText="#" />
                                                            <asp:TemplateField HeaderText="Division ITEMS">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lbldivprod" runat="server" Text='<%#Eval("TotDivProd") %>' ToolTip="After click on count you will get company division wise item upload list" NullDisplayText="#" CommandName="divprod" CommandArgument='<%#Eval("CompanyRefNo") %>'></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Unit ITEMS">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lblunitprod" runat="server" Text='<%#Eval("TotUnitProd") %>' ToolTip="After click on count you will get company unit wise item upload list" NullDisplayText="#" CommandArgument='<%#Eval("CompanyRefNo") %>' CommandName="unitprod"></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="TotalProd" HeaderText="Total ITEMS" NullDisplayText="#" />
                                                            <asp:BoundField DataField="IsIndiginised" HeaderText="ITEMS INDIGENIZED" NullDisplayText="#" />
                                                            <asp:BoundField DataField="IsMake2" HeaderText="MAKE II" NullDisplayText="#" />
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal fade" id="divCompany" role="dialog">
                            <div class="modal-dialog">
                                <!-- Modal content-->
                                <div class="modal-content">
                                    <div class="modal-header modal-header1">
                                        <button type="button" class="close close1" data-dismiss="modal">&times;</button>
                                        <h4 class="modal-title">Item Uploded Detail</h4>
                                    </div>
                                    <form class="form-horizontal changepassword" role="form">
                                        <div class="modal-body">
                                            <div class="tab-pane fade active in" id="add-form">
                                                <asp:GridView ID="gvcount" runat="server" AutoGenerateColumns="false" Class="commonAjaxTbl dataTable master-company-table ViewProductTable table 
                                        display responsive no-wrap table-hover manage-user Grid table-responsive"
                                                    OnRowCreated="gvcount_RowCreated">
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <%#Container.DataItemIndex+1 %>
                                                                <asp:HiddenField ID="hfcompref" runat="server" Value='<%#Eval("RefNo") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="Name" HeaderText="Name" NullDisplayText="#" />
                                                        <asp:BoundField DataField="TotProd" HeaderText="Total Item Uploaded" NullDisplayText="#" />
                                                    </Columns>
                                                </asp:GridView>
                                            </div>

                                        </div>
                                        <div class="modal-footer">
                                            <button type="button" class="btn btn-primary" data-dismiss="modal">Close</button>
                                        </div>
                                    </form>
                                </div>
                            </div>
                        </div>
                    </form>
                </div>
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
            <asp:PostBackTrigger ControlID="lbexportmake2" />
            <asp:PostBackTrigger ControlID="LinkButton2" />
        </Triggers>
    </asp:updatepanel>
    <asp:updateprogress id="UpdateProgress1" runat="server" associatedupdatepanelid="up">
        <ProgressTemplate>

            <div class="overlay-progress">
                <div class="custom-progress-bar blue stripes">
                    <span></span>
                    <p>Processing</p>
                </div>
            </div>
        </ProgressTemplate>
    </asp:updateprogress>
</asp:Content>
