<%@ Page Title="" Language="C#" MasterPageFile="~/User/MasterPage.master" AutoEventWireup="true" CodeFile="MasterUpdateSuccessStory.aspx.cs" Inherits="User_MasterUpdateSuccessStory" %>


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

        .dot4 {
            background-color: #edbbbb;
        }

        .dot5 {
            background-color: #25776e;
        }
    </style>
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
                                    <span class="dot"></span><span class="pb-2">&nbsp;Need to fill Supply Order status</span>
                                </div>
                                <div class="col-sm-3">
                                    <span class="dot dot1"></span><span class="pb-2">&nbsp;Need to fill Manufacture Name</span>
                                </div>
                                <div class="col-sm-3">
                                    <span class="dot dot3"></span><span class="pb-2">&nbsp;Need to fill Supply Order Address</span>
                                </div>
                                <div class="col-sm-3">
                                    <span class="dot dot2"></span><span class="pb-2">&nbsp;Need to fill Supply Order Value in (Rs Lakhs)</span>
                                </div>
                                <div class="col-sm-3">
                                    <span class="dot dot4"></span><span class="pb-2">&nbsp;Need to fill Supply Order Delivery (Compliance Date)</span>
                                </div>
                                <div class="col-sm-3">
                                    <span class="dot dot5"></span><span class="pb-2">&nbsp;Need to fill Supply Order Date</span>
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
                            <div class="table table-responsive">
                                <asp:GridView runat="server" ID="gvSuccessStory" AutoGenerateColumns="false" class="table table-hover table-dark" OnRowDataBound="gvSuccessStory_RowDataBound" OnRowCommand="gvSuccessStory_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sr.No" HeaderStyle-Width="80px">
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="chkHeader" runat="server" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox runat="server" ID="chkRow" />&nbsp;&nbsp;
                                                <%#Eval("row_no") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="ProductRefNo" HeaderText="Item No" NullDisplayText="#" HeaderStyle-Wrap="true" HeaderStyle-Width="100px" />
                                        <asp:BoundField DataField="ProductDescription" HeaderText="Item Name" NullDisplayText="#" HeaderStyle-Wrap="true" HeaderStyle-Width="100px" />
                                        <asp:BoundField DataField="SupplyOrderStatus" HeaderText="Status" NullDisplayText="#" HeaderStyle-Wrap="true" HeaderStyle-Width="200px" />
                                        <asp:BoundField DataField="SupplyManfutureName" HeaderText="Indigenized Year:" NullDisplayText="#" HeaderStyle-Wrap="true" HeaderStyle-Width="180px" />
                                        <asp:BoundField DataField="SupplyManfutureAddress" HeaderText="Supply Manufacture Name:" NullDisplayText="#" HeaderStyle-Wrap="true" HeaderStyle-Width="100px" />
                                        <asp:BoundField DataField="SupplyOrderValue" HeaderText="Supply Manufacture Address:" NullDisplayText="#" HeaderStyle-Wrap="true" HeaderStyle-Width="250px" />
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
    <div class="modal-quick-view modal fade" id="modelsupplyorder" tabindex="-1" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-xl" style="max-width: 500px!important; z-index: 9999999999;">
            <div class="modal-content">
                <div class="modal-header d-flex justify-content-center" style="background: #507CD1!important">
                    <h4 class="modal-title">Success Story -Updation</h4>
                </div>
                <div class="modal-body">
                    <p>All field fill mandatory. After successfully update record will remove from here and display in success stroy page.</p>
                    <div class="form-group">
                        <label>Indegenization Process Started:-</label>
                        <asp:DropDownList ID="ddleoistatus" runat="server" TabIndex="1" ToolTip="Please enter the status of your supply order" CssClass="form-control">
                            <asp:ListItem Value="">Select</asp:ListItem>
                            <asp:ListItem Value="Yes">Yes</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="form-group">
                        <label>Make In India Category:-</label>
                        <asp:RadioButtonList ID="rblcategory" runat="server" >
                            <asp:ListItem Text="IDEX/AI/INNOVATION/R&D" Value="1"></asp:ListItem>
                             <asp:ListItem Text="IGA" Value="2"></asp:ListItem>
                             <asp:ListItem Text="IN HOUSE" Value="3"></asp:ListItem>
                             <asp:ListItem Text="MAKE - II" Value="15"></asp:ListItem>
                             <asp:ListItem Text="OTHER THAN MAKE - 23" Value="1"></asp:ListItem>
                        </asp:RadioButtonList>
                         </div>
                    <div class="form-group">
                        <label>Indegenization Date:-</label>
                         <asp:TextBox runat="server" ID="TXTindgDate" TabIndex="5" ToolTip="Indegenization Date" CssClass="form-control" type="date"></asp:TextBox>
                        <p>Date will store in this format:- DD-MMM-YYYY (01-Mar-2021)</p> </div>
                    <div class="form-group">
                        <label>Import Negated value:-</label>
                         <asp:TextBox runat="server" ID="txtimportvalue" TabIndex="4" CssClass="form-control" onkeypress="return isNumberKey(event)" ToolTip="Import negated Value (Rs Lakhs) deciaml allowed"
                            Placeholder="Supply Order Value in (Rs Lakhs)"></asp:TextBox> </div>
                    <div class="form-group">
                        <label>Manufacture Name</label>
                        <asp:TextBox runat="server" ID="txtmanufacname" placeholder="Manufacture Name" TabIndex="2" ToolTip="Please enter name of manufacture name" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label>Address </label>
                        <asp:TextBox runat="server" ID="txtmanuadd" TabIndex="3" ToolTip="Manufacture address" CssClass="form-control" TextMode="MultiLine" Height="100px"></asp:TextBox>
                    </div>
                   
                    <div class="form-group">
                        <label>Year of Indeginization:-</label>
                        <asp:DropDownList ID ="ddlindeginization" runat="server" >
                            <asp:ListItem Text="2019-2020" Value="1920"></asp:ListItem>
                             <asp:ListItem Text="2020-2021" Value="2021"></asp:ListItem> 
                             <asp:ListItem Text="2021-2022" Value="2122"></asp:ListItem>
                            <asp:ListItem Text="2022-2023" Value="2223"></asp:ListItem>
                            <asp:ListItem Text="2023-2024" Value="2224"></asp:ListItem>
                            <asp:ListItem Text="2024-2025" Value="2225"></asp:ListItem>                          
                        </asp:DropDownList>
                    </div>
                    <div class="clearfix"></div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary  btn-sm" tabindex="8" data-dismiss="modal">Close</button>
                    <div class="form-group">
                        <asp:LinkButton runat="server" ID="lbsubmit" TabIndex="7" ToolTip="after fill all text box or select dorpdown your 
                            form will submit and update status of supply order" CssClass="btn btn-primary btn-sm" OnClick="lbsubmit_Click"><i class="fa fa-save"></i>&nbsp;Submit & Update</asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script src="User/Uassets/js/jquery-3.4.1.min.js"></script>
    <script type="text/javascript">
        function showPopup() {
            $('#modelsupplyorder').modal('show');
        }
    </script>
    <script type="text/javascript">
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode;
            if (charCode != 46 && charCode > 31
                && (charCode < 48 || charCode > 57))
                return false;
            return true;
        }
    </script>
</asp:Content>

