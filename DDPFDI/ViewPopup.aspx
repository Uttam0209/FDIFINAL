<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewPopup.aspx.cs" Inherits="ViewPopup" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="User/Uassets/css/bootstrap.min.css">
    <link rel="stylesheet" type="text/css" href="User/Uassets/css/theme.min.css">
    <link rel="stylesheet" type="text/css" href="User/Uassets\css\font-awesome-4.5.0\css\font-awesome.min.css">
    <link rel="stylesheet" type="text/css" href="User/Uassets/css/style.css">
</head>
<body>
    <form id="form1" runat="server">
        <div class="container-fluid">
            <div class="row" style="display: flex; justify-content: center; align-items: center; height: -webkit-fill-available;">
                <div class="col-md-9 p-4 box-shadow-lg">
                    <h4 class="modal-title product-title mb-3" style="font-size: 25px;">Import Item Details
                    </h4>
                    <div class="widget widget-categories mb-4">
                        <div class="accordion mt-n1" id="shop-categories">
                            <div id="printarea">
                                <div class="card" style="border-bottom: solid 1.4px #e5e5e5!important;">
                                    <div class="card-header">
                                        <h3 class="accordion-heading mb-2">
                                            <a class="collapsed" href="#shoes" role="button" data-toggle="collapse" aria-expanded="false" aria-controls="shoes">Item Description 
                          <span class="accordion-indicator iconupanddown">
                              <svg class="svg-inline--fa fa-chevron-up fa-w-14" aria-hidden="true" focusable="false" data-prefix="fas" data-icon="chevron-up" role="img" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 448 512" data-fa-i2svg="">
                                  <path fill="currentColor" d="M240.971 130.524l194.343 194.343c9.373 9.373 9.373 24.569 0 33.941l-22.667 22.667c-9.357 9.357-24.522 9.375-33.901.04L224 227.495 69.255 381.516c-9.379 9.335-24.544 9.317-33.901-.04l-22.667-22.667c-9.373-9.373-9.373-24.569 0-33.941L207.03 130.525c9.372-9.373 24.568-9.373 33.941-.001z"></path>
                              </svg>
                              <!-- <i class="fas fa-chevron-up"></i> -->
                          </span>
                                            </a>
                                        </h3>
                                    </div>
                                    <div class="collapse" id="shoes" data-parent="#shop-categories">
                                        <div class="card-body card-custom ">
                                            <h6 class="tablemidhead">DPSUs,OFB &amp; SHQs Details</h6>
                                            <table class="table mb-2">
                                                <tbody>
                                                    <tr>
                                                        <th scope="row">DPSU/OFB/SHQ:
                                                        </th>
                                                        <td>
                                                            <span id="lblcompname">MIDHANI</span>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                            <h6 class="tablemidhead">Item Description</h6>
                                            <table class="table mb-2">
                                                <tbody>
                                                    <tr>
                                                        <th scope="row">Item Id (Portal)
                                                        </th>
                                                        <td>
                                                            <span id="lblrefnoview">PRO0712</span>
                                                        </td>
                                                    </tr>
                                                    <tr id="Tr23" style="color: blue;">
                                                        <th>Item Name</th>
                                                        <td>
                                                            <span id="lblitemname1">NICKEL METAL</span>
                                                        </td>
                                                    </tr>
                                                    <tr id="three">
                                                        <th scope="row">DPSU Part Number
                                                        </th>
                                                        <td>
                                                            <span id="lbldpsupartno">RM02-001-00281</span>
                                                        </td>
                                                    </tr>
                                                    <tr id="four">
                                                        <th scope="row">HSN Code
                                                        </th>
                                                        <td>
                                                            <span id="lblhsncode8digit">75021000</span>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <th scope="row">Industry Domain
                                                        </th>
                                                        <td>
                                                            <span id="prodIndustryDomain">Foundry &amp; Casting</span>
                                                            /
                                   <span id="ProdIndusSubDomain">Permanent Mould Casting</span>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <h6 class="tablemidhead">OEM Details</h6>
                                                        </td>
                                                    </tr>
                                                    <tr id="nine">
                                                        <th scope="row">OEM Country
                                                        </th>
                                                        <td>
                                                            <span id="lbloemcountry">Norway</span>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <h6 class="tablemidhead">Item Classification (NATO Group &amp; Class)</h6>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <th scope="row">NATO Supply Group:
                                                        </th>
                                                        <td>
                                                            <span id="lblnsngroup">Ores, Minerals, and Their Primary Products(96)</span>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <th scope="row">NATO Supply Class:
                                                        </th>
                                                        <td>
                                                            <span id="lblnsngroupclass">Nonferrous Base Metal Refinery and Intermediate Forms(50)</span>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <th scope="row">Item Name Code:
                                                        </th>
                                                        <td>
                                                            <span id="lblclassitem">NICKEL,CATHODE(16073)</span>
                                                        </td>
                                                    </tr>
                                                    <tr id="six">
                                                        <th scope="row">NSC Code (4 digit):
                                                        </th>
                                                        <td>
                                                            <span id="lblnsccode4digit">9650</span>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                                <div class="card border-btm" style="border-bottom: solid 1.4px #e5e5e5!important;">
                                    <div class="card-header">
                                        <h3 class="accordion-heading mb-2">
                                            <a class="collapsed" href="#ItemSpecification" role="button" data-toggle="collapse" aria-expanded="false" aria-controls="shoes">Item Specification 
                          <span class="accordion-indicator iconupanddown">
                              <svg class="svg-inline--fa fa-chevron-up fa-w-14" aria-hidden="true" focusable="false" data-prefix="fas" data-icon="chevron-up" role="img" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 448 512" data-fa-i2svg="">
                                  <path fill="currentColor" d="M240.971 130.524l194.343 194.343c9.373 9.373 9.373 24.569 0 33.941l-22.667 22.667c-9.357 9.357-24.522 9.375-33.901.04L224 227.495 69.255 381.516c-9.379 9.335-24.544 9.317-33.901-.04l-22.667-22.667c-9.373-9.373-9.373-24.569 0-33.941L207.03 130.525c9.372-9.373 24.568-9.373 33.941-.001z"></path>
                              </svg>
                              <!-- <i class="fas fa-chevron-up"></i> -->
                          </span>
                                            </a>
                                        </h3>
                                    </div>
                                    <div class="collapse" id="ItemSpecification" data-parent="#shop-categories">
                                        <div class="card-body card-custom ">
                                            <table class="table mb-2">
                                                <tbody>
                                                    <tr id="eleven" style="color: blue;">
                                                        <th>Item Name</th>
                                                        <td>
                                                            <span id="itemname2">NICKEL METAL</span>
                                                        </td>
                                                    </tr>
                                                    <tr id="twentysix">
                                                        <th scope="row">Quality Assurance Agency 
                                                        </th>
                                                        <td>
                                                            <span id="lbqa">NIL</span>
                                                        </td>
                                                    </tr>
                                                    <tr id="fourteen">
                                                        <th scope="row">Features &amp; Details
                                                        </th>
                                                        <td>
                                                            <span id="lblfeaturesanddetail">
                                                                <p>Ni-99.90%min;Ag0.001%max;Co-0.05%max;Zn0.0015%max;As0.004%max;Al 0.001%max;C0.015%max;Bi0.0002%max;Cu 0.010%max;Se0.001%max;Fe 0.0 15%max;Mn 0.004%max;P0.002%max;Pb 0.001%max;S0.002%max;</p>
                                                            </span>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                                <div class="card border-btm" style="border-bottom: solid 1.4px #e5e5e5!important;">
                                    <div class="card-header">
                                        <h3 class="accordion-heading mb-2">
                                            <a class="collapsed" href="#Estimated" role="button" data-toggle="collapse" aria-expanded="false" aria-controls="shoes">Import Value, Quantity 
                          <span class="accordion-indicator iconupanddown">
                              <svg class="svg-inline--fa fa-chevron-up fa-w-14" aria-hidden="true" focusable="false" data-prefix="fas" data-icon="chevron-up" role="img" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 448 512" data-fa-i2svg="">
                                  <path fill="currentColor" d="M240.971 130.524l194.343 194.343c9.373 9.373 9.373 24.569 0 33.941l-22.667 22.667c-9.357 9.357-24.522 9.375-33.901.04L224 227.495 69.255 381.516c-9.379 9.335-24.544 9.317-33.901-.04l-22.667-22.667c-9.373-9.373-9.373-24.569 0-33.941L207.03 130.525c9.372-9.373 24.568-9.373 33.941-.001z"></path>
                              </svg>
                              <!-- <i class="fas fa-chevron-up"></i> -->
                          </span>
                                            </a>
                                        </h3>
                                    </div>
                                    <div class="collapse" id="Estimated" data-parent="#shop-categories">
                                        <div class="card-body card-custom ">
                                            <table class="table" width="100%">
                                                <tbody>
                                                    <tr id="fifteen">
                                                        <td>
                                                            <div>
                                                                <table class="table table-hover" cellspacing="0" rules="all" border="1" id="gvestimatequanorprice" style="border-collapse: collapse;">
                                                                    <tbody>
                                                                        <tr>
                                                                            <th scope="col">Year of Import</th>
                                                                            <th scope="col">Quantity</th>
                                                                            <th scope="col">Unit</th>
                                                                            <th scope="col">Import value in Rs lakh (Qty*Price)</th>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>2020-21</td>
                                                                            <td>1700</td>
                                                                            <td>tons(t)</td>
                                                                            <td>17000</td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table class="table mb-2">
                                                                <tbody>
                                                                    <tr id="five">
                                                                        <td colspan="2">
                                                                            <b>Import value during last 3 year (Rs lakhs) :</b>
                                                                            <span id="lblisproductimported"></span>&nbsp;&nbsp;
                                               &nbsp;<span id="lblvalueimport">24660.00</span>&nbsp;
                                                                        </td>
                                                                    </tr>
                                                                    <tr id="ten">
                                                                        <td colspan="2" style="border-top: 0px;">
                                                                            <div>
                                                                                <table cellspacing="0" rules="all" class="table table-responsive table-bordered" border="1" id="gvestimatequanold" style="border-collapse: collapse;">
                                                                                    <tbody>
                                                                                        <tr>
                                                                                            <th scope="col">Year of Import</th>
                                                                                            <th scope="col">Quantity</th>
                                                                                            <th scope="col">Unit</th>
                                                                                            <th scope="col">Imported value in Rs lakh (Qty*Price)</th>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td>2019-20</td>
                                                                                            <td>1315</td>
                                                                                            <td>tons(t)</td>
                                                                                            <td>11860</td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td>2018-19</td>
                                                                                            <td>1100</td>
                                                                                            <td>tons(t)</td>
                                                                                            <td>10200</td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td>2017-18</td>
                                                                                            <td>300</td>
                                                                                            <td>tons(t)</td>
                                                                                            <td>2600</td>
                                                                                        </tr>
                                                                                    </tbody>
                                                                                </table>
                                                                            </div>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                            <h6 class="tablemidhead">Status of Indigenization</h6>
                                            <table class="table mb-2">
                                                <tbody>
                                                    <tr id="sixteen">
                                                        <th scope="row">Make in India Category
                                                        </th>
                                                        <td>
                                                            <span id="lblindicate">OTHER THAN MAKE-II</span>
                                                        </td>
                                                    </tr>
                                                    <tr id="seventeen">
                                                        <th scope="row">EoI/RFP
                                                        </th>
                                                        <td>
                                                            <span id="lbleoirep">No</span>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                            <h6 class="tablemidhead">Contact Details</h6>
                                            <table id="nineteen" class="table mb-2">
                                                <tbody>
                                                    <tr>
                                                        <th scope="row">Name
                                                        </th>
                                                        <td>
                                                            <span id="lblempname">Mr K Rajkumar</span>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <th scope="row">Designation
                                                        </th>
                                                        <td>
                                                            <span id="lbldesignation">DGM (MATERIALS)</span>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <th scope="row">E-Mail ID
                                                        </th>
                                                        <td>
                                                            <span id="lblemailidpro">rajkumar@midhani-india.in</span>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <th scope="row">Phone Number
                                                        </th>
                                                        <td>
                                                            <span id="lblphonenumber"></span>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                                <div class="card border-btm" style="border-bottom: solid 1.4px #e5e5e5!important;">
                                    <div class="card-header">
                                        <h3 class="accordion-heading mb-2">
                                            <a class="collapsed" href="#AdditionalValue" role="button" data-toggle="collapse" aria-expanded="false" aria-controls="shoes">Additional Details 
                          <span class="accordion-indicator iconupanddown">
                              <svg class="svg-inline--fa fa-chevron-up fa-w-14" aria-hidden="true" focusable="false" data-prefix="fas" data-icon="chevron-up" role="img" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 448 512" data-fa-i2svg="">
                                  <path fill="currentColor" d="M240.971 130.524l194.343 194.343c9.373 9.373 9.373 24.569 0 33.941l-22.667 22.667c-9.357 9.357-24.522 9.375-33.901.04L224 227.495 69.255 381.516c-9.379 9.335-24.544 9.317-33.901-.04l-22.667-22.667c-9.373-9.373-9.373-24.569 0-33.941L207.03 130.525c9.372-9.373 24.568-9.373 33.941-.001z"></path>
                              </svg>
                              <!-- <i class="fas fa-chevron-up"></i> -->
                          </span>
                                            </a>
                                        </h3>
                                    </div>
                                    <div class="collapse" id="AdditionalValue" data-parent="#shop-categories">
                                        <div class="card-body card-custom ">
                                            <table class="table mb-2">
                                                <tbody>
                                                    <tr id="twenty">
                                                        <th scope="row">End User 
                                                        </th>
                                                        <td>
                                                            <span id="lblenduser">INDIAN AIR FORCE, DPSU/OFB</span>
                                                        </td>
                                                    </tr>
                                                    <tr id="twentyone">
                                                        <th scope="row">Defence Paltform 
                                                        </th>
                                                        <td>
                                                            <span id="lbldefenceplatform">OTHER</span>
                                                        </td>
                                                    </tr>
                                                    <tr id="twentytwo">
                                                        <th scope="row">Name of Defence Platform 
                                                        </th>
                                                        <td>
                                                            <span id="lblnameofdefplat">Other</span>
                                                        </td>
                                                    </tr>
                                                    <tr id="twentythree">
                                                        <th scope="row"></th>
                                                        <td>
                                                            <span id="lbldeclaration">No IPR issue, No violation of TOT agreement, No violation of Security Concern</span>
                                                        </td>
                                                    </tr>
                                                    <tr id="twentyfour">
                                                        <th scope="row"></th>
                                                        <td>
                                                            <span id="lblisshowgeneral">Yes</span>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <input id="btnprint" type="button" onclick="PrintDiv()" style="width: 70px;" class="btn btn-primary  pull-right" value="Print">
                </div>
            </div>
        </div>
        <script src="User/Uassets/js/jquery-3.4.1.min.js"></script>
        <script src="User/Uassets/js/all.min.js"></script>
        <script src="User/Uassets/js/bootstrap.bundle.min.js"></script>
        <script src="User/Uassets/js/theme.min.js"></script>
        <script type="text/javascript">
            function PrintDiv() {
                var divToPrint = document.getElementById('printarea');
                var popupWin = window.open('', '_blank', 'width=300,height=400,location=no,left=200px');
                popupWin.document.open();
                popupWin.document.write('<html><body onload="window.print()">' + divToPrint.innerHTML + '</html>');
                popupWin.document.close();
            }
        </script>
    </form>

</body>
</html>
