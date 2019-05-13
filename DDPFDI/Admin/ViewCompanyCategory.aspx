<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewCompanyCategory.aspx.cs" Inherits="Admin_ViewCompanyCategory" MasterPageFile="~/Admin/MasterPage.master" %>

<asp:Content ID="head" runat="server" ContentPlaceHolderID="head">
</asp:Content>
<asp:Content ID="inner" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <asp:ScriptManager ID="sc" runat="server"></asp:ScriptManager>
    <asp:HiddenField runat="server" ID="hidType" />
    <asp:HiddenField runat="server" ID="hfcomprefno" />
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
                                    <asp:UpdatePanel runat="server" ID="updrop">
                                        <ContentTemplate>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>Select Company</label>
                                                    <asp:DropDownList runat="server" ID="ddlcompany" class="form-control form-cascade-control" AutoPostBack="True" OnSelectedIndexChanged="ddlcompany_OnSelectedIndexChanged"></asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-4" runat="server" id="lblselectdivison">
                                                <div class="form-group">
                                                    <label>Select Division/Palnt</label>
                                                    <asp:DropDownList runat="server" ID="ddldivision" class="form-control form-cascade-control" AutoPostBack="True" OnSelectedIndexChanged="ddldivision_OnSelectedIndexChanged"></asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-4" runat="server" id="lblselectunit">
                                                <div class="form-group">
                                                    <label>Select Unit</label>
                                                    <asp:DropDownList runat="server" ID="ddlunit" class="form-control form-cascade-control" AutoPostBack="True" OnSelectedIndexChanged="ddlunit_OnSelectedIndexChanged"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="table-wrapper">
                                    <div class="table-wraper">
                                        <asp:GridView ID="gvCategory" runat="server" Width="100%" Class="commonAjaxTbl viewCatDropDown master-company-table table display responsive no-wrap table-hover manage-user Grid" AutoGenerateColumns="false" AllowPaging="true"
                                            OnPageIndexChanging="OnPageIndexChanging" PageSize="25" AllowSorting="true" OnSorting="OnSorting" OnRowDataBound="OnRowDataBound" OnRowCommand="gvCategory_RowCommand">
                                            <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <div class="acordian-table">
                                                            <i class="toggle-table-minus fa fa-minus" aria-hidden="true" style="display: none"></i>
                                                            <i class="toggle-table-plus fa fa-plus" aria-hidden="true"></i>
                                                            <asp:Panel ID="pnlOrders" runat="server" Style="display: none">
                                                                <asp:GridView ID="gvsubcatlevel1" runat="server" AutoGenerateColumns="false" Class="table table-hover ChildGrid" OnRowDataBound="gvsubcatlevel1_OnRowCommand" OnRowCommand="gvsubcatlevel1_RowCommand">
                                                                    <Columns>
                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <div class="acordian-table">
                                                                                    <i class="toggle-table-minus fa fa-minus" aria-hidden="true" style="display: none"></i>
                                                                                    <i class="toggle-table-plus fa fa-plus" aria-hidden="true"></i>
                                                                                    <asp:Panel ID="pnlunit" runat="server" Style="display: none">
                                                                                        <asp:GridView ID="gvsubcatlevel2" runat="server" AutoGenerateColumns="false" Class="table table-hover ChildGrid" OnRowDataBound="gvsubcatlevel2_OnRowCommand" OnRowCommand="gvsublevel2_RowCommand">
                                                                                            <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
                                                                                            <Columns>
                                                                                                <asp:TemplateField>
                                                                                                    <ItemTemplate>
                                                                                                        <div class="acordian-table">
                                                                                                            <i class="toggle-table-minus fa fa-minus" aria-hidden="true" style="display: none"></i>
                                                                                                            <i class="toggle-table-plus fa fa-plus" aria-hidden="true"></i>
                                                                                                            <asp:Panel ID="pnlmy3level" runat="server" Style="display: none">
                                                                                                                <asp:GridView ID="gvlevel3" runat="server" AutoGenerateColumns="false" Class="table table-hover ChildGrid" OnRowCommand="gvlevel3_RowCommand">
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
                                                                                                                        <asp:TemplateField HeaderText="Level 3">
                                                                                                                            <ItemTemplate>
                                                                                                                                <asp:Label ID="lbllevel3subcatname" runat="server" Text='<%#Eval("SCategoryName") %>' NullDisplayText="#" SortExpression="SCategoryName"></asp:Label>
                                                                                                                                <asp:HiddenField runat="server" ID="hflevel3" Value='<%#Eval("SCategoryId") %>' />
                                                                                                                            </ItemTemplate>
                                                                                                                        </asp:TemplateField>
                                                                                                                        <asp:TemplateField>
                                                                                                                            <ItemTemplate>
                                                                                                                                <%--<asp:LinkButton ID="lbleditlevel2values" runat="server" CssClass="fa fa-edit" CommandName="level2edit" CommandArgument='<%#Eval("SCategoryId") %>'></asp:LinkButton>--%>
                                                                                                                                <%--<asp:LinkButton ID="lbldellevel3values" runat="server" CssClass="fa fa-trash" CommandName="level3delete" OnClientClick="return confirm('Are you sure you want to delete this level 3 values?');" CommandArgument='<%#Eval("SCategoryId") %>'></asp:LinkButton>--%>
                                                                                                                            </ItemTemplate>
                                                                                                                        </asp:TemplateField>
                                                                                                                    </Columns>
                                                                                                                </asp:GridView>
                                                                                                            </asp:Panel>
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
                                                                                                <asp:TemplateField>
                                                                                                    <ItemTemplate>
                                                                                                        <%--<asp:LinkButton ID="lbleditlevel2values" runat="server" CssClass="fa fa-edit" CommandName="level2edit" CommandArgument='<%#Eval("SCategoryId") %>'></asp:LinkButton>--%>
                                                                                                        <%--<asp:LinkButton ID="lbldellevel2values" runat="server" CssClass="fa fa-trash" CommandName="level2delete" OnClientClick="return confirm('Are you sure you want to delete this level 2 values?');" CommandArgument='<%#Eval("SCategoryId") %>'></asp:LinkButton>--%>
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
                                                                                <%#Container.DataItemIndex+1 %>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Level 1">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblfactoryrefno" Text='<%#Eval("SCategoryName") %>'></asp:Label>
                                                                                <asp:HiddenField runat="server" ID="hfcatlevel1id" Value='<%#Eval("SCategoryID") %>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <%--<asp:LinkButton ID="lbllevel1edit" runat="server" CssClass="fa fa-edit" CommandName="level1edit" CommandArgument='<%#Eval("SCategoryId") %>'></asp:LinkButton>--%>
                                                                                <asp:LinkButton ID="lbllevel1del" runat="server" CssClass="fa fa-trash" CommandName="level2del" OnClientClick="return confirm('Are you sure you want to delete this level 1 values?');" CommandArgument='<%#Eval("SCategoryId") %>'></asp:LinkButton>
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
                                                <asp:TemplateField HeaderText="Dropdown Label">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblrefno" runat="server" Text='<%#Eval("MCategoryName") %>' NullDisplayText="#" SortExpression="CompanyRefNo"></asp:Label>
                                                        <asp:HiddenField ID="hfcat" runat="server" Value='<%#Eval("MCategoryId") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                        <%--<asp:LinkButton ID="lblactive" runat="server" CssClass="fa fa-check" CommandName="labelactive" OnClientClick="return confirm('Are you sure you want to active this Lable?');" CommandArgument='<%#Eval("MCategoryId") %>'></asp:LinkButton>--%>
                                                        <%--<asp:LinkButton ID="lbllabeldel" runat="server" CssClass="fa fa-trash" CommandName="labeldel" OnClientClick="return confirm('Are you sure you want to delete this Lable?');" CommandArgument='<%#Eval("MCategoryId") %>'></asp:LinkButton>--%>
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


