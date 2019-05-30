<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditHeadDropdown.aspx.cs" Inherits="Admin_EditHeadDropdown" MasterPageFile="MasterPage.master" %>

<asp:Content ID="headDesignation" runat="server" ContentPlaceHolderID="head">
</asp:Content>
<asp:Content ID="InnerDesignation" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <asp:ScriptManager ID="sc" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel runat="server" ID="updatepan">
        <ContentTemplate>
            <div class="content oem-content">
                <div class="sideBg">
                    <div class="row">
                        <div class="col-md-12 padding_0">
                            <div id="divHeadPage" runat="server"></div>
                        </div>
                    </div>
                    <form method="post" class="addfdi">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-4">
                                    <asp:Button runat="server" ID="btncomp" Text="Company Edit" OnClick="btncomp_Click" />
                                </div>
                                <div class="col-md-4" runat="server" id="divlblselectdivison">
                                    <asp:Button runat="server" ID="btndivision" Text="Division Edit" OnClick="btndivision_Click" />
                                </div>
                                <div class="col-md-4" runat="server" id="divlblselectunit">
                                    <asp:Button runat="server" ID="btnunit" Text="Unit Edit" OnClick="btnunit_Click" />
                                </div>
                            </div>
                        </div>
                    </form>
                </div>
            </div>

            <!-----Alert Box ------>
            <div class="alert-overlay alert-overlay-successful" style="display: none">
                <div class="alert-box">
                    <div class="box">
                        <div class="success-checkmark">
                            <div class="check-icon">
                                <span class="icon-line line-tip"></span>
                                <span class="icon-line line-long"></span>
                                <div class="icon-circle"></div>
                                <div class="icon-fix"></div>
                            </div>
                        </div>
                        <div class="alert alertMsg">
                            Sucessfull Saved !
                        </div>
                        <button class="btn btn-success close_alert " data-dismiss="modal">OK</button>
                    </div>
                </div>
            </div>
            <!-----Alert Box ------>
            <script src="assets/js/jquery-1.12.4.js"></script>
            <script>
                function SuccessfullPop() {
                    console.log('testing');
                    $("body").css('overflow', 'hidden');
                    $('.alert-overlay').show();
                }

                //Hide Alert Pop up
                $('.close_alert').on('click', function () {
                    $("body").css('overflow', 'visible');
                    $('.alert-overlay-successful').hide();
                });
            </script>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
