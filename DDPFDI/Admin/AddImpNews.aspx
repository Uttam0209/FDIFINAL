<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddImpNews.aspx.cs" Inherits="Admin_AddImpNews" MasterPageFile="~/Admin/MasterPage.master" %>

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
        <asp:ScriptManager ID="sn" runat="server"></asp:ScriptManager>
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
                                        <div class="fdi-add-content">
                                            <div class="row">
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <label class=" control-label">Add News </label>
                                                        <span data-toggle="tooltip" class="fa fa-question" title="Add news of latest update on website"></span>
                                                        <asp:TextBox ID="txtnews" runat="server" TabIndex="1" TextMode="MultiLine" Height="110px" required="" class="form-control form-cascade-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <label class=" control-label">Pages</label>
                                                        <span data-toggle="tooltip" class="fa fa-question" title="enter page name of updated"></span>
                                                        <asp:TextBox ID="txtpages" runat="server" TabIndex="2" required="" class="form-control form-cascade-control"></asp:TextBox>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class=" control-label">Date</label>
                                                        <span data-toggle="tooltip" class="fa fa-question" title="enter date of apply updated"></span>
                                                        <asp:TextBox ID="txtdate" runat="server" TabIndex="3" required="" type="date" class="form-control form-cascade-control"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="col-md-12">
                                                    <div class="form-group">
                                                        <asp:Button ID="btnsubmit" runat="server" TabIndex="4" Text="Save" CssClass="btn btn-primary pull-right" OnClick="btnsubmit_Click" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </asp:Panel>
                        <div class="table-wraper table-responsive">
                            <asp:GridView ID="gvnewsadd" runat="server" Width="100%" Class="commonAjaxTbl master-company-table table display 
                                responsive no-wrap table-hover manage-user Grid"
                                AutoGenerateColumns="false" OnRowCreated="gvnewsadd_RowCreated">
                                <Columns>
                                    <asp:TemplateField HeaderText="S.No.">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="News">
                                        <ItemTemplate>
                                            <asp:Label ID="lblrefno" runat="server" Text='<%#Eval("News") %>' NullDisplayText="#"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Date" HeaderText="Update Date" DataFormatString = "{0:dd/MM/yyyy}" NullDisplayText="#" />
                                    <asp:BoundField DataField="Pages" HeaderText="Update Pages" Visible="False" NullDisplayText="#" />
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
