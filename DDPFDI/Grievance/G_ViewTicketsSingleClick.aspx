<%@ Page Language="C#" AutoEventWireup="true" CodeFile="G_ViewTicketsSingleClick.aspx.cs" Inherits="Grievance_G_ViewTicketsSingleClick" MasterPageFile="~/Grievance/GMaster.master" %>

<asp:Content ID="conhead" runat="server" ContentPlaceHolderID="head">
</asp:Content>
<asp:Content ID="coninner" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div class="content oem-content">
        <div class="sideBg">
            <div class="row">
                <div class="col-mod-12 padding_0">
                    <ul class="breadcrumb">
                        <li>
                            <asp:Label ID="lblPageName" runat="server" Text="View Tickets"></asp:Label>
                        </li>
                    </ul>
                </div>
            </div>
            <div class="addfdi">
                <div class="admin-dashboard">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="col-sm-8">
                                <label title="Reference no you will get on your email when you rise your issue">Ticket No.</label>
                                <asp:TextBox runat="server" ID="txtrefeno" placeholder="Your Ticket No." TabIndex="1" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-sm-3">
                                <asp:LinkButton ID="lbstatus" runat="server" Style="margin-top: 20px;" CssClass="btn btn-primary btn-block btn-shadow pull-right" TabIndex="2" Text="Submit"
                                    ToolTip="Wait till we retrive your ticket" OnClick="lbstatus_Click"></asp:LinkButton>
                            </div>
                            <div class="clearfix" style="margin-top: 20px;">
                            </div>
                            <div class="col-sm-12">
                                <asp:DataList runat="server" ID="dlissue" RepeatColumns="1" RepeatLayout="Flow" RepeatDirection="Horizontal">
                                    <ItemTemplate>
                                        <div class="table-bordered">
                                            <div class="bg-dark text-white" style="padding: 5px; 5px; 5px; 5px;">
                                                <b><span style="float: left; margin-left: 5px;">&nbsp;Ticket No :&nbsp;<b>
                                                    <asp:Label runat="server" ID="ticketno" Text='<%#Eval("IssueRefno") %>'></asp:Label></b></span></b>
                                                <b><span style="text-align: center !important;">Ticket Issue Date :&nbsp;<b><%#Eval("RiseDate","{0:dd-MMM-yyyy}") %>&nbsp;Time :&nbsp;<%#Eval("RiseTime") %></b></span></b>
                                                <b><span style="float: right; margin-right: 5px;">Ticket Status :&nbsp;<b><%#Eval("IsOpen") %></b></span></b>
                                            </div>
                                            <div class="clearfix"></div>
                                            <div class="col-sm-12 row">
                                                <div class="col-sm-6" style="border-right: 1px solid">
                                                    <div class="modal-title bg-info text-center" style="padding: 5px; 5px; 5px; 5px;">
                                                        <h5>Your Detail</h5>
                                                    </div>
                                                    <hr />
                                                    <p>Ticket Rise For&nbsp;-&nbsp;<b class="float-lg-right"><%#Eval("HFrom") %></b></p>
                                                    <p>Purpose&nbsp;-&nbsp;<b class="float-lg-right"><%#Eval("QueryFor") %></b></p>
                                                    <p>Name&nbsp;-&nbsp;<b class="float-lg-right"><%#Eval("Name") %></b></p>
                                                    <p>Email&nbsp;-&nbsp;<b class="float-lg-right"><%#Eval("Email") %></b></p>
                                                    <p>MobileNo&nbsp;-&nbsp;<b class="float-lg-right"><%#Eval("MobileNo") %></b></p>
                                                    <p>State&nbsp;-&nbsp;<b class="float-lg-right"><%#Eval("State") %></b></p>
                                                    <p>Address&nbsp;-&nbsp;<b class="float-lg-right"><%#Eval("Address") %></b></p>
                                                </div>
                                                <div class="col-sm-6">
                                                    <div class="modal-title bg-info text-center" style="padding: 5px; 5px; 5px; 5px;">
                                                        <h5>Ticket Generate for Details</h5>
                                                    </div>
                                                    <hr />
                                                    <p>Subject&nbsp;-&nbsp;<b class="float-lg-right"><%#Eval("Subject") %></b></p>
                                                    <p>Sub-Subject&nbsp;-&nbsp;<b class="float-lg-right"><%#Eval("SubSubjectName") %></b></p>
                                                    <p>Issue&nbsp;-&nbsp;<b class="float-lg-right"><%#Eval("Issue") %></b></p>
                                                    <asp:Panel runat="server" ID="closedetail">
                                                        <p>Close-By&nbsp;-&nbsp;<b class="float-lg-right"><%#Eval("IsCloseBy") %></b></p>
                                                        <p>Close Date&nbsp;-&nbsp;<b class="float-lg-right"><%#Eval("IsCloseDate","{0:dd-MMM-yyyy}") %></b></p>
                                                        <p>Close Reasone&nbsp;-&nbsp;<b class="float-lg-right"><%#Eval("CloseReasone") %></b></p>
                                                    </asp:Panel>
                                                </div>
                                                <div class="clearfix mt-1"></div>
                                                <div class="col-sm-12"><a href='<%#Eval("Files") %>' target="_blank" class="fa fa-download fa-pull-right mr-1"></a></div>
                                                <div class="clearfix mt-1"></div>
                                            </div>
                                            <div class="clearfix mt-1"></div>

                                        </div>
                                    </ItemTemplate>
                                </asp:DataList>
                                <br />
                                <div class="card card-timeline px-2 border-none" runat="server" visible="false" id="divtrack">
                                    <ul class="bs4-order-tracking">
                                        <li runat="server" id="step1">
                                            <div><i class="fas fa-ticket-alt"></i></div>
                                            Tickets raises</li>

                                        <li runat="server" id="step2">
                                            <div><i class="fas fa-laptop-house"></i></div>
                                            Ticket assigned</li>

                                        <li runat="server" id="step3">
                                            <div><i class="fas fa-spinner"></i></div>
                                            Ticket in progress</li>

                                        <li runat="server" id="step4">
                                            <div><i class="fas fa-check-double"></i></div>
                                            Ticket resolved</li>
                                    </ul>
                                </div>
                                <div class="clearfix mt-1"></div>
                            </div>
                            <div class="clearfix mt-1"></div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
