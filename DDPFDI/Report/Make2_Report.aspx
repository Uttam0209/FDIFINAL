<%@ Page Title="" Language="C#" MasterPageFile="~/User/MasterPage.master" AutoEventWireup="true" CodeFile="Make2_Report.aspx.cs" Inherits="Report_Make2_Report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        #ContentPlaceHolder1_ddlcompany {
            display: flex;
            padding-left: 15px;
        }

            #ContentPlaceHolder1_ddlcompany span .box1 {
                padding: 12px 12px;
                margin-left: 15px;
            }

        .box2 {
            box-shadow: 0 0 5px;
        }
    </style>
</asp:Content>
<asp:Content ID="content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager runat="server" ID="sc"></asp:ScriptManager>
    <asp:UpdatePanel runat="server" ID="up" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="row d-flex justify-content-center">
                <div class="text-center">
                    <h4>Make-II Report Category Wise</h4>
                    <p><span class="alert" style="color: red;">Note: if DPSU user move the curser on functionality than the user get information about that particular functionality</span></p>

                </div>
                <div class="col-lg-11 d-flex justify-content-end py-3">
                    <asp:HiddenField runat="server" ID="chktargetclick" />
                    <asp:HiddenField runat="server" ID="chkindigclick" />
                    <asp:HiddenField runat="server" ID="chkcategclick" />
                    <asp:HiddenField runat="server" ID="chknameclick" />
                    <asp:HiddenField runat="server" ID="chkaddressclick" />
                </div>
                <div class="row d-flex justify-content-center ">
                    <div>
                        <asp:Button ID="btnback" runat="server" Text="Back" CssClass="btn"
                            Style="background: #6915cf!important; margin-right: 5px!important; color: white;" OnClick="btnback_Click" />
                    </div>
                </div>
            </div>
            <div id="div1" runat="server"></div>
            <div class="row d-flex justify-content-center mb-3">
                <div class="col-lg-11 mx-2">
                </div>
            </div>

            <div class="row mb-3 d-flex justify-content-center">
                <div class="col-lg-11 d-flex justify-content-center py-3 px-2">
                    <asp:HiddenField runat="server" ID="hidType" />
                    <asp:HiddenField runat="server" ID="hfcomprefno" />
                    <asp:HiddenField ID="hidCompanyRefNo" runat="server" />
                    <asp:DataList ID="ddlcompany" runat="server" RepeatColumns="1" RepeatDirection="Horizontal" RepeatLayout="Flow" OnItemCommand="ddlcompany_ItemCommand">
                        <ItemTemplate>
                            <div class="box1">
                                <asp:LinkButton runat="server" ID="lblc" ForeColor="White" data-toggle="tooltip" ToolTip="Indicates the name of Company" CommandArgument='<%#Eval("CompanyRefno") %>' CommandName="comp"><%#Eval("CompanyName") %></asp:LinkButton>
                            </div>
                        </ItemTemplate>
                    </asp:DataList>
                </div>
            </div>
            <div class="row d-flex justify-content-center">
                <div class="col-lg-11 d-flex justify-content-end py-3">
                </div>
            </div>
            <div id="divHeadPage" runat="server">
                <div class="contaier-fluid">

                    <div class="row d-flex justify-content-center mb-3" runat="server" visible="true">
                    </div>
                    <div class="row d-flex justify-content-center mb-3">
                        <div class="col-lg-11">
                            <div class="row">
                                <div class="col-sm-3 mb-sm-0 mb-2 d-flex">
                                    <asp:LinkButton runat="server" ID="btnExcel" CssClass="btn"
                                        Style="background: #6915cf!important; color: #fff;" data-toggle="tooltip" ToolTip="To download the details of Product in excel sheet press the excel button" OnClick="btnExcel_Click">Excel</asp:LinkButton>
                                </div>
                                <div class="col-sm-9 d-flex justify-content-end">
                                    <asp:TextBox ID="txtsearch" runat="server" Style="max-height: 45px; max-width: 420px;" data-toggle="tooltip"
                                        ToolTip="search tab with all criteria using words." onblur="SaveData('txtsearch')" CssClass="form-control appended-form-control"
                                        Placeholder="Search (type min three character)"></asp:TextBox>
                                    <asp:Button runat="server" ID="btnsearch" Style="background: #6915cf!important; color: #fff;"
                                        CssClass="btn" Text="Search" data-toggle="tooltip" ToolTip="Click on search button" OnClick="btnsearch_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row d-flex justify-content-center">
                        <div class="col-lg-11 overflow-auto" id="divhome">
                            <asp:GridView runat="server" ID="gveoi" Class="table table-hover table-bordered " AutoGenerateColumns="False"
                                CellPadding="4" GridLines="None" Width="100%" OnRowDataBound="gveoi_RowDataBound">
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sr.No" HeaderStyle-Width="50px">
                                        <ItemTemplate>
                                            <%#Eval("row_no") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Company" HeaderStyle-Width="60px">
                                        <HeaderTemplate>
                                            <asp:Label ID="Header1" data-toggle="tooltip" ToolTip="Display Company Name." runat="server" Text="Company"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblcompany" runat="server" Data-toggle="tooltip" ToolTip="Display Company name.." NullDisplayText="NA" Text='<%# Eval("CompanyName") %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Division/Unit" HeaderStyle-Width="60px">
                                        <HeaderTemplate>
                                            <asp:Label ID="Headerdivunit" data-toggle="tooltip" ToolTip="Display Division/Unit Name." runat="server" Text="Division/Unit"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lbldivsion" runat="server" Data-toggle="tooltip" ToolTip="Display Division name.." NullDisplayText="NA" Text='<%# Eval("FactoryName") %>'>
                                            </asp:Label>&nbsp;/&nbsp
                                            <asp:Label ID="lblunit" runat="server" Data-toggle="tooltip" ToolTip="Display Unit name" NullDisplayText="NA" Text='<%# Eval("UnitName") %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Item Name" HeaderStyle-Width="220px">
                                        <HeaderTemplate>
                                            <asp:Label ID="Headerditem" data-toggle="tooltip" ToolTip="Display Product Name" runat="server" Text="Item Name"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lblitemDesc" runat="server" data-toggle="tooltip" ToolTip='<%# Eval("ProductRefNo") %>'
                                                Text='<%# Eval("ProductDescription").ToString().Length > 35? (Eval("ProductDescription") as string).Substring(0,35) + ".." : Eval("ProductDescription")  %>' CommandArgument='<%# Eval("ProductRefNo") %>' CommandName="Product">
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Item Code" HeaderStyle-Width="70px">
                                        <HeaderTemplate>
                                            <asp:Label ID="Headerditemcode" data-toggle="tooltip" ToolTip="Display Product ID" runat="server" Text="Item Code"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblitemcode" runat="server" Data-toggle="tooltip" ToolTip="Display product Name" Text='<%# Eval("ProductRefNo") %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Portal Data" HeaderStyle-Width="60px">
                                        <HeaderTemplate>
                                            <asp:Label ID="Headerditemcode" data-toggle="tooltip" ToolTip="Display Product ID" runat="server" Text="Portal Data"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblpublic" runat="server" NullDisplayText="No" data-toggle="tooltip" ToolTip="It Display No of Make II Products on Market Place" Text='<%# Eval("IsShowGeneral") %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="MIS Data" HeaderStyle-Width="60px">
                                        <HeaderTemplate>
                                            <asp:Label ID="Headerditemcode" data-toggle="tooltip" ToolTip="Display Product ID" runat="server" Text="MIS Data"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblmis" runat="server" data-toggle="tooltip" ToolTip="It Display No of Make II Products on CMS" Text='<%# Eval("misdomain") %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Annual Import value(Rs.)" HeaderStyle-Width="60px">
                                        <HeaderTemplate>
                                            <asp:Label ID="Headerdindegyr" data-toggle="tooltip" ToolTip="Maximum Value taken of all the import values entered year wise" runat="server" Text="Annual Import value(Rs.)"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblindegyr" runat="server" NullDisplayText="0.00" data-toggle="tooltip" ToolTip="Maximum Value taken of all the import values entered year wise" Text='<%# Eval("MaximumImportValue") %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="EOI Status" HeaderStyle-Width="90px">
                                        <HeaderTemplate>
                                            <asp:Label ID="Headerdtegory" data-toggle="tooltip" ToolTip="Display the EOI status of Product" runat="server" Text="EOI Status"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblindiacategory" runat="server" data-toggle="tooltip" ToolTip="Display the EOI status of Product" Text='<%# Eval("EOIStatus") %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Supply Order Status" HeaderStyle-Width="100px">
                                        <HeaderTemplate>
                                            <asp:Label ID="Headerdmonth" data-toggle="tooltip" ToolTip="Display the Supply Order Status of Product " runat="server" Text="Supply Order Status"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblmonth" runat="server" data-toggle="tooltip" ToolTip="Display the Supply Order Status of Product " Text='<%# Eval("SupplyOrderStatus") %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Indegenization Status" HeaderStyle-Width="60px">
                                        <HeaderTemplate>
                                            <asp:Label ID="Headerdindegyrstatus" data-toggle="tooltip" ToolTip="Display the Indigenisation Status of Product " runat="server" Text="Indegenization Status"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblindegyrstatus" runat="server" data-toggle="tooltip" ToolTip="Display the Indigenisation Status of Product  " Text='<%# Eval("IsIndeginized") %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                                <EditRowStyle BackColor="#999999" />
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                            </asp:GridView>
                        </div>
                    </div>
                    <div class="row d-flex justify-content-center">
                        <div class="col-lg-11" id="divcontentproduct" runat="server">
                            <nav class="d-flex justify-content-between" aria-label="Page navigation">
                                <div class="col-6 d-flex justify-content-start">
                                    <ul class="pagination">
                                        <li class="page-item" style="background: #6915cf!important; color: white!important; padding: 8px!important; border-radius: 5px;">
                                            <span style="text-align: center;">Showing                                       
                                            <asp:Label runat="server" ID="lbltotalshowpageitem"></asp:Label>
                                                products of                                       
                                            <asp:Label ID="lbltotfilter" runat="server"></asp:Label>
                                                products  
                                            </span>
                                        </li>
                                    </ul>
                                </div>
                                <div class="col-6 d-flex justify-content-end">
                                    <ul class="pagination">
                                        <li class="page-item" style="background: #6915cf!important; color: white!important; padding: 8px!important; border-radius: 5px;">
                                            <asp:LinkButton ID="lnkbtnPgPrevious" Style="background: #6915cf!important; color: white!important; padding: 8px!important; border-radius: 5px;" runat="server" class="page-link" OnClick="lnkbtnPgPrevious_Click"><i class="fa fa-chevron-left mr-2"></i> Prev</asp:LinkButton>
                                        </li>
                                        <li class="page-item" style="background: #6915cf!important; color: white!important; padding: 8px!important; border-radius: 5px;">
                                            <asp:Label ID="lblpaging" runat="server"></asp:Label>
                                        </li>
                                        <li class="page-item" style="background: #6915cf!important; color: white!important; padding: 5px!important; border-radius: 5px;">
                                            <asp:LinkButton ID="lnkbtnPgNext" Style="background: #6915cf!important; color: white!important; padding: 8px!important; border-radius: 5px;" runat="server" class="page-link" OnClick="lnkbtnPgNext_Click"> Next<i class="fa fa-chevron-right ml-2"></i></asp:LinkButton>
                                        </li>
                                    </ul>
                                </div>
                            </nav>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExcel" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress" runat="server" AssociatedUpdatePanelID="up">
        <ProgressTemplate>
            <div class="overlay-progress">
                <div class="custom-progress-bar blue stripes">
                    <span></span>
                    <p style="margin-left: 200px; padding-bottom: 10px;">
                        <b>Processing...</b>
                    </p>
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <%--<script src="User/Uassets/js/jquery-3.4.1.min.js"></script>--%>
    <script src="User/Uassets/js/bootstrap.bundle.min.js"></script>
    <script src="User/Uassets/js/jquery.dataTables.min.js"></script>
    <script type="text/javascript">
        //On Page Load
        $(function () {
            $('#ContentPlaceHolder1_gveoi').dataTable({
                "bPaginate": false,
                "bFilter": false,
                "bInfo": false,
                destroy: true
            });
        });
        //On UpdatePanel Refresh
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {
                    $('#ContentPlaceHolder1_gveoi').dataTable({
                        "bPaginate": false,
                        "bFilter": false,
                        "bInfo": false,
                        destroy: true
                    });
                }
            });
        };
    </script>
    <script src="User/Uassets/js/jquery-ui.min.js"></script>
    <script type="text/javascript">
        $(function () {
            SetAutoComplete();
        });
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {
                    SetAutoComplete();
                }
            });
        };
        function SetAutoComplete() {
            $("[id$=txtsearch]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: 'User/S_Story2.aspx/GetSearchKeyword',
                        data: "{ 'prefix': '" + request.term + "'}",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item,
                                    val: item
                                };
                            }))
                        }
                    });
                },
                minLength: 1
            });
        }
    </script>
</asp:Content>

