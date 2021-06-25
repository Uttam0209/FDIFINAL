<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/User/MasterPage.master" CodeFile="Feedback.aspx.cs" Inherits="User_Feedback" %>

<asp:Content ID="content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">



    <div class="container">
        <div class="row text-align-center d-flex justify-content-between">
            <div class="col-md-3 my-2 box1">
                <img class="card-img-top md-auto" src="User/Uassets/Images/gmail.png" />
                <h4>Have any Query?</h4>
                <p><b>For technical related Query(website running, SMS, etc) with portal kindly Email</b></p>
                <p>helpdesk-dpit@ddpmod.gov.in</p>
                <p><b>For general Query / Feedback please Mail at</b></p>
                <p>shrishkumar.ofb@ofb.gov.in</p>
            </div>

            <div class="col-md-3 my-2 box1">
                <img class="card-img-top md-auto" src="User/Uassets/Images/phone.png" />
                <h4>Help Desk</h4>
                <p><b>For technical related Query kindly call at</b></p>
                <p>+011-20836145</p>
                <p><b>For other Query / Feedback kindly call at</b></p>
                <p>+011-23019066</p>
            </div>

            <div class="col-md-3 my-2 box1">
                <img class="card-img-top" src="User/Uassets/Images/address.png" />
                <h4>Office Address</h4>
                <p><b>For technical Query  may you reach us at</b></p>
                <p>DPIT 602B 6th floor, Konnectus building New Delhi 110001</p>
                <p><b>For other Query / Feedback may you reach us at</b></p>
                <p>OSDSK DDP Room No. 95 G-Block New Delhi 110011</p>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-4 my-3 Get_In_Touch">
                <h4 class="text-center mt-3 mb-lg-5 pb-lg-4 contact">Your Suggestions are Welcome</h4>
                <div class="contact_form">
                    <div class="form-group">
                        <label for="usr">Name:</label>
                        <asp:TextBox ID="TxtBxFirstNm" class="form-control" placeholder="Your Name" name="username" runat="server" required></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label for="email">Email:</label>
                        <asp:TextBox ID="Txtemail" class="form-control" placeholder="Email Id" name="email" TextMode="Email" runat="server" required></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label for="contact">Contact No:</label>
                        <asp:TextBox ID="Txtcontact" class="form-control" placeholder="Contact Number" MaxLength="10" onkeypress="return isNumberKeyOutDecimal(event)" name="contact" runat="server" required></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label for="company">Company Name:</label>
                        <asp:DropDownList ID="ddlcomp" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlcompany_SelectedIndexChanged" CssClass="custom-select"></asp:DropDownList>
                        <asp:TextBox ID="Txtnodalid" class="form-control" Visible="false" placeholder="nodal Id" name="modalid" TextMode="Email" runat="server"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label for="comment">Message</label>
                        <asp:TextBox runat="server" ID="TxtBxDesc" TextMode="MultiLine" Rows="5" class="form-control" placeholder="Type Your Message"></asp:TextBox>
                    </div>
                    <div class="text-center">
                        <asp:Button ID="BtnSave" Text="Save" type="submit" class="btn bg-primary text-white mt-4" runat="server" OnClick="BtnSave_Click" />
                    </div>
                </div>
            </div>
            <div class="col-lg-8 my-3 Contact_Persons overflow-auto">
                <h4 class="text-center my-3 contact">Contact Persons</h4>
                <p style="font-size: 14.5px;"><b>For any Query related to general issue of the DPSU/OFB/SHQ, may be done through contact details given below.</b></p>
                <p style="font-size: 14px;"><b>Note:</b> For any Query related to indiginization, may be done through contact details given under each item.</p>

                <table id="table1" class="table table-hover table-risponsive">
                    <thead>
                        <tr>
                            <th style="width: 60px;">S.No.</th>
                            <th>Company</th>
                            <th>Name of Nodal Officer</th>
                            <th>Email</th>
                            <th>Phone/Fax</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr class="text-dark">
                            <td>1.</td>
                            <td>HAL</td>
                            <td>Piyush Kumar Sinha</td>
                            <td>gm.indg@hal-india.com</td>
                            <td>080-22320664</td>
                        </tr>
                        <tr>
                            <td>2.</td>
                            <td>HSL</td>
                            <td>Rajesh Pandey</td>
                            <td>dgmmdo@hslvizag.in</td>
                            <td>0891-2577502</td>
                        </tr>
                        <tr>
                            <td>3.</td>
                            <td>MDL</td>
                            <td>Amit Nabira</td>
                            <td>anabira@mazdock.com</td>
                            <td>022-23782114</td>
                        </tr>
                        <tr>
                            <td>4.</td>
                            <td>GSL</td>
                            <td>D S Patekar</td>
                            <td>dspatekar@goashipyard.com</td>
                            <td>0832- 2514194</td>
                        </tr>
                        <tr>
                            <td>5.</td>
                            <td>BDL</td>
                            <td>Ch. Ramesh Babu</td>
                            <td>bdlmnr@bdl-india.in</td>
                            <td>0402- 4340170</td>
                        </tr>
                        <tr>
                            <td>6.</td>
                            <td>BEL</td>
                            <td>Manoj Yadav</td>
                            <td>manojyadav@bel.co.in</td>
                            <td>-/-</td>
                        </tr>
                        <tr>
                            <td>7.</td>
                            <td>BEML</td>
                            <td>S K Saha</td>
                            <td>edq@beml.co.in</td>
                            <td>-/-</td>
                        </tr>
                        <tr>
                            <td>8.</td>
                            <td>GRSE</td>
                            <td>Ratan Gulshan</td>
                            <td>ratan.gulshan@grse.co.in</td>
                            <td>-/-</td>
                        </tr>
                        <tr>
                            <td>9.</td>
                            <td>MIDHANI</td>
                            <td>Santanu Saha</td>
                            <td>ssaha@midhani-india.in</td>
                            <td>-/-</td>
                        </tr>
                        <tr>
                            <td>10.</td>
                            <td>OFB</td>
                            <td>P K Dash</td>
                            <td>pkdash.ofb@ofb.gov.in</td>
                            <td>033-22107627</td>
                        </tr>
                        <tr>
                            <td>11.</td>
                            <td>SHQ (AIR FORCE)</td>
                            <td>Gp Cap P K Anand</td>
                            <td>doi.1973@gov.in</td>
                            <td>-/-</td>
                        </tr>
                        <tr>
                            <td>12.</td>
                            <td>SHQ (ARMY)</td>
                            <td>-/-</td>
                            <td>doi-army@nic.in</td>
                            <td>011-26168620</td>
                        </tr>
                        <tr style="border-bottom: 1px solid #e3e9ef;">
                            <td>13.</td>
                            <td>SHQ (NAVY)</td>
                            <td>Cmde (Indigenization)</td>
                            <td>doi-navy@nic.in</td>
                            <td>011-24108377</td>
                        </tr>
                    </tbody>
                </table>


            </div>

        </div>
    </div>



    <script src="User/Uassets/js/jquery-3.4.1.min.js"></script>
    <script type="text/javascript">
        function isNumberKeyOutDecimal(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode;
            if (charCode > 31
                && (charCode < 48 || charCode > 57))
                return false;
            return true;
        }
    </script>
</asp:Content>
