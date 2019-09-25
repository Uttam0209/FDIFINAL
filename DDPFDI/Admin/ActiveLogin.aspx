<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ActiveLogin.aspx.cs" Inherits="Admin_ActiveLogin" MasterPageFile="~/Admin/MasterPage.master" %>

<asp:Content ID="AddNodal" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div class="content oem-content">
        <asp:UpdatePanel runat="server" ID="updatepan">
            <ContentTemplate>
                <asp:HiddenField ID="hidCompanyRefNo" runat="server" />
                <div class="sideBg">
                    <div class="row">
                        <div class="col-mod-12 padding_0">
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
                    <div class="col-sm-4" style="display: flex">
                        <asp:TextBox runat="server" ID="txtsearch" CssClass="form-control btn-defualt text-center red" AutoCompleteType="Search" Placeholder="Please enter email"></asp:TextBox>
                        <asp:LinkButton ID="btnsearch" runat="server" CssClass="btn btn-primary" OnClick="btnsearch_Click">Search</asp:LinkButton>
                    </div>
                    <div class="clearfix" style="margin-bottom: 10px;"></div>
                    <div class="add-profile">

                        <div class="table-wraper table-responsive">
                            <asp:GridView ID="gvViewNodalOfficerAdd" runat="server" Width="100%" Class="commonAjaxTbl master-company-table table display 
                                    responsive no-wrap table-hover manage-user Grid"
                                AutoGenerateColumns="false" OnRowCreated="gvViewNodalOfficerAdd_RowCreated" OnRowCommand="gvViewNodalOfficerAdd_RowCommand">
                                <Columns>
                                    <asp:TemplateField HeaderText="S.No.">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="LoginUser" HeaderText="User Email" NullDisplayText="#" />
                                    <asp:BoundField DataField="IsLogedIn" HeaderText="Is Acitve Status" NullDisplayText="#" />
                                    <asp:BoundField DataField="IsLogedOutTime" HeaderText="Date time Login" DataFormatString="{0:dd-MMM-yyyy hh:mm:ss}" NullDisplayText="#" />
                                    <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lblactive" runat="server" CssClass="fa fa-check-circle" CommandArgument='<%#Eval("LogID") %>' CommandName="active"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                            </asp:GridView>
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
