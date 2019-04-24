<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DetailofMasterCompany.aspx.cs" Inherits="Admin_DetailofMasterCompany" MasterPageFile="~/Admin/MasterPage.master" %>

<asp:Content ID="head" runat="server" ContentPlaceHolderID="head">

    <script type="text/javascript">
        function showPopup() {
            $('#changePass').modal('show');
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
<asp:Content ID="inner" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <%-- <asp:ScriptManager ID="sc" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="up" runat="server">
        <ContentTemplate>--%>
    <div class="content oem-content">
        <div class="sideBg">
            <div class="col-mod-12">
                <ul class="breadcrumb">
                    <asp:Label ID="lblPageName" runat="server" Text=""></asp:Label></li>
                </ul>
            </div>
            <form method="post" class="addfdi">
                <div class="row">
                    <div class="col-md-12">
                        <asp:HiddenField runat="server" ID="hfrole" />
                        <div class="table-wrapper">
                            <br />
                            <div runat="server" visible="False">
                                <div class="col-sm-4 row">
                                    <asp:TextBox ID="txtserch" runat="server" CssClass="form-cascade-control form-control" Placeholder="Type keyword to search"></asp:TextBox>
                                </div>
                                <div class="col-sm-2 row">
                                    <asp:LinkButton runat="server" ID="btnsearch" class="text-black btn btn-warning pull-left btn-md" OnClick="Search_Click" Text="Search"></asp:LinkButton>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-4">
                                    <label>Select Company</label>
                                    <asp:DropDownList runat="server" ID="ddlcompany" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlcompany_OnSelectedIndexChanged"></asp:DropDownList>
                                </div>
                                <div class="col-sm-4">
                                    <label runat="server" id="lblselectdivison" Visible="False">Select Division/Plant</label>
                                    <asp:DropDownList runat="server" ID="ddldivision" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddldivision_OnSelectedIndexChanged"></asp:DropDownList>
                                </div>
                                <div class="col-sm-4">
                                    <label runat="server" id="lblselectunit" Visible="False">Select Unit</label>
                                    <asp:DropDownList runat="server" ID="ddlunit" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlunit_OnSelectedIndexChanged"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="clearfix"></div>
                        <div class="text-center" style="font-size: 16px; margin-top: 10px;" runat="server" visible="False">
                            Total number of  active companies :<strong>
                                <asp:Label ID="lbltotal" runat="server" Text=""></asp:Label></strong>
                        </div>
                        <div class="clearfix"></div>
                        <div class="table-wraper">
                            <asp:GridView ID="gvcompanydetail" runat="server" Width="100%" Class="commonAjaxTbl table display responsive no-wrap table-hover manage-user Grid" AutoGenerateColumns="false" AllowPaging="true"
                                OnPageIndexChanging="OnPageIndexChanging" PageSize="25" AllowSorting="true" OnSorting="OnSorting" OnRowCommand="gvcompanydetail_RowCommand" OnRowDataBound="OnRowDataBound">
                                <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <div class="acordian-table">
                                                <i class="toggle-table-minus fa fa-minus" aria-hidden="true" style="display: none"></i>
                                                <i class="toggle-table-plus fa fa-plus" aria-hidden="true"></i>
                                                <asp:Panel ID="pnlOrders" runat="server" Style="display: none">
                                                    <asp:GridView ID="gvfactory" runat="server" AutoGenerateColumns="false" Class="table table-hover ChildGrid" OnRowDataBound="gvfactory_OnRowDataBound">
                                                        <Columns>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <div class="acordian-table">
                                                                        <i class="toggle-table-minus fa fa-minus" aria-hidden="true" style="display: none"></i>
                                                                        <i class="toggle-table-plus fa fa-plus" aria-hidden="true"></i>
                                                                        <asp:Panel ID="pnlunit" runat="server" Style="display: none">
                                                                            <asp:GridView ID="gvunit" runat="server" AutoGenerateColumns="false" Class="table table-hover ChildGrid">
                                                                                <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
                                                                                <Columns>
                                                                                    <asp:TemplateField HeaderText="S.No">
                                                                                        <ItemTemplate>
                                                                                            <%#Container.DataItemIndex+1 %>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:BoundField ItemStyle-Width="150px" DataField="UnitRefNo" HeaderText="UnitRefNo" />
                                                                                    <asp:BoundField ItemStyle-Width="150px" DataField="UnitName" HeaderText="UnitName" />
                                                                                    <asp:BoundField runat="server" DataField="Role" HeaderText="Role" />
                                                                                    <asp:TemplateField HeaderText="Nodel Person Email">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblunitnodelname" runat="server" Text='<%#Eval("UnitEmailId") %>' NullDisplayText="#" SortExpression="ContactPersonEmailID"></asp:Label>
                                                                                            <asp:HiddenField runat="server" ID="hfunitemail" Value='<%#Eval("UnitEmailId") %>' />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Action">
                                                                                        <ItemTemplate>
                                                                                            <asp:LinkButton ID="lblviewunit" runat="server" CssClass="fa fa-eye" CommandName="ViewComp" CommandArgument='<%#Eval("UnitRefNo") %>'></asp:LinkButton>
                                                                                            <asp:LinkButton ID="lbleditunit" runat="server" CssClass="fa fa-edit" CommandName="EditComp" CommandArgument='<%#Eval("UnitRefNo") %>'></asp:LinkButton>
                                                                                            <asp:LinkButton ID="lbldelunit" runat="server" CssClass="fa fa-trash" CommandName="DeleteComp" CommandArgument='<%#Eval("UnitRefNo") %>'></asp:LinkButton>
                                                                                            <asp:LinkButton ID="lbllogindetailunit" Visible="False" runat="server" CssClass=" fa fa-paper-plane" CommandName="SendLogin" CommandArgument='<%#Eval("UnitRefNo") %>'></asp:LinkButton>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                </Columns>
                                                                            </asp:GridView>
                                                                        </asp:Panel>
                                                                    </div>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="S.No">
                                                                <ItemTemplate>
                                                                    <%#Container.DataItemIndex+1 %>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="FactroyRefNo">
                                                                <ItemTemplate>
                                                                    <asp:Label runat="server" ID="lblfactoryrefno" Text='<%#Eval("FactoryRefNo") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField ItemStyle-Width="150px" DataField="FactoryName" HeaderText="FactoryName" />
                                                            <asp:BoundField runat="server" DataField="Role" HeaderText="Role" />
                                                            <asp:TemplateField HeaderText="Nodel Person Email">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblfactorynodelname" runat="server" Text='<%#Eval("FactoryEmailId") %>' NullDisplayText="#" SortExpression="ContactPersonEmailID"></asp:Label>
                                                                    <asp:HiddenField runat="server" ID="hffactoryemail" Value='<%#Eval("FactoryEmailId") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Action">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lblviewfactory" runat="server" CssClass="fa fa-eye" CommandName="ViewComp" CommandArgument='<%#Eval("FactoryRefNo") %>'></asp:LinkButton>
                                                                    <asp:LinkButton ID="lbleditfactory" runat="server" CssClass="fa fa-edit" CommandName="EditComp" CommandArgument='<%#Eval("FactoryRefNo") %>'></asp:LinkButton>
                                                                    <asp:LinkButton ID="lbldelfactory" runat="server" CssClass="fa fa-trash" CommandName="DeleteComp" CommandArgument='<%#Eval("FactoryRefNo") %>'></asp:LinkButton>
                                                                    <asp:LinkButton ID="lbllogindetailfactory" runat="server" Visible="False" CssClass=" fa fa-paper-plane" CommandName="SendLogin" CommandArgument='<%#Eval("FactoryRefNo") %>'></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                        </Columns>
                                                    </asp:GridView>
                                                </asp:Panel>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="S.No">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Reference No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblrefno" runat="server" Text='<%#Eval("CompanyRefNo") %>' NullDisplayText="#" SortExpression="CompanyRefNo"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="CompanyName" HeaderText="Company" NullDisplayText="#" SortExpression="CompanyName" />
                                    <asp:BoundField runat="server" DataField="Role" HeaderText="Role" />
                                    <asp:TemplateField HeaderText="Nodel Person Email">
                                        <ItemTemplate>
                                            <asp:Label ID="lblnodelname" runat="server" Text='<%#Eval("ContactPersonEmailID") %>' NullDisplayText="#" SortExpression="ContactPersonEmailID"></asp:Label>
                                            <asp:HiddenField runat="server" ID="hfemail" Value='<%#Eval("ContactPersonEmailID") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lblview" runat="server" CssClass="fa fa-eye" CommandName="ViewComp" CommandArgument='<%#Eval("CompanyID") %>'></asp:LinkButton>
                                            <asp:LinkButton ID="lbledit" runat="server" CssClass="fa fa-edit" CommandName="EditComp" CommandArgument='<%#Eval("CompanyID") %>'></asp:LinkButton>
                                            <asp:LinkButton ID="lbldel" runat="server" CssClass="fa fa-trash" CommandName="DeleteComp" CommandArgument='<%#Eval("CompanyID") %>'></asp:LinkButton>
                                            <asp:LinkButton ID="lbllogindetail" runat="server" Visible="False" CssClass=" fa fa-paper-plane" CommandName="SendLogin" CommandArgument='<%#Eval("CompanyID") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </form>
        </div>

    </div>
    <div class="footer">© 2019 <a href="#">Department of Defence Production</a> </div>

    <div class="modal fade" id="changePass" role="dialog">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header modal-header1">
                    <button type="button" class="close close1" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Company Detail</h4>
                </div>
                <form class="form-horizontal changepassword" role="form">
                    <div class="modal-body">
                        <div class="tab-pane fade active in" id="add-form">
                            <table class="table table-bordered">
                                <tbody>
                                    <caption>

                                        <tr>
                                            <td class="pass" width="30%">Joint Venture </td>
                                            <td>
                                                <asp:Label ID="lbljointventure" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <caption>

                                            <tr>
                                                <td class="pass">Defence Activities </td>
                                                <td>
                                                    <asp:Label ID="lbldefactivity" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="pass" width="30%">Reference No </td>
                                                <td>
                                                    <asp:Label ID="lblrefno" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="pass">Company Name</td>
                                                <td>
                                                    <asp:Label ID="lblcompanyname" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="pass">Address</td>
                                                <td>
                                                    <asp:Label ID="lbladdress" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="pass">State</td>
                                                <td>
                                                    <asp:Label ID="lblstate" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="pass">Pin Code</td>
                                                <td>
                                                    <asp:Label ID="lblpincode" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="pass">CEO Name</td>
                                                <td>
                                                    <asp:Label ID="lblceoname" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="pass">CEO Email</td>
                                                <td>
                                                    <asp:Label ID="lblceoemail" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="pass">Contact Person Name</td>
                                                <td>
                                                    <asp:Label ID="lblcontactpersonname" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="pass">Contact Person Contact No</td>
                                                <td>
                                                    <asp:Label ID="lblcontactpersonmobno" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="pass">Contact Person Email ID</td>
                                                <td>
                                                    <asp:Label ID="lblcontactperemailid" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="pass">CIN No</td>
                                                <td>
                                                    <asp:Label ID="lblcinno" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="pass">PAN No</td>
                                                <td>
                                                    <asp:Label ID="lblpanno" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <%-- <tr>
                                                        <td class="pass">GST No (If you use multiple gst no use with comma seprated)</td>
                                                        <td>
                                                            <asp:Label ID="lblgstno" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>--%>
                                        </caption>
                                    </caption>
                                </tbody>
                            </table>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-warning" data-dismiss="modal">Cancel</button>
                    </div>
                </form>
            </div>
        </div>
    </div>
    </div>
        <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>

