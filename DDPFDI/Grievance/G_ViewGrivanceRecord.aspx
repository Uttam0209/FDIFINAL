<%@ Page Language="C#" AutoEventWireup="true" CodeFile="G_ViewGrivanceRecord.aspx.cs" Inherits="Grievance_G_ViewGrivanceRecord" MasterPageFile="~/Grievance/GMaster.master" %>

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
                                    <li class=""><span>View Tickets</span></li>

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
                            <asp:Panel runat="server" ID="pansearch" CssClass="panel-success panel-title panel-group panel-wysiwyg">
                                <p style="padding-top: 5px; padding-left: 5px;"><b>Search Criteria</b></p>
                                <div class="col-sm-2">
                                    <div class="form-group">
                                        <label>Month</label>
                                        <asp:DropDownList runat="server" ID="ddlmonth" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-2">
                                    <div class="form-group">
                                        <label>Year</label>
                                        <asp:DropDownList runat="server" ID="ddlyear" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-2">
                                    <div class="form-group">
                                        <label>By Type</label>
                                        <asp:DropDownList runat="server" ID="ddltype" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-2">
                                    <div class="form-group">
                                        <label>Status</label>
                                        <asp:DropDownList runat="server" ID="ddlstatus" CssClass="form-control">
                                            <asp:ListItem Value="Y">Open</asp:ListItem>
                                            <asp:ListItem Value="N">Close</asp:ListItem>
                                            <asp:ListItem Value="P">In-Progress</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-2">
                                    <div class="form-group">
                                        <label>Portal</label>
                                        <asp:DropDownList runat="server" ID="ddlportal" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-2">
                                    <div class="form-group">
                                        <asp:LinkButton runat="server" ID="btnsearch" CssClass="btn btn-primary btn-block btn-sm" Style="margin-top: 22px;" Height="35px" OnClick="btnsearch_Click"><i class="fa fa-search"></i>&nbsp;Search</asp:LinkButton>
                                    </div>
                                </div>
                                <div class="clearfix" style="margin-top: 10px;"></div>
                            </asp:Panel>
                            <div class="table-responsive">
                                <div class="row">
                                    <div id="col1" class="col-sm-6">
                                        <h6>
                                            <asp:Label ID="lbltotal" runat="server"></asp:Label></h6>
                                    </div>
                                    <div id="col2" class="col-sm-6">
                                        <h6>
                                            <asp:Label ID="lbltotalleft" runat="server" CssClass="pull-right mr5"></asp:Label></h6>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="col-sm-2">
                                        </div>
                                        <div style="text-align: center;" class="col-sm-8">
                                            <span class="btn btn-primary btn-sm">
                                                <asp:LinkButton ID="LinkButton1" runat="server" ForeColor="White" OnClick="lnkbtnPgPrevious_Click">
                                        <i class="fa fa-chevron-left"></i>&nbsp;Prev</asp:LinkButton>
                                            </span>
                                            <span class="btn btn-info btn-sm">Showing
                                            <asp:Label runat="server" ID="lbltotalshowpageitem"></asp:Label>
                                                products of
                                            <asp:Label ID="lbltotfilter" runat="server"></asp:Label>
                                                products  </span>
                                            <span class="btn btn-primary btn-sm">
                                                <asp:LinkButton ID="LinkButton2" runat="server" ForeColor="White" Style="float: right;" OnClick="lnkbtnPgNext_Click">
                                          Next&nbsp;<i class="fa fa-chevron-right"></i></asp:LinkButton>
                                            </span>
                                        </div>
                                        <div class="col-sm-2">
                                        </div>
                                    </div>
                                </div>
                                <div class="clearfix" style="margin-top: 10px;"></div>
                                <div class="row">
                                    <div class="col-sm-6"></div>
                                    <div class="col-sm-2">
                                        <asp:LinkButton runat="server" ID="btnExcel" class="btn btn-primary btn-sm btn-block" Height="35px" OnClick="btnExcel_Click"><i class="fa fa-file-excel"></i>&nbsp;Download Excel</asp:LinkButton>
                                    </div>
                                    <div class="col-sm-2">
                                        <asp:DropDownList ID="ddlpaging" runat="server" CssClass="form-control filter_btn" AutoPostBack="true" OnSelectedIndexChanged="ddlpaging_SelectedIndexChanged">
                                            <asp:ListItem Value="50">50</asp:ListItem>
                                            <asp:ListItem Value="100">100</asp:ListItem>
                                            <asp:ListItem Value="150">150</asp:ListItem>
                                            <asp:ListItem Value="500">500</asp:ListItem>
                                            <asp:ListItem Value="1000">1000</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-2">
                                        <asp:DropDownList ID="ddlsort" runat="server" CssClass="form-control filter_btn" AutoPostBack="true" OnSelectedIndexChanged="ddlsort_SelectedIndexChanged">
                                            <asp:ListItem Value="Sort by">Sort by</asp:ListItem>
                                            <asp:ListItem Value="Asc">ASC</asp:ListItem>
                                            <asp:ListItem Value="Desc">DESC</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="clearfix" style="margin-top: 10px;"></div>
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
                                <div class="clearfix" style="margin-top: 10px;"></div>
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="col-sm-2">
                                        </div>
                                        <div class="col-sm-8" style="text-align: center;">
                                            <span class="btn btn-primary btn-sm">
                                                <asp:LinkButton ID="lnkbtnPgPrevious" runat="server" ForeColor="White" OnClick="lnkbtnPgPrevious_Click"><i class="fa fa-chevron-left mr-2"></i>&nbsp;Prev</asp:LinkButton>
                                            </span>
                                            <span class="btn btn-info btn-sm">
                                                <asp:Label ID="lblpaging" runat="server"></asp:Label>
                                            </span>
                                            <span class="btn btn-primary btn-sm">
                                                <asp:LinkButton ID="lnkbtnPgNext" runat="server" ForeColor="White" OnClick="lnkbtnPgNext_Click">
                            Next&nbsp;<i class="fa fa-chevron-right ml-2"></i></asp:LinkButton>
                                            </span>
                                        </div>
                                        <div class="col-sm-2">
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExcel" />
        </Triggers>
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
