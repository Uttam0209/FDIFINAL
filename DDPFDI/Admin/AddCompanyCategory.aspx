<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddCompanyCategory.aspx.cs" Inherits="Admin_AddCompanyCategory" MasterPageFile="MasterPage.master" %>

<asp:Content ID="headDesignation" runat="server" ContentPlaceHolderID="head">
    <script src="assets/js/jquery-3.4.1.min.js"></script>
    <script type="text/javascript">
        function radioMe(e) {
            if (!e) e = window.event;
            var sender = e.target || e.srcElement;

            if (sender.nodeName != 'INPUT') return;
            var checker = sender;
            var chkBox = document.getElementById('<%= chkSubCategory.ClientID %>');
            var chks = chkBox.getElementsByTagName('INPUT');
            for (i = 0; i < chks.length; i++) {
                if (chks[i] != checker)
                    chks[i].checked = false;
            }
        }
    </script>
    <script type="text/javascript">
        var atLeast = 1
        function Validate() {
            var CHK = document.getElementById("<%=chkSubCategory.ClientID%>");
            var checkbox = CHK.getElementsByTagName("input");
            var counter = 0;
            for (var i = 0; i < checkbox.length; i++) {
                if (checkbox[i].checked) {
                    counter++;
                }
            }
            if (atLeast > counter) {
                alert("Please select atleast " + atLeast + " Level in item(s)");
                return false;
            }
            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="InnerDesignation" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div class="content oem-content">
        <asp:ScriptManager ID="sn" runat="server"></asp:ScriptManager>
        <asp:HiddenField runat="server" ID="hidType" />
        <asp:HiddenField runat="server" ID="hfcomprefno" />
        <div class="sideBg">
            <div class="row">
                <div class="col-md-12 padding_0">
                    <div id="divHeadPage" runat="server"></div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="clearfix"></div>
                    <div style="margin-top: 5px;">
                        <a class="fa fa-arrow-circle-left pull-right" href="javascript: history.go(-1)">&nbsp; &nbsp;Back</a>
                    </div>
                    <div class="clearfix"></div>
                </div>
                <div class="clearfix" style="margin-bottom: 10px;"></div>
                <asp:UpdatePanel runat="server" ID="updrop">
                    <ContentTemplate>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Select Company</label>
                                <asp:DropDownList runat="server" ID="ddlcompany" CssClass="form-control form-cascade-control"></asp:DropDownList>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="section-pannel">
                <div class="row">
                    <asp:UpdatePanel runat="server" ID="up">
                        <ContentTemplate>
                            <asp:Panel ID="Panel1" runat="server" DefaultButton="btndemofirst">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Dropdown Label <span class="mandatory">*</span></label>
                                        <asp:DropDownList runat="server" ID="ddlmastercategory" TabIndex="1" class="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlmastercategory_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-6" id="level1" runat="server" visible="False">
                                    <div class="form-group">
                                        <h3 class="secondary-heading">Level 1 <span class="mandatory">*</span></h3>
                                        <asp:CheckBoxList ID="chkSubCategory" runat="server" TabIndex="2" RepeatColumns="1" RepeatDirection="Vertical"
                                            RepeatLayout="Flow">
                                        </asp:CheckBoxList>
                                    </div>
                                </div>
                                <div class="col-sm-12">
                                    <div class="form-group">
                                        <asp:LinkButton ID="btndemofirst" runat="server" CssClass="btn btn-primary pull-right" TabIndex="3" Style="margin-left: 10px;" Text="Save"
                                            OnClick="btndemofirst_Click" OnClientClick="return Validate()" />
                                    </div>
                                </div>
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="up">
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
            <div class="clearfix"></div>
            <div class="footer"><i class="far fa-copyright"></i>2019 <a href="#">Department of Defence Production</a> </div>
        </div>
    </div>
</asp:Content>
