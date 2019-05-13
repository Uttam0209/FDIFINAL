<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddNodalOfficer.aspx.cs" Inherits="Admin_AddNodalOfficer" MasterPageFile="~/Admin/MasterPage.master" %>

<asp:Content ID="AddNodal" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div class="content oem-content">
        <asp:ScriptManager ID="sc" runat="server"></asp:ScriptManager>
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
                                    <asp:TextBox class="form-control" runat="server" ID="txtname"></asp:TextBox>
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
                                    <asp:TextBox class="form-control" runat="server" ID="txtemailid"></asp:TextBox>
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

                            <div class="col-md-8">
                                <div class="fdi-add-content" id="DivNodalRole" runat="server">
                                  <div class="form-group">
                                            <%--<h4 class="secondary-heading">Nodal</h4>--%>
                                            <asp:CheckBox ID="chkrole" Text="Nodal Officer" runat="server"  CssClass="checkbox-inline" >
                                            </asp:CheckBox>
                                        
                                    </div>
                                </div>
                                

                            </div>
                             </div>

                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                         <asp:Button ID="btncancel" runat="server" Text="Cancel" CssClass="btn btn-default pull-right" OnClick="btncancel_Click" />
                                        <asp:LinkButton ID="btnsub" runat="server" Text="Save" class="btn btn-primary pull-right" OnClick="btnsub_Click"></asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                    </div>
                            <div class="table-wraper">
                                <asp:GridView ID="gvViewNodalOfficer" runat="server" Width="100%" Class="commonAjaxTbl master-company-table table display responsive no-wrap table-hover manage-user Grid" AutoGenerateColumns="false" AllowPaging="true"
                                    PageSize="25" AllowSorting="true">
                                    <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
                                    <Columns>

                                        <asp:TemplateField HeaderText="S.No.">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Company">
                                            <ItemTemplate>
                                                <asp:Label ID="lblrefno" runat="server" Text='<%#Eval("CompanyName") %>' NullDisplayText="#" SortExpression="Company"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="FactoryName" HeaderText="Division" NullDisplayText="-"  />
                                         <asp:BoundField DataField="UnitName" HeaderText="Unit" NullDisplayText="-"  />
                                        <asp:BoundField DataField="NodalOficerName" HeaderText="Name" NullDisplayText="#"  />
                                        <asp:BoundField DataField="NodalOfficerEmail" HeaderText="Email" NullDisplayText="#"  />
                                        <asp:BoundField DataField="IsNodalOfficer" HeaderText="Nodal Officer" NullDisplayText="#"  />
                                        

                                    </Columns>
                                </asp:GridView>
                            </div>
    </div>
    </div>
                </div>
            </div>
        </div>
</asp:Content>
