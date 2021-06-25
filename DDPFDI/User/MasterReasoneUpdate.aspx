<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MasterReasoneUpdate.aspx.cs" Inherits="User_MasterReasoneUpdate" MasterPageFile="~/User/MasterPage.master" %>

<asp:Content ID="content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager runat="server" ID="sc"></asp:ScriptManager>
    <asp:UpdatePanel runat="server" ID="up" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="row d-flex justify-content-center">
                <div class="col-lg-11">
                    <div class="row d-flex">
                        <div class="col-sm-12">
                            <h5 style="text-align:center;">
                                <p runat="server" id="infotitle"></p>
                            </h5>
                            <div class="clearfix mt-1"></div>
                            <br />
                            <p style="float: right;">
                                Showing
                                    <asp:Label runat="server" ID="lbltotalshowpageitem"></asp:Label>
                                products of
                                <asp:Label ID="lbltotfilter" runat="server"></asp:Label>
                                products  
                            </p>
                            <div class="row">
                                <div class="col-sm-1">
                                    <label>Index</label>
                                    <asp:DropDownList ID="ddlsort" runat="server" Width="83px" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlsort_SelectedIndexChanged">
                                        <asp:ListItem>100</asp:ListItem>
                                        <asp:ListItem>200</asp:ListItem>
                                        <asp:ListItem>400</asp:ListItem>
                                        <asp:ListItem>500</asp:ListItem>
                                        <asp:ListItem>1000</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-sm-4">
                                    <label>Search</label>
                                    <asp:TextBox runat="server" ID="txtsearch" CssClass="form-control" Placehodler="Search (Productrefno,Company,division,unit)" OnTextChanged="txtsearch_TextChanged"></asp:TextBox>
                                </div>
                            </div>
                            <div class="clearfix mt-1"></div>
                            <br />
                            <div class="table table-responsive">
                                <asp:GridView runat="server" ID="gvEoi" AutoGenerateColumns="false" class="table table-hover table-dark" OnRowDataBound="gvEoi_RowDataBound" OnRowCommand="gvEoi_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sr.No.">
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="chkHeader" runat="server" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox runat="server" ID="chkRow" />&nbsp;&nbsp;
                                                <%#Eval("row_no") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="ProductrefNo" HeaderText="Item No" NullDisplayText="#" />
                                        <asp:BoundField DataField="ProductDescription" HeaderText="Item Name" NullDisplayText="#" />
                                        <asp:BoundField DataField="Value1718" HeaderText="2017-18" NullDisplayText="#" />
                                        <asp:BoundField DataField="Value1819" HeaderText="2018-19" NullDisplayText="#" />
                                        <asp:BoundField DataField="Value1920" HeaderText="2019-20" NullDisplayText="#" />
                                        <asp:BoundField DataField="Value2021" HeaderText="2020-21" NullDisplayText="#" />
                                        <asp:BoundField DataField="Value2122" HeaderText="2021-22" NullDisplayText="#" />
                                        <asp:BoundField DataField="IsShowGeneral" HeaderText="Is Display" NullDisplayText="#" />
                                        <asp:BoundField DataField="IsIndeginized" HeaderText="Is Indiginized" NullDisplayText="#" />
                                        <asp:BoundField DataField="Make2" HeaderText="Make-II" NullDisplayText="#" />
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <asp:LinkButton runat="server" ID="lbupdate" ForeColor="White" CommandArgument='<%#Eval("ProductRefNo") %>' CommandName="VEOI"><i class="fa fa-edit"></i>Update</asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <div class="clearfix mt-1"></div>
                            <!-----------------------------------------Code for pageindexing----------------------------------------------------->
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="row">
                                        <div class="col-sm-6">
                                            <asp:LinkButton ID="lnkbtnPgPrevious" runat="server" CssClass="btn btn-dark btn-sm"
                                                OnClick="lnkbtnPgPrevious_Click">Previous</asp:LinkButton>
                                        </div>
                                        <div class="col-sm-6">
                                            <asp:LinkButton ID="lnkbtnPgNext" runat="server" CssClass="btn  btn-dark btn-sm pull-right"
                                                OnClick="lnkbtnPgNext_Click">Next</asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="clearfix"></div>
                                    <div class="text-center">
                                        <asp:Label ID="lblpaging" runat="server" class="btn btn-dark text-center" Text=""></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <!-----------------------------------------end code for page indexing----------------------------------------------------->
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
