<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GAssignJob.aspx.cs" Inherits="Grievance_GAssignJob" MasterPageFile="~/Grievance/GMaster.master" %>

<asp:Content ID="head" runat="server" ContentPlaceHolderID="head"></asp:Content>
<asp:Content ID="inner" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <asp:ScriptManager runat="server" ID="sccc"></asp:ScriptManager>
    <asp:UpdatePanel ID="up" runat="server">
        <ContentTemplate>
            <div class="content oem-content">
                <div class="sideBg">
                    <div class="row">
                        <div class="col-md-12 padding_0">
                            <div id="divHeadPage" runat="server"></div>
                            <div id="ContentPlaceHolder1_divHeadPage">
                                <ul class="breadcrumb">
                                    <li class=""><span>Assign Tickets</span></li>
                                </ul>
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
                    <div class="clearfix" style="margin-bottom: 10px;"></div>
                    <div class="row">
                        <div class="col-md-12">
                            <asp:Label runat="server" ID="lbltotalcase" CssClass="label mr10 note-float-right label-info"></asp:Label>&nbsp;&nbsp;                         
                            <div class="clearfix mt10"></div>
                            <asp:DataList ID="dlassignjob" runat="server" RepeatColumns="1" RepeatLayout="Flow" RepeatDirection="Horizontal"
                                OnItemCommand="dlassignjob_ItemCommand" OnItemDataBound="dlassignjob_ItemDataBound">
                                <ItemTemplate>
                                    <div class="table-bordered">
                                        <div class="btn btn-primary btn-block text-white" style="padding: 5px; 5px; 5px; 5px;">
                                            <b><span style="float: left; margin-left: 5px;">&nbsp;Ticket No :&nbsp;
                                                <asp:Label runat="server" ID="ticketno" Text='<%#Eval("IssueRefno") %>'></asp:Label></span></b>
                                            <b><span style="text-align: center !important;">Ticket Issue Date :&nbsp;<b><%#Eval("RiseDate","{0:dd-MMM-yyyy}") %></b></span></b><b><span style="float: right; margin-right: 5px;">Ticket Status :&nbsp;<%#Eval("IsOpen") %></span></b>
                                        </div>
                                        <div class="clearfix"></div>
                                        <div class=" row">
                                            <div class="col-sm-6" style="border-right: 1px solid">
                                                <div class="modal-title bg-info text-center" style="padding: 2px; 2px; 2px; 2px;">
                                                    <h5>Ticket Issuer Detail</h5>
                                                </div>
                                                <hr />
                                                <asp:HiddenField runat="server" ID="hfemail" Value='<%#Eval("Email") %>' />
                                                <p>
                                                    Name&nbsp;-&nbsp;<b class="float-lg-right">
                                                        <asp:Label runat="server" Text='<%#Eval("Name") %>'></asp:Label></b>
                                                </p>
                                                <p>Document&nbsp;-&nbsp;<b class="float-lg-right"><a href='<%#Eval("Files") %>' target="_blank"></a></b></p>
                                            </div>
                                            <div class="col-sm-6">
                                                <div class="modal-title bg-info text-center" style="padding: 2px; 2px; 2px; 2px;">
                                                    <h5>Ticket Generate for</h5>
                                                </div>
                                                <hr />
                                                <p>
                                                    Request&nbsp;-&nbsp;<b class="float-lg-right">
                                                        <asp:Label runat="server" ID="lblqueryfor" Text='<%#Eval("QueryFor") %>'></asp:Label></b>
                                                </p>
                                                <p>
                                                    Subject&nbsp;-&nbsp;<b class="float-lg-right">
                                                        <asp:Label runat="server" ID="lbsub" Text='<%#Eval("SubjectName") %>'></asp:Label></b>
                                                </p>
                                                 <p>
                                                    Sub-Subject&nbsp;-&nbsp;<b class="float-lg-right">
                                                        <asp:Label runat="server" ID="Label1" Text='<%#Eval("SubSubjectName") %>'></asp:Label></b>
                                                </p>
                                                <p>
                                                    Issue&nbsp;-&nbsp;<b class="float-lg-right">
                                                        <asp:Label runat="server" ID="lbissue" Text='<%#Eval("Issue") %>'></asp:Label></b>
                                                </p>
                                            </div>

                                        </div>
                                        <div class="clearfix mt-1"></div>
                                        <div class="col-sm-12">
                                            <div class="table-responsive" style="overflow: scroll;">
                                                <asp:GridView ID="gvassignjob" runat="server" class="table table-hover table-condensed table-striped" AutoGenerateColumns="false">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Ticket Reply">
                                                            <ItemTemplate>
                                                                <%#Container.DataItemIndex+1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Ticket Reply">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" Text='<%#Eval("Comment") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Ticket Handle by">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" Text='<%#Eval("ELook") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="IsForword" HeaderText="Ticket forword" />
                                                        <asp:BoundField DataField="ForwordFrom" HeaderText="Ticket forword to" />
                                                        <asp:BoundField DataField="ReasoneofForword" HeaderText="Reasone to forword" />
                                                        <asp:BoundField DataField="ForwordDate" HeaderText="Ticket forword date" DataFormatString="{0:dd-MMM-yyyy}" />
                                                        <asp:BoundField DataField="IsClose" HeaderText="Ticket status" />
                                                        <asp:TemplateField HeaderText="Documnet">
                                                            <ItemTemplate>
                                                                <a runat="server" href='<%#Eval("CommentFile") %>' target="_blank"></a>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                        <div class="clearfix mt5"></div>
                                        <div>

                                            <asp:LinkButton runat="server" ID="lbupdatereview" CssClass="btn btn-primary btn-sm pull-right" CommandName="Reply"
                                                CommandArgument='<%#Eval("IssueRefno") %>'><i class="fa fa-reply"></i></asp:LinkButton>
                                            <asp:LinkButton runat="server" ID="lbforword" CssClass="btn btn-primary btn-sm pull-right mr5" Style="margin-right: 5px;" CommandName="Forword"
                                                CommandArgument='<%#Eval("IssueRefno") %>'><i class="fa fa-forward"></i></asp:LinkButton>
                                        </div>
                                        <div class="clearfix mt5"></div>

                                    </div>
                                </ItemTemplate>
                            </asp:DataList>
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal fade" id="modelforword" role="dialog" data-keyboard="false" data-backdrop="static">
                <div class="modal-dialog" style="width: 400px;">
                    <!-- Modal content-->
                    <div class="modal-content">
                        <div class="modal-header modal-header1">
                            <button type="button" class="close close1" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Forword to Other</h4>
                        </div>
                        <form class="form-horizontal changepassword" role="form">
                            <div class="modal-body clearfix" style="padding: 0 20px;">
                                <div class="form-group" style="margin: 0">
                                    <asp:HiddenField runat="server" ID="hfissuerefno" />
                                    <label for="uname" class=" tetLable">
                                        Assign to
                                    </label>
                                    <asp:DropDownList ID="ddlAssginto" runat="server" TabIndex="1" CssClass="form-control"></asp:DropDownList>
                                </div>
                                <div class="form-group" style="margin: 0">
                                    <label for="uname" class=" tetLable">
                                        Comment
                                    </label>
                                    <asp:TextBox runat="server" ID="txtcomment" CssClass="form-control" TabIndex="2" TextMode="MultiLine" Height="70px" Placeholder="Comment...."></asp:TextBox>
                                </div>
                                <div class="form-group" style="margin: 0">
                                    <asp:LinkButton runat="server" ID="btnassignto" TabIndex="3" CssClass="btn btn-primary pull-right forgot-pass-btn"
                                        ToolTip="Forword this case to other user." OnClick="btnassignto_Click"><i class="fa fa-forward">&nbsp;Assign</i></asp:LinkButton>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-default" tabindex="4" data-dismiss="modal">Close</button>
                            </div>
                        </form>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnassignto" />
        </Triggers>
    </asp:UpdatePanel>
    <script type="text/javascript">
        function showPopup() {
            $('#modelforword').modal('show');
        }
    </script>
</asp:Content>
