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
    <asp:UpdatePanel ID="up" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
        <ContentTemplate>
            <div class="content oem-content">
                <div class="sideBg">
                    <div class="row">
                        <div class="col-md-12 padding_0">
                            <div id="divHeadPage" runat="server"></div>
                        </div>
                        <div class="col-md-12">
                            <div class="clearfix"></div>
                            <div style="margin-top: 5px;">
                                <%--<a class="fa fa-arrow-circle-left pull-right" href="javascript: history.go(-1)">&nbsp; &nbsp;Back</a>--%>
                                <asp:LinkButton ID="lblback" runat="server" class="fa fa-arrow-circle-left pull-right" OnClick="lblback_Click">Back</asp:LinkButton>
                            </div>
                            <div class="clearfix"></div>
                        </div>
                    </div>
                    <div class="clearfix" style="margin-bottom: 10px;"></div>
                    <div class="addfdi">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="clearfix"></div>
                                <div runat="server" id="divsearch" visible="false">
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
                                <asp:HiddenField runat="server" ID="hfmtype" />
                                <asp:HiddenField runat="server" ID="hfmref" />
                                <div class="table-responsive" runat="server" id="divcompanyGrid" visible="False">
                                    <asp:GridView ID="gvcompanydetail" runat="server" Width="100%" Class="table table-hover manage-user"
                                        AutoGenerateColumns="false" OnRowCommand="gvcompanydetail_RowCommand" OnRowCreated="gvcompanydetail_RowCreated">
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
                                <div class="table-responsive" id="divfactorygrid" runat="server" visible="False">
                                    <asp:GridView ID="gvfactory" runat="server" AutoGenerateColumns="false" Class="table table-hover manage-user"
                                        OnRowCommand="gvfactory_RowCommand" OnRowCreated="gvfactory_RowCreated">
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
                                <div class="table-responsive" id="divunitGrid" runat="server" visible="False" style="overflow: scroll;">
                                    <asp:GridView ID="gvunit" runat="server" AutoGenerateColumns="false" Class="table table-hover manage-user"
                                        Style="overflow: scroll;" OnRowCommand="gvunit_RowCommand" OnRowCreated="gvunit_RowCreated">
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
                                <div class="table-responsive" id="divEmployeeNodalGrid" runat="server" visible="False">
                                    <asp:GridView ID="gvViewNodalOfficer" runat="server" Width="100%" Class="table table-hover manage-user"
                                        AutoGenerateColumns="false" OnRowCommand="gvViewNodalOfficer_RowCommand" OnRowDataBound="gvViewNodalOfficer_RowDataBound" OnRowCreated="gvViewNodalOfficer_RowCreated">
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
                                <!-----------------------------------------Code for pageindexing----------------------------------------------------->
                                <div class="row" runat="server" id="divpageindex" visible="false">
                                    <div class="col-sm-9">
                                        <div class="col-sm-4 row">
                                            <asp:LinkButton ID="lnkbtnPgPrevious" runat="server" CssClass="btn btn-info  btn-sm"
                                                OnClick="lnkbtnPgPrevious_Click">Previous</asp:LinkButton>
                                        </div>
                                        <div class="col-sm-4" style="display: flex">
                                            <asp:TextBox runat="server" ID="txtpageno" CssClass="form-control btn-defualt text-center red" AutoCompleteType="Search" Placeholder="Please enter no of page"></asp:TextBox>
                                            <asp:LinkButton ID="btngoto" runat="server" CssClass="btn btn-primary" OnClick="btngoto_Click">Go to</asp:LinkButton>
                                        </div>
                                        <div class="col-sm-4 row">
                                            <asp:LinkButton ID="lnkbtnPgNext" runat="server" CssClass="btn btn-info btn-sm pull-right" Style="margin-right: 3px;"
                                                OnClick="lnkbtnPgNext_Click">Next</asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="pull-right">
                                            <asp:Label ID="lblpaging" runat="server" class="btn btn-primary text-center" Text=""></asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <!-----------------------------------------end code for page indexing----------------------------------------------------->
                            </div>
                        </div>
                       
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
                </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="lnkbtnPgPrevious" />
            <asp:PostBackTrigger ControlID="btngoto" />
            <asp:PostBackTrigger ControlID="lnkbtnPgNext" />
        </Triggers>
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
