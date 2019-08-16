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
                        <div class="col-md-12">
                            <div class="clearfix"></div>
                            <div style="margin-top: 5px;">
                                <a class="fa fa-arrow-circle-left pull-right" href="javascript: history.go(-1)">&nbsp; &nbsp;Back</a>
                            </div>
                            <div class="clearfix"></div>
                        </div>
                    </div>

                    <div class="clearfix" style="margin-bottom: 10px;"></div>
                    <form method="post" class="addfdi">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="clearfix"></div>

                                <asp:HiddenField runat="server" ID="hfrole" />
                                <div class="table-wrapper">

                                    <div id="Div1" runat="server" visible="False">
                                        <div class="col-sm-4 row">
                                            <asp:TextBox ID="txtserch" runat="server" CssClass="form-cascade-control form-control" Placeholder="Type keyword to search"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-2 row">
                                            <asp:LinkButton runat="server" ID="btnsearch" CssClass="text-black btn btn-warning pull-left btn-md" OnClick="Search_Click" Text="Search"></asp:LinkButton>
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
                                <div id="Div3">
                                    <asp:Button ID="btnAddDesignation" runat="server" Text="Add Designation" Visible="False" CssClass="btn btn-primary pull-right" OnClick="btnAddDesignation_Click" />

                                </div>

                                <div class="clearfix mt10"></div>
                                <div class="table-responsive">
                                    <asp:HiddenField ID="hfmtype" runat="server" />
                                    <asp:GridView ID="gvViewDesignation" runat="server" Width="100%" Class="table table-hover"
                                        AutoGenerateColumns="false" OnRowCommand="gvViewDesignation_RowCommand" OnRowCreated="gvViewDesignation_RowCreated">
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No.">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Company">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblrefno" runat="server" Text='<%#Eval("CompanyName") %>' NullDisplayText="#" SortExpression="CompanyRefNo"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Designation" HeaderText="Designation" NullDisplayText="#" />
                                            <%--<asp:BoundField DataField="DesignationRefNo" HeaderText="Reference No." NullDisplayText="#" />--%>
                                            <asp:BoundField DataField="CreatedBy" HeaderText="Created By" NullDisplayText="#" />
                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbledit" runat="server" CssClass="fa fa-edit" ToolTip="Edit or Update Designation" CommandName="EditComp" CommandArgument='<%#Eval("DesignationId") %>'></asp:LinkButton>
                                                    <asp:LinkButton ID="lbldel" runat="server" CssClass="fa fa-trash" Visible="false" CommandName="DeleteComp" OnClientClick="return confirm('Are you sure you want to delete this Company?');" CommandArgument='<%#Eval("DesignationId") %>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <!-----------------------------------------Code for pageindexing----------------------------------------------------->
                                    <div class="row" runat="server" id="divpageindex" visible="false">
                                        <div class="col-sm-2">
                                            <asp:LinkButton ID="lnkbtnPgFirst" runat="server" CssClass="btn  btn-success  btn-sm"
                                                OnClick="lnkbtnPgFirst_Click">First</asp:LinkButton>
                                            <asp:LinkButton ID="lnkbtnPgPrevious" runat="server" CssClass="btn btn-success  btn-sm"
                                                OnClick="lnkbtnPgPrevious_Click">Previous</asp:LinkButton>
                                        </div>
                                        <div class="col-sm-8">
                                            <asp:DataList ID="DataListPaging" runat="server" RepeatDirection="Horizontal" OnItemCommand="DataListPaging_ItemCommand"
                                                OnItemDataBound="DataListPaging_ItemDataBound">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="Pagingbtn" runat="server" CssClass="btn btn-success mt5 btn-xs"
                                                        CommandArgument='<%# Eval("PageIndex") %>' CommandName="Newpage" Text='<%# Eval("PageText")%>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:DataList>
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:LinkButton ID="lnkbtnPgLast" runat="server" CssClass="btn  btn-success btn-sm pull-right"
                                                OnClick="lnkbtnPgLast_Click">Last</asp:LinkButton>
                                            <asp:LinkButton ID="lnkbtnPgNext" runat="server" CssClass="btn  btn-success btn-sm pull-right" Style="margin-right: 3px;"
                                                OnClick="lnkbtnPgNext_Click">Next</asp:LinkButton>
                                        </div>
                                        <div class="clearfix padding_0 mt10">
                                        </div>
                                        <div class="text-center">
                                            <asp:Label ID="lblpaging" runat="server" class="btn btn-primary text-center" Text=""></asp:Label>
                                        </div>
                                    </div>
                                    <!-----------------------------------------end code for page indexing----------------------------------------------------->
                                </div>
                            </div>
                        </div>
                    </form>
                </div>
                <div class="footer">© 2019 <a href="#">Department of Defence Production</a> </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress" runat="server" AssociatedUpdatePanelID="up">
        <ProgressTemplate>
            <!---Progress Bar ---->
            <div class="overlay-progress">
                <div class="custom-progress-bar blue stripes">
                    <span></span>
                    <p>Processing</p>
                </div>
            </div>
            <!---Progress Bar ---->
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>

