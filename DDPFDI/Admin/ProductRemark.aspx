<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" CodeFile="ProductRemark.aspx.cs" Inherits="Admin_ProductRemark" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="content oem-content">
        <asp:UpdatePanel runat="server" ID="updatepan" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="sideBg">
                    <div class="row">
                        <div class="col-md-12 padding_0">
                            <div id="divHeadPage" runat="server">
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="clearfix"></div>
                            <div style="margin-top: 5px;">
                                <a class="fa fa-arrow-circle-left pull-right" href="javascript: history.go(-1)">&nbsp; &nbsp;Back</a>
                            </div>
                            <div class="clearfix"></div>
                        </div>
                    </div>
                    <%--<div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                 <asp:HiddenField ID="hfcomprefno" runat="server" />
            <asp:HiddenField ID="hidCompanyRefNo" runat="server" />
            <asp:HiddenField ID="hidType" runat="server" />
                                <label>Select Company</label>
                                <asp:DropDownList runat="server" ID="ddlcompany" CssClass="form-control form-cascade-control" AutoPostBack="True" OnSelectedIndexChanged="ddlcompany_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3" runat="server" id="lblselectdivison">
                            <div class="form-group">
                                <label>Select Division/Plant</label>
                                <asp:DropDownList runat="server" ID="ddldivision" CssClass="form-control form-cascade-control" AutoPostBack="True" OnSelectedIndexChanged="ddldivision_OnSelectedIndexChanged"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3" runat="server" id="lblselectunit">
                            <div class="form-group">
                                <label>Select Unit</label>
                                <asp:DropDownList runat="server" ID="ddlunit" CssClass="form-control form-cascade-control" AutoPostBack="True" OnSelectedIndexChanged="ddlunit_OnSelectedIndexChanged"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-3">
                            <div class="form-group">
                                <label>Item Id (Portal)</label>
                                <asp:TextBox ID="txtsearchbyrefid" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtsearchbyrefid_TextChanged"></asp:TextBox>
                            </div>
                        </div>
                    </div>--%>
                    <div class="clearfix" style="margin-bottom: 10px;"></div>
                    <div class="addfdi">
                        <div class="section-pannel">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="fdi-add-content">
                                        <div class="table-wraper table-responsive">
                                            <asp:GridView ID="gvproductremark" runat="server" Width="100%" Class="commonAjaxTbl master-company-table table display 
                                responsive no-wrap table-hover manage-user Grid"
                                                AutoGenerateColumns="false" OnRowCreated="gvproductremark_RowCreated" OnRowCommand="gvproductremark_RowCommand">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No.">
                                                        <ItemTemplate>
                                                            <%#Container.DataItemIndex+1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Product Code">
                                                        <ItemTemplate>
                                                          <asp:Label ID="lblproductrefno" runat="server" Text='<%#Eval("ProductRefNo") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                 <%--   <asp:BoundField ItemStyle-Width="150px" DataField="ProductRefNo" HeaderText="Product Code" />--%>
                                                    <asp:BoundField ItemStyle-Width="150px" DataField="CompanyName" HeaderText="Company" />
                                                    <asp:BoundField ItemStyle-Width="150px" DataField="Unit" HeaderText="Unit" />
                                                    <asp:BoundField ItemStyle-Width="150px" DataField="Division" HeaderText="Division" />
                                                 
                                                  <%--  <asp:BoundField ItemStyle-Width="150px" DataField="Remark" HeaderText="Remark" />--%>
                                                     <asp:TemplateField HeaderText="Remark">
                                                        <ItemTemplate>
                                                          <asp:Label ID="lblremark" runat="server" Text='<%#Eval("Remark") %>'></asp:Label>
                                                      
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Action">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkreply" runat="server" Text="Reply" CommandName="Reply" CommandArgument='<%#Eval("RequestID") %>'></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                                   <asp:Label ID="lblemail" Visible="false" runat="server"></asp:Label>
                                                     <asp:Label ID="lblmobile" Visible="false" runat="server" ></asp:Label>
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="clearfix"></div>
                    </div>
                        <div class="clearfix"></div>
                    <div class="modal fade" id="reply">
                        <div class="modal-dialog modal-lg">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal">x</button>
                                    <h4 class="modal-title">Reply</h4>
                                </div>
                                <div class="modal-body">
                                    <form>
                                       <div class="form-group">
                                            <label for="usr"><b>Remark Ref No:</b></label>
                                           <%-- <asp:TextBox ID="txtrefno" class="form-control" name="refno" runat="server" ></asp:TextBox>--%>
                                           <asp:Label ID="lblrefno" runat="server"></asp:Label>
                                        </div>
                                        <div class="form-group">
                                            <label for="usr"><b>User Name:</b></label>
                                            <asp:TextBox ID="txtusername" class="form-control" placeholder="Your Name" name="username" runat="server" required></asp:TextBox>
                                        </div>
                                        <div>
                                            <label for="usremail"><b>User Email:</b></label>
                                            <asp:TextBox ID="txtemail" class="form-control" Visible="false"  placeholder="Email Id" name="email" TextMode="Email" runat="server"></asp:TextBox>
                                          
                                        </div>
                                        <div class="form-group">
                                            <label for="comment"><b>Remark:</b></label>
                                            <asp:TextBox runat="server" ID="txtremark" TextMode="MultiLine" ReadOnly="true" Rows="5" class="form-control" placeholder="Message" required></asp:TextBox>
                                        </div>

                                        <div class="form-group">
                                            <label for="comment"><b>Your Revert:</b></label>
                                            <asp:TextBox runat="server" ID="txtreply" TextMode="MultiLine" Rows="5" class="form-control" placeholder="Type Your Message" required></asp:TextBox>
                                        </div>

                                    </form>
                                </div>
                                <div class="modal-footer">
                                    <asp:Button ID="btnsubmit" Text="Send" class="btn btn-primary" runat="server" OnClick="btnsubmit_Click" />
                                </div>
                            </div>
                        </div>
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
    </div>
      <script type="text/javascript">
          function showPopup2() {
              $('#reply').modal('show');
          }
      </script>
</asp:Content>

