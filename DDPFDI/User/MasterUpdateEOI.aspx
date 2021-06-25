<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MasterUpdateEOI.aspx.cs" Inherits="User_MasterUpdateEOI" MasterPageFile="~/User/MasterPage.master" %>

<asp:Content ID="content1" ContentPlaceHolderID="head" runat="server">
    <!------------------------------------CheckGrid----------------------------------------------------------->
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        $("[id*=chkHeader]").live("click", function () {
            var chkHeader = $(this);
            var grid = $(this).closest("table");
            $("input[type=checkbox]", grid).each(function () {
                if (chkHeader.is(":checked")) {
                    $(this).attr("checked", "checked");
                    $("td", $(this).closest("tr")).addClass("selected");
                } else {
                    $(this).removeAttr("checked");
                    $("td", $(this).closest("tr")).removeClass("selected");
                }
            });
        });
        $("[id*=chkRow]").live("click", function () {
            var grid = $(this).closest("table");
            var chkHeader = $("[id*=chkHeader]", grid);
            if (!$(this).is(":checked")) {
                $("td", $(this).closest("tr")).removeClass("selected");
                chkHeader.removeAttr("checked");
            } else {
                $("td", $(this).closest("tr")).addClass("selected");
                if ($("[id*=chkRow]", grid).length == $("[id*=chkRow]:checked", grid).length) {
                    chkHeader.attr("checked", "checked");
                }
            }
        });
    </script>
    <!------------------------------------------------End CheckListBox------------------------------------------------->
    <style>
        .dot {
            height: 25px;
            width: 25px;
            background-color: #8b0000;
            border-radius: 50%;
            display: inline-block;
        }

        .dot1 {
            background-color: #008b8b;
        }

        .dot2 {
            background-color: #8b008b;
        }

        .dot3 {
            background-color: #9400d3;
        }
    </style>
    <link rel="stylesheet" type="text/css" href="https://code.jquery.com/ui/1.12.0/themes/smoothness/jquery-ui.css">
