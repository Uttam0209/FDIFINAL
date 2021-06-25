<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ResetDuplicateInterest.aspx.cs" Inherits="Admin_ResetDuplicateInterest" MasterPageFile="~/Admin/MasterPage.master" %>

<asp:Content ID="headDesignation" runat="server" ContentPlaceHolderID="head">
</asp:Content>
<asp:Content ID="InnerDesignation" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <asp:UpdatePanel runat="server" ID="updatepan">
        <ContentTemplate>
            <div class="content oem-content">
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
                        <div class="table-wraper table-responsive">
                            <asp:GridView ID="gvViewDesignationSave" runat="server" Width="100%" Class="table table-responsive table-hover"
                                AutoGenerateColumns="false" OnRowCommand="gvViewDesignationSave_RowCommand">
                                <Columns>
                                    <asp:TemplateField HeaderText="S.No.">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="ProductRefNo" HeaderText="Product-Ref-No" NullDisplayText="#" />
                                    <asp:BoundField DataField="RequestID" HeaderText="RequestID" NullDisplayText="#" />
                                    <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            <asp:LinkButton runat="server" ID="lbledit"
                                                OnClientClick="return confirm('Are you sure you want to deactive this product?');" CommandArgument='<%#Eval("RequestId") %>' CommandName="PRef">UPDATE</asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                    <div class="clearfix"></div>
                </div>
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


</asp:Content>
