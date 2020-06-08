<%@ page language="C#" autoeventwireup="true" CodeFile="U_Cart.aspx.cs" inherits="User_U_Cart" viewStateEncryptionMode="Always" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Srijan Degence</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta charset="utf-8">
    <link rel="icon" href="media/fevi.png">
    <link rel="stylesheet" type="text/css" href="~/User/Uassets/css/bootstrap.min.css">
    <link rel="stylesheet" type="text/css" href="~/User/Uassets/css/theme.min.css">
    <link rel="stylesheet" type="text/css" href="~/User/Uassets\css\font-awesome-4.5.0\css\font-awesome.min.css">
    <link rel="stylesheet" type="text/css" href="~/User/Uassets/css/style.css">
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager runat="server" ID="sc"></asp:ScriptManager>
        <asp:UpdatePanel runat="server" ID="update">
            <ContentTemplate>
                 <div class="container-fluid">
                    <div class="row " style="padding:8px;">
                        <div class="col-sm-2 col-3 ">
                            <img src="user/ddp_logo.png" alt="" class="img-fluid" />
                        </div>
                        <div class="col-sm-10 topheadline col-9">
                            <h1 class="mb-0">Srijan Defence</h1>
                        </div>
                    </div>
                </div>
                <div class="page-title-overlap bg-dark pt-4">
                    <div class="container d-lg-flex justify-content-between">
                        <div class="order-lg-2 mb-3 mb-lg-0 pt-lg-2">
                            <div class="navbar-tool dropdown ml-5">
                                <asp:LinkButton ID="lbtotalcart" runat="server" class="navbar-tool-icon-box bg-secondary dropdown-toggle">
                                    <span class="navbar-tool-label" runat="server" id="totalno">0</span>
                                    <i class="navbar-tool-icon fas fa-cart-plus" style="margin-top: 13px;"></i>
                                </asp:LinkButton>
                                <asp:LinkButton ID="lbcart" runat="server" Style="color: white; margin-left: 10px;">
                                     Cart
                                </asp:LinkButton>
                            </div>
                        </div>
                        <div class="order-lg-1 pr-lg-4 text-center text-lg-left">
                            <h1 class="h3 text-light mb-0">Cart Details</h1>
                        </div>
                    </div>
                </div>
                <div class="container mb-5 pb-3">
                    <div class="bg-light box-shadow-lg rounded-lg overflow-hidden">
                        <div class="row">
                            <!-- Content-->
                            <section class="col-lg-8 pt-2 pt-lg-4 pb-4 mb-3">
                                <div class="pt-2 px-4 pr-lg-0 pl-xl-5">
                                    <!-- Header-->
                                    <div class="d-flex flex-wrap justify-content-between align-items-center border-bottom pb-3">
                                        <div class="py-1">
                                            <a class="btn btn-outline-accent btn-sm" href="UProductList">
                                                <i class="fas fa-chevron-left mr-1 ml-n1"></i>Back to Home</a>
                                        </div>
                                        <div class="d-none d-sm-block py-1 font-size-ms">
                                            <asp:Label ID="lbltotalprodincart" runat="server" Text=""></asp:Label>
                                        </div>
                                        <div class="py-1">
                                            <asp:LinkButton ID="lbclaercart" runat="server" Class="btn btn-outline-danger btn-sm" OnClick="lbclaercart_Click">
                                    <i class="fas fa-trash-alt font-size-xs mr-1 ml-n1"></i>Clear Cart
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                    <asp:DataList ID="dlCartProd" runat="server" RepeatColumns="1" RepeatDirection="Vertical" RepeatLayout="Flow" OnItemCommand="dlCartProd_ItemCommand">
                                        <ItemTemplate>
                                            <div class="media d-block d-sm-flex align-items-center py-4 border-bottom">
                                                <a class="d-block position-relative mb-3 mb-sm-0 mr-sm-4 mx-auto" href="#" style="width: 12.5rem;">
                                                    <img class="rounded-lg" src='<%#Eval("TopImages") %>' alt="Product">
                                                </a>
                                                &nbsp;&nbsp;&nbsp;<div class="media-body text-center text-sm-left">
                                                    <h3 class="h6 product-title mb-2">
                                                        <a href="#"><%#Eval("CompanyName") %>
                                                        </a>
                                                    </h3>
                                                    <div class="d-inline-block text-accent" runat="server" visible="false">
                                                        <%# Eval("NSNGroup").ToString().Length > 35? (Eval("NSNGroup") as string).Substring(0,35) + ".." : Eval("NSNGroup")  %>
                                                    </div>
                                                    <a class="d-inline-block text-accent font-size-ms" href="#">
                                                        <%# Eval("NSNGroupClass").ToString().Length > 35? (Eval("NSNGroupClass") as string).Substring(0,35) + ".." : Eval("NSNGroupClass")  %>
                                                        - (<%#Eval("NSCCode") %>)
                                                    </a>
                                                    <div class="form-inline pt-2">
                                                        <%# Eval("ProductDescription").ToString().Length > 150? (Eval("ProductDescription") as string).Substring(0,150) + ".." : Eval("ProductDescription")  %>
                                                    </div>
                                                    <div class="row w-1000" style="justify-content: flex-end;">
                                                        <asp:LinkButton ID="ddlremovecart" runat="server" class="cart-btns btn btn-outline-dark" CommandArgument='<%#Eval("ProductRefNo") %>' CommandName="removecart">
                                                    <i class="fas fa-trash-alt mr-2"></i>Remove Cart</asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                    </asp:DataList>
                                </div>
                            </section>
                            <!-- Sidebar-->
                            <aside class="col-lg-4">
                                <hr class="d-lg-none">
                                <div class="cz-sidebar-static h-100 ml-auto border-left">
                                    <h1 class="text-center mt-4 mb-2"><i class="fas fa fa-industry"></i></h1>
                                    <div class="text-center mb-4 pb-3 border-bottom">
                                        <h2 class="h6 mb-3 pb-1">Show intrest section</h2>
                                        <div class="widget widget-categories mb-3">
                                            <label>Your Name</label>
                                            <div class="input-group">
                                                <asp:TextBox ID="txtname" runat="server" CssClass="form-control" Placeholder="Name" TabIndex="1"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="widget widget-categories mb-3">
                                            <label>Your Email</label>
                                            <div class="input-group">
                                                <asp:TextBox ID="txtemail" runat="server" CssClass="form-control" TextMode="Email" Placeholder="Email" TabIndex="2"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="widget widget-categories mb-3">
                                            <div class="input-group">
                                                <asp:Button ID="btnsendmail" runat="server" CssClass="btn btn-primary btn-shadow btn-block mt-4" Text="Send Mail" TabIndex="3"
                                                    OnClick="btnsendmail_Click" ToolTip="Thank you for showing intrest in these product mail will send to admin and also you will recieved a copy."></asp:Button>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="text-center pt-2"><small class="text-form text-muted">Mail will send to site admin.</small></div>
                                </div>
                            </aside>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
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
        <div class="container-fluid" style="background-color:#000000;">
                    <div class="row " >
                        <div class="col-sm-2 col-3 ">
                            
                        </div>
                        <div class="col-sm-10 col-9">
                          <p style="color:white; padding:20px; text-align:center;">
                    Website content managed by : Department of Defence Production
                </p>
                        </div>
                    </div>
                </div>
        <script src="User/Uassets/js/jquery-3.4.1.min.js"></script>
        <script src="User/Uassets/js/all.min.js"></script>
        <script src="User/Uassets/js/bootstrap.bundle.min.js"></script>
        <script src="User/Uassets/js/theme.min.js"></script>
    </form>
</body>
</html>
