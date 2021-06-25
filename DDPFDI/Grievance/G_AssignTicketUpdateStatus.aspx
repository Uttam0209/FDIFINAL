<%@ Page Language="C#" AutoEventWireup="true" CodeFile="G_AssignTicketUpdateStatus.aspx.cs" Inherits="Grievance_G_AssignTicketUpdateStatus" MasterPageFile="~/Grievance/GMaster.master" %>

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
                                    <li class=""><span>Update Reply On Assign Ticket</span></li>
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
                    <div class="col-md-12">
                        <div class="row">
                            <h4>Your Revert </h4>
                            <asp:HiddenField runat="server" ID="hfotp" />
                            <asp:HiddenField runat="server" ID="hfemail" />
                            <div runat="server" id="divupdate">
                                <div class="col-sm-6">
                                    <div class="form-group" style="margin: 0">
                                        <label for="uname" class=" tetLable">
                                            Subject
                                        </label>
                                        <asp:TextBox runat="server" ID="txtsubject" Enabled="false" CssClass="form-control" TabIndex="1" Placeholder="Subject...."></asp:TextBox>
                                    </div>
                                    <div class="form-group" style="margin: 0">
                                        <label for="uname" class=" tetLable">
                                            Query/FeedBack
                                        </label>
                                        <asp:TextBox runat="server" ID="txtquery" Enabled="false" CssClass="form-control" TabIndex="2" TextMode="MultiLine" Height="70px" Placeholder="Comment...."></asp:TextBox>
                                    </div>
                                    <div class="form-group" style="margin: 0">
                                        <label for="uname" class=" tetLable">
                                            Update Status
                                        </label>
                                        <asp:DropDownList ID="ddlticketstatus" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group" style="margin: 0">
                                        <label for="uname" class=" tetLable">
                                            Reply
                                        </label>
                                        <asp:TextBox runat="server" ID="txtreply" CssClass="form-control" TabIndex="3" TextMode="MultiLine" Height="70px" Placeholder="Reply...."></asp:TextBox>
                                    </div>
                                    <div class="form-group" style="margin: 0">
                                        <label for="uname" class=" tetLable">
                                            File <span class="alert danger">(if any less then 1mb only pdf or word allowed.)</span>
                                        </label>
                                        <asp:FileUpload runat="server" ID="fufile" TabIndex="4" CssClass="form-control" />
                                        <asp:HiddenField ID="hfimage" runat="server" />
                                    </div>
                                    <div class="form-group" style="margin: 0">
                                        <label for="uname" class=" tetLable">
                                            Is Ticket Close
                                        </label>
                                        <asp:RadioButtonList runat="server" ID="chkstatus" RepeatColumns="1" RepeatDirection="Vertical" RepeatLayout="Flow" TabIndex="5" CssClass="radio">
                                            <asp:ListItem Value="Y">Yes</asp:ListItem>
                                            <asp:ListItem Value="N">No</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                    
                                </div>
                                <div class="form-group" style="margin: 0">
                                    <asp:HiddenField runat="server" ID="hfticketclose" />
                                    <asp:LinkButton runat="server" ID="lbupdate" TabIndex="6" CssClass="btn btn-primary pull-right forgot-pass-btn"
                                        ToolTip="Forword this case to other user." OnClick="lbupdate_Click"><i class="fa fa-forward">&nbsp;Reply</i></asp:LinkButton>
                                </div>
                            </div>
                            <div runat="server" id="divotp">
                                <h4>Enter OTP</h4>
                                <div class="col-sm-10">
                                    <div class="form-group" style="margin: 0">
                                        <label for="uname" class=" tetLable">
                                            Close Ticket No <span class="danger alert">(enter close ticket no that you will recived on your registerd email)</span>
                                        </label>
                                        <asp:TextBox ID="txtotp" runat="server" TabIndex="1" CssClass="form-control" Placeholder="6 digit close ticket no"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-2">
                                    <div class="form-group" style="margin: 35px; 0px; 0px; 0px;">
                                        <asp:LinkButton runat="server" ID="btnotp" CssClass="btn btn-primary btn-block btn-sm pull-right" OnClick="btnotp_Click"><i class="fa fa-ticket-alt"></i>&nbsp;Close Ticket</asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="lbupdate" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
