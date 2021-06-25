<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" CodeFile="CompaniesFeedback.aspx.cs" Inherits="Admin_CompaniesFeedback" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="content oem-content">
        <asp:UpdatePanel runat="server" ID="updatepan">
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

                    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.3/jquery.min.js"></script>
                    <script src="../assets/js/bootstrap.min.js"></script>



                    <div class="clearfix" style="margin-bottom: 10px;"></div>
                    <div class="addfdi">
                        <div class="section-pannel">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="fdi-add-content">
                                        <div class="table-wraper table-responsive">
                                            <asp:GridView ID="gvcompanyfeedback" runat="server" Width="100%" Class="commonAjaxTbl master-company-table table display 
                                               responsive no-wrap table-hover manage-user Grid"
                                                AutoGenerateColumns="false" OnRowCreated="gvcompanyfeedback_RowCreated" OnRowCommand="gvcompanyfeedback_RowCommand">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No.">
                                                        <ItemTemplate>
                                                            <%#Container.DataItemIndex+1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:BoundField ItemStyle-Width="150px" DataField="FeedBackRefNo" HeaderText="FeedBackRefNo" />
                                                    <asp:BoundField ItemStyle-Width="150px" DataField="UserName" HeaderText="User Name" />
                                                    <asp:BoundField ItemStyle-Width="150px" DataField="UserEmail" HeaderText="Email ID" />
                                                    <asp:BoundField ItemStyle-Width="150px" DataField="MobileNo" HeaderText="Contact No." />
                                                    <asp:BoundField ItemStyle-Width="150px" DataField="Message" HeaderText="Feedback" />
                                                    <asp:BoundField ItemStyle-Width="150px" DataField="CompanyName" HeaderText="Company" />
                                                    <asp:TemplateField HeaderText="Reply" HeaderStyle-Width="200px">
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="server" ID="lnkClick" CommandArgument='<%#Eval("FeedBackRefNo") %>' CommandName="Select" Text="Reply"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>

                                        </div>


                                    </div>

                                </div>
                            </div>
                        </div>
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
                                            <label for="usr"><b>Feedback Ref No:</b></label>
                                            <asp:TextBox ID="txtrefno" class="form-control" ReadOnly="true" name="refno" runat="server" ></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <label for="usr"><b>User Name:</b></label>
                                            <asp:TextBox ID="TxtBxFirstNm" class="form-control" ReadOnly="true" name="username" runat="server" required></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <label for="usremail"><b>User Email:</b></label>
                                            <asp:TextBox ID="Txtemail" class="form-control" ReadOnly="true" Visible="true" name="email" TextMode="Email" runat="server"></asp:TextBox>                                  
                                            <asp:TextBox ID="txtcompemail" Visible="false" class="form-control" placeholder="Company Email" name="comp email" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                             <label for="usremail"><b>Company Name:</b></label>
                                             <asp:TextBox ID="txtcompname" Visible="true" ReadOnly="true" class="form-control" name="comp name" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <label for="comment"><b>User Query:</b></label>
                                            <asp:TextBox runat="server" ID="txtusermsg" ReadOnly="true" TextMode="MultiLine" Rows="4" class="form-control"  required></asp:TextBox>
                                        </div>

                                        <div class="form-group">
                                            <label for="comment"><b>Your Revert:</b></label>
                                            <asp:TextBox runat="server" ID="TxtBxDesc" TextMode="MultiLine" Rows="5" class="form-control" placeholder="Type Your Message" required></asp:TextBox>
                                        </div>

                                    </form>
                                </div>
                                <div class="modal-footer">
                                    <asp:Button ID="btnclose" Text="Send" type="submit" class="btn btn-primary" OnClick="BtnSave_Click" runat="server" />
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

   <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.3/jquery.min.js"></script>
    <script src="../assets/js/bootstrap.min.js"></script>
     <script type="text/javascript">
         function showPopup() {
             $('#reply').modal('show');
         }
     </script>
</asp:Content>

