<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewPopup.aspx.cs" Inherits="ViewPopup" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="User/Uassets/css/bootstrap.min.css">
    <link rel="stylesheet" type="text/css" href="User/Uassets/css/theme.min.css">
    <link rel="stylesheet" type="text/css" href="User/Uassets\css\font-awesome-4.5.0\css\font-awesome.min.css">
    <link rel="stylesheet" type="text/css" href="User/Uassets/css/style.css">
</head>
<body>
    <form id="form1" runat="server">
        <div class="modal-content">
            <div class="modal-header modelhead">
                <h4 class="modal-title product-title" style="font-size: 25px;">Import Item Details
                </h4>
                <button class="close" style="padding-right: 45px;" type="button" data-dismiss="modal"
                    aria-label="Close">
                    <span aria-hidden="true">&times;</span></button>
            </div>
            <div class="modal-body" style="padding: 20px 40px 18px 40px;">
                <div class="card" style="border-bottom: solid 1.4px #e5e5e5!important;">
                    <div class="card-header">
                        <h3 class="accordion-heading mb-2">
                            <a class="collapsed" href="#shoes" role="button" data-toggle="collapse" aria-expanded="false"
                                aria-controls="shoes">Item Description <span class="accordion-indicator iconupanddown">
                                    <i class="fas fa-chevron-up"></i></span></a>
                        </h3>
                    </div>
                    <div class="collapse" id="shoes" data-parent="#shop-categories">
                        <div class="card-body card-custom ">
                            <h6 class="tablemidhead">DPSUs,OFB & SHQs Details</h6>
                            <table class="table mb-2">
                                <tbody>
                                    <tr>
                                        <th scope="row">DPSU/OFB/SHQ:
                                        </th>
                                        <td>
                                            <asp:Label ID="lblcompname" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr runat="server" id="one">
                                        <th scope="row">Division/Plant:
                                        </th>
                                        <td>
                                            <asp:Label ID="lbldiviname" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr runat="server" id="two">
                                        <th scope="row">Unit:
                                        </th>
                                        <td>
                                            <asp:Label ID="lblunitnamepro" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                            <h6 class="tablemidhead">Item Description</h6>
                            <table class="table mb-2">
                                <tbody>
                                    <tr>
                                        <th scope="row">Item Id (Portal)
                                        </th>
                                        <td>
                                            <asp:Label ID="lblrefnoview" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr runat="server" id="Tr23" style="color: blue;">
                                        <th>Item Name</th>
                                        <td>
                                            <asp:Label ID="lblitemname1" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr runat="server" id="three">
                                        <th scope="row">DPSU Part Number
                                        </th>
                                        <td>
                                            <asp:Label ID="lbldpsupartno" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr runat="server" id="Tr8">
                                        <th scope="row">NIN Code
                                        </th>
                                        <td>
                                            <asp:Label ID="lblnincode" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr runat="server" id="four">
                                        <th scope="row">HSN Code
                                        </th>
                                        <td>
                                            <asp:Label ID="lblhsncode8digit" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th scope="row">Industry Domain
                                        </th>
                                        <td>
                                            <asp:Label ID="prodIndustryDomain" runat="server" Text=""></asp:Label>
                                            /
                                                                        <asp:Label ID="ProdIndusSubDomain" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr id="Tr9" runat="server" visible="false">
                                        <th scope="row">Search keywords
                                        </th>
                                        <td>
                                            <asp:Label ID="lblsearchkeywords" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <h6 class="tablemidhead">OEM Details</h6>
                                        </td>
                                    </tr>
                                    <tr runat="server" id="seven">
                                        <th scope="row">OEM Name
                                        </th>
                                        <td>
                                            <asp:Label ID="lbloemname" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr runat="server" id="eight">
                                        <th scope="row">OEM Part Number
                                        </th>
                                        <td>
                                            <asp:Label ID="lbloempartno" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr runat="server" id="nine">
                                        <th scope="row">OEM Country
                                        </th>
                                        <td>
                                            <asp:Label ID="lbloemcountry" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr runat="server" id="twentyfive">
                                        <th scope="row">OEM Address
                                        </th>
                                        <td>
                                            <asp:Label ID="lbloemaddress" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <h6 class="tablemidhead">Item Classification (NATO Group & Class)</h6>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th scope="row">NATO Supply Group:
                                        </th>
                                        <td>
                                            <asp:Label ID="lblnsngroup" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th scope="row">NATO Supply Class:
                                        </th>
                                        <td>
                                            <asp:Label ID="lblnsngroupclass" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th scope="row">Item Name Code:
                                        </th>
                                        <td>
                                            <asp:Label ID="lblclassitem" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr runat="server" id="six">
                                        <th scope="row">NSC Code (4 digit):
                                        </th>
                                        <td>
                                            <asp:Label ID="lblnsccode4digit" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>

                            <%--<h6 class="tablemidhead">Imported During Last 3 years</h6>--%>
                        </div>
                    </div>
                </div>
                <div class="card border-btm" style="border-bottom: solid 1.4px #e5e5e5!important;">
                    <div class="card-header">
                        <h3 class="accordion-heading mb-2">
                            <a class="collapsed" href="#ItemSpecification" role="button" data-toggle="collapse"
                                aria-expanded="false" aria-controls="shoes">Item Specification <span class="accordion-indicator iconupanddown">
                                    <i class="fas fa-chevron-up"></i></span></a>
                        </h3>
                    </div>
                    <div class="collapse" id="ItemSpecification" data-parent="#shop-categories">
                        <div class="card-body card-custom ">
                            <table class="table mb-2">
                                <tbody>
                                    <tr runat="server" id="eleven" style="color: blue;">
                                        <th>Item Name</th>
                                        <td>
                                            <asp:Label ID="itemname2" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr runat="server" id="twele">
                                        <th scope="row">Document
                                        </th>
                                        <td>
                                            <asp:GridView runat="server" ID="gvpdf" AutoGenerateColumns="false" Class="table table-responsive table-bordered">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblpathname" runat="server" Text='<%#Eval("ImageName").ToString().Substring(7) %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="View or Download">
                                                        <ItemTemplate>
                                                            <a href='<%#Eval("ImageName") %>' target="_blank" title="Click on icon for download pdf">View or downlaod</a>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr runat="server" id="thirteen">
                                        <th scope="row">Image
                                        </th>
                                        <td>
                                            <asp:DataList ID="dlimage" runat="server" RepeatColumns="4" Visible="true" RepeatDirection="Horizontal"
                                                RepeatLayout="Flow">
                                                <ItemTemplate>
                                                    <div class="col-sm-3">
                                                        <a data-fancybox="Prodgridviewgellry" target="_blank" href='<%#Eval("[ImageName]") %>'>
                                                            <asp:Image ID="imgprodimage" runat="server" CssClass="img-responsive img-container"
                                                                Height="90px" Width="110px" src='<%#Eval("[ImageName]") %>' />
                                                        </a>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:DataList>
                                        </td>
                                    </tr>
                                    <tr runat="server" id="twentysix">
                                        <th scope="row">Quality Assurance Agency 
                                        </th>
                                        <td>
                                            <asp:Label ID="lbqa" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr id="Tr10" runat="server" visible="false">
                                        <th scope="row">Specification
                                        </th>
                                        <td>
                                            <asp:Label ID="lblitemspecification" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr runat="server" id="fourteen">
                                        <th scope="row">Features & Details
                                        </th>
                                        <td>
                                            <asp:Label ID="lblfeaturesanddetail" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr id="Tr11" runat="server" visible="false">
                                        <th scope="row">Information
                                        </th>
                                        <td>
                                            <asp:GridView ID="gvProdInfo" runat="server" AutoGenerateColumns="false" CssClass="table table-hover">
                                                <Columns>
                                                    <asp:BoundField DataField="NameOfSpec" HeaderText="Name of Specification" />
                                                    <asp:BoundField DataField="Value" HeaderText="Value " />
                                                    <asp:BoundField DataField="Unit" HeaderText="Unit" />
                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr id="Tr12" runat="server" visible="false">
                                        <th scope="row">Additional Information
                                        </th>
                                        <td>
                                            <asp:Label ID="lbladditionalinfo" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
                <div class="card border-btm" style="border-bottom: solid 1.4px #e5e5e5!important;">
                    <div class="card-header">
                        <h3 class="accordion-heading mb-2">
                            <a class="collapsed" href="#Estimated" role="button" data-toggle="collapse" aria-expanded="false"
                                aria-controls="shoes">Import Value, Quantity <span class="accordion-indicator iconupanddown">
                                    <i class="fas fa-chevron-up"></i></span></a>
                        </h3>
                    </div>
                    <div class="collapse" id="Estimated" data-parent="#shop-categories">
                        <div class="card-body card-custom ">
                            <table class="table" width="100%">
                                <tbody>
                                    <tr id="Tr13" runat="server" visible="false">
                                        <th scope="row">PROCURMENT CATEGORY REMARK
                                        </th>
                                        <td>
                                            <asp:Label ID="lblprocremarks" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr runat="server" id="fifteen">
                                        <td>
                                            <asp:GridView ID="gvestimatequanorprice" runat="server" AutoGenerateColumns="false"
                                                CssClass="table table-hover">
                                                <Columns>
                                                    <asp:BoundField DataField="FYear" HeaderText="Year of Import" />
                                                    <asp:BoundField DataField="EstimatedQty" HeaderText="Quantity" />
                                                    <asp:BoundField DataField="Unit" HeaderText="Unit" />
                                                    <asp:BoundField DataField="EstimatedPrice" HeaderText="Import value in Rs lakh (Qty*Price)" />
                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table class="table mb-2">
                                                <tbody>
                                                    <tr runat="server" id="five">
                                                        <td colspan="2">
                                                            <b>Import value during last 3 year (Rs lakhs) :</b>
                                                            <asp:Label ID="lblisproductimported" runat="server" Text=""></asp:Label>&nbsp;&nbsp;
                                                                                        &nbsp;<asp:Label ID="lblvalueimport" runat="server"
                                                                                            Text="0"></asp:Label>&nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr runat="server" id="ten">
                                                        <td colspan="2" style="border-top: 0px;">
                                                            <asp:GridView ID="gvestimatequanold" runat="server" AutoGenerateColumns="false" Class="table table-responsive table-bordered">
                                                                <Columns>
                                                                    <asp:BoundField HeaderText="Year of Import" DataField="FYear" />
                                                                    <asp:BoundField HeaderText="Quantity" DataField="EstimatedQty" />
                                                                    <asp:BoundField HeaderText="Unit" DataField="Unit" />
                                                                    <asp:BoundField HeaderText="Imported value in Rs lakh (Qty*Price)" DataField="EstimatedPrice" />
                                                                </Columns>
                                                            </asp:GridView>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                            <h6 class="tablemidhead">Status of Indigenization</h6>
                            <table class="table mb-2">
                                <tbody>
                                    <tr runat="server" id="sixteen">
                                        <th scope="row">Make in India Category
                                        </th>
                                        <td>
                                            <asp:Label ID="lblindicate" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr runat="server" id="seventeen">
                                        <th scope="row">EoI/RFP
                                        </th>
                                        <td>
                                            <asp:Label ID="lbleoirep" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr runat="server" id="eighteen">
                                        <th scope="row">Link
                                        </th>
                                        <td>
                                            <asp:Label ID="lbleoilink" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr id="Tr1" runat="server" visible="false">
                                        <th scope="row">Tendor Uploaded
                                        </th>
                                        <td>
                                            <asp:Label ID="lbltendor" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                            <h6 class="tablemidhead">Contact Details</h6>
                            <table class="table mb-2" runat="server" id="nineteen">
                                <tbody>
                                    <tr>
                                        <th scope="row">Name
                                        </th>
                                        <td>
                                            <asp:Label ID="lblempname" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th scope="row">Designation
                                        </th>
                                        <td>
                                            <asp:Label ID="lbldesignation" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th scope="row">E-Mail ID
                                        </th>
                                        <td>
                                            <asp:Label ID="lblemailidpro" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr id="Tr14" runat="server" visible="false">
                                        <th scope="row">Mobile Number
                                        </th>
                                        <td>
                                            <asp:Label ID="lblmobilenumber" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th scope="row">Phone Number
                                        </th>
                                        <td>
                                            <asp:Label ID="lblphonenumber" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr id="Tr15" runat="server" visible="false">
                                        <th scope="row">Fax
                                        </th>
                                        <td>
                                            <asp:Label ID="lblfaxpro" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
                <div class="card border-btm" style="border-bottom: solid 1.4px #e5e5e5!important;">
                    <div class="card-header">
                        <h3 class="accordion-heading mb-2">
                            <a class="collapsed" href="#AdditionalValue" role="button" data-toggle="collapse" aria-expanded="false"
                                aria-controls="shoes">Additional Details <span class="accordion-indicator iconupanddown">
                                    <i class="fas fa-chevron-up"></i></span></a>
                        </h3>
                    </div>
                    <div class="collapse" id="AdditionalValue" data-parent="#shop-categories">
                        <div class="card-body card-custom ">
                            <table class="table mb-2">
                                <tbody>
                                    <tr runat="server" id="twenty">
                                        <th scope="row">End User 
                                        </th>
                                        <td>
                                            <asp:Label ID="lblenduser" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr runat="server" id="twentyone">
                                        <th scope="row">Defence Paltform 
                                        </th>
                                        <td>
                                            <asp:Label ID="lbldefenceplatform" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr runat="server" id="twentytwo">
                                        <th scope="row">Name of Defence Platform 
                                        </th>
                                        <td>
                                            <asp:Label ID="lblnameofdefplat" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr runat="server" id="twentythree">
                                        <th scope="row"></th>
                                        <td id="Td1" runat="server">
                                            <asp:Label ID="lbldeclaration" runat="server" Text="No IPR issue, No violation of TOT agreement, No violation of Security Concern"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr runat="server" id="twentyfour">
                                        <th scope="row"></th>
                                        <td id="Td2" runat="server">
                                            <asp:Label ID="lblisshowgeneral" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr runat="server" id="Tr19" visible="false">
                                        <th id="Th1" scope="row" runat="server" visible="false">Is Indigenised 
                                        </th>
                                        <td id="Td3" runat="server" visible="false">
                                            <asp:Label ID="lblisindigenised" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr runat="server" id="Tr20" visible="false">
                                        <th scope="row">Indian Manufacturer
                                        </th>
                                        <td>
                                            <asp:Label ID="lblmanuname" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr runat="server" id="Tr21" visible="false">
                                        <th scope="row">Address
                                        </th>
                                        <td>
                                            <asp:Label ID="lblmanuaddress" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr runat="server" id="Tr22" visible="false">
                                        <th scope="row">Year of Make in India
                                        </th>
                                        <td>
                                            <asp:Label ID="lblyearofindi" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr runat="server" id="Tr24" visible="false">
                                        <th scope="row">Make in India Process started
                                        </th>
                                        <td>
                                            <asp:Label ID="lblprocstart" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr runat="server" id="Tr25" visible="false">
                                        <th scope="row">Make in India Target Year
                                        </th>
                                        <td>
                                            <asp:Label ID="lblindtrgyr" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
                <div class="clearfix mt10">
                </div>
                <div class="modal-footer">
                    <input id="btnprint" type="button" onclick="PrintDiv()" style="width: 70px;" class="btn btn-primary  pull-right"
                        value="Print" />
                </div>
            </div>
        </div>
        <script src="User/Uassets/js/jquery-3.4.1.min.js"></script>
        <script src="User/Uassets/js/all.min.js"></script>
        <script src="User/Uassets/js/bootstrap.bundle.min.js"></script>
        <script src="User/Uassets/js/theme.min.js"></script>
        <script type="text/javascript">
            function PrintDiv() {
                var divToPrint = document.getElementById('printarea');
                var popupWin = window.open('', '_blank', 'width=300,height=400,location=no,left=200px');
                popupWin.document.open();
                popupWin.document.write('<html><body onload="window.print()">' + divToPrint.innerHTML + '</html>');
                popupWin.document.close();
            }
        </script>
    </form>

</body>
</html>
