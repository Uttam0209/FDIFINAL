<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CreatetempTable.aspx.cs" Inherits="Admin_CreatetempTable" MasterPageFile="~/Admin/MasterPage.master" %>

<asp:Content ID="head" runat="server" ContentPlaceHolderID="head">
    <style>
        .myimgdivshowhide {
            display: none;
        }
    </style>
</asp:Content>
<asp:Content ID="inner" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <asp:UpdatePanel runat="server" ID="updatepan">
        <ContentTemplate>
            <div class="content oem-content">
                <div class="sideBg">
                    <div class="row">
                        <div class="col-md-12 padding_0">
                            <div id="divHeadPage" runat="server"></div>
                            <div id="ContentPlaceHolder1_divHeadPage">
                                <ul class="breadcrumb">
                                    <li class=""><span>Update User Product</span></li>
                                </ul>
                            </div>
                        </div>
                        <div class="col-md-12" style="text-align: center; margin-top: 200px;">
                            <div class="panel-info"><b>Please wait some time after click on update record it will take some time to update master record in database.</b></div>
                            <div class="clearfix mt10"></div>
                            <div class="myimgdivshowhide">
                                <img src="assets/images/loading.gif" alt="Please wait till product getting update in master database" class="img-responsive img-thumbnail" />
                            </div>
                            <div class="clearfix mt10"></div>
                            <asp:LinkButton ID="btnupdate" runat="server" CssClass="btn btn-primary text-center showbtn" OnClick="btnupdate_Click"><i class="fa fa-upload"></i>&nbsp;Click me to update record</asp:LinkButton>
                            <div class="clearfix mt10"></div>
                            <b>
                                <asp:Label ID="lbl" runat="server" Text="" CssClass="text-center"></asp:Label></b>
                            <div class="clearfix mt10"></div>
                            <asp:LinkButton ID="lbldownloadexcel" runat="server" CssClass="fa fa-download btn btn-success showbtn" OnClick="lbldownloadexcel_Click">&nbsp;Download Excel</asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnupdate" />
            <asp:PostBackTrigger ControlID="lbldownloadexcel" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress" runat="server" AssociatedUpdatePanelID="updatepan">
        <ProgressTemplate>
            <div class="overlay-progress">
                <div class="custom-progress-bar blue stripes">
                    <span></span>
                    <p>Processing</p>
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <script>
        $(document).ready(function () {
            $('.showbtn').click(function () {
                $('.myimgdivshowhide').show();
            });
        });
    </script>

</asp:Content>
