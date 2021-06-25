<%@ Control Language="C#" AutoEventWireup="true" CodeFile="InnerMenu.ascx.cs" Inherits="Vendor_InnerMenu" %>
<ul class="nav nav-tabs" id="myTab" role="tablist">
    <li class="nav-item" role="presentation">
        <a runat="server" href="~/GeneralInformation" id="gitab" role="tab"><i class="fa fa-info"></i>&nbsp;General Info</a>
    </li>
    <li class="nav-item" role="presentation">
        <a class="nav-link" href="~/CompanyInformation_I" runat="server" id="ci1tab" role="tab"><i class="fa fa-building"></i>&nbsp;Company Info-I</a>
    </li>
    <li class="nav-item" role="presentation">
        <a class="nav-link" href="~/CompanyInformation_II" runat="server" id="ci2tab" role="tab"><i class="fa fa-building"></i>&nbsp;Company Info-II</a>
    </li>
    <li class="nav-item" role="presentation">
        <a class="nav-link" href="~/DetailsofDefenceStores" runat="server" id="ddstab" role="tab"><i class="fa fa-exclamation-triangle"></i>&nbsp;Details Defence Store</a>
    </li>
    <li class="nav-item" role="presentation">
        <a class="nav-link" href="~/FinancialInformation" runat="server" id="fitab" role="tab"><i class="fa fa-sticky-note"></i>&nbsp;Financial Info</a>
    </li>
    <li class="nav-item" role="presentation">
        <a class="nav-link" href="~/CheckList" runat="server" id="cltab" role="tab"><i class="fa fa-list"></i>&nbsp;Check-List</a>
    </li>
</ul>
