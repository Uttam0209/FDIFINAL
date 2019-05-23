<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPage.master" CodeFile="AddDesignation.aspx.cs" Inherits="Admin_AddDesignation" %>

<asp:Content ID="headDesignation" runat="server" ContentPlaceHolderID="head">
    <script>
        //Alert pop up box
        function ShowMessage() {
            console.log('testing');
            $("body").css('overflow', 'hidden');
            $('.alert-overlay-success').show();
        }
    </script>
</asp:Content>

<asp:Content ID="InnerDesignation" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div class="content oem-content">
        <asp:ScriptManager ID="sn" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel runat="server" ID="updatepan">
            <ContentTemplate>
                <div class="sideBg">
                    <div class="row">
                        <div class="col-md-12 padding_0">
                            <div id="divHeadPage" runat="server"></div>
                        </div>
                    </div>

                    <div class="addfdi">
                        <div class="section-pannel">
                            <div class="row">
                                <div class="col-md-12">
                                    <%-- <asp:UpdatePanel ID="upfdival" runat="server">
                                <ContentTemplate>--%>
                                    <div class="form-group" runat="server" id="mastercompany">
                                        <asp:HiddenField ID="hdid" runat="server" />
                                        <asp:Label ID="lblMastcompany" runat="server" Text="" CssClass="form-label"></asp:Label>
                                        <asp:DropDownList runat="server" ID="ddlmaster" AutoPostBack="True" OnSelectedIndexChanged="ddlmaster_OnSelectedIndexChanged" CssClass="form-control form-cascade-control">
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
                                                        <label class=" control-label">Designation </label><span data-toggle="tooltip" class="fa fa-question" title="Please enter master designation of company designation also display in division/unit section.Before add designation please check company dropdown are selected with company name"></span>
                                                        <asp:TextBox ID="txtDesignation" runat="server" required="" class="form-control form-cascade-control"></asp:TextBox>
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
                                        <asp:Button ID="btncancel" runat="server" Text="Cancel" CssClass="btn btn-default pull-right" OnClick="btncancel_Click" />
                                        <asp:Button ID="btnsubmit" runat="server" Text="Save" CssClass="btn btn-primary pull-right" OnClick="btnsubmit_Click" OnClientClick="return confirm('Are you sure you want to save this designation?');" />
                                    </div>
                                </div>
                                <div>
                                </div>
                            </div>
                        </div>
                        <div class="table-wraper">
                            <asp:GridView ID="gvViewDesignation" runat="server" Width="100%" Class="commonAjaxTbl master-company-table table display responsive no-wrap table-hover manage-user Grid" AutoGenerateColumns="false" AllowPaging="true"
                                PageSize="25" AllowSorting="true">
                                <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
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
                        <%--</ContentTemplate>
                            </asp:UpdatePanel>--%>
                    </div>
                    <div class="clearfix"></div>
                    <div class="footer"><i class="far fa-copyright"></i>2019 <a href="#">Department of Defence Production</a> </div>
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

<%--         <!-----Alert Box ------>
    <div runat="server" id="AlertSuccess" visible="false" class="alert-overlay alert-overlay-success">
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
               
        <div class="alert">
            Successfully Saved !
        </div>
        <button class="btn btn-success close_alert">OK</button>
    </div>
    </div>
   
</div>
   <!-----Alert Box ------>--%>
    </div>
</asp:Content>
