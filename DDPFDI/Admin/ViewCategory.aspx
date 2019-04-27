<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewCategory.aspx.cs" Inherits="Admin_ViewCategory" MasterPageFile="~/Admin/MasterPage.master" %>

<asp:Content ID="head" runat="server" ContentPlaceHolderID="head">
</asp:Content>
<asp:Content ID="inner" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <asp:ScriptManager ID="sc" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="up" runat="server">
        <ContentTemplate>
            <div class="content oem-content">
                <div class="sideBg">
                    <div class="col-mod-12">
                        <ul class="breadcrumb">
                            <asp:Label ID="lblPageName" runat="server" Text=""></asp:Label></li>
                        </ul>
                    </div>
                    <form method="post" class="addfdi">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="table-wrapper">
                                    <div class="table-wraper">
                                        <asp:GridView ID="gvCategory" runat="server" Width="100%" Class="commonAjaxTbl master-company-table table display responsive no-wrap table-hover manage-user Grid" AutoGenerateColumns="false" AllowPaging="true"
                                            OnPageIndexChanging="OnPageIndexChanging" PageSize="25" AllowSorting="true" OnSorting="OnSorting" OnRowDataBound="OnRowDataBound">
                                            <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <div class="acordian-table">
                                                            <i class="toggle-table-minus fa fa-minus" aria-hidden="true" style="display: none"></i>
                                                            <i class="toggle-table-plus fa fa-plus" aria-hidden="true"></i>
                                                            <asp:Panel ID="pnlOrders" runat="server" Style="display: none">
                                                                <asp:GridView ID="gvsubcat" runat="server" AutoGenerateColumns="false" Class="table table-hover ChildGrid">
                                                                    <Columns>
                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="S.No">
                                                                            <ItemTemplate>
                                                                                <%#Container.DataItemIndex+1 %>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Category Name">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblfactoryrefno" Text='<%#Eval("MCategoryName") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:BoundField ItemStyle-Width="150px" DataField="SCategoryName" HeaderText="Sub Category Name" />
                                                                        <asp:TemplateField HeaderText="Action" Visible="False">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lblviewfactory" runat="server" CssClass="fa fa-eye" CommandName="ViewfactoryComp" CommandArgument='<%#Eval("SCategoryId") %>'></asp:LinkButton>
                                                                                <asp:LinkButton ID="lbleditfactory" runat="server" CssClass="fa fa-edit" CommandName="EditfactoryComp" CommandArgument='<%#Eval("SCategoryId") %>'></asp:LinkButton>
                                                                                <asp:LinkButton ID="lbldelfactory" runat="server" CssClass="fa fa-trash" CommandName="DeletefactoryComp" OnClientClick="return confirm('Are you sure you want to delete this division/plant?');" CommandArgument='<%#Eval("SCategoryId") %>'></asp:LinkButton>
                                                                                <asp:LinkButton ID="lbllogindetailfactory" runat="server" Visible="False" CssClass=" fa fa-paper-plane" CommandName="factorySendLogin" CommandArgument='<%#Eval("SCategoryId") %>'></asp:LinkButton>
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
                                                <asp:TemplateField HeaderText="Category Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblrefno" runat="server" Text='<%#Eval("MCategoryName") %>' NullDisplayText="#" SortExpression="CompanyRefNo"></asp:Label>
                                                        <asp:HiddenField ID="hfcat" runat="server" Value='<%#Eval("MCategoryId") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Action" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lblview" runat="server" CssClass="fa fa-eye" CommandName="ViewComp" CommandArgument='<%#Eval("MCategoryId") %>'></asp:LinkButton>
                                                        <asp:LinkButton ID="lbledit" runat="server" CssClass="fa fa-edit" CommandName="EditComp" CommandArgument='<%#Eval("MCategoryId") %>'></asp:LinkButton>
                                                        <asp:LinkButton ID="lbldel" runat="server" CssClass="fa fa-trash" CommandName="DeleteComp" OnClientClick="return confirm('Are you sure you want to delete this Company?');" CommandArgument='<%#Eval("MCategoryId") %>'></asp:LinkButton>
                                                        <asp:LinkButton ID="lbllogindetail" runat="server" Visible="False" CssClass=" fa fa-paper-plane" CommandName="SendLogin" CommandArgument='<%#Eval("MCategoryId") %>'></asp:LinkButton>
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

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


