<%@ Page Title="" Language="C#" MasterPageFile="~/Vendor/VendorMaster2.master" AutoEventWireup="true" CodeFile="V_Add_NodalOfficer.aspx.cs" Inherits="V_Add_NodalOfficer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type='text/javascript'>
        function emailValidator(element, alertMsg) {
            debugger;
            var emailvalid = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
            if (element.value.match(emailvalid)) {
                element.style.color = "Green";
                return true;
            } else {
                //alert(alertMsg);
                element.focus();
                element.style.color = "Red";
                return false;
            }
        }
    </script>
<div class="right_col" role="main">
<div class="">
<div class="page-title">
<div class="title_left">
<h3>Add New Nodal Officer</h3>
</div>
<div class="title_right">
</div>
</div>
<div class="clearfix"></div>
<div class="row">
<div class="col-md-12 col-sm-12">
<div class="x_panel">
<div class="x_content">
<form>
<span class="section">Nodal Officer</span>

<div class="field item form-group">
<label class="col-form-label col-md-3 col-sm-3  label-align">Nodal Officer Name<span class="required">*</span></label>
<div class="col-md-6 col-sm-6">
    <asp:TextBox ID="NolOffName" runat="server" class="form-control" placeholder="Nodal Officer Name *"></asp:TextBox>
</div>
</div>
<div class="field item form-group">
<label class="col-form-label col-md-3 col-sm-3  label-align">Nodal Officer email:<span class="required">*</span></label>
<div class="col-md-6 col-sm-6">
 <asp:TextBox ID="VEmailID" runat="server" class="form-control" placeholder="Nodal Officer Email ID *"></asp:TextBox>
    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"
             ErrorMessage="* Please Enter Correct Email" ControlToValidate="VEmailID"
             SetFocusOnError="True"
             ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">
</asp:RegularExpressionValidator>
</div>
</div>

    <div class="field item form-group">
<label class="col-form-label col-md-3 col-sm-3  label-align">Company Name <span class="required">*</span></label>
<div class="col-md-6 col-sm-6">
<asp:TextBox ID="companyname" runat="server" class="form-control" placeholder="Company Name *"></asp:TextBox>
</div>
</div>
       <div class="field item form-group">
<label class="col-form-label col-md-3 col-sm-3  label-align">Pan No <span class="required">*</span></label>
<div class="col-md-6 col-sm-6">
<asp:TextBox ID="panno" runat="server" class="form-control" placeholder="Pan No *"></asp:TextBox>
</div>
</div>
       <div class="field item form-group">
<label class="col-form-label col-md-3 col-sm-3  label-align">GST NO <span class="required">*</span></label>
<div class="col-md-6 col-sm-6">
<asp:TextBox ID="gstno" runat="server" class="form-control" placeholder="GST No *"></asp:TextBox>
</div>
</div>
<div class="field item form-group">
<label class="col-form-label col-md-3 col-sm-3  label-align">Nodal Officer Contact No. <span class="required">*</span></label>
<div class="col-md-6 col-sm-6">
<asp:TextBox ID="VmobileNO" runat="server" class="form-control" placeholder="Nodal Officer Mobile NO *" onkeypress="return isNumberKey(event)"></asp:TextBox>
</div>
</div>
<div class="field item form-group">
<label class="col-form-label col-md-3 col-sm-3  label-align">Nodal Officer Registered Office Address <span class="required">*</span></label>
<div class="col-md-6 col-sm-6">
<asp:TextBox ID="NolOffADDRE1" runat="server" class="form-control" placeholder="Nodal Officer Registered Office Address *"></asp:TextBox></div>
</div>
<div class="field item form-group">
<label class="col-form-label col-md-3 col-sm-3  label-align">Nodal Officer Registered Office Address 2 <span class="required">*</span></label>
<div class="col-md-6 col-sm-6">
<asp:TextBox ID="NolOffADDRE2" runat="server" class="form-control" placeholder="Nodal Officer Registered Office Address2 *"></asp:TextBox></div>
</div>

    <div class="field item form-group"style="margin-left: 25%;">
        <div class="col-md-2 col-sm-2">
                            <p>Pin Code</p>
            <asp:DropDownList runat="server" ID="ddlPin" CssClass="form-cascade-control form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlPincode_SelectedIndexChanged"></asp:DropDownList>
                                                 </div>
                                                        <div class="col-md-2 col-sm-2">
                                                            <p>City</p>
                                                            <asp:DropDownList runat="server" ID="ddlCity" CssClass="form-cascade-control form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCity_SelectedIndexChanged"></asp:DropDownList>
                                                            
                                                        </div>
                                                        <div class="col-md-2 col-sm-2">
                                                             <p>State</p>
                                                            <asp:DropDownList runat="server" ID="ddlstate" CssClass="form-cascade-control form-control"></asp:DropDownList>
                                                        </div>

                        </div>
    <%--<a href="#" onclick="showDiv5()" style="margin-left: 63%;"><i class="fa fa-pencil-square-o" style="font-size:18px;color:blue"></i>&nbsp;Suggest Location&nbsp;</a>--%>
    <div class="form-group" id="myID5" style="display: none;">        
