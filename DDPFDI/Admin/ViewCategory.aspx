<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewCategory.aspx.cs" Inherits="Admin_ViewCategory" MasterPageFile="~/Admin/MasterPage.master" %>

<asp:Content ID="head" runat="server" ContentPlaceHolderID="head">
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
                                <div class="row">
                                    <%--<div class="col-sm-4">
                                        <label>Select Company</label>
                                        <asp:DropDownList runat="server" ID="ddlcompany" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlcompany_OnSelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                    <div class="col-sm-4">
                                        <label runat="server" id="lblselectdivison" visible="False">Select Division/Plant</label>
                                        <asp:DropDownList runat="server" ID="ddldivision" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddldivision_OnSelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                    <div class="col-sm-4">
                                        <label runat="server" id="lblselectunit" visible="False">Select Unit</label>
                                        <asp:DropDownList runat="server" ID="ddlunit" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlunit_OnSelectedIndexChanged"></asp:DropDownList>
                                    </div>--%>
                                </div>
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
                                                                <asp:GridView ID="gvsubcatlevel1" runat="server" AutoGenerateColumns="false" Class="table table-hover ChildGrid" OnRowDataBound="gvsubcatlevel1_OnRowCommand">
                                                                    <Columns>
                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <div class="acordian-table">
                                                                                    <i class="toggle-table-minus fa fa-minus" aria-hidden="true" style="display: none"></i>
                                                                                    <i class="toggle-table-plus fa fa-plus" aria-hidden="true"></i>
                                                                                    <asp:Panel ID="pnlunit" runat="server" Style="display: none">
                                                                                        <asp:GridView ID="gvsubcatlevel2" runat="server" AutoGenerateColumns="false" Class="table table-hover ChildGrid">
                                                                                            <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
                                                                                            <Columns>
                                                                                                <asp:TemplateField>
                                                                                                    <ItemTemplate>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField>
                                                                                                    <ItemTemplate>
                                                                                                        <%#Container.DataItemIndex+1 %>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="Level 2">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lblsubcatlevelinner2" runat="server" Text='<%#Eval("SCategoryName") %>' NullDisplayText="#" SortExpression="SCategoryName"></asp:Label>
                                                                                                        <asp:HiddenField runat="server" ID="hfcatlevel2" Value='<%#Eval("SCategoryId") %>' />
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                            </Columns>
                                                                                        </asp:GridView>
                                                                                    </asp:Panel>
                                                                                </div>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="S.No">
                                                                            <ItemTemplate>
                                                                                <%#Container.DataItemIndex+1 %>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Level1">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblfactoryrefno" Text='<%#Eval("SCategoryName") %>'></asp:Label>
                                                                                <asp:HiddenField runat="server" ID="hfcatlevel1id" Value='<%#Eval("SCategoryID") %>' />
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
                                                <asp:TemplateField HeaderText="Main Category">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblrefno" runat="server" Text='<%#Eval("MCategoryName") %>' NullDisplayText="#" SortExpression="CompanyRefNo"></asp:Label>
                                                        <asp:HiddenField ID="hfcat" runat="server" Value='<%#Eval("MCategoryId") %>' />
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


