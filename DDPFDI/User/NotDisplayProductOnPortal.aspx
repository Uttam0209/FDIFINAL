<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NotDisplayProductOnPortal.aspx.cs" Inherits="User_NotDisplayProductOnPortal" MasterPageFile="~/User/MasterPage.master" %>

<asp:Content ID="content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager runat="server" ID="sc"></asp:ScriptManager>
    <asp:UpdatePanel runat="server" ID="up" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="row d-flex justify-content-center">
                <div class="col-lg-11">
                    <div class="row d-flex">
                        <div class="col-sm-12">
                            <p>
                                &nbsp;Product not display due to 
                                              
                            </p>
                            <div class="row">
                                <div class="table table-responsive">
                                    <asp:GridView runat="server" ID="gvNotDisplayPortal" AutoGenerateColumns="false" class="table table-hover table-dark"
                                        OnRowDataBound="gvNotDisplayPortal_RowDataBound" OnRowCommand="gvNotDisplayPortal_RowCommand">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sr.No" HeaderStyle-Width="80px">
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkHeader" runat="server" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox runat="server" ID="chkRow" />&nbsp;&nbsp;
                                                <%#Eval("row_no") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Value below 10 thousend" HeaderStyle-Width="60px">
                                                <HeaderTemplate>
                                                    <asp:Label ID="Header4" data-toggle="tooltip" ToolTip="Product not display due to Value below 10 thousend" runat="server" Text="Value below 10 thousend"></asp:Label>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:LinkButton runat="server" ID="lbbelowa" CssClass="pull-right" Data-toggle="tooltip" ToolTip="Product not display due to Value below 10 thousend" Text="0" OnClick="lbbelow0_Click"></asp:LinkButton></h4>     
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Value is 0" HeaderStyle-Width="60px">
                                                <HeaderTemplate>
                                                    <asp:Label ID="Header5" data-toggle="tooltip" ToolTip="Product not display due to Value is 0" runat="server" Text="Value is 0"></asp:Label>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Button runat="server" ID="lbvaluea" CssClass="pull-right" data-toggle="tooltip"
                                                        ToolTip="Product not display due to Value is 0" Text="0" OnClick="lbvaluea_Click"></asp:Button></h4>
                             
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Year of Import below 2017-18" HeaderStyle-Width="60px">
                                                <HeaderTemplate>
                                                    <asp:Label ID="Header6" data-toggle="tooltip" ToolTip="Product not display due to Year of Import below 2017-18" runat="server" Text="Year of Import below 2017-18"></asp:Label>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:LinkButton runat="server" ID="lb17" data-toggle="tooltip" ToolTip="Product not display due to Year of Import below 2017-18" CssClass="pull-right" Text="0" OnClick="lb17_Click"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Year of Import not available" HeaderStyle-Width="60px">
                                                <HeaderTemplate>
                                                    <asp:Label ID="Header7" data-toggle="tooltip" ToolTip="Product not display due to <b>Year of Import not available" runat="server" Text="Year of Import not available"></asp:Label>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:LinkButton runat="server" ID="lbyrnotavailable" CssClass="pull-right" Text="0" data-toggle="tooltip" ToolTip="Product not display due to <b>Year of Import not available" OnClick="lbyrnotavailable_Click"></asp:LinkButton></h4>
                                       
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="displayed for general viewing (without registration) is No" HeaderStyle-Width="60px">
                                                <HeaderTemplate>
                                                    <asp:Label ID="Header8" data-toggle="tooltip" ToolTip="Product not displayed for general viewing (without registration) is No" runat="server" Text="displayed for general viewing (without registration) is No"></asp:Label>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:LinkButton runat="server" ID="lbeligible" CssClass="pull-right" data-toggle="tooltip" ToolTip="Product not displayed for general viewing (without registration) is No" Text="0" OnClick="lbeligible_Click"></asp:LinkButton></h4>
                                        
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="IsIndeginized No" HeaderStyle-Width="60px">
                                                <HeaderTemplate>
                                                    <asp:Label ID="Header9" data-toggle="tooltip" ToolTip="Product not display due to IsIndeginized No" runat="server" Text="IsIndeginized No"></asp:Label>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:LinkButton runat="server" ID="lbisindiginized" CssClass="pull-right" data-toggle="tooltip" ToolTip="Product not display due to IsIndeginized No" Text="0" OnClick="lbisindiginized_Click"> </asp:LinkButton>

                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Already Indeginized or ,(make in India Category = IGA,IN-House)" HeaderStyle-Width="60px">
                                                <HeaderTemplate>
                                                    <asp:Label ID="Header10" data-toggle="tooltip" ToolTip="Product display but not getting Interest from vendor due to <b>Already Indeginized or,(make in India Category = IGA,IN-House)" runat="server" Text="Already Indeginized or ,(make in India Category = IGA,IN-House)"></asp:Label>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:LinkButton runat="server" ID="lbigaviewonly" CssClass="pull-right" Text="0" OnClick="lbigaviewonly_Click"></asp:LinkButton>

                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <asp:LinkButton runat="server" ID="lbupdate" ForeColor="White" CommandArgument='<%#Eval("ProductRefNo") %>' CommandName="VEOI"><i class="fa fa-edit"></i>Update</asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>



                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                        <%--<div class="col-sm-12">
                            <div class="row">
                                <p style="color:red">Note&nbsp;-&nbsp;Product total count may be vary sum of total display details here.</p>
                                <div class="clearfix mt-1"></div>
                                <div class="bg-faded-dark bg-info col-sm-12">
                                    <p>
                                        1.&nbsp;Product not display due to <b>Value below 10 thousend</b>
                                               <h4>
                                                   <asp:LinkButton runat="server" ID="lbbelow0" CssClass="pull-right" Text="0" OnClick="lbbelow0_Click"></asp:LinkButton></h4>
                                    </p>
                                </div>
                                <div class="clearfix mt-1"></div>
                                <div class=" bg-faded-primary bg-info col-sm-12">
                                    <p>
                                        2.&nbsp;Product not display due to <b>Value is 0</b>
                                        <h4>
                                           
                                    </p>
                                </div>
                                <div class="clearfix mt-1"></div>
                                <div class=" bg-faded-accent bg-info col-sm-12">
                                    <p>
                                        3.&nbsp;Product not display due to <b>Year of Import below 2017-18</b>
                                        <span>
                                            <h4>
                                                </h4>
                                        </span>
                                    </p>
                                </div>
                                <div class="clearfix mt-1"></div>
                                <div class=" bg-faded-light bg-info col-sm-12">
                                    <p>
                                        4.&nbsp;Product not display due to <b>Year of Import not available</b> 
                                        <span>
                                            <h4>
                                                 </span>
                                    </p>
                                </div>
                                <div class="clearfix mt-1"></div>
                                <div class=" bg-faded-info bg-info col-sm-12">
                                    <p>
                                        5.&nbsp;Product not display due to is eligible to be <b>displayed for general viewing (without registration) is No</b>
                                        <span>
                                            <h4>
                                                </span>
                                    </p>
                                </div>
                                <div class="clearfix mt-1"></div>
                                <div class=" bg-faded-warning bg-info col-sm-12">
                                    <p>
                                        6.&nbsp;Product not display due to  <b>IsIndeginized No</b>

                                        <span>
                                            <h4>
                                               </h4>
                                        </span>
                                    </p>
                                </div>
                                <div class="clearfix mt-1"></div>
                                <div class=" bg-faded-success bg-info col-sm-12">
                                    <p>
                                        7.&nbsp;Product display but not getting Interest from vendor due to <b>Already Indeginized or ,
                                        (make in India Category = IGA,IN-House)</b>
                                        <span>
                                            <h4>
                                                  </span>
                                    </p>
                                </div>                              
                                <div class="clearfix mt-1"></div>
                            </div>
                        </div>--%>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
