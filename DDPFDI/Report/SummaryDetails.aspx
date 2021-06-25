<%@ Page Title="" Language="C#" MasterPageFile="~/User/MasterPage.master" AutoEventWireup="true" CodeFile="SummaryDetails.aspx.cs" Inherits="Report_SummaryDetails" %>


<asp:Content ID="content1" ContentPlaceHolderID="head" runat="server">

    <style>
        #ContentPlaceHolder1_ddlcompany {
            display: flex;
            padding-left: 15px;
        }

            #ContentPlaceHolder1_ddlcompany span .box1 {
                padding: 12px 12px;
                margin-left: 15px;
            }

        .box2 {
            box-shadow: 0 0 5px;
        }
    </style>

</asp:Content>
<asp:Content ID="content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager runat="server" ID="sc"></asp:ScriptManager>
    <asp:UpdatePanel runat="server" ID="up" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="row d-flex justify-content-center">
                <div class="text-center">
                    <h4>Summary Details Report</h4>
                </div>
                <div class="clearfix"></div>
                <div class="col-lg-11 overflow-auto">
                    <p><span class="alert" style="color: red;">Note: For more information regarding functionalities please move cursor at respective functions</span></p>
                    <div class="row">
                        <div class="col-sm-2">
                            <label>Select Month</label>
                            <asp:DropDownList ID="ddlmonth" runat="server" AutoPostBack="true" CssClass="form-control" data-toggle="tooltip" ToolTip="Select Month to get month wise details" OnSelectedIndexChanged="ddlmonth_SelectedIndexChanged">
                                <asp:ListItem Value="0">Select</asp:ListItem>
                                <asp:ListItem Value="1">January</asp:ListItem>
                                <asp:ListItem Value="2">February</asp:ListItem>
                                <asp:ListItem Value="3">March</asp:ListItem>
                                <asp:ListItem Value="4">April</asp:ListItem>
                                <asp:ListItem Value="5">May</asp:ListItem>
                                <asp:ListItem Value="6">June</asp:ListItem>
                                <asp:ListItem Value="7">July</asp:ListItem>
                                <asp:ListItem Value="8">August</asp:ListItem>
                                <asp:ListItem Value="9">September</asp:ListItem>
                                <asp:ListItem Value="10">October</asp:ListItem>
                                <asp:ListItem Value="11">November</asp:ListItem>
                                <asp:ListItem Value="12">December</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-sm-3">
                            <label>Select Year</label>
                            <asp:DropDownList runat="server" ID="rbyear" CssClass="form-control" AutoPostBack="true" data-toggle="tooltip"
                                ToolTip="Select the Year for year wise details " OnSelectedIndexChanged="rbyear_SelectedIndexChanged">
                                <asp:ListItem Value="">Select</asp:ListItem>
                                <asp:ListItem Value="2020 or Myear='2019'">Previous Year</asp:ListItem>
                                <asp:ListItem style="margin-left: 10px;" Value="2020 or Myear='2021'">2020-21</asp:ListItem>
                                <asp:ListItem style="margin-left: 10px;" Value="2021 or Myear='2022'">2021-22</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-sm-2">
                            <asp:LinkButton ID="btnpdf" runat="server" class="btn btn-primary " style="margin-top:30px;" data-toggle="tooltip" ToolTip="Press Print Button to Get the Prinout of Report " OnClientClick="PrintPage();"><i class="fa fa-print"></i>&nbsp;Print</asp:LinkButton>
                        </div>
                    </div>
                    <div class="clearfix"></div>
                   <br />
                    <div id="divhome">
                        <asp:GridView runat="server" ID="gv_summary" Class="table table-hover table-bordered " ShowFooter="true" AutoGenerateColumns="False"
                            CellPadding="4" GridLines="None">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:TemplateField HeaderText="Sr.No." HeaderStyle-Width="50px">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Month" HeaderStyle-Width="60px">
                                    <HeaderTemplate>
                                        <asp:Label ID="Headerdmonth" data-toggle="tooltip" ToolTip="Display the month name " runat="server" Text="Month"></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblmonth" runat="server" data-toggle="tooltip" ToolTip="Display Month name" Text='<%# Eval("MntName") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item Indiginized" HeaderStyle-Width="60px">
                                    <HeaderTemplate>
                                        <asp:Label ID="Headerdmonth" data-toggle="tooltip" ToolTip="Display Indiginizated  " runat="server" Text="Item Indiginized"></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblIndig" runat="server" data-toggle="tooltip" ToolTip="Display Indiginizated" Text='<%# Eval("TotalProd") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Make-II" HeaderStyle-Width="60px">
                                    <HeaderTemplate>
                                        <asp:Label ID="Headerdakeii" data-toggle="tooltip" ToolTip="Display Make2 Sub Categories of Make In India" runat="server" Text="Make-II"></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblmakeii" runat="server" data-toggle="tooltip" ToolTip="Display Make2 Sub Categories of Make In India" Text='<%# Eval("MakeII") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Other Then Make-II" HeaderStyle-Width="60px">
                                    <HeaderTemplate>
                                        <asp:Label ID="Headerdmaketwo" data-toggle="tooltip" ToolTip="Display Other then make2 Sub Categories of Make In India" runat="server" Text="Other Then Make-II"></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblmaketwo" runat="server" data-toggle="tooltip" ToolTip="Display Other then make2 Sub Categories of Make In India" Text='<%# Eval("OtherThenMakeII") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="In-House" HeaderStyle-Width="60px">
                                    <HeaderTemplate>
                                        <asp:Label ID="Headerdotherthan" data-toggle="tooltip" ToolTip="Display In-house Sub Categories of Make In India" runat="server" Text="In-House"></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblotherthan" runat="server" data-toggle="tooltip" ToolTip="Display In-house Sub Categories of Make In India" Text='<%# Eval("InHouse") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Yet to be fill" HeaderStyle-Width="60px">
                                    <HeaderTemplate>
                                        <asp:Label ID="Headerdyettobe" data-toggle="tooltip" ToolTip="The pending product that DPSU user fill...." runat="server" Text="Yet to be fill"></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblyettobe" runat="server" data-toggle="tooltip" ToolTip="The pending product that DPSU user fill...." Text='<%# Eval("Yettobe") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EditRowStyle BackColor="#999999" />
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
        </Triggers>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress" runat="server" AssociatedUpdatePanelID="up">
        <ProgressTemplate>
            <div class="overlay-progress">
                <div class="custom-progress-bar blue stripes">
                    <span></span>
                    <p style="margin-left: 200px; padding-bottom: 10px;">
                        <b>Processing...</b>
                    </p>
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <script src="User/Uassets/js/bootstrap.bundle.min.js"></script>
    <script type="text/javascript">  
        function PrintPage() {
            var divContents = document.getElementById("divhome").innerHTML;
            var printWindow = window.open('', '', 'height=1000,width=1000');
            printWindow.document.write('<html><head><title>Success Story Data</title>');
            printWindow.document.write('</head><body >');
            printWindow.document.write(divContents);

            printWindow.document.write('</body></html>');
            printWindow.document.close();
            printWindow.print();
        }
    </script>
</asp:Content>

