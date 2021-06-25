<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HowToSearch.aspx.cs" MasterPageFile="~/User/MasterPage.master" Inherits="User_HowToSearch" %>


<asp:Content ID="content1" ContentPlaceHolderID="head" runat="server">
    <style>
        #more {
            display: none;
        }
    </style>

    <script> 
        var vid = document.getElementById("myVideo");

        function playVid() {
            vid.play();
        }

        function pauseVid() {
            vid.pause();
        }
    </script>

</asp:Content>
<asp:Content ID="content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="col-12">
        <div class="py-2">
            <asp:Button ID="btnvideo" runat="server" Text="view video" CssClass="btn btn-primary btn-sm pull-right" />
        </div>
    </div>
    <div class="clearfix">
        <div class="row">
            <b>Video</b>
            <div>
                <video width="100%" height="150" autoplay controls>
                    <source src="../Videos/how%20to%20search.mp4" autoplay="1" type="video/mp4">
                </video>
            </div>
        </div>
        <div class="row">
            <div class="col-12">
                <h5 class="py-2">How to Search</h5>
                <div class="clearfix"></div>
                <br />
                <p>There are <b>three ways</b> to search product(s) of your interest on srijandefence portal.</p>
                <br />
                <p><b>Search by typing minimum three characters. </p>
                </b>
                                        
                                        <p>(i)&nbsp;&nbsp;&nbsp;&nbsp; <b>Universal Search on the Top.</b></p>
                <img src="/User/Uassets/Images/img1-1.jpg" />
                <p>--------------------------------------------------------------------------------------------------</p>
                <p>
                    (ii.)<b>&nbsp;NATO classification based search in the left column</b>
                </p>
                <p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<i>In three steps.</i></p>
                <p>
                </p>
                <p>
                    <b>Step1 : drop down menu</b>- NATO Supply Group
                </p>
                <p>
                    Select “Electrical and Electronic Equipment Components (59)” which is shown in below image.
                </p>
                <img src="/User/Uassets/Images/img2-1.jpg" />
                <p>
                    <b>Step2 :drop down menu</b> -Nato Supply Class
                </p>
                <p>For example: Search Electronic Item like: <i>Resistor</i></p>
                <p>
                    <i>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Select“Resistor (05)” </i>.
                </p>
                <img src="/User/Uassets/Images/img2-2.jpg" />

                <p>
                    <b>Step3 : drop down menu</b> – Item Code
                </p>
                <p>
                    <i>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Select“Resistor, Fixed (A2178)”</i>
                </p>
                <img src="/User/Uassets/Images/img2-3.jpg" />
                <p>--------------------------------------------------------------------------------------------------</p>
                <p>
                    <b>(iii).Simple way defined “Industry Domain” and “Industry Sub-Domain” search</b>
                </p>
                <p>
                    <b>Step 1 - drop down menu</b>- Industry Domain
                </p>
                <p>
                    <i>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Select“Electronics”</i>
                </p>
                <img src="/User/Uassets/Images/img3-1.jpg" />
                <p>
                    <b>Step2 : drop down menu</b> – Industry Sub-Domain
                </p>
                <p>
                    <i>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Select “Resistor”</i>
                </p>
                <img src="/User/Uassets/Images/img3-2.jpg" />
                <p>--------------------------------------------------------------------------------------------------</p>

            </div>
        </div>
    </div>
    <script src="User/Uassets/js/jquery.min.js"></script>
    <script>
        function myFunction() {
            var dots = document.getElementById("dots");
            var moreText = document.getElementById("more");
            var btnText = document.getElementById("myBtn");

            if (dots.style.display === "none") {
                dots.style.display = "inline";
                btnText.innerHTML = "Read more";
                moreText.style.display = "none";
            } else {
                dots.style.display = "none";
                btnText.innerHTML = "Read less";
                moreText.style.display = "inline";
            }
        }
    </script>
</asp:Content>
