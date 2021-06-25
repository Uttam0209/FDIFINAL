<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SiteMap.aspx.cs" Inherits="User_SiteMap" MasterPageFile="~/User/MasterPage.master" %>

<asp:Content ID="content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="assets/css/SiteMap.css" />
</asp:Content>
<asp:Content ID="content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <div class="content">
            <h1 class="text-center">Responsive Organization Chart (updated)</h1>
            <figure class="org-chart cf">
                <div class="board ">
                    <ul class="columnOne">
                        <li>
                            <span class="lvl-b p-2">
                                <h5 class="mb-0 text-light">Market page
                                </h5>
                            </span>
                        </li>
                    </ul>
                </div>
                <ul class="departments ">
                    <li class="department ">
                        <span class="lvl-b p-2">
                            <h6 class="mb-0 text-light"><a href="ProductList" class="text-white" target="_blank">Home</a>
                            </h6>
                        </span>
                    </li>
                    <li class="department ">
                        <span class="lvl-b p-2">
                            <h6 class="mb-0 text-light"><a href="About" class="text-white" target="_blank">About Us</a>
                            </h6>
                        </span>
                    </li>
                    <li class="department ">
                        <span class="lvl-b p-2">
                            <h6 class="mb-0 text-light"><a href="Participate" class="text-white" target="_blank">How to participate</a>
                            </h6>
                        </span>
                    </li>
                    <li class="department central">
                        <span class="lvl-b p-2">
                            <h6 class="mb-0 text-light"><a href="#" class="text-white" target="_blank">Documentation</a>
                            </h6>
                        </span>
                        <ul class="sections">
                            <li class="section">
                                <span>
                                    <h6 class="mb-0 text-light"><a href="https://srijandefence.gov.in/Policy&framwork.pdf" class="text-white" target="_blank">Policy and frame work</a>
                                    </h6>
                                </span>
                            </li>
                            <li class="section" style="margin-top: -14px;">
                                <span>
                                    <h6 class="mb-0 text-light"><a href="https://srijandefence.gov.in/UserManualPublicDomain.pdf" class="text-white" target="_blank">User manual</a>
                                    </h6>
                                </span>
                            </li>
                            <li class="section" style="margin-top: -32px;">
                                <span>
                                    <h6 class="mb-0 text-light"><a href="FAQs" class="text-white" target="_blank">faq</a>
                                    </h6>
                                </span>
                            </li>
                        </ul>
                    </li>
                    <li class="department ">
                        <span class="lvl-b p-2">
                            <h6 class="mb-0 text-light"><a href="https://www.makeinindiadefence.gov.in/" class="text-white" target="_blank">Make in India defence portal</a>
                            </h6>
                        </span>
                    </li>
                    <li class="department ">
                        <span class="lvl-b p-2">
                            <h6 class="mb-0 text-light"><a href="FeedBack" class="text-white" target="_blank">feedback</a>
                            </h6>
                        </span>
                    </li>
                    <li class="department login">
                        <span class="lvl-b p-2">
                            <h6 class="mb-0 text-light"><a href="Login" class="text-white" target="_blank">Login</a>
                            </h6>
                        </span>
                    </li>
                </ul>
            </figure>
            <div runat="server" id="dpsudivlogin">
                <figure class="org-chart cf">
                    <div class=" boards">
                        <ul class="columnOne">
                            <li>
                                <span class="lvl-b p-2">
                                    <h6 class="mb-0 text-light">After login
                                    </h6>
                                </span>
                            </li>
                        </ul>
                    </div>
                    <ul class="PTp">
                        <li class="department central cms" style="margin-left: 8%;">
                            <span class="lvl-b p-2">
                                <h6 class="mb-0 text-light">CMS
                                </h6>
                            </span>
                            <ul class="sections">
                                <li class="section p-2">
                                    <span class="p-2">
                                        <h6 class="mb-0 text-light"><a href="Dashboard" class="text-white" target="_blank">Dashboard</a>
                                        </h6>
                                    </span>
                                </li>

                            </ul>
                        </li>

                        <li class="department central add" style="margin-left: 4%;">
                            <span class="lvl-b p-2">
                                <h6 class="mb-0 text-light">Company MASTER
                                </h6>
                            </span>
                            <ul class="sections">
                                <li class="section">
                                    <span class="p-2">
                                        <h6 class="mb-0 text-light"><a href="AddMasterCompany" class="text-white" target="_blank">ADD</a>
                                        </h6>
                                    </span>
                                </li>
                                <li class="section">
                                    <span class="p-2">
                                        <h6 class="mb-0 text-light"><a href="Add-Designation" class="text-white" target="_blank">ADD designation</a>
                                        </h6>
                                    </span>
                                </li>
                                <li class="section">
                                    <span class="p-2">
                                        <h6 class="mb-0 text-light"><a href="Add-Nodal" class="text-white" target="_blank">Add Nodal</a>
                                        </h6>
                                    </span>
                                </li>
                                <li class="section" style="margin-top: -25px;">
                                    <span class="p-2">
                                        <h6 class="mb-0 text-light"><a href="Detail-Company" class="text-white" target="_blank">VIEW</a>
                                        </h6>
                                    </span>
                                </li>
                                <li class="section" style="margin-top: -25px;">
                                    <span class="p-2">
                                        <h6 class="mb-0 text-light"><a href="View-Designation" class="text-white" target="_blank">Vive Designation</a>
                                        </h6>
                                    </span>
                                </li>
                                <li class="section" style="margin-top: -25px;">
                                    <span class="p-2">
                                        <h6 class="mb-0 text-light"><a href="View-NodalOfficer" class="text-white" target="_blank">View Nodal Officer</a>
                                        </h6>
                                    </span>
                                </li>
                            </ul>
                        </li>

                        <li class="department central add" style="margin-left: 4%;">
                            <span class="lvl-b p-2">
                                <h6 class="mb-0 text-light">CATEGORY MASTER
                                </h6>
                            </span>
                            <ul class="sections">
                                <li class="section">
                                    <span class="p-2">
                                        <h6 class="mb-0 text-light"><a href="Company-Category" class="text-white" target="_blank">DROP DOWN MENU</a>
                                        </h6>
                                    </span>
                                </li>
                                <li class="section" style="margin-top: -8px;">
                                    <span class="p-2">
                                        <h6 class="mb-0 text-light"><a href="View-Category" class="text-white" target="_blank">VIEW</a>
                                        </h6>
                                    </span>
                                </li>
                            </ul>
                        </li>

                        <li class="department central Pp" style="margin-left: 4%;">
                            <span class="lvl-b p-2">
                                <h6 class="mb-0 text-light">PRODUCT MASTER
                                </h6>
                            </span>
                            <ul class="sections">
                                <li class="section">
                                    <span class="p-2">
                                        <h6 class="mb-0 text-light">INDIGINIZED
                                        </h6>
                                    </span>
                                </li>
                                <li class="section" style="margin-top: -25px;">
                                    <span class="p-2">
                                        <h6 class="mb-0 text-light"><a href="AddProduct" class="text-white" target="_blank">ADD</a>
                                        </h6>
                                    </span>
                                </li>
                                <li class="section" style="margin-top: -21px;">
                                    <span class="p-2">
                                        <h6 class="mb-0 text-light"><a href="View-Product" class="text-white" target="_blank">VIEW</a>
                                        </h6>
                                    </span>
                                </li>
                                <li class="section" style="margin-top: -19px;">
                                    <span class="p-2">
                                        <h6 class="mb-0 text-light"><a href="UExcel" class="text-white" target="_blank">BULK UPLOAD</a>
                                        </h6>
                                    </span>
                                </li>
                                <li class="section" style="margin-top: -26px;">
                                    <span class="p-2">
                                        <h6 class="mb-0 text-light"><a href="ProdVerifiUpdate" class="text-white" target="_blank">PRODUCT VERIFICATION</a>
                                        </h6>
                                    </span>
                                </li>
                                <li class="section" style="margin-top: -8px;">
                                    <span class="p-2">
                                        <h6 class="mb-0 text-light"><a href="InterestedVendor" class="text-white" target="_blank">INTERESTING VENDOR</a>
                                        </h6>
                                    </span>
                                </li>
                            </ul>
                        </li>

                        <li class="department central ReportC" style="margin-left: 4%;">
                            <span class="lvl-b p-2">
                                <h6 class="mb-0 text-light">Report
                                </h6>
                            </span>
                            <ul class="sections">
                                <li class="section">
                                    <span class="p-2">
                                        <h6 class="mb-0 text-light"><a href="PReport2" class="text-white" target="_blank">Progress report</a>
                                        </h6>
                                    </span>
                                </li>
                                <li class="section" style="margin-top: -26px;">
                                    <span class="p-2">
                                        <h6 class="mb-0 text-light"><a href="SuccessStoryupdate" class="text-white" target="_blank">Success story</a>
                                        </h6>
                                    </span>
                                </li>
                                <li class="section" style="margin-top: -25px;">
                                    <span class="p-2">
                                        <h6 class="mb-0 text-light"><a href="Summery" class="text-white" target="_blank">Summary details</a>
                                        </h6>
                                    </span>
                                </li>
                                <li class="section" style="margin-top: -8px;">
                                    <span class="p-2">
                                        <h6 class="mb-0 text-light"><a href="Make2Report" class="text-white" target="_blank">Make 2 category</a>
                                        </h6>
                                    </span>
                                </li>
                                <li class="section" style="margin-top: -25px;">
                                    <span class="p-2">
                                        <h6 class="mb-0 text-light"><a href="CategoryWiseRep" class="text-white" target="_blank">Category waise report</a>
                                        </h6>
                                    </span>
                                </li>
                                <li class="section" style="margin-top: -8px;">
                                    <span class="p-2">
                                        <h6 class="mb-0 text-light"><a href="SONOIndig" class="text-white" target="_blank">Supply order/no indigined</a>
                                        </h6>
                                    </span>
                                </li>
                                <li class="section" style="margin-top: -8px;">
                                    <span class="p-2">
                                        <h6 class="mb-0 text-light"><a href="EOINOSOINDIG" class="text-white" target="_blank">eoi/no supply order/indigenized</a>
                                        </h6>
                                    </span>
                                </li>
                            </ul>
                        </li>
                    </ul>
                </figure>
            </div>
            <div class="clearfix mt-2"></div>
        </div>
    </div>
</asp:Content>
