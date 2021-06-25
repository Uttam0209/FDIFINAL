<%@ Page Title="" Language="C#" MasterPageFile="~/User/MasterPage.master" AutoEventWireup="true" CodeFile="CategoryWise_Report.aspx.cs" Inherits="Report_CategoryWise_Report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href='https://ajax.googleapis.com/ajax/libs/jqueryui/1.12.1/themes/ui-lightness/jquery-ui.css' rel='stylesheet' />
</asp:Content>
<asp:Content ID="content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager runat="server" ID="sc"></asp:ScriptManager>
    <asp:UpdatePanel runat="server" ID="up" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="row d-flex justify-content-center">
                <div class="col-lg-11 d-flex justify-content-end py-3">
                </div>
            </div>
            <div id="divHeadPage" runat="server">
                <div class="contaier-fluid">
                    <div class="row d-flex justify-content-center ">
                       <div class="col-lg-11 text-center">
                            <h4>Category Wise Report</h4>
                        </div>
                    </div>
                    <div class="row d-flex justify-content-center mb-3" runat="server" visible="true">
                        <div class="col-lg-11">
                            <div class="row">
                                <asp:HiddenField runat="server" ID="hidType" />
                                <asp:HiddenField runat="server" ID="hfcomprefno" />
                                <asp:HiddenField ID="hidCompanyRefNo" runat="server" />
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Select Company</label>
                                        <asp:DropDownList runat="server" ID="ddlcompany" AutoPostBack="true" Data-toggle="tooltip" ToolTip="Its shown Company name.. " CssClass="form-control form-cascade-control" OnSelectedIndexChanged="ddlcompany_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-2" runat="server" id="lblselectdivison">
                                    <div class="form-group">
                                        <label>Select Division/Plant</label>
                                        <asp:DropDownList runat="server" ID="ddldivision" AutoPostBack="true" Data-toggle="tooltip" ToolTip="Its shown Division name.. " CssClass="form-control form-cascade-control" OnSelectedIndexChanged="ddldivision_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-2" runat="server" id="lblselectunit">
                                    <div class="form-group">
                                        <label>Select Unit</label>
                                        <asp:DropDownList runat="server" ID="ddlunit" AutoPostBack="true" Data-toggle="tooltip" ToolTip="Its shown Unit name.." CssClass="form-control form-cascade-control" OnSelectedIndexChanged="ddlunit_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Select Category</label>
                                        <asp:DropDownList runat="server" ID="DDL_Cat" Data-toggle="tooltip" ToolTip="By selecting the category of the DPSU user,  data display is the category wise." CssClass="form-control form-cascade-control"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Select Indiginized Year</label>
                                        <asp:DropDownList runat="server" ID="DDLYear" Data-toggle="tooltip" ToolTip="By selecting the Indiginized Year of the DPSU user,  data is display is the Indiginized Year wise ." CssClass="form-control form-cascade-control"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Select Month</label>
                                        <asp:DropDownList runat="server" ID="DDLMonth" Data-toggle="tooltip" ToolTip="By selecting Month of the DPSU user,  data is display is month wise"  CssClass="form-control form-cascade-control">
                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                            <asp:ListItem Value="1">January</asp:ListItem>
                                            <asp:ListItem Value="2">February</asp:ListItem>
                                            <asp:ListItem Value="3">March</asp:ListItem>
                                            <asp:ListItem Value="4">April</asp:ListItem>
                                            <asp:ListItem Value="5">May</asp:ListItem>
                                            <asp:ListItem Value="6">June</asp:ListItem>
                                            <asp:ListItem Value="7">July</asp:ListItem>
                                            <asp:ListItem Value="8">August</asp:ListItem>
                                            <asp:ListItem Value="9">September</asp:ListItem>
                                            <asp:ListItem Value="10">October</asp:ListItem>
                                            <asp:ListItem Value="11">November</asp:ListItem>
                                            <asp:ListItem Value="12">December</asp:ListItem>

                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group">
                                        <label>Search</label>
                                        <asp:TextBox ID="txtsearch" runat="server" Style="max-height: 45px; max-width: 420px;"
                                            ToolTip="search tab with all criteria using words." onblur="SaveData('txtsearch')" CssClass="form-control appended-form-control"
                                            Placeholder="Search (ProductRefNo,ProductDescription)"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-1">
                                    <div class="form-group">
                                        <asp:Button runat="server" ID="btnsearch" Style="background: #6915cf!important; color: #fff; margin-top: 30px;"
                                            CssClass="btn btn-block" Text="Search" OnClick="btnsearch_Click" />
                                        <asp:LinkButton runat="server" ID="btnclear" Text="Clear" Visible="false" OnClick="btnclear_Click"></asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row d-flex justify-content-center mb-3">
                <div class="col-lg-11">
                    <div class="row">
                        <div class="col-sm-3 mb-sm-0 mb-2 d-flex">
                            <asp:LinkButton runat="server" ID="btnExcel" CssClass="btn" Style="background: #6915cf!important; color: #fff;" OnClick="btnExcel_Click">Excel</asp:LinkButton>
                            <a href="Dashboard" class="btn" style="background: #6915cf!important; color: #fff; margin-left:10px;">Back</a>
                        </div>

                    </div>
                </div>
            </div>
            <div class="row d-flex justify-content-center">
                <div class="col-lg-11 overflow-auto" id="divhome">
                    <asp:GridView runat="server" ID="gveoi" Class="table table-hover table-bordered " AutoGenerateColumns="False"
                        CellPadding="4" GridLines="None" Width="100%">
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
                            <%--<asp:BoundField HeaderText="Company" DataField="CompanyName" NullDisplayText="NA" HeaderStyle-Width="70px" />--%>
                            <asp:TemplateField HeaderText="Division/Unit" HeaderStyle-Width="60px">
                                <HeaderTemplate>
                                           <asp:Label ID="HeaderDivision" data-toggle="tooltip" ToolTip="Display Division/Unit Name." runat="server" Text="Division/Unit"></asp:Label>
                                           </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lbldivsion" runat="server" Data-toggle="tooltip" ToolTip=" Display Division name.." NullDisplayText="NA"  Text='<%# Eval("FactoryName") %>'>
                                    </asp:Label>&nbsp;/&nbsp
                                            <asp:Label ID="lblunit" runat="server" Data-toggle="tooltip" ToolTip="Display Unit name" NullDisplayText="NA" Text='<%# Eval("UnitName") %>'>
                                            </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Item Name" HeaderStyle-Width="70px">
                                <HeaderTemplate>
                                           <asp:Label ID="HeaderItemName" data-toggle="tooltip" ToolTip="Display Product Name." runat="server" Text="Item Name"></asp:Label>
                                           </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblitemName" runat="server" Data-toggle="tooltip" ToolTip="Display Product Name" Text='<%# Eval("ProductDescription") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                           <%-- <asp:BoundField HeaderText="Item Name" DataField="ProductDescription" NullDisplayText="NA" HeaderStyle-Width="300px"></asp:BoundField>--%>
                            <asp:TemplateField HeaderText="Item Code" HeaderStyle-Width="70px">
                              <HeaderTemplate>
                                           <asp:Label ID="HeaderCode" data-toggle="tooltip" ToolTip="Display Product Number." runat="server" Text="Item Code"></asp:Label>
                                           </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblitemcode" runat="server" Data-toggle="tooltip" ToolTip="Display Product Number" Text='<%# Eval("ProductRefNo") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Make In India Category" HeaderStyle-Width="90px">
                                 <HeaderTemplate>
                                           <asp:Label ID="Headerindiacategory" data-toggle="tooltip" ToolTip="Display Sub Categories of Make In India." runat="server" Text="Make In India Category"></asp:Label>
                                           </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:HiddenField runat="server" ID="hfproc" Value='<%# Eval("PurposeofProcurement") %>' />
                                    <asp:Label ID="lblindiacategory" runat="server" Data-toggle="tooltip" ToolTip="Display Sub Categories of Make In India" Text='<%# Eval("MakeInIndiaCategory") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="EOI Status" HeaderStyle-Width="90px">
                                <HeaderTemplate>
                                           <asp:Label ID="Headereoistatus" data-toggle="tooltip" ToolTip="Display product EOI Status " runat="server" Text="EOI Status"></asp:Label>
                                           </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="eoistatus" runat="server" Data-toggle="tooltip" ToolTip="Display product EOI Status " Text='<%# Eval("EOIStatus") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Indigenization Status" HeaderStyle-Width="60px">
                               <HeaderTemplate>
                                           <asp:Label ID="HeaderIndigenization" data-toggle="tooltip" ToolTip="Display product Indigenization Status " runat="server" Text="Indigenization Status"></asp:Label>
                                           </HeaderTemplate> 
                                <ItemTemplate>
                                    <asp:Label ID="lblindegyr" runat="server" Data-toggle="tooltip" ToolTip="Display product Indigenization Status " Text='<%# Eval("CIsIndiginized") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Approx value of import negated" HeaderStyle-Width="100px">
                                <HeaderTemplate>
                                           <asp:Label ID="HeaderApprox" data-toggle="tooltip" ToolTip="Maximum Value taken of all the import values entered year wise  " runat="server" Text="Approx value of import negated"></asp:Label>
                                           </HeaderTemplate> 
                                <ItemTemplate>
                                    <asp:Label ID="lblApprox" runat="server" Data-toggle="tooltip" ToolTip="Maximum Value taken of all the import values entered year wise  " Text='<%# Eval("estimate") %>'>
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
    <script src="User/Uassets/js/jquery-3.4.1.min.js"></script>
    <%--<script src="User/Uassets/js/bootstrap.bundle.min.js"></script>--%>
    <script src="User/Uassets/js/all.min.js"></script>
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
                        url: 'Report/CategoryWise_Report.aspx/GetSearchKeyword',
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
    <script> 
        $(document).ready(function () {
            $(function () {

            });
        })
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {
                    $(".my_date_picker2").datepicker(
                        {
                            dateFormat: 'dd-M-yy'
                        });
                }
            });
        };
    </script>
</asp:Content>
