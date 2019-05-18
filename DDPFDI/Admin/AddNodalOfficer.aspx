﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddNodalOfficer.aspx.cs" Inherits="Admin_AddNodalOfficer" MasterPageFile="~/Admin/MasterPage.master" %>

<asp:Content ID="AddNodal" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div class="content oem-content">
        <asp:ScriptManager ID="sc" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel runat="server" ID="updatepan">
            <ContentTemplate>
                <asp:HiddenField ID="hidCompanyRefNo" runat="server" />
                <asp:HiddenField ID="hidType" runat="server" />
                <div class="sideBg">
                    <div class="row">
                        <div class="col-mod-12 padding_0">
                            <div id="divHeadPage" runat="server"></div>
                        </div>
                    </div>
                    <div class="NodalOfficer">
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
                        <div class="add-profile">
                            <div class="section-pannel">
                                <div class="row">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Name</label>
                                            <asp:TextBox class="form-control" required="" runat="server" ID="txtname"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Designation</label>
                                            <asp:DropDownList runat="server" ID="ddldesignation" class="form-control" />
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Emp Code</label>
                                            <asp:TextBox class="form-control" runat="server" ID="txtEmpCode"></asp:TextBox>
                                        </div>
                                    </div>

                                </div>
                                <div class="row">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Email ID</label>
                                            <asp:TextBox class="form-control" required="" pattern="[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,}$" runat="server" ID="txtemailid"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Mobile</label>
                                            <asp:TextBox class="form-control" runat="server" ID="txtmobile"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Telephone </label>
                                            <asp:TextBox class="form-control" runat="server" ID="txttelephone"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Fax </label>
                                            <asp:TextBox class="form-control" runat="server" ID="txtfax"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="fdi-add-content NodalRole" id="DivNodalRole" runat="server">
                                            <div class="form-group">
                                                <asp:CheckBox ID="chkrole" Text="Nodal Officer" runat="server" style="margin-left: 20px;" CssClass="checkbox-inline"></asp:CheckBox>
                                                <asp:CheckBox ID="chkUser" Text="User" runat="server" style="margin-left: 20px;" CssClass="checkbox-inline"></asp:CheckBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <asp:Button ID="btncancel" runat="server" Text="Cancel" CssClass="btn btn-default pull-right" Style="margin-right: 0 !important" OnClick="btncancel_Click" />
                                            <asp:LinkButton ID="btnsub" runat="server" Text="Save" class="btn btn-primary pull-right" OnClick="btnsub_Click" OnClientClick="return confirm('Are you sure you want to save this nodal officer?');"></asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="table-wraper">
                                <asp:GridView ID="gvViewNodalOfficer" runat="server" Width="100%" Class="commonAjaxTbl master-company-table table display responsive no-wrap table-hover manage-user Grid"
                                    AutoGenerateColumns="false" AllowPaging="true" PageSize="25" AllowSorting="true" OnRowDataBound="gvViewNodalOfficer_RowDataBound">
                                    <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="S.No.">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="NodalOficerName" HeaderText="Name" NullDisplayText="#" />
                                        <asp:BoundField DataField="NodalOfficerRefNo" HeaderText="Reference No." Visible="False" NullDisplayText="#" />
                                        <asp:BoundField DataField="NodalOfficerEmail" HeaderText="Email" NullDisplayText="#" />
                                        <asp:BoundField DataField="IsNodalOfficer" HeaderText="Nodal Officer" NullDisplayText="#" />
                                        <asp:BoundField DataField="IsLoginActive" HeaderText="User" NullDisplayText="#" />
                                        <asp:TemplateField HeaderText="Company">
                                            <ItemTemplate>
                                                <asp:Label ID="lblrefno" runat="server" Text='<%#Eval("CompanyName") %>' NullDisplayText="#" SortExpression="Company"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="FactoryName" HeaderText="Division" NullDisplayText="-" />
                                        <asp:BoundField DataField="UnitName" HeaderText="Unit" NullDisplayText="-" />

                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdateProgress ID="UpdateProgress" runat="server" AssociatedUpdatePanelID="updatepan">
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
    </div>

</asp:Content>
