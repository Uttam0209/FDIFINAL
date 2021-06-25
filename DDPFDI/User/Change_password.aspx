<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Change_password.aspx.cs" Inherits="User_Change_password" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Change Password</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <meta charset="utf-8" />
    <link rel="icon" href="~/assets/images/icon.png" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.16.0/umd/popper.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
    <style>
        #box1 {
            box-shadow: 0 0 5px #6915cf;
        }

        #top {
            background-color: #373f50;
            text-transform: uppercase;
            color: #f1faee !important;
            padding-bottom: 0px !important;
            padding-top: 0px !important;
            padding-left: 20px;
        }

            #top a {
                color: #f1faee !important;
                display: inline-block;
                padding: 15px 10px 15px 10px;
                text-decoration: none;
            }


        #top2 {
            box-shadow: 0 0 5px;
        }

        #footer2 {
            background-color: #0c0032;
            color: #f1faee;
            width: 100%;
            text-align: center;
            padding: 15px !important;
        }


            #footer2 a {
                color: white !important;
                text-decoration: none !important;
            }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="top">
            <a href="#"><i class="fa fa-envelope" aria-hidden="true"></i>&nbsp; helpdesk-dpit@ddpmod.gov.in</a>
            <a href="#"><i class="fa fa-phone-square" aria-hidden="true"></i>&nbsp; 011-20836145 &nbsp;|&nbsp; 011-23019066</a>
        </div>
        <div id="top2" class="container-fluid" style="background: white;">

            <div class="row">
                <div class="col-md-2">
                    <a href="ProductList">
                        <img src="../ddp_logo.png" class="img-fluid" style="max-height: 70px" /></a>

                </div>
                <div class="col-md-8">
                    <h2 class="text-center" style="color: #6915cf;">OPPORTUNITIES FOR MAKE IN INDIA IN DEFENCE</h2>
                </div>
                <div class="col-md-2 mt-3">
                    <h5 class="page-header"><i class="fas fa-folder-plus"></i>
                    <asp:Label ID="lblPageName" runat="server" Text=""></asp:Label>
                </h5>
                </div>
            </div>
        </div>
        
        <div class="container-fluid" style="min-height: 82vh;">
            <div class="row d-flex justify-content-center" id="box" style="padding-top: 100px;">
                <div class="col-md-4 py-4" id="box1">
                    <div class="d-flex justify-content-center">
                        <h4>Create New Password</h4>
                    </div>
                    <p style="color: red"><b>Note:-</b>Password must be 8 Characters long with at least one Numeric, one Upper case Character and one Special Character.</p>
                    <div class="form-group">
                        <label for="newpass">New Password:</label>
                        <asp:TextBox type="password" runat="server" class="form-control" placeholder="Enter New Password" ID="txtnewpass"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="valPassword" Display="Dynamic" runat="server" ControlToValidate="txtnewpass"
                            ErrorMessage="Password must be 8 characters long with at least one numeric,</br>one upper case character and one special character." ForeColor="Red" ValidationExpression="^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$#^@$!%*?&])[A-Za-z\d$@$#^!%*?&]{8,64}" />
                    </div>
                    <div class="form-group">
                        <label for="pwd">Confirm Password:</label>
                        <asp:TextBox type="password" runat="server" class="form-control" placeholder="Confirm New password" ID="pwd"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" Display="Dynamic" runat="server" ControlToValidate="pwd"
                            ErrorMessage="Password must be 8 characters long with at least one numeric,</br>one upper case character and one special character." ForeColor="Red" ValidationExpression="^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$#^@$!%*?&])[A-Za-z\d$@$#^!%*?&]{8,64}" />
                    </div>
                    <div class="form-group d-flex justify-content-end">
                        <asp:LinkButton runat="server" ID="btnupdate" class="btn btn-primary" Style="margin-right: 15px;" TabIndex="4" Text="Update Password" OnClick="btnupdate_Click"></asp:LinkButton>
                    </div>
                </div>
                 <div class="clearfix"></div>
                <div class="form-group" runat="server" id="divmsg">
                </div>
            </div>
        </div>
        <div id="footer1" class="container-fluid" style="min-height: 50px; text-align: center; background: #373f50;">
            <div class="row">
                <div class="col-12" style="padding-top: 10px; color: white;">
                    ©2020 <a href="https://srijandefence.gov.in/ProductList" style="color: white;">www.srijandefence.gov.in</a> | All Right Reserved. | Designed, Developed and Hosted by Department of Defence Production                           
                </div>
            </div>
        </div>
    </form>
</body>
</html>
