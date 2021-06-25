<%@ Page Title="" Language="C#" MasterPageFile="~/Vendor/VendorMaster.master" AutoEventWireup="true" CodeFile="ReferrerVendorRegistration.aspx.cs" Inherits="Vendor_ReferrerVendorRegistration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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
                    <h1>Reffer Subvendor Registration</h1>

                    <div class="animated fadeIn">



                        <div class="form-group">
                            <div class="col-sm-5">
                                Please Enter Reffer Email Id: 
                                <%--<span class="mandatory">*</span>--%>
                            </div>
                            <div class="col-sm-7">
                                <asp:TextBox ID="VEmailID" runat="server" class="form-control" placeholder="Update Email ID *"></asp:TextBox><br />
                                <asp:Button class="btn btn-primary btn-sm" ID="Button1" Text="Click Here For Reffer" runat="server" OnClick="ReffEmail" OnClientClick="return validate1();" />
                            </div>
                            <div class="clearfix mt10"></div>
                        </div>
                        <div class="row">
                            <div class="col-lg-12">
                            </div>
                            <!--/.col-->


                        </div>
                        <!-- .content -->
                        <div class="modal-content" runat="server" id="P3" visible="false">
                                    <div class="modal-header modal-header1">
                                        <button type="button" class="close close1" data-dismiss="modal">&times;</button>
                                        <h4 class="modal-title">OTP</h4>
                                    </div>
                                    <form class="form-horizontal changepassword" role="form">
                                        <div class="modal-body clearfix" style="padding: 0 20px;">
                                            <div class="form-group">
                                                <label>Enter OTP <span class="mandatory">*</span></label>
                                                <asp:TextBox runat="server" ID="txtOTP" type="password" MaxLength="6" TabIndex="4" focus="true" class="form-control " placeholder="Enter OTP">
                                                </asp:TextBox>
                                                <asp:Label ID="lblmssg" runat="server"></asp:Label>
                                            </div>
                                            <div class="clearfix"></div>
                                            <div class="modal-footer">
                                                <asp:LinkButton runat="server" ID="lblresendotp" CausesValidation="False" UseSubmitBehavior="False" CssClass="btn btn-primary pull-right forgot-pass-btn" TabIndex="7" Text="Resend OTP" OnClick="lblresendotp_Click"></asp:LinkButton>
                                                <asp:LinkButton runat="server" ID="lbsubotp" CausesValidation="False" UseSubmitBehavior="False" Style="margin-right: 10px;" CssClass="btn btn-primary pull-right forgot-pass-btn" TabIndex="7" Text="Submit" OnClick="lbsubotp_Click"></asp:LinkButton>

                                            </div>
                                        </div>
                                    </form>
                                </div>
                    </div>
                </div>
            </div>

        </div>
    </div>
   

    <script type="text/javascript">
        function validate_disable() {
        }
        function validate1() {
            var flag = true;
            if ((document.getElementById("<%=VEmailID.ClientID%>").value.trim() == '') || (document.getElementById("<%=VEmailID.ClientID%>").value.trim() == undefined)) {
                alert('EmailID cannot be empty');
                flag = false;
            }

            return flag;
        }
    </script>

</asp:Content>

