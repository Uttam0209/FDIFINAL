<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UpdateCatstatus.aspx.cs" Inherits="Admin_UpdateCatstatus" MasterPageFile="~/Admin/MasterPage.master" %>

<asp:Content ID="headDesignation" runat="server" ContentPlaceHolderID="head">
    <script>
        function ShowMessage() {
            console.log('testing');
            $("body").css('overflow', 'hidden');
            $('.alert-overlay-success').show();
        }
    </script>
</asp:Content>
<asp:Content ID="InnerDesignation" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div class="content oem-content">
        
        <asp:UpdatePanel runat="server" ID="updatepan">
            <ContentTemplate>
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
                    <div class="addfdi">
                        <div class="section-pannel">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="fdi-add-content">
                                        <div class="row">
                                            <div class="text-center">
                                                <asp:RadioButtonList ID="rbtype" runat="server" RepeatColumns="3" RepeatLayout="Flow" AutoPostBack="true" RepeatDirection="Horizontal" OnSelectedIndexChanged="rbtype_SelectedIndexChanged">
                                                    <asp:ListItem Value="1" Selected="True">Master Category</asp:ListItem>
                                                    <asp:ListItem Value="2" style="margin-left: 10px;">SubCategory</asp:ListItem>
                                                    <asp:ListItem Value="3" style="margin-left: 10px;">2nd Level Sub Category</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                        </div>
                                        <div class="clearfix mt10"></div>
                                        <div class="table-wraper table-responsive">
                                            <asp:GridView ID="gvmastercategoryupdate" runat="server" AutoGenerateColumns="false" Width="100%" OnRowDataBound="gvmastercategoryupdate_RowDataBound"
                                                Class="commonAjaxTbl master-company-table table display responsive no-wrap table-hover manage-user Grid" OnRowCommand="gvmastercategoryupdate_RowCommand" OnRowCreated="gvmastercategoryupdate_RowCreated">
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <%#Container.DataItemIndex+1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="MCategoryName" HeaderText="CategoryName" />
                                                    <asp:BoundField DataField="IsActive" HeaderText="Status" />
                                                    <asp:TemplateField HeaderText="Action">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbactive" runat="server" CssClass="fa fa-check-circle" OnClientClick="return confirm('Are you sure you want to active this category record?');" CommandArgument='<%#Eval("MCategoryID") %>' CommandName="active"></asp:LinkButton>
                                                            <asp:LinkButton ID="lbunactive" runat="server" CssClass="fa fa-trash-restore" OnClientClick="return confirm('Are you sure you want to deactive this category record?');" CommandArgument='<%#Eval("MCategoryID") %>' CommandName="deactive"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                        <div class="table-wraper table-responsive">
                                            <asp:GridView ID="gvmastersubcategory" runat="server" AutoGenerateColumns="false" Width="100%" OnRowDataBound="gvmastersubcategory_RowDataBound"
                                                Class="commonAjaxTbl master-company-table table display responsive no-wrap table-hover manage-user Grid" OnRowCommand="gvmastersubcategory_RowCommand" OnRowCreated="gvmastersubcategory_RowCreated">
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <%#Container.DataItemIndex+1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="MasterCategory" HeaderText="CategoryName" />
                                                    <asp:BoundField DataField="SCategoryName" HeaderText="Sub CategoryName" />
                                                    <asp:BoundField DataField="IsActive" HeaderText="Status" />
                                                    <asp:TemplateField HeaderText="Action">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbsubactive" runat="server" CssClass="fa fa-check-circle" OnClientClick="return confirm('Are you sure you want to active this subcategory record?');" CommandArgument='<%#Eval("SCategoryID") %>' CommandName="subactive"></asp:LinkButton>
                                                            <asp:LinkButton ID="lbsubunactive" runat="server" CssClass="fa fa-trash-restore" OnClientClick="return confirm('Are you sure you want to deactive this subcategory record?');" CommandArgument='<%#Eval("SCategoryID") %>' CommandName="subdeactive"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdateProgress ID="UpdateProgress" runat="server" AssociatedUpdatePanelID="updatepan">
            <ProgressTemplate>
                <div class="overlay-progress">
                    <div class="custom-progress-bar blue stripes">
                        <span></span>
                        <p>Processing</p>
                    </div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
</asp:Content>
