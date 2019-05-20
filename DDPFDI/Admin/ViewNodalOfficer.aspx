<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPage.master" CodeFile="ViewNodalOfficer.aspx.cs" Inherits="Admin_ViewNodalOfficer" %>

<asp:Content ID="headViewDesignation" runat="server" ContentPlaceHolderID="head">

    <script type="text/javascript">
        function showPopup() {
            $('#ViewNodalDetail').modal('show');
        }
    </script>

    <script>
        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        };
    </script>
</asp:Content>
<asp:Content ID="innerViewDesignation" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <asp:ScriptManager ID="sc" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="up" runat="server">

        <ContentTemplate>
            <div class="content oem-content">
                <div class="sideBg">
                    <div class="row">
                        <div class="col-md-12 padding_0">
                            <div id="divHeadPage" runat="server"></div>
                        </div>
                    </div>
                    <form method="post" class="addfdi">
                        <div class="row">
                            <div class="col-md-12">
                                <asp:HiddenField runat="server" ID="hfrole" />
                                <div class="table-wrapper">

                                    <div id="Div1" runat="server" visible="False">
                                        <div class="col-sm-4 row">
                                            <asp:TextBox ID="txtserch" runat="server" CssClass="form-cascade-control form-control" Placeholder="Type keyword to search"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-2 row">
                                            <asp:LinkButton runat="server" ID="btnsearch" CssClass="text-black btn btn-warning pull-left btn-md" OnClick="Search_Click" Text="Search"></asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>Select Company</label>
                                                <asp:DropDownList runat="server" ID="ddlcompany" CssClass="form-control form-cascade-control" AutoPostBack="True" OnSelectedIndexChanged="ddlcompany_OnSelectedIndexChanged"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-4" runat="server" id="lblselectdivison">
                                            <div class="form-group">
                                                <label>Select Division/Plant</label>
                                                <asp:DropDownList runat="server" ID="ddldivision" CssClass="form-control form-cascade-control" AutoPostBack="True" OnSelectedIndexChanged="ddldivision_OnSelectedIndexChanged"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-4" runat="server" id="lblselectunit">
                                            <div class="form-group">
                                                <label>Select Unit</label>
                                                <asp:DropDownList runat="server" ID="ddlunit" CssClass="form-control form-cascade-control" AutoPostBack="True" OnSelectedIndexChanged="ddlunit_OnSelectedIndexChanged"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="clearfix"></div>
                                <div id="Div2" class="text-center" style="font-size: 16px; margin-top: 10px;" runat="server" visible="False">
                                    Total number of  Nodal Officer :<strong>
                                        <asp:Label ID="lbltotal" runat="server" Text=""></asp:Label></strong>
                                </div>
                                <div class="clearfix"></div>
                                <div id="Div3">
                                    <asp:Button ID="btnAddNodalOfficer" runat="server" Text="Add Employee" CssClass="btn btn-primary pull-right" OnClick="btnAddNodalOfficer_Click" />

                                </div>
                                <div class="clearfix"></div>
                                <div class="table-wraper">
                                    <asp:GridView ID="gvViewNodalOfficer" runat="server" Width="100%" Class="commonAjaxTbl master-company-table table display responsive no-wrap table-hover manage-user Grid" AutoGenerateColumns="false" AllowPaging="true"
                                        PageSize="25" AllowSorting="true" OnRowCommand="gvViewDesignation_RowCommand" OnRowDataBound="gvViewNodalOfficer_RowDataBound">
                                        <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
                                        <Columns>

                                            <asp:TemplateField HeaderText="S.No.">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblnodelname" runat="server" Text='<%#Eval("NodalOficerName") %>' NullDisplayText="#" SortExpression="NodalOficerName"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="CompanyReference No." Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblnodelrefno" runat="server" Text='<%#Eval("NodalOfficerRefNo") %>' NullDisplayText="#" SortExpression="NodalOfficerRefNo"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Email">
                                                <ItemTemplate>
                                                    <asp:Label ID="nodelemail" runat="server" Text='<%#Eval("NodalOfficerEmail") %>' NullDisplayText="#" SortExpression="NodalOfficerEmail"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="IsNodalOfficer" HeaderText="Nodal Officer" NullDisplayText="#" />
                                            <asp:BoundField DataField="IsLoginActive" HeaderText="User" NullDisplayText="#" />
                                            <asp:TemplateField HeaderText="Company">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblrefno" runat="server" Text='<%#Eval("CompanyName") %>' NullDisplayText="#" SortExpression="Company"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="FactoryName" HeaderText="Division" NullDisplayText="-" />
                                            <asp:BoundField DataField="UnitName" HeaderText="Unit" NullDisplayText="-" />
                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lblview" runat="server" ToolTip="View Detail of Nodal Officer/Employee" CssClass="fa fa-eye" CommandName="ViewComp" CommandArgument='<%#Eval("NodalOfficerID") %>'></asp:LinkButton>
                                                    <asp:LinkButton ID="lbledit" runat="server" ToolTip="Edit or Update Nodal officer/Employee" CssClass="fa fa-edit" CommandName="EditComp" CommandArgument='<%#Eval("NodalOfficerID") %>'></asp:LinkButton>
                                                    <asp:LinkButton ID="lbldel" runat="server" Visible="False" CssClass="fa fa-trash" CommandName="DeleteComp" OnClientClick="return confirm('Are you sure you want to delete this Company?');" CommandArgument='<%#Eval("NodalOfficerID") %>'></asp:LinkButton>
                                                    <asp:LinkButton ID="lbllogindetail" runat="server" ToolTip="Send mail to create login" CssClass=" fa fa-paper-plane" CommandName="SendLogin" OnClientClick="return confirm('Are you sure you want to send login detail?');" CommandArgument='<%#Eval("NodalOfficerID") %>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>


                            </div>
                        </div>
                    </form>
                </div>
                <div class="footer">© 2019 <a href="#">Department of Defence Production</a> </div>
            </div>
            <div class="modal fade" id="ViewNodalDetail" role="dialog">
                <div class="modal-dialog">
                    <!-- Modal content-->
                    <div class="modal-content">
                        <div class="modal-header modal-header1">
                            <button type="button" class="close close1" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Nodal Officer Details</h4>
                        </div>
                        <form class="form-horizontal changepassword" role="form">
                            <div class="modal-body">
                                <div class="tab-pane fade active in" id="add-form">
                                    <table class="table table-bordered">
                                        <tbody>

                                            <tr>
                                                <td class="pass">Company</td>
                                                <td>
                                                    <asp:Label ID="lblcompanyname" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="pass">Division</td>
                                                <td>
                                                    <asp:Label ID="lblDivision" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="pass">Unit</td>
                                                <td>
                                                    <asp:Label ID="lblUnit" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="pass">Nodal Refernce No</td>
                                                <td>
                                                    <asp:Label ID="lblNodalOfficerRefNo" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="pass">Nodal Oficer Name</td>
                                                <td>
                                                    <asp:Label ID="lblNodalOficerName" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <%--<tr>
                                                <td class="pass">Designation</td>
                                                <td>
                                                    <asp:Label ID="lblDesignation" runat="server"></asp:Label>
                                                </td>
                                            </tr>--%>
                                            <tr>
                                                <td class="pass">Employee Code</td>
                                                <td>
                                                    <asp:Label ID="lblNodalEmpCode" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="pass">Email</td>
                                                <td>
                                                    <asp:Label ID="lblEmail" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="pass">Mobile Number</td>
                                                <td>
                                                    <asp:Label ID="lblMobile" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="pass">Telephone</td>
                                                <td>
                                                    <asp:Label ID="lblTelephone" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="pass">Fax</td>
                                                <td>
                                                    <asp:Label ID="lblFax" runat="server"></asp:Label>
                                                </td>
                                            </tr>


                                            </caption>
                                            </caption>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-primary" data-dismiss="modal">Close</button>
                            </div>
                        </form>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
