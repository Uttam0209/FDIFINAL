<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewDashboard.aspx.cs" Inherits="Admin_ViewDashboard" MasterPageFile="~/Admin/MasterPage.master" %>

<asp:Content ID="head" runat="server" ContentPlaceHolderID="head">
    <script type="text/javascript">
        function showPopup() {
            $('#divCompany').modal('show');
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
    <script type="text/javascript">
        function showPopup3() {
            $('#ViewNodalDetail').modal('show');
        }
    </script>
    <script type="text/javascript">
        function showPopup4() {
            $('#ProductCompany').modal('show');
        }
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
                                <div class="clearfix"></div>
                                <div style="margin-top: 5px;">
                                    <a class="fa fa-arrow-circle-left pull-right" href='<%=ResolveUrl("~/Dashboard") %>'>&nbsp; &nbsp;Back</a>
                                </div>
                                <div class="clearfix"></div>
                                <div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <asp:TextBox runat="server" ID="txtsearch" placeholder="Search (Name,emailcompany/division/unit name)" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:Button runat="server" ID="btnseach" Text="Search" CssClass="btn btn-primary" OnClick="btnseach_Click" />
                                        </div>
                                    </div>
                                </div>
                                <div class="clearfix"></div>
                                <div id="divTotalNumber" class="text-center" style="font-size: 16px; margin-top: 10px;" runat="server">
                                    <asp:Label ID="lbltotal" runat="server" Text=""></asp:Label>
                                </div>
                                <div class="clearfix"></div>
                                <div class="table-wraper table-responsive" runat="server" id="divcompanyGrid" visible="False">
                                    <asp:GridView ID="gvcompanydetail" runat="server" Width="100%" Class="commonAjaxTbl master-company-table table display 
                                        responsive no-wrap table-hover manage-user Grid table-responsive"
                                        AutoGenerateColumns="false" OnRowCommand="gvcompanydetail_RowCommand">
                                        <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No.">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Reference No." Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblrefno" runat="server" Text='<%#Eval("CompanyRefNo") %>' NullDisplayText="#"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="CompanyName" HeaderText="Company" NullDisplayText="#" />
                                            <asp:TemplateField HeaderText="Role">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblcompanyrole" Text='<%#Eval("Role") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField ItemStyle-Width="150px" DataField="NodalOficerName" HeaderText="Nodal Officer Name" />
                                            <asp:TemplateField HeaderText="Nodal Officer Email">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblnodelname" runat="server" Text='<%#Eval("NodalOfficerEmail") %>' NullDisplayText="#"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lblview" runat="server" CssClass="fa fa-eye" CommandName="ViewComp" ToolTip="View detail of company" CommandArgument='<%#Eval("CompanyRefNo") %>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <div class="clearfix"></div>
                                <div class="table-wrapper table-responsive" id="divfactorygrid" runat="server" visible="False">
                                    <asp:GridView ID="gvfactory" runat="server" AutoGenerateColumns="false" Class="commonAjaxTbl master-company-table ViewProductTable table 
                                        display responsive no-wrap table-hover manage-user Grid table-responsive"
                                        OnRowCommand="gvfactory_RowCommand">
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No.">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="CompanyName" HeaderText="Company" NullDisplayText="#" />
                                            <asp:BoundField ItemStyle-Width="150px" DataField="FactoryName" HeaderText="Division" />
                                            <asp:TemplateField HeaderText="Reference No." Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblfactoryrefno" Text='<%#Eval("FactoryRefNo") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField ItemStyle-Width="150px" DataField="Role" HeaderText="Role" />
                                            <asp:BoundField ItemStyle-Width="150px" DataField="NodalOficerName" HeaderText="Nodal Officer Name" />
                                            <asp:TemplateField HeaderText="Nodal Officer Email">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblfactorynodelname" runat="server" Text='<%#Eval("NodalOfficerEmail") %>' NullDisplayText="#"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lblviewfactory" runat="server" CssClass="fa fa-eye" ToolTip="View detail of division" CommandName="ViewfactoryComp" CommandArgument='<%#Eval("FactoryRefNo") %>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <div class="clearfix"></div>
                                <div class="table-wrapper table-responsive" id="divunitGrid" runat="server" visible="False" style="overflow: scroll;">
                                    <asp:GridView ID="gvunit" runat="server" AutoGenerateColumns="false" Class="commonAjaxTbl master-company-table ViewProductTable table
                                         display responsive no-wrap table-hover manage-user Grid table-responsive"
                                        Style="overflow: scroll;" OnRowCommand="gvunit_RowCommand">
                                        <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No.">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="CompanyName" HeaderText="Company" NullDisplayText="#" />
                                            <asp:BoundField DataField="FactoryName" HeaderText="Division" />
                                            <asp:BoundField DataField="UnitName" HeaderText="Unit" />
                                            <asp:BoundField DataField="UnitRefNo" HeaderText="Reference No." Visible="False" />

                                            <asp:TemplateField HeaderText="Role">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblunitrole" Text='<%#Eval("Role") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField ItemStyle-Width="150px" DataField="NodalOficerName" HeaderText="Nodal Officer Name" />
                                            <asp:TemplateField HeaderText="Nodal Officer Email">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblunitnodelname" runat="server" Text='<%#Eval("NodalOfficerEmail") %>' NullDisplayText="#"></asp:Label>
                                                    <asp:HiddenField runat="server" ID="hfunitemail" Value='<%#Eval("NodalOfficerEmail") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lblviewunit" runat="server" CssClass="fa fa-eye" ToolTip="View detail of unit" CommandName="unitViewComp" CommandArgument='<%#Eval("UnitRefNo") %>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <div class="clearfix"></div>
                                <div class="table-wraper table-responsive" id="divEmployeeNodalGrid" runat="server" visible="False">
                                    <asp:GridView ID="gvViewNodalOfficer" runat="server" Width="100%" Class="commonAjaxTbl master-company-table table display 
                                        responsive no-wrap table-hover manage-user Grid"
                                        AutoGenerateColumns="false" OnRowCommand="gvViewNodalOfficer_RowCommand" OnRowDataBound="gvViewNodalOfficer_RowDataBound">
                                        <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No.">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="CompanyName" HeaderText="Company" />
                                            <asp:BoundField DataField="FactoryName" HeaderText="Division" />
                                            <asp:BoundField DataField="UnitName" HeaderText="Unit" />
                                            <asp:TemplateField HeaderText="Nodal Officer Name" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblnodelname" runat="server" Text='<%#Eval("NodalOficerName") %>' SortExpression="NodalOficerName"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Reference No." Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblnodelrefno" runat="server" Text='<%#Eval("NodalOfficerRefNo") %>' SortExpression="NodalOfficerRefNo"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField ItemStyle-Width="150px" DataField="NodalOficerName" HeaderText="Name" />
                                            <asp:TemplateField HeaderText="Email">
                                                <ItemTemplate>
                                                    <asp:Label ID="nodelemail" runat="server" Text='<%#Eval("NodalOfficerEmail") %>' SortExpression="NodalOfficerEmail"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Role">
                                                <ItemTemplate>
                                                    <asp:HiddenField runat="server" />
                                                    <asp:Label ID="lblnodalofficer" runat="server" Text='<%#Eval("IsNodalOfficer") %>' NullDisplayText="#" Visible="False" SortExpression="NodalOfficerEmail"></asp:Label>
                                                    <asp:Label ID="lblnodallogactive" runat="server" Text='<%#Eval("IsLoginActive") %>' NullDisplayText="#" Visible="False" SortExpression="NodalOfficerEmail"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Company" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblrefno" runat="server" Text='<%#Eval("CompanyName") %>' SortExpression="Company"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lblview" runat="server" ToolTip="View Detail of Nodal Officer/Employee" CssClass="fa fa-eye" CommandName="ViewComp" CommandArgument='<%#Eval("NodalOfficerID") %>'></asp:LinkButton>
                                                    <asp:HiddenField runat="server" ID="hfnodalrole" Value='<%#Eval("Type") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <div class="clearfix"></div>
                                <div class="table-wrapper" id="divProductGrid" runat="server" visible="False" style="overflow: scroll;">
                                    <asp:GridView ID="gvproduct" runat="server" Width="100%" Class="commonAjaxTbl master-company-table ViewProductTable table display 
                                        responsive no-wrap table-hover manage-user Grid table-responsive"
                                        Style="overflow: scroll;"
                                        AutoGenerateColumns="false" OnRowCommand="gvproduct_RowCommand">
                                        <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No.">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lblview" runat="server" CssClass="fa fa-eye" CommandName="ViewComp" CommandArgument='<%#Eval("ProductRefNo") %>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="CompanyName" HeaderText="Company" NullDisplayText="-" />
                                            <asp:BoundField DataField="FactoryName" HeaderText="Division" NullDisplayText="-" />
                                            <asp:BoundField DataField="UnitName" HeaderText="Unit" NullDisplayText="-" />
                                            <asp:TemplateField HeaderText="Product Reference No." Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblrefno" runat="server" Text='<%#Eval("ProductRefNo") %>' NullDisplayText="#" SortExpression="ProductRefNo"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="ProductDescription" HeaderText="Item Description" NullDisplayText="#" ItemStyle-Wrap="true" ItemStyle-Width="100" SortExpression="Description" />
                                            <asp:TemplateField HeaderText="OEM PartNumber">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblcompanyrole" Text='<%#Eval("OEMPartNumber") %>'></asp:Label>
                                                    <asp:HiddenField ID="hfrole" runat="server" Value='<%#Eval("Role") %>' />
                                                    <asp:HiddenField ID="hfcomprefno" runat="server" Value='<%#Eval("CompanyRefNo") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="DPSUPartNumber" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblnodelname" runat="server" Text='<%#Eval("DPSUPartNumber") %>' NullDisplayText="#" SortExpression="DPSUPartNumber"></asp:Label>
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
            <%--*/*************************************************************COde Of View**********************************//*--%>
            <div class="modal fade" id="divCompany" role="dialog">
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
                                                <td class="pass" width="30%">Reference No. </td>
                                                <td>
                                                    <asp:Label ID="lblrefno" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="pass">Company</td>
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
                                            <tr class="hidden">
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
                                <button type="button" class="btn btn-primary" data-dismiss="modal">Close</button>
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
                                                <td class="pass" width="30%">Reference No. </td>
                                                <td>
                                                    <asp:Label ID="lblfacrefno" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="pass">Name</td>
                                                <td>
                                                    <asp:Label ID="lblDivName" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="pass">Address</td>
                                                <td>
                                                    <asp:Label ID="lblDivAddress" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="pass">State</td>
                                                <td>
                                                    <asp:Label ID="lblDivState" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="pass">Pin Code</td>
                                                <td>
                                                    <asp:Label ID="lblDivPincode" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="pass">Head of Division Name</td>
                                                <td>
                                                    <asp:Label ID="lblDivCeoName" runat="server"></asp:Label>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td class="pass">Head of Division Email</td>
                                                <td>
                                                    <asp:Label ID="lblDivCeoEmail" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="pass">Nodal Officer Name</td>
                                                <td>
                                                    <asp:Label ID="lblDivNodalName" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="pass">Nodal Officer Email</td>
                                                <td>
                                                    <asp:Label ID="lblDivNodalEMail" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="pass">Telephone No</td>
                                                <td>
                                                    <asp:Label ID="lblDivConNo" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr class="hidden">
                                                <td class="pass">Email ID</td>
                                                <td>
                                                    <asp:Label ID="lblDivEmail" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="pass">FAX No</td>
                                                <td>
                                                    <asp:Label ID="lblDivFax" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr class="hidden">
                                                <td class="pass">Website</td>
                                                <td>
                                                    <asp:Label ID="lblDivWebsite" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="pass">Latitude</td>
                                                <td>
                                                    <asp:Label ID="lblDivlatitude" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="pass">Longitude</td>
                                                <td>
                                                    <asp:Label ID="lblDivLongitude" runat="server"></asp:Label>
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
                                                <td class="pass" width="30%">Reference No. </td>
                                                <td>
                                                    <asp:Label ID="lblurefno" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="pass">Name</td>
                                                <td>
                                                    <asp:Label ID="lblUnitName" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="pass">Address</td>
                                                <td>
                                                    <asp:Label ID="lblUnitAddress" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="pass">State</td>
                                                <td>
                                                    <asp:Label ID="lblUnitState" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="pass">Pin Code</td>
                                                <td>
                                                    <asp:Label ID="lblUnitPin" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="pass">Head of Unit Name</td>
                                                <td>
                                                    <asp:Label ID="lblUnitCeoName" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="pass">Head of Unit Email</td>
                                                <td>
                                                    <asp:Label ID="lblUnitCeoEmail" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="pass">Nodal Officer Name</td>
                                                <td>
                                                    <asp:Label ID="lblUnitNodalName" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="pass">Nodal Officer Email</td>
                                                <td>
                                                    <asp:Label ID="lblUnitNodalEmail" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr class="hidden">
                                                <td class="pass">Official Email ID</td>
                                                <td>
                                                    <asp:Label ID="lblUnitEmail" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr class="hidden">
                                                <td class="pass">Official Website</td>
                                                <td>
                                                    <asp:Label ID="lblUnitWebsite" runat="server"></asp:Label>
                                                </td>
                                            </tr>

                                            <tr class="hidden">
                                                <td class="pass">Facebook</td>
                                                <td>
                                                    <asp:Label ID="lblUnitFacebook" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr class="hidden">
                                                <td class="pass">Twitter</td>
                                                <td>
                                                    <asp:Label ID="lblUnitTwitter" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr class="hidden">
                                                <td class="pass">Instagram</td>
                                                <td>
                                                    <asp:Label ID="lblUnitInsta" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr class="hidden">
                                                <td class="pass">Linkedin</td>
                                                <td>
                                                    <asp:Label ID="lblUnitLink" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr class="hidden">
                                                <td class="pass">Longitude</td>
                                                <td>
                                                    <asp:Label ID="lblUnitLongitude" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr class="hidden">
                                                <td class="pass">Latitude</td>
                                                <td>
                                                    <asp:Label ID="lblUnitLatitude" runat="server"></asp:Label>
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
                                <div class="tab-pane fade active in" id="Div1">
                                    <table class="table table-bordered">
                                        <tbody>

                                            <tr>
                                                <td class="pass">Company</td>
                                                <td>
                                                    <asp:Label ID="lblNodalComp" runat="server"></asp:Label>
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
                                            <tr class="hidden">
                                                <td class="pass">Nodal Refernce No</td>
                                                <td>
                                                    <asp:Label ID="lblempNodalOfficerRefNo" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="pass">Name</td>
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
                                                    <asp:Label ID="lblempNodalEmpCode" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="pass">Email</td>
                                                <td>
                                                    <asp:Label ID="lblEmail" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="pass">Mobile No.</td>
                                                <td>
                                                    <asp:Label ID="lblMobile" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="pass">Telephone No.</td>
                                                <td>
                                                    <asp:Label ID="lblTelephone" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="pass">Fax No.</td>
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
            <div class="modal fade" id="ProductCompany" role="dialog">
                <div class="modal-dialog" style="width: 1100px; z-index: 9999999999;">
                    <!-- Modal content-->
                    <div class="modal-content">
                        <div class="modal-header modal-header1">
                            <button type="button" class="close close1" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Product Detail</h4>
                        </div>
                        <form class="form-horizontal changepassword" role="form">
                            <div class="modal-body sideBg">
                                <div class="tabing-section">
                                    <ul class="nav nav-tabs" style="margin-top: 10px;">
                                        <li class="active"><a data-toggle="tab" href="#pd">Description</a></li>
                                        <li><a data-toggle="tab" href="#pimg">Image</a></li>
                                        <li><a data-toggle="tab" href="#test">Testing</a></li>
                                        <li><a data-toggle="tab" href="#cer">Certification</a></li>
                                        <li><a data-toggle="tab" href="#spd">Technical Support</a></li>
                                        <li><a data-toggle="tab" href="#spfinn">Financial Support</a></li>
                                        <li><a data-toggle="tab" href="#qpt">Quantity Required</a></li>
                                        <li><a data-toggle="tab" href="#tdr">Tender</a></li>
                                        <li><a data-toggle="tab" href="#cd">Contact</a></li>
                                    </ul>
                                    <div class="tab-content">
                                        <div id="pd" class="tab-pane fade in active">
                                            <div>
                                                <table class="table table-bordered">
                                                    <tr>
                                                        <th runat="server" visible="False">Refrence No</th>
                                                        <th>Company</th>
                                                        <th>Division/Palnt</th>
                                                        <th>Unit</th>
                                                        <th>Product Refrence No</th>
                                                    </tr>
                                                    <tr>
                                                        <td runat="server" visible="False">
                                                            <asp:Label ID="lblcomprefno" runat="server"></asp:Label></td>
                                                        <td>
                                                            <asp:Label ID="lblcompname" runat="server"></asp:Label></td>
                                                        <td>
                                                            <asp:Label ID="lbldiviname" runat="server"></asp:Label></td>
                                                        <td>
                                                            <asp:Label ID="lblprodunitname" runat="server"></asp:Label></td>
                                                        <td>
                                                            <asp:Label ID="lblprodrefno" runat="server"></asp:Label></td>
                                                    </tr>
                                                </table>
                                            </div>
                                            <div>
                                                <table class="table table-bordered">
                                                    <tr>

                                                        <th>NSN GROUP</th>
                                                        <th>NSN GROUP CLASS</th>
                                                        <th>CLASS ITEM</th>
                                                        <th>NSC Code (4 digit)</th>
                                                        <th>NIIN Code (9-digit)</th>
                                                        <th>Product Description</th>
                                                    </tr>
                                                    <tr>

                                                        <td>
                                                            <asp:Label ID="lblprodlevel1" runat="server"></asp:Label></td>
                                                        <td>
                                                            <asp:Label ID="productlevel2" runat="server"></asp:Label></td>
                                                        <td>
                                                            <asp:Label ID="lblprodlevel3" runat="server"></asp:Label></td>
                                                        <td>
                                                            <asp:Label ID="lblnsccode" runat="server"></asp:Label></td>
                                                        <td>
                                                            <asp:Label ID="lblniincode" runat="server"></asp:Label></td>
                                                        <td>
                                                            <asp:Label ID="lblproductdescription" runat="server"></asp:Label>
                                                            <a runat="server" id="a_downitem" href="#" target="_Blank" class="fa fa-download pull-right"
                                                                tooltip="Download item description document releted to item"></a>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                            <div>
                                                <table class="table table-bordered">
                                                    <tr>
                                                        <th>OEM Part Number</th>
                                                        <th>OEM Name</th>
                                                        <th>OEM Country</th>
                                                        <th>DPSU Part Number</th>
                                                        <th>End User</th>
                                                        <th>End User Part Number</th>
                                                        <th>HSN Code</th>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lbloempartnumber" runat="server"></asp:Label></td>
                                                        <td>
                                                            <asp:Label ID="lbloemname" runat="server"></asp:Label></td>
                                                        <td>
                                                            <asp:Label ID="lbloemcountry" runat="server"></asp:Label></td>
                                                        <td>
                                                            <asp:Label ID="lbldpsupartno" runat="server"></asp:Label></td>
                                                        <td>
                                                            <asp:Label ID="lblenduser" runat="server"></asp:Label></td>
                                                        <td>
                                                            <asp:Label ID="lblenduserpartno" runat="server"></asp:Label></td>
                                                        <td>
                                                            <asp:Label ID="lblhsncode" runat="server"></asp:Label></td>
                                                    </tr>
                                                </table>
                                            </div>
                                            <div>
                                                <table class="table table-bordered">
                                                    <tr>
                                                        <th>DEFENCE PLATFORM</th>
                                                        <th>NAME OF DEFENCE PLATFORM</th>
                                                        <th>PRODUCT (INDUSTRY DOMAIN)</th>
                                                        <th>PRODUCT (INDUSTRY SUB DOMAIN)</th>
                                                        <th>PRODUCT (INDUSTRY 2nd SUB DOMAIN)</th>

                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblplatform" runat="server"></asp:Label></td>
                                                        <td>
                                                            <asp:Label ID="lblnomenclatureofmainsystem" runat="server"></asp:Label></td>

                                                        <td>
                                                            <asp:Label ID="lbltechlevel1" runat="server"></asp:Label></td>
                                                        <td>
                                                            <asp:Label ID="lbltechlevel2" runat="server"></asp:Label></td>
                                                        <td>
                                                            <asp:Label ID="lbltechlevel3" runat="server"></asp:Label></td>

                                                    </tr>
                                                </table>
                                            </div>
                                            <div>
                                                <table class="table table-bordered">
                                                    <tr>

                                                        <th>PROCURMENT CATEGORY</th>
                                                        <th>PROCURMENT CATEGORY REMARK</th>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblpurposeofprocurement" runat="server"></asp:Label></td>
                                                        <td>
                                                            <asp:Label ID="lblprocremarks" runat="server"></asp:Label></td>
                                                    </tr>
                                                </table>
                                            </div>
                                            <div>
                                                <table class="table table-bordered">
                                                    <tr>

                                                        <th>Product Already Indeginized</th>
                                                        <th runat="server" id="tablemanufacturename">Manufacturer Name</th>
                                                        <th runat="server" id="tablemanufactureAddress">Address</th>
                                                        <th runat="server" id="tablemanufactureYear">Year of Indiginization</th>
                                                        <th>Search Keywords :</th>

                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblprodalredyindeginized" runat="server"></asp:Label></td>
                                                        <td runat="server" id="tablemanufacturename1">
                                                            <asp:Label ID="lblmanufacturename" runat="server"></asp:Label>
                                                        </td>
                                                        <td runat="server" id="tablemanufacturename2">
                                                            <asp:Label ID="lblmanaddress" runat="server"></asp:Label></td>
                                                        <td runat="server" id="tablemanufacturename3">
                                                            <asp:Label ID="lblyearofindiginization" runat="server"></asp:Label></td>
                                                        <td>
                                                            <asp:Label ID="lblsearchkeyword" runat="server"></asp:Label></td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </div>
                                        <div id="pimg" class="tab-pane fade in">
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <asp:DataList runat="server" ID="dlimage" RepeatColumns="4" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                                        <ItemTemplate>
                                                            <div class="col-sm-3">
                                                                <asp:Image runat="server" ID="imgprodimage" class="image img-responsive img-rounded" Height="120px" Width="120" src='<%#Eval("ImageName") %>' />
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:DataList>
                                                </div>
                                            </div>
                                        </div>
                                        <div id="spd" class="tab-pane fade in">
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <table class="table table-bordered">
                                                        <tbody>
                                                            <tr>
                                                                <th class="pass" width="30%">Technical Support</th>
                                                                <th class="pass" width="30%">Remarks</th>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblsupportprovidedbydpsu" runat="server"></asp:Label>
                                                                </td>

                                                                <td>
                                                                    <asp:Label ID="lblremarks" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>
                                        <div id="spfinn" class="tab-pane fade in">
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <table class="table table-bordered">
                                                        <tbody>
                                                            <tr>
                                                                <th class="pass" width="30%">Financial Support</th>
                                                                <th class="pass" width="30%">Remarks</th>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblfinancial" runat="server"></asp:Label>
                                                                </td>

                                                                <td>
                                                                    <asp:Label ID="lblfinancialRemark" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>
                                        <div id="qpt" class="tab-pane fade in">
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <table class="table table-bordered">
                                                        <tbody>
                                                            <tr>
                                                                <th class="pass">Estimated Quantity</th>
                                                                <th class="pass">Estimated Price / LLP</th>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblestimatedquantity" runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblestimatedprice" runat="server"></asp:Label>
                                                                </td>
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>
                                        <div id="tdr" class="tab-pane fade in">
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <table class="table table-bordered">
                                                        <tbody>
                                                            <tr>
                                                                <th class="pass">Tender Status</th>
                                                                <th class="pass">Tender Submission</th>
                                                                <th class="pass">Tender Date</th>
                                                                <th class="pass">Tender URL</th>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lbltenderstatus" runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lbltendersubmission" runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lbltenderdate" runat="server"></asp:Label>
                                                                </td>

                                                                <td>
                                                                    <asp:Label ID="lbltenderurl" runat="server"></asp:Label>
                                                                </td>
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>
                                        <div id="cd" class="tab-pane fade in">
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <table class="table table-bordered">
                                                        <tbody>
                                                            <tr>
                                                                <th class="pass">Employee Code</th>
                                                                <th class="pass">Designation</th>
                                                                <th class="pass">Email</th>
                                                                <th class="pass">Mobile</th>
                                                                <th class="pass">Phone</th>
                                                                <th class="pass">Fax</th>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblempcode" runat="server"></asp:Label>
                                                                </td>

                                                                <td>
                                                                    <asp:Label ID="lbldesignation" runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblemailprod" runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblmobilenumber" runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblphonenumber" runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblfaxprod" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </div>
                                                <div class="col-sm-12" runat="server" visible="False">
                                                    <table class="table table-bordered" runat="server" id="tablenodal2">
                                                        <tbody>
                                                            <tr>
                                                                <th class="pass">Employee Code</th>
                                                                <th class="pass">Designation</th>
                                                                <th class="pass">Email</th>
                                                                <th class="pass">Mobile</th>
                                                                <th class="pass">Phone</th>
                                                                <th class="pass">Fax</th>

                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblempcode2" runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lbldesignation2" runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblemailid2" runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblmobileno2" runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblphoneno2" runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblfax2" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>

                                                        </tbody>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>
                                        <div id="test" class="tab-pane fade in">
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <table class="table table-bordered">
                                                        <tbody>
                                                            <tr>
                                                                <th class="pass" width="30%">Testing</th>
                                                                <th class="pass" width="30%">Remarks</th>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lbltesting" runat="server"></asp:Label>
                                                                </td>

                                                                <td>
                                                                    <asp:Label ID="lbltestingremarks" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>
                                        <div id="cer" class="tab-pane fade in">
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <table class="table table-bordered">
                                                        <tbody>
                                                            <tr>
                                                                <th class="pass" width="30%">Certification</th>
                                                                <th class="pass" width="30%">Remarks</th>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblcertification" runat="server"></asp:Label>
                                                                </td>

                                                                <td>
                                                                    <asp:Label ID="lblcertificationremarks" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                            </div>
                        </form>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress" runat="server" AssociatedUpdatePanelID="up">
        <ProgressTemplate>
            <!---Progress Bar ---->
            <div class="overlay-progress">
                <div class="custom-progress-bar blue stripes">
                    <span></span>
                    <p>Processing</p>
                </div>
            </div>
            <!---Progress Bar ---->
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
