<%@ Page Title="" Language="C#" MasterPageFile="~/User/MasterPage.master" AutoEventWireup="true" CodeFile="TargetValue2021.aspx.cs" Inherits="User_TargetValue2021" %>

<asp:Content ID="content1" ContentPlaceHolderID="head" runat="server">
    <style>
        #ContentPlaceHolder1_gvstatus th {
            background: #507CD1 !important;
        }

        #ContentPlaceHolder1_gvyrstatus th {
            background: #507CD1 !important;
        }
    </style>
</asp:Content>
<asp:Content ID="content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container py-4">
        <p>More Details</p>
        <input id="btnprint" type="button" runat="server" onclick="PrintDiv()" style="width: 70px;" class="btn btn-primary pull-right" value="Print" />
        <div runat="server" id="Div15">
            <div class="table table-responsive">
                <asp:GridView runat="server" ID="gvstatus" class="table table-hover table-bordered table-striped" OnRowCommand="gvstatus_RowCommand"
                    OnRowEditing="gvstatus_RowEditing" OnRowUpdating="gvstatus_RowUpdating" OnRowDataBound="gvstatus_RowDataBound" AutoGenerateColumns="false">
                    <Columns>
                        <asp:TemplateField HeaderText="Sr.No.">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1 %>
                                &nbsp;<asp:CheckBox ID="chk" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="DPSUs/OFB/SHQ">
                            <ItemTemplate>
                                <asp:HiddenField runat="server" ID="hfproc" Value='<%# Eval("CompanyRefNo") %>' />
                                <asp:Label ID="lblComp" runat="server" Text='<%# Eval("CompName") %>'>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Total Items Uploaded">
                            <ItemTemplate>
                                <asp:Label ID="TotalProd" runat="server" Text='<%# Eval("TotalProd") %>'>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Target Indigenization 2020-21">
                            <ItemTemplate>
                                <asp:TextBox ID="texttarget" onkeypress="return isNumberKeyOutDecimal(event)" runat="server" class="form-control" Text='<%#Eval("Target") %>'></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="In-house Indigenized">
                            <ItemTemplate>
                                <asp:Label ID="lblinhouse" runat="server" Text='<%# Eval("Inhouseindigenization") %>'>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Make-II Indigenized">
                            <ItemTemplate>
                                <asp:Label ID="lblmakeii" runat="server" Text='<%# Eval("MakeiiIndigenized") %>'>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Other than MakeII Indigenized">
                            <ItemTemplate>
                                <asp:Label ID="lblotherthan" runat="server" Text='<%# Eval("OtherthanMakeii") %>'>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Total items indigenized(Success Story 2020-21)">
                            <ItemTemplate>
                                <asp:Label ID="Totalindigenized" runat="server" Text='<%# Eval("Totalindigenized") %>'>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Total annual value negated in Rs Lakh">
                            <ItemTemplate>
                                <asp:Label ID="lblannual" runat="server" Text='<%# Eval("Total2425") %>'>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                       <%-- <asp:TemplateField HeaderText="Action">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbupdatestatus" runat="server" CausesValidation="false" CommandArgument='<%#Eval("CompanyRefNo")%>'
                                    CommandName="action" Text="Save"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                    </Columns>
                </asp:GridView>
                <asp:GridView runat="server" ID="GridView1" class="table table-hover table-bordered table-striped" Style="margin-top: -17px;" OnRowCommand="GridView1_RowCommand"
                    OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" OnRowDataBound="GridView1_RowDataBound" AutoGenerateColumns="false" ShowFooter="true">
                    <Columns>
                        <asp:TemplateField HeaderStyle-Width="67px">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+11 %>
                                  &nbsp;<asp:CheckBox ID="chk1" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-Width="146px">
                            <ItemTemplate>
                                <asp:HiddenField runat="server" ID="hfproc1" Value='<%# Eval("CompanyRefNo") %>' />
                                <asp:Label ID="lblComp" runat="server" Text='<%# Eval("CompName") %>'>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-Width="98px">
                            <ItemTemplate>
                                <asp:Label ID="TotalProd" runat="server" Text='<%# Eval("TotalProd") %>'>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-Width="138px">
                            <ItemTemplate>
                                <asp:TextBox ID="texttarget1" onkeypress="return isNumberKeyOutDecimal(event)" runat="server" class="form-control" Text='<%#Eval("Target") %>'></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-Width="115px">
                            <ItemTemplate>
                                <asp:Label ID="lblinhouse" runat="server" Text='<%# Eval("Inhouseindigenization") %>'>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-Width="115px">
                            <ItemTemplate>
                                <asp:Label ID="lblmakeii" runat="server" Text='<%# Eval("MakeiiIndigenized") %>'>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-Width="125px">
                            <ItemTemplate>
                                <asp:Label ID="lblotherthan" runat="server" Text='<%# Eval("OtherthanMakeii") %>'>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-Width="196px">
                            <ItemTemplate>
                                <asp:Label ID="Totalindigenized" runat="server" Text='<%# Eval("Totalindigenized") %>'>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-Width="137px">
                            <ItemTemplate>
                                <asp:Label ID="lblannual" runat="server" Text='<%# Eval("Total2425") %>'>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                       <%-- <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="lbupdatestatus" runat="server" CausesValidation="false" CommandArgument='<%#Eval("CompanyRefNo")%>'
                                    CommandName="action" Text="Save"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                    </Columns>
                </asp:GridView>
            </div>
            <div class="clearfix"></div>
            <asp:Button ID="btnsave" runat="server"  class="btn btn-primary pull-right" Text="Save and Update" OnClick="btnsave_Click" />

        </div>
        <div class="clearfix mt-1"></div>
    </div>
    <script type="text/javascript">
        function Prints() {
            var divToPrint = document.getElementById('Div15');
            var popupWin = window.open('', '_blank', 'width=300,height=400,location=no,left=200px');
            popupWin.document.open();
            popupWin.document.write('<html><body onload="window.print()">' + divToPrint.innerHTML + '</html>');
            popupWin.document.close();
        }
    </script>
    <script type="text/javascript">
        function isNumberKeyOutDecimal(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode;
            if (charCode > 31
                && (charCode < 48 || charCode > 57))
                return false;
            return true;
        }
    </script>
</asp:Content>
