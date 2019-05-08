<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewDesignation.aspx.cs" MasterPageFile="~/Admin/MasterPage.master" Inherits="Admin_ViewDesignation" %>

<asp:Content ID="headViewDesignation" runat="server" ContentPlaceHolderID="head">
    <script type="text/javascript">
        function showPopup() {
            $('#changePass').modal('show');
        }
    </script>
    <script type="text/javascript">
        function showPopup1() {
            $('#divfactoryshow').modal('show');
        }
    </script>
    <script type="text/javascript">
        function showPopup2() {
            $('#divunitshow').modal('show');
        }
    </script>
    <script>
        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        };
    </script>
</asp:Content>
<asp:Content ID="innerViewDesignation" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <asp:ScriptManager ID="sc" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="up" runat="server">

        <ContentTemplate>
            <div class="content oem-content">
                <div class="sideBg">
                    <div class="row">
                    <div class="col-md-12 padding_0">
                         <div id="divHeadPage" runat="server"></div>
                    </div>
                    </div>
                    <form method="post" class="addfdi">
                        <div class="row">
                            <div class="col-md-12">
                                <asp:HiddenField runat="server" ID="hfrole" />
                                <div class="table-wrapper">
                                    
                                    <div id="Div1" runat="server" visible="False">
                                        <div class="col-sm-4 row">
                                            <asp:TextBox ID="txtserch" runat="server" CssClass="form-cascade-control form-control" Placeholder="Type keyword to search"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-2 row">
                                            <asp:LinkButton runat="server" ID="btnsearch" class="text-black btn btn-warning pull-left btn-md" OnClick="Search_Click" Text="Search"></asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-4">
                                            <label>Select Company</label>
                                            <asp:DropDownList runat="server" ID="ddlcompany" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlcompany_OnSelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                        
                                    </div>
                                </div>
                                <div class="clearfix"></div>
                                <div id="Div2" class="text-center" style="font-size: 16px; margin-top: 10px;" runat="server" visible="False">
                                    Total number of  Designation :<strong>
                                        <asp:Label ID="lbltotal" runat="server" Text=""></asp:Label></strong>
                                </div>

                                <div class="clearfix"></div>
                                <div class="table-wraper">
                                    <asp:GridView ID="gvViewDesignation" runat="server" Width="100%" Class="commonAjaxTbl master-company-table table display responsive no-wrap table-hover manage-user Grid" AutoGenerateColumns="false" AllowPaging="true"
                                         PageSize="25" AllowSorting="true" OnRowCommand="gvViewDesignation_RowCommand">
                                        <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
                                        <Columns>
                                            
                                            <asp:TemplateField HeaderText="S.No">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Reference No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblrefno" runat="server" Text='<%#Eval("CompanyRefNo") %>' NullDisplayText="#" SortExpression="CompanyRefNo"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Designation" HeaderText="Designation" NullDisplayText="#" SortExpression="Designation" />

                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbledit" runat="server" CssClass="fa fa-edit" CommandName="EditComp" CommandArgument='<%#Eval("DesignationId") %>'></asp:LinkButton>
                                                    <asp:LinkButton ID="lbldel" runat="server" CssClass="fa fa-trash" CommandName="DeleteComp" OnClientClick="return confirm('Are you sure you want to delete this Company?');" CommandArgument='<%#Eval("DesignationId") %>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>


                            </div>
                        </div>
                    </form>
                </div>
                <div class="footer">© 2019 <a href="#">Department of Defence Production</a> </div>
            </div>
           
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

