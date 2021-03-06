﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="V_RegistrationNo.aspx.cs" Inherits="Vendor_V_RegistrationNo" MasterPageFile="~/Vendor/VendorMaster.master" %>

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
                    <div id="qpt" class="tab-pane">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <asp:Panel ID="panstep3" runat="server">
                                    <div class="form-group">
                                        <div class="col-sm-5">
                                            Do you have TAN
                                        </div>
                                        <div class="col-sm-7">
                                            <asp:DropDownList ID="ddldetailofpantan" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddldetailofpantan_SelectedIndexChanged">
                                                <asp:ListItem Selected="True" Value="0">Select</asp:ListItem>
                                                <asp:ListItem Value="1">NO</asp:ListItem>
                                                <asp:ListItem Value="2">YES</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group" runat="server" id="divpantan" visible="false">
                                        <div class="col-sm-5">
                                            <asp:Label ID="lblpantan" runat="server"></asp:Label>
                                        </div>
                                        <div class="col-sm-7">
                                            <asp:TextBox ID="txtpantan" runat="server" CssClass="form-control">
                                            </asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-sm-5">
                                            GSTIN
                                        </div>
                                        <div class="col-sm-7">
                                            <asp:TextBox ID="txtgstin" runat="server" CssClass="form-control" OnTextChanged="txtgstin_TextChanged">
                                            </asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-sm-5">
                                            UAM
                                        </div>
                                        <div class="col-sm-7">
                                            <asp:TextBox ID="txtUAM" runat="server" CssClass="form-control">
                                            </asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-sm-5">
                                            <asp:Label ID="lblcin" runat="server" Text="CIN"></asp:Label>
                                        </div>
                                        <div class="col-sm-7">
                                            <asp:TextBox ID="txtCIN" runat="server" CssClass="form-control">
                                            </asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-sm-5">
                                            Are you registered with Govt. Department/Undertaking/PSU under Ministry of Defence/Gem
                                        </div>
                                        <div class="col-sm-7">
                                            <asp:DropDownList ID="ddlDepartmentUndertakingPSU" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlDepartmentUndertakingPSU_SelectedIndexChanged">
                                                <asp:ListItem Selected="True" Value="0">Select</asp:ListItem>
                                                <asp:ListItem Value="1">YES</asp:ListItem>
                                                <asp:ListItem Value="2">NO</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div runat="server" id="divgovtundertaking" visible="false">
                                        <asp:GridView runat="server" ID="gvgovtundertaking" CssClass="table table-hover" AutoGenerateColumns="false"
                                            ShowFooter="true" OnRowCreated="gvgovtundertaking_RowCreated"
                                            CellPadding="4" ForeColor="#333333" GridLines="None">
                                            <AlternatingRowStyle BackColor="White" />
                                            <Columns>
                                                <asp:BoundField DataField="SrNoGovt" HeaderText="Sr.No" />
                                                <asp:TemplateField HeaderText="Name">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtnameundertaking" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Registration No">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtregisnogovtpsu" runat="server" CssClass="form-control"></asp:TextBox>
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
                                                        <asp:LinkButton ID="lbremoveGOvtPSU" runat="server" CssClass="fa fa-times"
                                                            OnClick="lbremoveGOvtPSU_Click"></asp:LinkButton>
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
                                        <asp:GridView runat="server" ID="gvgovtundertakingedit" CssClass="table table-hover" AutoGenerateColumns="false"
                                            ShowFooter="true" OnRowCreated="gvgovtundertaking_RowCreated"
                                            CellPadding="4" ForeColor="#333333" GridLines="None" OnRowCommand="gvgovtundertakingedit_RowCommand">
                                            <AlternatingRowStyle BackColor="White" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sr.No">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Name_PSU_UnderGovt" HeaderText="Name" />
                                                <asp:BoundField DataField="RegistrationNo" HeaderText="Registration No" />
                                                <asp:BoundField DataField="Certificate_valid_upto" HeaderText="Certificate Valid Upto" />
                                                <asp:TemplateField HeaderText="Uploaded Registration Certificate">
                                                    <ItemTemplate>
                                                         <a href='<%#Eval("Upload_Registration_Certificate","https://srijandefence.gov.in/Upload/VendorImage/{0}") %>' runat="server" id="img" target="_blank"><%#Eval("Upload_Registration_Certificate") %></a>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                        <asp:HiddenField runat="server" ID="hfeditgovt" Value='<%#Eval("MasterId") %>' />
                                                        <asp:LinkButton ID="lblsave" runat="server" CssClass="fa fa-save" CommandName="newsave" CommandArgument='<%#Eval("MasterId") %>'></asp:LinkButton>
                                                        <asp:LinkButton ID="lblupdate" runat="server" CssClass="fa fa-edit" CommandName="newedit" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'></asp:LinkButton>
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
                                    </div>
                                    <div class="clearfix mt10"></div>
                                    <asp:Button ID="btnsubmit" runat="server" Text="Submit" CssClass="btn btn-primary pull-right mr10" OnClick="btnsubmit_Click" />
                                </asp:Panel>
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="btnsubmit" />
                            </Triggers>
                        </asp:UpdatePanel>
                        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel2">
                            <ProgressTemplate>
                                <div class="overlay-progress">
                                    <div class="custom-progress-bar blue stripes">
                                        <span></span>
                                        <p>Processing</p>
                                    </div>
                                </div>
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <%--Modal Popup End--%>
    <div class="modal fade" id="divgovt" role="dialog" data-keyboard="false" data-backdrop="static">
        <div class="modal-dialog" style="width: 400px;">
            <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                <ContentTemplate>
                    <div class="modal-content" runat="server" id="Div10">
                        <div class="modal-header modal-header1">
                            <button type="button" class="close close1" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Govt. Department/Undertaking/PSU under Ministry of Defence/Gem</h4>
                        </div>
                        <form class="form-horizontal changepassword" role="form">
                            <div class="modal-body clearfix" style="padding: 0 20px;">
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
                        </form>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <script type="text/javascript">
        function showPopup() {
            $('#divgovt').modal('show', function () {
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
