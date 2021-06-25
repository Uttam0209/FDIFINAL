<%@ Page Language="C#" AutoEventWireup="true" CodeFile="G_Dashboard.aspx.cs" Inherits="Grievance_G_Dashboard" MasterPageFile="~/Grievance/GMaster.master" %>

<asp:Content ID="conhead" runat="server" ContentPlaceHolderID="head">
</asp:Content>
<asp:Content ID="coninner" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <asp:ScriptManager runat="server" ID="sccc"></asp:ScriptManager>
    <asp:UpdatePanel ID="up" runat="server">
        <ContentTemplate>
            <div class="content oem-content">
                <div class="sideBg">
                    <div class="row">
                        <div class="col-mod-12 padding_0">
                            <ul class="breadcrumb">
                                <li>
                                    <asp:Label ID="lblPageName" runat="server" Text="Dashboard"></asp:Label>
                                </li>
                            </ul>
                        </div>
                    </div>
                    <form method="post" class="addfdi">
                        <div class="admin-dashboard">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="row">
                                        <div class="col-lg-3 col-sm-6 col-xs-12">
                                            <div class="white-box analytics-info total-comp">
                                                <ul class="list-inline two-part">
                                                    <li>
                                                        <div class="icon-box">
                                                            <i class="far fa-user"></i>
                                                        </div>

                                                        <div class="compName">
                                                            <h3 class="box-title">Total Team</h3>
                                                            <div class="Number">
                                                                <asp:LinkButton ID="lnkTotalTeam" CssClass="comp_number" runat="server" OnClick="lnkTotalTeam_Click" Text="0"></asp:LinkButton>
                                                            </div>
                                                        </div>
                                                    </li>
                                                </ul>
                                                <div class="file-export">
                                                    <asp:LinkButton ID="lbtotalteamexcel" Visible="true" runat="server" class="fas fa-cloud-download-alt" data-toggle="tooltip"
                                                        ToolTip="Export to Excel"></asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-3 col-sm-6 col-xs-12">
                                            <div class="white-box analytics-info total-fdi">
                                                <ul class="list-inline two-part">
                                                    <li>
                                                        <div class="icon-box">
                                                            <i class="fa fa-plus-circle"></i>
                                                        </div>
                                                        <div class="compName">
                                                            <h3 class="box-title">Total Ticket</h3>
                                                            <div class="Number">
                                                                <asp:LinkButton ID="lnkTotalCase" runat="server" CssClass="comp_number" OnClick="lnkTotalCase_Click" Text="0"></asp:LinkButton>
                                                            </div>
                                                        </div>
                                                    </li>
                                                </ul>
                                                <div class="file-export">
                                                    <asp:LinkButton ID="lbTotalCaseExcel" runat="server" Visible="true" class="fas fa-cloud-download-alt"
                                                        data-toggle="tooltip" ToolTip="Export to Excel"></asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-3 col-sm-6 col-xs-12">
                                            <div class="white-box analytics-info last-fdi">
                                                <ul class="list-inline two-part">
                                                    <li>
                                                        <div class="icon-box">
                                                            <i class="fa fa-exclamation"></i>
                                                        </div>
                                                        <div class="compName">
                                                            <h3 class="box-title">Total Issue/Complains</h3>
                                                            <div class="Number">
                                                                <asp:LinkButton ID="lnkTotalIssue" runat="server" CssClass="comp_number" OnClick="lnkTotalIssue_Click" Text="0"></asp:LinkButton>
                                                            </div>
                                                        </div>
                                                    </li>
                                                </ul>
                                                <div class="file-export">
                                                    <asp:LinkButton ID="lbTotalIssueExcel" runat="server" Visible="true" class="fas fa-cloud-download-alt"
                                                        data-toggle="tooltip" ToolTip="Export to Excel"></asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-3 col-sm-6 col-xs-12">
                                            <div class="white-box analytics-info last-fdi">
                                                <ul class="list-inline two-part">
                                                    <li>
                                                        <div class="icon-box">
                                                            <i class="fa fa-comment" aria-hidden="true"></i>
                                                        </div>
                                                        <div class="compName">
                                                            <h3 class="box-title">Total FeedBack</h3>
                                                            <div class="Number">
                                                                <asp:LinkButton ID="lnkFeedback" runat="server" CssClass="comp_number" OnClick="lnkFeedback_Click" Text="0"></asp:LinkButton>
                                                            </div>
                                                        </div>
                                                    </li>
                                                </ul>
                                                <div class="file-export">
                                                    <asp:LinkButton ID="lnkFeedBackExcel" runat="server" Visible="true" class="fas fa-cloud-download-alt"
                                                        data-toggle="tooltip" ToolTip="Export to Excel"></asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="clearfix"></div>
                                        <br />
                                        <div class="col-lg-4 col-sm-6 col-xs-12">
                                            <div class="white-box analytics-info total-fdi">
                                                <ul class="list-inline two-part">
                                                    <li>
                                                        <div class="icon-box">
                                                            <i class="far fa-thumbs-up"></i>
                                                        </div>
                                                        <div class="compName">
                                                            <h3 class="box-title">ticket genrate</h3>
                                                            <div class="Number">
                                                                <asp:LinkButton ID="lnkResolvedIssue" runat="server" CssClass="comp_number" OnClick="lnkResolvedIssue_Click" Text="0"></asp:LinkButton>
                                                            </div>
                                                        </div>
                                                    </li>
                                                </ul>
                                                <div class="file-export">
                                                    <asp:LinkButton ID="lbResolvedIssueExcel" runat="server" Visible="true" class="fas fa-cloud-download-alt"
                                                        data-toggle="tooltip" ToolTip="Export to Excel"></asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-sm-6 col-xs-12">
                                            <div class="white-box analytics-info last-fdi">
                                                <ul class="list-inline two-part">
                                                    <li>
                                                        <div class="icon-box">
                                                            <i class="fa fa-spinner"></i>
                                                        </div>
                                                        <div class="compName">
                                                            <h3 class="box-title">ticket In Progress</h3>
                                                            <div class="Number">
                                                                <asp:LinkButton ID="lnkIssueInProgress" runat="server" CssClass="comp_number" OnClick="lnkIssueInProgress_Click" Text="0"></asp:LinkButton>
                                                            </div>
                                                        </div>
                                                    </li>
                                                </ul>
                                                <div class="file-export">
                                                    <asp:LinkButton ID="lbIssueInProgressExcel" runat="server" Visible="true" class="fas fa-cloud-download-alt"
                                                        data-toggle="tooltip" ToolTip="Export to Excel"></asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-sm-6 col-xs-12">
                                            <div class="white-box analytics-info last-fdi">
                                                <ul class="list-inline two-part">
                                                    <li>
                                                        <div class="icon-box">
                                                            <i class="fa fa-trash" aria-hidden="true"></i>
                                                        </div>
                                                        <div class="compName">
                                                            <h3 class="box-title"> Close TICKET</h3>
                                                            <div class="Number">
                                                                <asp:LinkButton ID="lnkClose" runat="server" CssClass="comp_number" OnClick="lnkClose_Click" Text="0"></asp:LinkButton>
                                                            </div>
                                                        </div>
                                                    </li>
                                                </ul>
                                                <div class="file-export">
                                                    <asp:LinkButton ID="lbIssuecloseExcel" runat="server" Visible="true" class="fas fa-cloud-download-alt"
                                                        data-toggle="tooltip" ToolTip="Export to Excel"></asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="clearfix" style="margin-top: 10px;"></div>

                                    <div class="row">
                                        <div class="col-lg-4 col-sm-6 col-xs-12">
                                            <div class="white-box analytics-info last-fdi">
                                                <ul class="list-inline two-part">
                                                    <li>
                                                        <div class="icon-box">
                                                            <i class="fa fa-users"></i>
                                                        </div>
                                                        <div class="compName">
                                                            <h3 class="box-title">Case On HelpDesk</h3>
                                                            <div class="Number">
                                                                <asp:LinkButton ID="lblcaseonhelpdesk" runat="server" CssClass="comp_number" OnClick="lblcaseonhelpdesk_Click" Text="0"></asp:LinkButton>
                                                            </div>
                                                        </div>
                                                    </li>
                                                </ul>
                                                <div class="file-export">
                                                    <asp:LinkButton ID="LinkButton2" runat="server" Visible="true" class="fas fa-cloud-download-alt"
                                                        data-toggle="tooltip" ToolTip="Export to Excel"></asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-sm-6 col-xs-12">
                                            <div class="white-box analytics-info last-fdi">
                                                <ul class="list-inline two-part">
                                                    <li>
                                                        <div class="icon-box">
                                                            <i class="fa fa-users" aria-hidden="true"></i>
                                                        </div>
                                                        <div class="compName">
                                                            <h3 class="box-title">Case On Developer</h3>
                                                            <div class="Number">
                                                                <asp:LinkButton ID="lbondeveloper" runat="server" CssClass="comp_number" OnClick="lbondeveloper_Click" Text="0"></asp:LinkButton>
                                                            </div>
                                                        </div>
                                                    </li>
                                                </ul>
                                                <div class="file-export">
                                                    <asp:LinkButton ID="LinkButton4" runat="server" Visible="true" class="fas fa-cloud-download-alt"
                                                        data-toggle="tooltip" ToolTip="Export to Excel"></asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-sm-6 col-xs-12">
                                            <div class="white-box analytics-info last-fdi">
                                                <ul class="list-inline two-part">
                                                    <li>
                                                        <div class="icon-box">
                                                            <i class="fa fa-users" aria-hidden="true"></i>
                                                        </div>
                                                        <div class="compName">
                                                            <h3 class="box-title">Case On Manager</h3>
                                                            <div class="Number">
                                                                <asp:LinkButton ID="lbonmanager" runat="server" CssClass="comp_number" OnClick="lbonmanager_Click" Text="0"></asp:LinkButton>
                                                            </div>
                                                        </div>
                                                    </li>
                                                </ul>
                                                <div class="file-export">
                                                    <asp:LinkButton ID="LinkButton6" runat="server" Visible="true" class="fas fa-cloud-download-alt"
                                                        data-toggle="tooltip" ToolTip="Export to Excel"></asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="clearfix" style="margin-top: 10px;"></div>
                                    <div class="row">
                                        <div class="col-sm-4">
                                            <asp:Label runat="server" ID="lblclosetoday" CssClass="btn btn-success btn-block btn-sm"></asp:Label>
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:Label runat="server" ID="lblopentoday" CssClass="btn btn-danger btn-block btn-sm"></asp:Label>
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:Label runat="server" ID="lblinprogresstoday" CssClass="btn btn-warning btn-block btn-sm"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="clearfix" style="margin-top: 10px;"></div>
                                    <div class="row">
                                        <asp:LinkButton runat="server" ID="lbMore" ToolTip="Show all details of Active/Close FeedBack or Issue" data-toggle="tooltip" data-placement="top" Style="margin-right: 15px;" CssClass="btn btn-danger pull-right mt10" OnClick="lbMore_Click"><i class="fa fa-eye"></i>View More</asp:LinkButton>
                                        <div class="table-responsive">
                                            <asp:GridView runat="server" ID="gvcase" class="table table-hover table-bordered" Width="100%" AutoGenerateColumns="False" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4">
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <%#Container.DataItemIndex+1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="IssueRefNo" HeaderText="Ticket No" NullDisplayText="#" />
                                                    <asp:BoundField DataField="Name" HeaderText="Name" NullDisplayText="#" />
                                                    <asp:BoundField DataField="State" HeaderText="State" NullDisplayText="#" />
                                                    <asp:BoundField DataField="RiseDate" HeaderText="Date" DataFormatString="{0:dd-MMM-yyyy}" NullDisplayText="#" />
                                                    <asp:BoundField DataField="QueryFor" HeaderText="Query for" NullDisplayText="#" />
                                                    <asp:BoundField DataField="Subject" HeaderText="Subject" NullDisplayText="#" />
                                                    <asp:BoundField DataField="Issue" HeaderText="Issue/FeedBack" NullDisplayText="#" />
                                                    <asp:BoundField DataField="IsOpen" HeaderText="Status" NullDisplayText="#" />
                                                </Columns>
                                                <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                                                <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" />
                                                <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                                                <RowStyle BackColor="White" ForeColor="#330099" />
                                                <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                                                <SortedAscendingCellStyle BackColor="#FEFCEB" />
                                                <SortedAscendingHeaderStyle BackColor="#AF0101" />
                                                <SortedDescendingCellStyle BackColor="#F6F0C0" />
                                                <SortedDescendingHeaderStyle BackColor="#7E0000" />
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </form>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="up">
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
