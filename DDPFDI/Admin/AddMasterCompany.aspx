<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddMasterCompany.aspx.cs" Inherits="Admin_AddMasterCompany" MasterPageFile="~/Admin/MasterPage.master" %>


<asp:Content ID="head123" runat="server" ContentPlaceHolderID="head">
    <script src="../assets/js/jquery-3.4.1.min.js"></script>
    <script type="text/javascript">
        //Alert pop up box
        function ShowMessage() {
            $("body").css('overflow', 'hidden');
            $('.alert-overlay-error').show();

            $('.alertMsg').text("Please select atleast " + atLeast + " intrested in item(s)");
        }
        function radioMe(e) {
            if (!e) e = window.event;
            var sender = e.target || e.srcElement;

            if (sender.nodeName != 'INPUT') return;
            var checker = sender;
            var chkBox = document.getElementById('<%= chkrole.ClientID %>');
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
            var CHK = document.getElementById("<%=chkintrestedarea.ClientID%>");
            var CHK1 = document.getElementById("<%=chkmastermenuallot.ClientID%>");
            var CHK2 = document.getElementById("<%=chkrole.ClientID%>");
            var checkbox = CHK.getElementsByTagName("input");
            var counter = 0;
            for (var i = 0; i < checkbox.length; i++) {
                if (checkbox[i].checked) {
                    counter++;
                }
            }
            if (atLeast > counter) {
                ShowMessage();
                //alert("Please select atleast " + atLeast + " intrested in item(s)");
                return false;
            }
            var checkbox1 = CHK1.getElementsByTagName("input");
            var counter = 0;
            for (var i = 0; i < checkbox1.length; i++) {
                if (checkbox1[i].checked) {
                    counter++;
                }
            }
            if (atLeast > counter) {
                ShowMessage();
                //alert("Please select atleast " + atLeast + " menu alloted item(s)");
                return false;
            }
            var checkbox2 = CHK2.getElementsByTagName("input");
            var counter = 0;
            for (var i = 0; i < checkbox2.length; i++) {
                if (checkbox2[i].checked) {
                    counter++;
                }
            }
            if (atLeast > counter) {
                alert("Please select atleast " + atLeast + " role item(s)");
                return false;
            }
            return true;
        }
    </script>
    <script type="text/javascript">
        function showPopup() {
            $('#ShowDetails').modal('show');
        }
    </script>
