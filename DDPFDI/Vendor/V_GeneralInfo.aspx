<%@ Page Language="C#" AutoEventWireup="true" CodeFile="V_GeneralInfo.aspx.cs" MasterPageFile="~/Vendor/VendorMaster.master" Inherits="Vendor_V_GeneralInfo" %>

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
                                        <h4 class="header-title mb-3">Please provide all required details to register your business with us
                                        </h4>
                                        <div class="row">
                                            <div class="col-md-12 mb-3">
                                                <div class="row">
                                                    <label for="simpleinput" class="form-label mb-1">
                                                        Upload company Logo
                                                    </label>
                                                    <div class="col-sm-2">
                                                        <asp:Image ID="Image1" runat="server" CssClass="mdi-camera-image uil-image-resize-square" Height="100" Width="100" />
                                                    </div>
                                                    <div class="col-sm-8">
                                                        <asp:FileUpload ID="FileUpload1" TabIndex="1" runat="server" CssClass="form-control" />
                                                    </div>
                                                    <div class="col-sm-2">
                                                        <asp:LinkButton ID="btnUploadLogo" runat="server" TabIndex="2" Text="Upload" CssClass="btn btn-primary btn-block"
                                                            OnClick="btnUploadLogo_Click"></asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-6 mb-3">
                                                <label for="simpleinput" class="form-label mb-1">
                                                    REGISTRATION CATEGORY <span class="text-danger">*</span>
                                                </label>
                                                <asp:TextBox ID="ddlregiscategory" TabIndex="3" runat="server" CssClass="form-control"
                                                    required="required"></asp:TextBox>
                                            </div>

                                            <div class="col-md-6 mb-3">
                                                <label for="simpleinput" class="form-label mb-1">
                                                    Type of OwnerShip
                                                </label>
                                                <asp:TextBox ID="ddltypeofbusiness" TabIndex="4" runat="server" CssClass="form-control">
                                                </asp:TextBox>
                                            </div>

                                            <div class="col-md-12 mb-0">
                                                <div class="row">
                                                    <div class="col-md-4">
                                                        <label for="simpleinput" class="form-label mb-1">
                                                            Business Sector
                                                        </label>
                                                        <asp:TextBox ID="ddlbusinesssector" TabIndex="5" runat="server" CssClass="form-control">
                                                        </asp:TextBox>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <div class="mb-3">
                                                            <label class="form-label">
                                                                Date of Incorporation of the Company
                                                            </label>
                                                            <asp:TextBox ID="txtdateofincorofthecompany" TabIndex="6" runat="server"
                                                                CssClass="form-control" type="date"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <label for="simpleinput" class="form-label mb-1">
                                                            Company Url
                                                        </label>
                                                        <asp:TextBox ID="TxtCompUrl" TabIndex="7" placeholder="ex : www.abc.com" runat="server" CssClass="form-control"></asp:TextBox>

                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-12 mb-3">
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <label class="mb-1">
                                                            Registered Address
                                                        </label>
                                                        <br />
                                                        <label for="floatingTextarea">
                                                            Street Address 1
                                                        </label>
                                                        <asp:TextBox ID="TxtAddress1" runat="server" TabIndex="8" TextMode="MultiLine" Height="100px"
                                                            CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <br />
                                                        <label class="mt-1" for="floatingTextarea">
                                                            Street Address 2
                                                        </label>
                                                        <asp:TextBox ID="TxtAddress2" runat="server" TabIndex="9" TextMode="MultiLine"
                                                            Height="100px" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-12">
                                                <div class="row">
                                                    <div class="col-md-4 mb-3">
                                                        <label for="simpleinput" class="form-label">
                                                            Mobile No
                                                        </label>
                                                        <asp:TextBox ID="txtmobile" runat="server" required="" TabIndex="10" placeholder="ex: 88888888" onkeypress="return isNumberKey(event)" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-4 mb-3">
                                                        <label for="simpleinput" class="form-label">
                                                            Phone Number
                                                        </label>
                                                        <asp:TextBox ID="txtphoneno" runat="server" required="" TabIndex="11" onkeypress="return isNumberKey(event)" MaxLength="10" CssClass="form-control"></asp:TextBox>

                                                    </div>
                                                    <div class="col-md-4 mb-3">
                                                        <label for="simpleinput" class="form-label">
                                                            Fax no
                                                        </label>
                                                        <asp:TextBox ID="txtfaxphoneno" runat="server" TabIndex="12" required="" onkeypress="return isNumberKey(event)" MaxLength="15" CssClass="form-control"></asp:TextBox>

                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-12 mb-3">
                                                <div class="row">
                                                    <div class="col-md-4">
                                                        <label for="simpleinput" class="form-label">
                                                            State
                                                        </label>
                                                        <asp:TextBox ID="txtState" runat="server" TabIndex="13" Class="form-control" />
                                                        <asp:HiddenField ID="hfState" runat="server" />
                                                    </div>
                                                    <div class="col-md-4">
                                                        <label for="simpleinput" class="form-label">
                                                            City
                                                        </label>
                                                        <asp:TextBox ID="txtCity" runat="server" TabIndex="14" Class="form-control" />
                                                        <asp:HiddenField ID="hfCity" runat="server" />
                                                    </div>
                                                    <div class="col-md-4">
                                                        <label for="simpleinput" class="form-label">
                                                            Pincode
                                                        </label>
                                                        <asp:TextBox ID="txtPinCode" runat="server" TabIndex="15" Class="form-control" />
                                                        <asp:HiddenField ID="hfPinCode" runat="server" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-12 mb-3">
                                                <div class="row">
                                                    <div class="col-md-4">
                                                        <label for="simpleinput" class="form-label">
                                                            Tan
                                                        </label>
                                                        <asp:TextBox ID="txTanNo" runat="server" TabIndex="16" Class="form-control" />
                                                    </div>
                                                    <div class="col-md-4">
                                                        <label for="simpleinput" class="form-label">
                                                            UAM
                                                        </label>
                                                        <asp:TextBox ID="txtuamno" runat="server" TabIndex="17" Class="form-control" />
                                                    </div>
                                                    <div class="col-md-4">
                                                        <label for="simpleinput" class="form-label">
                                                            CIN
                                                        </label>
                                                        <asp:TextBox ID="txtcinno" runat="server" TabIndex="18" Class="form-control" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-12">
                                                <asp:UpdatePanel runat="server" ID="upgrid">
                                                    <ContentTemplate>
                                                        <p>Details of company officials</p>
                                                        <asp:GridView ID="gridNameof" runat="server" AutoGenerateColumns="false" class="table table-bordered table-centered mb-0" ShowFooter="true" OnRowCreated="gridNameof_RowCreated">
                                                            <Columns>
                                                                <asp:BoundField DataField="RowNumber" HeaderText="SR.No" />
                                                                <asp:TemplateField HeaderText="Designation">
                                                                    <ItemTemplate>
                                                                        <asp:DropDownList ID="ddlenternameof" TabIndex="19" runat="server" CssClass="form-control">
                                                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                                                            <asp:ListItem Value="Proprietor">Proprietor</asp:ListItem>
                                                                            <asp:ListItem Value="Managing Director">Managing Director</asp:ListItem>
                                                                            <asp:ListItem Value="Partner">Partner</asp:ListItem>
                                                                            <asp:ListItem Value="Director">Director</asp:ListItem>
                                                                            <asp:ListItem Value="Holder of Power of Attorney">Holder of Power of Attorney</asp:ListItem>
                                                                            <asp:ListItem Value="Promoter">Promoter</asp:ListItem>
                                                                            <asp:ListItem Value="Company secretary">Company secretary</asp:ListItem>
                                                                            <asp:ListItem Value="Other">Other</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Name">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtEnterNameof" TabIndex="20" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Email">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtdesignation" TabIndex="21" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RegularExpressionValidator ID="regextxtdesignation" runat="server"
                                                                            ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="txtdesignation" ForeColor="Red" ErrorMessage="Invalid Email Format"></asp:RegularExpressionValidator>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="DIN No">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtdinno" runat="server" TabIndex="22" CssClass="form-control" onkeypress="return isNumberKey(event)" MaxLength="8"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Mobile No">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtmobno" runat="server" TabIndex="23" onkeypress="return isNumberKey(event)" MaxLength="10" CssClass="form-control"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                    <FooterStyle HorizontalAlign="Right" />
                                                                    <FooterTemplate>
                                                                        <asp:LinkButton ID="ButtonAddEnterNameof" runat="server" TabIndex="24" Text="Add New Row" CssClass="btn btn-primary pull-right" OnClick="ButtonAddEnterNameof_Click"></asp:LinkButton>
                                                                    </FooterTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-sm btn-danger"
                                                                            OnClick="LinkButton1_Click"><i class="fa fa-trash"></i></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                        <asp:GridView ID="gvgridNameof" runat="server" AutoGenerateColumns="false"
                                                            class="table table-bordered table-centered mb-0"
                                                            OnRowCommand="gvgridNameof_RowCommand">
                                                            <Columns>
                                                                <asp:BoundField DataField="EnterName" HeaderText="Designation" />
                                                                <asp:BoundField DataField="Name" HeaderText="Name" />
                                                                <asp:BoundField DataField="Designation" HeaderText="Email" />
                                                                <asp:BoundField DataField="DinNo" HeaderText="DIN No" />
                                                                <asp:BoundField DataField="MobileNo" HeaderText="Mobile No" />
                                                                <asp:TemplateField HeaderText="Action">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lbAdd" runat="server" Class="btn btn-sm btn-success" CommandName="Add"
                                                                            CommandArgument='<%#Eval("RowNumber") %>'><i class="fa fa-plus"></i></asp:LinkButton>
                                                                        <asp:LinkButton ID="lbUpdate" runat="server" Class="btn btn-sm btn-warning"
                                                                            CommandName="Upda" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'><i class="fa fa-edit"></i></asp:LinkButton>
                                                                        <asp:HiddenField ID="hfmid" runat="server" Value='<%#Eval("RowNumber") %>' />
                                                                        <asp:LinkButton ID="lbDelete" runat="server" Class="btn btn-sm btn-danger"
                                                                            CommandName="Del" CommandArgument='<%#Eval("RowNumber") %>'><i class="fa fa-trash"></i></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                                <div class="clearfix pb15"></div>
                                            </div>
                                            <div class="col-sm-12">
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                                                    <ContentTemplate>
                                                        <asp:GridView runat="server" ID="gvgovtundertaking" CssClass="table table-bordered table-centered mb-0" AutoGenerateColumns="false"
                                                            ShowFooter="true" OnRowCreated="gvgovtundertaking_RowCreated">
                                                            <Columns>
                                                                <asp:BoundField DataField="SrNoGovt" HeaderText="Sr.No" />
                                                                <asp:TemplateField HeaderText="Name">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtnameundertaking" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Registration No">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtregisnogovtpsu" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Certificate valid upto">
                                                                    <ItemTemplate>
                                                                        <div class="input-append date" id="datePickerall" data-date="12-02-2012" data-date-format="dd-mm-yyyy" style="margin-top: -15px;">
                                                                            <span class="add-on"><i class="icon-th"></i></span>
                                                                            <asp:TextBox ID="txtcertificatevalidupto" runat="server" CssClass="form-control datePickerall" data-date-format="dd/mm/yyyy"></asp:TextBox>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Please Upload Registration Certificate">
                                                                    <ItemTemplate>
                                                                        <asp:FileUpload ID="furegiscerti" runat="server" CssClass="form-control" />
                                                                        <asp:HiddenField ID="hffuregiscerti" runat="server" />
                                                                    </ItemTemplate>
                                                                    <FooterStyle HorizontalAlign="Right" />
                                                                    <FooterTemplate>
                                                                        <asp:Button ID="btnAddmoreGovtpsu" runat="server" CssClass="btn btn-primary pull-right"
                                                                            Text="Add New Row"
                                                                            OnClick="btnAddmoreGovtpsu_Click" />
                                                                    </FooterTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lbremoveGOvtPSU" runat="server" CssClass="btn btn-sm btn-danger"
                                                                            OnClick="lbremoveGOvtPSU_Click"><i class="fa fa-times"></i></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                        <asp:GridView runat="server" ID="gvgovtundertakingedit" CssClass="table table-bordered table-centered mb-0"
                                                            AutoGenerateColumns="false"
                                                            OnRowCreated="gvgovtundertaking_RowCreated"
                                                            OnRowCommand="gvgovtundertakingedit_RowCommand">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Sr.No">
                                                                    <ItemTemplate>
                                                                        <%#Container.DataItemIndex+1 %>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="GovtName" HeaderText="Name" />
                                                                <asp:BoundField DataField="RegistrationNo" HeaderText="Registration No" />
                                                                <asp:BoundField DataField="Validtill" HeaderText="Certificate Valid Upto" />
                                                                <asp:TemplateField HeaderText="Uploaded Registration Certificate">
                                                                    <ItemTemplate>
                                                                        <a href='<%#Eval("CertificateUpload","https://srijandefence.gov.in/Upload/VendorImage/{0}") %>' runat="server" id="img" target="_blank"><%#Eval("CertificateUpload") %></a>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Action">
                                                                    <ItemTemplate>
                                                                        <asp:HiddenField runat="server" ID="hfeditgovt" Value='<%#Eval("GovtMId") %>' />
                                                                        <asp:LinkButton ID="lblsave" runat="server" CssClass="btn btn-sm btn-success" CommandName="newsave" CommandArgument='<%#Eval("GovtMId") %>'>
                                                                            <i class="fa fa-save"></i>
                                                                        </asp:LinkButton>
                                                                        <asp:LinkButton ID="lblupdate" runat="server" CssClass="btn btn-sm btn-warning" CommandName="newedit" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'>
                                                                            <i class="fa fa-edit"></i>
                                                                        </asp:LinkButton>
                                                                        <asp:LinkButton ID="lbldelete" runat="server" CssClass="btn btn-sm btn-danger" CommandName="newdel" CommandArgument='<%#Eval("GovtMId") %>'>
                                                                            <i class="fa fa-trash"></i>
                                                                        </asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                            <div class="col-md-12">
                                                <div class="text-right mt-2">
                                                    <asp:LinkButton ID="btnsubmit" runat="server" Text="Save" TabIndex="25" CssClass="btn btn-primary" OnClick="btnsubmit_Click"></asp:LinkButton>
                                                    <asp:LinkButton ID="btncancel" runat="server" Text="Reset" TabIndex="26" CssClass="btn btn-warning" OnClick="btncancel_Click"></asp:LinkButton>
                                                </div>
                                            </div>
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
                            <asp:UpdatePanel runat="server" ID="mmm" ChildrenAsTriggers="true">
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
            <div class="modal fade" id="changePass" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title"><i class="fa fa-info"></i>&nbsp;&nbsp;Add/Update Details of company officials</h5>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div class="modal-body">
                            <asp:UpdatePanel runat="server" ID="a" ChildrenAsTriggers="true">
                                <ContentTemplate>
                                    <asp:HiddenField ID="hfGenInfoID" runat="server" />
                                    <div class="form-group" style="margin: 0">
                                        <label for="uname" class=" tetLable">
                                            Enter Name of
                                        </label>
                                        <asp:DropDownList runat="server" ID="ddlenternameedit" class="form-control" TabIndex="1" ToolTip="Select Type of Name"
                                            required="">
                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                            <asp:ListItem Value="Proprietor">Proprietor</asp:ListItem>
                                            <asp:ListItem Value="Managing Director">Managing Director</asp:ListItem>
                                            <asp:ListItem Value="Partner">Partner</asp:ListItem>
                                            <asp:ListItem Value="Director">Director</asp:ListItem>
                                            <asp:ListItem Value="Holder of Power of Attorney">Holder of Power of Attorney</asp:ListItem>
                                            <asp:ListItem Value="Promoter">Promoter</asp:ListItem>
                                            <asp:ListItem Value="Company secretary">Company secretary</asp:ListItem>
                                            <asp:ListItem Value="Other">Other</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group" style="margin: 0">
                                        <label for="uname" class=" tetLable">
                                            Name
                                        </label>
                                        <asp:TextBox runat="server" ID="txtnameedit" class="form-control" required="" TabIndex="2" ToolTip="Enter Name"></asp:TextBox>
                                    </div>
                                    <div class="form-group" style="margin: 0">
                                        <label for="uname" class=" tetLable">
                                            Designation
                                        </label>
                                        <asp:TextBox runat="server" ID="txtdesignationedit" class="form-control" required="" TabIndex="3" ToolTip="Enter Designation"></asp:TextBox>
                                    </div>
                                    <div class="form-group" style="margin: 0">
                                        <label for="uname" class=" tetLable">
                                            DIN No
                                        </label>
                                        <asp:TextBox runat="server" ID="txtdinnoedit" class="form-control" required="" TabIndex="4" ToolTip="Enter Din No"></asp:TextBox>
                                    </div>
                                    <div class="form-group" style="margin: 0">
                                        <label for="uname" class=" tetLable">
                                            Mobile No
                                        </label>
                                        <asp:TextBox runat="server" ID="txtmobnoedit" class="form-control" required="" onkeypress="return isNumberKey(event)" MaxLength="11" TabIndex="5" ToolTip="Mobile No (123456789) Only Number"></asp:TextBox>
                                    </div>
                                    <div class="clearfix mt-2"></div>
                                    <div class="col-md-12">
                                        <div class="text-right mt-2">
                                            <asp:LinkButton ID="btnupdate" runat="server" Text="Edit & Update" CssClass="btn btn-primary" OnClick="btnupdate_Click"></asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="clearfix"></div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal fade" id="divgovt" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title"><i class="fa fa-info"></i>&nbsp;&nbsp;Govt. Department/Undertaking/PSU under Ministry of Defence/Gem</h5>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div class="modal-body">
                            <asp:UpdatePanel runat="server" ID="UpdatePanel2" ChildrenAsTriggers="true">
                                <ContentTemplate>
                                    <div class="form-group" style="margin: 0">
                                        <label for="uname" class=" tetLable">
                                            Name
                                        </label>
                                        <asp:TextBox runat="server" ID="txtname" placeholder="Name" Class="form-control"></asp:TextBox>
                                    </div>
                                    <div class="form-group" style="margin: 0">
                                        <label for="uname" class=" tetLable">
                                            Registration No	
                                        </label>
                                        <asp:TextBox runat="server" ID="txtregno" placeholder="Complete Address" TextMode="MultiLine" Class="form-control"></asp:TextBox>
                                    </div>
                                    <div class="form-group" style="margin: 0">
                                        <label for="uname" class=" tetLable">
                                            Certificate Valid Upto
                                        </label>
                                        <div class="input-append date" id="datePicker" data-date="12-02-2012" data-date-format="dd-mm-yyyy">
                                            <span class="add-on"><i class="icon-th"></i></span>
                                            <asp:TextBox ID="txtdatevalid" runat="server" CssClass="form-control datePicker"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group" style="margin: 0">
                                        <label for="uname" class=" tetLable">
                                            File Authorization
                                        </label>
                                        <asp:HiddenField ID="hffile" runat="server" />
                                        <asp:FileUpload runat="server" ID="fufile" Class="form-control" />
                                    </div>
                                    <div class="clearfix mt10"></div>
                                    <div class="form-group" style="margin: 0">
                                        <asp:LinkButton ID="lbsub" runat="server" Text="Edit & Update" CssClass="btn btn-primary pull-right mr10" OnClick="lbsub_Click"></asp:LinkButton>
                                    </div>
                                    <div class="clearfix mt10"></div>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnUploadLogo" />
                                </Triggers>
                            </asp:UpdatePanel>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript">
        function showPopup1() {
            $('#changePass').modal('show', function () {
            });
        }
    </script>
     <script type="text/javascript">
         function showPopup2() {
             $('#divgovt').modal('show', function () {
             });
         }
     </script>
    <script type="text/javascript">
        function showPopup() {
            $('#modelmsg').modal('show', function () {
            });
        }
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
