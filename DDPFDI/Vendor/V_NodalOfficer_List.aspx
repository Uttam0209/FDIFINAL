<%@ Page Title="" Language="C#" MasterPageFile="~/Vendor/VendorMaster2.master" AutoEventWireup="true" CodeFile="V_NodalOfficer_List.aspx.cs" Inherits="Vendor_V_NodalOfficer_List" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<%-- <div class="content oem-content">--%>
    <div class="right_col" role="main">
        <div class="">
<div class="clearfix"></div>
<div class="row">
<div class="col-md-12 col-sm-12 ">
<div class="x_panel">
<div class="x_title">
<h2>Nodal Officer List&nbsp;<a href="V_Add_NodalOfficer?mu=MRtCwN+7N6dMmohOhVozbQ==&id=vlK1DvIRTlYRbBtbWziaInurr2GjdBWfFx9rihZLlUo="> <input type="button" value="Add Nodel Officer" class="btn btn-primary btn-sm"></a></h2>

<div class="clearfix"></div>
</div>
<div class="x_content">
<div class="row">
<div class="col-sm-12">
<div class="card-box table-responsive">

<table id="datatable-buttons" class="table table-striped table-bordered" style="width:100%">
<thead>
<tr>
<th>Nodal Officer Name</th>
<th>Nodal Officer Email</th>
<th>Nodal Officer Contact</th>
<th>Address</th>
<th>City/State</th>
<th>Pincode</th>
<th>Type</th>
<th>View</th>
<th>Edit</th>
</tr>
</thead>
<tbody>
  <%--  <%
        
        DataTable DtGetRegisVendor1 = Lo.RetriveUsersDetals(VendorID5);
        if (DtGetRegisVendor1.Rows.Count > 0)
        {
            UserrName = DtGetRegisVendor1.Rows[0]["UserName"].ToString();
            userrEmail = DtGetRegisVendor1.Rows[0]["UserEmail"].ToString();
            ContactNo = DtGetRegisVendor1.Rows[0]["UserMobile"].ToString();
            StreetAddress = DtGetRegisVendor1.Rows[0]["Address1"].ToString();
            StreetAddressLine2 = DtGetRegisVendor1.Rows[0]["Address2"].ToString();
            City = DtGetRegisVendor1.Rows[0]["City"].ToString();
            State = DtGetRegisVendor1.Rows[0]["State"].ToString();
            ZipCode = DtGetRegisVendor1.Rows[0]["Postalcode"].ToString();
         %>
<tr>
 <td><%=UserrName%></td>
<td><%=userrEmail%></td>
<td><%=ContactNo%></td>
<td><%=StreetAddress%>,<%=StreetAddressLine2%></td>
<td><%=State%></td>
<td><%=ZipCode%></td>
<td>User</td>
<td><button type="button" class="btn btn-success">View</button></td>
<td><button type="button" class="btn btn-success">Edit</button></td>
</tr>
    <%}%>--%>
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
</asp:Content>

