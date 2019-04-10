<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Dashboard.aspx.cs" Inherits="Admin_Dashboard" MasterPageFile="~/Admin/MasterPage.master" %>

<asp:Content ID="conhead" runat="server" ContentPlaceHolderID="head">
</asp:Content>
<asp:Content ID="coninner" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <asp:ScriptManager ID="sc" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="up" runat="server">
        <ContentTemplate>
            <div class="content oem-content">
                <div class="sideBg">
                    <div class="col-mod-12">
                        <ul class="breadcrumb">
                            <li><a href='<%=ResolveUrl("~/Dashboard") %>'>Dashboard</a></li>
                        </ul>
                    </div>
                    <form method="post" class="addfdi">
                        <div class="admin-dashboard">
                             <div class="row">
                            <div class="col-md-12">
                                <div class="fdi-tab add-inflow-tab">
                                    <ul>
                                        <li class="active"><a href="#fdistep1" data-toggle="tab">FDI</a></li>
                                        <li><a href="#fdistep2" data-toggle="tab">Export</a></li>
                                        <li><a href="#fdistep3" data-toggle="tab">Production</a></li>
                                    </ul>
                                </div>
                                <div class="fdi-add-content">
                                    <li id="fdistep1" class="tab-pane fade in active">

                                        <div class="row">
                                            <div class="col-lg-4 col-sm-6 col-xs-12">
                                                <div class="white-box analytics-info">
                                                    <h3 class="box-title">Total Visit</h3>
                                                    <ul class="list-inline two-part">
                                                        <li>
                                                           
                                                        </li>
                                                        <li class="text-right"><i class="ti-arrow-up text-success"></i><span class="counter text-success">659</span></li>
                                                       
                                                    </ul>
                                                     <div class="file-export"><i class="fas fa-file-export"></i></div>
                                                </div>
                                            </div>
                                            <div class="col-lg-4 col-sm-6 col-xs-12">
                                                <div class="white-box analytics-info">
                                                    <h3 class="box-title">Total Page Views</h3>
                                                    <ul class="list-inline two-part">
                                                        <li>
                                                           
                                                        </li>
                                                        <li class="text-right"><i class="ti-arrow-up text-purple"></i><span class="counter text-purple">869</span></li>
                                                       
                                                    </ul>
                                                     <div class="file-export"><i class="fas fa-file-export"></i></div>
                                                </div>
                                            </div>
                                            <div class="col-lg-4 col-sm-6 col-xs-12">
                                                <div class="white-box analytics-info">
                                                    <h3 class="box-title">Unique Visitor</h3>
                                                    <ul class="list-inline two-part">
                                                        <li>
                                                            
                                                        </li>
                                                        <li class="text-right"><i class="ti-arrow-up text-info"></i><span class="counter text-info">911</span></li>
                                                        
                                                    </ul>
                                                    <div class="file-export"><i class="fas fa-file-export"></i></div>
                                                </div>
                                            </div>
                                        </div>

                                    </li>
                                    <li id="fdistep2" class="tab-pane fade in"></li>
                                    <li id="fdistep3" class="tab-pane fade in "></li>
                                </div>
                            </div>
                        </div>
                        </div>
                       
                    </form>
                </div>
                <div class="footer">© 2019 <a href="#">Department of Defence Production</a> </div>
            </div>

            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
