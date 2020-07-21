<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Test.aspx.cs" Inherits="Admin_Test" MasterPageFile="~/Admin/MasterPage.master" %>

<asp:Content ID="AddNodal" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div class="content oem-content">
        <asp:UpdatePanel runat="server" ID="updatepan">
            <ContentTemplate>
                <asp:LinkButton ID="a" runat="server" Text="safgaga" CssClass="btn btn-primary" OnClick="a_Click"></asp:LinkButton>
                <div class="modal-quick-view modal fade" id="ProductCompany" tabindex="-1">
                    <div class="modal-dialog modal-xl" style="max-width: 800px!important; z-index: 9999999999;">
                        <iframe runat="server" id="aa" width="900" height="600" frameborder="0" visible="true"></iframe>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdateProgress ID="UpdateProgress" runat="server" AssociatedUpdatePanelID="updatepan">
            <ProgressTemplate>
                <!---Progress Bar ---->
                <div class="overlay-progress">
                    <div class="custom-progress-bar blue stripes">
                        <span></span>
                        <p>Processing</p>
                    </div>
                </div>
                <!---Progress Bar ---->
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    <script type="text/javascript">
        function showPopup() {
            $('#ProductCompany').modal('show');
        }
    </script>
</asp:Content>

