<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewRequestInfo.aspx.cs" Inherits="Admin_ViewRequestInfo" MasterPageFile="~/Admin/MasterPage.master" %>

<asp:Content ID="headDesignation" runat="server" ContentPlaceHolderID="head">
</asp:Content>
<asp:Content ID="InnerDesignation" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div class="content oem-content">
        <asp:UpdatePanel runat="server" ID="updatepan">
            <ContentTemplate>
                <div class="sideBg">
                    <div class="row">
                        <div class="col-md-12 padding_0">
                            <div id="divHeadPage" runat="server"></div>
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
                    <div class="addfdi">
                        <asp:DataList runat="server" ID="dlrequest" RepeatColumns="4" RepeatDirection="Horizontal" RepeatLayout="Flow" OnItemCommand="dlrequest_ItemCommand">
                            <ItemTemplate>
                                <div class="col-sm-3">
                                    <div class="card product-card" style="box-shadow: 0 0.3rem 1.525rem -0.375rem rgba(0, 0, 0, 0.1);">

                                        <div class="card-body py-2" style="height: 300px;">

                                            <h2 class="m-0 text-center">
                                                <b class="text-center">
                                                <asp:Label runat="server" ID="lblcompshow" class="product-meta d-block font-size-xs pb-1 text-center mt5" Style="color: #6915cf; font-size: 20px!important; margin-top: 10px;">
                                                                <%#Eval("RequestBy") %>
                                                                 </asp:Label>
                                            </b>
                                            </h2>
                                            <h3 class="product-title font-size-sm  text-center mt5">
                                                <asp:Label runat="server" ID="lbldesc" title='<%#Eval("RequestCompName") %>'>
                                                                <%# Eval("RequestCompName").ToString().Length > 100? (Eval("RequestCompName") as string).Substring(0,100) + ".." : Eval("RequestCompName")  %>
                                                                 </asp:Label>

                                            </h3>
                                            <table class="table" style="font-size: 14px;">
                                                <tbody>
                                                    <tr id="Tr3" runat="server">
                                                        <td colspan="2" style="padding: 8px; font-size: 12px;">mobile Number :- <b>
                                                            <%#Eval("RequestMobileNo") %></b>
                                                        </td>
                                                    </tr>
                                                    <tr id="Tr17" runat="server">
                                                        <td colspan="2" style="padding: 8px; font-size: 12px;">Address :- <b>
                                                            <%#Eval("RequestAddress") %></b>
                                                        </td>
                                                    </tr>
                                                    <tr id="Tr18" runat="server">
                                                        <td colspan="2" style="padding: 8px; font-size: 12px;">Email :- <b>
                                                            <%#Eval("RequestEmail") %></b>
                                                        </td>
                                                    </tr>

                                                    <tr id="Tr233" runat="server">
                                                        <td class="text-right" colspan="2" style="padding: 8px; font-size: 11px;">Date :- 
                                                                       
                                                                        <%#Eval("RequestDate","{0:dd-MMM-yyyy}") %>
                                                                         </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>
                                        <div class="text-center btn btn-primary btn-block " style="padding-bottom:10px;">
                                            <asp:LinkButton ID="lbview" runat="server" class="nav-link-style font-size-ms" CommandName="View"
                                                CommandArgument='<%#Eval("RequestDate","{0:yyyy-MM-dd}") %>'>                                                
                                                   <p style="color:white; margin-bottom:0px;">
                                                        <i class="fas fa-eye align-middle mr-1"></i>
                                                    More Details
                                                   </p>
                                                        </asp:LinkButton>
                                        </div>

                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:DataList>
                        <div class="clearfix"></div>
                        <div class="modal fade" id="aboutus" tabindex="-1" role="dialog">
                            <div class="modal-dialog modal-dialog-centered" role="document" style="width:1000px;">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <ul class="nav nav-tabs card-header-tabs" role="tablist" style="border: none!important;">
                                            <li class="nav-item"><a class="nav-link active" href="#" data-toggle="tab" role="tab"
                                                aria-selected="true">Request Product Detail</a></li>
                                            <li class="liclose">
                                                  <button class="close" type="button" data-dismiss="modal" aria-label="Close">
                                            <span aria-hidden="true">&times;</span></button>
                                            </li>
                                        </ul>
                                      
                                    </div>
                                    <div class="modal-body tab-content p-0">
                                        <asp:GridView ID="gvViewNodalOfficerAdd" runat="server" Width="100%" Class="commonAjaxTbl master-company-table table display 
                                    responsive no-wrap table-hover manage-user Grid"
                                            AutoGenerateColumns="false" OnRowCreated="gvRequestInfo_RowCreated">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No.">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="ProductRefno" HeaderText="Reference No" NullDisplayText="#" />
                                                <asp:BoundField DataField="CompanyName" HeaderText="Company Name" Visible="False" NullDisplayText="#" />
                                                <asp:BoundField DataField="ProductDescription" HeaderText="Description" NullDisplayText="#" />
                                                <asp:BoundField DataField="NSNGroup" HeaderText="NSN Group" NullDisplayText="-" />
                                                <asp:BoundField DataField="NSCCode" HeaderText="NSC Code" NullDisplayText="-" />
                                                <asp:BoundField DataField="NSNGroupClass" HeaderText="NSN Group Class" NullDisplayText="#" />
                                            </Columns>
                                        </asp:GridView>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <script type="text/javascript">
        function showPopup() {
            $('#aboutus').modal('show');
        }
        </script>
</asp:Content>
