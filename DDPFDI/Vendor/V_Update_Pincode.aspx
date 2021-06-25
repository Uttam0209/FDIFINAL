<%@ Page Title="" Language="C#" MasterPageFile="~/Vendor/VendorMaster.master" AutoEventWireup="true" CodeFile="V_Update_Pincode.aspx.cs" Inherits="Vendor_V_Update_Pincode" %>
<%@ Register Assembly="MSCaptcha" Namespace="MSCaptcha" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css"/>
     <div class="content oem-content">
         <asp:ScriptManager runat="server" ID="sc2"></asp:ScriptManager>
        <div class="sideBg">
            <div class="row">
                <div class="col-md-12 padding_0">
                    <div id="divHeadPage" runat="server"></div>
                </div>
            </div>
            <div class="container">
                <div class="cacade-forms">
                    <div class="clearfix mt10"></div>
                    <h1>Add Pincode/City/State</h1>

                    <div class="animated fadeIn">

                          <div class="form-group">Step 1
                          <div class="col-sm-4">
                                                         <p>Pin Code</p><br />
                                                           <asp:TextBox ID="sugpin" runat="server" class="form-control" placeholder="New Pin Code*" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                                        </div>
                                                        <div class="col-sm-4">
                                                            <p>Post Office Name</p><br />
                                                           <asp:TextBox ID="Postofficename" runat="server" class="form-control" placeholder="Post Office Name *"></asp:TextBox>
                                                        </div>
                                                       <div class="col-sm-4">
                                                           <p></p><br />
                                                            <asp:Button class="btn btn-primary btn-sm" ID="Button1" OnClick="AddPincode" Text="Add Pincode" runat="server" />
                                                           <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
                                                        </div>
                                                        

                        </div>
                        <a href="#" onclick="showDiv5()" style="margin-left: 63%;"><i class="fa fa-pencil-square-o" style="font-size:18px;color:blue"></i>&nbsp;Step 2</a>
                        <div id="myID5" style="display: none;">
                      <div class="form-group"> 
                          <div class="col-sm-4">
                                                         <p>Pin Code</p><br />
                                                          <%-- <asp:TextBox ID="sugpin" runat="server" class="form-control" placeholder="New Pin Code*"></asp:TextBox>--%>
                              <asp:DropDownList runat="server" ID="ddlPin" CssClass="form-cascade-control form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlPincode_SelectedIndexChanged"></asp:DropDownList>
                                                        </div>
                                                        <div class="col-sm-4">
                                                            <p>City</p><br />
                                                           <asp:TextBox ID="sugcity" runat="server" class="form-control" placeholder="New City *"></asp:TextBox>
                                                        </div>
                                                        <div class="col-sm-4">
                                                             <p>State</p><br />
                                                            <asp:TextBox ID="sugstate" runat="server" class="form-control" placeholder="State *"></asp:TextBox>
                                                        </div>

                        </div>

                       


                        <div class="form-group">
                            <div class="col-sm-5">
                                 <%--<asp:Button class="btn btn-primary btn-sm" ID="Button2" OnClick="profileMigrat" Text="Upgrade Information" runat="server" />--%>
                                <asp:Button class="btn btn-primary btn-sm" ID="Button2" OnClick="AddCity" Text="Update City Details" runat="server" />
                                <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                            </div>
                        </div>
                    </div> </div>
                        <div class="row">
                            <div class="col-lg-12">
                            </div>
                            <!--/.col-->

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
                        </div>
                        
                    </div>
                </div>
            </div>

        </div>
    </div>
    <script type="text/javascript">
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode;
            if (charCode != 46 && charCode > 31
                && (charCode < 48 || charCode > 57))
                return false;

            return true;
        }
    </script>
    <script type="text/javascript">
        function showDiv5() {
            // document.getElementById('myID2').style.display = "block";

            var y = document.getElementById("myID5");
            if (y.style.display === "block") {
                y.style.display = "none";
            } else {
                y.style.display = "block";
            }
        }

    </script>
</asp:Content>