</asp:Content>
<asp:Content ID="inner2" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div class="content oem-content">
        <asp:ScriptManager ID="sn" runat="server"></asp:ScriptManager>
        <div class="sideBg clearfix">
            <div class="row">
                <div class="col-md-12 padding_0">
                    <div id="divHeadPage" runat="server"></div>
                </div>
                <div class="col-md-12">
                    <div class="clearfix"></div>
                    <div style="margin-top: 5px;">
                        <a class="fa fa-arrow-circle-left pull-right" href="javascript: history.go(-1)">&nbsp; &nbsp;Back</a>
                    </div>
                    <div class="clearfix"></div>
                </div>
            </div>
            <div class="clearfix" style="margin-bottom: 10px;"></div>
            <div class="col-md-12">
                <div class="addfdi">
                    <asp:UpdatePanel ID="upfdival" runat="server">
                        <ContentTemplate>
                            <asp:Panel ID="Panel1" runat="server" DefaultButton="btnsubmit">
                                <div class="section-pannel">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="form-group" style="padding: 0 10px;" runat="server" id="mastercompany" visible="False">
                                                <asp:Label ID="lblMastcompany" runat="server" Text="" CssClass="form-label"></asp:Label><span data-toggle="tooltip" class="fa fa-question" title="Please Select Company Name"></span>
                                                <asp:DropDownList runat="server" ID="ddlmaster" AutoPostBack="True" TabIndex="1" CssClass="form-control form-cascade-control" OnSelectedIndexChanged="ddlmaster_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="form-group" style="padding: 0 10px;" runat="server" id="masterfacotry" visible="False">

                                                <asp:Label ID="lblfactoryName" runat="server" Text="" CssClass="form-label"></asp:Label><span data-toggle="tooltip" class="fa fa-question" title="Please Select Division/Plant Name"></span>
                                                <asp:DropDownList runat="server" ID="ddlfacotry" AutoPostBack="True" TabIndex="2" OnSelectedIndexChanged="ddlfacotry_SelectedIndexChanged" CssClass="form-control form-cascade-control">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-12">
                                            <div class="fdi-add-content">
                                                <div runat="server">
                                                    <div class="section-pannel">
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group">
                                                                    <asp:Label ID="lblName" runat="server" Text="" CssClass="form-label"></asp:Label><span data-toggle="tooltip" class="fa fa-question" title="Please enter organization name"></span>
                                                                    <asp:TextBox ID="txtcomp" runat="server" required="" TabIndex="3" class="form-control form-cascade-control"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-md-6" id="divOfficerEmail" runat="server">
                                                            <div class="form-group">
                                                                <label class=" control-label">Officer email id </label>
                                                                <asp:TextBox ID="txtemail" runat="server" pattern="[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,}$" TabIndex="4" class="form-control form-cascade-control"></asp:TextBox>
                                                                <p class="note">*Note: will be used as username </p>
                                                            </div>
                                                        </div>
                                                    </div>

                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="is-flex">
                                    <div class="col-md-5" id="Intrested" visible="False" runat="server">
                                        <div class="fdi-add-content">
                                            <div class="form-group">

                                                <h3 class="secondary-heading">Intrested In</h3>
                                                <asp:CheckBoxList ID="chkintrestedarea" runat="server" CssClass="checkbox-inline" TabIndex="6" RepeatColumns="8" RepeatDirection="Vertical" RepeatLayout="Flow">
                                                </asp:CheckBoxList>

                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4" id="MenuAlot" visible="False" runat="server">
                                        <div class="fdi-add-content">
                                            <div class="form-group">

                                                <h3 class="secondary-heading">Menu Alotted</h3>
                                                <asp:CheckBoxList ID="chkmastermenuallot" runat="server" TabIndex="7" RepeatColumns="1" RepeatDirection="Vertical" RepeatLayout="Flow">
                                                </asp:CheckBoxList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3" id="divRole" visible="False" runat="server">
                                        <div class="fdi-add-content">
                                            <div class="form-group">
                                                <h3 class="secondary-heading">Role</h3>
                                                <asp:CheckBoxList ID="chkrole" runat="server" onclick="MutExChkList(this);" RepeatColumns="1" TabIndex="8" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                                    <asp:ListItem Value="Admin">Admin</asp:ListItem>
                                                    <asp:ListItem Value="Company">Company</asp:ListItem>
                                                </asp:CheckBoxList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="fdi-add-content">
                                            <div class="form-group">
                                                <asp:Button ID="btncancel" runat="server" Text="Cancel" Visible="False" TabIndex="10" CssClass="btn btn-default pull-right" OnClick="btncancel_Click" />
                                                <asp:Button ID="btnsubmit" runat="server" Text="Save" TabIndex="9" CssClass="btn btn-primary pull-right" OnClick="btnsubmit_Click" OnClientClick="return Validate()" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                            <div class="parentRow" style="display: none">
                                Company : <span>Demo</span>
                            </div>
                            <div class="table-wraper">
                                <asp:GridView ID="gvcompanydetailsave" runat="server" Width="100%" Class="commonAjaxTbl master-company-table table display 
                                    responsive no-wrap table-hover manage-user Grid text-nowrap"
                                    AutoGenerateColumns="false" RowCommand="gvcompanydetailsave_RowCommand" OnRowCreated="gvcompanydetailsave_RowCreated"
                                     OnRowCommand="gvcompanydetailsave_RowCommand" OnRowDataBound="gvcompanydetailsave_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="S.No.">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="CompanyName" HeaderText="Company" />
                                        <asp:BoundField ItemStyle-Width="150px" DataField="CompanyRefNo" Visible="False" HeaderText="Reference No." />
                                        <asp:BoundField ItemStyle-Width="150px" DataField="FactoryName" HeaderText="Division" />
                                        <asp:BoundField ItemStyle-Width="150px" DataField="FactoryRefNo" Visible="False" HeaderText="Reference No." />
                                        <asp:BoundField ItemStyle-Width="150px" DataField="UnitName" HeaderText="Unit" />
                                        <asp:BoundField ItemStyle-Width="150px" DataField="UnitRefNo" Visible="False" HeaderText="Reference No." />
                                        <asp:BoundField ItemStyle-Width="150px" DataField="NodalOficerName" HeaderText="Nodal Officer Name" />
                                        <asp:BoundField ItemStyle-Width="150px" DataField="NodalOfficerEmail" HeaderText="Nodal Officer Email" />
                                        <asp:BoundField ItemStyle-Width="150px" DataField="NodalOfficerEmail" HeaderText="Nodal Officer Email" />
                                        <asp:BoundField ItemStyle-Width="150px" DataField="NodalOfficerEmail" HeaderText="Nodal Officer Email" />
                                        <asp:BoundField ItemStyle-Width="150px" DataField="Role" HeaderText="Role" />
                                        <asp:BoundField ItemStyle-Width="150px" DataField="CreatedBy" HeaderText="Created By" />
                                        <asp:TemplateField HeaderText="Action" Visible="true">
                                            <ItemTemplate>
                                                <asp:LinkButton runat="server" ID="lblview" CommandArgument='<%#Eval("CompanyRefNo") %>' CommandName="viewComp" CssClass="fa fa-eye"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <div class="modal fade" id="ShowDetails" role="dialog">
                                <div class="modal-dialog" style="width: 700px;">
                                    <!-- Modal content-->
                                    <div class="modal-content">
                                        <div class="modal-header modal-header1">
                                            <button type="button" class="close close1" data-dismiss="modal">&times;</button>
                                            <h4 class="modal-title">Detail</h4>
                                        </div>

                                        <div class="selectedInterest is-flex">
                                            <div class="box">
                                                <h4 class="secondary-heading">Interested</h4>
                                                <asp:Label ID="lblintrestedin" runat="server"></asp:Label>

                                            </div>
                                            <div class="box">
                                                <h4 class="secondary-heading">Menu Alloted</h4>
                                                <asp:Label ID="lblmenuallot" runat="server"></asp:Label>
                                            </div>

                                        </div>

                                        <div class="modal-footer">
                                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                        </div>
                                    </div>

                                </div>

                            </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:UpdateProgress ID="UpdateProgress" runat="server" AssociatedUpdatePanelID="upfdival">
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
            </div>
            <div class="clearfix"></div>
            <div class="footer"><i class="far fa-copyright"></i>2019 <a href="#">Department of Defence Production</a> </div>
        </div>
    </div>
</asp:Content>