<div class="field item form-group">
<label class="col-form-label col-md-3 col-sm-3  label-align">City <span class="required">*</span></label>
<div class="col-md-6 col-sm-2">
<asp:TextBox ID="sugcity" runat="server" class="form-control" placeholder="City *"></asp:TextBox></div>
</div>
<div class="field item form-group">
<label class="col-form-label col-md-3 col-sm-3  label-align">State <span class="required">*</span></label>
<div class="col-md-6 col-sm-2">
<asp:TextBox ID="sugstate" runat="server" class="form-control" placeholder="State *"></asp:TextBox></div>
</div>
<div class="field item form-group">
<label class="col-form-label col-md-3 col-sm-3  label-align">Pincode <span class="required">*</span></label>
<div class="col-md-6 col-sm-2">
<asp:TextBox ID="sugpin" runat="server" class="form-control" placeholder="Pincode *" onkeypress="return isNumberKey(event)"></asp:TextBox>
   
</div>
</div>
        </div>     

    <div class="field item form-group">
<label class="col-form-label col-md-3 col-sm-3  label-align">Upload Authorization Letter <span class="required">*</span></label>
<div class="col-md-6 col-sm-6">
<asp:FileUpload ID="FileUpload1" runat="server" class="form-control" /></div>
</div>

    <div class="field item form-group">
<label class="col-form-label col-md-3 col-sm-3  label-align">Upload Identity Card <span class="required">*</span></label>
<div class="col-md-6 col-sm-6">
<asp:FileUpload ID="FileUpload2" runat="server" class="form-control" /></div>
</div>
    <font color="red" style="margin-left: 45%;"> <B>All File should be pdf formate only * Size less than 1 mb.</B></font>
<div class="ln_solid">
<div class="form-group">
<div class="col-md-6 offset-md-3">
    <asp:Button class="btn btn-primary" ID="Button2" OnClick="profileMigrat" Text="Add New Nodal Officer" runat="server" OnClientClick= "return validate();"/>
    <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
<asp:Button ID="closeBTN" class="btn btn-success" OnClick="closebtn" Text="Resat" runat="server" />
</div>
</div>
</div>
</form>
</div>
</div>
</div>
</div>
</div>

     <div class="col-md-6 col-sm-12" id="MYotpPM" style="margin-top: -35%; color: white; background-color: #032d5a; margin-left: 24%;" runat="server" visible="false" >
                      <asp:Button ID="btnclose" runat="server" OnClick="closebtn" text="X" style="color: red;"/>
                            <div class="x_panel" style="background-color: #094c94;">
                                <div class="x_title">
                                    <h2 style="margin-left: 10%;">Plesae enter OTP we have sent you in your email ID</h2>
                                    <div class="clearfix"></div>
                                </div>
                                <div class="x_content">
                                    <form class="form-horizontal form-label-left">
                                        <div class="form-group row">
                                            <div class="col-sm-9" style="margin-left: 12%;">
                                                <div class="input-group">
                                                     <asp:TextBox runat="server" ID="txtotpVery" placeholder="OTP (6 Digit)" Class="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                                   <span class="input-group-btn">
                                                      
                                                        <asp:LinkButton class="btn btn-primary go-class" Text="Verify OTP" id="btn_VOTP" OnClick="btn_VOTP_Click" OnClientClick= "return validate();" runat="server"/>
                                                       <asp:Label ID="mylable" runat="server" Text=""></asp:Label>
                                                    </span>

                                                </div>
                                            </div>
                                        </div>
                                        <div class="divider-dashed"></div>
                                    </form>
                                </div>
                            </div>
                        </div>
</div>

       <asp:HiddenField runat="server" ID="PMotp" />
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
    <script type="text/javascript">
        function validate_disable() {
        }
        function validate() {
            var flag = true;
            if ((document.getElementById("<%=txtotpVery.ClientID%>").value.trim() == '') || (document.getElementById("<%=txtotpVery.ClientID%>").value.trim() == undefined)) {
                alert('Please enter OTP');
                flag = false;
            }

            return flag;
        }

    </script>
    <script type="text/javascript">
        function ValidNumeric() {

            var charCode = (event.which) ? event.which : event.keyCode;
            if (charCode >= 10 && charCode <= 10) { return true; }
            else { return false; }
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

          function validate_disable() {

          }
          function validate() {
              var flag = true;
              if ((document.getElementById("<%=NolOffName.ClientID%>").value.trim() == '') || (document.getElementById("<%=NolOffName.ClientID%>").value.trim() == undefined)) {
                  alert('Nodal officer cannot be empty');
                  flag = false;
              }
              else if ((document.getElementById("<%=VEmailID.ClientID%>").value.trim() == '') || (document.getElementById("<%=VEmailID.ClientID%>").value.trim() == undefined)) {
                  alert('Please enter emailID');
                  flag = false;
              }

              else if ((document.getElementById("<%=VmobileNO.ClientID%>").value.trim() == '') || (document.getElementById("<%=VmobileNO.ClientID%>").value.trim() == undefined)) {
                  alert('Please enter Mobile No.');
                flag = false;
            }

            else if ((document.getElementById("<%=NolOffADDRE1.ClientID%>").value.trim() == '') || (document.getElementById("<%=NolOffADDRE1.ClientID%>").value.trim() == undefined)) {
                  alert('Please enter Address Line1');
                  flag = false;
              }

              return flag;

               else if ((document.getElementById("<%=NolOffADDRE2.ClientID%>").value.trim() == '') || (document.getElementById("<%=NolOffADDRE2.ClientID%>").value.trim() == undefined)) {
                  alert('Please enter Address Line2');
                  flag = false;
              }

              return flag;

          }

      </script>

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
</asp:Content>

