<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddMasterCompany.aspx.cs" Inherits="Admin_AddMasterCompany" MasterPageFile="~/Admin/MasterPage.master" %>


<asp:Content ID="head123" runat="server" ContentPlaceHolderID="head">
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
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
                alert("Please select atleast " + atLeast + " intrested in item(s)");
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
                alert("Please select atleast " + atLeast + " menu alloted item(s)");
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
</asp:Content>

<asp:Content ID="inner2" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div class="content oem-content">
        <asp:ScriptManager ID="sn" runat="server"></asp:ScriptManager>
        <div class="sideBg">
            <div class="row">
                <div class="col-md-12 padding_0">
                    <div id="divHeadPage" runat="server"></div>
                </div>
            </div>


            <div class="col-md-12">

                <div class="addfdi">
                    <asp:UpdatePanel ID="upfdival" runat="server">
                        <ContentTemplate>
                            <div class="form-group" runat="server" id="mastercompany" visible="False">
                                <asp:Label ID="lblMastcompany" runat="server" Text="" CssClass="form-label"></asp:Label>
                                <asp:DropDownList runat="server" ID="ddlmaster" AutoPostBack="True" CssClass="form-control form-cascade-control" OnSelectedIndexChanged="ddlmaster_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                            <div class="form-group" runat="server" id="masterfacotry" visible="False">
                                <asp:Label ID="lblfactoryName" runat="server" Text="" CssClass="form-label"></asp:Label>
                                <asp:DropDownList runat="server" ID="ddlfacotry" AutoPostBack="True" OnSelectedIndexChanged="ddlfacotry_SelectedIndexChanged" CssClass="form-control form-cascade-control">
                                </asp:DropDownList>
                            </div>

                            <div class="row">
                                <div class="col-md-12">
                                    <div class="fdi-add-content">
                                        <div runat="server">

                                            <div class="row">
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <asp:Label ID="lblName" runat="server" Text="" CssClass="form-label"></asp:Label>
                                                        <asp:TextBox ID="txtcomp" runat="server" class="form-control form-cascade-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-6" id="divOfficerEmail" runat="server">
                                                    <div class="form-group">

                                                        <label class=" control-label">Officer email id </label>
                                                        <asp:TextBox ID="txtemail" runat="server" pattern="[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,}$" class="form-control form-cascade-control"></asp:TextBox>
                                                        <p class="note">*Note: will be used as username </p>
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
                                            <asp:CheckBoxList ID="chkintrestedarea" runat="server" CssClass="checkbox-inline" RepeatColumns="5" RepeatDirection="Vertical" RepeatLayout="Flow">
                                            </asp:CheckBoxList>

                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4" id="MenuAlot" visible="False" runat="server">
                                    <div class="fdi-add-content">
                                        <div class="form-group">

                                            <h3 class="secondary-heading">Menu Alotted</h3>
                                            <asp:CheckBoxList ID="chkmastermenuallot" runat="server" CssClass="checkbox-inline" RepeatColumns="4" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                            </asp:CheckBoxList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3" id="divRole" visible="False" runat="server">
                                    <div class="fdi-add-content">
                                        <div class="form-group">

                                            <h3 class="secondary-heading">Role</h3>

                                            <asp:CheckBoxList ID="chkrole" runat="server" onclick="MutExChkList(this);" RepeatColumns="5" CssClass="checkbox-inline" RepeatDirection="Horizontal" RepeatLayout="Flow">
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
                                            <asp:Button ID="btncancel" runat="server" Text="Cancel" CssClass="btn btn-default pull-right" OnClick="btncancel_Click" />
                                            <asp:Button ID="btnsubmit" runat="server" Text="Save" CssClass="btn btn-primary pull-right" OnClick="btnsubmit_Click" OnClientClick="return Validate()" />
                                        </div>
                                    </div>
                                    <div>
                                    </div>
                                </div>
                            </div>
                            <div class="table-wraper">
                                <asp:GridView ID="gvcompanydetail" runat="server" Width="100%" Class="commonAjaxTbl master-company-table table display responsive no-wrap table-hover manage-user Grid" AutoGenerateColumns="false" AllowPaging="true"
                                    PageSize="25" AllowSorting="true">
                                    <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
                                    <Columns>

                                        <asp:TemplateField HeaderText="S.No">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="CompanyName" HeaderText="Company" />
                                        <asp:BoundField ItemStyle-Width="150px" DataField="CompanyRefNo" HeaderText="Refernce No." />
                                        <asp:BoundField ItemStyle-Width="150px" DataField="FactoryName" HeaderText="Division" />
                                        <asp:BoundField ItemStyle-Width="150px" DataField="FactoryRefNo" HeaderText="Refernce No." />
                                        <asp:BoundField ItemStyle-Width="150px" DataField="UnitName" HeaderText="Unit" />
                                        <asp:BoundField ItemStyle-Width="150px" DataField="UnitRefNo" HeaderText="Refernce No." />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>

            </div>


            <div class="clearfix"></div>
            <div class="footer"><i class="far fa-copyright"></i>2019 <a href="#">Department of Defence Production</a> </div>
        </div>
    </div>
</asp:Content>
