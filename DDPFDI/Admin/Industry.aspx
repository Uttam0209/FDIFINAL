<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Industry.aspx.cs" Inherits="Admin_Industry" MasterPageFile="~/Admin/MasterPage.master" %>

<asp:Content ID="head" runat="server" ContentPlaceHolderID="head"></asp:Content>
<asp:Content ID="inner" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <asp:HiddenField ID="hidCompanyRefNo" runat="server" />
    <asp:HiddenField ID="hidType" runat="server" />
    <div class="content oem-content">
        <div class="sideBg">
            <div class="row">
                <div class="col-md-12 padding_0">
                    <div id="divHeadPage" runat="server"></div>
                </div>
            </div>
            <div id="exTab3">
                <ul class="nav nav-pills">
                    <li class="active"><a href="#1b" data-toggle="tab">GENERAL INFORMATION</a> </li>
                    <li><a href="#2b" data-toggle="tab">FINANCIAL DETAILS</a> </li>
                    <li><a href="#3b" data-toggle="tab">PROFILE </a></li>
                    <li><a href="#4b" data-toggle="tab">SPECIALIZATION</a> </li>
                    <li><a href="#5b" data-toggle="tab">PROJECT DETAILS</a> </li>
                    <li><a href="#6b" data-toggle="tab">MANUFACTURING PLANT </a></li>
                </ul>
                <div class="tab-content clearfix">
                    <div class="tab-pane active" id="1b">
                        <div class="col-sm-3">
                            <form>
                                <div class="form-group">
                                    <label for="exampleInputEmail1">Company Name</label>
                                    <input type="email" class="form-control" id="exampleInputEmail1" aria-describedby="emailHelp">
                                </div>
                            </form>
                        </div>
                        <div class="col-sm-3">
                            <form>
                                <div class="form-group">
                                    <label for="exampleInputEmail1">Ownership Type</label>
                                    <input type="email" class="form-control" id="Email1" aria-describedby="emailHelp">
                                </div>
                            </form>
                        </div>
                        <div class="col-sm-3">
                            <div class="form-group">
                                <label for="exampleFormControlSelect1">Year of Incorporation</label>
                                <select class="form-control" id="exampleFormControlSelect1">
                                    <option>Select</option>
                                    <option>2</option>
                                    <option>3</option>
                                    <option>4</option>
                                    <option>5</option>
                                </select>
                            </div>
                        </div>
                        <div class="col-sm-3">
                            <form>
                                <div class="form-group">
                                    <label for="exampleInputEmail1">PAN</label>
                                    <input type="email" class="form-control" id="Email2" aria-describedby="emailHelp">
                                </div>
                            </form>
                        </div>
                        <div class="clearfix"></div>
                        <div class="col-sm-3">
                            <form>
                                <div class="form-group">
                                    <label for="exampleInputEmail1">CIN</label>
                                    <input type="email" class="form-control" id="Email3" aria-describedby="emailHelp">
                                </div>
                            </form>
                        </div>
                        <div class="col-sm-3">
                            <form>
                                <div class="form-group">
                                    <label for="exampleInputEmail1">GSTIN</label>
                                    <input type="email" class="form-control" id="Email4" aria-describedby="emailHelp">
                                </div>
                            </form>
                        </div>
                        <div class="col-sm-3">
                            <form>
                                <span class="spanMr">MSME</span>
                                <label class="radio-inline">
                                    <input type="radio" name="optradio" checked>
                                    Yes
                                </label>
                                <label class="radio-inline">
                                    <input type="radio" name="optradio">
                                    No
                                </label>
                                <input type="email" class="form-control" id="Email5" aria-describedby="emailHelp">
                            </form>
                        </div>
                        <div class="col-sm-3">
                            <form>
                                <span class="spanMr">Start Up</span>
                                <label class="radio-inline">
                                    <input type="radio" name="optradio" checked>
                                    Yes
                                </label>
                                <label class="radio-inline">
                                    <input type="radio" name="optradio">
                                    No
                                </label>
                                <input type="email" class="form-control" id="Email6" aria-describedby="emailHelp">
                            </form>
                        </div>
                        <div class="clearfix"></div>
                        <!--- <div class="whitebg m10">
                <div class="col-sm-4 pt20px">
                  <form>
                    <span class="spanMr">Quality System Certification</span>
                    <label class="radio-inline mt5">
                      <input type="radio" name="optradio" checked>
                      Yes </label>
                    <label class="radio-inline">
                      <input type="radio" name="optradio">
                      No </label>
                  </form>
                </div>
                <div class="col-sm-4">
                  <form>
                    <div class="form-group">
                      <label for="exampleInputEmail1">Number of Employees</label>
                      <input type="email" class="form-control brd" id="exampleInputEmail1" aria-describedby="emailHelp" placeholder="No of Technical">
                    </div>
                  </form>
                </div>
                <div class="col-sm-4">
                  <form>
                    <div class="form-group">
                      <label for="exampleInputEmail1"></label>
                      <input type="email" class="form-control brd" id="exampleInputEmail1" aria-describedby="emailHelp" placeholder="No of Administrative" >
                    </div>
                  </form>
                </div>
                <div class="clearfix"></div>
              </div>--->
                        <div class="clearfix"></div>
                        <div class="col-sm-6">
                            <form>
                                <label class="balckColor">Registered Address</label>
                                <textarea class="form-control" cols="3" rows="12"></textarea>
                            </form>
                        </div>
                        <div class="col-sm-6">
                            <form>
                                <div class="form-group">
                                    <label for="exampleInputEmail1" class="balckColor">City </label>
                                    <input type="email" class="form-control" id="Email7" aria-describedby="emailHelp">
                                </div>
                                <div class="form-group">
                                    <label for="exampleInputEmail1">District</label>
                                    <input type="email" class="form-control" id="Email8" aria-describedby="emailHelp">
                                </div>
                                <div class="form-group">
                                    <label for="exampleInputEmail1">State</label>
                                    <input type="email" class="form-control" id="Email9" aria-describedby="emailHelp">
                                </div>
                                <div class="form-group">
                                    <label for="exampleInputEmail1">Pincode</label>
                                    <input type="email" class="form-control" id="Email10" aria-describedby="emailHelp">
                                </div>
                                <div class="clearfix"></div>
                            </form>
                        </div>
                        <div class="col-sm-12 text-right">
                            <button type="submit" class="btn btn-primary">Submit</button>
                            <button type="submit" class="btn btn-default">Cancel</button>
                            <div class="clearfix"></div>
                            <br>
                            <br>
                        </div>
                    </div>

                    <!------------- tab 1 -------------------->
                    <div class="tab-pane" id="2b">
                        <div class="col-sm-6">
                            <h5>Annual Turn Over ( In Last 3 Year )</h5>
                            <div class="table-responsive">
                                <table class="table table-striped ">
                                    <thead class="tableHead">
                                        <tr>
                                            <th scope="col">Year</th>
                                            <th scope="col">Value ( Rs. In Crore )</th>
                                        </tr>
                                    </thead>
                                    <tr>
                                        <td>
                                            <div class="form-group">

                                                <select class="form-control" id="Select1">
                                                    <option>Select</option>
                                                    <option>2</option>
                                                    <option>3</option>
                                                    <option>4</option>
                                                    <option>5</option>
                                                </select>
                                            </div>
                                        </td>
                                        <td>
                                            <form>
                                                <div class="form-group">

                                                    <input type="email" class="form-control" id="Email11" aria-describedby="emailHelp">
                                                </div>
                                            </form>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="form-group">

                                                <select class="form-control" id="Select2">
                                                    <option>Select</option>
                                                    <option>2</option>
                                                    <option>3</option>
                                                    <option>4</option>
                                                    <option>5</option>
                                                </select>
                                            </div>
                                        </td>
                                        <td>
                                            <form>
                                                <div class="form-group">

                                                    <input type="email" class="form-control" id="Email12" aria-describedby="emailHelp">
                                                </div>
                                            </form>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="form-group">

                                                <select class="form-control" id="Select3">
                                                    <option>Select</option>
                                                    <option>2</option>
                                                    <option>3</option>
                                                    <option>4</option>
                                                    <option>5</option>
                                                </select>
                                            </div>
                                        </td>
                                        <td>
                                            <form>
                                                <div class="form-group">

                                                    <input type="email" class="form-control" id="Email13" aria-describedby="emailHelp">
                                                </div>
                                            </form>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <h5>Net Worth ( In Last 3 Year )</h5>
                            <div class="table-responsive">
                                <table class="table table-striped ">
                                    <thead class="tableHead">
                                        <tr>
                                            <th scope="col">Year</th>
                                            <th scope="col">Value ( Rs. In Crore )</th>
                                        </tr>
                                    </thead>
                                    <tr>
                                        <td>
                                            <div class="form-group">

                                                <select class="form-control" id="Select4">
                                                    <option>Select</option>
                                                    <option>2</option>
                                                    <option>3</option>
                                                    <option>4</option>
                                                    <option>5</option>
                                                </select>
                                            </div>
                                        </td>
                                        <td>
                                            <form>
                                                <div class="form-group">

                                                    <input type="email" class="form-control" id="Email14" aria-describedby="emailHelp">
                                                </div>
                                            </form>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="form-group">

                                                <select class="form-control" id="Select5">
                                                    <option>Select</option>
                                                    <option>2</option>
                                                    <option>3</option>
                                                    <option>4</option>
                                                    <option>5</option>
                                                </select>
                                            </div>
                                        </td>
                                        <td>
                                            <form>
                                                <div class="form-group">

                                                    <input type="email" class="form-control" id="Email15" aria-describedby="emailHelp">
                                                </div>
                                            </form>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="form-group">

                                                <select class="form-control" id="Select6">
                                                    <option>Select</option>
                                                    <option>2</option>
                                                    <option>3</option>
                                                    <option>4</option>
                                                    <option>5</option>
                                                </select>
                                            </div>
                                        </td>
                                        <td>
                                            <form>
                                                <div class="form-group">

                                                    <input type="email" class="form-control" id="Email16" aria-describedby="emailHelp">
                                                </div>
                                            </form>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                        <div class="clearfix"></div>
                        <div class="col-sm-6">
                            <h5>Defence Export ( In Last 3 Year </h5>
                            <div class="table-responsive">
                                <table class="table table-striped ">
                                    <thead class="tableHead">
                                        <tr>
                                            <th scope="col">Year</th>
                                            <th scope="col">Value ( Rs. In Crore )</th>
                                        </tr>
                                    </thead>
                                    <tr>
                                        <td>
                                            <div class="form-group">

                                                <select class="form-control" id="Select7">
                                                    <option>Select</option>
                                                    <option>2</option>
                                                    <option>3</option>
                                                    <option>4</option>
                                                    <option>5</option>
                                                </select>
                                            </div>
                                        </td>
                                        <td>
                                            <form>
                                                <div class="form-group">

                                                    <input type="email" class="form-control" id="Email17" aria-describedby="emailHelp">
                                                </div>
                                            </form>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="form-group">

                                                <select class="form-control" id="Select8">
                                                    <option>Select</option>
                                                    <option>2</option>
                                                    <option>3</option>
                                                    <option>4</option>
                                                    <option>5</option>
                                                </select>
                                            </div>
                                        </td>
                                        <td>
                                            <form>
                                                <div class="form-group">

                                                    <input type="email" class="form-control" id="Email18" aria-describedby="emailHelp">
                                                </div>
                                            </form>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="form-group">

                                                <select class="form-control" id="Select9">
                                                    <option>Select</option>
                                                    <option>2</option>
                                                    <option>3</option>
                                                    <option>4</option>
                                                    <option>5</option>
                                                </select>
                                            </div>
                                        </td>
                                        <td>
                                            <form>
                                                <div class="form-group">

                                                    <input type="email" class="form-control" id="Email19" aria-describedby="emailHelp">
                                                </div>
                                            </form>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>

                    <!------------- tab 2 -------------------->
                    <div class="tab-pane" id="3b">
                        <div class="col-sm-12 pt20px">
                            <form>
                                <span class="spanMr">Quality System Certification</span>
                                <label class="radio-inline mt5">
                                    <input type="radio" name="optradio" checked>
                                    Yes
                                </label>
                                <label class="radio-inline">
                                    <input type="radio" name="optradio">
                                    No
                                </label>
                            </form>
                        </div>
                        <div class="col-sm-3">
                            <form>
                                <div class="form-group">
                                    <input type="email" class="form-control brd" id="Email20" aria-describedby="emailHelp" placeholder="License No">
                                </div>
                            </form>
                        </div>
                        <div class="col-sm-3">
                            <form>
                                <div class="form-group">
                                    <input type="email" class="form-control brd" id="Email21" aria-describedby="emailHelp" placeholder="Date of Issue">
                                </div>
                            </form>
                        </div>
                        <div class="col-sm-3">
                            <form>
                                <div class="form-group">
                                    <input type="email" class="form-control brd" id="Email22" aria-describedby="emailHelp" placeholder="Item">
                                </div>
                            </form>
                        </div>
                        <div class="col-sm-3">
                            <form>
                                <div class="form-group">
                                    <input type="email" class="form-control brd" id="Email23" aria-describedby="emailHelp" placeholder="Product Manufactured Location">
                                </div>
                            </form>
                        </div>
                        <div class="clearfix"></div>
                        <div class="col-sm-12">
                            <button type="submit" class="btn btn-primary pull-right">Add</button>
                            <div class="clearfix pb15"></div>
                            <div class="table-responsive">
                                <table class="table table-striped ">
                                    <thead class="tableHead">
                                        <tr>
                                            <th>S. No</th>
                                            <th>License No</th>
                                            <th>Date of Issue</th>
                                            <th>Item</th>
                                            <th>Product Manufactured Location</th>
                                        </tr>
                                    </thead>
                                    <tr>
                                        <td>1</td>
                                        <td>1234</td>
                                        <td>16/6/2019</td>
                                        <td>Bolt</td>
                                        <td>Noida</td>
                                    </tr>
                                    <tr>
                                        <td>1</td>
                                        <td>1234</td>
                                        <td>16/6/2019</td>
                                        <td>Bolt</td>
                                        <td>Noida</td>
                                    </tr>
                                    <tr>
                                        <td>1</td>
                                        <td>1234</td>
                                        <td>16/6/2019</td>
                                        <td>Bolt</td>
                                        <td>Noida</td>
                                    </tr>
                                </table>
                            </div>
                            <br>
                            <br>
                        </div>
                    </div>

                    <!------------- tab 3 -------------------->
                    <div class="tab-pane" id="4b">

                        <form>
                            <div class="col-sm-4">
                                <label class="checkbox-inline">
                                    <input type="checkbox" value="">
                                    Mechanical
                                </label>
                            </div>
                            <div class="col-sm-4">
                                <label class="checkbox-inline">
                                    <input type="checkbox" value="">
                                    Electrical
                                </label>
                            </div>
                            <div class="col-sm-4">
                                <label class="checkbox-inline">
                                    <input type="checkbox" value="">
                                    Civil
                                </label>
                            </div>
                            <div class="clearfix pb15"></div>
                            <div class="col-sm-4">
                                <label class="checkbox-inline">
                                    <input type="checkbox" value="">
                                    Electronic
                                </label>
                            </div>
                            <div class="col-sm-4">
                                <label class="checkbox-inline">
                                    <input type="checkbox" value="">
                                    Chemical
                                </label>
                            </div>
                            <div class="col-sm-4">
                                <label class="checkbox-inline">
                                    <input type="checkbox" value="">
                                    Software Development
                                </label>
                            </div>
                            <div class="clearfix pb15"></div>
                            <div class="col-sm-12 text-right">
                                <button type="submit" class="btn btn-primary">Save</button>
                                <button type="cancle" class="btn btn-default">cancle</button>
                            </div>
                        </form>



                    </div>
                    <div class="tab-pane" id="5b">
                        <div class="col-sm-12">
                            <h4>DETAILS OF PROJECTS EXECUTED</h4>
                        </div>
                        <div class="col-sm-4">
                            <div class="form-group">
                                <label for="exampleInputEmail1" class="balckColor">Year </label>
                                <input type="email" class="form-control" id="Email24" aria-describedby="emailHelp">
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <div class="form-group">
                                <label for="exampleInputEmail1" class="balckColor">Title of Project </label>
                                <input type="email" class="form-control" id="Email25" aria-describedby="emailHelp">
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <div class="form-group">
                                <label for="exampleInputEmail1" class="balckColor">Discipline/Field/Area </label>
                                <input type="email" class="form-control" id="Email26" aria-describedby="emailHelp">
                            </div>
                        </div>
                        <div class="clearfix"></div>
                        <div class="col-sm-4">
                            <div class="form-group">
                                <label for="exampleInputEmail1" class="balckColor">Cost of Project </label>
                                <input type="email" class="form-control" id="Email27" aria-describedby="emailHelp">
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <div class="form-group">
                                <label for="exampleInputEmail1" class="balckColor">Name of the client </label>
                                <input type="email" class="form-control" id="Email28" aria-describedby="emailHelp">
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <div class="form-group">
                                <label for="exampleInputEmail1" class="balckColor">Status </label>
                                <input type="email" class="form-control" id="Email29" aria-describedby="emailHelp">
                            </div>
                        </div>
                        <div class="clearfix "></div>
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label for="exampleInputEmail1" class="balckColor">Status </label>
                                <input type="email" class="form-control" id="Email30" aria-describedby="emailHelp">
                            </div>
                        </div>
                        <div class="col-sm-6">

                            <form>
                                <div class="form-group" style="background: #fff; padding: 5px; margin-top: 25px;">
                                    <input type="file" class="form-control-file" id="exampleFormControlFile1">
                                </div>
                            </form>

                        </div>

                        <div class="col-sm-12 text-right">
                            <button type="submit" class="btn btn-primary">Add</button>
                        </div>

                        <div class="clearfix pb15"></div>
                        <div class="col-sm-12">
                            <div class="table-responsive">

                                <table class="table tableHead">
                                    <thead>
                                        <tr>
                                            <th scope="col">S.No.</th>
                                            <th scope="col">Year</th>
                                            <th scope="col">Title of Project</th>
                                            <th scope="col">Discipline/Field/Area</th>
                                            <th scope="col">Cost of Project</th>
                                            <th scope="col">Name of the client</th>
                                            <th scope="col">Status</th>
                                            <th scope="col">% of Import Content</th>
                                            <th scope="col">Suporting Document</th>
                                        </tr>
                                    </thead>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                </table>
                            </div>

                        </div>
                        <div class="clearfix pb15"></div>



                        <div class="col-sm-12">
                            <h4>DETAILS OF PROJECTS EXECUTED</h4>
                        </div>
                        <div class="col-sm-4">
                            <div class="form-group">
                                <label for="exampleInputEmail1" class="balckColor">Year </label>
                                <input type="email" class="form-control" id="Email31" aria-describedby="emailHelp">
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <div class="form-group">
                                <label for="exampleInputEmail1" class="balckColor">Title of Project </label>
                                <input type="email" class="form-control" id="Email32" aria-describedby="emailHelp">
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <div class="form-group">
                                <label for="exampleInputEmail1" class="balckColor">Discipline/Field/Area </label>
                                <input type="email" class="form-control" id="Email33" aria-describedby="emailHelp">
                            </div>
                        </div>
                        <div class="clearfix"></div>
                        <div class="col-sm-4">
                            <div class="form-group">
                                <label for="exampleInputEmail1" class="balckColor">Cost of Project </label>
                                <input type="email" class="form-control" id="Email34" aria-describedby="emailHelp">
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <div class="form-group">
                                <label for="exampleInputEmail1" class="balckColor">Name of the client </label>
                                <input type="email" class="form-control" id="Email35" aria-describedby="emailHelp">
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <div class="form-group">
                                <label for="exampleInputEmail1" class="balckColor">Status </label>
                                <input type="email" class="form-control" id="Email36" aria-describedby="emailHelp">
                            </div>
                        </div>
                        <div class="clearfix "></div>
                        <div class="col-sm-12">
                            <div class="table-responsive">

                                <table class="table tableHead">
                                    <thead>
                                        <tr>
                                            <th scope="col">S.No.</th>
                                            <th scope="col">Year</th>
                                            <th scope="col">Title of Project</th>
                                            <th scope="col">Amount</th>
                                            <th scope="col">Agency</th>
                                            <th scope="col">Purpose</th>
                                            <th scope="col">Awards/Recognization</th>

                                        </tr>
                                    </thead>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>

                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>

                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>

                                    </tr>
                                </table>
                            </div>

                        </div>
                        <div class="col-sm-12 text-right">
                            <button type="submit" class="btn btn-primary">Save</button>
                            <button type="submit" class="btn btn-primary">Cancel</button>
                        </div>
                    </div>
                    <div class="tab-pane" id="6b">

                        <div class="col-sm-4">
                            <form>
                                <div class="form-group">
                                    <label for="exampleInputEmail1">Plant Name</label>
                                    <input type="email" class="form-control" id="Email37" aria-describedby="emailHelp">
                                </div>
                            </form>
                        </div>
                        <div class="col-sm-4">
                            <form>
                                <div class="form-group">
                                    <label for="exampleInputEmail1">Address</label>
                                    <input type="email" class="form-control" id="Email38" aria-describedby="emailHelp">
                                </div>
                            </form>
                        </div>
                        <div class="col-sm-4">
                            <div class="form-group">
                                <label for="exampleFormControlSelect1">City</label>
                                <select class="form-control" id="Select10">
                                    <option>Select</option>
                                    <option>2</option>
                                    <option>3</option>
                                    <option>4</option>
                                    <option>5</option>
                                </select>
                            </div>
                        </div>
                        <div class="clearfix"></div>
                        <div class="col-sm-4">
                            <form>
                                <div class="form-group">
                                    <label for="exampleInputEmail1">District</label>
                                    <input type="email" class="form-control" id="Email39" aria-describedby="emailHelp">
                                </div>
                            </form>
                        </div>
                        <div class="col-sm-4">
                            <form>
                                <div class="form-group">
                                    <label for="exampleInputEmail1">State</label>
                                    <input type="email" class="form-control" id="Email40" aria-describedby="emailHelp">
                                </div>
                            </form>
                        </div>
                        <div class="col-sm-4">
                            <div class="form-group">
                                <label for="exampleFormControlSelect1">Pincode</label>
                                <input type="PlantAeria" class="form-control" id="PlantAeria1" aria-describedby="emailHelp">
                            </div>
                        </div>
                        <div class="clearfix"></div>
                        <div class="col-sm-5">
                            <form>
                                <div class="form-group">
                                    <label for="exampleInputEmail1">Plant Aeria</label>
                                    <input type="PlantAeria" class="form-control" id="PlantAeria2" aria-describedby="emailHelp">
                                </div>
                            </form>
                        </div>
                        <div class="col-sm-7 text-left">
                            <div class="row">

                                <div class="col-sm-6">
                                    <form>
                                        <label for="exampleInputEmail1" class="fontW">No. Of Employer</label>
                                        <div class="form-group">

                                            <input type="PlantAeria" class="form-control" id="PlantAeria3" placeholder="Teachnical" aria-describedby="emailHelp">
                                        </div>
                                    </form>
                                </div>
                                <div class="col-sm-6">
                                    <form>
                                        <div class="form-group" style="margin-top: 25px;">

                                            <input type="PlantAeria" class="form-control" id="PlantAeria4" placeholder="
administrative"
                                                aria-describedby="emailHelp">
                                        </div>
                                    </form>
                                </div>
                                <div class="clearfix"></div>
                            </div>
                        </div>
                        <div class="clearfix"></div>
                        <div class="col-sm-5">
                            <form>
                                <div class="form-group">
                                    <label for="exampleInputEmail1">Latitude</label>
                                    <input type="email" class="form-control" id="Email41" aria-describedby="emailHelp">
                                </div>
                            </form>
                        </div>
                        <div class="col-sm-6">
                            <form>
                                <div class="form-group">
                                    <label for="exampleInputEmail1">Longitude</label>
                                    <input type="email" class="form-control" id="Email42" aria-describedby="emailHelp">
                                </div>
                            </form>
                        </div>
                        <div class="col-sm-1 text-right" style="padding-top: 25px;">
                            <button type="submit" class="btn btn-primary">Add</button>
                        </div>

                        <div class="clearfix"></div>
                        <div class="col-sm-12 text-right">

                            <div class="clearfix"></div>
                            <div class="table-responsive">
                                <table class="table">
                                    <thead class="tableHead">
                                        <tr>
                                            <th scope="col">S.No.</th>
                                            <th scope="col">Plant Name</th>
                                            <th scope="col">Address</th>
                                            <th scope="col">City</th>
                                            <th scope="col">District</th>
                                            <th scope="col">State</th>
                                            <th scope="col">Pincode</th>
                                            <th scope="col">Plant Aeria</th>
                                            <th scope="col">No. Of Employer</th>
                                            <th scope="col">Latitude</th>
                                            <th scope="col">Longitude</th>

                                        </tr>
                                    </thead>
                                    <tr>
                                        <td>1</td>
                                        <td>HAL</td>
                                        <td>Delhi</td>
                                        <td>Delhi</td>
                                        <td>Delhi</td>
                                        <td>Delhi</td>
                                        <td>10059</td>
                                        <td>Delhi</td>
                                        <td>333</td>
                                        <td>4444</td>
                                        <td>45553</td>

                                    </tr>
                                    <tr>
                                        <td>2</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                </table>

                            </div>

                        </div>
                        <div class="clearfix"></div>


                        <div class="col-sm-12">
                            <h4 style="border-bottom: 1px solid #fff; line-height: 40px; margin-top: -20px;">Product Manufactured</h4>
                            <div class="row">
                                <div class="col-sm-6">
                                    <form>
                                        <div class="form-group">
                                            <label for="exampleInputEmail1">Product Name</label>
                                            <input type="email" class="form-control" id="Email43" aria-describedby="emailHelp">
                                        </div>
                                    </form>
                                </div>
                                <div class="col-sm-6">
                                    <form>
                                        <div class="form-group">
                                            <label for="exampleInputEmail1">Annual Capacity</label>
                                            <input type="email" class="form-control" id="Email44" aria-describedby="emailHelp">
                                        </div>
                                    </form>
                                </div>

                                <div class="clearfix"></div>
                                <div class="col-sm-12 text-right">
                                    <div class="table-responsive">
                                        <table class="table">
                                            <thead class="tableHead">
                                                <tr>
                                                    <th scope="col">S.No.</th>
                                                    <th scope="col">Product Name</th>
                                                    <th scope="col">Annual Capacity</th>
                                                </tr>
                                            </thead>
                                            <tr>
                                                <td>1</td>
                                                <td>Tejas</td>
                                                <td>4</td>
                                            </tr>
                                            <tr>
                                                <td>2</td>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                            </tr>
                                        </table>

                                        <button type="submit" class="btn btn-primary">Add</button>
                                    </div>
                                    <br>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
