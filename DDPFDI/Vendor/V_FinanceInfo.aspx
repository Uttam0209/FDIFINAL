<%@ Page Language="C#" AutoEventWireup="true" CodeFile="V_FinanceInfo.aspx.cs" Inherits="Vendor_V_FinanceInfo" MasterPageFile="~/Vendor/VendorMaster.master" %>

<asp:Content ID="ConHead" runat="server" ContentPlaceHolderID="head"></asp:Content>
<asp:Content ID="Innercontent" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <asp:ScriptManager ID="sc" runat="server"></asp:ScriptManager>
    <div class="content oem-content">
        <div class="sideBg">
            <div class="row">
                <div class="col-md-12 padding_0">
                    <div id="divHeadPage" runat="server"></div>
                </div>
            </div>
            <div class="container">
                <div class="cacade-forms">
                    <div class="clearfix mt10"></div>
                    <div id="test" class="tab-pane">
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:Panel ID="panstep4" runat="server">
                                    <p>
                                        TURN OVER DURING LAST 3 YEARS
                                    </p>
                                    <asp:GridView runat="server" ID="gvTURNOVERDURINGLAST3YEARS" CssClass="table table-hover" ShowFooter="true"
                                        AutoGenerateColumns="false" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowCreated="gvTURNOVERDURINGLAST3YEARS_RowCreated">
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:BoundField DataField="SNo" HeaderText="Raw Number" />
                                            <asp:TemplateField HeaderText="Financial Year">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtfinyear" runat="server" CssClass="form-control"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Value of Current Assets">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtcurrentasset" runat="server" CssClass="form-control"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Value of Current Liabilites">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtcurrentlibilites" runat="server" CssClass="form-control"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total Profit/Loss">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtprofitloss" runat="server" CssClass="form-control"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Upload Audited Balance account sheet">
                                                <ItemTemplate>
                                                    <asp:FileUpload ID="fufileprofitloss" runat="server" CssClass="form-control" />
                                                    <asp:HiddenField ID="hfprofitloss" runat="server" Value="" />
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <FooterTemplate>
                                                    <asp:Button ID="btnturnover" runat="server" Text="Add New Row"
                                                        CssClass="btn btn-primary pull-right" OnClick="btnturnover_Click" />
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbturnover" runat="server" CssClass="fa fa-times"
                                                        OnClick="lbturnover_Click"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EditRowStyle BackColor="#2461BF" />
                                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="#EFF3FB" />
                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                        <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                        <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                        <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                    </asp:GridView>
                                    <asp:GridView runat="server" ID="gvturnoveredit" CssClass="table table-hover" AutoGenerateColumns="false"
                                        ShowFooter="true"
                                        CellPadding="4" ForeColor="#333333" GridLines="None">
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sr.No">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="FinancialYear" HeaderText="Financial Year" />
                                            <asp:BoundField DataField="Value_of_Current_Assets" HeaderText="Value of Current Assets" />
                                            <asp:BoundField DataField="Value_of_Current_Liabilites" HeaderText="Value of Current Liabilites" />
                                            <asp:BoundField DataField="Total_Profit_Loss" HeaderText="Total Profit/Loss" />
                                            <asp:BoundField DataField="File_Audited_Balance_account_sheet" HeaderText="Upload Audited Balance account sheet" />
                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lblsave" runat="server" CssClass="fa fa-save" CommandName="newsave" CommandArgument='<%#Eval("MasterId") %>'></asp:LinkButton>
                                                    <asp:LinkButton ID="lblupdate" runat="server" CssClass="fa fa-edit" CommandName="newedit" CommandArgument='<%#Eval("MasterId") %>'></asp:LinkButton>
                                                    <asp:LinkButton ID="lbldelete" runat="server" CssClass="fa fa-trash" CommandName="newdel" CommandArgument='<%#Eval("MasterId") %>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EditRowStyle BackColor="#2461BF" />
                                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="#EFF3FB" />
                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                        <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                        <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                        <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                    </asp:GridView>
                                    <div class="clearfix mt10"></div>
                                    <hr />
                                    <h3>Bank Details</h3>
                                    <div class="clearfix mt10"></div>

                                    <asp:GridView runat="server" ID="gvaccount" CssClass="table table-hover" ShowFooter="true"
                                        AutoGenerateColumns="false" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowCreated="gvaccount_RowCreated">
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:BoundField DataField="SNo" HeaderText="Raw Number" />
                                            <asp:TemplateField HeaderText="Name of Bank">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtnameofbank" runat="server" CssClass="form-control"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Type of Account">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="ddltypeofaccount" runat="server" CssClass="form-control">
                                                        <asp:ListItem>Select</asp:ListItem>
                                                        <asp:ListItem Value="1">Saving Account</asp:ListItem>
                                                        <asp:ListItem Value="2">Current Account</asp:ListItem>
                                                        <asp:ListItem Value="3">Over Draft Account</asp:ListItem>
                                                        <asp:ListItem Value="4">Any Other Account</asp:ListItem>
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Account No">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtaccountno" runat="server" CssClass="form-control"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="MICR Code">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtmicrcode" runat="server" CssClass="form-control"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="IFSC Code">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtifsccode" runat="server" CssClass="form-control"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Copy of Valid Bank Solvency Certificate">
                                                <ItemTemplate>
                                                    <asp:FileUpload ID="fusolvencycertificate" runat="server" CssClass="form-control" />
                                                    <asp:HiddenField ID="hffusolvencycertificate" runat="server" Value="" />
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <FooterTemplate>
                                                    <asp:Button ID="lbNewAccount" runat="server" Text="Add New Row"
                                                        CssClass="btn btn-primary pull-right" OnClick="lbNewAccount_Click" />
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbNewAccountxx" runat="server" CssClass="fa fa-times"
                                                        OnClick="lbNewAccountxx_Click"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EditRowStyle BackColor="#2461BF" />
                                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="#EFF3FB" />
                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                        <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                        <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                        <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                    </asp:GridView>
                                    <asp:GridView runat="server" ID="gvaccountedit" CssClass="table table-hover" AutoGenerateColumns="false"
                                        ShowFooter="true"
                                        CellPadding="4" ForeColor="#333333" GridLines="None">
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sr.No">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="NameofBank" HeaderText="Name of Bank" />
                                            <asp:BoundField DataField="TypeOfAccount" HeaderText="Type of Account" />
                                            <asp:BoundField DataField="AccountNo" HeaderText="Account No" />
                                            <asp:BoundField DataField="MICRNo" HeaderText="MICR Code" />
                                            <asp:BoundField DataField="IFSCCode" HeaderText="IFSC Code" />
                                            <asp:BoundField DataField="File_Bank_Solvency_Certificate" HeaderText="Copy of Valid Bank Solvency Certificate" />
                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lblsave" runat="server" CssClass="fa fa-save" CommandName="newsave" CommandArgument='<%#Eval("MasterId") %>'></asp:LinkButton>
                                                    <asp:LinkButton ID="lblupdate" runat="server" CssClass="fa fa-edit" CommandName="newedit" CommandArgument='<%#Eval("MasterId") %>'></asp:LinkButton>
                                                    <asp:LinkButton ID="lbldelete" runat="server" CssClass="fa fa-trash" CommandName="newdel" CommandArgument='<%#Eval("MasterId") %>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EditRowStyle BackColor="#2461BF" />
                                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="#EFF3FB" />
                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                        <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                        <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                        <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                    </asp:GridView>
                                    <div class="clearfix mt10"></div>
                                    <div runat="server" id="m" visible="false">
                                        <p>Has the firm been debarred from Govt. Contracts</p>
                                        <asp:GridView runat="server" ID="gvtype" CssClass="table table-hover" ShowFooter="true"
                                            AutoGenerateColumns="false" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowCreated="gvTURNOVERDURINGLAST3YEARS_RowCreated">
                                            <AlternatingRowStyle BackColor="White" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Select">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chk" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Contract Type">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbltype" runat="server" Text='<%#Eval("Type") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Value of Current Assets">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtvalidupto" runat="server" Type="date" CssClass="form-control"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <EditRowStyle BackColor="#2461BF" />
                                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                            <RowStyle BackColor="#EFF3FB" />
                                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                            <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                            <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                            <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                        </asp:GridView>
                                    </div>
                                    <div class="clearfix mt10"></div>
                                    <asp:Button ID="btnsubmit" runat="server" Text="Submit" CssClass="btn btn-primary pull-right mr10" OnClick="btnsubmit_Click" />

                                </asp:Panel>
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="btnsubmit" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
