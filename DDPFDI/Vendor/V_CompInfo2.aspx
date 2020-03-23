<%@ Page Language="C#" AutoEventWireup="true" CodeFile="V_CompInfo2.aspx.cs" Inherits="Vendor_V_CompInfo2" MasterPageFile="~/Vendor/VendorMaster.master" %>

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
                    <div id="ocd" class="tab-pane">
                        <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                            <ContentTemplate>
                                <asp:Panel ID="Panel1" runat="server">
                                    <p>
                                        Name and Address of Product OEM
                                    </p>
                                    <p style="float: right; margin-right: 5px;"><span>Check upload file is selected or not before update/submit.</span></p>
                                    <asp:GridView ID="gvOEMNameadd" runat="server" CssClass="table table-hover" ShowFooter="true" CellPadding="4"
                                        ForeColor="#333333" GridLines="None" AutoGenerateColumns="false" OnRowCreated="gvOEMNameadd_RowCreated">
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:BoundField DataField="SNo" HeaderText="Raw Number" />
                                            <asp:TemplateField HeaderText="Name">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtmanofficename1" runat="server" CssClass="form-control"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="OEM">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="ddlOEM1" runat="server" CssClass="form-control">
                                                        <asp:ListItem>Indian</asp:ListItem>
                                                        <asp:ListItem>Foreign</asp:ListItem>
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Complete Address">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtCAddrssMF1" runat="server" CssClass="form-control"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Contact Official Name">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtofficialNameMF1" runat="server" CssClass="form-control"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Telephone No">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txttelephonenoMF1" runat="server" CssClass="form-control"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Fax No">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtfaxnoMF1" runat="server" CssClass="form-control"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Email Id">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtemailidMF1" runat="server" TextMode="Email" CssClass="form-control"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Authrization">
                                                <ItemTemplate>
                                                    <asp:FileUpload ID="fuAUTHRIZATION1" runat="server" CssClass="form-control" />
                                                    <asp:HiddenField ID="hfauth1" runat="server" />
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <FooterTemplate>
                                                    <asp:Button ID="btnoem" runat="server" Text="Add New Row" CssClass="btn btn-primary pull-right" OnClick="btnoem_Click" />
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbremoveoem" runat="server" CssClass="fa fa-times"
                                                        OnClick="lbremoveoem_Click"></asp:LinkButton>
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
                                    <asp:GridView ID="gveditoemnameadd" runat="server" CssClass="table table-hover" ShowFooter="true" CellPadding="4"
                                        ForeColor="#333333" GridLines="None" AutoGenerateColumns="false" OnRowCommand="gveditoemnameadd_RowCommand">
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sr.No">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="OEMName" HeaderText="Name" />
                                            <asp:BoundField DataField="OEMAddress" HeaderText="Complete Address" />
                                            <asp:BoundField DataField="OEMCountry" HeaderText="OEM Country" />
                                            <asp:BoundField DataField="OEMOfficialName" HeaderText="Contact Official Name" />
                                            <asp:BoundField DataField="OEMTelephoneNo" HeaderText="Telephone No" />
                                            <asp:BoundField DataField="OEMFaxNo" HeaderText="Fax No" />
                                            <asp:BoundField DataField="OEMEmailId" HeaderText="Email Id" />
                                            <asp:TemplateField HeaderText="File Authorization">
                                                <ItemTemplate>
                                                    <a href='<%#Eval("FileAuthorization","https://srijandefence.gov.in/Upload/VendorImage/{0}") %>' runat="server" id="img" target="_blank"><%#Eval("FileAuthorization") %></a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <asp:HiddenField runat="server" ID="hfeditoemname" Value='<%#Eval("MasterId") %>' />
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
                                    <asp:Button ID="btnsubmit" runat="server" Text="Submit" CssClass="btn btn-primary pull-right mr10" OnClick="btnsubmit_Click" />
                                </asp:Panel>
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="btnsubmit" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="divoem" role="dialog" data-keyboard="false" data-backdrop="static">
        <div class="modal-dialog" style="width: 400px;">
            <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                <ContentTemplate>
                    <div class="modal-content" runat="server" id="Div10">
                        <div class="modal-header modal-header1">
                            <button type="button" class="close close1" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Product OEM</h4>
                        </div>
                        <form class="form-horizontal changepassword" role="form">
                            <div class="modal-body clearfix" style="padding: 0 20px;">
                                <asp:HiddenField ID="HiddenField9" runat="server" />
                                <div class="form-group" style="margin: 0">
                                    <label for="uname" class=" tetLable">
                                        Name
                                    </label>
                                    <asp:TextBox runat="server" ID="txtname" placeholder="Name" Class="form-control"></asp:TextBox>
                                </div>
                                <div class="form-group" style="margin: 0">
                                    <label for="uname" class=" tetLable">
                                        Complete Address	
                                    </label>
                                    <asp:TextBox runat="server" ID="txtcompadd" placeholder="Complete Address" TextMode="MultiLine" Class="form-control"></asp:TextBox>
                                </div>
                                <div class="form-group" style="margin: 0">
                                    <label for="uname" class=" tetLable">
                                        OEM Country
                                    </label>
                                    <asp:DropDownList runat="server" ID="ddlcountry" TextMode="MultiLine" Class="form-control">
                                        <asp:ListItem>Indian</asp:ListItem>
                                        <asp:ListItem>Foreign</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group" style="margin: 0">
                                    <label for="uname" class=" tetLable">
                                        Contact Official Name
                                    </label>
                                    <asp:TextBox runat="server" ID="txtofficialname" placeholder="Contact Official Name" Class="form-control"></asp:TextBox>
                                </div>
                                <div class="form-group" style="margin: 0">
                                    <label for="uname" class=" tetLable">
                                        Telephone No
                                    </label>
                                    <asp:TextBox runat="server" ID="txttelephoneno" placeholder="Telephone No" onkeypress="return isNumberKey(event)" Class="form-control"></asp:TextBox>
                                </div>
                                <div class="form-group" style="margin: 0">
                                    <label for="uname" class=" tetLable">
                                        Fax No
                                    </label>
                                    <asp:TextBox runat="server" ID="txtfaxno" placeholder="Fax No" onkeypress="return isNumberKey(event)" Class="form-control"></asp:TextBox>
                                </div>
                                <div class="form-group" style="margin: 0">
                                    <label for="uname" class=" tetLable">
                                        Email Id
                                    </label>
                                    <asp:TextBox runat="server" ID="txtemailid" placeholder="Email Id" TextMode="Email" Class="form-control"></asp:TextBox>
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
                <Triggers>
                    <asp:PostBackTrigger ControlID="lbsub" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
    <script type="text/javascript">
        function showPopup() {
            $('#divoem').modal('show', function () {
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
