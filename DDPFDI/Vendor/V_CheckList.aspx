<%@ Page Language="C#" AutoEventWireup="true" CodeFile="V_CheckList.aspx.cs" Inherits="Vendor_V_CheckList1" MasterPageFile="~/Vendor/VendorMaster.master" %>

<%@ Register Src="~/Vendor/InnerMenu.ascx" TagName="Head" TagPrefix="Menu" %>
<asp:Content runat="server" ID="msthead" ContentPlaceHolderID="head">
    <style>
        label {
            padding-left: 5px !important;
            margin-right: 10px !important;
            margin-top: 20px !important;
        }
    </style>
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
                                        <div class="row">
                                            <div class="col-12">
                                                <div class="page-title-box">
                                                    <h4 class="page-title">Please select and check (checkbox) that file you uploaded in this form.
                                                    </h4>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-12">
                                            <asp:HiddenField ID="Catid" runat="server" />
                                            <div class="mb-3">
                                                <asp:DropDownList ID="ddltypeofchk" runat="server" AutoPostBack="true"
                                                    CssClass="form-select" OnSelectedIndexChanged="ddltypeofchk_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="custom-control custom-checkbox">
                                                    <asp:CheckBoxList ID="CheckBoxList3" CssClass="custom-control-input" runat="server" RepeatColumns="4"
                                                        RepeatDirection="Horizontal" Visible="false" RepeatLayout="Flow">
                                                    </asp:CheckBoxList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-12">
                                            <div class="text-right mt-2">
                                                <asp:LinkButton ID="btnsubmit" runat="server" CssClass="btn btn-success" OnClick="btnsubmit_Click"><i class="fa fa-sticky-note"></i>&nbsp;Save</asp:LinkButton>
                                                <asp:LinkButton ID="btnPrev" runat="server" CssClass="btn btn-info" OnClick="btnPrev_Click"><i class="fa fa-backward"></i>&nbsp;Previous </asp:LinkButton>
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
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript">
        function showPopup2() { $('#modelmsg').modal('show', function () { }); }
    </script>
</asp:Content>

