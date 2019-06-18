<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Test.aspx.cs" Inherits="Admin_Test" MasterPageFile="~/Admin/MasterPage.master" %>

<asp:Content ID="headDesignation" runat="server" ContentPlaceHolderID="head">
</asp:Content>
<asp:Content ID="InnerDesignation" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div class="content oem-content">
        <asp:ScriptManager ID="sn" runat="server"></asp:ScriptManager>
        <asp:HiddenField runat="server" ID="hidType" />
        <asp:HiddenField runat="server" ID="hfcomprefno" />
        <div class="sideBg">
            <div class="row">
                <div class="col-md-12 padding_0">
                    <div id="divHeadPage" runat="server"></div>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-12">
                  <%--  <asp:UpdatePanel runat="server" ID="updrop">
                        <ContentTemplate>--%>
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" OnRowDataBound="OnRowDataBound"
                                DataKeyNames="ProdInfoId1" OnRowEditing="OnRowEditing" OnRowCancelingEdit="OnRowCancelingEdit" PageSize="5" AllowPaging="true" OnPageIndexChanging="OnPaging"
                                OnRowUpdating="OnRowUpdating" OnRowDeleting="OnRowDeleting" EmptyDataText="No records has been added."
                                Width="450">
                                <Columns>
                                    <asp:TemplateField HeaderText="Name" ItemStyle-Width="150">
                                        <ItemTemplate>
                                            <asp:Label ID="lblName" runat="server" Text='<%# Eval("NameofSpec") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtName" runat="server" Text='<%# Eval("NameofSpec") %>' Width="140"></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Value" ItemStyle-Width="150">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCountry" runat="server" Text='<%# Eval("Value") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtCountry" runat="server" Text='<%# Eval("Value") %>' Width="140"></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unit" ItemStyle-Width="150">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCountry1" runat="server" Text='<%# Eval("Unit") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtCountry1" runat="server" Text='<%# Eval("Unit") %>' Width="140"></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:CommandField ButtonType="Link" ShowEditButton="true" ShowDeleteButton="true"
                                        ItemStyle-Width="150" />
                                </Columns>
                            </asp:GridView>
                            <table border="1" cellpadding="0" cellspacing="0" style="border-collapse: collapse">
                                <tr>
                                    <td style="width: 150px">Name:<br />
                                        <asp:TextBox ID="txtName" runat="server" Width="140" />
                                    </td>
                                    <td style="width: 150px">Value:<br />
                                        <asp:TextBox ID="txtCountry" runat="server" Width="140" />
                                    </td>
                                    <td style="width: 150px">Unit:<br />
                                        <asp:TextBox ID="txtUnit" runat="server" Width="140" />
                                    </td>
                                    <td style="width: 150px">
                                        <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="Insert" />
                                    </td>
                                </tr>
                            </table>
                        <%--</ContentTemplate>
                    </asp:UpdatePanel>--%>
                </div>
            </div>
            <div class="clearfix"></div>
            <div class="footer"><i class="far fa-copyright"></i>2019 <a href="#">Department of Defence Production</a> </div>
        </div>
    </div>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript" src="scripts/jquery.blockUI.js"></script>
    <script type="text/javascript">
        $(function () {
            BlockUI("dvGrid");
            $.blockUI.defaults.css = {};
        });
        function BlockUI(elementID) {
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_beginRequest(function () {
                $("#" + elementID).block({
                    message: '<div align = "center">' + '<img src="images/loadingAnim.gif"/></div>',
                    css: {},
                    overlayCSS: { backgroundColor: '#000000', opacity: 0.6, border: '3px solid #63B2EB' }
                });
            });
            prm.add_endRequest(function () {
                $("#" + elementID).unblock();
            });
        };
    </script>
</asp:Content>
