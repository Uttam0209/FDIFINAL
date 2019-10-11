<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPage.master" CodeFile="AddDesignation.aspx.cs" Inherits="Admin_AddDesignation" %>

<asp:Content ID="headDesignation" runat="server" ContentPlaceHolderID="head">
    <script>
        function ShowMessage() {
            console.log('testing');
            $("body").css('overflow', 'hidden');
            $('.alert-overlay-success').show();
        }
    </script>
</asp:Content>
<asp:Content ID="InnerDesignation" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div class="content oem-content">

        <asp:UpdatePanel runat="server" ID="updatepan">
            <ContentTemplate>
                <div class="sideBg">
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
                    <div class="addfdi">
                        <asp:Panel ID="Panel1" runat="server" DefaultButton="btnsubmit">
                            <div class="section-pannel">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group" runat="server" id="mastercompany">
                                            <asp:HiddenField ID="hdid" runat="server" />
                                            <asp:Label ID="lblMastcompany" runat="server" Text="" CssClass="form-label"></asp:Label>
                                            <asp:DropDownList runat="server" ID="ddlmaster" AutoPostBack="True" TabIndex="1" OnSelectedIndexChanged="ddlmaster_OnSelectedIndexChanged" CssClass="form-control form-cascade-control">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="fdi-add-content">
                                            <div id="Div1" runat="server">
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <label class=" control-label">Designation </label>
                                                            <span data-toggle="tooltip" class="fa fa-question" title="Please enter master designation of company designation also display in division/unit section.Before add designation please check company dropdown are selected with company name"></span>
                                                            <asp:TextBox ID="txtDesignation" runat="server" TabIndex="2" required="" class="form-control form-cascade-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="fdi-add-content">
                                        <div class="form-group">
                                            <asp:Button ID="btncancel" runat="server" Text="Cancel" Visible="False" TabIndex="4" CssClass="btn btn-default pull-right" OnClick="btncancel_Click" />
                                            <asp:Button ID="btnsubmit" runat="server" Text="Save" TabIndex="3" CssClass="btn btn-primary pull-right" OnClick="btnsubmit_Click" OnClientClick="return confirm('Are you sure you want to save this designation?');" />
                                        </div>
                                    </div>
                                    <div>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                        <div class="table-wraper table-responsive">
                            <asp:GridView ID="gvViewDesignationSave" runat="server" Width="100%" Class="commonAjaxTbl master-company-table table display 
                                responsive no-wrap table-hover manage-user Grid"
                                AutoGenerateColumns="false" OnRowCreated="gvViewDesignationSave_RowCreated">
                                <Columns>
                                    <asp:TemplateField HeaderText="S.No.">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Company">
                                        <ItemTemplate>
                                            <asp:Label ID="lblrefno" runat="server" Text='<%#Eval("CompanyName") %>' NullDisplayText="#" SortExpression="CompanyRefNo"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Designation" HeaderText="Designation" NullDisplayText="#" />
                                    <asp:BoundField DataField="DesignationRefNo" HeaderText="Reference No." Visible="False" NullDisplayText="#" />
                                    <asp:BoundField DataField="CreatedBy" HeaderText="Created By" NullDisplayText="#" />
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                    <div class="clearfix"></div>
                    <div class="footer"><i class="far fa-copyright"></i>2019 <a href="#">Department of Defence Production</a> </div>
                </div>
            </ContentTemplate>
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
    </div>
</asp:Content>