</asp:Content>
<asp:Content ID="content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager runat="server" ID="sc"></asp:ScriptManager>
    <asp:UpdatePanel runat="server" ID="up" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="row d-flex justify-content-center">
                <div class="col-lg-11">
                    <div class="row d-flex">
                        <div class="col-sm-12">
                            <div class="row">
                                <div class="col-sm-3">
                                    <span class="dot"></span><span class="pb-2">&nbsp;Need to fill eoi status</span>
                                </div>
                                <div class="col-sm-3">
                                    <span class="dot dot1"></span><span class="pb-2">&nbsp;Need to fill eoi start date</span>
                                </div>
                                <div class="col-sm-3">
                                    <span class="dot dot2"></span><span class="pb-2">&nbsp;Need to fill eoi end date</span>
                                </div>
                                <div class="col-sm-3">
                                    <span class="dot dot3"></span><span class="pb-2">&nbsp;Need to fill eoi url</span>
                                </div>
                            </div>
                            <hr />

                            <p style="float: right;">
                                Showing
                                    <asp:Label runat="server" ID="lbltotalshowpageitem"></asp:Label>
                                products of
                                <asp:Label ID="lbltotfilter" runat="server"></asp:Label>
                                products  
                            </p>
                            <div class="row">
                                <div class="col-sm-1">
                                    <label>Index</label>
                                    <asp:DropDownList ID="ddlsort" runat="server" Width="83px" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlsort_SelectedIndexChanged">
                                        <asp:ListItem>100</asp:ListItem>
                                        <asp:ListItem>200</asp:ListItem>
                                        <asp:ListItem>400</asp:ListItem>
                                        <asp:ListItem>500</asp:ListItem>
                                        <asp:ListItem>1000</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-sm-4">
                                    <label>Search</label>
                                    <asp:TextBox runat="server" ID="txtsearch" CssClass="form-control" Placehodler="Search (Productrefno,Company,division,unit)" OnTextChanged="txtsearch_TextChanged"></asp:TextBox>
                                </div>
                            </div>
                            <div class="clearfix mt-1"></div>
                            <br />
                            <div class="table table-responsive">
                                <asp:GridView runat="server" ID="gvEoi" AutoGenerateColumns="false" class="table table-hover table-dark" 
                                    OnRowDataBound="gvEoi_RowDataBound" OnRowCommand="gvEoi_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sr.No.">
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="chkHeader" runat="server" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox runat="server" ID="chkRow" />&nbsp;&nbsp;
                                                <%#Eval("row_no") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="ProductRefNo" HeaderText="Item No" NullDisplayText="#" HeaderStyle-Wrap="true" />
                                        <asp:BoundField DataField="ProductDescription" HeaderText="Item Name" HeaderStyle-Width="300px" NullDisplayText="#" HeaderStyle-Wrap="true" />
                                        <asp:BoundField DataField="EOIStatus" HeaderText="EOI Status" NullDisplayText="#" HeaderStyle-Width="150px" HeaderStyle-Wrap="true" />
                                        <asp:BoundField DataField="EOIStartDate" HeaderText="EOI Start Date" NullDisplayText="#" HeaderStyle-Width="150px" HeaderStyle-Wrap="true" />
                                        <asp:BoundField DataField="EOIEndDate" HeaderText="EOI End Date" NullDisplayText="#" HeaderStyle-Width="150px" HeaderStyle-Wrap="true" />
                                        <asp:BoundField DataField="EOIUrl" HeaderText="EOI Url" NullDisplayText="#" HeaderStyle-Wrap="true" />
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <asp:LinkButton runat="server" ID="lbupdate" ForeColor="White" CommandArgument='<%#Eval("ProductRefNo") %>' CommandName="VEOI"><i class="fa fa-edit"></i>Update</asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <div class="clearfix mt-1"></div>
                            <!-----------------------------------------Code for pageindexing----------------------------------------------------->
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="row">
                                        <div class="col-sm-6">
                                            <asp:LinkButton ID="lnkbtnPgPrevious" runat="server" CssClass="btn btn-dark btn-sm"
                                                OnClick="lnkbtnPgPrevious_Click">Previous</asp:LinkButton>
                                        </div>
                                        <div class="col-sm-6">
                                            <asp:LinkButton ID="lnkbtnPgNext" runat="server" CssClass="btn  btn-dark btn-sm pull-right"
                                                OnClick="lnkbtnPgNext_Click">Next</asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="clearfix"></div>
                                    <div class="text-center">
                                        <asp:Label ID="lblpaging" runat="server" class="btn btn-dark text-center" Text=""></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <!-----------------------------------------end code for page indexing----------------------------------------------------->
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="modal-quick-view modal fade" id="modeleoi" tabindex="-1" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-xl" style="max-width: 500px!important; z-index: 9999999999;">
            <div class="modal-content">
                <asp:UpdatePanel runat="server" ID="up2" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="modal-header d-flex justify-content-center" style="background: #507CD1!important">
                            <h4 class="modal-title">EOI-Updation</h4>
                        </div>
                        <div class="modal-body">
                            <p>All field fill mandatory. After successfully update record will remove from here and display in success stroy page.</p>
                            <div class="form-group">
                                <label>EOI Status</label>&nbsp;<span style="color: red">*</span>
                                <asp:DropDownList ID="ddleoistatus" runat="server" TabIndex="1" ToolTip="Please enter the status of your eoi" AutoPostBack="true" OnSelectedIndexChanged="ddleoistatus_SelectedIndexChanged" CssClass="form-control">
                                    <asp:ListItem Value="">Select</asp:ListItem>
                                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                    <%--<asp:ListItem Value="No">No</asp:ListItem>--%>
                                </asp:DropDownList>
                            </div>
                            <div runat="server" id="diveoishow" visible="false">
                                <div class="form-group">
                                    <label>EOI Status</label>&nbsp;<span style="color: red">*</span>
                                    <asp:DropDownList ID="ddleoitype" runat="server" TabIndex="2" ToolTip="select the status of eoi" CssClass="form-control">
                                        <asp:ListItem Value="">Select</asp:ListItem>
                                        <asp:ListItem Value="Yes">Active</asp:ListItem>
                                        <asp:ListItem Value="Archive">Archive</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group">
                                    <label>EOI Start Date</label>&nbsp;<span style="color: red">*</span>
                                    <asp:TextBox runat="server" ID="txtstartdate" TabIndex="3" type="date" ToolTip="eoi start date (eoi start date could not be greater then eoi end date)"
                                        CssClass="form-control datepicker" autocomplete="off" placeholder="Start date (format:- 01-jan-1900)"></asp:TextBox>
                                    <p>Date will store in this format:- DD-MMM-YYYY (01-Mar-2021)</p>
                                </div>
                                <div class="form-group">
                                    <label>EOI End Date</label>&nbsp;<span style="color: red">*</span>
                                    <asp:TextBox runat="server" ID="txtenddate" TabIndex="4" type="date" AutoPostBack="true" OnTextChanged="txtenddate_TextChanged" ToolTip="eoi end date (eoi end date could not be less then eoi start date)"
                                        CssClass="form-control datepicker" autocomplete="off" placeholder="Start date (format:- 01-jan-1900)"></asp:TextBox>
                                    <p>Date will store in this format:- DD-MMM-YYYY (01-Mar-2021)</p>
                                </div>
                                <div class="form-group">
                                    <label>EOI URL</label>&nbsp;<span style="color: red">*</span>
                                    <asp:TextBox runat="server" ID="txturl" TabIndex="5" CssClass="form-control" ToolTip="eoi url" Placeholder="EOI URL" TextMode="Url"></asp:TextBox>

                                </div>
                            </div>
                            <div class="clearfix"></div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-secondary  btn-sm" tabindex="7" data-dismiss="modal">Close</button>
                            <div class="form-group">
                                <asp:LinkButton runat="server" ID="lbsubmit" TabIndex="6" Visible="false" ToolTip="after fill all text box or select dorpdown your form will submit and update status of eoi"
                                    CssClass="btn btn-primary btn-sm" OnClick="lbsubmit_Click"><i class="fa fa-save"></i>&nbsp;Submit & Update</asp:LinkButton>
                            </div>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="lbsubmit" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <script src="User/Uassets/js/jquery-3.4.1.min.js"></script>
    <script type="text/javascript">
        function showPopup() {
            $('#modeleoi').modal('show');
        }
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#ContentPlaceHolder1_txtstartdate').datepicker({
                dateFormat: "dd-M-yy"
            });
            $('#ContentPlaceHolder1_txtenddate').datepicker({
                dateFormat: "dd-M-yy"
            });
            var parameter = Sys.WebForms.PageRequestManager.getInstance();
            parameter.add_endRequest(function () {
                $('#ContentPlaceHolder1_txtstartdate').datepicker({
                    dateFormat: "dd-M-yy"
                });
                $('#ContentPlaceHolder1_txtenddate').datepicker({
                    dateFormat: "dd-M-yy"
                });
            });
        });
    </script>
</asp:Content>
