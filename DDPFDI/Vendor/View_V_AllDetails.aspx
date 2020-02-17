<%@ Page Language="C#" AutoEventWireup="true" CodeFile="View_V_AllDetails.aspx.cs" Inherits="Vendor_View_V_AllDetails" MasterPageFile="~/Vendor/VendorMaster.master" %>

<asp:Content ID="ConHead" runat="server" ContentPlaceHolderID="head">
    <script type="text/javascript">
        function showPopup() {
            $('#VendorDetail').modal('show');
        }
    </script>
    <style>
        .sideNavbar ul {
            display: flex;
            flex-direction: column;
            box-shadow: 0 0 4px rgba(0,0,0,.2);
        }

            .sideNavbar ul li {
                width: 100% !important;
            }

                .sideNavbar ul li a {
                    border: 1px 0 0 0 solid #eee !important;
                }

                .sideNavbar ul li.active a {
                    background: #eee !important;
                    border: 0 !important;
                }

        .sideContent {
            box-shadow: 0 0 4px rgba(0,0,0,0.2);
            padding: 10px;
            height: 100%;
        }
    </style>
</asp:Content>
<asp:Content ID="Innercontent" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <asp:ScriptManager ID="sc" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="up" runat="server">
        <ContentTemplate>
            <div class="content oem-content">
                <div class="sideBg">
                    <div class="row">
                        <div class="col-md-12 padding_0">
                            <div id="divHeadPage" runat="server"></div>
                        </div>
                    </div>
                    <div class="container">
                        <div class="cacade-forms">
                            <div class="clearfix mt10"></div>
                            <div class="text-center">
                                <asp:Label ID="lbltotalcount" runat="server" CssClass="label label-info"></asp:Label>
                            </div>
                            <div class="clearfix mt10"></div>
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="sideNavbar">
                                        <ul class="nav nav-tabs nav-justified md-tabs indigo" id="myTabJust" role="tablist">
                                            <li class="nav-item active">
                                                <a class="nav-link" id="home-tab-just" data-toggle="tab" href="#home-just" role="tab" aria-controls="home-just"
                                                    aria-selected="true">General Informatrion</a>
                                            </li>
                                            <li class="nav-item">
                                                <a class="nav-link" id="profile-tab-just" data-toggle="tab" href="#profile-just" role="tab" aria-controls="profile-just"
                                                    aria-selected="false">Company Information</a>
                                            </li>
                                            <li class="nav-item">
                                                <a class="nav-link" id="contact-tab-just" data-toggle="tab" href="#contact-just" role="tab" aria-controls="contact-just"
                                                    aria-selected="false">Defence Store</a>
                                            </li>
                                            <li class="nav-item">
                                                <a class="nav-link" id="A1" data-toggle="tab" href="#contact-just" role="tab" aria-controls="contact-just"
                                                    aria-selected="false">Registration no details</a>
                                            </li>
                                            <li class="nav-item">
                                                <a class="nav-link" id="A2" data-toggle="tab" href="#contact-just" role="tab" aria-controls="contact-just"
                                                    aria-selected="false">Financial Information</a>
                                            </li>
                                            <li class="nav-item">
                                                <a class="nav-link" id="A3" data-toggle="tab" href="#contact-just" role="tab" aria-controls="contact-just"
                                                    aria-selected="false">CheckList</a>
                                            </li>
                                        </ul>
                                    </div>
                                </div>
                                <div class="col-md-9">
                                    <div class="sideContent">
                                        <div class="tab-content card pt-5" id="myTabContentJust">
                                            <div class="tab-pane fade active in" id="home-just" role="tabpanel" aria-labelledby="home-tab-just">
                                                <h4>Details of General Information or Registration
                                                </h4>
                                                <hr />

                                                <div class="col-sm-5">
                                                    Dpsu Applied :-
                                                </div>
                                                <div class="col-sm-7">
                                                    <asp:Label ID="lbldpsuapplied" runat="server" Text=""></asp:Label>
                                                </div>
                                                <div class="clearfix pb15"></div>
                                                <div class="col-sm-5">
                                                    Pan No :-
                                                </div>
                                                <div class="col-sm-7">
                                                    <asp:Label ID="lblpanno" runat="server" Text=""></asp:Label>
                                                </div>
                                                <div class="clearfix pb15"></div>
                                                <div class="col-sm-5">
                                                    Name of Firm Company :-
                                                </div>
                                                <div class="col-sm-7">
                                                    <asp:Label ID="lblnameoffirmorcompany" runat="server" Text=""></asp:Label>
                                                </div>
                                                <div class="clearfix pb15"></div>
                                                <div class="col-sm-5">
                                                    Registration Category :-
                                                </div>
                                                <div class="col-sm-7">
                                                    <asp:Label ID="lblregistrationcategory" runat="server" Text=""></asp:Label>
                                                </div>
                                                <div class="clearfix pb15"></div>
                                                <div class="col-sm-5">
                                                    Type of Ownership :-
                                                </div>
                                                <div class="col-sm-7">
                                                    <asp:Label ID="lbltypeofownership" runat="server" Text=""></asp:Label>
                                                </div>
                                                <div class="clearfix pb15"></div>
                                                <div class="col-sm-5">
                                                    Business Sector :-
                                                </div>
                                                <div class="col-sm-7">
                                                    <asp:Label ID="lblbuisnesssector" runat="server" Text=""></asp:Label>
                                                </div>
                                                <div class="clearfix pb15"></div>
                                                <h4>Nodal Officer Details</h4>
                                                <hr />
                                                <div class="col-sm-5">
                                                    Nodal officer Name :-
                                                </div>
                                                <div class="col-sm-7">
                                                    <asp:Label ID="lblnodalofficername" runat="server" Text=""></asp:Label>
                                                </div>
                                                <div class="clearfix pb15"></div>
                                                <div class="col-sm-5">
                                                    Nodal officer Email :-
                                                </div>
                                                <div class="col-sm-7">
                                                    <asp:Label ID="lblnodalemail" runat="server" Text=""></asp:Label>
                                                </div>
                                                <div class="clearfix pb15"></div>
                                                <div class="col-sm-5">
                                                    Nodal officer ContactNo :-
                                                </div>
                                                <div class="col-sm-7">
                                                    <asp:Label ID="lblnodalcontact" runat="server" Text=""></asp:Label>
                                                </div>
                                                <div class="clearfix pb15"></div>
                                                <div class="col-sm-5">
                                                    Nodal officer Complete Address :-
                                                </div>
                                                <div class="col-sm-7">
                                                    <asp:Label ID="lbladdress" runat="server" Text=""></asp:Label>
                                                </div>
                                                <div class="col-sm-5">
                                                    Nodal officer City/State/Pincode :-
                                                </div>
                                                <div class="col-sm-7">
                                                    <asp:Label ID="lblcitystatepincode" runat="server" Text=""></asp:Label>
                                                </div>
                                                <div class="clearfix pb15"></div>
                                                <div class="col-sm-5">
                                                    Scale of buisness :-
                                                </div>
                                                <div class="col-sm-7">
                                                    <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                                                </div>
                                                <div class="clearfix pb15"></div>
                                                <div class="col-sm-5">
                                                    OwnerShip :-
                                                </div>
                                                <div class="col-sm-7">
                                                    <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
                                                </div>
                                                <div class="clearfix pb15"></div>
                                                <div class="col-sm-5">
                                                    Percentage of ownership :-
                                                </div>
                                                <div class="col-sm-7">
                                                    <asp:Label ID="Label3" runat="server" Text=""></asp:Label>
                                                </div>
                                                <div class="clearfix pb15"></div>
                                                <div class="col-sm-5">
                                                    MSME certificate issued by competent authorities (NSIC/ DIC/ KVIC/KVIB/ Coir Board, Directorate of Handicraft & Handlooms) :-
                                                </div>
                                                <div class="col-sm-7">
                                                    <asp:Label ID="Label4" runat="server" Text=""></asp:Label>
                                                </div>
                                                <div class="clearfix pb15"></div>
                                                <div class="col-sm-5">
                                                    Date of Incorporation of the Company :-
                                                </div>
                                                <div class="col-sm-7">
                                                    <asp:Label ID="Label5" runat="server" Text=""></asp:Label>
                                                </div>
                                                <div class="clearfix pb15"></div>
                                                <div class="col-sm-5">
                                                    Landline No :-
                                                </div>
                                                <div class="col-sm-7">
                                                    <asp:Label ID="Label6" runat="server" Text=""></asp:Label>
                                                </div>
                                                <div class="clearfix pb15"></div>
                                                <div class="col-sm-5">
                                                    Fax No :-
                                                </div>
                                                <div class="col-sm-7">
                                                    <asp:Label ID="Label7" runat="server" Text=""></asp:Label>
                                                </div>
                                                <div class="clearfix pb15"></div>
                                                <asp:GridView ID="gvnameof" runat="server" CssClass="table table-hover table-responsive" AutoGenerateColumns="false" Style="overflow: scroll;">
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <%#Container.DataItemIndex+1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="EnterNameof" HeaderText="Type" />
                                                        <asp:BoundField DataField="Name" HeaderText="Name" />
                                                        <asp:BoundField DataField="Designation" HeaderText="Designation" />
                                                        <asp:BoundField DataField="MobileNo" HeaderText="Mobile" />
                                                        <asp:BoundField DataField="DIN_No" HeaderText="DIN No" />
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                            <div class="tab-pane fade" id="profile-just" role="tabpanel" aria-labelledby="profile-tab-just">
                                                <p>
                                                    Food truck fixie locavore, accusamus mcsweeney's marfa nulla single-origin coffee squid. Exercitation +1
      labore velit, blog sartorial PBR leggings next level wes anderson artisan four loko farm-to-table craft
      beer twee. Qui photo booth letterpress, commodo enim craft beer mlkshk aliquip jean shorts ullamco ad
      vinyl cillum PBR. Homo nostrud organic, assumenda labore aesthetic magna delectus mollit. Keytar
      helvetica VHS salvia yr, vero magna velit sapiente labore stumptown. Vegan fanny pack odio cillum wes
      anderson 8-bit, sustainable jean shorts beard ut DIY ethical culpa terry richardson biodiesel. Art party
      scenester stumptown, tumblr butcher vero sint qui sapiente accusamus tattooed echo park.
                                                </p>
                                            </div>
                                            <div class="tab-pane fade" id="contact-just" role="tabpanel" aria-labelledby="contact-tab-just">
                                                <p>
                                                    Etsy mixtape wayfarers, ethical wes anderson tofu before they sold out mcsweeney's organic lomo retro
      fanny pack lo-fi farm-to-table readymade. Messenger bag gentrify pitchfork tattooed craft beer, iphone
      skateboard locavore carles etsy salvia banksy hoodie helvetica. DIY synth PBR banksy irony. Leggings
      gentrify squid 8-bit cred pitchfork. Williamsburg banh mi whatever gluten-free, carles pitchfork
      biodiesel fixie etsy retro mlkshk vice blog. Scenester cred you probably haven't heard of them, vinyl
      craft beer blog stumptown. Pitchfork sustainable tofu synth chambray yr.
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
