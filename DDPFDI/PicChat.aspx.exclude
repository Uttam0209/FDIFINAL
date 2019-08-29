<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PicChat.aspx.cs" Inherits="PicChat" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <%-- Here We need to write some js code for load google chart with database data --%>
    <script src="assets/js/jquery-1.7.1.js"></script>
    <script type="text/javascript" src="https://www.google.com/jsapi"></script>

    <script>
        var chartData; // globar variable for hold chart data
        google.load("visualization", "1", { packages: ["corechart"] });

        // Here We will fill chartData

        $(document).ready(function () {

            $.ajax({
                url: "PicChat.aspx/GetChartData",
                data: "",
                dataType: "json",
                type: "POST",
                contentType: "application/json; chartset=utf-8",
                success: function (data) {
                    chartData = data.d;
                },
                error: function () {
                    alert("Error loading data! Please try again.");
                }
            }).done(function () {
                // after complete loading data
                google.setOnLoadCallback(drawChart);
                drawChart();
            });
        });

        function drawChart() {
            var data = google.visualization.arrayToDataTable(chartData);

            var options = {
                title: "Company Performance Product Category Wise",
                pointSize: 5,
                seriesType:"bars",
                series: { 3: {type: "line"}}
            };

           var pieChart = new google.visualization.PieChart(document.getElementById('chart_div'));
            pieChart.draw(data, options);

            var barChart = new google.visualization.BarChart(document.getElementById('chart_div1'));
            barChart.draw(data, options);

            var comboChart = new google.visualization.ComboChart(document.getElementById('chart_div2'));
            comboChart.draw(data, options);

            var lineChart = new google.visualization.ComboChart(document.getElementById('chart_div3'));
            lineChart.draw(data, options);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server" method="post" asp-antiforgery="false">
    <div>
    <div id="chart_div" style="width:800px;height:600px">
                <%-- Here Chart Will Load --%>
                                                 
            </div>

        <div id="chart_div1" style="width:800px;height:600px">
                <%-- Here Chart Will Load --%>
                                                 
            </div>

         <div id="chart_div2" style="width:800px;height:600px">
                <%-- Here Chart Will Load --%>
                                                 
            </div>

        <div id="chart_div3" style="width:800px;height:600px">
                <%-- Here Chart Will Load --%>
                                                 
            </div>
    </div>
    </form>
</body>
</html>
