﻿<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPage.master" CodeFile="ViewNodalOfficer.aspx.cs" Inherits="Admin_ViewNodalOfficer" %>

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
                                <a class="fa fa-arrow-circle-left pull-right" href="javascript: history.go(-1)">&nbsp; &nbsp;Back</a>
                            </div>
                            <div class="clearfix"></div>
                        </div>
                    </div>

                    <div class="clearfix" style="margin-bottom: 10px;"></div>
                    <form method="post" class="addfdi">
                        <div class="row">
                            <div class="col-md-12">

                                <div class="clearfix"></div>
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
                                        <div class="col-md-4" runat="server" id="lblselectcompany">
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
                                <div id="divTotalNumber" class="text-center" style="font-size: 16px; margin-top: 10px;" runat="server" visible="False">

                                    <asp:Label ID="lbltotal" runat="server" Text=""></asp:Label>
                                </div>
                                <div class="clearfix"></div>
                                <div id="Div3">
                                    <asp:Button ID="btnAddNodalOfficer" runat="server" Text="Add Employee" Visible="False" CssClass="btn btn-primary pull-right" OnClick="btnAddNodalOfficer_Click" />

                                </div>

                                <div class="clearfix mt10"></div>
                                <div class="table-responsive">
                                    <asp:HiddenField ID="hfmrole" runat="server" />
                                    <div class="clearfix mt10"></div>
                                    <asp:GridView ID="gvViewNodalOfficer" runat="server" Width="100%" Class="table table-hover manage-user"
                                        AutoGenerateColumns="false" OnRowCommand="gvViewDesignation_RowCommand" OnRowDataBound="gvViewNodalOfficer_RowDataBound"
                                        OnRowCreated="gvViewNodalOfficer_RowCreated">
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
                                            <asp:TemplateField HeaderText="Role">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblnodalofficer" runat="server" Text='<%#Eval("IsNodalOfficer") %>' NullDisplayText="#" Visible="False" SortExpression="NodalOfficerEmail"></asp:Label>
                                                    <asp:Label ID="lblnodallogactive" runat="server" Text='<%#Eval("IsLoginActive") %>' NullDisplayText="#" Visible="False" SortExpression="NodalOfficerEmail"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Company">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblrefno" runat="server" Text='<%#Eval("CompanyName") %>' NullDisplayText="#" SortExpression="Company"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="FactoryName" HeaderText="Division" NullDisplayText="-" />
                                            <asp:BoundField DataField="UnitName" HeaderText="Unit" NullDisplayText="-" />
                                            <asp:BoundField DataField="CreatedBy" HeaderText="Created By" NullDisplayText="-" />
                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lblview" runat="server" ToolTip="View Detail of Nodal Officer/Employee" CssClass="fa fa-eye" CommandName="ViewComp" CommandArgument='<%#Eval("NodalOfficerID") %>'></asp:LinkButton>
                                                    <asp:HiddenField runat="server" ID="hfnodalrole" Value='<%#Eval("Type") %>' />
                                                    <asp:LinkButton ID="lbledit" runat="server" ToolTip="Edit or Update Nodal officer/Employee" CssClass="fa fa-edit" CommandName="EditComp" CommandArgument='<%#Eval("NodalOfficerID") %>'></asp:LinkButton>
                                                    <asp:LinkButton ID="lbldel" runat="server" Visible="False" CssClass="fa fa-trash" CommandName="DeleteComp" OnClientClick="return confirm('Are you sure you want to delete this Company?');" CommandArgument='<%#Eval("NodalOfficerID") %>'></asp:LinkButton>
                                                    <asp:LinkButton ID="lbllogindetail" runat="server" ToolTip="Send mail to create password" CssClass=" fa fa-paper-plane" CommandName="SendLogin" OnClientClick="return confirm('Are you sure you want to send login detail?');" CommandArgument='<%#Eval("NodalOfficerID") %>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
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
                                                <asp:LinkButton ID="lnkbtnPgNext" runat="server" CssClass="btn  btn-info btn-sm pull-right" Style="margin-right: 3px;"
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
                    </form>
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
                                                <td class="pass">Refernce No</td>
                                                <td>
                                                    <asp:Label ID="lblNodalOfficerRefNo" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="pass">Name</td>
                                                <td>
                                                    <asp:Label ID="lblNodalOficerName" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="pass">Email ID</td>
                                                <td>
                                                    <asp:Label ID="lblEmail" runat="server"></asp:Label>
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
                                                <td class="pass">Mobile Number</td>
                                                <td>
                                                    <asp:Label ID="lblMobile" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="pass">Telephone No</td>
                                                <td>
                                                    <asp:Label ID="lblTelephone" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="pass">Fax No</td>
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
