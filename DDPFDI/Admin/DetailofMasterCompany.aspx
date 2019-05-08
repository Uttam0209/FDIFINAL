<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DetailofMasterCompany.aspx.cs" Inherits="Admin_DetailofMasterCompany" MasterPageFile="~/Admin/MasterPage.master" %>

<asp:Content ID="head" runat="server" ContentPlaceHolderID="head">
    <script type="text/javascript">
        function showPopup() {
            $('#changePass').modal('show');
        }
    </script>
    <script type="text/javascript">
        function showPopup1() {
            $('#divfactoryshow').modal('show');
        }
    </script>
    <script type="text/javascript">
        function showPopup2() {
            $('#divunitshow').modal('show');
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
                                            <label runat="server" id="lblselectdivison" visible="False">Select Division/Plant</label>
                                            <asp:DropDownList runat="server" ID="ddldivision" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddldivision_OnSelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                        <div class="col-sm-4">
                                            <label runat="server" id="lblselectunit" visible="False">Select Unit</label>
                                            <asp:DropDownList runat="server" ID="ddlunit" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlunit_OnSelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="clearfix"></div>
                                <div id="Div1" class="text-center" style="font-size: 16px; margin-top: 10px;" runat="server" visible="False">
                                    Total number of  active companies :<strong>
                                        <asp:Label ID="lbltotal" runat="server" Text=""></asp:Label></strong>
                                </div>

                                <div class="clearfix"></div>
                                <div class="table-wraper">
                                    <asp:GridView ID="gvcompanydetail" runat="server" Width="100%" Class="commonAjaxTbl master-company-table table display responsive no-wrap table-hover manage-user Grid" AutoGenerateColumns="false" AllowPaging="true"
                                        OnPageIndexChanging="OnPageIndexChanging" PageSize="25" AllowSorting="true" OnSorting="OnSorting" OnRowCommand="gvcompanydetail_RowCommand" OnRowDataBound="OnRowDataBound">
                                        <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <div class="acordian-table">
                                                       
                                                        <i class="toggle-table-minus fa fa-minus" aria-hidden="true" style="display: none"></i>
                                                        <i class="toggle-table-plus fa fa-plus" aria-hidden="true"></i>
                                                        
                                                        <asp:Panel ID="pnlOrders" runat="server" Style="display: none">
                                                            <asp:GridView ID="gvfactory" runat="server" AutoGenerateColumns="false" Class="table table-hover ChildGrid" OnRowDataBound="gvfactory_OnRowDataBound" OnRowCommand="gvfactory_RowCommand">
                                                                <Columns>
                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <div class="acordian-table">
                                                                                <i class="toggle-table-minus fa fa-minus" aria-hidden="true" style="display: none"></i>
                                                                                <i class="toggle-table-plus fa fa-plus" aria-hidden="true"></i>
                                                                                <asp:Panel ID="pnlunit" runat="server" Style="display: none">
                                                                                    <asp:GridView ID="gvunit" runat="server" AutoGenerateColumns="false" Class="table table-hover ChildGrid" OnRowCommand="gvunit_RowCommand">
                                                                                        <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
                                                                                        <Columns>
                                                                                            <asp:TemplateField>
                                                                                                <ItemTemplate>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField>
                                                                                                <ItemTemplate>
                                                                                                    <%#Container.DataItemIndex+1 %>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:BoundField ItemStyle-Width="150px" DataField="UnitRefNo" />
                                                                                            <asp:BoundField ItemStyle-Width="150px" DataField="UnitName" HeaderText="Unit" />
                                                                                            <asp:TemplateField >
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label runat="server" ID="lblunitrole" Text='<%#Eval("Role") %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField >
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblunitnodelname" runat="server" Text='<%#Eval("UnitEmailId") %>' NullDisplayText="#" SortExpression="ContactPersonEmailID"></asp:Label>
                                                                                                    <asp:HiddenField runat="server" ID="hfunitemail" Value='<%#Eval("UnitEmailId") %>' />
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField>
                                                                                                <ItemTemplate>
                                                                                                    <asp:LinkButton ID="lblviewunit" runat="server" CssClass="fa fa-eye" CommandName="unitViewComp" CommandArgument='<%#Eval("UnitRefNo") %>'></asp:LinkButton>
                                                                                                    <asp:LinkButton ID="lbleditunit" runat="server" CssClass="fa fa-edit" CommandName="unitEditComp" CommandArgument='<%#Eval("UnitRefNo") %>'></asp:LinkButton>
                                                                                                    <asp:LinkButton ID="lbldelunit" runat="server" CssClass="fa fa-trash" CommandName="unitdel" OnClientClick="return confirm('Are you sure you want to delete this unit?');" CommandArgument='<%#Eval("UnitRefNo") %>'></asp:LinkButton>
                                                                                                    <asp:LinkButton ID="lbllogindetailunit" Visible="False" runat="server" CssClass=" fa fa-paper-plane" CommandName="unitSendLogin" CommandArgument='<%#Eval("UnitRefNo") %>'></asp:LinkButton>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                        </Columns>
                                                                                    </asp:GridView>
                                                                                </asp:Panel>
                                                                            </div>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField >
                                                                        <ItemTemplate>
                                                                            <%#Container.DataItemIndex+1 %>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField >
                                                                        <ItemTemplate>
                                                                            <asp:Label runat="server" ID="lblfactoryrefno" Text='<%#Eval("FactoryRefNo") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField ItemStyle-Width="150px" DataField="FactoryName" HeaderText="Division" />
                                                                    <asp:TemplateField >
                                                                        <ItemTemplate>
                                                                            <asp:Label runat="server" ID="lblfactoryrole" Text='<%#Eval("Role") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField >
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblfactorynodelname" runat="server" Text='<%#Eval("FactoryEmailId") %>' NullDisplayText="#" SortExpression="ContactPersonEmailID"></asp:Label>
                                                                            <asp:HiddenField runat="server" ID="hffactoryemail" Value='<%#Eval("FactoryEmailId") %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField >
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lblviewfactory" runat="server" CssClass="fa fa-eye" CommandName="ViewfactoryComp" CommandArgument='<%#Eval("FactoryRefNo") %>'></asp:LinkButton>
                                                                            <asp:LinkButton ID="lbleditfactory" runat="server" CssClass="fa fa-edit" CommandName="EditfactoryComp" CommandArgument='<%#Eval("FactoryRefNo") %>'></asp:LinkButton>
                                                                            <asp:LinkButton ID="lbldelfactory" runat="server" CssClass="fa fa-trash" CommandName="DeletefactoryComp" OnClientClick="return confirm('Are you sure you want to delete this division/plant?');" CommandArgument='<%#Eval("FactoryRefNo") %>'></asp:LinkButton>
                                                                            <asp:LinkButton ID="lbllogindetailfactory" runat="server" Visible="False" CssClass=" fa fa-paper-plane" CommandName="factorySendLogin" CommandArgument='<%#Eval("FactoryRefNo") %>'></asp:LinkButton>
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
                                            <asp:BoundField DataField="CompanyName" HeaderText="Company" NullDisplayText="#" SortExpression="Company" />
                                            <asp:TemplateField HeaderText="Role">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblcompanyrole" Text='<%#Eval("Role") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Official Email">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblnodelname" runat="server" Text='<%#Eval("ContactPersonEmailID") %>' NullDisplayText="#" SortExpression="ContactPersonEmailID"></asp:Label>
                                                    <asp:HiddenField runat="server" ID="hfemail" Value='<%#Eval("ContactPersonEmailID") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lblview" runat="server" CssClass="fa fa-eye" CommandName="ViewComp" CommandArgument='<%#Eval("CompanyRefNo") %>'></asp:LinkButton>
                                                    <asp:LinkButton ID="lbledit" runat="server" CssClass="fa fa-edit" CommandName="EditComp" CommandArgument='<%#Eval("CompanyRefNo") %>'></asp:LinkButton>
                                                    <asp:LinkButton ID="lbldel" runat="server" CssClass="fa fa-trash" CommandName="DeleteComp" OnClientClick="return confirm('Are you sure you want to delete this Company?');" CommandArgument='<%#Eval("CompanyRefNo") %>'></asp:LinkButton>
                                                    <asp:LinkButton ID="lbllogindetail" runat="server" Visible="False" CssClass=" fa fa-paper-plane" CommandName="SendLogin" CommandArgument='<%#Eval("CompanyRefNo") %>'></asp:LinkButton>
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
                                                <td class="pass">Telephone No</td>
                                                <td>
                                                    <asp:Label ID="lblTelephoneNo" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="pass">Fax No</td>
                                                <td>
                                                    <asp:Label ID="lblFaxNo" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="pass">EmailID</td>
                                                <td>
                                                    <asp:Label ID="lblEmailID" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="pass">Website</td>
                                                <td>
                                                    <asp:Label ID="lblWebsite" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="pass">GSTNo</td>
                                                <td>
                                                    <asp:Label ID="lblGSTNo" runat="server"></asp:Label>
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
                                             <tr>
                                                <td class="pass">Nodal Officer Name</td>
                                                <td>
                                                    <asp:Label ID="lblNodalOfficerName" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="pass">Nodal Officer Email</td>
                                                <td>
                                                    <asp:Label ID="lblNodalEmail" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                             <tr>
                                                <td class="pass">Role</td>
                                                <td>
                                                    <asp:Label ID="lblRole" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                              <tr>
                                                <td class="pass">Latitude</td>
                                                <td>
                                                    <asp:Label ID="lblAad_Mobile" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                              <tr>
                                                <td class="pass">Longitude</td>
                                                <td>
                                                    <asp:Label ID="lblLongitude" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                              <tr>
                                                <td class="pass">Facebook</td>
                                                <td>
                                                    <asp:Label ID="lblFacebook" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                             <tr>
                                                <td class="pass">Twitter</td>
                                                <td>
                                                    <asp:Label ID="lblTwitter" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                             <tr>
                                                <td class="pass">Linkedin</td>
                                                <td>
                                                    <asp:Label ID="lblLinkedin" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                             <tr>
                                                <td class="pass">Instagram</td>
                                                <td>
                                                    <asp:Label ID="lblInstagram" runat="server"></asp:Label>
                                                </td>
                                            </tr>
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
            <div class="modal fade" id="divfactoryshow" role="dialog">
                <div class="modal-dialog">
                    <!-- Modal content-->
                    <div class="modal-content">
                        <div class="modal-header modal-header1">
                            <button type="button" class="close close1" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Division/Plant Detail</h4>
                        </div>
                        <form class="form-horizontal changepassword" role="form">
                            <div class="modal-body">
                                <div class="tab-pane fade active in" id="Div2">
                                    <table class="table table-bordered">
                                        <tbody>
                                            <tr>
                                                <td class="pass" width="30%">Reference No </td>
                                                <td>
                                                    <asp:Label ID="lblfacrefno" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="pass">Division Name</td>
                                                <td>
                                                    <asp:Label ID="lblfacname" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="pass">Address</td>
                                                <td>
                                                    <asp:Label ID="lblfacaddress" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="pass">State</td>
                                                <td>
                                                    <asp:Label ID="lblfacstate" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="pass">Pin Code</td>
                                                <td>
                                                    <asp:Label ID="lblfacpincode" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="pass">Nodal Name</td>
                                                <td>
                                                    <asp:Label ID="lblfacnodalname" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="pass">Official Contact No</td>
                                                <td>
                                                    <asp:Label ID="lblfacmobno" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="pass">Official Email ID</td>
                                                <td>
                                                    <asp:Label ID="lblfacemail" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="pass">CIN</td>
                                                <td>
                                                    <asp:Label ID="lblfaccin" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="pass">PAN No</td>
                                                <td>
                                                    <asp:Label ID="lblfacpan" runat="server"></asp:Label>
                                                </td>
                                            </tr>
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
            <div class="modal fade" id="divunitshow" role="dialog">
                <div class="modal-dialog">
                    <!-- Modal content-->
                    <div class="modal-content">
                        <div class="modal-header modal-header1">
                            <button type="button" class="close close1" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Unit Detail</h4>
                        </div>
                        <form class="form-horizontal changepassword" role="form">
                            <div class="modal-body">
                                <div class="tab-pane fade active in" id="Div4">
                                    <table class="table table-bordered">
                                        <tbody>
                                            <tr>
                                                <td class="pass" width="30%">Reference No </td>
                                                <td>
                                                    <asp:Label ID="lblurefno" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="pass">Unit Name</td>
                                                <td>
                                                    <asp:Label ID="lbluname" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="pass">Address</td>
                                                <td>
                                                    <asp:Label ID="lbluaddress" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="pass">State</td>
                                                <td>
                                                    <asp:Label ID="lblustate" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="pass">Pin Code</td>
                                                <td>
                                                    <asp:Label ID="lblupin" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="pass">Nodal Name</td>
                                                <td>
                                                    <asp:Label ID="lblnodalname" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="pass">Official Contact No</td>
                                                <td>
                                                    <asp:Label ID="lblnofficialcontact" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="pass">Official Email ID</td>
                                                <td>
                                                    <asp:Label ID="lbluemail" runat="server"></asp:Label>
                                                </td>
                                            </tr>
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

