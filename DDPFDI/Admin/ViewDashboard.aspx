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
    <script type="text/javascript">
        function datatable() {
            $('#ContentPlaceHolder1_gvproduct').DataTable({
            });
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
                                <div class="table-responsive" id="divProductGrid" runat="server" visible="False" style="overflow: scroll;">
                                    <asp:GridView ID="gvproduct" runat="server" Width="100%" Class="table table-hover manage-user"
                                        Style="overflow: scroll;"
                                        AutoGenerateColumns="false" OnRowCommand="gvproduct_RowCommand" OnRowCreated="gvproduct_RowCreated" OnRowDataBound="gvproduct_RowDataBound">
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
                                                    <asp:HiddenField ID="hfroleProd" runat="server" Value='<%#Eval("Role") %>' />
                                                    <asp:HiddenField ID="hfcomprefno" runat="server" Value='<%#Eval("CompanyRefNo") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="PDF">
                                                <ItemTemplate>
                                                    <asp:HyperLink runat="server" ID="lbpdffile" Target="_blank" NavigateUrl='<%#Eval("ItemDescriptionPDFFile","~/Upload/{0}") %>' CssClass="fa fa-file-pdf"></asp:HyperLink>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Image">
                                                <ItemTemplate>
                                                    <a data-fancybox="Prodgridviewgellry" href='<%#Eval("TopImages") %>'>
                                                        <asp:Image ID="imgtop" runat="server" Height="80px" Width="80px" src='<%#Eval("TopImages") %>' />
                                                    </a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="CompanyName" HeaderText="Company" NullDisplayText="-" />

                                            <asp:BoundField DataField="FactoryName" Visible="false" HeaderText="Division" NullDisplayText="-" />
                                            <asp:BoundField DataField="UnitName" Visible="false" HeaderText="Unit" NullDisplayText="-" />
                                            <asp:TemplateField HeaderText="Item Reference No." Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblrefno" runat="server" Text='<%#Eval("ProductRefNo") %>' NullDisplayText="#" SortExpression="ProductRefNo"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="ProductDescription" HeaderText="Item Description" NullDisplayText="#" ItemStyle-Wrap="true" ItemStyle-Width="100" SortExpression="ProductDescription" />
                                            <asp:BoundField DataField="ProdIndustryDoamin" HeaderText="Product (Industry Domain)" NullDisplayText="#" ItemStyle-Wrap="true" ItemStyle-Width="100" SortExpression="ProdIndustryDoamin" />
                                            <asp:BoundField DataField="NSNGroup" HeaderText="NSN Group" NullDisplayText="#" ItemStyle-Wrap="true" ItemStyle-Width="100" SortExpression="NameDefencePlatform" />
                                            <asp:BoundField DataField="DefencePlatform" HeaderText="Defence Platform" NullDisplayText="#" ItemStyle-Wrap="true" ItemStyle-Width="100" SortExpression="DefencePlatform" />
                                            <asp:TemplateField HeaderText="OEM PartNumber" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblcompanyrole" Text='<%#Eval("OEMPartNumber") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="DPSUPartNumber">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblnodelname" runat="server" Text='<%#Eval("DPSUPartNumber") %>' NullDisplayText="#" SortExpression="DPSUPartNumber"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="IsIndeginized" HeaderText="Item Indeginized" NullDisplayText="#" ItemStyle-Wrap="true" ItemStyle-Width="100" SortExpression="IsIndeginized" />
                                            <asp:TemplateField HeaderText="Last Updated">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblLastUpdated" Text='<%#Eval("LastUpdated") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <div class="clearfix"></div>
                                <!-----------------------------------------Code for pageindexing----------------------------------------------------->
                                <div class="row" runat="server" id="divpageindex" visible="false">
                                    <div class="col-sm-2">
                                        <asp:LinkButton ID="lnkbtnPgFirst" runat="server" CssClass="btn  btn-success  btn-sm"
                                            OnClick="lnkbtnPgFirst_Click">First</asp:LinkButton>
                                        <asp:LinkButton ID="lnkbtnPgPrevious" runat="server" CssClass="btn btn-success  btn-sm"
                                            OnClick="lnkbtnPgPrevious_Click">Previous</asp:LinkButton>
                                    </div>
                                    <div class="col-sm-8">
                                        <asp:DataList ID="DataListPaging" runat="server" RepeatDirection="Horizontal" OnItemCommand="DataListPaging_ItemCommand"
                                            OnItemDataBound="DataListPaging_ItemDataBound">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="Pagingbtn" runat="server" CssClass="btn btn-success mt5 btn-xs"
                                                    CommandArgument='<%# Eval("PageIndex") %>' CommandName="Newpage" Text='<%# Eval("PageText")%>'></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:DataList>
                                    </div>
                                    <div class="col-sm-2">
                                        <asp:LinkButton ID="lnkbtnPgLast" runat="server" CssClass="btn  btn-success btn-sm pull-right"
                                            OnClick="lnkbtnPgLast_Click">Last</asp:LinkButton>
                                        <asp:LinkButton ID="lnkbtnPgNext" runat="server" CssClass="btn  btn-success btn-sm pull-right" Style="margin-right: 3px;"
                                            OnClick="lnkbtnPgNext_Click">Next</asp:LinkButton>
                                    </div>
                                    <div class="clearfix padding_0 mt10">
                                    </div>
                                    <div class="text-center">
                                        <asp:Label ID="lblpaging" runat="server" class="btn btn-primary text-center" Text=""></asp:Label>
                                    </div>
                                </div>
                                <!-----------------------------------------end code for page indexing----------------------------------------------------->
                            </div>
                        </div>
                    </div>
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
                <div class="modal-dialog" style="width: 1200px; z-index: 9999999999;">
                    <!-- Modal content-->
                    <div class="modal-content">
                        <div class="modal-header modal-header1">
                            <button type="button" class="close close1" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Item Detail</h4>
                        </div>
                        <form class="form-horizontal changepassword" role="form">
                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="faq-secion product-view">
                                            <div class="accordion" id="accordion">
                                                <div class="card">
                                                    <div class="card-header">
                                                        <h2 data-toggle="collapse" data-parent="#accordion" data-target="#faq1" aria-expanded="false" aria-controls="faq1">Description 
                                                            <i class="fa fa-minus pull-right"></i>
                                                        </h2>
                                                    </div>
                                                    <div id="faq1" class="collapse in" aria-labelledby="headingOne">
                                                        <div class="card-body">
                                                            <ul>
                                                                <li>
                                                                    <div class="row two-col">
                                                                        <div class="col-md-6">
                                                                            <table>
                                                                                <tr>
                                                                                    <td>Refrence No:</td>
                                                                                    <td>
                                                                                        <asp:Label ID="lblcomprefno" runat="server" Text=""></asp:Label>
                                                                                    </td>

                                                                                </tr>
                                                                                <tr>
                                                                                    <td>Company:</td>
                                                                                    <td>
                                                                                        <asp:Label ID="lblcompname" runat="server" Text=""></asp:Label></td>
                                                                                </tr>

                                                                                <tr>
                                                                                    <td>Division/Palnt:</td>
                                                                                    <td>
                                                                                        <asp:Label ID="lbldiviname" runat="server" Text=""></asp:Label></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>Unit:</td>
                                                                                    <td>
                                                                                        <asp:Label ID="lblunitnamepro" runat="server" Text=""></asp:Label></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>Item Description:</td>
                                                                                    <td>
                                                                                        <asp:Label ID="lblproductdescription" runat="server" Text=""></asp:Label></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>Item Refrence No:</td>
                                                                                    <td>
                                                                                        <asp:Label ID="lblprodrefno" runat="server" Text=""></asp:Label></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>OEM Part Number:</td>
                                                                                    <td>
                                                                                        <asp:Label ID="lbloempartnumber" runat="server" Text=""></asp:Label></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>OEM Name:</td>
                                                                                    <td>
                                                                                        <asp:Label ID="lbloemname" runat="server" Text=""></asp:Label></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>OEM Country:</td>
                                                                                    <td>
                                                                                        <asp:Label ID="lbloemcountry" runat="server" Text=""></asp:Label></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>DPSU Part Number:</td>
                                                                                    <td>
                                                                                        <asp:Label ID="lbldpsupartno" runat="server" Text=""></asp:Label></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>HSN Code (8-digit)</td>
                                                                                    <td>
                                                                                        <asp:Label ID="lblhsncode8digit" runat="server" Text=""></asp:Label></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>End User:</td>
                                                                                    <td>
                                                                                        <asp:Label ID="lblenduser" runat="server" Text=""></asp:Label></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>NSN GROUP:</td>
                                                                                    <td>
                                                                                        <asp:Label ID="lblnsngroup" runat="server" Text=""></asp:Label></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>NSN GROUP CLASS:</td>
                                                                                    <td>
                                                                                        <asp:Label ID="lblnsngroupclass" runat="server" Text=""></asp:Label></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>CLASS ITEM:</td>
                                                                                    <td>
                                                                                        <asp:Label ID="lblclassitem" runat="server" Text=""></asp:Label></td>
                                                                                </tr>
                                                                                <tr id="Tr1" runat="server" visible="false">
                                                                                    <td>HSN Code:</td>
                                                                                    <td>
                                                                                        <asp:Label ID="lblhsncode" runat="server" Text=""></asp:Label></td>
                                                                                </tr>
                                                                                <tr id="Tr2" runat="server" visible="false">
                                                                                    <td>HS Chapter:</td>
                                                                                    <td>
                                                                                        <asp:Label ID="lblhschapter" runat="server" Text=""></asp:Label></td>
                                                                                </tr>
                                                                                <tr id="Tr3" runat="server" visible="false">
                                                                                    <td>HS Heading No:</td>
                                                                                    <td>
                                                                                        <asp:Label ID="lblhsncodelevel1" runat="server" Text=""></asp:Label></td>
                                                                                </tr>
                                                                                <tr id="Tr4" runat="server" visible="false">
                                                                                    <td>Description:</td>
                                                                                    <td>
                                                                                        <asp:Label ID="lblhsncodelevel2" runat="server" Text=""></asp:Label></td>
                                                                                </tr>
                                                                                <tr id="Tr5" runat="server" visible="false">
                                                                                    <td>HSN Code:</td>
                                                                                    <td>
                                                                                        <asp:Label ID="lblhsncodelevel3" runat="server" Text=""></asp:Label></td>
                                                                                </tr>
                                                                                <tr id="Tr6" runat="server" visible="false">
                                                                                    <td>HS Code (4 digit)</td>
                                                                                    <td>
                                                                                        <asp:Label ID="lblhscode4digit" runat="server" Text=""></asp:Label></td>
                                                                                </tr>
                                                                                <tr id="Tr7" runat="server" visible="false">
                                                                                    <td>End User Part Number:</td>
                                                                                    <td>
                                                                                        <asp:Label ID="lblenduserpartno" runat="server" Text=""></asp:Label></td>
                                                                                </tr>
                                                                            </table>
                                                                        </div>
                                                                        <div class="col-md-6">
                                                                            <table>

                                                                                <tr>
                                                                                    <td>NSC Code (4 digit):</td>
                                                                                    <td>
                                                                                        <asp:Label ID="lblnsccode" runat="server" Text=""></asp:Label></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>NIIN Code (9-digit):</td>
                                                                                    <td>
                                                                                        <asp:Label ID="lblniincode" runat="server" Text=""></asp:Label></td>
                                                                                </tr>

                                                                                <tr>
                                                                                    <td>DEFENCE PLATFORM:</td>
                                                                                    <td>
                                                                                        <asp:Label ID="lbldefenceplatform" runat="server" Text=""></asp:Label></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>NAME OF DEFENCE PLATFORM:</td>
                                                                                    <td>
                                                                                        <asp:Label ID="lblnameofdefenceplatform" runat="server" Text=""></asp:Label></td>

                                                                                </tr>
                                                                                <tr>
                                                                                    <td>PRODUCT (INDUSTRY DOMAIN):</td>
                                                                                    <td>
                                                                                        <asp:Label ID="prodIndustryDomain" runat="server" Text=""></asp:Label></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>PRODUCT (INDUSTRY SUB DOMAIN):</td>
                                                                                    <td>
                                                                                        <asp:Label ID="ProdIndusSubDomain" runat="server" Text=""></asp:Label></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>PRODUCT (INDUSTRY 2nd SUB DOMAIN):</td>
                                                                                    <td>
                                                                                        <asp:Label ID="ProdIndus2SubDomain" runat="server" Text=""></asp:Label></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>Search Keywords:</td>
                                                                                    <td>
                                                                                        <asp:Label ID="lblsearchkeyword" runat="server" Text=""></asp:Label></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>Item Already Indeginized</td>
                                                                                    <td>
                                                                                        <asp:Label ID="lblprodalredyindeginized" runat="server" Text=""></asp:Label></td>
                                                                                </tr>
                                                                                <div runat="server" id="tableIsIndiginized">
                                                                                    <tr>
                                                                                        <td>Manufacturer Name</td>
                                                                                        <td>
                                                                                            <asp:Label ID="lblmanufacturename" runat="server" Text=""></asp:Label></td>

                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>Address</td>

                                                                                        <td>
                                                                                            <asp:Label ID="lblmanaddress" runat="server" Text=""></asp:Label></td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>Year of Indiginization</td>
                                                                                        <td>
                                                                                            <asp:Label ID="lblyearofindiginization" runat="server" Text=""></asp:Label></td>
                                                                                    </tr>

                                                                                </div>
                                                                                <tr>
                                                                                    <td>Is Item Imported</td>
                                                                                    <td>
                                                                                        <asp:Label ID="lblisproductimported" runat="server" Text=""></asp:Label></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>Year of Import</td>
                                                                                    <td>
                                                                                        <asp:Label ID="lblyearofimport" runat="server" Text=""></asp:Label></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>Remarks</td>
                                                                                    <td>
                                                                                        <asp:Label ID="lblremarksproductimported" runat="server" Text=""></asp:Label></td>
                                                                                </tr>
                                                                            </table>
                                                                        </div>
                                                                    </div>
                                                                </li>
                                                            </ul>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="card">
                                                    <div class="card-header">
                                                        <h2 data-toggle="collapse" data-parent="#accordion" class="collapsed" data-target="#faq2" aria-expanded="false" aria-controls="faq2">Item Specification
                                                            <i class="fa fa-plus pull-right"></i>
                                                        </h2>
                                                    </div>
                                                    <div id="faq2" class="collapse">
                                                        <div class="card-body">
                                                            <div class="row">
                                                                <div class="col-md-12">
                                                                    <table>
                                                                        <tr>
                                                                            <td>Document related to item</td>
                                                                            <div runat="server" id="itemdocument">
                                                                                <td>
                                                                                    <a href="#" target="_blank" runat="server" id="a_downitem" class="fa fa-download"></a>
                                                                                    <span data-toggle="tooltip" class="fa fa-question" title="Click on icon for downloaf"></span>
                                                                                </td>
                                                                            </div>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>Item Image</td>
                                                                            <td>
                                                                                <asp:DataList ID="dlimage" runat="server" RepeatColumns="4" Visible="true" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                                                                    <ItemTemplate>
                                                                                        <div class="col-sm-3">
                                                                                            <a data-fancybox="Prodgridviewgellry" href='<%#Eval("[ImageName]") %>'>
                                                                                                <asp:Image ID="imgprodimage" runat="server" CssClass="img-responsive img-container" Height="90px" Width="110px" src='<%#Eval("[ImageName]") %>' />
                                                                                            </a>
                                                                                        </div>
                                                                                    </ItemTemplate>
                                                                                </asp:DataList>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>Features & Details</td>
                                                                            <td>
                                                                                <asp:Label ID="lblfeaturesanddetail" runat="server" Text=""></asp:Label></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>Item Specification</td>
                                                                            <td>
                                                                                <asp:Label ID="lblitemspecification" runat="server" Text=""></asp:Label></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>Additional Information</td>
                                                                            <td>
                                                                                <asp:Label ID="lbladditionalinfo" runat="server" Text=""></asp:Label></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>Item Information
                                                                            </td>
                                                                            <td>&nbsp;
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="2">
                                                                                <asp:GridView ID="gvProdInfo" runat="server" AutoGenerateColumns="false" CssClass="table table-hover">
                                                                                    <Columns>
                                                                                        <asp:BoundField DataField="NameOfSpec" HeaderText="Name of Specification" />
                                                                                        <asp:BoundField DataField="Value" HeaderText="Value " />
                                                                                        <asp:BoundField DataField="Unit" HeaderText="Unit" />
                                                                                    </Columns>

                                                                                </asp:GridView>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="card" runat="server">
                                                    <div class="card-header">
                                                        <h2 data-toggle="collapse" data-parent="#accordion" class="collapsed" data-target="#faq3" aria-expanded="false" aria-controls="faq3">Estimated Quantity
                                                            <i class="fa fa-plus pull-right"></i>
                                                        </h2>
                                                    </div>
                                                    <div id="faq3" class="collapse">
                                                        <div class="card-body">
                                                            <div class="row">
                                                                <div class="col-md-12">
                                                                    <table>

                                                                        <tr>
                                                                            <td>PROCURMENT CATEGORY</td>
                                                                            <td>
                                                                                <asp:Label ID="lblpurposeofprocurement" runat="server" Text=""></asp:Label></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>PROCURMENT CATEGORY REMARK</td>
                                                                            <td>
                                                                                <asp:Label ID="lblprocremarks" runat="server" Text=""></asp:Label></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>Estimate Quantity or Price</td>
                                                                            <td>&nbsp;
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="2">
                                                                                <asp:GridView ID="gvestimatequanorprice" runat="server" AutoGenerateColumns="false" CssClass="table table-hover">
                                                                                    <Columns>
                                                                                        <asp:BoundField DataField="FYear" HeaderText="Year" />
                                                                                        <asp:BoundField DataField="EstimatedQty" HeaderText="Estimated Quantity" />
                                                                                        <asp:BoundField DataField="Unit" HeaderText="Measuring Unit" />
                                                                                        <%--<asp:BoundField DataField="EstimatedPrice" HeaderText="Estimated Price / LPP" />--%>
                                                                                    </Columns>
                                                                                </asp:GridView>
                                                                            </td>
                                                                        </tr>
                                                                    </table>

                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="card" runat="server" visible="false">
                                                    <div class="card-header">
                                                        <h2 data-toggle="collapse" data-parent="#accordion" class="collapsed" data-target="#faq4" aria-expanded="false" aria-controls="faq4">Testing & Certification
                                                            <i class="fa fa-plus pull-right"></i>
                                                        </h2>
                                                    </div>
                                                    <div id="faq4" class="collapse">
                                                        <div class="card-body">
                                                            <div class="row">
                                                                <div class="col-md-12">
                                                                    <table>
                                                                        <tr>
                                                                            <td>QA Agency:</td>
                                                                            <td>
                                                                                <asp:Label ID="lblqaagency" runat="server" Text=""></asp:Label></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>Remarks:</td>
                                                                            <td>
                                                                                <asp:Label ID="lblremarks" runat="server" Text=""></asp:Label></td>
                                                                        </tr>

                                                                    </table>
                                                                </div>
                                                                <div class="col-md-12">
                                                                    <table>
                                                                        <tr>
                                                                            <td>Testing:</td>
                                                                            <td>
                                                                                <asp:Label ID="lbltesting" runat="server" Text=""></asp:Label></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>Remarks:</td>
                                                                            <td>
                                                                                <asp:Label ID="lbltestingremarks" runat="server" Text=""></asp:Label></td>
                                                                        </tr>

                                                                    </table>
                                                                </div>
                                                                <div class="col-md-12">
                                                                    <table>
                                                                        <tr>
                                                                            <td>Certification:</td>
                                                                            <td>
                                                                                <asp:Label ID="lblcertification" runat="server" Text=""></asp:Label></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>Remarks:</td>
                                                                            <td>
                                                                                <asp:Label ID="lblcertificationremarks" runat="server" Text=""></asp:Label></td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="card" runat="server" visible="false">
                                                    <div class="card-header">
                                                        <h2 data-toggle="collapse" data-parent="#accordion" class="collapsed" data-target="#faq6" aria-expanded="false" aria-controls="faq6">Technical & Financial Support
                                                            <i class="fa fa-plus pull-right"></i>
                                                        </h2>
                                                    </div>
                                                    <div id="faq6" class="collapse">
                                                        <div class="card-body">
                                                            <div class="row">
                                                                <div class="col-md-12">
                                                                    <table>
                                                                        <tr>
                                                                            <td>Technical Support provided by DPSU:</td>
                                                                            <td>
                                                                                <asp:Label ID="lblsupportprovidedbydpsu" runat="server" Text=""></asp:Label></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>Remarks:</td>
                                                                            <td>
                                                                                <asp:Label ID="lblremarksdpsu" runat="server" Text=""></asp:Label></td>
                                                                        </tr>

                                                                    </table>
                                                                </div>
                                                                <div class="col-md-12">
                                                                    <table>
                                                                        <tr>
                                                                            <td>Financial Support provided by DPSU:</td>
                                                                            <td>
                                                                                <asp:Label ID="lblfinancial" runat="server" Text=""></asp:Label></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>Remarks:</td>
                                                                            <td>
                                                                                <asp:Label ID="lblfinancialRemark" runat="server" Text=""></asp:Label></td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="card">
                                                    <div class="card-header">
                                                        <h2 data-toggle="collapse" data-parent="#accordion" class="collapsed" data-target="#faq9" aria-expanded="false" aria-controls="faq9">Tender
                                                            <i class="fa fa-plus pull-right"></i>
                                                        </h2>
                                                    </div>

                                                    <div id="faq9" class="collapse">
                                                        <div class="card-body">
                                                            <div class="row">
                                                                <div class="col-md-6">
                                                                    <table>
                                                                        <tr>
                                                                            <td>Tender Status:</td>
                                                                            <td>
                                                                                <asp:Label ID="lbltenderstatus" runat="server" Text=""></asp:Label></td>
                                                                        </tr>

                                                                        <tr>
                                                                            <td>Tender Submission:</td>
                                                                            <td>
                                                                                <asp:Label ID="lbltendersubmission" runat="server" Text=""></asp:Label></td>
                                                                        </tr>
                                                                        <table runat="server" id="tenderstatus">
                                                                            <tr>
                                                                                <td>Tender Date:</td>
                                                                                <td>
                                                                                    <asp:Label ID="lbltenderdate" runat="server" Text=""></asp:Label></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>Tender URL:</td>
                                                                                <td>
                                                                                    <asp:Label ID="lbltenderurl" runat="server" Text=""></asp:Label></td>
                                                                            </tr>
                                                                        </table>
                                                                    </table>
                                                                </div>
                                                                <div class="col-md-6">
                                                                    <table>
                                                                        <tr>
                                                                            <td>EOI Status:</td>
                                                                            <td>
                                                                                <asp:Label ID="lbleoistatus" runat="server" Text=""></asp:Label></td>
                                                                        </tr>

                                                                        <tr>
                                                                            <td>EOI Submission:</td>
                                                                            <td>
                                                                                <asp:Label ID="lbleoisub" runat="server" Text=""></asp:Label></td>
                                                                        </tr>
                                                                        <table runat="server" id="tbleoidate">
                                                                            <tr>
                                                                                <td>EOI Date:</td>
                                                                                <td>
                                                                                    <asp:Label ID="lbleoidate" runat="server" Text=""></asp:Label></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>EOI URL:</td>
                                                                                <td>
                                                                                    <asp:Label ID="lbleoiurl" runat="server" Text=""></asp:Label></td>
                                                                            </tr>
                                                                        </table>
                                                                    </table>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="card">
                                                    <div class="card-header">
                                                        <h2 data-toggle="collapse" data-parent="#accordion" class="collapsed" data-target="#faq10" aria-expanded="false" aria-controls="faq10">Contact
                                                            <i class="fa fa-plus pull-right"></i>
                                                        </h2>
                                                    </div>

                                                    <div id="faq10" class="collapse">
                                                        <div class="card-body">
                                                            <div class="row">
                                                                <div class="col-md-6">
                                                                    <table>
                                                                        <tr>
                                                                            <td>Nodel Detail -1</td>
                                                                            <tr>
                                                                                <td>Employee Code:</td>
                                                                                <td>
                                                                                    <asp:Label ID="lblempcode" runat="server" Text=""></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>Employee Name:</td>
                                                                                <td>
                                                                                    <asp:Label ID="lblempname" runat="server" Text=""></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>Designation:</td>
                                                                                <td>
                                                                                    <asp:Label ID="lbldesignation" runat="server" Text=""></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>E-Mail ID:</td>
                                                                                <td>
                                                                                    <asp:Label ID="lblemailidpro" runat="server" Text=""></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>Mobile Number:</td>
                                                                                <td>
                                                                                    <asp:Label ID="lblmobilenumber" runat="server" Text=""></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>Phone Number:</td>
                                                                                <td>
                                                                                    <asp:Label ID="lblphonenumber" runat="server" Text=""></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>Fax:</td>
                                                                                <td>
                                                                                    <asp:Label ID="lblfaxpro" runat="server" Text=""></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                        </tr>

                                                                    </table>
                                                                </div>
                                                                <div class="col-md-6">
                                                                    <table>
                                                                        <tr>
                                                                            <td>Nodel Detail -2</td>
                                                                            <tr>
                                                                                <td>Employee Code:</td>
                                                                                <td>
                                                                                    <asp:Label ID="lblempcode2" runat="server" Text=""></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>Employee Name:</td>
                                                                                <td>
                                                                                    <asp:Label ID="lblempname2" runat="server" Text=""></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>Designation:</td>
                                                                                <td>
                                                                                    <asp:Label ID="lbldesignation2" runat="server" Text=""></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>E-Mail ID:</td>
                                                                                <td>
                                                                                    <asp:Label ID="lblemailid2" runat="server" Text=""></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>Mobile Number:</td>
                                                                                <td>
                                                                                    <asp:Label ID="lblmobilenumber2" runat="server" Text=""></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>Phone Number:</td>
                                                                                <td>
                                                                                    <asp:Label ID="lblphonenumber2" runat="server" Text=""></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>Fax:</td>
                                                                                <td>
                                                                                    <asp:Label ID="lblfax2" runat="server" Text=""></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                            </div>
                                                        </div>
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
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="gvproduct" EventName="RowCommand" />
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
