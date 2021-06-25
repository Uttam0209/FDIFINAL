<%@ Page Language="C#" AutoEventWireup="true" CodeFile="V_FinanceInfo.aspx.cs" Inherits="Vendor_V_FinanceInfo" MasterPageFile="~/Vendor/VendorMaster.master" %>

<%@ Register Src="~/Vendor/InnerMenu.ascx" TagName="Head" TagPrefix="Menu" %>
<asp:Content runat="server" ID="msthead" ContentPlaceHolderID="head">
</asp:Content>
<asp:Content runat="server" ID="Content" ContentPlaceHolderID="ContentPlaceHolder1">
    <asp:UpdatePanel runat="server" ID="up" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:ScriptManager runat="server" ID="sc"></asp:ScriptManager>
            <div class="container-fluid">
                <div class="card-body">
                    <Menu:Head ID="inner" runat="server" />
                    <div class="tab-content">
                        <div class="row">
                            <div class="col-12">
                                <div class="page-title-box">
                                    <h4 class="page-title">Company Name :
                        <asp:Label runat="server" ID="lbcomp"></asp:Label>
                                    </h4>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="card card-h-100">
                                    <div class="card-body">
                                        <h3 class="mb-2">TURN OVER
                                        </h3>
                                        <h4 class="header-title mb-1">DURING LAST 3 YEARS
                                        </h4>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <asp:GridView runat="server" ID="gvTURNOVERDURINGLAST3YEARS"
                                                    CssClass="table table-bordered table-centered mb-0" ShowFooter="true"
                                                    AutoGenerateColumns="false" OnRowCreated="gvTURNOVERDURINGLAST3YEARS_RowCreated">
                                                    <Columns>
                                                        <asp:BoundField DataField="SNo" HeaderText="SR.NO" />
                                                        <asp:TemplateField HeaderText="Financial Year">
                                                            <ItemTemplate>
                                                                <div class="input-append date" id="datePicker" data-date="12-02-2012" data-date-format="yyyy" style="margin-top: -15px;">
                                                                    <span class="add-on"><i class="icon-th"></i></span>
                                                                    <asp:TextBox ID="txtfinyear" runat="server" CssClass="form-control datePicker" data-date-format="yyyy"></asp:TextBox>
                                                                </div>
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
                                                                <asp:LinkButton ID="lbturnover" runat="server" CssClass="btn btn-sm btn-danger"
                                                                    OnClick="lbturnover_Click"><i class="fa fa-times"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                                <asp:GridView runat="server" ID="gvturnoveredit" CssClass="table table-bordered table-centered mb-0"
                                                    AutoGenerateColumns="false" ShowFooter="true" OnRowCommand="gvturnoveredit_RowCommand">
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
                                                        <asp:TemplateField HeaderText="Upload Audited Balance account sheet">
                                                            <ItemTemplate>
                                                                <a href='<%#Eval("File_Audited_Balance_account_sheet","https://srijandefence.gov.in/Upload/VendorImage/{0}") %>' runat="server" id="img" target="_blank"><%#Eval("File_Audited_Balance_account_sheet") %></a>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Action">
                                                            <ItemTemplate>
                                                                <asp:HiddenField runat="server" ID="hfturnedit" Value='<%#Eval("FinancialInfoId") %>' />
                                                                <asp:LinkButton ID="lblsave" runat="server" CssClass="btn btn-sm btn-success" CommandName="newsave"
                                                                    CommandArgument='<%#Eval("FinancialInfoId") %>'><i class="fa fa-save"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="lblupdate" runat="server" CssClass="btn btn-sm btn-warning" CommandName="newedit"
                                                                    CommandArgument='<%#((GridViewRow) Container).RowIndex %>'><i class="fa fa-edit"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="lbldelete" runat="server" CssClass="btn btn-sm btn-danger"
                                                                    CommandName="newdel" CommandArgument='<%#Eval("FinancialInfoId") %>'><i class="fa fa-trash"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                        <h3 class="mb-2">Bank Details
                                        </h3>
                                        <h4 class="header-title mb-1">Please enter Bank Details
                                        </h4>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <asp:GridView runat="server" ID="gvaccount" CssClass="table table-bordered table-centered mb-0" ShowFooter="true"
                                                    AutoGenerateColumns="false" OnRowCreated="gvaccount_RowCreated">
                                                    <Columns>
                                                        <asp:BoundField DataField="SNo" HeaderText="SR.NO" />
                                                        <asp:TemplateField HeaderText="Name of Bank">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtnameofbank" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Type of Account">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddltypeofaccount" runat="server" CssClass="form-control">
                                                                    <asp:ListItem>Select</asp:ListItem>
                                                                    <asp:ListItem Value="Saving Account">Saving Account</asp:ListItem>
                                                                    <asp:ListItem Value="Current Account">Current Account</asp:ListItem>
                                                                    <asp:ListItem Value="Over Draft Account">Over Draft Account</asp:ListItem>
                                                                    <asp:ListItem Value="Any Other Account">Any Other Account</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Account No">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtaccountno" runat="server" CssClass="form-control" MaxLength="20"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="MICR Code">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtmicrcode" runat="server" CssClass="form-control" MaxLength="15"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="IFSC Code">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtifsccode" runat="server" CssClass="form-control" MaxLength="15"></asp:TextBox>
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
                                                                <asp:LinkButton ID="lbNewAccountxx" runat="server" CssClass="btn btn-sm  btn-danger"
                                                                    OnClick="lbNewAccountxx_Click"><i class="fa fa-times"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                                <asp:GridView runat="server" ID="gvaccountedit" CssClass="table table-bordered table-centered mb-0" AutoGenerateColumns="false"
                                                    ShowFooter="true" OnRowCommand="gvaccountedit_RowCommand">
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
                                                        <asp:TemplateField HeaderText="Copy of Valid Bank Solvency Certificate">
                                                            <ItemTemplate>
                                                                <a href='<%#Eval("File_Bank_Solvency_Certificate","https://srijandefence.gov.in/Upload/VendorImage/{0}") %>' runat="server" id="img1" target="_blank"><%#Eval("File_Bank_Solvency_Certificate") %></a>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Action">
                                                            <ItemTemplate>
                                                                <asp:HiddenField runat="server" ID="hfaccountedit" Value='<%#Eval("FinancialAccountId") %>' />
                                                                <asp:LinkButton ID="lblsave" runat="server" CssClass="btn btn-sm btn-success" CommandName="newsave" CommandArgument='<%#Eval("FinancialAccountId") %>'><i class="fa fa-save"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="lblupdate" runat="server" CssClass="btn btn-sm btn-warning" CommandName="newedit" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'><i class="fa fa-edit"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="lbldelete" runat="server" CssClass="btn btn-sm btn-danger" CommandName="newdel" CommandArgument='<%#Eval("FinancialAccountId") %>'><i class="fa fa-trash"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                        <div class="col-md-12 text-right mt-3">
                                            <asp:LinkButton ID="btnPrev" runat="server" CssClass="btn btn-secondary"
                                                OnClick="btnPrev_Click"><i class="fa fa-backward"></i>&nbsp;Previous </asp:LinkButton>
                                            <asp:LinkButton ID="btnsubmit" runat="server" CssClass="btn btn-success"
                                                OnClick="btnsubmit_Click"><i class="fa fa-save"></i>&nbsp;Save</asp:LinkButton>
                                            <asp:LinkButton ID="btnNext" runat="server"
                                                CssClass="btn btn-primary" OnClick="btnNext_Click">Next&nbsp;<i class="fa fa-forward"></i></asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal fade" id="modelmsg" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title"><i class="fa fa-bell "></i>&nbsp;Alert</h5>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div class="modal-body">
                            <asp:UpdatePanel runat="server" ID="uep2" ChildrenAsTriggers="true">
                                <ContentTemplate>
                                    <h4>
                                        <asp:Label runat="server" ID="lblmsg" ForeColor="Red"></asp:Label></h4>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal fade" id="divturnover" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title"><i class="fa fa-info"></i>&nbsp;&nbsp;Turn Over During Last 3 years</h5>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <asp:UpdatePanel ID="UpdatePanel10" runat="server" ChildrenAsTriggers="true">
                            <ContentTemplate>
                                <div class="modal-body">
                                    <div class="form-group" style="margin: 0">
                                        <label for="uname" class=" tetLable">
                                            Financial Year
                                        </label>
                                        <asp:TextBox runat="server" ID="txtfinancialyear" placeholder="Financial Year" Class="form-control"></asp:TextBox>
                                    </div>
                                    <div class="form-group" style="margin: 0">
                                        <label for="uname" class=" tetLable">
                                            Value of Current Assets
                                        </label>
                                        <asp:TextBox runat="server" ID="txtcurrasst" placeholder=" Value of Current Assets" Class="form-control"></asp:TextBox>
                                    </div>
                                    <div class="form-group" style="margin: 0">
                                        <label for="uname" class=" tetLable">
                                            Value of Current Liabilites
                                        </label>
                                        <asp:TextBox runat="server" ID="txtcurrliablities" placeholder=" Value of Current Liabilites" Class="form-control"></asp:TextBox>
                                    </div>
                                    <div class="form-group" style="margin: 0">
                                        <label for="uname" class=" tetLable">
                                            Total Profit/Loss
                                        </label>
                                        <asp:TextBox runat="server" ID="txtprofitloss" placeholder="Total Profit/Loss" Class="form-control"></asp:TextBox>
                                    </div>
                                    <div class="form-group" style="margin: 0">
                                        <label for="uname" class=" tetLable">
                                            Upload Audited Balance account sheet
                                        </label>
                                        <asp:HiddenField ID="hffileaudit" runat="server" />
                                        <asp:FileUpload runat="server" ID="fufileaudit" Class="form-control" />
                                    </div>
                                    <div class="clearfix mt10"></div>
                                    <div class="col-md-12">
                                        <div class="text-right mt-2">
                                            <asp:LinkButton ID="lbsub" runat="server" Text="Edit & Update" CssClass="btn btn-primary" OnClick="lbsub_Click"></asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="clearfix mt10"></div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
            <div class="modal fade" id="divbank" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title"><i class="fa fa-info"></i>&nbsp;&nbsp;Bank Details</h5>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div class="modal-body">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" ChildrenAsTriggers="true">
                                <ContentTemplate>
                                    <div class="form-group" style="margin: 0">
                                        <label for="uname" class=" tetLable">
                                            Name of Bank
                                        </label>
                                        <asp:TextBox runat="server" ID="txtnameofbank" placeholder="Name of Bank" Class="form-control"></asp:TextBox>
                                    </div>
                                    <div class="form-group" style="margin: 0">
                                        <label for="uname" class=" tetLable">
                                            Type of Account
                                        </label>
                                        <asp:DropDownList runat="server" ID="txttypeofaccount" Class="form-control">
                                            <asp:ListItem>Select</asp:ListItem>
                                            <asp:ListItem Value="Saving Account">Saving Account</asp:ListItem>
                                            <asp:ListItem Value="Current Account">Current Account</asp:ListItem>
                                            <asp:ListItem Value="Over Draft Account">Over Draft Account</asp:ListItem>
                                            <asp:ListItem Value="Any Other Account">Any Other Account</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group" style="margin: 0">
                                        <label for="uname" class=" tetLable">
                                            Account No
                                        </label>
                                        <asp:TextBox ID="txtaccountno" runat="server" palceholder="Account No" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="form-group" style="margin: 0">
                                        <label for="uname" class=" tetLable">
                                            MICR Code
                                        </label>
                                        <asp:TextBox ID="txtmicrcode" runat="server" palceholder="MICR Code" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="form-group" style="margin: 0">
                                        <label for="uname" class=" tetLable">
                                            IFSC Code
                                        </label>
                                        <asp:TextBox ID="txtifsc" runat="server" palceholder=" IFSC Code" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="form-group" style="margin: 0">
                                        <label for="uname" class=" tetLable">
                                            Copy of Valid Bank Solvency Certificate	
                                        </label>
                                        <asp:HiddenField ID="hfsolencycertificate" runat="server" />
                                        <asp:FileUpload runat="server" ID="fusolvencycerti" Class="form-control" />
                                    </div>
                                    <div class="clearfix mt10"></div>
                                    <div class="col-md-12">
                                        <div class="text-right mt-2">
                                            <asp:LinkButton ID="lblsub2" runat="server" Text="Edit & Update" CssClass="btn btn-primary" OnClick="lblsub2_Click"></asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="clearfix mt10"></div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnsubmit" />
        </Triggers>
    </asp:UpdatePanel>

    <script type="text/javascript">
        function showPopup() {
            $('#divturnover').modal('show', function () {
            });
        }
    </script>
    <script type="text/javascript">
        function showPopup1() {
            $('#divbank').modal('show', function () {
            });
        }
    </script>
    <script type="text/javascript">
        function showPopup2() { $('#modelmsg').modal('show', function () { }); }
    </script>
    <script type="text/javascript">
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode;
            if (charCode != 46 && charCode > 31
                && (charCode < 48 || charCode > 57))
                return false;
            return true;
        }
    </script>
</asp:Content>
