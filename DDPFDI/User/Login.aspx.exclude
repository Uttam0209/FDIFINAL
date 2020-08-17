<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="User_Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <head>
        <title>Login</title>
        <meta name="viewport" content="width=device-width, initial-scale=1.0">
        <meta charset="utf-8">
        <link rel="icon" href="media/fevi.png">


        <!-- External CSS libraries -->
        <link rel="stylesheet" type="text/css" href="~/User/Uassets/css/bootstrap.min.css">
        <link rel="stylesheet" type="text/css" href="~/User/Uassets/css/theme.min.css">
        <link rel="stylesheet" type="text/css" href="~/User/Uassets/css/font-awesome-4.5.0/css/font-awesome.min.css">
        <!-- Custom stylesheet -->
        <link rel="stylesheet" type="text/css" href="~/User/Uassets/css/responsive.css">
        <link rel="stylesheet" type="text/css" href="~/User/Uassets/css/style.css">
    </head>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container-fluid">
            <div class="row " style="padding: 8px;">
                <div class="col-sm-2 col-3 ">
                    <img src="user/ddp_logo.png" alt="" class="img-fluid" />
                </div>
                <div class="col-sm-10 topheadline col-9">
                    <h2 class="mb-0 top_headline" style="color: #6915cf;">Opportunities for Make in India in Defence</h2>
                </div>
            </div>
        </div>

        <div class="page-title-overlap bg-dark pb-3 pt-3">
            <div class="container d-lg-flex justify-content-between">
                <div class="order-lg-1 pr-lg-4 text-center text-lg-left">
                    <%--<h1 class="h3 text-light mb-0">Login to DDP </h1>--%>
                </div>
                 <div class="order-lg-1 pr-lg-4 text-center text-lg-right">
                 <b><a href="https://srijandefence.gov.in/Login" target="_blank" class="menu_"><i class="fa fa-user" aria-hidden="true"></i> DPSU Login </a></b>
                  </div>           
            </div>
        </div>
        <div class="container py-4 py-lg-5 my-4">
            <div class="row justify-content-center">
                <div class="col-md-6">
                    <div class="card border-0 shadow">
                        <div class="card-body">
                            <h2 class="h4 mb-4">Log in</h2>
                            <form class="needs-validation" novalidate="">
                                <div class="input-group-overlay form-group">
                                    <div class="input-group-prepend-overlay"><span class="input-group-text"><i class="fas fa-envelope"></i></span></div>
                                    <input class="form-control prepended-form-control" type="email" placeholder="Email" required="">
                                </div>
                                <div class="input-group-overlay form-group">
                                    <div class="input-group-prepend-overlay"><span class="input-group-text"><i class="fas fa-lock"></i></span></div>
                                    <div class="password-toggle">
                                        <asp:TextBox ID="txtpass" runat="server" class="form-control prepended-form-control" type="password" placeholder="Password" required=""></asp:TextBox>
                                        <%-- <label class="password-toggle-btn">
                                            <input class="custom-control-input" type="checkbox"><i class="fas fa-eye password-toggle-indicator"></i><span class="sr-only">Show password</span>
                                        </label>--%>
                                    </div>
                                </div>
                                <%--   <div class="d-flex flex-wrap justify-content-between">
                                    <div class="custom-control custom-checkbox">
                                        <input class="custom-control-input" type="checkbox" checked="" id="remember_me">
                                        <label class="custom-control-label" for="remember_me">Remember me</label>
                                    </div>
                                    <a class="nav-link-inline font-size-sm" href="account-password-recovery.html">Forgot password?</a>
                                </div>--%>
                                <hr class="mt-4">
                                <div class="text-right pt-4">
                                    <asp:LinkButton runat="server" ID="log" CssClass="btn btn-dark" OnClick="log_Click"><i class="fas fa-sign-in-alt mr-2 ml-n21"></i>Sign In</asp:LinkButton>
                                </div>
                            </form>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <footer class="bg-dark p-2 ffooter">
            <p class="mb-0">Department of Defence Production</p>
        </footer>
        <script src="User/Uassets/js/jquery-3.4.1.min.js"></script>
        <script src="User/Uassets/js/all.min.js"></script>
        <script src="User/Uassets/js/bootstrap.bundle.min.js"></script>
        <script src="User/Uassets/js/theme.min.js"></script>


    </form>
</body>
</html>
