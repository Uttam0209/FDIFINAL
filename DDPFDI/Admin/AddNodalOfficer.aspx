<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddNodalOfficer.aspx.cs" Inherits="Admin_AddNodalOfficer" MasterPageFile="~/Admin/MasterPage.master" %>

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
                                <asp:Panel ID="Panel1" runat="server" DefaultButton="btnsub">
                                    <div class="row">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>Name</label>
                                                <asp:TextBox class="form-control" required="" TabIndex="1" runat="server" ID="txtname"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>Designation</label><span data-toggle="tooltip" tabindex="2" class="fa fa-question" title="Please select designation (if you are not see any designation please add master designation in designation section."></span>
                                                <asp:DropDownList runat="server" ID="ddldesignation" class="form-control" />
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>Emp Code</label>
                                                <asp:TextBox class="form-control" runat="server" TabIndex="3" ID="txtEmpCode"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>Email ID</label>
                                                <asp:TextBox class="form-control" required="" TabIndex="4" pattern="[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,}$" runat="server" ID="txtemailid"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>Mobile</label>
                                                <asp:TextBox class="form-control" runat="server" TabIndex="5" MaxLength="12" onkeydown="return onlyNos(event)" ID="txtmobile"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>Telephone </label>
                                                <asp:TextBox class="form-control" runat="server" MaxLength="10" TabIndex="6" ID="txttelephone"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>Fax </label>
                                                <asp:TextBox class="form-control" runat="server" MaxLength="10" TabIndex="7" ID="txtfax"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="fdi-add-content NodalRole" id="DivNodalRole" runat="server">
                                                <div class="form-group">
                                                    <asp:CheckBoxList runat="server" Style="margin-left: 20px;" CssClass="checkbox-inline" ID="chkrole" TabIndex="8" ToolTip="If you want to give login access please select nodel or user/if check is not selected it will be default employee.">
                                                        <asp:ListItem Text="Nodal Officer"></asp:ListItem>
                                                        <asp:ListItem Text="User"></asp:ListItem>
                                                    </asp:CheckBoxList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <asp:Button ID="btncancel" runat="server" Text="Cancel" TabIndex="10" CssClass="btn btn-default pull-right" Style="margin-right: 0 !important" OnClick="btncancel_Click" />
                                                <asp:LinkButton ID="btnsub" runat="server" Text="Save" TabIndex="9" class="btn btn-primary pull-right" OnClick="btnsub_Click" OnClientClick="return confirm('Are you sure you want to save this nodal officer?');"></asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
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
                                        <asp:TemplateField HeaderText="Role">
                                            <ItemTemplate>
                                                <asp:Label ID="lblnodalofficer" runat="server" Text='<%#Eval("IsNodalOfficer") %>' NullDisplayText="#" Visible="False" SortExpression="NodalOfficerEmail"></asp:Label>
                                                <asp:Label ID="lblnodallogactive" runat="server" Text='<%#Eval("IsLoginActive") %>' NullDisplayText="#" Visible="False" SortExpression="NodalOfficerEmail"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="CreatedBy" HeaderText="Create By" NullDisplayText="#" />
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
