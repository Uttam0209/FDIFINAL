<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StatusOnDate.aspx.cs" Inherits="User_StatusOnDate" MasterPageFile="~/User/MasterPage.master" %>
<asp:Content ID="content1" ContentPlaceHolderID="head" runat="server">
    <style>
        #ContentPlaceHolder1_gvstatus th{
            background: #507CD1!important;
        }
        #ContentPlaceHolder1_gvyrstatus th
        {
            background: #507CD1!important;
        }
    </style>
</asp:Content>
<asp:Content ID="content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container py-4">
        <p>Status on date</p>
        <div class="table table-responsive">
            <asp:GridView runat="server" ID="gvstatus" class="table table-hover table-bordered table-striped" OnRowCommand="gvstatus_RowCommand" AutoGenerateColumns="false">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <%#Container.DataItemIndex+1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="TargetYear" HeaderText="Company" />
                    <asp:TemplateField HeaderText="Target">
                        <ItemTemplate>
                            <asp:TextBox runat="server" class="form-control" Text='<%#Eval("Target") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="ItematPortal" HeaderText="Item at Portal" />
                    <asp:BoundField DataField="Eois" HeaderText="EoIs" />
                    <asp:BoundField DataField="SoPlaced" HeaderText="SO Placed" />
                    <asp:BoundField DataField="Indinized" HeaderText="Indigenized" />
                    <asp:BoundField DataField="TotalProd" HeaderText="Total" />
                    <asp:TemplateField HeaderText="Action">
                        <ItemTemplate>
                            <asp:LinkButton runat="server" ID="lbupdatestatus" CssClass="fa fa-edit" CommandName="action" CommandArgument='<%#Eval("Target") %>'></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
        <div class="clearfix mt-1"></div>
        <div class="table table-responsive">
            <asp:GridView runat="server" ID="gvyrstatus" class="table table-hover table-bordered table-striped" AutoGenerateColumns="false">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <%#Container.DataItemIndex+1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="CompName" HeaderText="Company" />
                    <asp:BoundField DataField="Total2021" HeaderText="2020-21" />
                    <asp:BoundField DataField="Total2122" HeaderText="2021-22" />
                    <asp:BoundField DataField="Total2223" HeaderText="2022-23" />
                    <asp:BoundField DataField="Total2324" HeaderText="2023-24" />
                    <asp:BoundField DataField="Total2425" HeaderText="2024-25" />
                    <asp:BoundField DataField="TotalProd" HeaderText="Total" />
                </Columns>
            </asp:GridView>
        </div>


    </div>
</asp:Content>

