<%@ Page Language="C#" AutoEventWireup="true" Inherits="User_U_Cart" CodeFile="U_Cart.aspx.cs" ViewStateEncryptionMode="Always" MasterPageFile="~/User/MasterPage.master" %>


<asp:Content ID="content1" ContentPlaceHolderID="head" runat="server">
    <style>
        #container1 {
            box-shadow: 0 0 5px black;
            margin-top: 0px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager runat="server" ID="sc"></asp:ScriptManager>
    <asp:UpdatePanel runat="server" ID="update">
        <ContentTemplate>
            <div id="container1" class="container-flued">
                <div class="py-3 px-5">
                    <div class="row">
                        <div class="col-lg-8">
                            <div class="order-lg-1 text-center text-lg-left">
                                <h2 class="text-dark mr-auto">Cart Details</h2>
                            </div>
                            <div class="pr-lg-0">
                                <!-- Header-->
                                <div class="d-flex flex-wrap justify-content-between align-items-center border-bottom pb-3">
                                    <div class="py-3">
                                        <asp:LinkButton runat="server" class="btn btn-outline-accent btn-sm" ID="lblhome" OnClick="lblhome_Click">
                                                <i class="fa fa-backward"></i>&nbsp;Back to Home</asp:LinkButton>
                                    </div>
                                    <div class="d-none d-sm-block py-1 font-size-ms">
                                        <asp:Label ID="lbltotalprodincart" runat="server" Text=""></asp:Label>
                                    </div>
                                    <div class="py-2">
                                        <asp:LinkButton ID="lbclaercart" runat="server" Class="btn btn-outline-primary btn" OnClick="lbclaercart_Click">
                                    <i class="fa fa-trash font-size-xs"></i>&nbsp;Clear Cart
                                        </asp:LinkButton>
                                    </div>
                                </div>
                                <asp:DataList ID="dlCartProd" runat="server" RepeatColumns="1" RepeatDirection="Vertical" RepeatLayout="Flow" OnItemCommand="dlCartProd_ItemCommand">
                                    <ItemTemplate>
                                        <div class="media d-block d-sm-flex align-items-center py-4 border-bottom">
                                            <a class="d-block position-relative mb-3 mb-sm-0 mr-sm-4 mx-auto" href="#" style="width: 12.5rem;">
                                                <img class="rounded-lg" src='<%#Eval("TopImages") %>' alt="Product">
                                            </a>
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<div class="media-body text-center text-sm-left">
                                                <h3 class="h6 product-title mb-2">
                                                    <asp:Label runat="server" ID="mComp" Text='<%#Eval("CompanyName") %>'>
                                                    </asp:Label>
                                                    <asp:TextBox ID="txtremark" runat="server" TextMode="MultiLine" Height="85px" Style="float: right; background-color: #f1f1f1; max-width: 300px;" placeholder="Remarks against product"></asp:TextBox>
                                                    <asp:HiddenField ID="hfrole" runat="server" Value='<%#Eval("Role") %>' />
                                                    <asp:HiddenField ID="hfnodelname" runat="server" Value='<%#Eval("NodalName") %>' />
                                                    <asp:HiddenField ID="hfnodalphone" runat="server" Value='<%#Eval("NodalPhoneNo") %>' />
                                                </h3>
                                                <div class="d-inline-block text-accent" runat="server" visible="false">
                                                    <%# Eval("NSNGroup").ToString().Length > 35? (Eval("NSNGroup") as string).Substring(0,35) + ".." : Eval("NSNGroup")  %>
                                                </div>
                                                <a class="d-inline-block text-accent font-size-ms" href="#">[<%#Eval("NSCCode") %>] -  <%# Eval("NSNGroupClass").ToString().Length > 35? (Eval("NSNGroupClass") as string).Substring(0,35) + ".." : Eval("NSNGroupClass")  %>
                                                       
                                                </a>
                                                <div class="form-inline pt-2">
                                                    <asp:Label ID="lblprodname" runat="server" Text='<%# Eval("ProductDescription").ToString().Length > 150? (Eval("ProductDescription") as string).Substring(0,150) + ".." : Eval("ProductDescription")  %>'></asp:Label>

                                                </div>
                                                <div class="clearfix mt-1"></div>
                                                <div class="row w-1000" style="justify-content: flex-end;">
                                                    <asp:LinkButton ID="lbmoredetail" runat="server" class="cart-btns btn btn-outline-primary mr-1" CommandArgument='<%#Eval("ProductRefNo") %>' CommandName="moredetail">
                                                    <i class="fa fa-eye"></i>&nbsp;More Detail</asp:LinkButton>
                                                    <asp:LinkButton ID="ddlremovecart" runat="server" class="cart-btns btn btn-outline-primary" CommandArgument='<%#Eval("ProductRefNo") %>' CommandName="removecart">
                                                    <i class="fa fa-trash"></i>&nbsp;Remove from cart</asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:DataList>
                            </div>
                        </div>
                        <!-- Sidebar-->
                        <aside class="col-lg-4">
                            <hr class="d-lg-none">
                            <div class="cz-sidebar-static h-100 ml-auto border-left">
                                <h1 class="text-center mt-2"><i class="fas fa fa-industry"></i></h1>
                                <div class="text-center border-bottom">
                                    <h2 class="h6">Show interest section</h2>
                                    <div class="widget widget-categories mb-3">
                                        <asp:HiddenField runat="server" ID="hfsaveid" />
                                        <label>Your Name</label>
                                        <div class="input-group">
                                            <asp:TextBox ID="txtname" runat="server" CssClass="form-control" Placeholder="Name" TabIndex="1"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="widget widget-categories mb-3">
                                        <label>Your Email</label>
                                        <div class="input-group">
                                            <asp:TextBox ID="txtemail" runat="server" CssClass="form-control" TextMode="Email"  AutoPostBack="true" OnTextChanged="txtemail_TextChanged" Placeholder="Email" TabIndex="2"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="widget widget-categories mb-3">
                                        <label>Company Name</label>
                                        <div class="input-group">
                                            <asp:TextBox ID="txtcompname" runat="server" CssClass="form-control" Placeholder="Company Name" TabIndex="3"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="widget widget-categories mb-3">
                                        <label>Registerd Office Address</label>
                                        <div class="input-group">
                                            <asp:TextBox ID="txtofficeaddress" runat="server" TextMode="MultiLine" Height="70px" CssClass="form-control" Placeholder="Registerd Office Address" TabIndex="4"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="widget widget-categories mb-3">
                                        <label>Mobile</label>
                                        <div class="input-group">
                                            <asp:TextBox ID="txtphone" runat="server" CssClass="form-control" MaxLength="10" Placeholder="Mobile" AutoPostBack="true" TextMode="Phone" OnTextChanged="txtphone_TextChanged" onkeypress="return isNumberKey(event)" TabIndex="5"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="widget widget-categories mb-3">
                                        <div class="input-group">
                                            <asp:Button ID="btnsendmail" runat="server" CssClass="btn btn-primary btn-shadow btn-block mt-4" Text="Get OTP" TabIndex="6"
                                                OnClick="btnsendmail_Click" ToolTip="Get otp after click on this"></asp:Button>
                                        </div>
                                    </div>
                                </div>
                                <div class="text-center pt-2"><small class="text-form text-muted">Email will be sent to the site administrator.</small></div>
                            </div>
                        </aside>
                    </div>
                </div>
            </div>
            <div class="modal-quick-view modal fade" id="ProductCompany" tabindex="-1" style="z-index: 9999999;">
                <div class="modal-dialog modal-xl" style="max-width: 800px !important;">
                    <div class="modal-content">
                        <div class="modal-header modal-header1 d-flex justify-content-center" style="background: #507CD1!important;">
                            <h5 class="modal-title text-white">Import Item Details</h5>
                        </div>
                        <div class="modal-body" style="padding: 20px 40px 18px 40px;">
                            <div class="simplebar-content">
                                <!-- Categories-->
                                <div class="widget widget-categories mb-4">
                                    <div class="accordion mt-n1" id="shop-categories">
                                        <div id="printarea">
                                            <div class="card" style="border-bottom: solid 1.4px #e5e5e5 !important;">
                                                <div class="card-header">
                                                    <h3 class="accordion-heading mb-2">
                                                        <a class="collapsed" href="#shoes" role="button" data-toggle="collapse" aria-expanded="false"
                                                            aria-controls="shoes">Item Description <span class="accordion-indicator iconupanddown">
                                                                <i class="fa fa-chevron-up"></i></span></a>
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
                                                                    <th scope="row">Division/Plant:Unit
                                                                    </th>
                                                                    <td>
                                                                        <asp:Label ID="lbldiviname" runat="server" Text=""></asp:Label>
                                                                        &nbsp:
                                                                            <asp:Label ID="lblunitnamepro" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr runat="server" id="two" visible="false" class="d-none">
                                                                    <th scope="row" class="d-none">Unit:
                                                                    </th>
                                                                    <td></td>
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
                                                                <tr>
                                                                    <td colspan="2">
                                                                        <h6 class="tablemidhead">OEM Details</h6>
                                                                    </td>
                                                                </tr>
                                                                <tr runat="server" id="seven">
                                                                    <th scope="row">OEM Name:Country
                                                                    </th>
                                                                    <td>
                                                                        <asp:Label ID="lbloemname" runat="server" Text=""></asp:Label>
                                                                        :&nbsp;<asp:Label ID="lbloemcountry" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr runat="server" id="eight">
                                                                    <th scope="row">OEM Part Number
                                                                    </th>
                                                                    <td>
                                                                        <asp:Label ID="lbloempartno" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr runat="server" id="nine" visible="FALSE" class="d-none">
                                                                    <th scope="row" class="d-none">OEM Country
                                                                    </th>
                                                                    <td></td>
                                                                </tr>
                                                                <tr runat="server" id="twentyfive" visible="FALSE">
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
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="card border-btm" style="border-bottom: solid 1.4px #e5e5e5 !important;">
                                                <div class="card-header">
                                                    <h3 class="accordion-heading mb-2">
                                                        <a class="collapsed" href="#ItemSpecification" role="button" data-toggle="collapse"
                                                            aria-expanded="false" aria-controls="shoes">Item Specification <span class="accordion-indicator iconupanddown">
                                                                <i class="fa fa-chevron-up"></i></span></a>
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
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="card border-btm" style="border-bottom: solid 1.4px #e5e5e5 !important;">
                                                <div class="card-header">
                                                    <h3 class="accordion-heading mb-2">
                                                        <a class="collapsed" href="#Estimated" role="button" data-toggle="collapse" aria-expanded="false"
                                                            aria-controls="shoes">Import Value, Quantity <span class="accordion-indicator iconupanddown">
                                                                <i class="fa fa-chevron-up"></i></span></a>
                                                    </h3>
                                                </div>
                                                <div class="collapse" id="Estimated" data-parent="#shop-categories">
                                                    <div class="card-body card-custom ">
                                                        <table class="table" width="100%">
                                                            <tbody>

                                                                <tr runat="server" id="fifteen">
                                                                    <td>
                                                                        <asp:GridView ID="gvestimatequanorprice" runat="server" AutoGenerateColumns="false"
                                                                            CssClass="table table-hover">
                                                                            <Columns>
                                                                                <asp:BoundField DataField="FYear" HeaderText="Year of Import" />
                                                                                <asp:TemplateField HeaderText="Quantity">
                                                                                    <ItemTemplate>
                                                                                        <%# Eval("EstimatedQty").ToString() == "0" ? "*" : Eval("EstimatedQty").ToString()%>
                                                                                        <%-- <%# Eval("EstimatedQty").ToString() == "0" ? "*" : "*"%>--%>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:BoundField DataField="Unit" HeaderText="Unit" />
                                                                                <asp:BoundField DataField="EstimatedPrice" HeaderText="Import value in million Rs (Qty*Price)" DataFormatString="{0:f2}" />
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
                                                                                        <b>Import value during last 3 year (million Rs) :</b>
                                                                                        <asp:Label ID="lblisproductimported" runat="server" Text=""></asp:Label>&nbsp;&nbsp;
                                                                                        &nbsp;<asp:Label ID="lblvalueimport" runat="server"
                                                                                            Text="0"></asp:Label>&nbsp;
                                                                                    </td>
                                                                                </tr>
                                                                                <tr runat="server" id="ten">
                                                                                    <td colspan="2" style="border-top: 0px;">
                                                                                        <asp:GridView ID="gvestimatequanold" runat="server" AutoGenerateColumns="false"
                                                                                            Class="table table-responsive table-bordered">
                                                                                            <Columns>
                                                                                                <asp:BoundField HeaderText="Year of Import" DataField="FYear" />
                                                                                                <asp:TemplateField HeaderText="Quantity">
                                                                                                    <ItemTemplate>
                                                                                                        <%# Eval("EstimatedQty").ToString() == "0" ? "*" : Eval("EstimatedQty").ToString()%>
                                                                                                        <%-- <%# Eval("EstimatedQty").ToString() == "0" ? "*" : "*"%>--%>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:BoundField HeaderText="Unit" DataField="Unit" />
                                                                                                <asp:BoundField HeaderText="Imported value in million Rs (Qty*Price)" DataField="EstimatedPrice" DataFormatString="{0:f2}" />
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
                                                                <tr runat="server" id="Tr25">
                                                                    <th scope="row">Indigenization starting  Year
                                                                    </th>
                                                                    <td>
                                                                        <asp:Label ID="lblindtrgyr" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr runat="server" id="Tr1">
                                                                    <th scope="row">Indigenization started
                                                                    </th>
                                                                    <td>
                                                                        <asp:Label ID="lblindstart" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                </tr>
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
                                                                    <th scope="row">EoI/RFP URL
                                                                    </th>
                                                                    <td>
                                                                        <asp:Label ID="lbleoilink" runat="server" Text=""></asp:Label>
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
                                                                <tr>
                                                                    <th scope="row">Phone Number
                                                                    </th>
                                                                    <td>
                                                                        <asp:Label ID="lblphonenumber" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="card border-btm" style="border-bottom: solid 1.4px #e5e5e5 !important;" runat="server" visible="false">
                                                <div class="card-header">
                                                    <h3 class="accordion-heading mb-2">
                                                        <a class="collapsed" href="#AdditionalValue" role="button" data-toggle="collapse" aria-expanded="false"
                                                            aria-controls="shoes">Additional Details <span class="accordion-indicator iconupanddown">
                                                                <i class="fa fa-chevron-up"></i></span></a>
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
                                                                <tr runat="server" id="twentyfour" visible="false">
                                                                    <th scope="row"></th>
                                                                    <td runat="server">
                                                                        <asp:Label ID="lblisshowgeneral" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <input id="btnprint" type="button" runat="server" visible="false" onclick="PrintDiv()" style="width: 70px;" class="btn btn-primary  pull-right"
                                value="Print" />
                            <asp:LinkButton ID="LinkButton5" runat="server" Text="Close" Style="width: 80px; background: #507CD1!important;" class="btn"
                                ClientIDMode="Static" ToolTip="Update Data" data-dismiss="modal" />
                        </div>
                    </div>
                </div>
            </div>
            <asp:HiddenField runat="server" ID="hfotp" />
            <div class="modal fade" id="modelotp" tabindex="-1" role="dialog" data-backdrop="static" data-keyboard="false">
                <div class="modal-dialog modal-dialog-centered" role="document" style="max-width: 600px;">
                    <div class="modal-content">
                        <div class="modal-header">
                            <ul class="nav nav-tabs card-header-tabs" role="tablist">
                                <li class="nav-item"><a class="nav-link active" href="#" data-toggle="tab" role="tab"
                                    aria-selected="true">OTP</a></li>
                            </ul>
                            <button class="close" type="button" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span></button>
                        </div>
                        <div class="modal-body tab-content py-4">
                            <p class="text-justify">
                                <asp:TextBox runat="server" ID="txtotp" placeholder="OTP (6 Digit)" TabIndex="1" CssClass="form-control"></asp:TextBox>
                                <div class="clearfix mt-1">
                                </div>
                                <span>Please enter otp received on your given email id.</span>
                                <div class="clearfix mt-1">
                                </div>
                                <asp:LinkButton ID="lbsubmit" runat="server" CssClass="btn btn-primary btn-shadow pull-right" TabIndex="2" Text="Submit"
                                    ToolTip="Thank you for showing intrest in these product mail will send to admin and also you will recieved a copy." OnClick="lbsubmit_Click"></asp:LinkButton>
                                <asp:LinkButton runat="server" Text="Resend OTP" ID="lbresendotp" TabIndex="3" CssClass="pull-right mr-1 p-2" OnClick="lbresendotp_Click"></asp:LinkButton>
                                <p>
                                </p>
                            </p>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="lbsubmit" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress" runat="server" AssociatedUpdatePanelID="update">
        <ProgressTemplate>
            <div class="overlay-progress">
                <div class="custom-progress-bar blue stripes">
                    <span></span>
                    <p style="margin-left: 200px;">Processing</p>
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <script src="User/Uassets/js/jquery-3.4.1.min.js"></script>
    <script type="text/javascript">
        function showPopup() {
            $('#ProductCompany').modal('show');
        }
    </script>
    <script type="text/javascript">
        function showPopup1() {
            $('#modelotp').modal('show');
        }
    </script>
    <script type="text/javascript">
        function PrintDiv() {
            var divToPrint = document.getElementById('printarea');
            var popupWin = window.open('', '_blank', 'width=300,height=400,location=no,left=200px');
            popupWin.document.open();
            popupWin.document.write('<html><body onload="window.print()">' + divToPrint.innerHTML + '</html>');
            popupWin.document.close();
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
    <script src="User/Uassets/js/jquery-ui.min.js"></script>
    <script type="text/javascript">
        $(function () {
            SetAutoComplete();
        });
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {
                    SetAutoComplete();
                }
            });
        };
        function SetAutoComplete() {
            $("[id$=txtcompname]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: 'User/U_Cart.aspx/GetSearchKeyword',
                        data: "{ 'prefix': '" + request.term + "'}",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item,
                                    val: item
                                };
                            }))
                        }
                    });
                },
                minLength: 1

            });
        }
    </script>
</asp:Content>

