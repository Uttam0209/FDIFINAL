<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ImportantNews.aspx.cs" Inherits="Admin_ImportantNews" MasterPageFile="~/Admin/MasterPage.master" %>

<asp:Content ID="head" runat="server" ContentPlaceHolderID="head"></asp:Content>
<asp:Content ID="inner" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div class="content oem-content">
        <div class="sideBg">
            <div class="row">
                <div class="col-md-12 padding_0">
                    <div id="divHeadPage" runat="server"></div>
                    <div id="ContentPlaceHolder1_divHeadPage">
                        <ul class="breadcrumb">
                            <li class=""><span>Important News</span></li>

                        </ul>
                    </div>
                </div>
                <div class="col-md-12">
                    <div class="clearfix"></div>
                    <div style="margin-top: 5px;">
                        <p runat="server" style="color: red;" class="pull-left">Note:- If any update are not displays in your browser please hard refresh your page using Ctrl+F5 or Shift+F5 or fn+F5</p>
                        <a class="fa fa-arrow-circle-left pull-right" href="javascript: history.go(-1)">&nbsp; &nbsp;Back</a>
                    </div>
                    <div class="clearfix"></div>
                </div>
            </div>
            <div class="clearfix" style="margin-bottom: 10px;"></div>
            <div class="row">
                <div class="col-md-12">
                    <asp:GridView ID="gv" runat="server" AutoGenerateColumns="false" Class="table table-responsive table-hover table-bordered manage-user">
                        <Columns>
                            <asp:TemplateField HeaderText="News">
                                <ItemTemplate>
                                    <i class="fa fa-hand-point-right" style="margin-right: 7px; color: #f00;"></i><%#Eval("News") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Date" DataFormatString = "{0:dd/MM/yyyy}" HeaderText="Updated Date" />
                            <asp:BoundField DataField="Pages" HeaderText="Pages" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>


