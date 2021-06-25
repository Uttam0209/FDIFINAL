<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/User/MasterPage.master" CodeFile="ProductUpdation.aspx.cs" Inherits="User_ProductUpdation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" type="text/css" href="User/Uassets/css/jquery.dataTables.min.css" />
    <style>
        #ContentPlaceHolder1_gvproductupdate th {
            color: white !important;
        }
        #ContentPlaceHolder1_div1 div{
            padding:0!important;
        }
        #firstgrid ul li a{
            text-transform:uppercase;
        }
        .table-bordered
        {
            border:none;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager runat="server" ID="sc"></asp:ScriptManager>
    <asp:UpdatePanel runat="server" ID="update" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="firstgrid" class="row d-flex justify-content-center my-2">
                <div class="col-11">
                    <ul class="nav nav-tabs" style="border-bottom: none;">
                        <li class="nav-item p-4" style="width: 250px; background: rgb(0, 102, 153); color: white; font-weight: bold; text-align: center;">
                            <asp:LinkButton runat="server" ID="lblimg" ClientIDMode="Static" Text="Image/Specification/QA" OnClick="lblimg_Click" ForeColor="White"></asp:LinkButton>
                        </li>
                        <i class="fa fa-arrow-circle-right" style="font-size: 25px; margin-top: 25px; color: darkgray;" data-toggle="tab" aria-hidden="true"></i>
                        <li class="nav-item p-4" style="width: 250px; background: #999966; color: white; font-weight: bold; text-align: center;">
                            <asp:LinkButton runat="server" ID="lblcontact" ClientIDMode="Static" Text="Contact" OnClick="lblcontact_Click" ForeColor="White"></asp:LinkButton>
                        </li>
                        <i class="fa fa-arrow-circle-right" style="font-size: 25px; margin-top: 25px; color: darkgray"
                            data-toggle="tab" aria-hidden="true"></i>
                        <li class="nav-item p-4" style="width: 250px; background: #00CC66; color: white; font-weight: bold; text-align: center;">
                            <asp:LinkButton runat="server" ID="lblindegenized" ClientIDMode="Static" OnClick="lblindegenized_Click" Text="Indegenized Status" ForeColor="White"></asp:LinkButton>
                        </li>
                    </ul>
                </div>
            </div>
            <div class="row d-flex justify-content-center my-2">
                <div class="col-11">
                    <div class="row d-flex">
                        <div class="col-3" id="updatediv" runat="server">
                            <asp:LinkButton ID="btnproupdate" runat="server" Text="Update Image/Specification/QA" OnClick="btnproupdate_Click" Style="background: #507CD1!important;" class="btn btn-primary"></asp:LinkButton>
                          
                        </div>
                        <div class="col-10 d-flex justify-content-end">
                            <asp:TextBox ID="txtsearch" runat="server" Style="max-height: 40px; max-width: 420px;"
                                ToolTip="search tab with all criteria using words." CssClass="form-control appended-form-control"
                                Placeholder="Search (type min three character)"></asp:TextBox>
                            <asp:Button runat="server" ID="btnsearch" Style="max-height: 40px; width: auto; padding: 10px; background: #6915cf!important;"
                                CssClass="btn btn-info" Text="Search" OnClick="btnsearch_Click" />
                           
                        </div>
                    </div>
                </div>
            </div>
            <div class="row d-flex justify-content-center my-2">
                <div class="col-11 overflow-auto">
                
                        <asp:GridView runat="server" ID="gvproductupdate" OnRowCreated="gvproductupdate_RowCreated" OnRowCommand="gvproductupdate_RowCommand" OnRowDataBound="gvproductupdate_RowDataBound" Class="table table-hover table-bordered table-responsive" AutoGenerateColumns="False"
                            CellPadding="4" GridLines="None" ForeColor="#333333" Style="text-align: center;">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:TemplateField HeaderText="Select Box (√)">
                                    <%--  <HeaderTemplate>
                                    <asp:CheckBox ID="chkall" runat="server" />
                                </HeaderTemplate>--%>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chckrow" Checked="false" runat="server" AutoPostBack="true" OnCheckedChanged="chckrow_CheckedChanged" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sr.No.">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Company">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblcompshow" ToolTip='<%#Eval("ProductRefNo") %>' Text='<%#Eval("CompanyName") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Division/Unit">
                                    <ItemTemplate>
                                        <asp:Label ID="lbldivsion" runat="server" Text='<%# Eval("FactoryName") %>'>
                                        </asp:Label>
                                        <asp:Label ID="lblunit" runat="server" Text='<%# Eval("UnitName") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lblitemcode" runat="server" Text='<%# Eval("ProductRefNo") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="ProductDescription" HeaderText="Item Name" NullDisplayText="NA" />
                                <asp:TemplateField HeaderText="Image">
                                    <ItemTemplate>
                                        <asp:Label ID="lblimg" runat="server">
                                        <img src='<%#Eval("TopImages") %>' alt="Product" style="max-width: 100%; width: 80%; height: 90px;"></asp:Label>
                                        <asp:FileUpload ID="fuimages" runat="server" CssClass="uploadimage form-control" AllowMultiple="true" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Specification" Visible="true">
                                    <ItemTemplate>
                                        <asp:Label ID="lblspecification" runat="server" Text='<%# Eval("FeatursandDetail") %>'></asp:Label>
                                        <asp:TextBox runat="server" ID="txtbxfeaturesanddetails" Width="100%" Text='<%# Eval("FeatursandDetail") %>' Height="100px" placeholder="Ductile,Tensile,Lusture" MaxLength="250"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="QA Agency" Visible="true">
                                    <ItemTemplate>
                                        <asp:Label ID="lblqa" runat="server" Text='<%# Eval("QAAgency") %>'></asp:Label>
                                        <asp:CheckBoxList ID="chkQAA" runat="server" RepeatColumns="2" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                            <asp:ListItem Text="CEMILAC" Value="20"></asp:ListItem>
                                            <asp:ListItem Text="DGAQA" Value="3303"></asp:ListItem>
                                            <asp:ListItem Text="DGNAI" Value="58261"></asp:ListItem>
                                            <asp:ListItem Text="DGQA" Value="3302"></asp:ListItem>
                                            <asp:ListItem Text="NIL" Value="58262"></asp:ListItem>
                                        </asp:CheckBoxList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Name" HeaderStyle-Width="150px" Visible="false">
                                    <ItemTemplate>
                                        <asp:TextBox ID="username" runat="server" CssClass="form-control" Enabled="false" Text='<%# Eval("NodalOficerName") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Mobile No." HeaderStyle-Width="70px" Visible="false">
                                    <ItemTemplate>
                                        <asp:TextBox runat="server" ID="contactno" MaxLength="12" Enabled="false" onkeydown="return onlyNos(event)" CssClass="form-control" Text='<%# Eval("NodalOfficerMobile") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Email" HeaderStyle-Width="150px" Visible="false">
                                    <ItemTemplate>
                                        <asp:TextBox ID="email" runat="server" pattern="[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,}$" CssClass="form-control" Enabled="false" Text='<%# Eval("NodalOfficerEmail") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Designation" HeaderStyle-Width="150px" Visible="false">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="designation" runat="server" CssClass="form-control" Enabled="false">
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="Indegenized Status" HeaderStyle-Width="150px" Visible="false">
                                    <ItemTemplate>
                                    <%--     <asp:HiddenField runat="server" ID="hfproc" Value='<%# Eval("PurposeofProcurement") %>' />--%>
                                     <asp:LinkButton ID="lnkindegstatus" runat="server" Text='<%# Eval("IsIndeginized") %>' CommandArgument='<%# Eval("ProductRefNo") %>' CommandName="Status"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Starting Year" HeaderStyle-Width="150px" Visible="false">
                                    <ItemTemplate>                                       
                                     <asp:Label ID="lnktarget" runat="server" Text='<%# Eval("IndTargetYear") %>' ></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="Year of Indegenization" HeaderStyle-Width="150px" Visible="false">
                                    <ItemTemplate>                                       
                                     <asp:Label ID="lblindegyear" runat="server" Text='<%# Eval("YearofIndiginization") %>' ></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="Manufacture Name" HeaderStyle-Width="150px" Visible="false">
                                    <ItemTemplate>                                       
                                     <asp:Label ID="lblmanufname" runat="server" Text='<%# Eval("ManufactureName") %>' ></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="Manufacture Address" HeaderStyle-Width="150px" Visible="false">
                                    <ItemTemplate>                                       
                                     <asp:Label ID="lblmanufadress" runat="server" Text='<%# Eval("ManufactureAddress") %>' ></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                             
                            </Columns>
                            <EditRowStyle BackColor="#2461BF" />
                            <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#EFF3FB" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#F5F7FB" />
                            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                            <SortedDescendingCellStyle BackColor="#E9EBEF" />
                            <SortedDescendingHeaderStyle BackColor="#4870BE" />
                        </asp:GridView>
                        <div class="row d-flex justify-content-center">
                            <div class="col-lg-12 my-2" id="divcontentproduct" runat="server">
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
                
         <div class="modal-quick-view modal fade" id="divupdate" role="dialog">
                <div class="modal-dialog  modal-xl" style="max-width: 500px!important; z-index: 9999999999;">
                    <asp:UpdatePanel ID="upn" runat="server" ChildrenAsTriggers="true">
                        <ContentTemplate>
                            <div class="modal-content">
                                <div runat="server" id="Div15">
                                    <div class="modal-header modal-header1 d-flex justify-content-center" style="background: #6915cf!important;">
                                        <h5 style="color: white;">Update Your Item</h5>
                                        <span id="success" runat="server"></span>
                                    </div>
                                    <div class="modal-body">
                                        <asp:HiddenField ID="hfprorefno" runat="server" />
                                        <table class="table" id="updatetable">
                                            <tr>
                                                <td>Item Code:-</td>
                                                <td>
                                                    <asp:Label ID="lblprorefcode" runat="server"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td>Starting Year:-</td>
                                                <td>
                                                    <asp:RadioButtonList ID="chkinditargetyear" runat="server" RepeatColumns="1" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                                        <asp:ListItem Value="NIL">&nbsp;NIL</asp:ListItem>
                                                        <asp:ListItem Value="2020-21">&nbsp;2020-21</asp:ListItem>
                                                        <asp:ListItem Value="2021-22">&nbsp;2021-22</asp:ListItem>
                                                        <asp:ListItem Value="2022-23">&nbsp;2022-23</asp:ListItem>
                                                        <asp:ListItem Value="2023-24">&nbsp;2023-24</asp:ListItem>
                                                        <asp:ListItem Value="2024-25">&nbsp;2024-25</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Indegenization Process Started:-</td>
                                                <td>
                                                    <asp:RadioButtonList ID="rdblindegprocess" runat="server" ClientIDMode="Static" RepeatColumns="2"
                                                        RepeatDirection="Horizontal" RepeatLayout="Flow" CausesValidation="false">
                                                        <asp:ListItem Value="Y" >Yes</asp:ListItem>
                                                        <asp:ListItem Value="N" style="margin-left: 10px;">No</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Make In India Category:-</td>
                                                <td>
                                                    <asp:RadioButtonList ID="rbIgCategory" runat="server" RepeatColumns="1" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>
                                            <tr id="itemindeg" runat="server">
                                                <td>Item Indegenized:-</td>
                                                <td>
                                                    <asp:RadioButtonList ID="rdblisindig" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                                                        RepeatLayout="Flow">
                                                        <asp:ListItem Value="Y">Yes</asp:ListItem>
                                                        <asp:ListItem Value="N" style="margin-left: 10px;">No</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>
                                            <tr runat="server" id="manuf">
                                                <td>Manufacturing Name:-</td>
                                                <td>
                                                    <asp:TextBox ID="txtmanufacturngname" runat="server" CssClass="form-control"></asp:TextBox></td>
                                            </tr>
                                            <tr runat="server" id="manufadd">
                                                <td>Manufacturing Address:-</td>
                                                <td>
                                                    <asp:TextBox ID="txtmanufacturngadress" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox></td>
                                            </tr>
                                            <tr style="width: 50px;" runat="server" id="yearindeg">
                                                <td>Year of Indegenization:- </td>
                                                <td>
                                                    <asp:DropDownList runat="server" ID="ddlyearofindiginization" TabIndex="22" class="form-control"></asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td />
                                                <td class="d-flex justify-content-end">
                                                    <asp:Button ID="btnupdate" runat="server" Text="Update" CssClass="btn btn-primary" OnClick="btnupdate_Click" Style="margin-right: 10px;" />

                                                    <asp:Button ID="btnclose" runat="server" Text="Close" CssClass="btn btn-primary" data-dismiss="modal" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnproupdate" />
        </Triggers>
    </asp:UpdatePanel>
    <script src="User/Uassets/js/jquery-3.4.1.min.js"></script>
    <script type="text/javascript">
        function showPopup2() {
            $('#divprodupdate').modal('show');
        }
    </script>
    <script type="text/javascript">
        function showPopup2() {
            $('#divupdate').modal('show');
        }
    </script>
    <script src="User/Uassets/js/jquery.dataTables.min.js"></script>
    <script type="text/javascript">
        //On Page Load
        $(function () {
            $('#ContentPlaceHolder1_gvproductupdate').dataTable({
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
                    $('#ContentPlaceHolder1_gvproductupdate').dataTable({
                        "bPaginate": false,
                        "bFilter": false,
                        "bInfo": false,
                        destroy: true
                    });
                }
            });
        };
    </script>
    <script type="text/javascript">
        function onlyNos(e, t) {
            try {
                if (window.event) {
                    var charCode = window.event.keyCode;
                }
                else if (e) {
                    var charCode = e.which;
                }
                else { return true; }
                if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57)) {
                    alert("Only number allowed");
                    return false;
                }
                return true;
            }
            catch (err) {
                alert(err.Description);
            }
        }
    </script>
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
                        url: 'User/ProductUpdation.aspx/GetSearchKeyword',
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
