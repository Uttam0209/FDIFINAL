<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Industry.aspx.cs" Inherits="Admin_Industry" MasterPageFile="~/Admin/MasterPage.master" %>

<asp:Content ID="head" runat="server" ContentPlaceHolderID="head"></asp:Content>

<asp:Content ID="inner" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <asp:ScriptManager runat="server" ID="sc"></asp:ScriptManager>
    <asp:HiddenField ID="hidCompanyRefNo" runat="server" />
    <asp:HiddenField ID="hidType" runat="server" />
    <div class="content oem-content">
        <div class="sideBg">
            <div class="row">
                <div class="col-md-12 padding_0">
                    <div id="divHeadPage" runat="server"></div>
                    <div id="ContentPlaceHolder1_divHeadPage">
                        <ul class="breadcrumb">
                            <li class=""><span>Company Master </span></li>
                            <li class=""><span>Add </span></li>
                            <li class=""><span>Company</span></li>
                        </ul>
                    </div>
                </div>
            </div>
            <div class="tabing-section">
                <ul class="nav nav-tabs">
                    <li class="active"><a data-toggle="tab" href="#gi">General Information</a></li>
                    <li><a data-toggle="tab" href="#pro">Profile</a></li>
                    <li><a data-toggle="tab" href="#spez">Specialization</a></li>
                    <li><a data-toggle="tab" href="#pd">Project Details</a></li>
                    <li><a data-toggle="tab" href="#mp">Manufacturing Plant</a></li>
                </ul>

                <div class="tab-content">
                    <div id="gi" class="tab-pane fade in active">
                        <div class="section-pannel">
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group live-status-box">
                                        <label>
                                            MSME
                                      <span class="checkbox-box productalreadylabel">
                                          <input type="radio" name="msme" />No
                                          <input type="radio" class="yes" name="msme" />Yes
                                      </span>
                                        </label>
                                        <input type="text" class="form-control" placeholder="MSME Registration No" />
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group live-status-box">
                                        <label>
                                            Start Up
                                      <span class="checkbox-box productalreadylabel">
                                          <input type="radio" name="startup" />No
                                          <input type="radio" class="yes" name="startup" />Yes
                                      </span>
                                        </label>
                                        <input type="text" class="form-control" placeholder="Start Up Registration No" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="section-pannel">
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Company Name</label>
                                        <input type="text" class="form-control" />
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Type of Company</label>
                                        <select class="form-control">
                                            <option>Select</option>
                                            <option>Private Ltd Company</option>
                                            <option>Public Ltd Company</option>
                                            <option>Sole Proprietorship</option>
                                            <option>PartnerShip</option>
                                            <option>Cooperatives</option>
                                            <option>Limited Liability Partnership (LLP)</option>
                                        </select>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Company Website</label>
                                        <input type="text" class="form-control" />
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Company Regsitration Number</label>
                                        <div class="multiinput">
                                            <input type="text" class="form-control" />
                                            <select class="form-control">
                                                <option>Year</option>
                                            </select>
                                        </div>

                                    </div>

                                </div>
                            </div>
                        </div>
                        <div class="section-pannel">
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>
                                            Quality System Certification:
                                          <span class="checkbox-box productalreadylabel">
                                              <input type="radio" name="startup" />No
                                          <input type="radio" class="yes" name="startup" />Yes
                                          </span>

                                        </label>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Number of Permanant Employees</label>
                                        <div class="multiinput">
                                            <input type="text" class="form-control" placeholder="No. of Technical " />
                                            <input type="text" class="form-control" placeholder="No. of Administrative" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="section-pannel">
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Address</label>
                                        <textarea class="form-control" style="height: 30px;"></textarea>
                                    </div>

                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>City</label>
                                        <input type="text" class="form-control" />
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Pincode</label>
                                        <input type="text" class="form-control" />
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>State</label>
                                        <select class="form-control">
                                            <option>Select</option>
                                        </select>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="section-pannel">
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Phone No</label>
                                        <input type="text" class="form-control" />
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Fax</label>
                                        <input type="text" class="form-control" />
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Email</label>
                                        <input type="text" class="form-control" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="section-pannel">
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>PAN</label>
                                        <input type="text" class="form-control" />
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>GST NUmber</label>
                                        <input type="text" class="form-control" />
                                    </div>
                                </div>

                            </div>
                        </div>
                        <div class="section-pannel">
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Annual Turn Over (In last 3 years)</label>
                                        <input type="text" class="form-control" />
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Net Worth (2017 - 2018)</label>
                                        <input type="text" class="form-control" />
                                    </div>
                                </div>

                            </div>
                        </div>

                    </div>
                    <div id="pro" class="tab-pane fade">
                        <div class="section-pannel">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group live-status-box">
                                        <label>
                                            Wehther holds Defense Industry License
                                      <span class="checkbox-box productalreadylabel">
                                          <input type="radio" name="license" />No
                                          <input type="radio" class="yes" name="license" />Yes
                                      </span>
                                        </label>

                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>License No</label>
                                        <input type="text" class="form-control">
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Date of Issue</label>
                                        <input type="text" class="form-control">
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Item</label>
                                        <input type="text" class="form-control">
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Product Manufactured Location</label>
                                        <input type="text" class="form-control">
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <button class="btn btn-primary">Add</button>
                                    </div>
                                </div>

                                <div class="col-md-12">
                                    <table class="table responsive no-wrap table-hover manage-user Grid">
                                        <tr>
                                            <th>S.No</th>
                                            <th>License No</th>
                                            <th>Date of Issue</th>
                                            <th>Item</th>
                                            <th>Product Manufactured Location</th>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                        <div class="section-pannel">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label>Defense Export (In Last 3 Years)</label>
                                    </div>
                                    <div>
                                        <table class="table responsive no-wrap table-hover manage-user Grid">
                                            <tr>
                                                <th>S.No</th>
                                                <th>Year</th>
                                                <th>Value (Rs. in Crore)</th>
                                            </tr>
                                            <tr>
                                                <td>1
                                                </td>
                                                <td>
                                                    <select class="form-control">
                                                        <option>2015-2016</option>
                                                        <option>2016-2017</option>
                                                        <option>2017-2018</option>
                                                    </select>
                                                </td>
                                                <td></td>

                                            </tr>
                                            <tr>
                                                <td>2
                                                </td>
                                                <td>
                                                    <select class="form-control">
                                                        <option>2015-2016</option>
                                                        <option>2016-2017</option>
                                                        <option>2017-2018</option>
                                                    </select>
                                                </td>
                                                <td></td>

                                            </tr>
                                            <tr>
                                                <td>3
                                                </td>
                                                <td>
                                                    <select class="form-control">
                                                        <option>2015-2016</option>
                                                        <option>2016-2017</option>
                                                        <option>2017-2018</option>
                                                    </select>
                                                </td>
                                                <td></td>

                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="section-pannel">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label>FDI Received (In Last 3 Years)</label>
                                    </div>
                                    <div>
                                        <table class="table responsive no-wrap table-hover manage-user Grid">
                                            <tr>
                                                <th>S.No</th>
                                                <th>Year</th>
                                                <th>Value (Rs. in Crore)</th>
                                            </tr>
                                            <tr>
                                                <td>1
                                                </td>
                                                <td>
                                                    <select class="form-control">
                                                        <option>2015-2016</option>
                                                        <option>2016-2017</option>
                                                        <option>2017-2018</option>
                                                    </select>
                                                </td>
                                                <td>
                                                    <input type="text" class="form-control" />
                                                </td>

                                            </tr>
                                            <tr>
                                                <td>2
                                                </td>
                                                <td>
                                                    <select class="form-control">
                                                        <option>2015-2016</option>
                                                        <option>2016-2017</option>
                                                        <option>2017-2018</option>
                                                    </select>
                                                </td>
                                                <td>
                                                    <input type="text" class="form-control" /></td>

                                            </tr>
                                            <tr>
                                                <td>3
                                                </td>
                                                <td>
                                                    <select class="form-control">
                                                        <option>2015-2016</option>
                                                        <option>2016-2017</option>
                                                        <option>2017-2018</option>
                                                    </select>
                                                </td>
                                                <td>
                                                    <input type="text" class="form-control" /></td>

                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="section-pannel">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label>Defense Production (In Last 3 Years)</label>
                                    </div>
                                    <div>
                                        <table class="table responsive no-wrap table-hover manage-user Grid">
                                            <tr>
                                                <th>S.No</th>
                                                <th>Year</th>
                                                <th>Value (Rs. in Crore)</th>
                                            </tr>
                                            <tr>
                                                <td>1
                                                </td>
                                                <td>
                                                    <select class="form-control">
                                                        <option>2015-2016</option>
                                                        <option>2016-2017</option>
                                                        <option>2017-2018</option>
                                                    </select>
                                                </td>
                                                <td></td>

                                            </tr>
                                            <tr>
                                                <td>2
                                                </td>
                                                <td>
                                                    <select class="form-control">
                                                        <option>2015-2016</option>
                                                        <option>2016-2017</option>
                                                        <option>2017-2018</option>
                                                    </select>
                                                </td>
                                                <td></td>

                                            </tr>
                                            <tr>
                                                <td>3
                                                </td>
                                                <td>
                                                    <select class="form-control">
                                                        <option>2015-2016</option>
                                                        <option>2016-2017</option>
                                                        <option>2017-2018</option>
                                                    </select>
                                                </td>
                                                <td></td>

                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="spez" class="tab-pane fade">
                        <div class="section-pannel">
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>
                                            <input type="checkbox">Mechanical</label>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>
                                            <input type="checkbox">Electrical</label>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>
                                            <input type="checkbox">Civil</label>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>
                                            <input type="checkbox">Electronic</label>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>
                                            <input type="checkbox">Chemical</label>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>
                                            <input type="checkbox">Software Development</label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="pd" class="tab-pane fade">
                        <div class="section-pannel">
                            <div class="row">
                                <div class="col-md-12">
                                    <h4 class="secondary-heading">Details of Projects Executed</h4>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Year</label>
                                        <input type="text" class="form-control">
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Title of Project</label>
                                        <input type="text" class="form-control">
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Discipline/Field/Area</label>
                                        <input type="text" class="form-control">
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Cost of Project</label>
                                        <input type="text" class="form-control">
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Name of the Client</label>
                                        <input type="text" class="form-control">
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Status</label>
                                        <input type="text" class="form-control">
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>% of Import Content</label>
                                        <input type="text" class="form-control">
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Supporting Document</label>
                                        <input type="file" class="form-control">
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <button class="btn btn-primary pull-right">Add</button>
                                    </div>
                                </div>
                            </div>
                            <div>
                                <table class="table responsive no-wrap table-hover manage-user Grid">
                                    <tr>
                                        <th>S.No</th>
                                        <th>Year</th>
                                        <th>Title of Project</th>
                                        <th>Discipline/Field/Area</th>
                                        <th>Cost of Project</th>
                                        <th>Name of the Client</th>
                                        <th>Status</th>
                                        <th>% of Import Content</th>
                                        <th>Supporting Document</th>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                        <div class="section-pannel">
                            <div class="row">
                                <div class="col-md-12">
                                    <h4 class="secondary-heading">Funding/Grants/Awards from Govt. Agencies</h4>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Year</label>
                                        <input type="text" class="form-control">
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Title of Project</label>
                                        <input type="text" class="form-control">
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Amount</label>
                                        <input type="text" class="form-control">
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Agency</label>
                                        <input type="text" class="form-control">
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Purpose</label>
                                        <input type="text" class="form-control">
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Awards/Recognization</label>
                                        <input type="text" class="form-control">
                                    </div>
                                </div>

                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <button class="btn btn-primary pull-right">Add</button>
                                    </div>
                                </div>
                            </div>
                            <div>
                                <table class="table responsive no-wrap table-hover manage-user Grid">
                                    <tr>
                                        <th>S.No</th>
                                        <th>Year</th>
                                        <th>Title of Project</th>
                                        <th>Amount</th>
                                        <th>Agency</th>
                                        <th>Purpose</th>
                                        <th>Awards/Recognization</th>

                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>

                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                    <div id="mp" class="tab-pane fade">
                        <div class="section-pannel">
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Plant Name</label>
                                        <input type="text" class="form-control" />
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Address </label>
                                        <input type="text" class="form-control" />
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>City</label>
                                        <input type="text" class="form-control" />
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Pincode</label>
                                        <input type="text" class="form-control" />
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>State</label>
                                        <select class="form-control">
                                            <option>Select</option>
                                        </select>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>District</label>
                                        <select class="form-control">
                                            <option>Select</option>
                                        </select>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="section-pannel">
                            <div class="row">
                                <div class="col-md-12">
                                    <h4 class="secondary-heading">Product Manufactured</h4>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Product Name</label>
                                        <input type="text" class="form-control">
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Annual Capacity</label>
                                        <input type="text" class="form-control">
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <button class="btn btn-primary pull-right">Add</button>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <table class="table responsive no-wrap table-hover manage-user Grid">
                                        <tr>
                                            <th>S.No</th>
                                            <th>Product Name</th>
                                            <th>Annual Capacity</th>

                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                        <div class="section-pannel">
                            <div class="row">
                                <div class="col-md-5">
                                    <div class="form-group">
                                        <label>Latitude</label>
                                        <input type="text" class="form-control">
                                    </div>
                                </div>
                                <div class="col-md-5">
                                    <div class="form-group">
                                        <label>Longitude</label>
                                        <input type="text" class="form-control">
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label style="display: block">&nbsp;</label>
                                        <button class="btn btn-primary pull-right">Save</button>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <iframe src="https://www.google.com/maps/embed?pb=!1m16!1m12!1m3!1d2965.0824050173574!2d-93.63905729999999!3d41.998507000000004!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!2m1!1sWebFilings%2C+University+Boulevard%2C+Ames%2C+IA!5e0!3m2!1sen!2sus!4v1390839289319" width="100%" height="200" frameborder="0" style="border: 0"></iframe>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-12">
                        <div class="form-group">
                            <input type="submit" class="btn btn-default pull-right" style="margin-right: 10px;" value="Cancel" />
                            <input type="submit" class="btn btn-primary pull-right" value="Save" />

                        </div>
                    </div>
                </div>

            </div>


        </div>
    </div>
</asp:Content>
